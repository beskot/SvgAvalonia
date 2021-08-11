using Avalonia;
using Avalonia.Media;

namespace Svg.Avalonia.Lib.Source
{
    /// <summary>
    /// SvgGeometryDrawing.
    /// </summary>
    public class SvgGeometryDrawing : GeometryDrawing
    {
        public static readonly StyledProperty<Geometry> ClipGeometryProperty =
            AvaloniaProperty.Register<SvgGeometryDrawing, Geometry>(nameof(ClipGeometry));

        public static readonly StyledProperty<IBrush> OpacityMaskProperty =
          AvaloniaProperty.Register<DrawingGroup, IBrush>(nameof(OpacityMask));

        public Geometry ClipGeometry
        {
            get => GetValue(ClipGeometryProperty);
            set => SetValue(ClipGeometryProperty, value);
        }

        public IBrush OpacityMask
        {
            get => GetValue(OpacityMaskProperty);
            set => SetValue(OpacityMaskProperty, value);
        }
        
        public override void Draw(DrawingContext context)
        {
            using (OpacityMask != null ? context.PushOpacityMask(OpacityMask, GetBounds()) : default(DrawingContext.PushedState))
            using (ClipGeometry != null ? context.PushGeometryClip(ClipGeometry) : default(DrawingContext.PushedState))
            {
                base.Draw(context);
            }
        }
    }
}