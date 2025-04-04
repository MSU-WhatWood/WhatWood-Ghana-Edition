using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using GhanaWoods.Database;
using static GhanaWoods.TabAssets;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace GhanaWoods;

public partial class ReferenceMainPageNew : ContentPage
{
	bool isCreated = false;
	public double FSize = Preferences.Get("FontSize", 16.0);
    List<SpeciesGroup> sGroups = new List<SpeciesGroup>();
	public ReferenceMainPageNew()
	{
		InitializeComponent();

		this.Title = ReferenceTabMain.referenceFC;

		this.HideSoftInputOnTapped = true;

		isCreated = false;

        DevicePlatform dPlat = new DevicePlatform();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        if (dPlat == DevicePlatform.Android) this.SizeChanged += OnSizeChanged;

        sGroups = new List<SpeciesGroup>();
        sGroups = App.DB.GetSpeciesGroupsList();
    }

    private void OnSizeChanged(object? sender, EventArgs e)
    {
        DisplayOrientation dOri = new DisplayOrientation();
        double sWid = 0;
        double dWid = 0;
        double dHei = 0;
        double dDen = 0;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            dOri = DeviceDisplay.Current.MainDisplayInfo.Orientation;
            dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
            dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
            dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
        });

        if (dHei < dWid) sWid = (dHei / dDen);
        else sWid = (dWid / dDen);

        if (dOri == DisplayOrientation.Landscape)
        {
            if (sWid < 700)
            {
                Shell.SetNavBarIsVisible(this, false);
                Shell.SetTabBarIsVisible(this, false);
            }
        }
        else
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, true);
        }
    }

    protected override void OnAppearing()
	{
		base.OnAppearing();

		bool fsChanged = Preferences.Get("FontSizeChangedRef", false);
        if (!isCreated || fsChanged)
        {
			FSize = Preferences.Get("FontSize", 16.0);

			aVSL.Children.Clear();

			for (int i = 0; i < speciesGroups.Count; i++)
            {
				if (fsChanged)
				{
                    for (int j = 0; j < localLabelsRef[i].FormattedText.Spans.Count; j++)
                    {
                        localLabelsRef[i].FormattedText.Spans[j].FontSize = FSize;
                    }
                    localLabelsRef1[i].FontSize = FSize;
                }
				
				aVSL.Children.Add(localGridsRef[i]);						
			}

			Preferences.Set("FontSizeChangedRef", false);
            isCreated = true;
        }

        return;
	}

    void OnTextChanged(object sender, EventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
		string query = searchBar.Text;
        //List<SpeciesGroup> sGs = App.DB.GetSpeciesGroupsList();
        List<SpeciesGroup> sGs = sGroups;

        if (!string.IsNullOrEmpty(query))
		{
			for (int i = 0; i < localGridsRef.Count; i++)
			{
                //if (!sGs[i].Name.ToLower().Contains(query.ToLower())) localGridsRef[i].IsVisible = false;
                //else localGridsRef[i].IsVisible = true;
                //if (!sGs[i].NameES.ToLower().Contains(query.ToLower())) localGridsRef[i].IsVisible = false;
                //else localGridsRef[i].IsVisible = true;

                //if (!string.IsNullOrEmpty(sGs[i].NameES) && sGs[i].NameES != " ") 
                string queryL = query.ToLower();
                if (!sGs[i].Name.ToLower().Contains(queryL) && !sGs[i].NameES.ToLower().Contains(queryL) &&
                    !sGs[i].TransverseES.ToLower().Contains(queryL) && !sGs[i].Species.ToLower().Contains(queryL)) localGridsRef[i].IsVisible = false;
                else localGridsRef[i].IsVisible = true;
            }
		}
		else
		{
			for (int i = 0; i < localGridsRef.Count; i++)
			{
				localGridsRef[i].IsVisible = true;
			}
		}
    }

    private async void SB_SearchButtonPressed(object sender, EventArgs e)
    {
		await SB.HideSoftInputAsync(CancellationToken.None);
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}