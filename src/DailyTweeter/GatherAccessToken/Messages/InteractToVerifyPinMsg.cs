using System;
using SoftLattice.Common;

namespace DailyTweeter.GatherAccessToken
{
    public class InteractToVerifyPinMsg : ActivateInteractionMsg
    {
        private readonly string _identifiedVerifier;

        public InteractToVerifyPinMsg(string identifiedVerifier) : base(typeof(VerifyPinInteractionViewModel), InteractionKind.Warning)
        {
            _identifiedVerifier = identifiedVerifier;
        }

        public string IdentifiedVerifier
        {
            get { return _identifiedVerifier; }
        }
    }
}