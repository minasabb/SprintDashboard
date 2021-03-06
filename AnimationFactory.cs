﻿using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace IQ.Core.Windows.Animation
{
    public class AnimationFactory
    {
        public static DoubleAnimationUsingKeyFrames CreateDoubleAnimation(KeySpline toKeySpline, double toValue, double fromValue = 0.0, KeySpline fromKeySpline=null, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null)
        {
            var duration = durationSpan != null ? durationSpan.Value : TimeSpan.FromSeconds(0);
            beginTimeSpan = beginTimeSpan != null ? beginTimeSpan.Value : TimeSpan.FromSeconds(0);

            var animation = new DoubleAnimationUsingKeyFrames 
            {
                BeginTime = beginTimeSpan,
                Duration = duration,
            };
            if(fromKeySpline==null)
                animation.KeyFrames.Add(new SplineDoubleKeyFrame(fromValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            else
                animation.KeyFrames.Add(new SplineDoubleKeyFrame(fromValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),fromKeySpline));
            animation.KeyFrames.Add(new SplineDoubleKeyFrame(toValue, KeyTime.FromTimeSpan(duration), toKeySpline));

            return animation;
        }

        public static DoubleAnimation CreateDoubleAnimation(double toValue, double fromValue = 0.0, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var duration = durationSpan != null ? new Duration(durationSpan.Value) : new Duration(TimeSpan.FromSeconds(0));
            beginTimeSpan = beginTimeSpan != null ? beginTimeSpan.Value : TimeSpan.FromSeconds(0);

            var animation = new DoubleAnimation
            {
                BeginTime = beginTimeSpan,
                From = fromValue,
                To = toValue,
                Duration = duration,
                EasingFunction = easingFuction,
                
            };
            return animation;
        }

        public static DoubleAnimationUsingKeyFrames CreateDoubleAnimation(UIElement targetElement, DependencyProperty targetProperty, KeySpline toKeySpline, double toValue, double fromValue = 0.0, KeySpline fromKeySpline =null, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null)
        {
            var animation = CreateDoubleAnimation(toKeySpline,toValue ,fromValue, fromKeySpline,beginTimeSpan, durationSpan);
            Storyboard.SetTarget(animation, targetElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

            return animation;
        }

        public static DoubleAnimation CreateDoubleAnimation(UIElement targetElement, DependencyProperty targetProperty, double toValue, double fromValue = 0.0, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var animation = CreateDoubleAnimation(toValue, fromValue, beginTimeSpan, durationSpan, easingFuction);
            Storyboard.SetTarget(animation, targetElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

            return animation;
        }

        public static DoubleAnimation CreateDoubleAnimation(UIElement targetElement, string targetProperty, double toValue, double fromValue = 0.0, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var animation = CreateDoubleAnimation(toValue, fromValue, beginTimeSpan, durationSpan, easingFuction);
            Storyboard.SetTarget(animation, targetElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

            return animation;
        }

        public static DoubleAnimation CreateDoubleAnimation(string targetName, DependencyProperty targetProperty, double toValue, double fromValue = 0.0, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var animation = CreateDoubleAnimation(toValue, fromValue, beginTimeSpan, durationSpan, easingFuction);
            Storyboard.SetTargetName(animation, targetName);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

            return animation;
        }

        public static ThicknessAnimation CreateThicknessAnimation(Thickness toValue, Thickness? fromValue = null, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var duration = durationSpan != null ? new Duration(durationSpan.Value) : new Duration(TimeSpan.FromSeconds(0));
            beginTimeSpan = beginTimeSpan != null ? beginTimeSpan.Value : TimeSpan.FromSeconds(0);
            fromValue = fromValue ?? new Thickness(0);

            var animation = new ThicknessAnimation()
            {
                BeginTime = beginTimeSpan,
                From = fromValue,
                To = toValue,
                Duration = duration,
                EasingFunction = easingFuction
            };

            return animation;
        }

        public static ThicknessAnimation CreateThicknessAnimation(UIElement targetElement, DependencyProperty targetProperty, Thickness toValue, Thickness? fromValue = null, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var animation = CreateThicknessAnimation(toValue, fromValue, beginTimeSpan, durationSpan, easingFuction);
            Storyboard.SetTarget(animation, targetElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

            return animation;
        }


        public static ThicknessAnimation CreateThicknessAnimation(string targetName, DependencyProperty targetProperty, Thickness toValue, Thickness? fromValue = null, TimeSpan? beginTimeSpan = null, TimeSpan? durationSpan = null, IEasingFunction easingFuction = null)
        {
            var animation = CreateThicknessAnimation(toValue, fromValue, beginTimeSpan, durationSpan, easingFuction);
            Storyboard.SetTargetName(animation, targetName);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));

            return animation;
        }
    }
}