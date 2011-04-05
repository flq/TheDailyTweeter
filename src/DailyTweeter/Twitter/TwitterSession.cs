using System;
using System.Threading.Tasks;
using DailyTweeter.Common;
using Twitterizer;

namespace DailyTweeter.Twitter
{
    public class TwitterSession : ITwitterSession
    {
        private readonly TwitterKeys _keys;
        private readonly UserSettings _userSettings;

        private string requestToken;

        public TwitterSession(TwitterKeys keys, UserSettings userSettings)
        {
            _keys = keys;
            _userSettings = userSettings;
        }

        public Task<Uri> GetAuthorizationUrl()
        {
            return new Task<Uri>(() =>
                {
                    var response1 = OAuthUtility.GetRequestToken(_keys.ConsumerKey,
                                                                _keys.ConsumerSecret, "oob");
                    requestToken = response1.Token;
                    var uri1 = OAuthUtility.BuildAuthorizationUri(requestToken);
                    return uri1;
                });
        }

        public Task<TwitterAccessToken> GetAccessToken(string verifyCode)
        {
            return new Task<TwitterAccessToken>(() =>
                {
                    var response = OAuthUtility.GetAccessToken(_keys.ConsumerKey, _keys.ConsumerSecret, requestToken, verifyCode);
                    var twitterAccessToken = new TwitterAccessToken { Secret = response.TokenSecret, Token = response.Token };
                    _userSettings.StoreAccessToken(twitterAccessToken);
                    return twitterAccessToken;
                });
        }

        public Task<TwitterResponse<TwitterStatusCollection>> LoadHomeTimeline()
        {
            return new Task<TwitterResponse<TwitterStatusCollection>>(() =>
                                     {
                                         var statuses = TwitterTimeline.HomeTimeline(getOAuthToken());
                                         return statuses;
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