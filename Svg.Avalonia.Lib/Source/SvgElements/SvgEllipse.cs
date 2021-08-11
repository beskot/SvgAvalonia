using System.Xml;
using Avalonia.Media;
using Avalonia;
using Svg.Avalonia.Lib.Utils;
using System.Collections.Generic;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgEllipse (inherited from SvgElementBase).
    /// </summary>
    public class SvgEllipse : SvgElementBase
    {
        public SvgEllipse(XmlElement xmlElement) : base(xmlElement) { }

        /// <summary>
        /// Create geometry of SvgEllipse.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>EllipseGeometry</returns>
        public override Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources)
        {
            var cx = Element.Attributes["cx"].ToDouble();
            var cy = Element.Attributes["cy"].ToDouble();
            var rx = Element.Attributes["rx"].ToDouble();
            var ry = Element.Attributes["ry"].ToDouble();

            return new EllipseGeometry
            {
                Center = new Point(cx, cy),
                RadiusX = rx,
                RadiusY = ry
            };
        }
    }
}