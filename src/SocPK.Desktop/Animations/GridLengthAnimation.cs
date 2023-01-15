using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SocPK.Desktop.Animations
{
    public class GridLengthAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(GridLength);

        public override bool IsDestinationDefault => true;

        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From",
            typeof(GridLength),
            typeof(GridLengthAnimation)
        );

        public GridLength To
        {
            get => (GridLength)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To",
            typeof(GridLength),
            typeof(GridLengthAnimation)
        );

        public override object GetCurrentValue(
            object defaultOriginValue,
            object defaultDestinationValue,
            AnimationClock animationClock
        )
        {
            var easing = EasingFunction;

            var progress = !(easing is null)
                ? easing.Ease(animationClock.CurrentProgress.Value)
                : animationClock.CurrentProgress.Value;

            return new GridLength(
                progress * (To.Value - From.Value) + From.Value,
                GridUnitType.Star
            );
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(
                "EasingFunction",
                typeof(IEasingFunction),
                typeof(GridLengthAnimation)
            );

        public IEasingFunction EasingFunction
        {
            get => (IEasingFunction)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }
    }
}
