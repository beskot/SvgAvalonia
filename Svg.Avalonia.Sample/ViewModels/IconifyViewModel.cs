using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Svg.Avalonia.Lib.Source;
using Svg.Avalonia.Sample.Models;

namespace Svg.Avalonia.Sample.ViewModels
{
    public class IconifyViewModel : ViewModelBase
    {
        private string _fileName;
        public IconifyViewModel(string fileName)
        {
            _fileName = fileName;
        }

        public async IAsyncEnumerable<IconifyExt> LoadDataAsync(string searchText)
        {
            var context = new IconifyContext(_fileName);
            var iconsData = await context.Iconifies
                .AsNoTracking()
                .Where(x => (string.IsNullOrEmpty(searchText) ? true : x.Name.Contains(searchText)))
                .ToListAsync();

            foreach (var item in iconsData)
            {
                yield return new IconifyExt
                {
                    Name = item.Name.Split(':')[1],
                    Data = new SvgDrawing
                    {
                        Content = item.SvgContent
                    }
                };
            }
        }
    }

    public class IconifyExt
    {
        public string Name { get; set; }
        public SvgDrawing Data { get; set; }
    }
}