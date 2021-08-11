using System.Xml;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgTransforms
{
    /// <summary>
    /// SvgTranslateTransform.
    /// </summary>
    public class SvgTranslateTransform : ISvgTransform
    {
        /// <summary>
        /// Create translate transform.
        /// </summary>
        /// <param name="content">Transform content</param>
        /// <returns>Transform</returns>
        public Transform CreateTransform(XmlAttribute attribute)
        {
            var values = attribute.ExtractDoubleValues();
            (var x, var y) = values.Length switch
            {
                1 => (values[0], 0),
                2 => (values[0], values[1]),
                _ => (0, 0)
            };

            return new TranslateTransform(x, y);;
        }
    }
}