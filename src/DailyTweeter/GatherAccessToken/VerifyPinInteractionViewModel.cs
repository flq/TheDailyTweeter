using System;
using SoftLattice.Common;

namespace DailyTweeter.GatherAccessToken
{
    public class VerifyPinInteractionViewModel : IInteractionModel
    {
        private readonly IPublishMessage _publisher;
        private readonly string possibleVerifier;

        public VerifyPinInteractionViewModel(ActivateViewModelMsg msg, IPublishMessage publisher)
        {
            _publisher = publisher;
            possibleVerifier = ((InteractToVerifyPinMsg)msg).IdentifiedVerifier;
            
        }

        public string PossibleVerifier
        {
            get { return possibleVerifier; }
        }

        public string VerifierOverride { get; set; }

        public void CaptureIsGood()
        {
            _publisher.Publish(new GetAccessTokenMsg(PossibleVerifier));
            if (Closed != null) Closed(this, EventArgs.Empty);
        }

        public void UseUserPin()
        {
            _publisher.Publish(new GetAccessTokenMsg(VerifierOverride));
            if (Closed != null) Closed(this, EventArgs.Empty);
        }

        public void Clicked()
        {
            // Main Button clicked.
        }

        public event EventHandler Closed;
    }
}