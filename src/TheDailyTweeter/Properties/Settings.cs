using Membus.WpfTwitterClient.Frame;
using Membus.WpfTwitterClient.Frame.Config;

namespace TheDailyTweeter.Properties
{
    internal partial class Settings //:IUserSettings
    {
        public void StoreAccessToken(TwitterAccessToken token)
        {
            //AccessToken = token;
            Save();
        }
    }
}