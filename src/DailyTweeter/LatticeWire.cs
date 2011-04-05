using DailyTweeter.Common;
using DailyTweeter.GatherAccessToken;
using DailyTweeter.Timeline;
using DailyTweeter.Twitter;
using SoftLattice.Common;

namespace DailyTweeter
{
    public class LatticeWire : ILatticeGroup
    {
        private readonly IPublishMessage _publisher;
        


        public LatticeWire(IPublishMessage publisher)
        {
            _publisher = publisher;
        }

        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(this);
            wiring.AddResources(s=>s.Contains("resources"));
            wiring.RegisterSingleService<UserSettings,UserSettings>();
            wiring.RegisterSingleService<ITwitterSession,TwitterSession>();
            wiring.RegisterMessageListener<GetAccessTokenUserHub>();
            wiring.RegisterMessageListener<TwitterInteractionHub>();
            wiring.GetFromAppConfig<TwitterKeys>();
        }

        public void Handle(StartupMsg message)
        {
            _publisher.Publish(new ActivatePluginMsg(typeof(DailyTweeterViewModel), "The Daily Tweeter."));
            _publisher.Publish(new ActivateGetAccessTokenUserStoryMsg());
        }
    }
}