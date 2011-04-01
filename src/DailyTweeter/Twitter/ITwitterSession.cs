using System;
using Twitterizer;

namespace DailyTweeter.Twitter
{
    public interface ITwitterSession
    {
        void GetAuthorizationUrl(Action<Uri> authorizationUriAvailable);
        void GetAccessToken(string verifyCode, Action<TwitterAccessToken> accessTokenAvailable);
        void LoadHomeTimeline(Action<TwitterResponse<TwitterStatusCollection>> timelineAvailable);
    }
}