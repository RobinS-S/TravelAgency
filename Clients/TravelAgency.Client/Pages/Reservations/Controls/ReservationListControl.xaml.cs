using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Reservations.Controls;

public partial class ReservationListControl : ContentView
{
    public ReservationListControl()
    {
        InitializeComponent();
    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var item = (ReservationDto)frame.BindingContext;
        await ((dynamic)BindingContext).ViewDetailsCommand.ExecuteAsync(item.Id);
    }
}