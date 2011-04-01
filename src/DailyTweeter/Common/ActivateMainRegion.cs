using SoftLattice.Common;

namespace DailyTweeter.Common
{
    public class ActivateMainRegion<T> : ActivateRegionMsg
    {
        public ActivateMainRegion() : base("Main", "DailyTweeter", typeof(T))
        {
        }
    }
}