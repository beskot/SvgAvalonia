using System.Collections.Generic;
using System.Xml;
using Avalonia;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgCircle (inherited from SvgElementBase).
    /// </summary>
    public class SvgCircle : SvgElementBase
    {
        public SvgCircle(XmlElement xmlElement) : base(xmlElement) { }

        /// <summary>
        /// Create geometry of SvgCircle.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>EllipseGeometry</returns>
        public override Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources)
        {
            var cx = Element.Attributes["cx"].ToDouble();
            var cy = Element.Attributes["cy"].ToDouble();
            var r = Element.Attributes["r"].ToDouble();

            return new EllipseGeometry
            {
                Center = new Point(cx, cy),
                RadiusX = r,
                RadiusY = r
            };
        }
    }
}