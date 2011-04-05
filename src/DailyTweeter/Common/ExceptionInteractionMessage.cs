using System;
using DailyTweeter.Timeline;
using SoftLattice.Common;

namespace DailyTweeter.Common
{
    [Message]
    public class ExceptionInteractionMessage : ActivateInteractionMsg
    {
        public Exception Exception { get; private set; }

        public ExceptionInteractionMessage(Exception exception) : base(typeof(ExceptionDisplayViewModel), InteractionKind.Error)
        {
            Exception = exception;
        }
    }
}