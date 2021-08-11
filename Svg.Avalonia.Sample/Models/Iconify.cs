using System.ComponentModel.DataAnnotations.Schema;
using Avalonia.Media;

namespace Svg.Avalonia.Sample.Models
{
    public class Iconify
    {
        public string Name { get; set; }
        public string SvgContent { get; set; }

        public Iconify(string name, string svgContent)
        {
            Name = name;
            SvgContent = svgContent;
        }

        public override string ToString()
            => Name;
    }
}