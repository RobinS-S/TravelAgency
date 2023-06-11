using System.ComponentModel;

namespace TravelAgency.Client.Controls;

public partial class DateTimePicker
{
    public static readonly BindableProperty DateTimeProperty = BindableProperty.Create(
        propertyName: nameof(DateTime),
        returnType: typeof(DateTime),
        declaringType: typeof(DateTimePicker),
        defaultValue: DateTime.Now,
        defaultBindingMode: BindingMode.TwoWay
    );

    public DateTime DateTime
    {
        get => (DateTime)GetValue(DateTimeProperty);
        set => SetValue(DateTimeProperty, value);
    }

    public DateTimePicker()
    {
        InitializeComponent();
    }

    private void OnDatePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DatePicker.Date))
        {
            UpdateDateTime();
        }
    }

    private void OnTimePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
        {
            UpdateDateTime();
        }
    }

    private void UpdateDateTime()
    {
        DateTime = DatePicker.Date.Date + TimePicker.Time;
    }
}