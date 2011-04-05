using System;
using System.Threading.Tasks;
using Twitterizer;

namespace DailyTweeter.Twitter
{
    public interface ITwitterSession
    {
        Task<Uri> GetAuthorizationUrl();
        Task<TwitterAccessToken> GetAccessToken(string verifyCode);
        Task<TwitterResponse<TwitterStatusCollection>> LoadHomeTimeline();
    }
}