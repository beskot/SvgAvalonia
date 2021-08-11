using System.Xml;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgTransforms
{
    /// <summary>
    /// SvgScaleTransform.
    /// </summary>
    public class SvgScaleTransform : ISvgTransform
    {
        /// <summary>
        /// Create scale transform.
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

            return new ScaleTransform(x, y);
        }
    }
}