using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgPath (inherited from SvgElementBase).
    /// </summary>
    public class SvgPath : SvgElementBase
    {
        public SvgPath(XmlElement xmlElement) : base(xmlElement) { }

        /// <summary>
        /// Create geometry of SvgPath.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>Geometry</returns>
        public override Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources)
        {
            return Geometry.Parse(Element.GetAttribute("d"));
        }
    }
}