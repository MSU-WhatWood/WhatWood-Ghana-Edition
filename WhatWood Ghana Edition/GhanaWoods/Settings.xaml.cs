using GhanaWoods.Resources.Languages;
using Microsoft.Maui.Controls;
using System.Globalization;
using Microsoft.Maui.ApplicationModel.Communication;
using static GhanaWoods.TabAssets;
using CommunityToolkit.Maui;
using GhanaWoods.Database;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace GhanaWoods;

public partial class Settings : ContentPage
{
    private double fSize = Preferences.Get("FontSize", 16.0);
    public double FSize
    {
        get { return fSize; }
        set
        {
            if (value == fSize) return;

            fSize = value;
            OnPropertyChanged();
        }
    }
    public List<string> LaunchPages = new() { SettingsTab.studyFC, SettingsTab.learnFC, SettingsTab.idC, SettingsTab.referenceFC };
    public List<string> Languages = new() { SettingsTab.English, SettingsTab.Spanish };

    //private string fileCopyName = "";
    //private string filePath = "";

    public int Plat = 0;
    public bool isCreated = true;

    public Settings()
    {
        InitializeComponent();
        this.Title = SettingsTab.settingsFC;

        FSize = Preferences.Get("FontSize", 16.0);

        this.SizeChanged += OnSizeChanged;
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

        isCreated = false;
        FSize = Preferences.Get("FontSize", 16.0);

        if (DeviceInfo.Current.Platform == DevicePlatform.iOS) Plat = 1;
        else Plat = 0;

        //string test = Resource2.test;

        settingsLabel.Text = SettingsTab.settingsFC;
        settingsLabel.FontSize = FSize + 3.0;

        FormattedString fStr = new();
        fsizeLabel.FormattedText?.Spans.Clear();
        Span span = new Span()
        {
            Text = SettingsTab.Font_size_
        };
        fStr.Spans.Add(span);
        span = new Span()
        {
            Text = FSize.ToString()
        };
        fStr.Spans.Add(span);
        fsizeLabel.FormattedText = fStr;
        fsizeSlider.Value = FSize;

        scrollLabel.Text = SettingsTab.I_D__scroll_animation_;
        scrollCheckBox.IsChecked = Preferences.Get("IDScrolling", true);

        colorLabel.Text = SettingsTab.Colorblind_mode_;
        int appTheme = Preferences.Get("AppTheme", 0);
        if (appTheme == 0)
        {
            colorCheckBox.IsChecked = false;
        }
        else if (appTheme == 1)
        {
            colorCheckBox.IsChecked = true;
        }

        launchLabel.Text = SettingsTab.Launch_page_;
        launchPicker.ItemsSource = LaunchPages;
        launchPicker.SelectedIndex = Preferences.Get("LaunchPage", 2);

        //languageLabel.Text = SettingsTab.Language_;
        //languagePicker.ItemsSource = Languages;

        //int currentLang = 0;
        //if (CultureInfo.CurrentCulture.Name.Contains("en")) currentLang = 0;
        //else if (CultureInfo.CurrentCulture.Name.Contains("es")) currentLang = 1;
        //languagePicker.SelectedIndex = currentLang;

        btnCredits.Text = SettingsTab.Credits;

        isCreated = true;

        return;
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        double value = Math.Round(e.NewValue, 0);
        fsizeLabel.FontSize = value;
        scrollLabel.FontSize = value;
        colorLabel.FontSize = value;
        launchLabel.FontSize = value;
        //languageLabel.FontSize = value;

        settingsLabel.FontSize = value + 3.0;
        fsizeLabel.FormattedText.Spans.RemoveAt(1);

        Span span = new Span()
        {
            Text = value.ToString()
        };
        fsizeLabel.FormattedText.Spans.Add(span);

        Preferences.Set("FontSize", value);
        Preferences.Set("FontSizeChangedID", true);
        Preferences.Set("FontSizeChangedRef", true);
    }

    //void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    if (isCreated) HapticFeedback.Default.Perform(HapticFeedbackType.Click);
    //    // Perform required operation after examining e.Value
    //    if (e.Value)
    //    {
    //        Preferences.Set("Hyperlinks", true);
    //    }
    //    else
    //    {
    //        Preferences.Set("Hyperlinks", false);
    //    }
    //}

    void OnCheckBoxCheckedChanged1(object sender, CheckedChangedEventArgs e)
    {
        if (isCreated) HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        // Perform required operation after examining e.Value
        if (e.Value)
        {
            Preferences.Set("IDScrolling", true);
        }
        else
        {
            Preferences.Set("IDScrolling", false);
        }
    }

    void OnColorCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isCreated) HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
        // Perform required operation after examining e.Value
        if (Application.Current != null)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (e.Value)
            {
                Preferences.Set("AppTheme", 1);
                if (mergedDictionaries != null)
                {
                    mergedDictionaries.Clear();

                    mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewCBColors());
                    mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewStyles());
                }
            }
            else
            {
                Preferences.Set("AppTheme", 0);
                if (mergedDictionaries != null)
                {
                    mergedDictionaries.Clear();

                    mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewColors());
                    mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewStyles());
                }
            }
        }
    }

    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (isCreated) HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            Preferences.Set("LaunchPage", selectedIndex);
        }
    }

    void OnLanguagePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (isCreated) HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        int currentLang = 0;
        if (CultureInfo.CurrentCulture.Name.Contains("en")) currentLang = 0;
        else if (CultureInfo.CurrentCulture.Name.Contains("es")) currentLang = 1;

        if (selectedIndex != -1)
        {
            if (selectedIndex != currentLang)
            {
                if (selectedIndex == 0)
                {
                    // Change the current culture to English (en).
                    CultureInfo.CurrentCulture = new CultureInfo("en", false);
                    // Change the current UI culture to English (en).
                    CultureInfo.CurrentUICulture = new CultureInfo("en", false);

                    int lCount = localLabelsRef.Count;
                    List<SpeciesGroup> sgList = App.DB.GetSpeciesGroupsList();
                    for (int i = 0; i < localLabels.Count; i++)
                    {
                        localLabels[i].Text = keyEntries[i].Text0;
                        localLabels1[i].Text = keyEntries[i].Text1;
                        if (i < lCount)
                        {
                            if (sgList[i].Name != sgList[i].NameES)
                            {
                                localLabelsRef[i].FormattedText.Spans.Clear();
                                localLabelsRef[i].FormattedText = FormatRefTitle(sgList[i].Name, sgList[i].Spp);
                            }
                        }
                    }                    
                }
                else
                {
                    // Change the current culture to Spanish (es).
                    CultureInfo.CurrentCulture = new CultureInfo("es", false);
                    // Change the current UI culture to Spanish (es).
                    CultureInfo.CurrentUICulture = new CultureInfo("es", false);

                    int lCount = localLabelsRef.Count;
                    List<SpeciesGroup> sgList = App.DB.GetSpeciesGroupsList();
                    for (int i = 0; i < localLabels.Count; i++)
                    {
                        localLabels[i].Text = keyEntries[i].Text0ES;
                        localLabels1[i].Text = keyEntries[i].Text1ES;
                        if (i < lCount)
                        {
                            if (sgList[i].Name != sgList[i].NameES)
                            {
                                localLabelsRef[i].FormattedText.Spans.Clear();
                                if (sgList[i].NameES != null) localLabelsRef[i].FormattedText = FormatRefTitle(sgList[i].NameES, sgList[i].Spp);
                                else localLabelsRef[i].FormattedText = FormatRefTitle(sgList[i].Name, sgList[i].Spp);
                            }
                        }
                    }
                }

                Preferences.Set("Language", selectedIndex);
                Preferences.Set("LanguageChanged", true);
                Preferences.Set("LanguageSet", true);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    //Application.Current.MainPage = new AppShell();
                    if (Application.Current != null) Application.Current.Windows[0].Page = new AppShell();

                    Shell.Current.GoToAsync("//Settings", false);
                });
            }
        }
    }

    private async void OnCreditsButtonClicked(object sender, EventArgs e)
    {
        if (isCreated) HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
        await Shell.Current.Navigation.PushAsync(new Credits(), false);
    }

    //private void BtnDBBU_ClickedAsync(object sender, EventArgs e)
    //{
    //    var canCreate = CreateBU();
    //    if (canCreate != null)
    //    {
    //        List<string> rec = new List<string>();
    //        EmailMessage message = new EmailMessage
    //        {
    //            To = rec,
    //            Subject = "CAMC Woods Db Back-up",
    //            Body = "This is a backup of the database from the CAMC Woods app.",

    //        };

    //        message.Attachments.Add(new EmailAttachment(canCreate));
    //        Email.Default.ComposeAsync(message);
    //    }
    //}
    //private string CreateBU()
    //{
    //    var folder = Environment.SpecialFolder.LocalApplicationData;
    //    var folderPath = Environment.GetFolderPath(folder);
    //    string documentsPath = System.IO.Path.Join(folderPath, "ghanawoods.db");

    //    fileCopyName = string.Format("GhanaWoodsDatabase_{0:dd-MM-yyyy_HH-mm-ss-tt}.db", System.DateTime.Now);
    //    filePath = Path.Combine(folderPath, fileCopyName);

    //    var bytes = File.ReadAllBytes(documentsPath);
    //    File.WriteAllBytes(filePath, bytes);

    //    return filePath;
    //}

    private FormattedString FormatRefTitle(string inputStr, bool sppVal)
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
        }

        return fStr;
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}