using SoftLattice.Common;

namespace DailyTweeter.Common
{
    public static class Helpfulextensions
    {
        public static void ActivityStarts(this IPublishMessage publisher, string message)
        {
            publisher.Publish(new ActivityMsg(message));
        }

        public static void ActivityEnds(this IPublishMessage publisher)
        {
            publisher.Publish(new ActivityMsg());
        }
    }
}