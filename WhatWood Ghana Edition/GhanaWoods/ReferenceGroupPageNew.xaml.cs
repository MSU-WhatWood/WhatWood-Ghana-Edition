using GhanaWoods.Resources.Helpers;
using GhanaWoods.Resources.Languages;
using GhanaWoods.Database;
using System.Globalization;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using System.Windows.Input;

namespace GhanaWoods;

public partial class ReferenceGroupPageNew : ContentPage
{
    SpeciesGroup spGr = new SpeciesGroup();
    List<ImageTbl> imageTbls = new List<ImageTbl>();
    double FSize = Preferences.Get("FontSize", 16.0);
    string notesKey = "";
    int IDReturn = 0;
    int language = Preferences.Get("Language", 0);
    DevicePlatform dPlat = new DevicePlatform();

    List<string> extraImages0 = new List<string>();
    int images0Index = 0;
    List<string> extraImages1 = new List<string>();
    int images1Index = 0;

    TapGestureRecognizer zTGR = new();

    public ReferenceGroupPageNew()
    {
        InitializeComponent();

        this.Title = ReferenceTabMain.referenceFC;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        if (dPlat == DevicePlatform.Android) this.SizeChanged += OnSizeChanged;
    }

    public ReferenceGroupPageNew(SpeciesGroup group, int idReturn = 0)
    {
        InitializeComponent();

        this.Title = ReferenceTabGroup.Species_Group;

        this.SizeChanged += OnSizeChanged;

        zTGR = new TapGestureRecognizer();
        zTGR.Tapped += async (s, e) =>
        {
            Image? s1 = s as Image;
            if (s1 != null)
            {
                if (s1.Source != null) await AppShell.Current.Navigation.PushAsync(new ImageZoomPage(s1.Source), false);
            }
        };
        for (int i = 0; i < grid2.Children.Count; i++)
        {
            if (grid2.Children[i].GetType() == typeof(Image)) ((Image)grid2.Children[i]).GestureRecognizers.Add(zTGR);
        }
        //for (int i = 0; i < grid9.Children.Count; i++)
        //{
        //    if (grid9.Children[i].GetType() == typeof(Image)) ((Image)grid9.Children[i]).GestureRecognizers.Add(zTGR);
        //}


        spGr = group;
        FSize = Preferences.Get("FontSize", 16.0);
        notesKey = "SpGrNotes" + spGr.SpeciesGroupID.ToString();
        IDReturn = idReturn;
        //if (CultureInfo.CurrentCulture.Name.Contains("en")) language = 0;
        //else if (CultureInfo.CurrentCulture.Name.Contains("es")) language = 1;
        language = 0;

        label0.Text = ReferenceTabGroup.Family_ + spGr.Family;
        label0.FontSize = 16;

        label1.FormattedText?.Spans.Clear();

        FormattedString fStr = new FormattedString();
        //if (language == 0) fStr = FormatTitle(spGr.Name, spGr.Spp);
        //else if (spGr.NameES != null) fStr = FormatTitle(spGr.NameES, spGr.Spp);
        fStr = FormatTitle(spGr.Name, spGr.Spp, spGr);

        //if (spGr.Species != " " && spGr.Species != string.Empty && !string.IsNullOrEmpty(spGr.Species) && !String.IsNullOrEmpty(spGr.Species))
        if (spGr.NameES != " " && spGr.NameES != string.Empty && !string.IsNullOrEmpty(spGr.NameES) && !String.IsNullOrEmpty(spGr.NameES))
        {
            Span span = new()
            {
                //Text = Environment.NewLine + spGr.Species,
                Text = Environment.NewLine + spGr.NameES,
                //FontFamily = "OpenSansItalic",
                FontFamily = "OpenSansRegular",
                FontSize = FSize + 3.0
            };
            fStr.Spans.Add(span);
            frameS2.Padding = new Thickness(10);
        }
        else frameS2.Padding = new Thickness(10, 10, 10, 10);
        label1.FormattedText = fStr;

        extraImages0 = new List<string>();
        images0Index = 0;


        AddImages0New();


        AddCharacters();


        AddAdditionalandSimilar();


        extraImages1 = new List<string>();
        images1Index = 0;
        //AddImages1New();


        editor10.Placeholder = ReferenceTabGroup.Notes;
        if (Preferences.Get(notesKey, "") != string.Empty || Preferences.Get(notesKey, "") != null)
        {
            editor10.Text = Preferences.Get(notesKey, "");
        }
        else
        {
            editor10.Text = string.Empty;
            editor10.Placeholder = ReferenceTabGroup.Notes;
        }
        editor10.FontSize = FSize;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (IDReturn == 1)
        {
            bool answer = await DisplayAlert(ReferenceTabGroup.Reminder, ReferenceTabGroup.potential_match + "\n\n" +
                ReferenceTabGroup.Remember_to_compare, ReferenceTabGroup.Accept, ReferenceTabGroup.Return);
            if (!answer)
            {
                await Shell.Current.Navigation.PopToRootAsync(false);
                await Shell.Current.GoToAsync("//ID");
            }
            else IDReturn = 0;
        }
    }

    void OnEditorCompleted(object sender, EventArgs e)
    {
        string text = ((Editor)sender).Text;

        if (notesKey != string.Empty)
        {
            Preferences.Set(notesKey, text);
        }
    }

    void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        string myText = ((Editor)sender).Text;


        if (notesKey != string.Empty)
        {
            Preferences.Set(notesKey, myText);
        }
    }

    private FormattedString FormatSimilar(string inputStr, string groupIDs)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        int simIndex = 0;
        List<int> simIDs = new List<int>();
        List<string> simIDstrings = new List<string>();
        if (!string.IsNullOrEmpty(groupIDs) && groupIDs != " ")
        {
            simIDstrings = groupIDs.Split(", ").ToList();
            //foreach (string simIDstring in simIDstrings) simIDs.Add(int.Parse(simIDstring));
            for (int i = 0; i < simIDstrings.Count; i++) simIDs.Add(int.Parse(simIDstrings[i]));
        }
        else
        {
            for (int i = 0; i < 10; i++) simIDs.Add(1);
        }

        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansRegular"
        };
        if (str != " " && !string.IsNullOrEmpty(str))
        {
            if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            else if (str[0] == '^')
            {
                //span.FontFamily = "OpenSansRegular";
                span.FontFamily = "OpenSansItalic";
                span.TextDecorations = TextDecorations.Underline;
                span.TextColor = Colors.Blue;
            }
            else span.Text += str[0];

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
                else if (str[i] == '^')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        //FontFamily = "OpenSansRegular",
                        FontFamily = "OpenSansItalic",
                        TextDecorations = TextDecorations.Underline
                    };
                    span.TextColor = Colors.Blue;
                }
                else if (str[i] == '!')
                {
                    TapGestureRecognizer tGR = new();
                    //tGR.Tapped += async (s, e) =>
                    //{
                    //    await Shell.Current.Navigation.PushAsync(new ReferenceGroupPageNew(App.DB.GetSpeciesGroupByID(simIDs[simIndex]), 2));
                    //};
                    if (simIDs.Count > simIndex) tGR.CommandParameter = simIDs[simIndex];
                    tGR.Command = TapCommand;
                    span.GestureRecognizers.Add(tGR);
                    fStr.Spans.Add(span);

                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular",
                        TextDecorations = TextDecorations.Underline
                    };
                    span.TextColor = Colors.Blue;
                }
                else if (str[i] == '|')
                {
                    TapGestureRecognizer tGR = new();
                    //tGR.CommandParameter = span.Text;
                    if (simIDs.Count > simIndex) tGR.CommandParameter = simIDs[simIndex];
                    simIndex++;
                    tGR.Command = TapCommand;
                    //tGR.Tapped += async (s, e) =>
                    //{
                    //    await Shell.Current.Navigation.PushAsync(new ReferenceGroupPageNew(App.DB.GetSpeciesGroupByID(simIDs[simIndex]), 2));
                    //};
                    //simIndex++;
                    //if (dPlat != DevicePlatform.iOS) span.GestureRecognizers.Add(tGR);
                    span.GestureRecognizers.Add(tGR);
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
            //if (span.Text != null || span.Text != "" || span.Text != string.Empty) fStr.Spans.Add(span);
            if (!string.IsNullOrEmpty(span.Text)) fStr.Spans.Add(span);
        }

        return fStr;
    }

    private List<Span> FormatStringReturnSpans(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        List<Span> spans = new List<Span>();
        Span span = new()
        {
            FontSize = fSize
        };
        if (str != string.Empty || str != " " || !string.IsNullOrEmpty(str) || !String.IsNullOrEmpty(str))
        {
            if (str[0] == '[')
            {
                span.FontFamily = "OpenSansItalic";
            }
            else
            {
                span.FontFamily = "OpenSansRegular";
                span.Text += str[0];
            }
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '[')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansItalic"
                    };
                }
                else if (str[i] == ']')
                {
                    spans.Add(span);
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
            span.Text += "\n";
            spans.Add(span);
        }

        return spans;
    }

    private FormattedString FormatString(string inputStr, string test = "")
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize
        };
        if (str != string.Empty || str != " " || !string.IsNullOrEmpty(str) || !String.IsNullOrEmpty(str))
        {
            Console.WriteLine(test + " - " + str);
            if (str[0] == '[')
            {
                span.FontFamily = "OpenSansItalic";
            }
            else
            {
                span.FontFamily = "OpenSansRegular";
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
                else if (str[i - 1] == '\\' && str[i] == 'n')
                {
                    span.Text = span.Text.Remove(span.Text.Length - 1, 1);
                    span.Text += Environment.NewLine;
                }
                else
                {
                    span.Text += str[i];
                }
            }
            if (span.Text != null || span.Text != "" || span.Text != string.Empty) fStr.Spans.Add(span);
        }

        return fStr;
    }

    private FormattedString FormatStringRemoveParentheses(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0) - 3.0;
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
                span.FontFamily = "OpenSansRegular";
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

                if (str[i] == ' ' && str[i + 1] == '(')
                {
                    if (str[i - 1] != ']') fStr.Spans.Add(span);

                    return fStr;
                }
            }
            if (span.Text != null || span.Text != "" || span.Text != string.Empty) fStr.Spans.Add(span);
        }

        return fStr;
    }

    private FormattedString FormatStringRemoveParenthesesFirstExample(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0) - 3.0;
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize
        };
        if (str != string.Empty || str != " " || !string.IsNullOrEmpty(str))
        {
            if (str[0] == '[')
            {
                span.FontFamily = "OpenSansSemiboldItalic";
            }
            else
            {
                span.FontFamily = "OpenSansSemibold";
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

                if (str[i] == ' ' && str[i + 1] == '(')
                {
                    if (str[i - 1] != ']') fStr.Spans.Add(span);

                    return fStr;
                }
            }
            if (span.Text != null || span.Text != "" || span.Text != string.Empty) fStr.Spans.Add(span);
        }

        return fStr;
    }

    private FormattedString FormatTitle(string inputStr, bool sppVal, SpeciesGroup spGr)
    {
        string str = inputStr;
        bool spp = sppVal;
        double fSize = Preferences.Get("FontSize", 16.0) + 5.0;
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize
        };
        bool ital = false;
        if (spp)
        {
            span.Text = str;
            span.FontFamily = "OpenSansSemiboldItalic";
            fStr.Spans.Add(span);
            span = new()
            {
                Text = ReferenceTabMain.spp_,
                FontFamily = "OpenSansSemibold",
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
                span.FontFamily = "OpenSansSemiboldItalic";
            }
            else
            {
                span.Text += str[0];
                span.FontFamily = "OpenSansSemibold";
            }
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
                    ital = true;
                }
                else
                {
                    span.Text += str[i];
                }
            }
            if (!ital) span.FontFamily = "OpenSansSemiboldItalic";
            if (span.Text != null || span.Text != "" || span.Text != string.Empty) fStr.Spans.Add(span);

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

    //void AddImages1New()
    //{
    //    List<string> imageNames = new List<string>();
    //    List<string> fileNames = new List<string>();
    //    fileNames = spGr.SimilarFileNames.Split("* ").ToList();

    //    imageNames = App.DB.GetImageNamesByFileNames(fileNames);
    //    List<FormattedString> formattedImageNames = new List<FormattedString>();
    //    formattedImageNames.Add(FormatStringRemoveParenthesesFirstExample(imageNames[0]));
    //    for (int i = 1; i < imageNames.Count; i++)
    //    {
    //        formattedImageNames.Add(FormatStringRemoveParentheses(imageNames[i]));
    //    }

    //    images1Label0.FormattedText?.Spans.Clear();
    //    images1Label0.FormattedText = formattedImageNames[0];
    //    images1Image0.Source = ImageSource.FromFile(fileNames[0]);
    //    //if (dPlat == DevicePlatform.Android) grid9.Padding = new Thickness(-5, 5, -5, -15);
    //    //else grid9.Padding = new Thickness(-5, 5, -5, 10);


    //    if (fileNames.Count > 1)
    //    {
    //        images1Label1.FormattedText?.Spans.Clear();
    //        images1Label1.FormattedText = formattedImageNames[1];
    //        images1Image1.Source = ImageSource.FromFile(fileNames[1]);
    //    }
    //    if (fileNames.Count > 2)
    //    {
    //        grid9rd2.Height = new GridLength(1, GridUnitType.Auto);
    //        grid9rd3.Height = new GridLength(1, GridUnitType.Auto);
    //        images1Label2.FormattedText?.Spans.Clear();
    //        images1Label2.FormattedText = formattedImageNames[2];
    //        images1Image2.Source = ImageSource.FromFile(fileNames[2]);
    //        //grid9.Padding = new Thickness(-5, 5, -5, 10);
    //    }
    //    if (fileNames.Count > 3)
    //    {
    //        images1Label3.FormattedText?.Spans.Clear();
    //        images1Label3.FormattedText = formattedImageNames[3];
    //        images1Image3.Source = ImageSource.FromFile(fileNames[3]);
    //    }
    //    if (fileNames.Count > 4)
    //    {
    //        grid9rd4.Height = new GridLength(1, GridUnitType.Auto);
    //        grid9rd5.Height = new GridLength(1, GridUnitType.Auto);
    //        images1Label4.FormattedText?.Spans.Clear();
    //        images1Label4.FormattedText = formattedImageNames[4];
    //        images1Image4.Source = ImageSource.FromFile(fileNames[4]);
    //    }
    //    if (fileNames.Count > 5)
    //    {
    //        images1Label5.FormattedText?.Spans.Clear();
    //        images1Label5.FormattedText = formattedImageNames[5];
    //        images1Image5.Source = ImageSource.FromFile(fileNames[5]);
    //    }
    //    if (fileNames.Count > 6)
    //    {
    //        grid9rd6.Height = new GridLength(1, GridUnitType.Auto);
    //        images1NavGrid.IsVisible = true;

    //        extraImages1 = fileNames;
    //        images1Index = 0;
    //    }

    //    return;
    //}
    //void OnImages1LeftNavClicked(object? sender, EventArgs args)
    //{
    //    if (extraImages1.Count > 0)
    //    {
    //        if (extraImages1.Count > 6)
    //        {
    //            List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages1);
    //            List<FormattedString> formattedImageNames = new List<FormattedString>();
    //            formattedImageNames.Add(FormatStringRemoveParenthesesFirstExample(imageNames[0]));
    //            for (int i = 1; i < imageNames.Count; i++)
    //            {
    //                if (i % 6 == 0) formattedImageNames.Add(FormatStringRemoveParenthesesFirstExample(imageNames[i]));
    //                else formattedImageNames.Add(FormatStringRemoveParentheses(imageNames[i]));
    //                Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
    //            }

    //            int numGroups = imageNames.Count / 6;
    //            int extra = 0;
    //            if (imageNames.Count % 6 > 0) extra = 1;
    //            numGroups += extra;
    //            int imageIndex = numGroups * 6 - 6;

    //            for (int i = numGroups - 1; i >= 0; i--)
    //            {
    //                if (imageIndex == images1Index && i > 0)
    //                {
    //                    images1Label0.FormattedText?.Spans.Clear();
    //                    images1Label1.FormattedText?.Spans.Clear();
    //                    images1Label2.FormattedText?.Spans.Clear();
    //                    images1Label3.FormattedText?.Spans.Clear();
    //                    images1Label4.FormattedText?.Spans.Clear();
    //                    images1Label5.FormattedText?.Spans.Clear();
    //                    images1Image0.Source = null;
    //                    images1Image1.Source = null;
    //                    images1Image2.Source = null;
    //                    images1Image3.Source = null;
    //                    images1Image4.Source = null;
    //                    images1Image5.Source = null;
    //                    for (int j = imageIndex - 6; j < formattedImageNames.Count; j++)
    //                    {
    //                        if (j >= imageIndex) break;

    //                        if (j == imageIndex - 6)
    //                        {
    //                            images1Label0.FormattedText = formattedImageNames[j];
    //                            images1Image0.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex - 5)
    //                        {
    //                            images1Label1.FormattedText = formattedImageNames[j];
    //                            images1Image1.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex - 4)
    //                        {
    //                            images1Label2.FormattedText = formattedImageNames[j];
    //                            images1Image2.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex - 3)
    //                        {
    //                            images1Label3.FormattedText = formattedImageNames[j];
    //                            images1Image3.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex - 2)
    //                        {
    //                            images1Label4.FormattedText = formattedImageNames[j];
    //                            images1Image4.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex - 1)
    //                        {
    //                            images1Label5.FormattedText = formattedImageNames[j];
    //                            images1Image5.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                    }
    //                    Console.WriteLine("SETTING IMAGES:   1");
    //                    images1Index = imageIndex - 6;

    //                    break;
    //                }
    //                else if (images1Index == 0)
    //                {
    //                    images1Label0.FormattedText?.Spans.Clear();
    //                    images1Label1.FormattedText?.Spans.Clear();
    //                    images1Label2.FormattedText?.Spans.Clear();
    //                    images1Label3.FormattedText?.Spans.Clear();
    //                    images1Label4.FormattedText?.Spans.Clear();
    //                    images1Label5.FormattedText?.Spans.Clear();
    //                    images1Image0.Source = null;
    //                    images1Image1.Source = null;
    //                    images1Image2.Source = null;
    //                    images1Image3.Source = null;
    //                    images1Image4.Source = null;
    //                    images1Image5.Source = null;

    //                    imageIndex = numGroups * 6 - 6;

    //                    for (int j = imageIndex; j < formattedImageNames.Count; j++)
    //                    {
    //                        if (j >= imageIndex + 6) break;

    //                        if (j == imageIndex)
    //                        {
    //                            images1Label0.FormattedText = formattedImageNames[j];
    //                            images1Image0.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 1)
    //                        {
    //                            images1Label1.FormattedText = formattedImageNames[j];
    //                            images1Image1.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 2)
    //                        {
    //                            images1Label2.FormattedText = formattedImageNames[j];
    //                            images1Image2.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 3)
    //                        {
    //                            images1Label3.FormattedText = formattedImageNames[j];
    //                            images1Image3.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 4)
    //                        {
    //                            images1Label4.FormattedText = formattedImageNames[j];
    //                            images1Image4.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 5)
    //                        {
    //                            images1Label5.FormattedText = formattedImageNames[j];
    //                            images1Image5.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                    }


    //                    Console.WriteLine("SETTING IMAGES:   0");
    //                    images1Index = imageIndex;

    //                    break;
    //                }

    //                imageIndex -= 6;
    //            }
    //        }
    //    }
    //}
    //void OnImages1RightNavClicked(object? sender, EventArgs args)
    //{
    //    if (extraImages1.Count > 0)
    //    {
    //        if (extraImages1.Count > 6)
    //        {
    //            List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages1);
    //            List<FormattedString> formattedImageNames = new List<FormattedString>();
    //            formattedImageNames.Add(FormatStringRemoveParenthesesFirstExample(imageNames[0]));
    //            for (int i = 1; i < imageNames.Count; i++)
    //            {
    //                if (i % 6 == 0) formattedImageNames.Add(FormatStringRemoveParenthesesFirstExample(imageNames[i]));
    //                else formattedImageNames.Add(FormatStringRemoveParentheses(imageNames[i]));
    //                Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
    //            }

    //            int numGroups = imageNames.Count / 6;
    //            int extra = 0;
    //            if (imageNames.Count % 6 > 0) extra = 1;
    //            numGroups += extra;
    //            int imageIndex = 0;

    //            for (int i = 0; i < numGroups; i++)
    //            {
    //                if (imageIndex == images1Index && i < numGroups - 1)
    //                {
    //                    images1Label0.FormattedText?.Spans.Clear();
    //                    images1Label1.FormattedText?.Spans.Clear();
    //                    images1Label2.FormattedText?.Spans.Clear();
    //                    images1Label3.FormattedText?.Spans.Clear();
    //                    images1Label4.FormattedText?.Spans.Clear();
    //                    images1Label5.FormattedText?.Spans.Clear();
    //                    images1Image0.Source = null;
    //                    images1Image1.Source = null;
    //                    images1Image2.Source = null;
    //                    images1Image3.Source = null;
    //                    images1Image4.Source = null;
    //                    images1Image5.Source = null;
    //                    for (int j = imageIndex + 6; j < formattedImageNames.Count; j++)
    //                    {
    //                        if (j >= imageIndex + 12) break;

    //                        if (j == imageIndex + 6)
    //                        {
    //                            images1Label0.FormattedText = formattedImageNames[j];
    //                            images1Image0.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 7)
    //                        {
    //                            images1Label1.FormattedText = formattedImageNames[j];
    //                            images1Image1.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 8)
    //                        {
    //                            images1Label2.FormattedText = formattedImageNames[j];
    //                            images1Image2.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 9)
    //                        {
    //                            images1Label3.FormattedText = formattedImageNames[j];
    //                            images1Image3.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 10)
    //                        {
    //                            images1Label4.FormattedText = formattedImageNames[j];
    //                            images1Image4.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                        else if (j == imageIndex + 11)
    //                        {
    //                            images1Label5.FormattedText = formattedImageNames[j];
    //                            images1Image5.Source = ImageSource.FromFile(extraImages1[j]);
    //                        }
    //                    }
    //                    Console.WriteLine("SETTING IMAGES:   1");
    //                    images1Index = imageIndex + 6;

    //                    break;
    //                }
    //                else if (i == numGroups - 1)
    //                {
    //                    images1Label0.FormattedText?.Spans.Clear();
    //                    images1Label1.FormattedText?.Spans.Clear();
    //                    images1Label2.FormattedText?.Spans.Clear();
    //                    images1Label3.FormattedText?.Spans.Clear();
    //                    images1Label4.FormattedText?.Spans.Clear();
    //                    images1Label5.FormattedText?.Spans.Clear();
    //                    images1Image0.Source = null;
    //                    images1Image1.Source = null;
    //                    images1Image2.Source = null;
    //                    images1Image3.Source = null;
    //                    images1Image4.Source = null;
    //                    images1Image5.Source = null;

    //                    images1Label0.FormattedText = formattedImageNames[0];
    //                    images1Image0.Source = ImageSource.FromFile(extraImages1[0]);
    //                    images1Label1.FormattedText = formattedImageNames[1];
    //                    images1Image1.Source = ImageSource.FromFile(extraImages1[1]);
    //                    images1Label2.FormattedText = formattedImageNames[2];
    //                    images1Image2.Source = ImageSource.FromFile(extraImages1[2]);
    //                    images1Label3.FormattedText = formattedImageNames[3];
    //                    images1Image3.Source = ImageSource.FromFile(extraImages1[3]);
    //                    images1Label4.FormattedText = formattedImageNames[4];
    //                    images1Image4.Source = ImageSource.FromFile(extraImages1[4]);
    //                    images1Label5.FormattedText = formattedImageNames[5];
    //                    images1Image5.Source = ImageSource.FromFile(extraImages1[5]);

    //                    Console.WriteLine("SETTING IMAGES:   0");
    //                    images1Index = 0;

    //                    break;
    //                }

    //                imageIndex += 6;
    //            }
    //        }
    //    }
    //}

    void AddAdditionalandSimilar()
    {
        if (spGr.AdditionalComments != " " && !string.IsNullOrEmpty(spGr.AdditionalComments))
        {
            label5.Text = ReferenceTabGroup.Additional_comments;
            label5.FontSize = FSize + 5.0;
            label5.FontFamily = "OpenSansBold";

            label6.FormattedText?.Spans.Clear();
            //if (language == 0) label6.FormattedText = FormatString(spGr.AdditionalComments);
            //else if (spGr.AdditionalCommentsES != null) label6.FormattedText = FormatString(spGr.AdditionalCommentsES);
            label6.FormattedText = FormatString(spGr.AdditionalComments, "Add. Comments");
        }
        else
        {
            aGrid.RowDefinitions[5].Height = 0;
            aGrid.RowDefinitions[6].Height = 0;
        }

        if (spGr.SimilarWoods != " " && !string.IsNullOrEmpty(spGr.SimilarWoods))
        {
            label7.Text = ReferenceTabGroup.Similar_woods;
            label7.FontSize = FSize + 5.0;
            label7.FontFamily = "OpenSansBold";

            label8.FormattedText?.Spans.Clear();
            //if (language == 0) label8.FormattedText = FormatString(spGr.SimilarWoods);
            //else if (spGr.SimilarWoodsES != null) label8.FormattedText = FormatString(spGr.SimilarWoodsES);
            //label8.FormattedText = FormatString(spGr.SimilarWoods);
            label8.FormattedText = FormatSimilar(spGr.SimilarWoods, spGr.SimilarFileNames);
        }
        else
        {
            aGrid.RowDefinitions[7].Height = 0;
            aGrid.RowDefinitions[8].Height = 0;
        }

        return;
    }

    void AddCharacters()
    {
        label3.Text = ReferenceTabGroup.Summary_of_characters;
        label3.FontSize = FSize + 5.0;
        label3.FontFamily = "OpenSansBold";


        if (spGr.Transverse != " " || spGr.Porosity != " " || spGr.Vessels != " " ||
            spGr.Rays != " " || spGr.Parenchyma != " ")
        {
            dotTransverse.Text = ReferenceTabGroup.dot;
            dotTransverse.FontSize = FSize + 3.0;
            dotTransverse.FontFamily = "OpenSansBold";

            labelTransverse.Text = ReferenceTabGroup.Transverse;
            labelTransverse.FontSize = FSize + 2.0;
            labelTransverse.FontFamily = "OpenSansSemibold";

            if (spGr.Transverse != " " && spGr.Transverse != null)
            {
                labelTransverseDesc.FormattedText?.Spans.Clear();
                if (spGr.Transverse.Contains('['))
                {
                    if (language == 0) labelTransverseDesc.FormattedText = FormatString(spGr.Transverse, "Transverse");
                    else if (spGr.TransverseES != null) labelTransverseDesc.FormattedText = FormatString(spGr.TransverseES);
                }
                else
                {
                    if (language == 0) labelTransverseDesc.Text = spGr.Transverse;
                    else if (spGr.TransverseES != null) labelTransverseDesc.Text = spGr.TransverseES;
                }
                labelTransverseDesc.FontSize = FSize;
                labelTransverseDesc.Padding = new Thickness(0, 0, 0, 5);
            }
            else
            {
                labelTransverseDesc.FormattedText?.Spans.Clear();
                FormattedString fStr = new();
                Span span = new()
                {
                    FontSize = FSize,
                    FontFamily = "OpenSansSemibold"
                };

                if (spGr.Porosity != " " && spGr.Porosity != null)
                {
                    span.Text = ReferenceTabGroup.Porosity_;
                    fStr.Spans.Add(span);
                    if (spGr.Porosity.Contains('['))
                    {
                        List<Span> spans = new();
                        if (language == 0) spans = FormatStringReturnSpans(spGr.Porosity);
                        else if (spGr.PorosityES != null) spans = FormatStringReturnSpans(spGr.PorosityES);
                        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
                    }
                    else
                    {
                        span = new()
                        {
                            FontSize = FSize,
                            FontFamily = "OpenSansRegular"
                        };
                        if (language == 0) span.Text = spGr.Porosity + "\n";
                        else span.Text = spGr.PorosityES + "\n";
                        fStr.Spans.Add(span);
                    }
                    span = new()
                    {
                        FontSize = FSize,
                        FontFamily = "OpenSansSemibold"
                    };
                }

                if (spGr.Vessels != " " && spGr.Vessels != null)
                {
                    span.Text = ReferenceTabGroup.Vessels_;
                    fStr.Spans.Add(span);
                    if (spGr.Vessels.Contains('['))
                    {
                        List<Span> spans = new();
                        if (language == 0) spans = FormatStringReturnSpans(spGr.Vessels);
                        else if (spGr.VesselsES != null) spans = FormatStringReturnSpans(spGr.VesselsES);
                        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
                    }
                    else
                    {
                        span = new()
                        {
                            FontSize = FSize,
                            FontFamily = "OpenSansRegular"
                        };
                        if (language == 0) span.Text = spGr.Vessels + "\n";
                        else span.Text = spGr.VesselsES + "\n";
                        fStr.Spans.Add(span);
                    }
                    span = new()
                    {
                        FontSize = FSize,
                        FontFamily = "OpenSansSemibold"
                    };
                }

                if (spGr.Rays != " " && spGr.Rays != null)
                {
                    span.Text = ReferenceTabGroup.Rays_;
                    fStr.Spans.Add(span);
                    if (spGr.Rays.Contains('['))
                    {
                        List<Span> spans = new();
                        if (language == 0) spans = FormatStringReturnSpans(spGr.Rays);
                        else if (spGr.RaysES != null) spans = FormatStringReturnSpans(spGr.RaysES);
                        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
                    }
                    else
                    {
                        span = new()
                        {
                            FontSize = FSize,
                            FontFamily = "OpenSansRegular"
                        };
                        if (language == 0) span.Text = spGr.Rays + "\n";
                        else span.Text = spGr.RaysES + "\n";
                        fStr.Spans.Add(span);
                    }
                    span = new()
                    {
                        FontSize = FSize,
                        FontFamily = "OpenSansSemibold"
                    };
                }

                if (spGr.Parenchyma != " " && spGr.Parenchyma != null)
                {
                    span.Text = ReferenceTabGroup.Parenchyma_;
                    fStr.Spans.Add(span);
                    if (spGr.Parenchyma.Contains('['))
                    {
                        List<Span> spans = new();
                        if (language == 0) spans = FormatStringReturnSpans(spGr.Parenchyma);
                        else if (spGr.ParenchymaES != null) spans = FormatStringReturnSpans(spGr.ParenchymaES);
                        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
                    }
                    else
                    {
                        span = new()
                        {
                            FontSize = FSize,
                            FontFamily = "OpenSansRegular"
                        };
                        if (language == 0) span.Text = spGr.Parenchyma + "\n";
                        else span.Text = spGr.ParenchymaES + "\n";
                        fStr.Spans.Add(span);
                    }
                }

                labelTransverseDesc.FormattedText = fStr;
                labelTransverseDesc.Padding = new Thickness(0, 0, 0, -10);
            }
        }

        if (spGr.Tangential != " " && spGr.Tangential != null)
        {
            dotTangential.Text = ReferenceTabGroup.dot;
            dotTangential.FontSize = FSize + 3.0;
            dotTangential.FontFamily = "OpenSansBold";

            labelTangential.Text = ReferenceTabGroup.Tangential;
            labelTangential.FontSize = FSize + 2.0;
            labelTangential.FontFamily = "OpenSansSemibold";

            labelTangentialDesc.FormattedText?.Spans.Clear();
            if (language == 0) labelTangentialDesc.FormattedText = FormatString(spGr.Tangential, "Tangential");
            else if (spGr.TangentialES != null) labelTangentialDesc.FormattedText = FormatString(spGr.TangentialES);
        }
        else
        {
            grid4.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Absolute);
            grid4.RowDefinitions[3].Height = new GridLength(0, GridUnitType.Absolute);
        }

        if (spGr.OtherCharacters != " " && spGr.OtherCharacters != null )
        {
            dotOther.Text = ReferenceTabGroup.dot;
            dotOther.FontSize = FSize + 3.0;
            dotOther.FontFamily = "OpenSansBold";

            labelOther.Text = ReferenceTabGroup.Other_characters;
            labelOther.FontSize = FSize + 2.0;
            labelOther.FontFamily = "OpenSansSemibold";

            labelOtherDesc.FormattedText?.Spans.Clear();
            if (language == 0) labelOtherDesc.FormattedText = FormatString(spGr.OtherCharacters, "Other Characters");
            else if (spGr.OtherCharactersES != null) labelOtherDesc.FormattedText = FormatString(spGr.OtherCharactersES);
        }
        else
        {
            grid4.RowDefinitions[4].Height = new GridLength(0, GridUnitType.Absolute);
            grid4.RowDefinitions[5].Height = new GridLength(0, GridUnitType.Absolute);
        }

        return;
    }

    void AddImages0New()
    {
        List<string> imageNames = new List<string>();
        List<string> fileNames = new List<string>();
        fileNames = spGr.FileNames.Split("* ").ToList();
        imageNames = App.DB.GetImageNamesByFileNames(fileNames);
        List<FormattedString> formattedImageNames = new List<FormattedString>();
        for (int i = 0; i < imageNames.Count; i++)
        {
            //formattedImageNames.Add(FormatStringRemoveParentheses(imageNames[i]));
            formattedImageNames.Add(FormatString(imageNames[i], "imageName " + i.ToString()));
        }

        images0Label0.FormattedText?.Spans.Clear();
        images0Label0.FormattedText = formattedImageNames[0];
        images0Image0.Source = ImageSource.FromFile(fileNames[0]);
        //if (dPlat == DevicePlatform.Android) grid2.Padding = new Thickness(-5, 5, -5, -15);
        //else grid2.Padding = new Thickness(-5, 5, -5, 10);

        if (fileNames.Count > 1)
        {
            images0Label1.FormattedText?.Spans.Clear();
            images0Label1.FormattedText = formattedImageNames[1];
            images0Image1.Source = ImageSource.FromFile(fileNames[1]);
        }
        if (fileNames.Count > 2)
        {
            images0Label2.FormattedText?.Spans.Clear();
            images0Label2.FormattedText = formattedImageNames[2];
            images0Image2.Source = ImageSource.FromFile(fileNames[2]);
        }
        if (fileNames.Count > 3)
        {
            grid2rd2.Height = new GridLength(1, GridUnitType.Auto);
            grid2rd3.Height = new GridLength(1, GridUnitType.Auto);
            images0Label3.FormattedText?.Spans.Clear();
            images0Label3.FormattedText = formattedImageNames[3];
            images0Image3.Source = ImageSource.FromFile(fileNames[3]);
            //grid2.Padding = new Thickness(-5, 5, -5, 10);
        }
        if (fileNames.Count > 4)
        {
            images0Label4.FormattedText?.Spans.Clear();
            images0Label4.FormattedText = formattedImageNames[4];
            images0Image4.Source = ImageSource.FromFile(fileNames[4]);
        }
        if (fileNames.Count > 5)
        {
            images0Label5.FormattedText?.Spans.Clear();
            images0Label5.FormattedText = formattedImageNames[5];
            images0Image5.Source = ImageSource.FromFile(fileNames[5]);
        }
        if (fileNames.Count > 6)
        {
            grid2rd4.Height = new GridLength(1, GridUnitType.Auto);
            images0NavGrid.IsVisible = true;

            extraImages0 = fileNames;
            images0Index = 0;
        }
    }
    void OnImages0LeftNavClicked(object? sender, EventArgs args)
    {
        if (extraImages0.Count > 0)
        {
            if (extraImages0.Count > 6)
            {
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages0);
                List<FormattedString> formattedImageNames = new List<FormattedString>();
                for (int i = 0; i < imageNames.Count; i++)
                {
                    //formattedImageNames.Add(FormatStringRemoveParentheses(imageNames[i]));
                    formattedImageNames.Add(FormatString(imageNames[i], "imageName " + i.ToString()));
                    Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
                }

                int numGroups = imageNames.Count / 6;
                int extra = 0;
                if (imageNames.Count % 6 > 0) extra = 1;
                numGroups += extra;
                int imageIndex = numGroups * 6 - 6;

                for (int i = numGroups-1; i >= 0; i--)
                {
                    if (imageIndex == images0Index && i > 0)
                    {
                        images0Label0.FormattedText?.Spans.Clear();
                        images0Label1.FormattedText?.Spans.Clear();
                        images0Label2.FormattedText?.Spans.Clear();
                        images0Label3.FormattedText?.Spans.Clear();
                        images0Label4.FormattedText?.Spans.Clear();
                        images0Label5.FormattedText?.Spans.Clear();
                        images0Image0.Source = null;
                        images0Image1.Source = null;
                        images0Image2.Source = null;
                        images0Image3.Source = null;
                        images0Image4.Source = null;
                        images0Image5.Source = null;
                        for (int j = imageIndex - 6; j < formattedImageNames.Count; j++)
                        {
                            if (j >= imageIndex) break;

                            if (j == imageIndex - 6)
                            {
                                images0Label0.FormattedText = formattedImageNames[j];
                                images0Image0.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex - 5)
                            {
                                images0Label1.FormattedText = formattedImageNames[j];
                                images0Image1.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex - 4)
                            {
                                images0Label2.FormattedText = formattedImageNames[j];
                                images0Image2.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex - 3)
                            {
                                images0Label3.FormattedText = formattedImageNames[j];
                                images0Image3.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex - 2)
                            {
                                images0Label4.FormattedText = formattedImageNames[j];
                                images0Image4.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex - 1)
                            {
                                images0Label5.FormattedText = formattedImageNames[j];
                                images0Image5.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                        }
                        Console.WriteLine("SETTING IMAGES:   1");
                        images0Index = imageIndex - 6;

                        break;
                    }
                    else if (images0Index == 0)
                    {
                        images0Label0.FormattedText?.Spans.Clear();
                        images0Label1.FormattedText?.Spans.Clear();
                        images0Label2.FormattedText?.Spans.Clear();
                        images0Label3.FormattedText?.Spans.Clear();
                        images0Label4.FormattedText?.Spans.Clear();
                        images0Label5.FormattedText?.Spans.Clear();
                        images0Image0.Source = null;
                        images0Image1.Source = null;
                        images0Image2.Source = null;
                        images0Image3.Source = null;
                        images0Image4.Source = null;
                        images0Image5.Source = null;

                        imageIndex = numGroups * 6 - 6;

                        for (int j = imageIndex; j < formattedImageNames.Count; j++)
                        {
                            if (j >= imageIndex + 6) break;

                            if (j == imageIndex)
                            {
                                images0Label0.FormattedText = formattedImageNames[j];
                                images0Image0.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 1)
                            {
                                images0Label1.FormattedText = formattedImageNames[j];
                                images0Image1.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 2)
                            {
                                images0Label2.FormattedText = formattedImageNames[j];
                                images0Image2.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 3)
                            {
                                images0Label3.FormattedText = formattedImageNames[j];
                                images0Image3.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 4)
                            {
                                images0Label4.FormattedText = formattedImageNames[j];
                                images0Image4.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 5)
                            {
                                images0Label5.FormattedText = formattedImageNames[j];
                                images0Image5.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                        }


                        Console.WriteLine("SETTING IMAGES:   0");
                        images0Index = imageIndex;

                        break;
                    }

                    imageIndex -= 6;
                }
            }
        }
    }
    void OnImages0RightNavClicked(object? sender, EventArgs args)
    {
        if (extraImages0.Count > 0)
        {
            if (extraImages0.Count > 6)
            {
                List<string> imageNames = App.DB.GetImageNamesByFileNames(extraImages0);
                List<FormattedString> formattedImageNames = new List<FormattedString>();
                for (int i = 0; i < imageNames.Count; i++)
                {
                    //formattedImageNames.Add(FormatStringRemoveParentheses(imageNames[i]));
                    formattedImageNames.Add(FormatString(imageNames[i], "imageName " + i.ToString()));
                    Console.WriteLine("FORMATTING IMAGE LABEL:   " + i.ToString());
                }

                int numGroups = imageNames.Count / 6;
                int extra = 0;
                if (imageNames.Count % 6 > 0) extra = 1;
                numGroups += extra;
                int imageIndex = 0;

                for (int i = 0; i < numGroups; i++)
                {
                    if (imageIndex == images0Index && i < numGroups - 1)
                    {
                        images0Label0.FormattedText?.Spans.Clear();
                        images0Label1.FormattedText?.Spans.Clear();
                        images0Label2.FormattedText?.Spans.Clear();
                        images0Label3.FormattedText?.Spans.Clear();
                        images0Label4.FormattedText?.Spans.Clear();
                        images0Label5.FormattedText?.Spans.Clear();
                        images0Image0.Source = null;
                        images0Image1.Source = null;
                        images0Image2.Source = null;
                        images0Image3.Source = null;
                        images0Image4.Source = null;
                        images0Image5.Source = null;
                        for (int j = imageIndex + 6; j < formattedImageNames.Count; j++)
                        {
                            if (j >= imageIndex + 12) break;

                            if (j == imageIndex + 6)
                            {
                                images0Label0.FormattedText = formattedImageNames[j];
                                images0Image0.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 7)
                            {
                                images0Label1.FormattedText = formattedImageNames[j];
                                images0Image1.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 8)
                            {
                                images0Label2.FormattedText = formattedImageNames[j];
                                images0Image2.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 9)
                            {
                                images0Label3.FormattedText = formattedImageNames[j];
                                images0Image3.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 10)
                            {
                                images0Label4.FormattedText = formattedImageNames[j];
                                images0Image4.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                            else if (j == imageIndex + 11)
                            {
                                images0Label5.FormattedText = formattedImageNames[j];
                                images0Image5.Source = ImageSource.FromFile(extraImages0[j]);
                            }
                        }

                        Console.WriteLine("SETTING IMAGES:   1");
                        images0Index = imageIndex + 6;

                        break;
                    }
                    else if (i == numGroups - 1)
                    {
                        images0Label0.FormattedText?.Spans.Clear();
                        images0Label1.FormattedText?.Spans.Clear();
                        images0Label2.FormattedText?.Spans.Clear();
                        images0Label3.FormattedText?.Spans.Clear();
                        images0Label4.FormattedText?.Spans.Clear();
                        images0Label5.FormattedText?.Spans.Clear();
                        images0Image0.Source = null;
                        images0Image1.Source = null;
                        images0Image2.Source = null;
                        images0Image3.Source = null;
                        images0Image4.Source = null;
                        images0Image5.Source = null;

                        images0Label0.FormattedText = formattedImageNames[0];
                        images0Image0.Source = ImageSource.FromFile(extraImages0[0]);
                        images0Label1.FormattedText = formattedImageNames[1];
                        images0Image1.Source = ImageSource.FromFile(extraImages0[1]);
                        images0Label2.FormattedText = formattedImageNames[2];
                        images0Image2.Source = ImageSource.FromFile(extraImages0[2]);
                        images0Label3.FormattedText = formattedImageNames[3];
                        images0Image3.Source = ImageSource.FromFile(extraImages0[3]);
                        images0Label4.FormattedText = formattedImageNames[4];
                        images0Image4.Source = ImageSource.FromFile(extraImages0[4]);
                        images0Label5.FormattedText = formattedImageNames[5];
                        images0Image5.Source = ImageSource.FromFile(extraImages0[5]);

                        Console.WriteLine("SETTING IMAGES:   0");
                        images0Index = 0;

                        break;
                    }

                    imageIndex += 6;
                }
            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (!string.IsNullOrEmpty(editor10.Text)) Preferences.Set(notesKey, editor10.Text);
    }

    public ICommand TapCommand => new Command<int>(async (sGroupID) => await Shell.Current.Navigation.PushAsync(new ReferenceGroupPageNew(App.DB.GetSpeciesGroupByID(sGroupID), 2)));

    public Command BackCommand => new Command(async () =>
    {
        if (IDReturn == 2) await Shell.Current.Navigation.PopAsync(false);
        else await Shell.Current.Navigation.PopToRootAsync(false);
        if (IDReturn == 1) await Shell.Current.GoToAsync("//ID");
    });

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        //Shell.Current.Navigation.PopToRootAsync(false);
        if (IDReturn == 2) Shell.Current.Navigation.PopAsync(false);
        else Shell.Current.Navigation.PopToRootAsync(false);
        if (IDReturn == 1) Shell.Current.GoToAsync("//ID");
        return true;
    }
}