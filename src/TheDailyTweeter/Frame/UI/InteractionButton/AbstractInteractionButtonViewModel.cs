using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Membus.WpfTwitterClient.Frame.UI
{
    public class AbstractInteractionButtonViewModel : DependencyObject
    {
        private static DoubleAnimationBase makeVisible;
        private static DoubleAnimationBase makeInvisible;

        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof (double), typeof (AbstractInteractionButtonViewModel), new PropertyMetadata(0.0));

        private UIElement openingSource;
        private const double Duration = 1.5;

        public event EventHandler CloseRequested;

        public void Start(object source)
        {
            //WPF objects MUST be created on the goddamn dispatcher thread, otherwise you run into trouble
            makeVisible = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(Duration)));
            makeInvisible = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(Duration)));

            openingSource = source as UIElement;
            if (openingSource == null) return;

            openingSource.BeginAnimation(UIElement.OpacityProperty, makeVisible);
        }

        public virtual void MainPush()
        {
            
        }

        public void Stop()
        {
            if (openingSource == null)
            {
                raiseCloseEvent();
                return;
            }
            makeInvisible.Completed += (s, e) => raiseCloseEvent();
            openingSource.BeginAnimation(UIElement.OpacityProperty, makeInvisible);
        }

        private void raiseCloseEvent()
        {
            if (CloseRequested != null)
                CloseRequested(this, EventArgs.Empty);
        }
    }
}