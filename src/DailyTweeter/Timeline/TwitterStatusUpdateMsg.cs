using System.Collections.Generic;
using Twitterizer;

namespace DailyTweeter.Timeline
{
    public class TwitterStatusUpdateMsg
    {
        public IEnumerable<TwitterStatus> Statuses { get; private set; }

        public TwitterStatusUpdateMsg(IEnumerable<TwitterStatus> statuses)
        {
            Statuses = statuses;
        }
    }
}