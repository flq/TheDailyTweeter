using System;
using SoftLattice.Common;
using SoftLattice.Enhancements.Models;

namespace DailyTweeter
{
    public class DailyTweeterViewModel : TemplatingConductor
    {
        public DailyTweeterViewModel(IObservable<ActivateRegionMsg> activateRegionMessages) : base(activateRegionMessages, "DailyTweeter")
        {
        }

        public object Main { get; private set; }
    }
}