using System;
using MLToolkit.Forms.SwipeCardView.Core;
using SwipeCardView.Sample.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeCardView.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TinderPage : ContentPage
    {
        public TinderPage()
        {
            this.InitializeComponent();
            this.BindingContext = new TinderPageViewModel();
            SwipeCardView.Dragging += OnDragging;
        }

        private void OnDislikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
        }

        private void OnSuperLikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Up);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void OnLikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Right);
        }

        private void OnStart(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var timerValue = view.FindByName<Label>("Timer");
            timerValue.Text = "I am here";
        }

        private void OnDragging(object sender, DraggingCardEventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var nopeFrame = view.FindByName<Frame>("NopeFrame");
            //var timerValue = view.FindByName<Label>("Timer");
            var endDateValue = view.FindByName<Label>("EndDate");

            var likeFrame = view.FindByName<Frame>("LikeFrame");
            var superLikeFrame = view.FindByName<Frame>("SuperLikeFrame");
            var threshold = (this.BindingContext as TinderPageViewModel).Threshold;

            var draggedXPercent = e.DistanceDraggedX / threshold;

            var draggedYPercent = e.DistanceDraggedY / threshold;

            switch (e.Position)
            {
                case DraggingCardPosition.Start:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    superLikeFrame.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    superLikeButton.Scale = 1;
                    Timer(endDateValue.Text, sender);
                    break;

                case DraggingCardPosition.UnderThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeFrame.Opacity = (-1) * draggedXPercent;
                        nopeButton.Scale = 1 + draggedXPercent / 2;
                        superLikeFrame.Opacity = 0;
                        superLikeButton.Scale = 1;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeFrame.Opacity = draggedXPercent;
                        likeButton.Scale = 1 - draggedXPercent / 2;
                        superLikeFrame.Opacity = 0;
                        superLikeButton.Scale = 1;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                        nopeFrame.Opacity = 0;
                        likeFrame.Opacity = 0;
                        nopeButton.Scale = 1;
                        likeButton.Scale = 1;
                        superLikeFrame.Opacity = (-1) * draggedYPercent;
                        superLikeButton.Scale = 1 + draggedYPercent / 2;
                    }
                    break;

                case DraggingCardPosition.OverThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeFrame.Opacity = 1;
                        superLikeFrame.Opacity = 0;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeFrame.Opacity = 1;
                        superLikeFrame.Opacity = 0;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                        nopeFrame.Opacity = 0;
                        likeFrame.Opacity = 0;
                        superLikeFrame.Opacity = 1;
                    }
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    superLikeFrame.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    superLikeButton.Scale = 1;
                    break;

                case DraggingCardPosition.FinishedOverThreshold:
                    nopeFrame.Opacity = 0;
                    likeFrame.Opacity = 0;
                    superLikeFrame.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    superLikeButton.Scale = 1;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string FnTimerData(string endDate)
        {
            return "I am finally here";
        }

        public void Timer(string endDateString, object sender)
        {
            var view = (Xamarin.Forms.View)sender;
            var timerValue = view.FindByName<Label>("Timer");
            var time = 0;
            string displayDate = string.Empty;
            DateTime endDate = Convert.ToDateTime(endDateString);
            TimeSpan bidTime = endDate - DateTime.Now;

            if (endDate.CompareTo(DateTime.Now) <= 0)
            {
                displayDate = "Auction Expired";
                timerValue.Text = displayDate;
            }
            else
            {
                displayDate = bidTime.ToString();
                var updateRate = 1000f; // 30Hz
                Device.StartTimer(TimeSpan.FromMilliseconds(updateRate), () =>
                {
                    var timeInSecond = Convert.ToInt32(TimeSpan.Parse(displayDate).TotalSeconds);
                    time = Convert.ToInt32(timeInSecond);
                    if (time == 0)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            displayDate = "Auction Expired";
                            timerValue.Text = displayDate;
                        });
                        return false;
                    }
                    else
                    {
                        time--;

                        TimeSpan ts = TimeSpan.FromSeconds(time);

                        string answer = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            displayDate = answer;
                            timerValue.Text = displayDate;
                        });
                    }
                    return true;
                });
            }
        }
    }
}