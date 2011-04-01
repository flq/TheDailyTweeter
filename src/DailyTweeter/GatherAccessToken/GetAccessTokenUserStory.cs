using System;
using System.Text.RegularExpressions;
using DailyTweeter.Common;
using DailyTweeter.Twitter;
using MemBus.Subscribing;
using SoftLattice.Common;

namespace DailyTweeter.GatherAccessToken
{
    public class GetAccessTokenUserStory : IAcceptDisposeToken
    {
        private static readonly Regex verifier = new Regex(@"\d\d\d\d\d\d\d", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly IPublishMessage _publisher;
        private readonly IApplicationStorage _store;
        private readonly ITwitterSession _session;
        private readonly UserSettings _settings;
        private IDisposable _disposeToken;

        public GetAccessTokenUserStory(IPublishMessage publisher, IApplicationStorage store, ITwitterSession session, UserSettings settings)
        {
            _publisher = publisher;
            _session = session;
            _settings = settings;
            _store = store;
        }

        public void Handle(ActivateGetAccessTokenUserStoryMsg msg)
        {
            var token = _store.Get<TwitterAccessToken>("AccessToken");
            //var msg = token == null
            //              ? new ActivateMainRegion<GetAccessTokenViewModel>()
            //              : new ActivatePluginMsg(typeof (object), "Gather Access token");
            _publisher.Publish(new ActivateMainRegion<GetAccessTokenViewModel>());
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
            _session.GetAccessToken(msg.Pin, token =>
            {
                _settings.StoreAccessToken(token);
                //TODO: Publish message to load the main twitter view
                _disposeToken.Dispose();
            });
        }

        public void Accept(IDisposable disposeToken)
        {
            _disposeToken = disposeToken;
        }
    }
}