using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source.SvgBrushes
{
    public class SvgFilter : ISvgElement, ISvgBrush
    {
        public XmlElement Element { get; }

        public SvgFilter(XmlElement element)
        {
            Element = element;
        }

        public Brush CreateBrush(Dictionary<string, ISvgElement> Resources)
        {
            //TODO pass
            return default;
        }
    }
}