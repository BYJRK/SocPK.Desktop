using Microsoft.Xaml.Behaviors;
using SocPK.Desktop.Animations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SocPK.Desktop.Behaviors
{
    public class GridLengthAnimationBehavior : Behavior<ColumnDefinition>
    {
        public GridLength From
        {
            get { return (GridLength)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From),
            typeof(GridLength),
            typeof(GridLengthAnimationBehavior)
        );
        public GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To),
            typeof(GridLength),
            typeof(GridLengthAnimationBehavior)
        );

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration),
            typeof(TimeSpan),
            typeof(GridLengthAnimationBehavior)
        );

        protected override void OnAttached()
        {
            var animation = new GridLengthAnimation();
            animation.From = From;
            animation.To = To;
            animation.Duration = new Duration(Duration);
            Storyboard.SetTarget(animation, AssociatedObject);
            Storyboard.SetTargetProperty(animation, new PropertyPath(ColumnDefinition.WidthProperty));
            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            AssociatedObject.Loaded += (sender, e) => storyboard.Begin(AssociatedObject);
        }
    }
}
