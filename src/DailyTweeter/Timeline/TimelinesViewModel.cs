using System;
using System.Collections.ObjectModel;
using System.Linq;
using DailyTweeter.Common;
using MemBus.Support;
using SoftLattice.Common;
using Twitterizer;

namespace DailyTweeter.Timeline
{
    public class TimelinesViewModel
    {
        private readonly IPublishMessage _publisher;
        private readonly IDisposable _streamDispose;

        public TimelinesViewModel(IPublishMessage publisher, IObservable<TwitterStatusUpdateMsg> streamOfTwitterStatuses)
        {
            _publisher = publisher;
            _streamDispose = streamOfTwitterStatuses.ObserveOnDispatcher().Subscribe(OnNewTwitterStatuses);
            Statuses = new ObservableCollection<TwitterStatus>();
            
        }

        public ObservableCollection<TwitterStatus> Statuses
        {
            get;
            private set;
        }

        public void Loaded()
        {
            AskForNewStatuses();
        }

        private void AskForNewStatuses()
        {
            _publisher.ActivityStarts("Retrieving statuses");
            _publisher.Publish(new GetTwitterStatusMsg());
        }

        private void OnNewTwitterStatuses(TwitterStatusUpdateMsg statuses)
        {
            _publisher.ActivityEnds();
            statuses.Statuses.Each(Statuses.Add);
        }
    }
}