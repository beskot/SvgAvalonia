using System.Linq;
using System.Xml;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgTransforms
{
    /// <summary>
    /// SvgRotateTransform.
    /// </summary>
    public class SvgRotateTransform : ISvgTransform
    {
        /// <summary>
        /// Create rotate transform.
        /// </summary>
        /// <param name="content">Transform content</param>
        /// <returns>Transform</returns>
        public Transform CreateTransform(XmlAttribute attribute)
        {
            var values = attribute.ExtractDoubleValues();

            if (values.Any())
            {
                var transformGroup = new TransformGroup();

                if (values.Length == 3)
                {
                    transformGroup.Children.Add(new TranslateTransform(values[1], values[2]));
                }

                transformGroup.Children.Add(new RotateTransform(values[0]));

                if (values.Length == 3)
                {
                    transformGroup.Children.Add(new TranslateTransform(-values[1], -values[2]));
                }

                return transformGroup;
            }

            return default;
        }
    }
}