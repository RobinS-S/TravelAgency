using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Locations.Controls;

public partial class LocationListControl : ContentView
{
    public LocationListControl()
    {
        InitializeComponent();

        SizeChanged += LocationListControl_SizeChanged;
    }

    private void LocationListControl_SizeChanged(object? sender, EventArgs e)
    {
        bool isPortrait = Height > Width;
        if (isPortrait)
        {
            LocationCollection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
        }
        else
        {
            LocationCollection.ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Horizontal);
        }
    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var item = (LocationDto)frame.BindingContext;
        await ((dynamic)BindingContext).ViewDetailsCommand.ExecuteAsync(item.Id);
    }
}