using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Avalonia;
using Avalonia.Media;
using Svg.Avalonia.Lib.Source;
using Svg.Avalonia.Lib.Source.SvgBrushes;
using Svg.Avalonia.Lib.Source.SvgElements;
using Svg.Avalonia.Lib.Source.SvgTransforms;

namespace Svg.Avalonia.Lib.Utils
{
    /// <summary>
    /// XmlExtension.
    /// </summary>
    internal static class XmlExtension
    {
        public static double[] ExtractDoubleValues(this XmlAttribute attribute)
        {
            return attribute is null
                ? default
                : new Regex(@"-?[0-9]*[.[0-9]+]?")
                    .Matches(attribute.Value)
                    .Select(p => double.Parse(p.Value, NumberStyles.Any, CultureInfo.InvariantCulture))
                    .ToArray();
        }
        
        /// <summary>
        /// Convert attribute value to double.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>Value double</returns>
        public static double ToDouble(this XmlAttribute attribute)
        {
            return attribute is { } ? attribute.ExtractDoubleValues()[0] : 0;
        }

        /// <summary>
        /// Convert attribute value to Rect.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>Value Rect</returns>
        public static Rect ToRect(this XmlAttribute attribute)
        {
            return (attribute.ExtractDoubleValues() is { } values
                    && values.Length == 4)
                        ? new Rect(values[0], values[1], values[2], values[3])
                        : default;
        }

        /// <summary>
        /// Convert attribute value to Transform.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>Value Transform</returns>
        public static Transform ToTransform(this XmlAttribute attribute)
        {
            return (new SvgGroupTransform().CreateTransform(attribute) is { } transform)
                ? transform
                : default;
        }

        /// <summary>
        /// Convert attribute value to Brush.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>Value Brush</returns>
        public static Brush ToBrush(this XmlAttribute attribute)
        {
            return Color.TryParse(attribute?.Value, out var color)
                ? new SolidColorBrush(color)
                : default;
        }

        /// <summary>
        /// Convert attribute value to Color.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>Value Color</returns>
        public static Color ToColor(this XmlAttribute attribute)
        {
            return Color.TryParse(attribute?.Value, out var color)
                ? color
                : default;
        }

        /// <summary>
        /// Convert attribute value to Opacity.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>Value double</returns>
        public static double ToOpacity(this XmlAttribute attribute)
        {
            return attribute is { } ? attribute.ToDouble() : 1.0;
        }

        /// <summary>
        /// Convert attribute value to SvgElement.
        /// </summary>
        /// <param name="element">Attribute</param>
        /// <returns>Value ISvgElement</returns>
        public static ISvgElement ToSvgElement(this XmlElement element)
        {
            return element switch
            {
                { Name: "linearGradient" } => new SvgLinearGradient(element),
                { Name: "radialGradient" } => new SvgRadialGradient(element),
                { Name: "path" } => new SvgPath(element),
                { Name: "ellipse" } => new SvgEllipse(element),
                { Name: "circle" } => new SvgCircle(element),
                { Name: "rect" } => new SvgRect(element),
                { Name: "use" } => new SvgUse(element),
                { Name: "mask" } => new SvgMask(element),
                { Name: "filter" } => new SvgFilter(element),
                _ => null
            };
        }

        /// <summary>
        /// Convert attribute value to resource url.
        /// </summary>
        /// <param name="element">Attribute</param>
        /// <returns>value url</returns>
        public static string ToResourceId(this XmlAttribute element)
        {
            return element.Value.StartsWith("url")
                ? element.Value.Substring(5).TrimEnd(')')
                : string.Empty;
        }
    }
}