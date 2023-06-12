using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Countries.Controls;

public partial class CountryListControl : ContentView
{
    public CountryListControl()
    {
        InitializeComponent();

        SizeChanged += CountriesPage_SizeChanged;
    }

    private void CountriesPage_SizeChanged(object? sender, EventArgs e)
    {
        bool isPortrait = Height > Width;
        if (isPortrait)
        {
            CountryCollection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
        }
        else
        {
            CountryCollection.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Horizontal);
        }
    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var item = (CountryDto)frame.BindingContext;
        await ((dynamic)BindingContext).ViewDetails(item.Id);
    }
}