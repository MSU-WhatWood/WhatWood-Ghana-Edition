using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhanaWoods.Resources.Helpers;
using GhanaWoods.Database;
using Microsoft.Maui.Storage;

namespace GhanaWoods
{
    internal static class TabAssets
    {
        //Local list of some classes from Entity Framework Database
        public static List<KeyEntry> keyEntries = new List<KeyEntry>();
        public static List<KeyHistory> keyHistories = new List<KeyHistory>();
        public static List<SpeciesGroup> speciesGroups = new List<SpeciesGroup>();

        public static double FontS = Preferences.Get("FontSize", 16.0);

        //Local lists of UI elements for Key and Species pages
        public static List<Grid> localGrids = new List<Grid>();
        public static List<BoxView> localBoxes = new List<BoxView>();
        public static List<Label> localLabels = new List<Label>();
        public static List<Label> localLabels1 = new List<Label>();
        public static List<Label> localLabelsNum = new List<Label>();
        public static List<Label> localLabelsNum1 = new List<Label>();
        public static List<ImageButton> localButtons = new List<ImageButton>();
        public static List<ImageButton> localButtons1 = new List<ImageButton>();

        public static List<Grid> localGridsRef = new List<Grid>();
        public static List<ImageButton> localButtonsRef = new List<ImageButton>();
        public static List<Label> localLabelsRef = new List<Label>();
        public static List<Label> localLabelsRef1 = new List<Label>();
    }
}
