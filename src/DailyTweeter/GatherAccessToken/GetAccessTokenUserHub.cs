using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyTweeter.Common;
using DailyTweeter.Timeline;
using DailyTweeter.Twitter;
using MemBus.Messages;
using MemBus.Subscribing;
using SoftLattice.Common;

namespace DailyTweeter.GatherAccessToken
{
    public class GetAccessTokenUserHub : IAcceptDisposeToken
    {
        private static readonly Regex verifier = new Regex(@"\d\d\d\d\d\d\d", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly IPublishMessage _publisher;
        private readonly ITwitterSession _session;
        private readonly UserSettings _settings;
        private IDisposable _disposeToken;

        public GetAccessTokenUserHub(IPublishMessage publisher, ITwitterSession session, UserSettings settings)
        {
            _publisher = publisher;
            _session = session;
            _settings = settings;
        }

        public void Handle(ActivateGetAccessTokenUserStoryMsg msg)
        {
            var next = _settings.IsAccessTokenAvailable
                          ? (object)new ActivateMainRegion<TimelinesViewModel>()
                          : new ActivateMainRegion<GetAccessTokenViewModel>();
            _publisher.Publish(next);
        }

        public void Handle(ScanContentForVerifierMsg msg)
        {
             var match = verifier.Match(msg.Content);
            if (match.Captures.Count == 0)
                return;
            var possibleVerifier = match.Captures[0].Value;
            _publisher.Publish(new InteractToVerifyPinMsg(possibleVerifier));
        }

        public void Handle(GetAccessTokenMsg msg)
        {
            var getAccessToken = _session.GetAccessToken(msg.Pin);
            getAccessToken.ContinueWith(AccessTokenAvailable);
            getAccessToken.Start();
        }

        public void Accept(IDisposable disposeToken)
        {
            _disposeToken = disposeToken;
        }

        private void AccessTokenAvailable(Task<TwitterAccessToken> task)
        {
            try
            {
                _settings.StoreAccessToken(task.Result);
                _publisher.Publish(new ActivateMainRegion<TimelinesViewModel>());
                _disposeToken.Dispose();
            }
            catch (AggregateException x)
            {
                foreach (var ex in x.InnerExceptions)
                    _publisher.Publish(new ExceptionOccurred(ex));
            }
        }
    }
}