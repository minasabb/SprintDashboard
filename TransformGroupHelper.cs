using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace IQ.Core.Windows.Animation
{
    public static class TransformGroupHelper
    {
        /// <summary>
        /// Reset supplied element's TransformGroup
        /// </summary>
        /// <param name="element"></param>
        public static void ResetTransformGroup(UIElement element)
        {
            DeleteTransformGroup(element);
            CreateTransformGroup(element);
        }

        /// <summary>
        /// Creates a transform group for the supplied UIElement
        /// </summary>
        /// <param name="element"></param>
        public static void CreateTransformGroup(UIElement element)
        {
            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new TranslateTransform());
            transformGroup.Children.Add(new ScaleTransform(1,1));
            transformGroup.Children.Add(new SkewTransform());
            transformGroup.Children.Add(new RotateTransform());

            element.RenderTransform = transformGroup;
        }

        /// <summary>
        /// Delete the supplied element's TransformGroup if it exists
        /// </summary>
        /// <param name="element"></param>
        public static void DeleteTransformGroup(UIElement element)
        {
            var transformGroup = (element.RenderTransform as TransformGroup);

            if (transformGroup == null)
                return;

            transformGroup.Children.Clear();
        }

        /// <summary>
        /// Get specific transform from UIElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="transformType"></param>
        /// <returns></returns>
        public static Transform GetTransformGroup(UIElement element, Enums.TransformType transformType)
        {
            var transformGroup = element.RenderTransform as TransformGroup;

            if (transformGroup == null)
                return null;

            switch (transformType)
            {
                case Enums.TransformType.Translate:
                    return transformGroup.Children[0] is TranslateTransform ? transformGroup.Children[0] : FindTransformGroupByType(element, typeof(TranslateTransform));
                case Enums.TransformType.Scale:
                    return transformGroup.Children[1] is ScaleTransform ? transformGroup.Children[2] : FindTransformGroupByType(element, typeof(ScaleTransform));
                case Enums.TransformType.Skew:
                    return transformGroup.Children[2] is SkewTransform ? transformGroup.Children[2] : FindTransformGroupByType(element, typeof(SkewTransform));
                case Enums.TransformType.Rotate:
                    return transformGroup.Children[3] is RotateTransform ? transformGroup.Children[3] : FindTransformGroupByType(element, typeof(RotateTransform));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Return a specific Transform from the supplied element by a specific type
        /// </summary>
        /// <param name="element"></param>
        /// <param name="transformGroupType"></param>
        /// <returns></returns>
        public static Transform FindTransformGroupByType(UIElement element, Type transformGroupType)
        {
            if (element == null)
                return null;

            var transformGroup = element.RenderTransform as TransformGroup;

            if (transformGroup == null)
                throw new Exception("No Transform Group Present");

            foreach (var transform in transformGroup.Children.Where(g => g.GetType() == transformGroupType))
            {
                return transform;
            }

            throw new Exception("No " + transformGroupType + " present");
        }
    }
}