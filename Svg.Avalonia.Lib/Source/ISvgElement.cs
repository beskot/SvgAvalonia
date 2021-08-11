using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source
{
    public interface ISvgElement
    {
        XmlElement Element { get; }
    }

    public interface ISvgBrush : ISvgElement
    {
        Brush CreateBrush(Dictionary<string, ISvgElement> Resources);
    }

    public interface ISvgGeometry : ISvgElement
    {
        Geometry CreateGeometry(Dictionary<string, ISvgElement> Resources);
    }
    
    public interface ISvgTransform
    {
        Transform CreateTransform(XmlAttribute attribute);
    }
}