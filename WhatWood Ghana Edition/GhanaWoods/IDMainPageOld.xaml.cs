using GhanaWoods.Resources.Languages;
using GhanaWoods.Database;
using static GhanaWoods.TabAssets;
using System.Globalization;
using System.Reflection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace GhanaWoods;

public partial class IDMainPageOld : ContentPage
{
	bool isCreated = false;
    public double FSize = Preferences.Get("FontSize", 16.0);
    DevicePlatform dPlat = new();

    public List<int> pastDecisions = new List<int>();
    public List<int> futureDecisions = new List<int>();
    public int currentDecision = 0;
    public int currentDecisionLast = 0;
    public string currentImages = "1a";
    public double currentScroll = 0.0;
    public List<string> extraImages = new List<string>();
    public int imagesIndex = 0;

    public bool imgZoom = false;

    //double currentScale = 1;
    //double startScale = 1;
    //double xOffset = 0;
    //double yOffset = 0;

    TapGestureRecognizer zTGR = new();

    public IDMainPageOld()
	{
		InitializeComponent();

		this.Title = IDTabMain.Identification;

        dPlat = new DevicePlatform();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        zTGR = new TapGestureRecognizer();
        zTGR.Tapped += async (s, e) =>
        {
            Image? s1 = s as Image;
            if (s1 != null)
            if (s1.Source != null)
            {
                isCreated = true;
                currentScroll = SV.ScrollY;
                //await Shell.Current.Navigation.PushModalAsync(new NavigationPage(new ImageZoomModalPage(((Image)s).Source)), false);
                await Shell.Current.Navigation.PushModalAsync(new ImageZoomModalPage(s1.Source), false);
                //await Navigation.PushAsync(new ImageZoomPage(((Image)s).Source), false);
            }
        };
        Image0.GestureRecognizers.Add(zTGR);
        Image1.GestureRecognizers.Add(zTGR);
        Image2.GestureRecognizers.Add(zTGR);
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        this.BindingContext = this;

        bool fsChanged = Preferences.Get("FontSizeChangedID", false);
        bool langChanged = Preferences.Get("LanguageChanged", false);
        if (!isCreated || fsChanged || langChanged)
        {
            FSize = Preferences.Get("FontSize", 16.0);

            if (dPlat == DevicePlatform.iOS) Image1.Aspect = Aspect.AspectFit;
            else Image1.Aspect = Aspect.AspectFill;

            isCreated = true;

            aVSL.Children.Clear();

            for (int i = 0; i < keyEntries.Count; i++)
            {
                try
                {
                    int trueIndex = i;

                    if (fsChanged)
                    {
                        localLabels[i].FontSize = FSize;
                        localLabels1[i].FontSize = FSize;
                        localLabelsNum[i].FontSize = FSize;
                        localLabelsNum1[i].FontSize = FSize;
                    }

                    TapGestureRecognizer tGR = new TapGestureRecognizer
                    {
                        CommandParameter = trueIndex.ToString() + "a",
                        Command = TapCommand
                    };
                    TapGestureRecognizer tGR1 = new TapGestureRecognizer
                    {
                        CommandParameter = trueIndex.ToString() + "b",
                        Command = TapCommand
                    };
                    localLabels[trueIndex].GestureRecognizers.Clear();
                    localLabels1[trueIndex].GestureRecognizers.Clear();
                    localLabels[trueIndex].GestureRecognizers.Add(tGR);
                    localLabels1[trueIndex].GestureRecognizers.Add(tGR1);

                    ClearEventInvocations(localButtons[trueIndex], "Clicked");
                    ClearEventInvocations(localButtons1[trueIndex], "Clicked");
                    localButtons[trueIndex].Clicked += OnButtonClicked;
                    localButtons1[trueIndex].Clicked += OnButtonClicked1;

                    localBoxes[i].Color = Color.FromArgb("#BDE6E2");
                    localGrids[i].BackgroundColor = Color.FromArgb("#BDD6E6");

                    aVSL.Children.Add(localGrids[trueIndex]);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("you need help:   " + ex.Message);
                    Console.WriteLine("you need exceptional help:   " + ex.InnerException);
                }
            }


            headLabel.FontSize = FSize + 3.0;
            headLabelZ.FontSize = FSize;
            headLabel.Text = "1a";
            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[0].Text0;
            else headLabelZ.Text = keyEntries[0].Text0ES;

            headLabel.GestureRecognizers.Clear();
            headLabelZ.GestureRecognizers.Clear();
            TapGestureRecognizer tGR2 = new();
            tGR2.Tapped += OnTGRTapped;
            headLabel.GestureRecognizers.Add(tGR2);
            headLabelZ.GestureRecognizers.Add(tGR2);


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();

            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(1);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for(int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
            ImageLabel0.FormattedText = formattedImageNames[0];
            ImageLabel1.FormattedText = formattedImageNames[1];
            ImageLabel2.FormattedText = formattedImageNames[2];
            Image0.Source = ImageSource.FromFile(fileNames[0]);
            Image1.Source = ImageSource.FromFile(fileNames[1]);
            Image2.Source = ImageSource.FromFile(fileNames[2]);
            
            if (fileNames.Count > 3)
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            else
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            imagesIndex = 0;

            KeyRD.Height = new GridLength(1, GridUnitType.Star);
            ImgRD.Height = 0;
            ZoomRD.Height = 0;
            GRZ.IsVisible = false;

            pastDecisions = new List<int>();
            futureDecisions = new List<int>();
            currentDecision = 0;
            currentDecisionLast = 0;
            currentImages = "1a";
            lNav.IsVisible = false;
            lNavZ.IsVisible = false;
            rNav.IsVisible = false;
            rNavZ.IsVisible = false;
            imgNav.IsVisible = true;
            imgZoom = false;
            await SV.ScrollToAsync(0, 0, false);

            Preferences.Set("FontSizeChangedID", false);
            Preferences.Set("LanguageChanged", false);
            isCreated = true;
        }
        else if (isCreated && currentDecision > 0)
        {
            await SV.ScrollToAsync(localGrids[currentDecision - 1], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
        }
        else if (isCreated && currentDecision == 0)
        {
            if (currentScroll <= 0.0)
            {
                await SV.ScrollToAsync(0, 0, false);
            }
            else
            {
                await SV.ScrollToAsync(0, currentScroll, false);
                currentScroll = 0.0;
            }
        }

        if (ImgRD.Height == 0) imgNav.IsVisible = true;
        else if (ImgRD.Height == GridLength.Auto) imgNav.IsVisible = false;

        return;
    }

    public static void ClearEventInvocations(object obj, string eventName)
    {
        //var fi = obj.GetType().GetEventField(eventName);
        FieldInfo? fi = GetEventField(obj.GetType(), eventName);
        if (fi == null) return;
        fi.SetValue(obj, null);
    }

    private static FieldInfo? GetEventField(Type? type, string eventName)
    {
        FieldInfo? field = null;
        while (type != null)
        {
            /* Find events defined as field */
            field = type.GetField(eventName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null && (field.FieldType == typeof(MulticastDelegate) || field.FieldType.IsSubclassOf(typeof(MulticastDelegate))))
                break;

            /* Find events defined as property { add; remove; } */
            field = type.GetField("EVENT_" + eventName.ToUpper(), BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                break;
            type = type.BaseType;
        }
        return field;
        //if (field != null) return field;
        //else
        //{
        //    FieldInfo fieldInfo = new();
        //    return fieldInfo;
        //}
    }

    public Command TapCommand => new Command(async labelParameter =>
    {
        string index = (string)labelParameter;
        KeyEntry entry = App.DB.GetKeyEntryByID(int.Parse(index.Substring(0, index.Length - 1)) + 1);
        string keyEntID = entry.DecisionNum.ToString();
        int entID = entry.DecisionNum;
        int entryTarget = 0;
        int keyTarget = 0;
        if (index.EndsWith('a'))
        {
            if (entry.TargetID0 != null)
            {
                entryTarget = (int)entry.TargetID0;
                keyTarget = (int)entry.TargetID0 - 1;
            }
        }
        else if (index.EndsWith('b'))
        {
            if (entry.TargetID1 != null)
            {
                entryTarget = (int)entry.TargetID1;
                keyTarget = (int)entry.TargetID1 - 1;
            }
        }

        if (entryTarget > 0)
        {
            headLabel.Text = entryTarget.ToString() + "a";

            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[keyTarget].Text0;
            else headLabelZ.Text = keyEntries[keyTarget].Text0ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }
        }


        if (currentDecision == 0 || currentDecision == entID)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            if (entryTarget <= 0)
            {
                SpeciesGroup speciesGroup = new SpeciesGroup();
                if (index.EndsWith('a'))
                {
                    if (entry.TargetGroupsIDs0 != null)
                    {
                        if (entry.TargetGroupsIDs0.Contains("+  "))
                        {
                            List<string> groupsIDs = new List<string>();
                            groupsIDs = entry.TargetGroupsIDs0.Split("+  ").ToList();
                            speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        }
                        else
                        {
                            int groupID = int.Parse(entry.TargetGroupsIDs0);
                            speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        }
                    }
                    currentImages = entID.ToString() + "a";
                }
                else if (index.EndsWith('b'))
                {
                    if (entry.TargetGroupsIDs1 != null)
                    {
                        if (entry.TargetGroupsIDs1.Contains("+  "))
                        {
                            List<string> groupsIDs = new List<string>();
                            groupsIDs = entry.TargetGroupsIDs1.Split("+  ").ToList();
                            speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        }
                        else
                        {
                            int groupID = int.Parse(entry.TargetGroupsIDs1);
                            speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        }
                    }
                    currentImages = entID.ToString() + "b";
                }
                currentScroll = SV.ScrollY;
                await Navigation.PushAsync(new ReferenceGroupPageNew(speciesGroup, 1));
            }
            else
            {
                KeyHistory keyHistory = App.DB.GetKeyHistoryByID(entryTarget);
                List<string> prev = new();
                List<string> grey = new();
                if (keyHistory.PrevDecisions != null) prev = keyHistory.PrevDecisions.Split(", ").ToList();
                if (keyHistory.GreyDecisions != null) grey = keyHistory.GreyDecisions.Split(", ").ToList();

                for (int i = 0; i < prev.Count; i++)
                {
                    string val = prev[i];
                    if (val.EndsWith('a'))
                    {
                        int ind = int.Parse(val.Substring(0, val.Length - 1)) - 1;
                        localBoxes[ind].Color = Colors.LightGrey;
                        localGrids[ind].BackgroundColor = Colors.Grey;
                    }
                    else if (val.EndsWith('b'))
                    {
                        int ind = int.Parse(val.Substring(0, val.Length - 1)) - 1;
                        localBoxes[ind].Color = Colors.Grey;
                        localGrids[ind].BackgroundColor = Colors.LightGrey;
                    }
                }

                if (grey[0] != " ")
                {
                    for (int i = 0; i < grey.Count; i++)
                    {
                        int ind = int.Parse(grey[i]) - 1;
                        localBoxes[ind].Color = Color.FromArgb("#84A19E");
                        localGrids[ind].BackgroundColor = Color.FromArgb("#8495A1");
                    }
                }

                for (int i = keyTarget + 1; i < 169; i++)
                {
                    localBoxes[i].Color = Color.FromArgb("#BDE6E2");
                    localGrids[i].BackgroundColor = Color.FromArgb("#BDD6E6");
                }

                if (ImgRD.Height != 0) localBoxes[keyTarget].Color = Colors.LightGrey;
                else localBoxes[keyTarget].Color = Color.FromArgb("#BDE6E2");
                localGrids[keyTarget].BackgroundColor = Color.FromArgb("#BDD6E6");


                lNav.IsVisible = true;
                lNavZ.IsVisible = true;
                currentDecision = keyTarget + 1;
                currentDecisionLast = keyTarget + 1;
                if (pastDecisions.Count > 0)
                {
                    if (pastDecisions[pastDecisions.Count - 1] != entID)
                    {
                        pastDecisions.Add(entID);
                    }
                }
                else
                {
                    pastDecisions.Add(entID);
                }
                if (futureDecisions.Count > 0)
                {
                    if (currentDecision != futureDecisions[futureDecisions.Count - 1])
                    {
                        futureDecisions.Clear();
                        rNav.IsVisible = false;
                        rNavZ.IsVisible = false;
                    }
                    else if (futureDecisions.Count > 1 && futureDecisions.Last() == currentDecision)
                    {
                        futureDecisions.RemoveAt(futureDecisions.Count - 1);
                    }
                    else if (futureDecisions.Count == 1 && futureDecisions[0] == currentDecision)
                    {
                        futureDecisions.Clear();
                        rNav.IsVisible = false;
                        rNavZ.IsVisible = false;
                    }
                }


                EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
                entryImagesTbl = App.DB.GetEntryImageByID(entryTarget);
                List<string> fileNames = new List<string>();
                if (entryImagesTbl.ExampleImages == true)
                {
                    if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
                    currentImages = entryTarget.ToString() + "a";
                }
                else
                {
                    if (entryImagesTbl.SpeciesGroup != null)
                    {
                        if (entryImagesTbl.SpeciesGroup.Contains("* "))
                        {
                            List<string> groupsIDs = new List<string>();
                            groupsIDs = entryImagesTbl.SpeciesGroup.Split("* ").ToList();
                            SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                            fileNames = speciesGroup.FileNames.Split("* ").ToList();
                            currentImages = entryTarget.ToString() + "a";
                        }
                        else
                        {
                            int groupID = int.Parse(entryImagesTbl.SpeciesGroup);
                            SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                            fileNames = speciesGroup.FileNames.Split("* ").ToList();
                            currentImages = entryTarget.ToString() + "a";
                        }
                    }
                }

                List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
                List<FormattedString> formattedImageNames = new List<FormattedString>();
                for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
                ImageLabel0.FormattedText?.Spans.Clear();
                ImageLabel0.FormattedText = formattedImageNames[0];
                ImageLabel1.FormattedText?.Spans.Clear();
                ImageLabel1.FormattedText = formattedImageNames[1];
                ImageLabel2.FormattedText?.Spans.Clear();
                ImageLabel2.FormattedText = formattedImageNames[2];
                Image0.Source = ImageSource.FromFile(fileNames[0]);
                Image1.Source = ImageSource.FromFile(fileNames[1]);
                Image2.Source = ImageSource.FromFile(fileNames[2]);

                if (fileNames.Count < 4)
                {
                    extraImages = new();
                    headMore.IsVisible = false;
                    headMoreZ.IsVisible = false;
                }
                else
                {
                    extraImages = fileNames;
                    headMore.IsVisible = true;
                    headMoreZ.IsVisible = true;
                }
                imagesIndex = 0;

                if (ImgRD.Height != 0) localBoxes[keyTarget].Color = Colors.LightGray;

                await SV.ScrollToAsync(localGrids[keyTarget], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
            }
        }
    });

    void OnButtonClicked(object? sender, EventArgs args)
    {
        string keyEntID = "1";
        if (sender != null)
        {
            keyEntID = ((ImageButton)sender).ClassId;
        }

        if (currentDecision == 0 || currentDecision == int.Parse(keyEntID))
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            headLabel.Text = keyEntID + "a";

            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text0;
            else headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text0ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }

            int entID = int.Parse(keyEntID);
            int currentImgs = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
            if (entID != currentImgs)
            {
                localBoxes[currentImgs - 1].Color = Color.FromArgb("#BDE6E2");
                localGrids[currentImgs - 1].BackgroundColor = Color.FromArgb("#BDD6E6");
            }

            if (ImgRD.Height != 0) localBoxes[entID - 1].Color = Colors.LightGray;
            else localBoxes[entID - 1].Color = Color.FromArgb("#BDE6E2");
            localGrids[entID - 1].BackgroundColor = Color.FromArgb("#BDD6E6");

            // DB query for images for first decision of entID
            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(entID);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.ExampleImages == true)
            {
                if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
            }
            else
            {
                if (entryImagesTbl.SpeciesGroup != null)
                {
                    if (entryImagesTbl.SpeciesGroup.Contains("* "))
                    {
                        List<string> groupsIDs = new List<string>();
                        groupsIDs = entryImagesTbl.SpeciesGroup.Split("* ").ToList();
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                    else
                    {
                        int groupID = int.Parse(entryImagesTbl.SpeciesGroup);
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                }
            }


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));

            ImageLabel0.FormattedText = formattedImageNames[0];
            Image0.Source = ImageSource.FromFile(fileNames[0]);

            if (fileNames.Count > 1)
            {
                ImageLabel1.FormattedText = formattedImageNames[1];
                Image1.Source = ImageSource.FromFile(fileNames[1]);
            }
            else
            {
                Image1.Source = null;
                Image2.Source = null;
            }
            if (fileNames.Count > 2)
            {
                ImageLabel2.FormattedText = formattedImageNames[2];
                Image2.Source = ImageSource.FromFile(fileNames[2]);
            }
            else
            {
                Image2.Source = null;
            }
            currentImages = keyEntID + "a";

            if (fileNames.Count < 4)
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            else
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            imagesIndex = 0;
        }
    }
    void OnButtonClicked1(object? sender, EventArgs args)
    {
        string keyEntID = "1";
        if (sender != null)
        {
            keyEntID = ((ImageButton)sender).ClassId;
        }

        if (currentDecision == 0 || currentDecision == int.Parse(keyEntID))
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            headLabel.Text = keyEntID + "b";
            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text1;
            else headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text1ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }

            int entID = int.Parse(keyEntID);
            int currentImgs = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
            if (entID != currentImgs)
            {
                localBoxes[currentImgs - 1].Color = Color.FromArgb("#BDE6E2");
                localGrids[currentImgs - 1].BackgroundColor = Color.FromArgb("#BDD6E6");
            }

            localBoxes[entID - 1].Color = Color.FromArgb("#BDE6E2");
            if (ImgRD.Height != 0) localGrids[entID - 1].BackgroundColor = Colors.LightGray;
            else localGrids[entID - 1].BackgroundColor = Color.FromArgb("#BDD6E6");

            // DB query for images for second decision of entID
            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(entID);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.ExampleImages1 == true)
            {
                if (entryImagesTbl.FileNames1 != null) fileNames = entryImagesTbl.FileNames1.Split("* ").ToList();
            }
            else
            {
                if (entryImagesTbl.SpeciesGroup1 != null)
                {
                    if (entryImagesTbl.SpeciesGroup1.Contains("* "))
                    {
                        List<string> groupsIDs = new List<string>();
                        groupsIDs = entryImagesTbl.SpeciesGroup1.Split("* ").ToList();
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                    else
                    {
                        int groupID = int.Parse(entryImagesTbl.SpeciesGroup1);
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                }
            }


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
            ImageLabel0.FormattedText = formattedImageNames[0];
            Image0.Source = ImageSource.FromFile(fileNames[0]);
            if (fileNames.Count > 1)
            {
                ImageLabel1.FormattedText = formattedImageNames[1];
                Image1.Source = ImageSource.FromFile(fileNames[1]);
            }
            else
            {
                Image1.Source = null;
                Image2.Source = null;
            }
            if (fileNames.Count > 2)
            {
                ImageLabel2.FormattedText = formattedImageNames[2];
                Image2.Source = ImageSource.FromFile(fileNames[2]);
            }
            else
            {
                Image2.Source = null;
            }
            currentImages = keyEntID + "b";

            if (fileNames.Count < 4)
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            else
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            imagesIndex = 0;
        }
    }

    void OnTGRTapped(object? sender, TappedEventArgs args)
    {
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);

        char currentImg = currentImages[currentImages.Length - 1];
        int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
        ImageButton but = new()
        {
            ClassId = currentInd.ToString()
        };
        EventArgs newArgs = new();

        if (currentImg == 'a')
        {
            string keyEntID = currentInd.ToString();

            headLabel.Text = keyEntID + "b";
            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text1;
            else headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text1ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }

            int entID = int.Parse(keyEntID);
            int currentImgs = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
            if (entID != currentImgs)
            {
                localBoxes[currentImgs - 1].Color = Color.FromArgb("#BDE6E2");
                localGrids[currentImgs - 1].BackgroundColor = Color.FromArgb("#BDD6E6");
            }

            localBoxes[entID - 1].Color = Color.FromArgb("#BDE6E2");
            if (ImgRD.Height != 0 || ZoomRD.Height != 0) localGrids[entID - 1].BackgroundColor = Colors.LightGray;
            else localGrids[entID - 1].BackgroundColor = Color.FromArgb("#BDD6E6");
            
            // DB query for images for second decision of entID
            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(entID);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.ExampleImages1 == true)
            {
                if (entryImagesTbl.FileNames1 != null) fileNames = entryImagesTbl.FileNames1.Split("* ").ToList();
            }
            else
            {
                if (entryImagesTbl.SpeciesGroup1 != null)
                {
                    if (entryImagesTbl.SpeciesGroup1.Contains("* "))
                    {
                        List<string> groupsIDs = new List<string>();
                        groupsIDs = entryImagesTbl.SpeciesGroup1.Split("* ").ToList();
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                    else
                    {
                        int groupID = int.Parse(entryImagesTbl.SpeciesGroup1);
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                }
            }


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
            ImageLabel0.FormattedText = formattedImageNames[0];
            ImageLabel1.FormattedText = formattedImageNames[1];
            ImageLabel2.FormattedText = formattedImageNames[2];
            Image0.Source = ImageSource.FromFile(fileNames[0]);
            Image1.Source = ImageSource.FromFile(fileNames[1]);
            Image2.Source = ImageSource.FromFile(fileNames[2]);
            if (imgZoom)
            {
                Image0Z.Source = ImageSource.FromFile(fileNames[0]);
                Image1Z.Source = ImageSource.FromFile(fileNames[1]);
                Image2Z.Source = ImageSource.FromFile(fileNames[2]);
            }
            currentImages = keyEntID + "b";

            if (fileNames.Count < 4)
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            else
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            imagesIndex = 0;
        }
        else if (currentImg == 'b')
        {
            string keyEntID = currentInd.ToString();

            headLabel.Text = keyEntID + "a";

            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text0;
            else headLabelZ.Text = keyEntries[int.Parse(keyEntID) - 1].Text0ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }

            int entID = int.Parse(keyEntID);
            int currentImgs = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
            if (entID != currentImgs)
            {
                localBoxes[currentImgs - 1].Color = Color.FromArgb("#BDE6E2");
                localGrids[currentImgs - 1].BackgroundColor = Color.FromArgb("#BDD6E6");
            }

            if (ImgRD.Height != 0 || ZoomRD.Height != 0) localBoxes[entID - 1].Color = Colors.LightGray;
            else localBoxes[entID - 1].Color = Color.FromArgb("#BDE6E2");
            localGrids[entID - 1].BackgroundColor = Color.FromArgb("#BDD6E6");

            // DB query for images for first decision of entID
            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(entID);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.ExampleImages == true)
            {
                if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
            }
            else
            {
                if (entryImagesTbl.SpeciesGroup != null)
                {
                    if (entryImagesTbl.SpeciesGroup.Contains("* "))
                    {
                        List<string> groupsIDs = new List<string>();
                        groupsIDs = entryImagesTbl.SpeciesGroup.Split("* ").ToList();
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                    else
                    {
                        int groupID = int.Parse(entryImagesTbl.SpeciesGroup);
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                }
            }


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
            ImageLabel0.FormattedText = formattedImageNames[0];
            ImageLabel1.FormattedText = formattedImageNames[1];
            ImageLabel2.FormattedText = formattedImageNames[2];
            Image0.Source = ImageSource.FromFile(fileNames[0]);
            Image1.Source = ImageSource.FromFile(fileNames[1]);
            Image2.Source = ImageSource.FromFile(fileNames[2]);
            if (imgZoom)
            {
                Image0Z.Source = ImageSource.FromFile(fileNames[0]);
                Image1Z.Source = ImageSource.FromFile(fileNames[1]);
                Image2Z.Source = ImageSource.FromFile(fileNames[2]);
            }
            currentImages = keyEntID + "a";

            if (fileNames.Count < 4)
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            else
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            imagesIndex = 0;
        }
    }

    private void OnMoreButtonClicked(object sender, EventArgs args)
    {
        if (extraImages.Count > 0)
        {
            if (extraImages.Count > 3)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages);
                List<FormattedString> formattedImageNames = new List<FormattedString>();
                for (int i = 0; i < imageNames.Count; i++)
                {
                    formattedImageNames.Add(FormatImageLabel(imageNames[i]));
                    Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
                }

                int numGroups = imageNames.Count / 3;
                int extra = 0;
                if (imageNames.Count % 3 > 0) extra = 1;
                numGroups += extra;
                int imageIndex = 0;

                for (int i = 0; i < numGroups; i++)
                {
                    if (imageIndex == imagesIndex && i < numGroups - 1)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        ImageLabel1.FormattedText?.Spans.Clear();
                        ImageLabel2.FormattedText?.Spans.Clear();
                        if (dPlat != DevicePlatform.iOS)
                        {
                            Image0.Source = null;
                            Image0Z.Source = null;
                            Image1.Source = null;
                            Image1Z.Source = null;
                            Image2.Source = null;
                            Image2Z.Source = null;
                        }
                        for (int j = imageIndex + 3; j < formattedImageNames.Count; j++)
                        {
                            if (j >= imageIndex + 6) break;

                            if (j == imageIndex + 3)
                            {
                                ImageLabel0.FormattedText = formattedImageNames[j];
                                Image0.Source = ImageSource.FromFile(extraImages[j]);
                                if (imgZoom) Image0Z.Source = ImageSource.FromFile(extraImages[j]);
                            }
                            else if (j == imageIndex + 4)
                            {
                                ImageLabel1.FormattedText = formattedImageNames[j];
                                Image1.Source = ImageSource.FromFile(extraImages[j]);
                                if (imgZoom) Image1Z.Source = ImageSource.FromFile(extraImages[j]);
                            }
                            else if (j == imageIndex + 5)
                            {
                                ImageLabel2.FormattedText = formattedImageNames[j];
                                Image2.Source = ImageSource.FromFile(extraImages[j]);
                                if (imgZoom) Image2Z.Source = ImageSource.FromFile(extraImages[j]);
                            }
                        }

                        Console.WriteLine("SETTING IMAGES:   1");
                        imagesIndex = imageIndex + 3;

                        break;
                    }
                    else if (i == numGroups - 1)
                    {
                        ImageLabel0.FormattedText?.Spans.Clear();
                        ImageLabel1.FormattedText?.Spans.Clear();
                        ImageLabel2.FormattedText?.Spans.Clear();

                        if (dPlat != DevicePlatform.iOS)
                        {
                            Image0.Source = null;
                            Image0Z.Source = null;
                            Image1.Source = null;
                            Image1Z.Source = null;
                            Image2.Source = null;
                            Image2Z.Source = null;
                        }

                        ImageLabel0.FormattedText = formattedImageNames[0];
                        Image0.Source = ImageSource.FromFile(extraImages[0]);
                        ImageLabel1.FormattedText = formattedImageNames[1];
                        Image1.Source = ImageSource.FromFile(extraImages[1]);
                        ImageLabel2.FormattedText = formattedImageNames[2];
                        Image2.Source = ImageSource.FromFile(extraImages[2]);
                        if (imgZoom)
                        {
                            Image0Z.Source = ImageSource.FromFile(extraImages[0]);
                            Image1Z.Source = ImageSource.FromFile(extraImages[1]);
                            Image2Z.Source = ImageSource.FromFile(extraImages[2]);
                        }

                        Console.WriteLine("SETTING IMAGES:   0");
                        imagesIndex = 0;

                        break;
                    }

                    imageIndex += 3;
                }
            }
        }

        return;
    }

    private async void OnImagesButtonClicked(object sender, EventArgs args)
    {
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        bool scrollBool = false;

        if (imgZoom)
        {
            if (ZoomRD.Height == 0)
            {
                KeyRD.Height = 0;
                ImgRD.Height = 0;

                Image0Z.Source = Image0.Source;
                Image1Z.Source = Image1.Source;
                Image2Z.Source = Image2.Source;
                ZoomRD.Height = new GridLength(1, GridUnitType.Star);
                GRZ.IsVisible = true;

                int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
                localBoxes[currentInd - 1].Color = Color.FromArgb("#BDE6E2");
                localGrids[currentInd - 1].BackgroundColor = Color.FromArgb("#BDD6E6");

                imgNav.IsVisible = false;
            }
            else
            {
                KeyRD.Height = new GridLength(1, GridUnitType.Star);
                ImgRD.Height = 0;

                ZoomRD.Height = 0;
                GRZ.IsVisible = false;

                char currentImg = currentImages[currentImages.Length - 1];
                int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
                if (currentImg == 'a') localBoxes[currentInd - 1].Color = Colors.LightGray;
                else if (currentImg == 'b') localGrids[currentInd - 1].BackgroundColor = Colors.LightGray;

                imgNav.IsVisible = true;
                scrollBool = true;
            }
        }
        else
        {
            if (ImgRD.Height == 0)
            {
                KeyRD.Height = new GridLength(1, GridUnitType.Star);
                ImgRD.Height = new GridLength(1, GridUnitType.Auto);

                char currentImg = currentImages[currentImages.Length - 1];
                int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
                if (currentImg == 'a') localBoxes[currentInd - 1].Color = Colors.LightGray;
                else if (currentImg == 'b') localGrids[currentInd - 1].BackgroundColor = Colors.LightGray;

                ZoomRD.Height = 0;
                GRZ.IsVisible = false;

                imgNav.IsVisible = false;
                scrollBool = true;
            }
            else
            {
                KeyRD.Height = new GridLength(1, GridUnitType.Star);
                ImgRD.Height = 0;

                int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
                localBoxes[currentInd - 1].Color = Color.FromArgb("#BDE6E2");
                localGrids[currentInd - 1].BackgroundColor = Color.FromArgb("#BDD6E6");

                ZoomRD.Height = 0;
                GRZ.IsVisible = false;

                imgNav.IsVisible = true;
                scrollBool = true;
            }
        }


        if (scrollBool)
        {
            if (currentDecision > 0)
            {
                await Task.Delay(10);
                await SV.ScrollToAsync(localGrids[currentDecision - 1], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
            }
            else if (ImgRD.Height != 0 && SV.ScrollY != 0.0)
            {
                int currentImg = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
                await Task.Delay(10);
                await SV.ScrollToAsync(localGrids[currentImg - 1], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
            }
        }

        return;
    }


    private async void OnZoomButtonClicked(object sender, EventArgs args)
    {
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        if (ZoomRD.Height == 0)
        {
            KeyRD.Height = 0;
            ImgRD.Height = 0;

            Image0Z.Source = Image0.Source;
            Image1Z.Source = Image1.Source;
            Image2Z.Source = Image2.Source;
            ZoomRD.Height = new GridLength(1, GridUnitType.Star);
            GRZ.IsVisible = true;

            imgZoom = true;

            int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
            localBoxes[currentInd - 1].Color = Color.FromArgb("#BDE6E2");
            localGrids[currentInd - 1].BackgroundColor = Color.FromArgb("#BDD6E6");
        }
        else
        {
            ZoomRD.Height = 0;
            GRZ.IsVisible = false;

            KeyRD.Height = new GridLength(1, GridUnitType.Star);
            ImgRD.Height = new GridLength(1, GridUnitType.Auto);

            imgZoom = false;

            char currentImg = currentImages[currentImages.Length - 1];
            int currentInd = int.Parse(currentImages.Substring(0, currentImages.Length - 1));
            if (currentImg == 'a') localBoxes[currentInd - 1].Color = Colors.LightGray;
            else if (currentImg == 'b') localGrids[currentInd - 1].BackgroundColor = Colors.LightGray;

            if (currentDecision > 0)
            {
                await Task.Delay(10);
                await SV.ScrollToAsync(localGrids[currentDecision - 1], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
            }
            else if (ImgRD.Height != 0 && SV.ScrollY != 0.0)
            {
                await Task.Delay(10);
                await SV.ScrollToAsync(localGrids[currentInd - 1], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
            }
        }
    }


    private void ClearID_Clicked(object sender, EventArgs e)
    {
        HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
        for (int i = 0; i < 169; i++)
        {
            localBoxes[i].Color = Color.FromArgb("#BDE6E2");
            localGrids[i].BackgroundColor = Color.FromArgb("#BDD6E6");
        }
        if (ImgRD.Height != 0) localBoxes[0].Color = Colors.LightGrey;

        EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
        entryImagesTbl = App.DB.GetEntryImageByID(1);
        List<string> fileNames = new List<string>();
        if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
        List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
        List<FormattedString> formattedImageNames = new List<FormattedString>();
        for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
        ImageLabel0.FormattedText?.Spans.Clear();
        ImageLabel0.FormattedText = formattedImageNames[0];
        ImageLabel1.FormattedText?.Spans.Clear();
        ImageLabel1.FormattedText = formattedImageNames[1];
        ImageLabel2.FormattedText?.Spans.Clear();
        ImageLabel2.FormattedText = formattedImageNames[2];
        Image0.Source = ImageSource.FromFile(fileNames[0]);
        Image1.Source = ImageSource.FromFile(fileNames[1]);
        Image2.Source = ImageSource.FromFile(fileNames[2]);
        if (imgZoom)
        {
            Image0Z.Source = ImageSource.FromFile(fileNames[0]);
            Image1Z.Source = ImageSource.FromFile(fileNames[1]);
            Image2Z.Source = ImageSource.FromFile(fileNames[2]);
        }

        if (fileNames.Count > 3)
        {
            extraImages = fileNames;
            headMore.IsVisible = true;
            headMoreZ.IsVisible = true;
        }
        else
        {
            extraImages = new();
            headMore.IsVisible = false;
            headMoreZ.IsVisible = false;
        }
        imagesIndex = 0;

        pastDecisions.Clear();
        lNav.IsVisible = false;
        lNavZ.IsVisible = false;
        currentDecision = 0;
        currentDecisionLast = 0;
        currentImages = "1a";
        currentScroll = 0.0;
        futureDecisions.Clear();
        rNav.IsVisible = false;
        rNavZ.IsVisible = false;

        headLabel.Text = "1a";
        if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[0].Text0;
        else headLabelZ.Text = keyEntries[0].Text0ES;

        return;
    }


    private async void lNav_Clicked(object sender, EventArgs e)
    {
        if (pastDecisions.Count() > 0)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            int keyTarget = pastDecisions[pastDecisions.Count - 1] - 1;
            headLabel.Text = keyEntries[keyTarget].DecisionNum.ToString() + "a";
            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[keyTarget].Text0;
            else headLabelZ.Text = keyEntries[keyTarget].Text0ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }

            if (pastDecisions.Count > 1)
            {
                List<string> prev = new();
                List<string> grey = new();
                if (keyHistories[keyTarget].PrevDecisions != null) prev = keyHistories[keyTarget].PrevDecisions.Split(", ").ToList();
                if (keyHistories[keyTarget].GreyDecisions != null) grey = keyHistories[keyTarget].GreyDecisions.Split(", ").ToList();
                for (int i = 0; i < prev.Count; i++)
                {
                    string val = prev[i];
                    char decisionChar = val[val.Length - 1];
                    if (decisionChar == 'a')
                    {
                        int ind = int.Parse(val.Substring(0, val.Length - 1)) - 1;
                        localBoxes[ind].Color = Colors.LightGrey;
                        localGrids[ind].BackgroundColor = Colors.Grey;
                    }
                    else if (decisionChar == 'b')
                    {
                        int ind = int.Parse(val.Substring(0, val.Length - 1)) - 1;
                        localBoxes[ind].Color = Colors.Grey;
                        localGrids[ind].BackgroundColor = Colors.LightGrey;
                    }
                }

                if (grey[0] != " ")
                {
                    for (int i = 0; i < grey.Count; i++)
                    {
                        int ind = int.Parse(grey[i]) - 1;
                        localBoxes[ind].Color = Color.FromArgb("#84A19E");
                        localGrids[ind].BackgroundColor = Color.FromArgb("#8495A1");
                    }
                }

                for (int i = keyTarget + 1; i < 169; i++)
                {
                    localBoxes[i].Color = Color.FromArgb("#BDE6E2");
                    localGrids[i].BackgroundColor = Color.FromArgb("#BDD6E6");
                }

                if (ImgRD.Height != 0) localBoxes[keyTarget].Color = Colors.LightGrey;
                else localBoxes[keyTarget].Color = Color.FromArgb("#BDE6E2");
                localGrids[keyTarget].BackgroundColor = Color.FromArgb("#BDD6E6");
            }
            else
            {
                for (int i = 0; i < 169; i++)
                {
                    localBoxes[i].Color = Color.FromArgb("#BDE6E2");
                    localGrids[i].BackgroundColor = Color.FromArgb("#BDD6E6");
                }

                if (ImgRD.Height != 0) localBoxes[keyTarget].Color = Colors.LightGrey;
                else localBoxes[keyTarget].Color = Color.FromArgb("#BDE6E2");
                localGrids[keyTarget].BackgroundColor = Color.FromArgb("#BDD6E6");
            }


            if (futureDecisions.Count > 0)
            {
                futureDecisions.Add(currentDecision);
            }
            else
            {
                futureDecisions.Add(currentDecision);
            }
            pastDecisions.RemoveAt(pastDecisions.Count() - 1);
            currentDecision = keyTarget + 1;
            currentDecisionLast = keyTarget + 1;
            if (futureDecisions.Count > 0)
            {
                rNav.IsVisible = true;
                rNavZ.IsVisible = true;
            }
            if (pastDecisions.Count <= 0)
            {
                lNav.IsVisible = false;
                lNavZ.IsVisible = false;
                currentDecision = 0;
            }

            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(keyTarget + 1);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.ExampleImages == true)
            {
                if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
            }
            else
            {
                if (entryImagesTbl.SpeciesGroup != null)
                {
                    if (entryImagesTbl.SpeciesGroup.Contains("* "))
                    {
                        List<string> groupsIDs = new List<string>();
                        groupsIDs = entryImagesTbl.SpeciesGroup.Split("* ").ToList();
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                    else
                    {
                        int groupID = int.Parse(entryImagesTbl.SpeciesGroup);
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                }
            }

            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
            ImageLabel0.FormattedText = formattedImageNames[0];
            ImageLabel1.FormattedText = formattedImageNames[1];
            ImageLabel2.FormattedText = formattedImageNames[2];
            Image0.Source = ImageSource.FromFile(fileNames[0]);
            Image1.Source = ImageSource.FromFile(fileNames[1]);
            Image2.Source = ImageSource.FromFile(fileNames[2]);
            if (imgZoom)
            {
                Image0Z.Source = ImageSource.FromFile(fileNames[0]);
                Image1Z.Source = ImageSource.FromFile(fileNames[1]);
                Image2Z.Source = ImageSource.FromFile(fileNames[2]);
            }
            currentImages = (keyTarget + 1).ToString() + "a";

            if (fileNames.Count < 4)
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            else
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            imagesIndex = 0;

            await SV.ScrollToAsync(localGrids[keyTarget], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
        }

        return;
    }

    private async void rNav_Clicked(object sender, EventArgs e)
    {
        if (futureDecisions.Count > 0)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            int keyTarget = futureDecisions[futureDecisions.Count() - 1] - 1;

            // Correction for returning many decisions and subsequent pastDecisions duplication at pivot point
            if (futureDecisions.Count > 1)
            {
                if (currentDecision == futureDecisions[futureDecisions.Count - 1])
                {
                    futureDecisions.RemoveAt(futureDecisions.Count() - 1);
                    keyTarget = futureDecisions.Last() - 1;
                }
            }

            headLabel.Text = keyEntries[keyTarget].DecisionNum.ToString() + "a";
            if (CultureInfo.CurrentCulture.Name.Contains("en")) headLabelZ.Text = keyEntries[keyTarget].Text0;
            else headLabelZ.Text = keyEntries[keyTarget].Text0ES;
            if (headLabelZ.Text != null)
            {
                if (headLabelZ.Text.Length > 135)
                {
                    headLabelZ.FontSize = FSize - 2.0;
                }
                else
                {
                    headLabelZ.FontSize = FSize;
                }
            }
            List<string> prev = new();
            List<string> grey = new();
            if (keyHistories[keyTarget].PrevDecisions != null) prev = keyHistories[keyTarget].PrevDecisions.Split(", ").ToList();
            if (keyHistories[keyTarget].GreyDecisions != null) grey = keyHistories[keyTarget].GreyDecisions.Split(", ").ToList();
            for (int i = 0; i < prev.Count(); i++)
            {
                string val = prev[i];
                char decisionChar = val[val.Length - 1];
                if (decisionChar == 'a')
                {
                    int ind = int.Parse(val.Substring(0, val.Length - 1)) - 1;
                    localBoxes[ind].Color = Colors.LightGrey;
                    localGrids[ind].BackgroundColor = Colors.Grey;
                }
                else if (decisionChar == 'b')
                {
                    int ind = int.Parse(val.Substring(0, val.Length - 1)) - 1;
                    localBoxes[ind].Color = Colors.Grey;
                    localGrids[ind].BackgroundColor = Colors.LightGrey;
                }
            }

            if (grey[0] != " ")
            {
                for (int i = 0; i < grey.Count(); i++)
                {
                    int ind = int.Parse(grey[i]) - 1;
                    localBoxes[ind].Color = Color.FromArgb("#84A19E");
                    localGrids[ind].BackgroundColor = Color.FromArgb("#8495A1");
                }
            }

            for (int i = keyTarget + 1; i < 169; i++)
            {
                localBoxes[i].Color = Color.FromArgb("#BDE6E2");
                localGrids[i].BackgroundColor = Color.FromArgb("#BDD6E6");
            }

            if (ImgRD.Height != 0) localBoxes[keyTarget].Color = Colors.LightGrey;
            else localBoxes[keyTarget].Color = Color.FromArgb("#BDE6E2");
            localGrids[keyTarget].BackgroundColor = Color.FromArgb("#BDD6E6");

            if (currentDecision > 0)
            {
                pastDecisions.Add(currentDecision);
            }
            else
            {
                pastDecisions.Add(currentDecisionLast);
            }
            futureDecisions.RemoveAt(futureDecisions.Count() - 1);
            currentDecision = keyTarget + 1;
            currentDecisionLast = keyTarget + 1;
            if (pastDecisions.Count > 0)
            {
                lNav.IsVisible = true;
                lNavZ.IsVisible = true;
            }
            if (futureDecisions.Count <= 0)
            {
                rNav.IsVisible = false;
                rNavZ.IsVisible = false;
            }


            EntryImagesTbl entryImagesTbl = new EntryImagesTbl();
            entryImagesTbl = App.DB.GetEntryImageByID(keyTarget + 1);
            List<string> fileNames = new List<string>();
            if (entryImagesTbl.ExampleImages == true)
            {
                if (entryImagesTbl.FileNames != null) fileNames = entryImagesTbl.FileNames.Split("* ").ToList();
            }
            else
            {
                if (entryImagesTbl.SpeciesGroup != null)
                {
                    if (entryImagesTbl.SpeciesGroup.Contains("* "))
                    {
                        List<string> groupsIDs = new List<string>();
                        groupsIDs = entryImagesTbl.SpeciesGroup.Split("* ").ToList();
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(int.Parse(groupsIDs[0]));
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                    else
                    {
                        int groupID = int.Parse(entryImagesTbl.SpeciesGroup);
                        SpeciesGroup speciesGroup = App.DB.GetSpeciesGroupByID(groupID);
                        fileNames = speciesGroup.FileNames.Split("* ").ToList();
                    }
                }
            }


            ImageLabel0.FormattedText?.Spans.Clear();
            ImageLabel1.FormattedText?.Spans.Clear();
            ImageLabel2.FormattedText?.Spans.Clear();
            List<string> imageNames = App.DB.GetImageNamesByFileNames(fileNames);
            List<FormattedString> formattedImageNames = new List<FormattedString>();
            for (int i = 0; i < imageNames.Count; i++) formattedImageNames.Add(FormatImageLabel(imageNames[i]));
            ImageLabel0.FormattedText = formattedImageNames[0];
            ImageLabel1.FormattedText = formattedImageNames[1];
            ImageLabel2.FormattedText = formattedImageNames[2];
            Image0.Source = ImageSource.FromFile(fileNames[0]);
            Image1.Source = ImageSource.FromFile(fileNames[1]);
            Image2.Source = ImageSource.FromFile(fileNames[2]);
            if (imgZoom)
            {
                Image0Z.Source = ImageSource.FromFile(fileNames[0]);
                Image1Z.Source = ImageSource.FromFile(fileNames[1]);
                Image2Z.Source = ImageSource.FromFile(fileNames[2]);
            }
            currentImages = (keyTarget + 1).ToString() + "a";

            if (fileNames.Count < 4)
            {
                extraImages = new();
                headMore.IsVisible = false;
                headMoreZ.IsVisible = false;
            }
            else
            {
                extraImages = fileNames;
                headMore.IsVisible = true;
                headMoreZ.IsVisible = true;
            }
            imagesIndex = 0;

            await SV.ScrollToAsync(localGrids[keyTarget], ScrollToPosition.Center, Preferences.Get("IDScrolling", true));
        }

        return;
    }


    private FormattedString FormatImageLabel(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize - 3.0
        };

        if (str[0] == '[') span.FontFamily = "OpenSansItalic";
        else span.Text += str[0];

        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] == '[')
            {
                fStr.Spans.Add(span);
                span = new()
                {
                    FontSize = fSize - 3.0,
                    FontFamily = "OpenSansItalic"
                };
            }
            else if (str[i] == ']')
            {
                fStr.Spans.Add(span);
                span = new()
                {
                    FontSize = fSize - 3.0,
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
        if (span.Text != null || span.Text != string.Empty || span.Text != "") fStr.Spans.Add(span);

        return fStr;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this.BindingContext = null;

        Task.Run(async () =>
        {
            await Task.Delay(100); // Small delay to allow safe disposal
        });
    }
}