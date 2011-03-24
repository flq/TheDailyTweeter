using SoftLattice.Common;

namespace DailyTweeter
{
    public class LatticeWire : ILatticeGroup
    {
        private readonly IPublishMessage _publisher;
        private readonly Store _store;

        public LatticeWire(IPublishMessage publisher, Store store)
        {
            _publisher = publisher;
            _store = store;
        }

        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(this);
        }

        public void Handle(StartupMsg message)
        {
            var msg = _store.IsAccessTokenAvailable
                          ? new ActivatePluginMsg(typeof (object), "Twitter timeline")
                          : new ActivatePluginMsg(typeof (object), "Gather Access token");
            _publisher.Publish(msg);
        }
    }
}