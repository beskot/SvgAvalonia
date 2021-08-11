using System.Collections.Generic;
using System.Xml;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Media;
using Svg.Avalonia.Lib.Utils;

namespace Svg.Avalonia.Lib.Source
{
    /// <summary>
    /// SvgDrawing.
    /// </summary>
    public class SvgDrawing : DrawingGroup
    {
        public static readonly AvaloniaProperty<string> ContentProperty
            = AvaloniaProperty.Register<SvgDrawing, string>(nameof(Content));

        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        private Rect _bounds = Rect.Empty;
        private Dictionary<string, ISvgElement> _dictResource = new();

        static SvgDrawing()
        {
            ContentProperty.Changed.AddClassHandler<SvgDrawing>((o, e) => o.ContentChanged(e));
        }

        /// <summary>
        /// Handle changed of content
        /// </summary>
        /// <param name="e">AvaloniaPropertyChangedEventArgs argument</param>
        private void ContentChanged(AvaloniaPropertyChangedEventArgs e)
        {
            try
            {
                if (e.OldValue is string oldContent)
                {
                    Children.Clear();
                }

                if (e.NewValue is string newContent)
                {
                    var document = new XmlDocument
                    {
                        InnerXml = newContent
                    };

                    DefineViewBox(document.DocumentElement);
                    CreateFromNodes(document.DocumentElement);
                }
            }
            catch (System.Exception ex)
            {
                Logger.TryGet(LogEventLevel.Debug, ex.Message);
            }
            finally
            {
                _dictResource = null;
            }
        }

        /// <summary>
        /// Create SvgDrawing. SvgElements is combine.
        /// </summary>
        /// <param name="node">DocumentElement</param>
        private void CreateFromNodes(XmlElement node)
        {
            foreach (XmlElement element in node.SelectNodes("*"))
            {
                if (element.ToSvgElement() is { } svgElement)
                {
                    if (svgElement.Element.Attributes["id"] is { } id)
                    {
                        _dictResource.Add(id.Value, svgElement);
                    }
                    else
                    {
                        Children.Add(new SvgGeometryDrawing
                        {
                            Geometry = GeometryDefine(svgElement),
                            Brush = BrushDefine(svgElement),
                            Pen = PenDefine(svgElement),
                            ClipGeometry = ClipGeometryDefine(svgElement),
                            OpacityMask = OpacityMaskDefine(svgElement)
                        });
                    }
                }
                else
                {
                    CreateFromNodes(element);
                }
            }
        }

        /// <summary>
        /// Define of geometry.
        /// </summary>
        /// <param name="svgElement">ISvgElement</param>
        /// <returns>Geometry</returns>
        protected virtual Geometry GeometryDefine(ISvgElement svgElement)
        {
            if ((svgElement as ISvgGeometry)?.CreateGeometry(_dictResource) is { } geometry)
            {
                geometry.Transform = (svgElement.Element.Attributes["transform"]
                    ?? svgElement.Element.ParentNode.Attributes["transform"]).ToTransform();

                return geometry;
            }

            return default;
        }

        /// <summary>
        /// Define of brush.
        /// </summary>
        /// <param name="svgElement">ISvgElement</param>
        /// <returns>Brush</returns>
        protected virtual Brush BrushDefine(ISvgElement svgElement)
        {
            return (svgElement as ISvgBrush)?.CreateBrush(_dictResource);
        }

        /// <summary>
        /// Define of pen.
        /// </summary>
        /// <param name="svgElement">ISvgElement</param>
        /// <returns>Pen</returns>
        protected virtual Pen PenDefine(ISvgElement svgElement)
        {
            var stroke = (svgElement.Element.Attributes["stroke"]
                ?? svgElement.Element.Attributes["stroke"]);

            var strokeWidth = (svgElement.Element.Attributes["stroke-width"]
                ?? svgElement.Element.Attributes["stroke-width"]);

            return new Pen
            {
                Brush = (stroke != null) ? stroke.ToBrush() : default,
                Thickness = (strokeWidth != null) ? strokeWidth.ToDouble() : default
            };
        }

        /// <summary>
        /// Define of clip geometry.
        /// </summary>
        /// <param name="svgElement">ISvgElement</param>
        /// <returns>Geometry</returns>
        protected virtual Geometry ClipGeometryDefine(ISvgElement svgElement)
        {
            return (svgElement.Element.Attributes["mask"] is { } mask)
                && _dictResource.TryGetValue(mask.ToResourceId(), out var element)
                    ? (element as ISvgGeometry)?.CreateGeometry(_dictResource)
                    : default;
        }

        /// <summary>
        /// Define opacity mask.
        /// </summary>
        /// <param name="svgElement">ISvgElement</param>
        /// <returns>Brush</returns>
        protected virtual Brush OpacityMaskDefine(ISvgElement svgElement)
        {
            return (svgElement?.Element.Attributes["mask"] is { } mask)
                && _dictResource.TryGetValue(mask.ToResourceId(), out var element)
                    ? (element as ISvgBrush)?.CreateBrush(_dictResource)
                    : default;
        }

        /// <summary>
        /// Load attributes of <svg/>
        /// </summary>
        /// <param name="node">Root node</param>
        protected virtual void DefineViewBox(XmlElement node)
        {
            _bounds = node.Attributes["viewbox"]?.ToRect() ??
                node.Attributes["viewBox"]?.ToRect() ??
                new Rect(0, 0, node.Attributes["width"].ToDouble(), node.Attributes["height"].ToDouble());

            //TODO viewBox and preserveAspectRatio???
            // _bounds = node.Attributes["preserveAspectRatio"]?.Value switch
            // {
            //     "xMinYMin meet" => new Rect(),
            //     "xMidYMid meet" => _bounds.Translate(new Vector(_bounds.Width / 2, _bounds.Height / 2)),
            //     "xMaxYMax meet" => new Rect(),
            //     "xMinYMin slice" => new Rect(),
            //     "xMidYMid slice" => new Rect(),
            //     "xMaxYMax slice" => new Rect(),
            //     _ => _bounds
            // };
        }

        /// <summary>
        /// Get bounds.
        /// </summary>
        /// <returns>Bounds of SvgDrawing element</returns>
        public override Rect GetBounds()
        {
            if (_bounds.IsEmpty)
            {
                throw new System.Exception("Bounds is empty.");
            }

            return _bounds;
        }
    }
}