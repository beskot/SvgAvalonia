using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgElements
{
    /// <summary>
    /// SvgElementBase.
    /// </summary>
    public abstract class SvgElementBase : ISvgGeometry, ISvgBrush
    {
        /// <summary>
        /// Corresponding node.
        /// </summary>
        public XmlElement Element { get; }

        public SvgElementBase(XmlElement xmlElement)
        {
            Element = xmlElement;
        }

        /// <summary>
        /// Create geometry.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>Geometry</returns>
        public abstract Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources);

        /// <summary>
        /// Create brush.
        /// </summary>
        /// <param name="Resources">Resources dictionary</param>
        /// <returns>Brush</returns>
        public virtual Brush CreateBrush(Dictionary<string, ISvgElement> Resources)
        {
            var brush = (Element.Attributes["fill"]
                ?? Element.ParentNode.Attributes["fill"]).ToBrush();

            if (Resources is { } && brush is null)
            {
                if ((Element.Attributes["fill"]
                    ?? Element.ParentNode.Attributes["fill"]) is { } fill)
                {
                    Resources.TryGetValue(fill.ToResourceId(), out var brushResource);
                    brush = (brushResource as ISvgBrush)?.CreateBrush(Resources);
                }
            }

            if (brush is { })
            {
                brush.Opacity = (Element.Attributes["fill-opacity"]
                    ?? Element.Attributes["opacity"]
                    ?? Element.ParentNode.Attributes["fill-opacity"]
                    ?? Element.ParentNode.Attributes["opacity"]).ToOpacity();
            }

            return brush;
        }
    }
}