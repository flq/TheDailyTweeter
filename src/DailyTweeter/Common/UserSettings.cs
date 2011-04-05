using System;
using DailyTweeter.Twitter;
using SoftLattice.Common;

namespace DailyTweeter.Common
{
    public class UserSettings
    {
        private readonly Func<IApplicationStorage> _storageLoader;
        private readonly Func<TwitterAccessToken> _token;

        public UserSettings(Func<IApplicationStorage> storageLoader)
        {
            _storageLoader = storageLoader;
            _token = ()=> _storageLoader().Get<TwitterAccessToken>("AccessToken");
        }

        public bool IsAccessTokenAvailable { get { return _token() != null; } }

        public TwitterAccessToken AccessToken { get { return _token(); } }

        public void StoreAccessToken(TwitterAccessToken token)
        {
            _storageLoader().Put("AccessToken", token);
        }
    }
}