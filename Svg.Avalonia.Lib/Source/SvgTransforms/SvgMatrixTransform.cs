using System.Xml;
using Avalonia;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgTransforms
{
    /// <summary>
    /// SvgMatrixTransform.
    /// </summary>
    public class SvgMatrixTransform : ISvgTransform
    {
        /// <summary>
        /// Create matrix transform.
        /// </summary>
        /// <param name="attribute">Transform content</param>
        /// <returns>Transform</returns>
        public Transform CreateTransform(XmlAttribute attribute)
        {
            return (attribute.ExtractDoubleValues() is { } values
                 && values.Length == 6)
                    ? new MatrixTransform(
                        new Matrix(values[0], values[1], values[2],
                                   values[3], values[4], values[5]))
                    : default;
        }
    }
}