using DailyTweeter.Common;
using DailyTweeter.GatherAccessToken;
using DailyTweeter.Twitter;
using MemBus.Messages;
using SoftLattice.Common;

namespace DailyTweeter.Timeline
{
    public class TwitterInteractionHub
    {
        private readonly ITwitterSession _session;
        private readonly IPublishMessage _publisher;

        public TwitterInteractionHub(ITwitterSession session, IPublishMessage publisher)
        {
            _session = session;
            _publisher = publisher;
        }

        public void Handle(GetTwitterStatusMsg msg)
        {
            var homeTimeline = _session.LoadHomeTimeline();
            homeTimeline.ContinueWith(task => _publisher.Publish(new TwitterStatusUpdateMsg(task.Result.ResponseObject)));
            homeTimeline.Start();
        }

        public void Handle(ExceptionOccurred msg)
        {
            _publisher.Publish(new ExceptionInteractionMessage(msg.Exception));
        }
    }
}