using NewsApp.ViewModels;

namespace NewsApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        StackLayout layout = new StackLayout();
        CollectionView collectionView = new CollectionView();
        Button button = new Button { Text = "Использовать мок данные" };
        collectionView.ItemsLayout = LinearItemsLayout.Horizontal;
        InitializeComponent();
        BindingContext = new MainPageView(collectionView, button);

        layout.Children.Add(button);
        layout.Children.Add(collectionView);

        Content = layout;
    }
}