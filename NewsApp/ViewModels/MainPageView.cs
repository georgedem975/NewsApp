using NewsApp.Services;
using System.ComponentModel;
using NewsApp.Models;

namespace NewsApp.ViewModels
{
    internal class MainPageView : INotifyPropertyChanged
    {
        private readonly NewsService _newsService;

        private readonly CollectionView _collectionView;

        private readonly Button _mockButton;

        public MainPageView(CollectionView collectionView, Button button)
        {
            _newsService = new NewsService();
            _collectionView = collectionView;
            _mockButton = button;
            _mockButton.Clicked += OnButtonClicked;
            LoadNewsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void LoadNewsAsync(bool mock = false)
        {
             List<NewsItem>? items = await _newsService.GetNewsAsync();

            if (items is null && !mock)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось получить данные", "OK");
                return;
            }

            if (mock)
            {
                items = new List<NewsItem>();

                for (int i = 0; i < 10; i++)
                {
                    items.Add(new NewsItem
                    {
                        Title = $"Новость {i}",
                        Description = $"Описание новости {i}"
                    });
                }

            }

            _collectionView.ItemsSource = items;

            _collectionView.ItemTemplate = new DataTemplate(() =>
            {
                var titleLable = new Label
                {
                    FontSize = 20,
                    TextColor = Color.FromArgb("#006064"),
                    Margin = 10
                };

                titleLable.SetBinding(Label.TextProperty, "Title");

                var descriptionLabel = new Label();
                descriptionLabel.SetBinding(Label.TextProperty, new Binding { Path = "Description"});


                return new StackLayout
                {
                    Children = { titleLable, descriptionLabel },
                    Margin = 20
                };
            });
        }

        private void OnButtonClicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            LoadNewsAsync(true);
        }
    }
}
