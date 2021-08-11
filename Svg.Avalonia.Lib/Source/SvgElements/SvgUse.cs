using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgUse (inherited from SvgElementBase).
    /// </summary>
    public class SvgUse : SvgElementBase
    {
        public SvgUse(XmlElement xmlElement) : base(xmlElement) { }

        /// <summary>
        /// Create geometry of SvgUse.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>Geometry from resource dictionary by element Id</returns>
        public override Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources)
        {
            if (Resources is null)
            {
                return default;
            }

            var href = Element.Attributes["xlink:href"]?.Value.TrimStart('#');
            Resources.TryGetValue(href, out var element);

            return (element as ISvgGeometry)?.CreateGeometry(Resources);
        }

    }
}