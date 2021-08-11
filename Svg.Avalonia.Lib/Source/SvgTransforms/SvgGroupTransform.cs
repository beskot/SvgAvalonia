using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source.SvgTransforms
{
    /// <summary>
    /// SvgGroupTransform.
    /// </summary>
    public class SvgGroupTransform : ISvgTransform
    {
        /// <summary>
        /// Transform dictionary.
        /// </summary>
        private Dictionary<string, ISvgTransform> _transformDict = new()
        {
            { "matrix", new SvgMatrixTransform() },
            { "translate", new SvgTranslateTransform() },
            { "scale", new SvgScaleTransform() },
            { "rotate", new SvgRotateTransform() }
        };

        /// <summary>
        /// Create transform.
        /// </summary>
        /// <param name="content">Transform content</param>
        /// <returns>Transform</returns>
        public Transform CreateTransform(XmlAttribute attribute)
        {
            if (attribute is null)
            {
                return default;
            }
                        
            var list = Utility.ExtractTokenContent(attribute.Value, _transformDict.Keys.ToArray());
            var groupTransform = new TransformGroup();
            var temp = attribute.Clone() as XmlAttribute;
            foreach (var item in list)
            {
                if (_transformDict.TryGetValue(item.Split('(')[0], out var transform))
                {
                    temp.Value = item;
                    groupTransform.Children.Add(transform.CreateTransform(temp));
                }
            }             

            return groupTransform;
        }
    }
}