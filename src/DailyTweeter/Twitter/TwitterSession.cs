using System;
using System.Threading.Tasks;
using DailyTweeter.Common;
using SoftLattice.Common;
using Twitterizer;

namespace DailyTweeter.Twitter
{
    public class TwitterSession : ITwitterSession
    {
        private readonly TwitterKeys _keys;
        private readonly UserSettings _userSettings;

        private string requestToken;

        readonly TaskFactory taskFactory = new TaskFactory();

        public TwitterSession(TwitterKeys keys, UserSettings userSettings)
        {
            _keys = keys;
            _userSettings = userSettings;
        }

        public void GetAuthorizationUrl(Action<Uri> authorizationUriAvailable)
        {
            taskFactory.StartNew(() =>
                                     {
                                         var response = OAuthUtility.GetRequestToken(_keys.ConsumerKey,
                                                                                     _keys.ConsumerSecret, "oob");
                                         requestToken = response.Token;
                                         var uri = OAuthUtility.BuildAuthorizationUri(requestToken);
                                         authorizationUriAvailable(uri);
                                     });
        }

        public void GetAccessToken(string verifyCode, Action<TwitterAccessToken> accessTokenAvailable)
        {
            taskFactory.StartNew(() =>
                {
                    var response = OAuthUtility.GetAccessToken(_keys.ConsumerKey, _keys.ConsumerSecret, requestToken, verifyCode);
                    var twitterAccessToken = new TwitterAccessToken { Secret = response.TokenSecret, Token = response.Token };
                    _userSettings.StoreAccessToken(twitterAccessToken);
                    accessTokenAvailable(twitterAccessToken);
                });
        }

        public void LoadHomeTimeline(Action<TwitterResponse<TwitterStatusCollection>> timelineAvailable)
        {
            taskFactory.StartNew(() =>
                                     {
                                         var statuses = TwitterTimeline.HomeTimeline(getOAuthToken());
                                         timelineAvailable(statuses);
                                     });
        }

        private OAuthTokens getOAuthToken()
        {
          
            if (!_userSettings.IsAccessTokenAvailable)
                throw new InvalidOperationException("By some reason no access token is available, this is an invalid state of the application.");
            var token = _userSettings.AccessToken;
            return new OAuthTokens
                       {
                           AccessToken = token.Token,
                           AccessTokenSecret = token.Secret,
                           ConsumerKey = _keys.ConsumerKey,
                           ConsumerSecret = _keys.ConsumerSecret
                       };
        }
    }
}