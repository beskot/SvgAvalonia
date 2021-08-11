using System.Collections.Generic;
using System.Xml;
using Avalonia;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgRect (inherited from SvgElementBase).
    /// </summary>
    public class SvgRect : SvgElementBase
    {
        public SvgRect(XmlElement xmlElement) : base(xmlElement) { }

        /// <summary>
        /// Create geometry of SvgRect.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>RectangleGeometry</returns>
        public override Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources)
        {
            var x = Element.Attributes["x"].ToDouble();
            var y = Element.Attributes["y"].ToDouble();
            var width = Element.Attributes["width"].ToDouble();
            var height = Element.Attributes["height"].ToDouble();

            return new RectangleGeometry(new Rect(x, y, width, height));
        }
    }
}