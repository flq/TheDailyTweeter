using System;
using SoftLattice.Common;

namespace DailyTweeter
{
    public class Store
    {
        private readonly IApplicationStorage _storage;

        public Store(IApplicationStorage storage)
        {
            _storage = storage;
        }

        public bool IsAccessTokenAvailable
        {
            get { return _storage.Get<string>("DailyTweeter.AccessToken") != null; }
        }
    }
}