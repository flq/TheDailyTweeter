using SoftLattice.Common;

namespace DailyTweeter.GatherAccessToken
{
    [Message]
    public class GetAccessTokenMsg
    {
        private readonly string _pin;

        public GetAccessTokenMsg(string pin)
        {
            _pin = pin;
        }

        public string Pin
        {
            get { return _pin; }
        }
    }
}