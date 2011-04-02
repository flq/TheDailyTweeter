using DailyTweeter.Twitter;
using SoftLattice.Common;

namespace DailyTweeter.Timeline
{
    public class TwitterInteractionController
    {
        private readonly ITwitterSession _session;
        private readonly IPublishMessage _publisher;

        public TwitterInteractionController(ITwitterSession session, IPublishMessage publisher)
        {
            _session = session;
            _publisher = publisher;
        }

        public void Handle(GetTwitterStatusMsg msg)
        {
            _session.LoadHomeTimeline(response => _publisher.Publish(new TwitterStatusUpdateMsg(response.ResponseObject)));
        }
    }
}