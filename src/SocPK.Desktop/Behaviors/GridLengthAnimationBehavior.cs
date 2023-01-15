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

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(
                "EasingFunction",
                typeof(IEasingFunction),
                typeof(GridLengthAnimationBehavior)
            );

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration),
            typeof(Duration),
            typeof(GridLengthAnimationBehavior)
        );

        protected override void OnAttached()
        {
            var animation = new GridLengthAnimation();
            animation.From = From;
            animation.To = To;
            animation.Duration = Duration;
            animation.EasingFunction = EasingFunction;
            Storyboard.SetTarget(animation, AssociatedObject);
            Storyboard.SetTargetProperty(
                animation,
                new PropertyPath(ColumnDefinition.WidthProperty)
            );
            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            AssociatedObject.Loaded += (sender, e) => storyboard.Begin(AssociatedObject);
        }
    }
}
