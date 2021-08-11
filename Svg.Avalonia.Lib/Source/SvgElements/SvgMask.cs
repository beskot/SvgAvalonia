using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgMask (inherited from SvgElementBase).
    /// </summary>
    public class SvgMask : SvgElementBase
    {
        public SvgMask(XmlElement element) : base(element) { }

        /// <summary>
        /// Create geometry of SvgMask. 
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>Geometry of mask node content</returns>
        public override Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources)
        {
            Geometry geometry = default;

            foreach (XmlElement node in Element.SelectNodes("*"))
            {
                if (node.ToSvgElement() is { } svgElement)
                {
                    //(svgElement as ISvgResource)?.AddToResources(_svgIdElements);
                    geometry = (svgElement as ISvgGeometry)?.CreateGeometry(Resources);
                }
            }

            return geometry;
        }
    }
}