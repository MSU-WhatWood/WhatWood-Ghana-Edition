using GhanaWoods.Resources.Helpers;
using GhanaWoods.Resources.Languages;
using GhanaWoods.Database;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class LearnFeaturesPage : ContentPage
{
    public double FSize = Preferences.Get("FontSize", 16.0);
    bool isCreated = false;
    List<string> SWcategories = new List<string> { LearnTabMain.SWcat1, LearnTabMain.SWcat2, LearnTabMain.SWcat3, LearnTabMain.SWcat4 };
    List<List<int>> SWfeaturesLists = new List<List<int>>();
    Dictionary<int, string> Softwoods = new Dictionary<int, string>();
    List<int> SWfeaturesCombined = new List<int> { 40, 41, 42, 43, 72, 73, 74, 109, 111 };
    List<string> HWcategories = new List<string> { LearnTabMain.HWcat1, LearnTabMain.HWcat2, LearnTabMain.HWcat3, LearnTabMain.HWcat4, LearnTabMain.HWcat5,
                                                   LearnTabMain.HWcat6, LearnTabMain.HWcat7, LearnTabMain.HWcat8, LearnTabMain.HWcat9, LearnTabMain.HWcat10 };
    List<List<int>> HWfeaturesLists = new List<List<int>>();
    Dictionary<int, string> Hardwoods = new Dictionary<int, string>();
    List<int> HWfeaturesCombined = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 56,
                                                   58, 96, 97, 98, 99, 114, 115, 116, 76,
                                                   77, 79, 80, 81, 82, 83, 85, 86,
                                                   87, 89};
    List<int> CorrectAnswers = new List<int>();
    List<int> BlockedAnswers = new List<int>();
    List<int> SelectedAnswers = new List<int>();
    int HardwoodOrSoftwood = 0;

    List<string> extraImages = new List<string>();
    int imagesIndex = 0;

    DevicePlatform dPlat = new();
    TapGestureRecognizer zTGR = new();

    public LearnFeaturesPage()
	{
		InitializeComponent();

        this.Title = LearnTabMain.learnFC;

        isCreated = false;

        FSize = Preferences.Get("FontSize", 16.0);

        AddSoftwoods();

        AddHardwoods();

        dPlat = new DevicePlatform();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        if (dPlat == DevicePlatform.Android) this.SizeChanged += OnSizeChanged;

        zTGR = new TapGestureRecognizer();
        zTGR.Tapped += async (s, e) =>
        {
            Image? s1 = s as Image;
            if (s1 != null) await AppShell.Current.Navigation.PushAsync(new ImageZoomPage(s1.Source), false);
        };
        Image0.GestureRecognizers.Add(zTGR);

        if (dPlat == DevicePlatform.Android) iGRIDrd2.Height = new GridLength(1, GridUnitType.Auto);
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
            if (dPlat == DevicePlatform.Android) iGRIDrd2.Height = new GridLength(0, GridUnitType.Absolute);
            ImageSource iSource = Image0.Source;
            Image0.Source = null;
            Image0.Source = iSource;
        }
        else
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, true);
            if (dPlat == DevicePlatform.Android) iGRIDrd2.Height = new GridLength(1, GridUnitType.Auto);
            ImageSource iSource = Image0.Source;
            Image0.Source = null;
            Image0.Source = iSource;
        }
    }

    private void AddSoftwoods()
    {
        Softwoods = new Dictionary<int, string>();

        Softwoods.Add(40, LearnTabMain.SW40);
        Softwoods.Add(41, LearnTabMain.SW41);
        Softwoods.Add(42, LearnTabMain.SW42);
        Softwoods.Add(43, LearnTabMain.SW43);
        Softwoods.Add(72, LearnTabMain.SW72);
        Softwoods.Add(73, LearnTabMain.SW73);
        Softwoods.Add(74, LearnTabMain.SW74);
        Softwoods.Add(109, LearnTabMain.SW109);
        Softwoods.Add(111, LearnTabMain.SW111);

        SWfeaturesLists = new List<List<int>>();

        for (int i = 0; i < SWcategories.Count; i++)
        {
            SWfeaturesLists.Add(new List<int>());

            switch (i)
            {
                case 0:
                    SWfeaturesLists[0].Add(40);
                    SWfeaturesLists[0].Add(41);
                    break;
                case 1:
                    SWfeaturesLists[1].Add(42);
                    SWfeaturesLists[1].Add(43);
                    break;
                case 2:
                    SWfeaturesLists[2].Add(72);
                    SWfeaturesLists[2].Add(73);
                    SWfeaturesLists[2].Add(74);
                    break;
                case 3:
                    SWfeaturesLists[3].Add(109);
                    SWfeaturesLists[3].Add(111);
                    break;
            }
        }
    }
    private void AddHardwoods()
    {
        Hardwoods = new Dictionary<int, string>();

        Hardwoods.Add(1, LearnTabMain.HW1);
        Hardwoods.Add(2, LearnTabMain.HW2);
        Hardwoods.Add(3, LearnTabMain.HWf3);
        Hardwoods.Add(4, LearnTabMain.HWf4);
        Hardwoods.Add(5, LearnTabMain.HWf5);
        Hardwoods.Add(6, LearnTabMain.HW6);
        Hardwoods.Add(7, LearnTabMain.HW7);
        Hardwoods.Add(8, LearnTabMain.HW8);
        Hardwoods.Add(9, LearnTabMain.HW9);
        Hardwoods.Add(10, LearnTabMain.HWf10);
        Hardwoods.Add(11, LearnTabMain.HWf11);
        Hardwoods.Add(56, LearnTabMain.HWf56);
        Hardwoods.Add(58, LearnTabMain.HW58);
        Hardwoods.Add(96, LearnTabMain.HW96);
        Hardwoods.Add(97, LearnTabMain.HW97);
        Hardwoods.Add(98, LearnTabMain.HW98);
        Hardwoods.Add(99, LearnTabMain.HW99);
        Hardwoods.Add(114, LearnTabMain.HW114);
        Hardwoods.Add(115, LearnTabMain.HW115);
        Hardwoods.Add(116, LearnTabMain.HW116);
        Hardwoods.Add(76, LearnTabMain.HW76);
        Hardwoods.Add(77, LearnTabMain.HW77);
        //Hardwoods.Add(78, LearnTabMain.HW78);
        Hardwoods.Add(79, LearnTabMain.HW79);
        Hardwoods.Add(80, LearnTabMain.HW80);
        Hardwoods.Add(81, LearnTabMain.HW81);
        Hardwoods.Add(82, LearnTabMain.HW82);
        Hardwoods.Add(83, LearnTabMain.HW83);
        //Hardwoods.Add(84, LearnTabMain.HW84);
        Hardwoods.Add(85, LearnTabMain.HW85);
        Hardwoods.Add(86, LearnTabMain.HW86);
        Hardwoods.Add(87, LearnTabMain.HW87);
        Hardwoods.Add(89, LearnTabMain.HW89);

        HWfeaturesLists = new List<List<int>>();

        for (int i = 0; i < HWcategories.Count; i++)
        {
            HWfeaturesLists.Add(new List<int>());

            switch (i)
            {
                case 0:
                    HWfeaturesLists[0].Add(1);
                    HWfeaturesLists[0].Add(2);
                    break;
                case 1:
                    HWfeaturesLists[1].Add(3);
                    HWfeaturesLists[1].Add(4);
                    HWfeaturesLists[1].Add(5);
                    break;
                case 2:
                    HWfeaturesLists[2].Add(6);
                    HWfeaturesLists[2].Add(7);
                    HWfeaturesLists[2].Add(8);
                    break;
                case 3:
                    HWfeaturesLists[3].Add(9);
                    HWfeaturesLists[3].Add(10);
                    HWfeaturesLists[3].Add(11);
                    break;
                case 4:
                    HWfeaturesLists[4].Add(56);
                    HWfeaturesLists[4].Add(58);
                    break;
                case 5:
                    HWfeaturesLists[5].Add(96);
                    HWfeaturesLists[5].Add(97);
                    HWfeaturesLists[5].Add(98);
                    HWfeaturesLists[5].Add(99);
                    break;
                case 6:
                    HWfeaturesLists[6].Add(114);
                    HWfeaturesLists[6].Add(115);
                    HWfeaturesLists[6].Add(116);
                    break;
                case 7:
                    HWfeaturesLists[7].Add(76);
                    HWfeaturesLists[7].Add(77);
                    break;
                case 8:
                    //HWfeaturesLists[8].Add(78);
                    HWfeaturesLists[8].Add(79);
                    HWfeaturesLists[8].Add(80);
                    HWfeaturesLists[8].Add(81);
                    HWfeaturesLists[8].Add(82);
                    HWfeaturesLists[8].Add(83);
                    //HWfeaturesLists[8].Add(84);
                    break;
                case 9:
                    HWfeaturesLists[9].Add(85);
                    HWfeaturesLists[9].Add(86);
                    HWfeaturesLists[9].Add(87);
                    HWfeaturesLists[9].Add(89);
                    break;
            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!isCreated && Image0.Source == null)
        {
            int Counter = 0;

            qGRID.Clear();
            for (int i = qGRID.RowDefinitions.Count - 1; i >= 0; i--) qGRID.RowDefinitions.RemoveAt(i);
            int SpeciesID = 0;
            //SpeciesTbl qSpecies = new SpeciesTbl();
            SpeciesGroup qGroup = new SpeciesGroup();
            List<int> qCategories = new List<int>();
            CorrectAnswers = new List<int>();
            BlockedAnswers = new List<int>();
            SelectedAnswers = new List<int>();
            HardwoodOrSoftwood = 0;
            extraImages = new List<string>();
            imagesIndex = 0;
            Random rand = new Random();
            int HWorSW = rand.Next(0, 7);
            qLabel.FormattedText?.Spans.Clear();
            FormattedString fStr = new();
            //if (HWorSW < 6)
            //{
                HardwoodOrSoftwood = 0;
            //qSpecies = App.DB.SelectHWSpeciesForFeatures();
            //SpeciesID = qSpecies.SpeciesTblID;
            //fStr = FormatQuestion(LearnTabMain.sAFs + qSpecies.Name);
            //if (qSpecies.Features != null)
            //{
            //    CorrectAnswers = App.DB.GetCorrectAnswersForFeatures(qSpecies.Features);
            //    BlockedAnswers = App.DB.GetBlockedAnswersForFeatures(qSpecies.Features);
            //}
            qGroup = App.DB.SelectHWGroupForFeatures();
            SpeciesID = qGroup.SpeciesGroupID;
            fStr = FormatQuestion(LearnTabMain.sAFs + qGroup.Name);
            if (qGroup.OtherCharactersES != null)
            {
                CorrectAnswers = App.DB.GetCorrectAnswersForFeatures(qGroup.OtherCharactersES);
                BlockedAnswers = App.DB.GetBlockedAnswersForFeatures(qGroup.OtherCharactersES);
            }
            qCategories = App.DB.GetHWCategoriesForFeatures(CorrectAnswers);
                for (Counter = 0; Counter < qCategories.Count; Counter++)
                {
                    CreateRD(HWcategories[qCategories[Counter]], HWfeaturesLists[qCategories[Counter]], Counter);
                }
                this.Title = LearnTabMain.Hardwoods;
            //}
            //else
            //{
            //    HardwoodOrSoftwood = 1;
            //    qSpecies = App.DB.SelectSWSpeciesForFeatures();
            //    SpeciesID = qSpecies.SpeciesTblID;
            //    fStr = FormatQuestion(LearnTabMain.sAFs + qSpecies.Name);
            //    if (qSpecies.Features != null)
            //    {
            //        CorrectAnswers = App.DB.GetCorrectAnswersForFeatures(qSpecies.Features);
            //        BlockedAnswers = App.DB.GetBlockedAnswersForFeatures(qSpecies.Features);
            //    }
            //    qCategories = App.DB.GetSWCategoriesForFeatures(CorrectAnswers);
            //    for (Counter = 0; Counter < qCategories.Count; Counter++)
            //    {
            //        CreateRD(SWcategories[qCategories[Counter]], SWfeaturesLists[qCategories[Counter]], Counter);
            //    }
            //    this.Title = LearnTabMain.Softwoods;
            //}

            qLabel.FormattedText = fStr;

            CreateConfirm(Counter);


            //ImageLabel0.FormattedText?.Spans.Clear();
            //ImageLabel0.FormattedText = FormatSpecies(qSpecies.Name);


            List<ImageTbl> qImages = new List<ImageTbl>();
            //qImages = App.DB.GetUniqueImageTblsBySpeciesID(qSpecies.SpeciesTblID);
            qImages = App.DB.GetUniqueImageTblsByGroupID(qGroup.SpeciesGroupID);
            Image0.Source = null;
            Image0.Source = ImageSource.FromFile(qImages[0].FileName);
            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel0.FormattedText = FormatSpecies(qImages[0].ImageName);

            if (qImages.Count > 1)
            {
                extraImages = new();
                for (int i = 0; i < qImages.Count; i++) extraImages.Add(qImages[i].FileName);
                imagesIndex = 0;
                arrowLeft.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages = new();
                imagesIndex = 0;
                arrowLeft.BackgroundColor = Colors.Gray;
                arrowRight.BackgroundColor = Colors.Gray;
            }

            await qSV.ScrollToAsync(0.0, 0.0, false);

            isCreated = true;
        }

        return;
    }

    private void CreateConfirm(int counter = 0)
    {
        ImageButton confirm = new()
        {
            BackgroundColor = Color.FromArgb("#C99700"),
            BorderColor = Colors.Black,
            BorderWidth = 2.5,
            Source = ImageSource.FromFile("big_check_blk.png"),
            CornerRadius = 15,
            Aspect = Aspect.Center,
            Margin = new Thickness(30, 10, 30, 20),
            HeightRequest = 70,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center
        };
        confirm.Clicked += ConfirmClicked;
        RowDefinition rowDefinition2 = new()
        {
            Height = new GridLength(1, GridUnitType.Auto)
        };
        qGRID.AddRowDefinition(rowDefinition2);
        qGRID.Add(confirm, 0, counter);
    }

    private async void ConfirmClicked(object? sender, EventArgs e)
    {
        int correctNum = 0;
        int totalNum = CorrectAnswers.Count;
        int incorrectNum = 0;
        List<int> missedCorrect = new List<int>();
        List<int> selectedIncorrect = new List<int>();
        missedCorrect = CorrectAnswers;
        for (int i = 0; i < SelectedAnswers.Count; i++)
        {
            if (CorrectAnswers.Contains(SelectedAnswers[i]))
            {
                correctNum++;
                missedCorrect.Remove(SelectedAnswers[i]);
            }
            else
            {
                incorrectNum++;
                selectedIncorrect.Add(SelectedAnswers[i]);
            }
        }
        string alertMessage = string.Empty;
        if (correctNum < totalNum || incorrectNum > 0)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            alertMessage = LearnTabMain.Correct_answers_selected_ + "\n" + correctNum.ToString() + "/" + totalNum.ToString() + "\n";
        }
        else
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            alertMessage = LearnTabMain.Well_done__Correct_answers_selected_ + "\n" + correctNum.ToString() + "/" + totalNum.ToString() + "\n";
        }
        if (correctNum < totalNum)
        {
            for (int i = 0; i < missedCorrect.Count; i++)
            {
                if (i == 0)
                {
                    if (HardwoodOrSoftwood == 0) alertMessage += LearnTabMain.Missed_ + Hardwoods[missedCorrect[i]];
                    else alertMessage += LearnTabMain.Missed_ + Softwoods[missedCorrect[i]];
                }
                else
                {
                    if (HardwoodOrSoftwood == 0) alertMessage += ", " + Hardwoods[missedCorrect[i]];
                    else alertMessage += ", " + Softwoods[missedCorrect[i]];
                }
            }
            alertMessage += "\n";
        }
        alertMessage += "\n" + LearnTabMain.Incorrect_selections_ + "\n" + incorrectNum.ToString() + "\n";
        if (incorrectNum > 0)
        {
            for (int i = 0; i < selectedIncorrect.Count; i++)
            {
                if (i == 0)
                {
                    if (HardwoodOrSoftwood == 0) alertMessage += LearnTabMain.Selected_ + Hardwoods[selectedIncorrect[i]];
                    else alertMessage += LearnTabMain.Selected_ + Softwoods[selectedIncorrect[i]];
                }
                else
                {
                    if (HardwoodOrSoftwood == 0) alertMessage += ", " + Hardwoods[selectedIncorrect[i]];
                    else alertMessage += ", " + Softwoods[selectedIncorrect[i]];
                }
            }
            alertMessage += "\n";
        }
        await DisplayAlert(LearnTabMain.Result_, alertMessage, LearnTabMain.Continue);

        int Counter = 0;

        qGRID.Clear();
        for (int i = qGRID.RowDefinitions.Count - 1; i >= 0; i--) qGRID.RowDefinitions.RemoveAt(i);
        int SpeciesID = 0;
        //SpeciesTbl qSpecies = new SpeciesTbl();
        SpeciesGroup qGroup = new SpeciesGroup();
        List<int> qCategories = new List<int>();
        CorrectAnswers = new List<int>();
        BlockedAnswers = new List<int>();
        SelectedAnswers = new List<int>();
        HardwoodOrSoftwood = 0;
        Random rand = new Random();
        int HWorSW = rand.Next(0, 7);
        qLabel.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        //if (HWorSW < 6)
        //{
        //HardwoodOrSoftwood = 0;
        //qSpecies = App.DB.SelectHWSpeciesForFeatures();
        //SpeciesID = qSpecies.SpeciesTblID;
        //fStr = FormatQuestion(LearnTabMain.sAFs + qSpecies.Name);
        //if (qSpecies.Features != null)
        //{
        //    CorrectAnswers = App.DB.GetCorrectAnswersForFeatures(qSpecies.Features);
        //    BlockedAnswers = App.DB.GetBlockedAnswersForFeatures(qSpecies.Features);
        //}
        HardwoodOrSoftwood = 0;
        qGroup = App.DB.SelectHWGroupForFeatures();
        SpeciesID = qGroup.SpeciesGroupID;
        fStr = FormatQuestion(LearnTabMain.sAFs + qGroup.Name);
        if (qGroup.OtherCharactersES != null)
        {
            CorrectAnswers = App.DB.GetCorrectAnswersForFeatures(qGroup.OtherCharactersES);
            BlockedAnswers = App.DB.GetBlockedAnswersForFeatures(qGroup.OtherCharactersES);
        }
        qCategories = App.DB.GetHWCategoriesForFeatures(CorrectAnswers);
            for (Counter = 0; Counter < qCategories.Count; Counter++)
            {
                CreateRD(HWcategories[qCategories[Counter]], HWfeaturesLists[qCategories[Counter]], Counter);
            }
            this.Title = LearnTabMain.Hardwoods;
        //}
        //else
        //{
        //    HardwoodOrSoftwood = 1;
        //    qSpecies = App.DB.SelectSWSpeciesForFeatures();
        //    SpeciesID = qSpecies.SpeciesTblID;
        //    fStr = FormatQuestion(LearnTabMain.sAFs + qSpecies.Name);
        //    if (qSpecies.Features != null)
        //    {
        //        CorrectAnswers = App.DB.GetCorrectAnswersForFeatures(qSpecies.Features);
        //        BlockedAnswers = App.DB.GetBlockedAnswersForFeatures(qSpecies.Features);
        //    }
        //    qCategories = App.DB.GetSWCategoriesForFeatures(CorrectAnswers);
        //    for (Counter = 0; Counter < qCategories.Count; Counter++)
        //    {
        //        CreateRD(SWcategories[qCategories[Counter]], SWfeaturesLists[qCategories[Counter]], Counter);
        //    }
        //    this.Title = LearnTabMain.Softwoods;
        //}

        qLabel.FormattedText = fStr;

        CreateConfirm(Counter);

        //ImageLabel0.FormattedText?.Spans.Clear();
        //ImageLabel0.FormattedText = FormatSpecies(qSpecies.Name);

        List<ImageTbl> qImages = new List<ImageTbl>();
        //qImages = App.DB.GetUniqueImageTblsBySpeciesID(qSpecies.SpeciesTblID);
        qImages = App.DB.GetUniqueImageTblsByGroupID(qGroup.SpeciesGroupID);
        Image0.Source = null;
        Image0.Source = ImageSource.FromFile(qImages[0].FileName);
        ImageLabel0.FormattedText?.Spans.Clear();
        ImageLabel0.FormattedText = FormatSpecies(qImages[0].ImageName);

        if (qImages.Count > 1)
        {
            extraImages = new();
            for (int i = 0; i < qImages.Count; i++) extraImages.Add(qImages[i].FileName);
            imagesIndex = 0;
            arrowLeft.BackgroundColor = Color.FromArgb("#FFB81C");
            arrowRight.BackgroundColor = Color.FromArgb("#FFB81C");
        }
        else
        {
            extraImages = new();
            imagesIndex = 0;
            arrowLeft.BackgroundColor = Colors.Gray;
            arrowRight.BackgroundColor = Colors.Gray;
        }

        await qSV.ScrollToAsync(0.0, 0.0, false);
    }

    void OnArrowLeftClicked(object? sender, EventArgs args)
    {
        if (extraImages.Count > 0)
        {
            if (extraImages.Count > 1)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages);
                List<FormattedString> formattedImageNames = new List<FormattedString>();
                for (int i = 0; i < imageNames.Count; i++)
                {
                    formattedImageNames.Add(FormatSpecies(imageNames[i]));
                    Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
                }

                int numGroups = imageNames.Count;
                int imageIndex = numGroups - 1;

                for (int i = numGroups - 1; i >= 0; i--)
                {
                    if (imageIndex == imagesIndex && i > 0)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;
                        ImageLabel0.FormattedText = formattedImageNames[imageIndex - 1];
                        Image0.Source = ImageSource.FromFile(extraImages[imageIndex - 1]);
                        
                        Console.WriteLine("SETTING IMAGES:   1");
                        imagesIndex = imageIndex - 1;

                        break;
                    }
                    else if (i == 0)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;

                        imageIndex = numGroups - 1;

                        ImageLabel0.FormattedText = formattedImageNames[imageIndex];
                        Image0.Source = ImageSource.FromFile(extraImages[imageIndex]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        imagesIndex = imageIndex;

                        break;
                    }

                    imageIndex -= 1;
                }
            }
        }
    }
    void OnArrowRightClicked(object? sender, EventArgs args)
    {
        if (extraImages.Count > 0)
        {
            if (extraImages.Count > 1)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages);
                List<FormattedString> formattedImageNames = new List<FormattedString>();
                for (int i = 0; i < imageNames.Count; i++)
                {
                    formattedImageNames.Add(FormatSpecies(imageNames[i]));
                    Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
                }

                int numGroups = imageNames.Count;
                int imageIndex = 0;

                for (int i = 0; i < numGroups; i++)
                {
                    if (imageIndex == imagesIndex && i < numGroups - 1)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;
                        ImageLabel0.FormattedText = formattedImageNames[imageIndex + 1];
                        Image0.Source = ImageSource.FromFile(extraImages[imageIndex + 1]);

                        Console.WriteLine("SETTING IMAGES:   1");
                        imagesIndex = imageIndex + 1;

                        break;
                    }
                    else if (i == numGroups - 1)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;
                        ImageLabel0.FormattedText = formattedImageNames[0];
                        Image0.Source = ImageSource.FromFile(extraImages[0]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        imagesIndex = 0;

                        break;
                    }

                    imageIndex += 1;
                }
            }
        }
    }

    private void CreateRD(string keyEntry, List<int> ints, int counter = 0)
    {
        Grid entry = CreateEntry(keyEntry, ints, 0);
        RowDefinition rowDefinition = new()
        {
            Height = new GridLength(1, GridUnitType.Auto)
        };
        qGRID.AddRowDefinition(rowDefinition);
        qGRID.Add(entry, 0, counter);
    }

    private Grid CreateEntry(string keyEntry, List<int> ints, int counter = 0)
    {
        string entryName = keyEntry;
        List<int> entries = ints;
        int indexNum = counter;
        Grid grid = new Grid()
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(2.5, GridUnitType.Absolute) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(2.5, GridUnitType.Absolute) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2.5, GridUnitType.Absolute) }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Absolute) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Absolute) }
            },
            Margin = new Thickness(10, 10, 10, 10),
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            BackgroundColor = Color.FromArgb("#C9BDB1")
        };


        BoxView BV = new()
        {
            CornerRadius = 0,
            BackgroundColor = Colors.Transparent,
            Color = Colors.Transparent,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };


        //Frame newFrame = new()
        //{
        //    CornerRadius = 0,
        //    BackgroundColor = Colors.Black,
        //    BorderColor = Colors.Black,
        //    VerticalOptions = LayoutOptions.Fill,
        //    HorizontalOptions = LayoutOptions.Fill
        //};
        Border newFrame = new()
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            BackgroundColor = Colors.Black,
            Stroke = Colors.Black,
            StrokeThickness = 0,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };
        grid.SetRow(newFrame, 0);
        grid.SetColumn(newFrame, 0);
        grid.SetColumnSpan(newFrame, 3);
        grid.Add(newFrame);
        //Frame newFrame1 = new()
        //{
        //    CornerRadius = 0,
        //    BackgroundColor = Colors.Black,
        //    BorderColor = Colors.Black,
        //    VerticalOptions = LayoutOptions.Fill,
        //    HorizontalOptions = LayoutOptions.Fill
        //};
        Border newFrame1 = new()
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            BackgroundColor = Colors.Black,
            Stroke = Colors.Black,
            StrokeThickness = 0,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };
        grid.SetRow(newFrame1, 2);
        grid.SetColumn(newFrame1, 0);
        grid.SetColumnSpan(newFrame1, 3);
        grid.Add(newFrame1);
        //Frame newFrame2 = new()
        //{
        //    CornerRadius = 0,
        //    BackgroundColor = Colors.Black,
        //    BorderColor = Colors.Black,
        //    VerticalOptions = LayoutOptions.Fill,
        //    HorizontalOptions = LayoutOptions.Fill
        //};
        Border newFrame2 = new()
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            BackgroundColor = Colors.Black,
            Stroke = Colors.Black,
            StrokeThickness = 0,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };
        grid.SetRow(newFrame2, 4);
        grid.SetColumn(newFrame2, 0);
        grid.SetColumnSpan(newFrame2, 3);
        grid.Add(newFrame2);


        //Frame newFrame3 = new()
        //{
        //    CornerRadius = 0,
        //    BackgroundColor = Colors.Black,
        //    BorderColor = Colors.Black,
        //    VerticalOptions = LayoutOptions.Fill,
        //    HorizontalOptions = LayoutOptions.Fill
        //};
        Border newFrame3 = new()
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            BackgroundColor = Colors.Black,
            Stroke = Colors.Black,
            StrokeThickness = 0,
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
        Border newFrame4 = new()
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(0) },
            BackgroundColor = Colors.Black,
            Stroke = Colors.Black,
            StrokeThickness = 0,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };
        grid.SetRowSpan(newFrame4, 5);
        grid.SetRow(newFrame4, 0);
        grid.SetColumn(newFrame4, 2);
        grid.Add(newFrame4);


        Label labelNum0 = new()
        {
            Text = entryName,
            Padding = new Thickness(5, 5, 5, 5),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Fill,
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        grid.SetRow(labelNum0, 1);
        grid.SetColumn(labelNum0, 1);
        grid.Add(labelNum0);


        Grid gridChoices = new()
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto)},
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            },
            Margin = new Thickness(10),
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            ColumnSpacing = 10,
            RowSpacing = 5
        };
        grid.SetRow(gridChoices, 3);
        grid.SetColumn(gridChoices, 1);
        grid.Add(gridChoices);

        int gridRow = 0;
        for (int i = 0; i < entries.Count; i++)
        {
            if (!BlockedAnswers.Contains(entries[i]))
            {
                CheckBox check = new()
                {
                    ClassId = entries[i].ToString()
                };
                check.CheckedChanged += (s, e) =>
                {
                    HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                    if (check.IsChecked)
                    {
                        if (!SelectedAnswers.Contains(int.Parse(check.ClassId))) SelectedAnswers.Add(int.Parse(check.ClassId));
                    }
                    else
                    {
                        if (SelectedAnswers.Contains(int.Parse(check.ClassId))) SelectedAnswers.Remove(int.Parse(check.ClassId));
                    }
                };
                Label labelNum1 = new()
                {
                    ClassId = entries[i].ToString(),
                    Padding = new Thickness(0),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    FontSize = FSize,
                    FontFamily = "OpenSansRegular"
                };
                if (HardwoodOrSoftwood == 0) labelNum1.Text = Hardwoods[entries[i]];
                else labelNum1.Text = Softwoods[entries[i]];

                TapGestureRecognizer tGR = new TapGestureRecognizer();
                tGR.Tapped += (s, e) =>
                {
                    if (check.IsChecked)
                    {
                        check.IsChecked = false;
                        if (SelectedAnswers.Contains(int.Parse(check.ClassId))) SelectedAnswers.Remove(int.Parse(check.ClassId));
                    }
                    else
                    {
                        check.IsChecked = true;
                        if (!SelectedAnswers.Contains(int.Parse(check.ClassId))) SelectedAnswers.Add(int.Parse(check.ClassId));
                    }
                };
                labelNum1.GestureRecognizers.Add(tGR);

                RowDefinition rowDefinition = new()
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                };
                gridChoices.AddRowDefinition(rowDefinition);

                gridChoices.Add(check, 0, gridRow);
                gridChoices.Add(labelNum1, 1, gridRow);

                gridRow += 1;
            }
        }

        return grid;
    }

    private FormattedString FormatQuestion(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansRegular"
        };
        if (str != string.Empty || str != " " || !string.IsNullOrEmpty(str))
        {
            span.Text += str[0];
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '[')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansSemiboldItalic"
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
            if (span.Text != null || !string.IsNullOrEmpty(span.Text)) fStr.Spans.Add(span);
        }

        return fStr;
    }

    private FormattedString FormatSpecies(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0) + 1.0;
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize
        };
        if (str != string.Empty || str != " " || !string.IsNullOrEmpty(str))
        {
            if (str[0] == '[')
            {
                span.FontFamily = "OpenSansItalic";
            }
            else
            {
                span.Text += str[0];
                span.FontFamily = "OpenSansRegular";
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

                if (str[i] == ' ' && str[i + 1] == '(')
                {
                    if (str[i - 1] != ']') fStr.Spans.Add(span);

                    return fStr;
                }
            }
            fStr.Spans.Add(span);
        }

        return fStr;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopToRootAsync(false);
    }

    public Command BackCommand => new Command(async () =>
    {
        await Shell.Current.Navigation.PopToRootAsync(false);
    });

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}