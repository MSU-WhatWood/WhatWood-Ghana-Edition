using GhanaWoods.Database;
using GhanaWoods.Resources.Helpers;
using static GhanaWoods.TabAssets;
using System.Globalization;
using GhanaWoods.Resources.Languages;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;
//using EventKit;

namespace GhanaWoods;

public partial class InitPage : ContentPage
{
    public bool langSwitch = false;
    public double minScreenSize;

    public InitPage()
    {
        InitializeComponent();

        this.Title = SettingsTab.Initialization;
        aLabel.Text = SettingsTab.Loading_assets;

        Shell.SetTabBarIsVisible(this, false);

        double dWid = 0;
        double dHei = 0;
        double dDen = 0;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
            dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
            dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
        });

        if (dHei < dWid) minScreenSize = (dHei / dDen);
        else minScreenSize = (dWid / dDen);

        if (minScreenSize > 700)
        {
            fplImage.Margin = new Thickness(200, 30, 200, 30);
            msuImage.Margin = new Thickness(100, 45, 100, 45);
        }
    }

    public InitPage(bool LangSwitch)
	{
		InitializeComponent();

        langSwitch = LangSwitch;

        this.Title = SettingsTab.Initialization;
        aLabel.Text = SettingsTab.Loading_assets;

        Shell.SetTabBarIsVisible(this, false);

        double dWid = 0;
        double dHei = 0;
        double dDen = 0;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
            dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
            dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
        });

        if (dHei < dWid) minScreenSize = (dHei / dDen);
        else minScreenSize = (dWid / dDen);

        if (minScreenSize > 700)
        {
            fplImage.Margin = new Thickness(200, 30, 200, 30);
            msuImage.Margin = new Thickness(100, 45, 100, 45);
        }
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();

        //Initialize local lists of classes from Entity Framework database
        keyEntries = new List<KeyEntry>();
        keyHistories = new List<KeyHistory>();
        speciesGroups = new List<SpeciesGroup>();

        FontS = Preferences.Get("FontSize", 16.0);

        //Populate class lists
        try
        {
            keyEntries = App.DB.GetKeyEntriesList();
            keyHistories = App.DB.GetKeyHistoriesList();
            speciesGroups = App.DB.GetSpeciesGroupsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("message:   " + ex.Message);
            Console.WriteLine("inner exception:   " + ex.InnerException);
        }

        //Initialize local lists of UI elements for Key and Species pages
        localGrids = new List<Grid>(keyEntries.Count);
        localBoxes = new List<BoxView>(keyEntries.Count);
        localLabels = new List<Label>(keyEntries.Count);
        localLabels1 = new List<Label>(keyEntries.Count);
        localButtons = new List<ImageButton>(keyEntries.Count);
        localButtons1 = new List<ImageButton>(keyEntries.Count);
        Console.WriteLine("InitPage     keyEntries count: " + keyEntries.Count.ToString());

        localGridsRef = new List<Grid>(speciesGroups.Count);
        localLabelsRef = new List<Label>(speciesGroups.Count);
        localLabelsRef1 = new List<Label>(speciesGroups.Count);
        localButtonsRef = new List<ImageButton>(speciesGroups.Count);
        Console.WriteLine("InitPage     speciesGroups count: " + speciesGroups.Count.ToString());


        //int language = Preferences.Get("Language", 0);
        //try
        //{
        //    if (CultureInfo.CurrentCulture.Name.Contains("en"))
        //    {
        //        language = 0;
        //    }
        //    else if (CultureInfo.CurrentCulture.Name.Contains("es"))
        //    {
        //        language = 1;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("ID ERROR  ID ERROR:   " + ex.ToString());
        //    Console.WriteLine("ID ERROR  ID ERROR:   " + ex.InnerException);
        //}
        int language = 0;

        //Create grids of decisions/entries for Key page
        try
        {
            for (int i = 0; i < keyEntries.Count; i++)
            {
                int trueIndex = (i / 2);

                CreateIDGrid(keyEntries[i], i, language);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("message:   " +  ex.Message);
            Console.WriteLine("inner exception:   " + ex.InnerException);
        }

        //Create grids of species groups for Species page
        for (int i = 0; i < speciesGroups.Count; i++)
        {
            CreateRefGrid(speciesGroups[i], i, language);
        }

        //If app language was switched on Settings page then return to Settings,
        //else (app was simply launched) go to "Launch Page" set in Settings or default of Key page
        if (langSwitch)
        {
            await Shell.Current.Navigation.PopAsync();
            await Shell.Current.GoToAsync("//Settings", false);
        }
        else
        {
            int launchPage = Preferences.Get("LaunchPage", 2);
            switch (launchPage)
            {
                case 0:
                    await Shell.Current.GoToAsync("//Study", false);
                    break;
                case 1:
                    await Shell.Current.GoToAsync("//Learn", false);
                    break;
                case 2:
                    await Shell.Current.GoToAsync("//ID", false);
                    break;
                case 3:
                    await Shell.Current.GoToAsync("//Reference", false);
                    break;
                default:
                    await Shell.Current.GoToAsync("//ID", false);
                    break;
            }
        }
    }

    //Method to create grid of decisions/entries for Key page
    private void CreateIDGrid(KeyEntry keyEntry, int index, int Language)
    {
        try
        {
            KeyEntry keyEnt = keyEntry;
            int trueIndex = index;

            //Create base grid for entries
            Grid grid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(2.5, GridUnitType.Absolute) },
                    //new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(2.5, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2.5, GridUnitType.Absolute) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(37.5, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(37.5, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Absolute) }
                },
                ClassId = keyEnt.KeyEntryID.ToString(),
                Margin = new Thickness(10),
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start
            };

            //Create BoxView for setting background color of top entry
            BoxView BV = new()
            {
                CornerRadius = 0,
                BackgroundColor = Colors.Transparent,
                Color = Colors.Transparent,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ClassId = keyEnt.KeyEntryID.ToString(),
            };
            grid.SetRowSpan(BV, 3);
            grid.SetColumnSpan(BV, 7);
            grid.Add(BV);

            BV.Color = Color.FromArgb("#BDE6E2");
            grid.BackgroundColor = Color.FromArgb("#BDD6E6");

            //Create black borders for base grid
            //Frame newFrame = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRow(newFrame, 0);
            grid.SetColumn(newFrame, 0);
            grid.SetColumnSpan(newFrame, 7);
            grid.Add(newFrame);
            //Frame newFrame1 = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame1 = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame1 = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRow(newFrame1, 2);
            grid.SetColumn(newFrame1, 0);
            grid.SetColumnSpan(newFrame1, 7);
            grid.Add(newFrame1);
            //Frame newFrame2 = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame2 = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame2 = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRow(newFrame2, 4);
            grid.SetColumn(newFrame2, 0);
            grid.SetColumnSpan(newFrame2, 7);
            grid.Add(newFrame2);

            //Frame newFrame3 = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame3 = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame3 = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRowSpan(newFrame3, 5);
            grid.SetRow(newFrame3, 0);
            grid.SetColumn(newFrame3, 0);
            grid.Add(newFrame3);
            //Frame newFrame4 = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame4 = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame4 = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRowSpan(newFrame4, 5);
            grid.SetRow(newFrame4, 0);
            grid.SetColumn(newFrame4, 2);
            grid.Add(newFrame4);
            //Frame newFrame5 = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame5 = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame5 = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRowSpan(newFrame5, 5);
            grid.SetRow(newFrame5, 0);
            grid.SetColumn(newFrame5, 4);
            grid.Add(newFrame5);
            //Frame newFrame6 = new()
            //{
            //    CornerRadius = 0,
            //    BackgroundColor = Colors.Black,
            //    BorderColor = Colors.Black,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            //Border newFrame6 = new()
            //{
            //    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            //    //StrokeShape = new Line(),
            //    BackgroundColor = Colors.Black,
            //    Stroke = Colors.Black,
            //    StrokeThickness = 0,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            BoxView newFrame6 = new()
            {
                Color = Colors.Black,
                BackgroundColor = Colors.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            grid.SetRowSpan(newFrame6, 5);
            grid.SetRow(newFrame6, 0);
            grid.SetColumn(newFrame6, 6);
            grid.Add(newFrame6);


            //Create labels for numbering of decisions
            Label labelNum0 = new()
            {
                Text = keyEnt.DecisionNum.ToString() + "a",
                ClassId = keyEnt.KeyEntryID.ToString(),
                Padding = new Thickness(0),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Colors.Black,
                FontSize = FontS
            };
            Label labelNum1 = new()
            {
                Text = keyEnt.DecisionNum.ToString() + "b",
                ClassId = keyEnt.KeyEntryID.ToString(),
                Padding = new Thickness(0),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Colors.Black,
                FontSize = FontS
            };
            grid.SetRow(labelNum0, 1);
            grid.SetColumn(labelNum0, 1);
            grid.SetRow(labelNum1, 3);
            grid.SetColumn(labelNum1, 1);
            grid.Add(labelNum0);
            grid.Add(labelNum1);

            localLabelsNum.Add(labelNum0);
            localLabelsNum1.Add(labelNum1);

            //Create labels for entries
            Label label0 = new Label()
            {
                ClassId = keyEnt.DecisionNum.ToString() + "a",
                Padding = new Thickness(0),
                Margin = new Thickness(10,5,10,5),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Colors.Black,
                FontSize = FontS
            };
            Label label1 = new Label()
            {
                ClassId = keyEnt.DecisionNum.ToString() + "b",
                Padding = new Thickness(0),
                Margin = new Thickness(10,5,10,5),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Colors.Black,
                FontSize = FontS
            };

            //if (CultureInfo.CurrentCulture.Name.Contains("en"))
            //{
                label0.Text = keyEnt.Text0;
                label1.Text = keyEnt.Text1;
            //}
            //else
            //{
            //    label0.Text = keyEnt.Text0ES;
            //    label1.Text = keyEnt.Text1ES;
            //}

            localLabels.Add(label0);
            localLabels1.Add(label1);

            //Create buttons for selecting example images
            ImageButton button = new()
            {
                ClassId = keyEnt.DecisionNum.ToString(),
                BackgroundColor = Color.FromArgb("#00A3AD"),
                Padding = new Thickness(4),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BorderColor = Colors.White,
                BorderWidth = 0,
                CornerRadius = 0,
                Source = ImageSource.FromFile("show_images_id_black_nofill.png"),
                Aspect = Aspect.AspectFit
            };
            localButtons.Add(button);

            ImageButton button1 = new()
            {
                ClassId = keyEnt.DecisionNum.ToString(),
                BackgroundColor = Color.FromArgb("#007FA3"),
                Padding = new Thickness(4),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BorderColor = Colors.White,
                BorderWidth = 0,
                CornerRadius = 0,
                Source = ImageSource.FromFile("show_images_id_black_nofill.png"),
                Aspect = Aspect.AspectFit
            };
            localButtons1.Add(button1);


            grid.SetRow(label0, 1);
            grid.SetColumn(label0, 3);

            grid.SetRow(label1, 3);
            grid.SetColumn(label1, 3);
            grid.SetRow(button, 1);
            grid.SetColumn(button, 5);
            grid.SetRow(button1, 3);
            grid.SetColumn(button1, 5);
            grid.Add(label0);
            grid.Add(label1);
            grid.Add(button);
            grid.Add(button1);

            Console.WriteLine("InitPage     ID grid created: " + keyEnt.KeyEntryID.ToString());
            localBoxes.Add(BV);
            localGrids.Add(grid);
        }
        catch (Exception ex)
        {
            Console.WriteLine("KeyEntry:   " + ex.Message);
            Console.WriteLine("KeyEntry exception:   " + ex.InnerException);
        }

        return;
    }

    private void CreateRefGrid(SpeciesGroup SpGr, int LeIndex, int Language)
    {
        try
        {
            SpeciesGroup spGr = SpGr;
            Grid grid = new Grid()
            {
                RowDefinitions =
            {
				new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = 0 }
			},
                ColumnDefinitions =
            {
                //new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(39, GridUnitType.Absolute) },
            },
                ClassId = "gr" + spGr.SpeciesGroupID.ToString(),
                Padding = new Thickness(10),
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start
            };


            //Frame frame = new()
            //{
            //    ClassId = spGr.SpeciesGroupID.ToString(),
            //    CornerRadius = 10,
            //    BackgroundColor = Color.FromArgb("#DAC79D"),
            //    BorderColor = Colors.White,
            //    VerticalOptions = LayoutOptions.Fill,
            //    HorizontalOptions = LayoutOptions.Fill
            //};
            Border frame = new()
            {
                ClassId = spGr.SpeciesGroupID.ToString(),
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) },
                BackgroundColor = Color.FromArgb("#DAC79D"),
                Stroke = Colors.White,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            //frame.AddLogicalChild(new VerticalStackLayout());

            Label label0 = new Label()
            {
                ClassId = spGr.SpeciesGroupID.ToString(),
                Margin = new Thickness(10),
                FontSize = FontS
            };
            Label label1 = new Label()
            {
                //FontFamily = "OpenSansItalic",
                FontFamily = "OpenSansRegular",
                ClassId = spGr.SpeciesGroupID.ToString(),
                FontSize = FontS
            };
            //if (spGr.Species != " ")
            if (spGr.NameES != " ")
            {
                label1.Text = spGr.NameES;
                label1.Margin = new Thickness(10, 0, 10, 9);
            }
            else
            {
                label0.VerticalOptions = LayoutOptions.Center;
                label0.VerticalTextAlignment = TextAlignment.Center;
            }


            //if (CultureInfo.CurrentCulture.Name.Contains("en")) label0.FormattedText = FormatRefTitle(spGr.Name, spGr.Spp);
            //else if (spGr.NameES != null) label0.FormattedText = FormatRefTitle(spGr.NameES, spGr.Spp);
            label0.FormattedText = FormatRefTitle(spGr.Name, spGr.Spp, spGr);

            TapGestureRecognizer tGR = new();
            tGR.Tapped += (s, e) =>
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
                Label? s1 = s as Label;
                int spGrID = 1;
                if (s1 != null) spGrID = int.Parse(s1.ClassId);

                Shell.Current.Navigation.PushAsync(new ReferenceGroupPageNew(speciesGroups[spGrID - 1]), false);
            };
            TapGestureRecognizer tGR1 = new();
            tGR1.Tapped += (s, e) =>
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
                //int spGrID = int.Parse(((Frame)s).ClassId);
                Border? s1 = s as Border;
                int spGrID = 1;
                if (s1 != null) spGrID = int.Parse(s1.ClassId);

                Shell.Current.Navigation.PushAsync(new ReferenceGroupPageNew(speciesGroups[spGrID - 1]), false);
            };
            frame.GestureRecognizers.Add(tGR1);
            label0.GestureRecognizers.Add(tGR);
            //if (spGr.Species != " ")
            if (spGr.NameES != " ")
            {
                label1.GestureRecognizers.Add(tGR);
            }
            else
            {
                grid.RowDefinitions[1].Height = 0;
            }

            localLabelsRef.Add(label0);
            localLabelsRef1.Add(label1);


            ImageButton button = new()
            {
                ClassId = spGr.SpeciesGroupID.ToString(),
                BackgroundColor = Color.FromArgb("#007FA3"),
                Padding = new Thickness(5),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BorderColor = Colors.White,
                Source = ImageSource.FromFile("show_images_id_black_nofill.png"),
                Aspect = Aspect.AspectFit,
                CornerRadius = 10
            };
            button.Clicked += (s, e) =>
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                if (grid.RowDefinitions[2].Height == 0)
                {
                    if (grid.Children.Count < 5)
                    {
                        Grid imgGrid = new Grid()
                        {
                            RowDefinitions =
                        {
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                        },
                            ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                        },
                            ClassId = "gr" + spGr.SpeciesGroupID.ToString(),
                            Padding = new Thickness(10, 5, 10, 10),
                            ColumnSpacing = 10,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill
                        };

                        List<string> fileNames = new List<string>();
                        fileNames = spGr.FileNames.Split("* ").ToList();
                        Image img0 = new()
                        {
                            Source = ImageSource.FromFile(fileNames[0]),
                            Aspect = Aspect.AspectFit
                        };
                        Image img1 = new()
                        {
                            Source = ImageSource.FromFile(fileNames[1]),
                            Aspect = Aspect.AspectFit
                        };
                        Image img2 = new()
                        {
                            Source = ImageSource.FromFile(fileNames[2]),
                            Aspect = Aspect.AspectFit
                        };
                        imgGrid.Add(img0, 0, 0);
                        imgGrid.Add(img1, 1, 0);
                        imgGrid.Add(img2, 2, 0);

                        TapGestureRecognizer zTGR = new TapGestureRecognizer();
                        zTGR.Tapped += async (s, e) =>
                        {
                            //await Shell.Current.Navigation.PushAsync(new ImageZoomPage(((Image)s).Source), false);
                            DevicePlatform dPlat = new DevicePlatform();
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                dPlat = DeviceInfo.Current.Platform;
                            });
                            Image? s1 = s as Image;
                            if (s1 != null)
                            {
                                if (dPlat == DevicePlatform.Android) await Shell.Current.Navigation.PushModalAsync(new ImageZoomModalPage(s1.Source), false);
                                else await Shell.Current.Navigation.PushAsync(new ImageZoomPage(s1.Source), false);
                            }
                            //await Navigation.PushAsync(new ImageZoomPage(((Image)s).Source), false);
                        };
                        img0.GestureRecognizers.Add(zTGR);
                        img1.GestureRecognizers.Add(zTGR);
                        img2.GestureRecognizers.Add(zTGR);

                        grid.SetColumnSpan(imgGrid, 2);
                        grid.SetRow(imgGrid, 2);
                        grid.SetColumn(imgGrid, 0);
                        grid.Add(imgGrid);
                    }

                    grid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    grid.RowDefinitions[2].Height = 0;
                }
            };
            localButtonsRef.Add(button);


            grid.SetRowSpan(frame, 3);
            grid.SetColumnSpan(frame, 2);
            
            grid.SetRow(label0, 0);
            grid.SetColumn(label0, 0);
            grid.SetRow(label1, 1);
            grid.SetColumn(label1, 0);
            grid.SetRow(button, 0);
            grid.SetColumn(button, 1);
            grid.SetRowSpan(button, 2);

            grid.Add(frame);
            grid.Add(label0);
            grid.Add(label1);
            grid.Add(button);

            Console.WriteLine("InitPage     Ref grid created: " + spGr.SpeciesGroupID.ToString());
            localGridsRef.Add(grid);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ref message:   " + ex.Message);
            Console.WriteLine("Ref source:   " + ex.Source);
            Console.WriteLine("Ref target site:   " + ex.TargetSite);
            Console.WriteLine("GroupID:   " + SpGr.SpeciesGroupID);
            Console.WriteLine("LeIndex: " + LeIndex);
        }

        return;
    }

    private FormattedString FormatString(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize
        };
        if (str[0] == '[')
        {
            span.FontAttributes = FontAttributes.Italic;
        }
        else
        {
            span.Text += str[0];
        }
        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] == '[')
            {
                fStr.Spans.Add(span);
                span = new()
                {
                    FontSize = fSize,
                    FontFamily = "OpenSansItalic"
                };
            }
            else if (str[i] == ']')
            {
                fStr.Spans.Add(span);
                span = new()
                {
                    FontSize = fSize,
                    FontFamily = "OpenSansRegular"
                };
            }
            else
            {
                span.Text += str[i];
            }
        }
        fStr.Spans.Add(span);

        return fStr;
    }


    private FormattedString FormatRefTitle(string inputStr, bool sppVal, SpeciesGroup spGr)
    {
        string str = inputStr;
        bool spp = sppVal;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize
        };
        bool ital = false;
        if (spp)
        {
            span.Text = str;
            span.FontFamily = "OpenSansBoldItalic";
            fStr.Spans.Add(span);
            span = new()
            {
                Text = ReferenceTabMain.spp_,
                FontFamily = "OpenSansBold",
                FontSize = fSize
            };
            fStr.Spans.Add(span);
            string fName = "";
            if (spGr.Species != " " && !string.IsNullOrEmpty(spGr.Species)) fName = "  [" + spGr.Species + "]";
            if (spGr.TransverseES != " " && !string.IsNullOrEmpty(spGr.TransverseES)) fName += Environment.NewLine + spGr.TransverseES;
            if (!string.IsNullOrEmpty(fName))
            {
                span = new()
                {
                    FontFamily = "OpenSansRegular",
                    FontSize = fSize
                };
                if (fName[0] == '[')
                {
                    span.FontFamily = "OpenSansItalic";
                }
                else
                {
                    span.Text += fName[0];
                    span.FontFamily = "OpenSansRegular";
                }
                for (int i = 1; i < fName.Length; i++)
                {
                    if (fName[i] == '[')
                    {
                        fStr.Spans.Add(span);
                        span = new()
                        {
                            FontSize = fSize,
                            FontFamily = "OpenSansItalic"
                        };
                    }
                    else if (fName[i] == ']')
                    {
                        fStr.Spans.Add(span);
                        span = new()
                        {
                            FontSize = fSize,
                            FontFamily = "OpenSansRegular"
                        };
                        //ital = true;
                    }
                    else
                    {
                        span.Text += fName[i];
                    }
                }
                //if (!ital) span.FontFamily = "OpenSansItalic";
                fStr.Spans.Add(span);
            }
        }
        else
        {
            if (str[0] == '[')
            {
                span.FontFamily = "OpenSansBoldItalic";
            }
            else
            {
                span.Text += str[0];
                span.FontFamily = "OpenSansBold";
            }
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '[')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBoldItalic"
                    };
                }
                else if (str[i] == ']')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                    ital = true;
                }
                else
                {
                    span.Text += str[i];
                }
            }
            if (!ital) span.FontFamily = "OpenSansBoldItalic";
            fStr.Spans.Add(span);

            //bool ital1 = false;
            string fName = "";
            if (spGr.Species != " " && !string.IsNullOrEmpty(spGr.Species)) fName = "  [" + spGr.Species + "]";
            if (spGr.TransverseES != " " && !string.IsNullOrEmpty(spGr.TransverseES)) fName += Environment.NewLine + spGr.TransverseES;
            if (!string.IsNullOrEmpty(fName))
            {
                span = new()
                {
                    FontFamily = "OpenSansRegular",
                    FontSize = fSize
                };
                if (fName[0] == '[')
                {
                    span.FontFamily = "OpenSansItalic";
                }
                else
                {
                    span.Text += fName[0];
                    span.FontFamily = "OpenSansRegular";
                }
                for (int i = 1; i < fName.Length; i++)
                {
                    if (fName[i] == '[')
                    {
                        fStr.Spans.Add(span);
                        span = new()
                        {
                            FontSize = fSize,
                            FontFamily = "OpenSansItalic"
                        };
                    }
                    else if (fName[i] == ']')
                    {
                        fStr.Spans.Add(span);
                        span = new()
                        {
                            FontSize = fSize,
                            FontFamily = "OpenSansRegular"
                        };
                        //ital = true;
                    }
                    else
                    {
                        span.Text += fName[i];
                    }
                }
                //if (!ital) span.FontFamily = "OpenSansItalic";
                fStr.Spans.Add(span);
            }
        }

        return fStr;
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}
