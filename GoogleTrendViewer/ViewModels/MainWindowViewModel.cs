using GoogleTrendViewer.Services;
using System.Collections.ObjectModel;

namespace GoogleTrendViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GoogleTrendService trendService = new GoogleTrendService();
        public ObservableCollection<TrendItemViewModel> Items { get; set; }

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<TrendItemViewModel>();
            var trendUrl = "https://trends.google.com.vn/trends/trendingsearches/daily/rss?geo=VN";
            var googleTrendItems = trendService.GetTodayTrends(trendUrl);

            for (int i = 0; i < googleTrendItems.Count; i++)
            {
                var googleTrendItem = googleTrendItems[i];

                var trendItem = new TrendItemViewModel
                {
                    Title = googleTrendItem.Title,
                    Traffic = googleTrendItem.Traffic,
                    Image = googleTrendItem.Picture,
                    Number = i + 1,
                    Description = googleTrendItem.Description,
                };

                trendItem.NewsItems = new ObservableCollection<TrendNewsItemViewModel>();

                foreach(var news in googleTrendItem.NewsItems)
                {
                    var newsItem = new TrendNewsItemViewModel { Title = news.Title, Description = news.Description, Url = news.Url };
                    trendItem.NewsItems.Add(newsItem);
                }
                

                Items.Add(trendItem);
            }

        }
    }
}