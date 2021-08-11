using System.Globalization;
using System.Xml;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgBrushes
{
    /// <summary>
    /// SvgRadialGradient (Inherited from SvgGradientsBase).
    /// </summary>
    public class SvgRadialGradient : SvgGradientsBase
    {
        public SvgRadialGradient(XmlElement element) : base(element) { }

        /// <summary>
        /// Create radial gradient brush.
        /// </summary>
        /// <param name="node">XmlElement</param>
        protected override GradientBrush OnCreate()
        {
            return new RadialGradientBrush
            {
                Center = GetPoint(Element, "cx", "cy"),
                GradientOrigin = GetPoint(Element, "fx", "fy"),
                Radius = Element.Attributes["r"].ToDouble()
            };
        }
    }
}