using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using DailyTweeter.Twitter;
using SoftLattice.Common;
using DailyTweeter.Common;

namespace DailyTweeter.GatherAccessToken
{
    public class GetAccessTokenViewModel : Screen
    {
        private readonly ITwitterSession _session;
        private readonly IPublishMessage _publisher;
        private readonly IDispatchServices _dispatchServices;
        private Action<Uri> _loadUri;
        private Func<string> _loadedContents;

        public GetAccessTokenViewModel(ITwitterSession session, IPublishMessage publisher, IDispatchServices dispatchServices)
        {
            DisplayName = "Authorize Membus OnTweet to interact with Twitter on your behalf";
            _session = session;
            _publisher = publisher;
            _dispatchServices = dispatchServices;
        }

        public void TwitterLoadCompleted()
        {
            var s = _loadedContents();
            _publisher.ActivityEnds();
            _publisher.Publish(new ScanContentForVerifierMsg(s));
            
        }

        public void BrowserLoaded(WebBrowser browser)
        {
            _publisher.ActivityStarts("Visiting Twitter to obtain an access token");
            // Source property cannot be bound, which is why we have to do silly things here
            _loadUri = uri => browser.Source = uri;
            _loadedContents = () =>
                                 {
                                     var b = browser.Document as dynamic;
                                     return (string) b.documentElement.innerText;
                                 };

            var authorizeTask = _session.GetAuthorizationUrl();
            authorizeTask.ContinueWith(OnAuthorizationuriAvailable);
            authorizeTask.Start();
        }

        public void TwitterWebsiteNavigating()
        {
            _publisher.ActivityStarts("Loading Twitter screen");
        }

        private void OnAuthorizationuriAvailable(Task<Uri> uriCreationTask)
        {
            var uri = uriCreationTask.Result;
            _dispatchServices.EnsureActionOnDispatcher(()=>_loadUri(uri));
        }
        
    }
}