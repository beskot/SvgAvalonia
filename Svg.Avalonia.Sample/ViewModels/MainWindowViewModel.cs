using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using ReactiveUI;

namespace Svg.Avalonia.Sample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IconifyViewModel _iconify;
        private string? _searchText;

        public string? SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }
        public ObservableCollection<IconifyExt> IconifyCollection { get; set; } = new();

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(Search!);

            _iconify = new IconifyViewModel("Resources/SVGLogos.db");
            SvgLocadAsync();
        }

        private async void SvgLocadAsync()
        {
            await foreach (var item in _iconify.LoadDataAsync(string.Empty))
            {
                IconifyCollection.Add(item);
            }
        }

        private async void Search(string searchText)
        {
            IconifyCollection.Clear();

            await foreach (var item in _iconify.LoadDataAsync(searchText))
            {
                IconifyCollection.Add(item);
            }
        }
    }
}
