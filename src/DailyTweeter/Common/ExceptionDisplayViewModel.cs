using System;
using SoftLattice.Common;

namespace DailyTweeter.Common
{
    public class ExceptionDisplayViewModel
    {
        private readonly Exception _exception;
        public string ExceptionType { get { return _exception.GetType().Name; } }
        public string ExceptionMessage { get { return _exception.Message; } }

        public ExceptionDisplayViewModel(ActivateViewModelMsg msg)
        {
            _exception = ((ExceptionInteractionMessage) msg).Exception;
        }
    }
}