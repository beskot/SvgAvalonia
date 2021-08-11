using System.Collections.Generic;
using System.Xml;
using Avalonia;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source.SvgBrushes
{
    /// <summary>
    /// SvgGradientBase.
    /// </summary>
    public abstract class SvgGradientsBase : ISvgElement, ISvgBrush
    {
        /// <summary>
        /// Corresponding node.
        /// </summary>
        public XmlElement Element { get; private set; }

        public SvgGradientsBase(XmlElement element)
        {
            Element = element;
        }

        /// <summary>
        /// Create of gradient object. Call in CreateBrush.
        /// </summary>
        /// <returns></returns>
        protected abstract GradientBrush OnCreate();

        /// <summary>
        /// Get GradientStop from XmlElement.
        /// </summary>
        /// <param name="stop">XmlElement</param>
        /// <returns>GradientStop</returns>
        protected GradientStop GetGradientStop(XmlElement stop)
        {
            var color = stop.Attributes["stop-color"].ToColor();
            var offset = stop.Attributes["offset"].ToDouble();

            return new GradientStop(color, offset / 100.0);
        }

        /// <summary>
        /// Get RelativePoint from XmlElement.
        /// </summary>
        /// <param name="xmlElement">XmlElement</param>
        /// <param name="xName">Attribute x name</param>
        /// <param name="yName">Attribute y name</param>
        /// <returns>RelativePoint</returns>
        protected RelativePoint GetPoint(XmlElement xmlElement, string xName, string yName)
        {
            var x = xmlElement.Attributes[xName]?.Value;
            var y = xmlElement.Attributes[yName]?.Value;

            return (x != null && y != null )
                ? RelativePoint.Parse(string.Join(',', x, y))
                : default;
        }

        /// <summary>
        /// Create brush from gradients stops.
        /// </summary>
        /// <param name="Resources">Resource dictionary</param>
        /// <returns>Brush</returns>
        public Brush CreateBrush(Dictionary<string, ISvgElement> Resources)
        {
            var brush = OnCreate();
            foreach (XmlElement stop in Element.SelectNodes("*"))
            {
                brush.GradientStops.Add(GetGradientStop(stop));
            }

            return brush;
        }
    }
}