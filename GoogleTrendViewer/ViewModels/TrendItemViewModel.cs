using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTrendViewer.ViewModels
{
    public class TrendItemViewModel : ViewModelBase
    {
        private string _title;
        public string Title { get { return _title; } set { this.RaiseAndSetIfChanged(ref _title, value); } }

        private string _image;
        public string Image { get { return _image; } set { this.RaiseAndSetIfChanged(ref _image, value); } }

        private string _traffic;
        public string Traffic { get { return _traffic; } set { this.RaiseAndSetIfChanged(ref _traffic, value); } }

        private string _description;
        public string Description { get { return _description; } set { this.RaiseAndSetIfChanged(ref _description, value); } }

        private int _number;
        public int Number { get { return _number; } set { this.RaiseAndSetIfChanged(ref _number, value); } }

        private int _pubDate;
        public int PubDate { get { return _pubDate; } set { this.RaiseAndSetIfChanged(ref _pubDate, value); } }


        public ObservableCollection<TrendNewsItemViewModel> NewsItems { get; set; }

        public TrendItemViewModel()
        {
            NewsItems = new ObservableCollection<TrendNewsItemViewModel>();
        }
    }
}

