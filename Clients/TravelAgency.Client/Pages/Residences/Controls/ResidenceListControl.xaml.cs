using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Residences.Controls;

public partial class ResidenceListControl : ContentView
{
	public ResidenceListControl()
	{
		InitializeComponent();

        SizeChanged += ResidenceListControl_SizeChanged;
	}

    private void ResidenceListControl_SizeChanged(object? sender, EventArgs e)
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
        var item = (ResidenceDto)frame.BindingContext;
        if (item?.Id != null)
        {
            await ((dynamic)BindingContext).ViewDetails(item.Id.Value);
        }
    }
}