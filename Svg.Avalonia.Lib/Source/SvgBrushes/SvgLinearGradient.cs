
using System.Xml;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source.SvgBrushes
{
    /// <summary>
    /// SvgLinearGradient (Inherited from SvgGradientsBase).
    /// </summary>
    public class SvgLinearGradient : SvgGradientsBase
    {
        public SvgLinearGradient(XmlElement element) : base(element) { }

        /// <summary>
        /// Create linear gradient brush.
        /// </summary>
        /// <param name="node">XmlElement</param>
        protected override GradientBrush OnCreate()
        {
            return new LinearGradientBrush
            {
                StartPoint = GetPoint(Element, "x1", "y1"),
                EndPoint = GetPoint(Element, "x2", "y2")
            };
        }
    }
}