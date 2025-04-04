using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using GhanaWoods.Database;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace GhanaWoods;

public partial class LearnMainPage : ContentPage
{
	public double FSize = Preferences.Get("FontSize", 16.0);
	bool isCreated = false;
    Dictionary<int, string> Softwoods = new Dictionary<int, string>();
    List<int> SWfeaturesCombined = new List<int> { 40, 41, 42, 43, 72, 73, 74, 109, 111 };

    Dictionary<int, string> Hardwoods = new Dictionary<int, string>();
    //List<int> HWfeaturesCombined = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 56, 
    //                                               58, 96, 97, 98, 99, 114, 115, 116, 76,
    //                                               77, 79, 80, 81, 82, 83, 85, 86,
    //                                               87, 89};
    List<int> HWfeaturesCombined = new List<int> { 1, 2, 4, 5, 7, 9, 10, 11, 56,
                                                   58, 96, 97, 98, 99, 115, 116, 76,
                                                   77, 79, 80, 81, 82, 83, 85, 86, 89};
    int CorrectSpecies = 0;
    int HardwoodOrSoftwood = 0;

    List<string> extraImages0 = new List<string>();
    int images0Index = 0;
    List<string> extraImages1 = new List<string>();
    int images1Index = 0;

    DevicePlatform dPlat = new();
    TapGestureRecognizer zTGR = new();

    int TotalCorrect = 0;
    int TotalSelections = 0;
    bool ResetTotal = true;

	public LearnMainPage()
	{
		InitializeComponent();

		this.BindingContext = this;

		FSize = Preferences.Get("FontSize", 16.0);
        chooseSpecies0.FontSize = FSize + 1.0;
        chooseSpecies1.FontSize = FSize + 1.0;

		this.Title = LearnTabMain.learnFC;

		isCreated = false;

        AddSoftwoods();

        AddHardwoods();

        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            iGRIDrd2.Height = 44;
            iGrHeading.RowDefinitions[0].Height = 44;
        }

        dPlat = new DevicePlatform();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        if (dPlat == DevicePlatform.Android) this.SizeChanged += OnSizeChanged;
        else
        {
            //iGRIDrd0.Height = new GridLength(1, GridUnitType.Auto);
            //iGRIDrd1.Height = new GridLength(1, GridUnitType.Auto);
            //ImgRD.Height = new GridLength(1, GridUnitType.Star);
        }

        zTGR = new TapGestureRecognizer();
        zTGR.Tapped += async (s, e) =>
        {
            ResetTotal = false;
            Image? s1 = s as Image;
            if (s1 != null) await AppShell.Current.Navigation.PushAsync(new ImageZoomPage(s1.Source), false);
        };
        Image0.GestureRecognizers.Add(zTGR);
        Image1.GestureRecognizers.Add(zTGR);
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
            iGRIDrd0.Height = new GridLength(0, GridUnitType.Absolute);
            iGRIDrd1.Height = new GridLength(0, GridUnitType.Absolute);
            ImageSource iSource = Image0.Source;
            ImageSource iSource1 = Image1.Source;
            Image0.Source = null;
            Image1.Source = null;
            Image0.Source = iSource;
            Image1.Source = iSource1;
        }
        else
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, true);
            iGRIDrd0.Height = new GridLength(1, GridUnitType.Star);
            iGRIDrd1.Height = new GridLength(1, GridUnitType.Star);
            ImageSource iSource = Image0.Source;
            ImageSource iSource1 = Image1.Source;
            Image0.Source = null;
            Image1.Source = null;
            Image0.Source = iSource;
            Image1.Source = iSource1;
        }
        ImgRD.Height = 0;
        iGRIDrd0.Height = 0;
        iGRIDrd1.Height = 0;
        Task.Delay(10);
        iGRIDrd0.Height = new GridLength(1, GridUnitType.Auto);
        iGRIDrd1.Height = new GridLength(1, GridUnitType.Auto);
        ImgRD.Height = new GridLength(1, GridUnitType.Auto);

        Image0.IsVisible = false;
        Image1.IsVisible = false;
        Task.Delay(10);
        Image0.IsVisible = true;
        Image1.IsVisible = true;
    }

    private void AddSoftwoods()
    {
        Softwoods.Add(40, LearnTabMain.SW40);
        Softwoods.Add(41, LearnTabMain.SW41);
        Softwoods.Add(42, LearnTabMain.SW42);
        Softwoods.Add(43, LearnTabMain.SW43);
        Softwoods.Add(72, LearnTabMain.SW72);
        Softwoods.Add(73, LearnTabMain.SW73);
        Softwoods.Add(74, LearnTabMain.SW74);
        //Softwoods.Add(75, "marginal parenchyma");
        //Softwoods.Add(107, "exclusively uniseriate rays");
        //Softwoods.Add(108, "rays 2-3 seriate in part");
        Softwoods.Add(109, LearnTabMain.SW109);
        Softwoods.Add(111, LearnTabMain.SW111);
    }
    private void AddHardwoods()
    {
        Hardwoods.Add(1, LearnTabMain.HW1);
        Hardwoods.Add(2, LearnTabMain.HW2);
        //Hardwoods.Add(3, LearnTabMain.HW3);
        Hardwoods.Add(4, LearnTabMain.HW4);
        Hardwoods.Add(5, LearnTabMain.HW5);
        //Hardwoods.Add(6, LearnTabMain.HW6);
        Hardwoods.Add(7, LearnTabMain.HW7);
        //Hardwoods.Add(8, LearnTabMain.HW8);
        Hardwoods.Add(9, LearnTabMain.HW9);
        Hardwoods.Add(10, LearnTabMain.HW10);
        Hardwoods.Add(11, LearnTabMain.HW11);
        Hardwoods.Add(56, LearnTabMain.HW56);
        Hardwoods.Add(58, LearnTabMain.HW58);
        Hardwoods.Add(96, LearnTabMain.HW96);
        Hardwoods.Add(97, LearnTabMain.HW97);
        Hardwoods.Add(98, LearnTabMain.HW98);
        Hardwoods.Add(99, LearnTabMain.HW99);
        //Hardwoods.Add(114, LearnTabMain.HW114);
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
        //Hardwoods.Add(87, LearnTabMain.HW87);
        Hardwoods.Add(89, LearnTabMain.HW89);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!isCreated)
        {
            TotalCorrect = 0;
            TotalSelections = 0;
            ResetTotal = true;

            extraImages0 = new();
            extraImages1 = new();
            images0Index = 0;
            images1Index = 0;
            int FeatureNum = 0;
            Random rand = new Random();
            int HWorSW = rand.Next(0, 7);
            QuestionLabel.FormattedText?.Spans.Clear();
            FormattedString fStr = new();
            //List<SpeciesTbl> questionSpecies = new List<SpeciesTbl>();
            List<SpeciesGroup> questionGroups = new List<SpeciesGroup>();
            //if (HWorSW < 6)
            //{
                FeatureNum = SelectHWFeatureForQuestion();
                fStr = FormatQuestion(LearnTabMain.wSD + "[" + Hardwoods[FeatureNum] + "]" + LearnTabMain.qMark);
                questionGroups = App.DB.GetHWGroupsByFeature(FeatureNum);
                this.Title = LearnTabMain.Hardwoods;
                HardwoodOrSoftwood = 0;
            //}
            //else
            //{
            //    FeatureNum = SelectSWFeatureForQuestion();
            //    fStr = FormatQuestion(LearnTabMain.wSD + "[" + Softwoods[FeatureNum] + "]" + LearnTabMain.qMark);
            //    questionSpecies = App.DB.GetSWSpeciesByFeature(FeatureNum);
            //    this.Title = LearnTabMain.Softwoods;
            //    HardwoodOrSoftwood = 1;
            //}

            QuestionLabel.FormattedText = fStr;
          
            int correctNum = rand.Next(0, 2);
            CorrectSpecies = correctNum;

            List<ImageTbl> questionImages = new List<ImageTbl>();
            questionImages = App.DB.GetUniqueImageTblsByGroupID(questionGroups[0].SpeciesGroupID);
            List<ImageTbl> questionImages1 = new List<ImageTbl>();
            questionImages1 = App.DB.GetUniqueImageTblsByGroupID(questionGroups[1].SpeciesGroupID);


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();

            if (correctNum == 0)
            {
                fStr = new();
                fStr = FormatSpecies(questionImages[0].ImageName);
                ImageLabel0.FormattedText = fStr;
                fStr = new();
                fStr = FormatSpecies(questionImages1[0].ImageName);
                ImageLabel1.FormattedText = fStr;

                Image0.Source = null;
                Image1.Source = null;
                Image0.Source = ImageSource.FromFile(questionImages[0].FileName);
                Image1.Source = ImageSource.FromFile(questionImages1[0].FileName);

                if (questionImages.Count > 1)
                {
                    extraImages0 = new();
                    for (int i = 0; i < questionImages.Count; i++) extraImages0.Add(questionImages[i].FileName);
                    images0Index = 0;
                    arrowLeft0.BackgroundColor = Color.FromArgb("#FFB81C");
                    arrowRight0.BackgroundColor = Color.FromArgb("#FFB81C");
                }
                else
                {
                    extraImages0 = new();
                    images0Index = 0;
                    arrowLeft0.BackgroundColor = Colors.Gray;
                    arrowRight0.BackgroundColor = Colors.Gray;
                }
                if (questionImages1.Count > 1)
                {
                    extraImages1 = new();
                    for (int i = 0; i < questionImages1.Count; i++) extraImages1.Add(questionImages1[i].FileName);
                    images1Index = 0;
                    arrowLeft1.BackgroundColor = Color.FromArgb("#FFB81C");
                    arrowRight1.BackgroundColor = Color.FromArgb("#FFB81C");
                }
                else
                {
                    extraImages1 = new();
                    images1Index = 0;
                    arrowLeft1.BackgroundColor = Colors.Gray;
                    arrowRight1.BackgroundColor = Colors.Gray;
                }
            }
            else
            {
                fStr = new();
                fStr = FormatSpecies(questionImages1[0].ImageName);
                ImageLabel0.FormattedText = fStr;
                fStr = new();
                fStr = FormatSpecies(questionImages[0].ImageName);
                ImageLabel1.FormattedText = fStr;

                Image0.Source = null;
                Image1.Source = null;
                Image0.Source = ImageSource.FromFile(questionImages1[0].FileName);
                Image1.Source = ImageSource.FromFile(questionImages[0].FileName);

                if (questionImages1.Count > 1)
                {
                    extraImages0 = new();
                    for (int i = 0; i < questionImages1.Count; i++) extraImages0.Add(questionImages1[i].FileName);
                    images0Index = 0;
                    arrowLeft0.BackgroundColor = Color.FromArgb("#FFB81C");
                    arrowRight0.BackgroundColor = Color.FromArgb("#FFB81C");
                }
                else
                {
                    extraImages0 = new();
                    images0Index = 0;
                    arrowLeft0.BackgroundColor = Colors.Gray;
                    arrowRight0.BackgroundColor = Colors.Gray;
                }
                if (questionImages.Count > 1)
                {
                    extraImages1 = new();
                    for (int i = 0; i < questionImages.Count; i++) extraImages1.Add(questionImages[i].FileName);
                    images1Index = 0;
                    arrowLeft1.BackgroundColor = Color.FromArgb("#FFB81C");
                    arrowRight1.BackgroundColor = Color.FromArgb("#FFB81C");
                }
                else
                {
                    extraImages1 = new();
                    images1Index = 0;
                    arrowLeft1.BackgroundColor = Colors.Gray;
                    arrowRight1.BackgroundColor = Colors.Gray;
                }
            }

            isCreated = true;
        }
        else
        {
            if (HardwoodOrSoftwood == 0) this.Title = LearnTabMain.Hardwoods;
            else this.Title = LearnTabMain.Softwoods;

            if (ResetTotal)
            {
                TotalCorrect = 0;
                TotalSelections = 0;
            }
            ResetTotal = true;
        }
    }

    public int SelectHWFeatureForQuestion()
    {
        int featureNum = 0;

        Random rand = new Random();

        //int listNum = rand.Next(0, 33);
        //int listNum = rand.Next(0, 31);
        int listNum = rand.Next(0, 26);
        featureNum = HWfeaturesCombined[listNum];

        return featureNum;
    }

    public int SelectSWFeatureForQuestion()
    {
        int featureNum = 0;

        Random rand = new Random();
        int listNum = rand.Next(0, 9);
        featureNum = SWfeaturesCombined[listNum];

        return featureNum;
    }

    async void OnButtonClicked(object? sender, EventArgs args)
    {
        if (CorrectSpecies == 0)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            TotalCorrect += 1;
            TotalSelections += 1;
            string total = TotalCorrect.ToString() + " / " + TotalSelections.ToString();
            await DisplayAlert(LearnTabMain.Correct_, total, LearnTabMain.Continue);
        }
        else
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            TotalSelections += 1;
            string total = TotalCorrect.ToString() + " / " + TotalSelections.ToString();
            await DisplayAlert(LearnTabMain.Incorrect_, total, LearnTabMain.Continue);

        }

        extraImages0 = new();
        extraImages1 = new();
        images0Index = 0;
        images1Index = 0;
        int FeatureNum = 0;
        Random rand = new Random();
        int HWorSW = rand.Next(0, 7);
        QuestionLabel.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        //List<SpeciesTbl> questionSpecies = new List<SpeciesTbl>();
        List<SpeciesGroup> questionGroups = new List<SpeciesGroup>();
        //if (HWorSW < 6)
        //{
            FeatureNum = SelectHWFeatureForQuestion();
            fStr = FormatQuestion(LearnTabMain.wSD + "[" + Hardwoods[FeatureNum] + "]" + LearnTabMain.qMark);
            questionGroups = App.DB.GetHWGroupsByFeature(FeatureNum);
            this.Title = LearnTabMain.Hardwoods;
            HardwoodOrSoftwood = 0;
        //}
        //else
        //{
        //    FeatureNum = SelectSWFeatureForQuestion();
        //    fStr = FormatQuestion(LearnTabMain.wSD + "[" + Softwoods[FeatureNum] + "]" + LearnTabMain.qMark);
        //    questionSpecies = App.DB.GetSWSpeciesByFeature(FeatureNum);
        //    this.Title = LearnTabMain.Softwoods;
        //    HardwoodOrSoftwood = 1;
        //}

        QuestionLabel.FormattedText = fStr;

        int correctNum = rand.Next(0, 2);
        CorrectSpecies = correctNum;

        List<ImageTbl> questionImages = new List<ImageTbl>();
        questionImages = App.DB.GetUniqueImageTblsByGroupID(questionGroups[0].SpeciesGroupID);
        List<ImageTbl> questionImages1 = new List<ImageTbl>();
        questionImages1 = App.DB.GetUniqueImageTblsByGroupID(questionGroups[1].SpeciesGroupID);


        //iGRIDrd0.Height = new GridLength(0, GridUnitType.Absolute);
        //iGRIDrd1.Height = new GridLength(0, GridUnitType.Absolute);
        ImageLabel0.FormattedText?.Spans.Clear();
        ImageLabel1.FormattedText?.Spans.Clear();

        if (correctNum == 0)
        {
            fStr = new();
            fStr = FormatSpecies(questionImages[0].ImageName);
            ImageLabel0.FormattedText = fStr;
            fStr = new();
            fStr = FormatSpecies(questionImages1[0].ImageName);
            ImageLabel1.FormattedText = fStr;

            ImgRD.Height = new GridLength(0, GridUnitType.Absolute);
            Image0.Source = null;
            Image1.Source = null;
            Image0.Source = ImageSource.FromFile(questionImages[0].FileName);
            Image1.Source = ImageSource.FromFile(questionImages1[0].FileName);
            //iGRIDrd0.Height = new GridLength(1, GridUnitType.Star);
            //iGRIDrd1.Height = new GridLength(1, GridUnitType.Star);
            ImgRD.Height = new GridLength(1, GridUnitType.Auto);

            if (questionImages.Count > 1)
            {
                extraImages0 = new();
                for (int i = 0; i < questionImages.Count; i++) extraImages0.Add(questionImages[i].FileName);
                images0Index = 0;
                arrowLeft0.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight0.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages0 = new();
                images0Index = 0;
                arrowLeft0.BackgroundColor = Colors.Gray;
                arrowRight0.BackgroundColor = Colors.Gray;
            }
            if (questionImages1.Count > 1)
            {
                extraImages1 = new();
                for (int i = 0; i < questionImages1.Count; i++) extraImages1.Add(questionImages1[i].FileName);
                images1Index = 0;
                arrowLeft1.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight1.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages1 = new();
                images1Index = 0;
                arrowLeft1.BackgroundColor = Colors.Gray;
                arrowRight1.BackgroundColor = Colors.Gray;
            }
        }
        else
        {
            fStr = new();
            fStr = FormatSpecies(questionImages1[0].ImageName);
            ImageLabel0.FormattedText = fStr;
            fStr = new();
            fStr = FormatSpecies(questionImages[0].ImageName);
            ImageLabel1.FormattedText = fStr;

            ImgRD.Height = new GridLength(0, GridUnitType.Absolute);
            Image0.Source = null;
            Image1.Source = null;
            Image0.Source = ImageSource.FromFile(questionImages1[0].FileName);
            Image1.Source = ImageSource.FromFile(questionImages[0].FileName);
            //iGRIDrd0.Height = new GridLength(1, GridUnitType.Star);
            //iGRIDrd1.Height = new GridLength(1, GridUnitType.Star);
            ImgRD.Height = new GridLength(1, GridUnitType.Auto);

            if (questionImages1.Count > 1)
            {
                extraImages0 = new();
                for (int i = 0; i < questionImages1.Count; i++) extraImages0.Add(questionImages1[i].FileName);
                images0Index = 0;
                arrowLeft0.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight0.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages0 = new();
                images0Index = 0;
                arrowLeft0.BackgroundColor = Colors.Gray;
                arrowRight0.BackgroundColor = Colors.Gray;
            }
            if (questionImages.Count > 1)
            {
                extraImages1 = new();
                for (int i = 0; i < questionImages.Count; i++) extraImages1.Add(questionImages[i].FileName);
                images1Index = 0;
                arrowLeft1.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight1.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages1 = new();
                images1Index = 0;
                arrowLeft1.BackgroundColor = Colors.Gray;
                arrowRight1.BackgroundColor = Colors.Gray;
            }
        }
    }

    async void OnButtonClicked1(object? sender, EventArgs args)
    {
        if (CorrectSpecies == 1)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            TotalCorrect += 1;
            TotalSelections += 1;
            string total = TotalCorrect.ToString() + " / " + TotalSelections.ToString();
            await DisplayAlert(LearnTabMain.Correct_, total, LearnTabMain.Continue);
        }
        else
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            TotalSelections += 1;
            string total = TotalCorrect.ToString() + " / " + TotalSelections.ToString();
            await DisplayAlert(LearnTabMain.Incorrect_, total, LearnTabMain.Continue);
        }

        extraImages0 = new();
        extraImages1 = new();
        images0Index = 0;
        images1Index = 1;
        int FeatureNum = 0;
        Random rand = new Random();
        int HWorSW = rand.Next(0, 7);
        QuestionLabel.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        //List<SpeciesTbl> questionSpecies = new List<SpeciesTbl>();
        List<SpeciesGroup> questionGroups = new List<SpeciesGroup>();
        //if (HWorSW < 6)
        //{
            FeatureNum = SelectHWFeatureForQuestion();
            fStr = FormatQuestion(LearnTabMain.wSD + "[" + Hardwoods[FeatureNum] + "]" + LearnTabMain.qMark);
        //questionSpecies = App.DB.GetHWSpeciesByFeature(FeatureNum);
        questionGroups = App.DB.GetHWGroupsByFeature(FeatureNum);
        this.Title = LearnTabMain.Hardwoods;
            HardwoodOrSoftwood = 0;
        //}
        //else
        //{
        //    FeatureNum = SelectSWFeatureForQuestion();
        //    fStr = FormatQuestion(LearnTabMain.wSD + "[" + Softwoods[FeatureNum] + "]" + LearnTabMain.qMark);
        //    questionSpecies = App.DB.GetSWSpeciesByFeature(FeatureNum);
        //    this.Title = LearnTabMain.Softwoods;
        //    HardwoodOrSoftwood = 1;
        //}

        QuestionLabel.FormattedText = fStr;

        int correctNum = rand.Next(0, 2);
        CorrectSpecies = correctNum;

        List<ImageTbl> questionImages = new List<ImageTbl>();
        //questionImages = App.DB.GetUniqueImageTblsBySpeciesID(questionSpecies[0].SpeciesTblID);
        questionImages = App.DB.GetUniqueImageTblsByGroupID(questionGroups[0].SpeciesGroupID);
        List<ImageTbl> questionImages1 = new List<ImageTbl>();
        //questionImages1 = App.DB.GetUniqueImageTblsBySpeciesID(questionSpecies[1].SpeciesTblID);
        questionImages1 = App.DB.GetUniqueImageTblsByGroupID(questionGroups[1].SpeciesGroupID);


        //iGRIDrd0.Height = new GridLength(0, GridUnitType.Absolute);
        //iGRIDrd1.Height = new GridLength(0, GridUnitType.Absolute);
        ImageLabel0.FormattedText?.Spans.Clear();
        ImageLabel1.FormattedText?.Spans.Clear();

        if (correctNum == 0)
        {
            fStr = new();
            //fStr = FormatSpecies(questionSpecies[0].Name);
            fStr = FormatSpecies(questionImages[0].ImageName);
            ImageLabel0.FormattedText = fStr;
            fStr = new();
            //fStr = FormatSpecies(questionSpecies[1].Name);
            fStr = FormatSpecies(questionImages1[0].ImageName);
            ImageLabel1.FormattedText = fStr;

            ImgRD.Height = new GridLength(0, GridUnitType.Absolute);
            Image0.Source = null;
            Image1.Source = null;
            Image0.Source = ImageSource.FromFile(questionImages[0].FileName);
            Image1.Source = ImageSource.FromFile(questionImages1[0].FileName);
            //iGRIDrd0.Height = new GridLength(1, GridUnitType.Star);
            //iGRIDrd1.Height = new GridLength(1, GridUnitType.Star);
            ImgRD.Height = new GridLength(1, GridUnitType.Auto);

            if (questionImages.Count > 1)
            {
                extraImages0 = new();
                for (int i = 0; i < questionImages.Count; i++) extraImages0.Add(questionImages[i].FileName);
                images0Index = 0;
                arrowLeft0.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight0.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages0 = new();
                images0Index = 0;
                arrowLeft0.BackgroundColor = Colors.Gray;
                arrowRight0.BackgroundColor = Colors.Gray;
            }
            if (questionImages1.Count > 1)
            {
                extraImages1 = new();
                for (int i = 0; i < questionImages1.Count; i++) extraImages1.Add(questionImages1[i].FileName);
                images1Index = 0;
                arrowLeft1.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight1.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages1 = new();
                images1Index = 0;
                arrowLeft1.BackgroundColor = Colors.Gray;
                arrowRight1.BackgroundColor = Colors.Gray;
            }
        }
        else
        {
            fStr = new();
            //fStr = FormatSpecies(questionSpecies[1].Name);
            fStr = FormatSpecies(questionImages1[0].ImageName);
            ImageLabel0.FormattedText = fStr;
            fStr = new();
            //fStr = FormatSpecies(questionSpecies[0].Name);
            fStr = FormatSpecies(questionImages[0].ImageName);
            ImageLabel1.FormattedText = fStr;

            ImgRD.Height = new GridLength(0, GridUnitType.Absolute);
            Image0.Source = null;
            Image1.Source = null;
            Image0.Source = ImageSource.FromFile(questionImages1[0].FileName);
            Image1.Source = ImageSource.FromFile(questionImages[0].FileName);
            //iGRIDrd0.Height = new GridLength(1, GridUnitType.Star);
            //iGRIDrd1.Height = new GridLength(1, GridUnitType.Star);
            ImgRD.Height = new GridLength(1, GridUnitType.Auto);

            if (questionImages1.Count > 1)
            {
                extraImages0 = new();
                for (int i = 0; i < questionImages1.Count; i++) extraImages0.Add(questionImages1[i].FileName);
                images0Index = 0;
                arrowLeft0.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight0.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages0 = new();
                images0Index = 0;
                arrowLeft0.BackgroundColor = Colors.Gray;
                arrowRight0.BackgroundColor = Colors.Gray;
            }
            if (questionImages.Count > 1)
            {
                extraImages1 = new();
                for (int i = 0; i < questionImages.Count; i++) extraImages1.Add(questionImages[i].FileName);
                images1Index = 0;
                arrowLeft1.BackgroundColor = Color.FromArgb("#FFB81C");
                arrowRight1.BackgroundColor = Color.FromArgb("#FFB81C");
            }
            else
            {
                extraImages1 = new();
                images1Index = 0;
                arrowLeft1.BackgroundColor = Colors.Gray;
                arrowRight1.BackgroundColor = Colors.Gray;
            }
        }
    }

    void OnArrowLeft0Clicked(object? sender, EventArgs args)
    {
        if (extraImages0.Count > 0)
        {
            if (extraImages0.Count > 1)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages0);
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
                    if (imageIndex == images0Index && i > 0)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;

                        ImageLabel0.FormattedText = formattedImageNames[imageIndex - 1];
                        Image0.Source = ImageSource.FromFile(extraImages0[imageIndex - 1]);

                        Console.WriteLine("SETTING IMAGES:   1");
                        images0Index = imageIndex - 1;

                        break;
                    }
                    else if (i == 0)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;

                        imageIndex = numGroups - 1;

                        ImageLabel0.FormattedText = formattedImageNames[imageIndex];
                        Image0.Source = ImageSource.FromFile(extraImages0[imageIndex]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        images0Index = imageIndex;

                        break;
                    }

                    imageIndex -= 1;
                }
            }
        }
    }
    void OnArrowRight0Clicked(object? sender, EventArgs args)
    {
        if (extraImages0.Count > 0)
        {
            if (extraImages0.Count > 1)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages0);
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
                    if (imageIndex == images0Index && i < numGroups - 1)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;

                        ImageLabel0.FormattedText = formattedImageNames[imageIndex + 1];
                        Image0.Source = ImageSource.FromFile(extraImages0[imageIndex + 1]);

                        Console.WriteLine("SETTING IMAGES:   1");
                        images0Index = imageIndex + 1;

                        break;
                    }
                    else if (i == numGroups - 1)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        Image0.Source = null;

                        ImageLabel0.FormattedText = formattedImageNames[0];
                        Image0.Source = ImageSource.FromFile(extraImages0[0]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        images0Index = 0;

                        break;
                    }

                    imageIndex += 1;
                }
            }
        }
    }

    void OnArrowLeft1Clicked(object? sender, EventArgs args)
    {
        if (extraImages1.Count > 0)
        {
            if (extraImages1.Count > 1)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages1);
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
                    if (imageIndex == images1Index && i > 0)
                    {
                        ImageLabel1.FormattedText?.Spans.Clear();
                        Image1.Source = null;

                        ImageLabel1.FormattedText = formattedImageNames[imageIndex - 1];
                        Image1.Source = ImageSource.FromFile(extraImages1[imageIndex - 1]);

                        Console.WriteLine("SETTING IMAGES:   1");
                        images1Index = imageIndex - 1;

                        break;
                    }
                    else if (i == 0)
                    {
                        ImageLabel1.FormattedText?.Spans.Clear();
                        Image1.Source = null;

                        imageIndex = numGroups - 1;

                        ImageLabel1.FormattedText = formattedImageNames[imageIndex];
                        Image1.Source = ImageSource.FromFile(extraImages1[imageIndex]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        images1Index = imageIndex;

                        break;
                    }

                    imageIndex -= 1;
                }
            }
        }
    }
    void OnArrowRight1Clicked(object? sender, EventArgs args)
    {
        if (extraImages1.Count > 0)
        {
            if (extraImages1.Count > 1)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages1);
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
                    if (imageIndex == images1Index && i < numGroups - 1)
                    {
                        ImageLabel1.FormattedText?.Spans.Clear();
                        Image1.Source = null;

                        ImageLabel1.FormattedText = formattedImageNames[imageIndex + 1];
                        Image1.Source = ImageSource.FromFile(extraImages1[imageIndex + 1]);

                        Console.WriteLine("SETTING IMAGES:   1");
                        images1Index = imageIndex + 1;

                        break;
                    }
                    else if (i == numGroups - 1)
                    {
                        ImageLabel1.FormattedText?.Spans.Clear();
                        Image1.Source = null;

                        ImageLabel1.FormattedText = formattedImageNames[0];
                        Image1.Source = ImageSource.FromFile(extraImages1[0]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        images1Index = 0;

                        break;
                    }

                    imageIndex += 1;
                }
            }
        }
    }

    private FormattedString FormatQuestion(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0) + 5.0;
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
                        FontFamily = "OpenSansSemibold"
                    };
                }
                else
                {
                    span.Text += str[i];
                }
            }
            fStr.Spans.Add(span);
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
        ResetTotal = false;
        Shell.Current.Navigation.PushAsync(new LearnFeaturesPage(), false);
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}