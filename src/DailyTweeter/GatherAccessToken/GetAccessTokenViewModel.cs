using System;
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

        public void TwitterLoadCompleted(NavigationEventArgs e)
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
            _session.GetAuthorizationUrl(OnAuthorizationuriAvailable);
        }

        public void TwitterWebsiteNavigating(NavigatingCancelEventArgs args)
        {
            _publisher.ActivityStarts("Loading Twitter screen");
        }

        private void OnAuthorizationuriAvailable(Uri uri)
        {
            _dispatchServices.EnsureActionOnDispatcher(()=>_loadUri(uri));
        }
        
    }
}