namespace GhanaWoods;

public partial class ImageZoomModalPage : ContentPage
{
	public ImageZoomModalPage()
	{
		InitializeComponent();
	}

	public ImageZoomModalPage(ImageSource iSource)
	{
		InitializeComponent();

		Image0.Source = null;
		Image0.Source = iSource;
	}

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopModalAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopModalAsync(false);
        return true;
    }
}