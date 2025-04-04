using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using AuthenticationServices;
using GhanaWoods.Resources.Helpers;
using GhanaWoods.Resources.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Storage;
//using static Kotlin.Collections.Builders.MapBuilder;

namespace GhanaWoods.Database
{
    internal class AddDBdata
    {
        public static List<SpeciesTbl> localSpeciesTbls = new List<SpeciesTbl>();
        public static List<SpeciesGroup> localSpeciesGroups = new List<SpeciesGroup>();
        public static async void AddDataToDB(ModelBuilder modelBuilder)
        {
            //KeyEntriesClassOld kEClass = new KeyEntriesClassOld();
            //List<KeyEntriesOld> entries = new List<KeyEntriesOld>();
            //List<KeyHistoriesOld> keyHistories = new List<KeyHistoriesOld>();
            //entries = await kEClass.CreateKeyEntries();
            //keyHistories = await kEClass.CreateKeyHistories();
            List<KeyEntry> entriesList = new List<KeyEntry>();
            List<KeyHistory> keyHistoriesList = new List<KeyHistory>();

            //SpeciesGroupsClassOld sGClass = new SpeciesGroupsClassOld();
            //List<SpeciesGroupsOld> speciesGroups = new List<SpeciesGroupsOld>();
            //speciesGroups = await sGClass.CreateSpeciesGroups();
            List<SpeciesGroup> groupsList = new List<SpeciesGroup>();
            List<SpeciesTbl> speciesList = new List<SpeciesTbl>();
            List<ImageTbl> imageTbls = new List<ImageTbl>();
            //ImageTbl[] imageTbls = new ImageTbl[929];
            List<EntryImagesTbl> entryImagesList = new List<EntryImagesTbl>();

            //List<SpeciesTbl> localSpeciesTbls = new List<SpeciesTbl>();

            //for (int i = 0; i < entries.Count; i += 2)
            //{
            //    entriesList.Add(new KeyEntry { KeyEntryID = entries[i].DecisionNum, DecisionNum = entries[i].DecisionNum, Text0 = entries[i].Text, Text0ES = "Testpañol", TargetID0 = entries[i].Target, 
            //        Text1 = entries[i + 1].Text, Text1ES = "Testpañol1", TargetID1 = entries[i+1].Target });
            //    //KeyEntries.Add(keyEntry);
            //}


            entriesList = await CreateKeyEntries();


            //for (int i = 0; i < keyHistories.Count; i++)
            //{
            //    keyHistoriesList.Add(new KeyHistory
            //    {
            //        KeyHistoryID = keyHistories[i].IDNum,
            //        PrevDecisions = string.Join(", ", keyHistories[i].prev),
            //        GreyDecisions = string.Join(", ", keyHistories[i].grey),
            //        KeyEntryID = keyHistories[i].IDNum
            //    });
            //}


            keyHistoriesList = await CreateKeyHistories();


            groupsList = await CreateSpeciesGroups();


            //for (int i = 0; i < speciesGroups.Count; i++)
            //{
            //    groupsList.Add(new SpeciesGroup
            //    {

            //    });
            //}


            speciesList = await CreateSpeciesTbls();


            imageTbls = await CreateImageTbls();


            entryImagesList = await CreateEntryImagesTbls();


            modelBuilder.Entity<KeyEntry>(entity =>
            {
                entity.HasData(
                    //new KeyEntry { KeyEntryID = 1, DecisionNum = 1, Text0 = "Test", Text0ES = "Testpañol", Text1 = "Test1", Text1ES = "Testpañol1" },
                    //new KeyEntry { KeyEntryID = 2, DecisionNum = 2, Text0 = "Test", Text0ES = "Testpañol", Text1 = "Test1", Text1ES = "Testpañol1" },
                    //new KeyEntry { KeyEntryID = 3, DecisionNum = 3, Text0 = "Test", Text0ES = "Testpañol", Text1 = "Test1", Text1ES = "Testpañol1" }
                    entriesList
                );
            });
            modelBuilder.Entity<KeyHistory>(entity =>
            {
                entity.HasData(
                    //new KeyHistory { KeyHistoryID = 1, PrevDecisions = "", GreyDecisions = "", KeyEntryID = 1 },
                    //new KeyHistory { KeyHistoryID = 2, PrevDecisions = "1a", GreyDecisions = "", KeyEntryID = 2 },
                    //new KeyHistory { KeyHistoryID = 3, PrevDecisions = "1a, 2b", GreyDecisions = "", KeyEntryID = 3 }


                    //new KeyHistory { KeyHistoryID = 4, PrevDecisions = "1a, 2b, 3b", GreyDecisions = "", KeyEntryID = 4 },
                    //new KeyHistory { KeyHistoryID = 5, PrevDecisions = "1a, 2b, 3b, 4b", GreyDecisions = "", KeyEntryID = 5 },
                    //new KeyHistory { KeyHistoryID = 6, PrevDecisions = "1a, 2b, 3b, 4b, 5b", GreyDecisions = "", KeyEntryID = 6 },
                    //new KeyHistory { KeyHistoryID = 7, PrevDecisions = "1b", GreyDecisions = "2, 3, 4, 5, 6", KeyEntryID = 7 },
                    //new KeyHistory { KeyHistoryID = 8, PrevDecisions = "1b, 7a", GreyDecisions = "2, 3, 4, 5, 6", KeyEntryID = 8 },
                    //new KeyHistory { KeyHistoryID = 9, PrevDecisions = "1b, 7a, 8a", GreyDecisions = "2, 3, 4, 5, 6", KeyEntryID = 9 },
                    //new KeyHistory { KeyHistoryID = 10, PrevDecisions = "1b, 7a, 8b", GreyDecisions = "2, 3, 4, 5, 6, 9", KeyEntryID = 10 },
                    //new KeyHistory { KeyHistoryID = 11, PrevDecisions = "1b, 7a, 8b, 10b", GreyDecisions = "2, 3, 4, 5, 6, 9", KeyEntryID = 11 },
                    //new KeyHistory { KeyHistoryID = 12, PrevDecisions = "1b, 7a, 8b, 10b, 11b", GreyDecisions = "2, 3, 4, 5, 6, 9", KeyEntryID = 12 }

                    keyHistoriesList
                );
            });
            modelBuilder.Entity<SpeciesGroup>(entity =>
            {
                entity.HasData(
                    groupsList
                );
            });
            modelBuilder.Entity<SpeciesTbl>(entity =>
            {
                entity.HasData(
                    speciesList
                );
            });
            modelBuilder.Entity<ImageTbl>(entity =>
            {
                entity.HasData(
                    imageTbls
                );
            });
            modelBuilder.Entity<EntryImagesTbl>(entity =>
            {
                entity.HasData(
                    entryImagesList
                );
            });



            //App.DB.SaveChanges();


            //modelBuilder.Entity<KeyEntry>(entity =>
            //{
            //    entity.HasData(
            //        new KeyEntry()
            //    );
            //});
        }


        private static async Task<List<KeyEntry>> CreateKeyEntries()
        {
            //List<KeyEntriesOld> keyEntries = new List<KeyEntriesOld>();

            //KeyEntriesOld entry = new();

            List<KeyEntry> keyEntries = new List<KeyEntry>();

            KeyEntry entry = new();

            //using var stream = await FileSystem.OpenAppPackageFileAsync("KeyEntriesData.txt");
            //using var stream = await FileSystem.OpenAppPackageFileAsync("KeyEntriesDataAdjCombined.txt");
            using var stream = await FileSystem.OpenAppPackageFileAsync("KeyEntriesData.txt");
            using var reader = new StreamReader(stream);

            //int IDNum = 0, Decision = 0, target0 = 0, target1 = 0;
            //string text0 = string.Empty, textES0 = string.Empty, targetIDs0 = string.Empty, targetNames0 = string.Empty, targetNamesES0 = string.Empty,
            //    text1 = string.Empty, textES1 = string.Empty, targetIDs1 = string.Empty, targetNames1 = string.Empty, targetNamesES1 = string.Empty;
            string emptyStr = string.Empty;

            int count = 2;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (count <= 0 || line == string.Empty || line == null || line == " ")
                {
                    count = 2;

                    //IDNum = 0;
                    //Decision = 0;
                    //target0 = 0;
                    //target1 = 0;
                    //text0 = string.Empty;
                    //textES0 = string.Empty;
                    //targetIDs0 = string.Empty;
                    //targetNames0 = string.Empty;
                    //targetNamesES0 = string.Empty;
                    //text1 = string.Empty;
                    //textES1 = string.Empty;
                    //targetIDs1 = string.Empty;
                    //targetNames1 = string.Empty;
                    //targetNamesES1 = string.Empty;

                    entry = new KeyEntry();
                }
                else if (count == 2)
                {
                    //List<string> strs = line.Split("+  ").ToList<string>();
                    List<string> strs = line.Split("+  ").ToList();

                    //IDNum = int.Parse(strs[1]);
                    entry.KeyEntryID = int.Parse(strs[1]);

                    //Decision = int.Parse(strs[1]);
                    entry.DecisionNum = int.Parse(strs[1]);

                    //text0 = strs[2];
                    entry.Text0 = strs[2];

                    //textES0 = strs[3];
                    //entry.Text0ES = strs[3];

                    //target0 = 0;

                    if (strs.Count == 3)
                    //if (strs.Count == 4)
                    {
                        //target0 = 0;
                        entry.TargetID0 = 0;

                        entry.TargetGroupsIDs0 = emptyStr;
                        entry.TargetGroupsNames0 = emptyStr;
                        entry.TargetGroupsNames0ES = emptyStr;
                    }
                    else if (strs.Count == 4)
                    //else if (strs.Count == 5)
                    {
                        //target0 = int.Parse(strs[4]);
                        //entry.TargetID0 = int.Parse(strs[4]);
                        entry.TargetID0 = int.Parse(strs[3]);

                        entry.TargetGroupsIDs0 = emptyStr;
                        entry.TargetGroupsNames0 = emptyStr;
                        entry.TargetGroupsNames0ES = emptyStr;
                    }
                    else if (strs.Count == 5)
                    //else if (strs.Count == 6)
                    {
                        //target0 = 0;
                        entry.TargetID0 = 0;

                        //if (strs[4].IndexOf(',') > -1)
                        if (strs[3].IndexOf(',') > -1)
                        {
                            //string[] targetsGrouped = strs[4].Split(",  ");
                            string[] targetsGrouped = strs[3].Split(",  ");

                            //targetNames0 = targetsGrouped[0];
                            //targetNamesES0 = targetsGrouped[1];
                            entry.TargetGroupsNames0 = targetsGrouped[0];
                            entry.TargetGroupsNames0ES = targetsGrouped[1];
                        }
                        //else targets.Add(strs[4]);
                        //else targetNames0 = strs[4];
                        else
                        {
                            //entry.TargetGroupsNames0 = strs[4];
                            //entry.TargetGroupsNames0ES = strs[4];
                            entry.TargetGroupsNames0 = strs[3];
                            entry.TargetGroupsNames0ES = strs[3];
                        }

                        //targetsIDs.Add(int.Parse(strs[5]));
                        //targetIDs0 = strs[5];
                        //entry.TargetGroupsIDs0 = strs[5];
                        entry.TargetGroupsIDs0 = strs[4];
                    }
                    else if (strs.Count > 5)
                    //else if (strs.Count > 6)
                    {
                        //List<string> targets = new List<string>();
                        //List<string> targetsES = new List<string>();
                        //List<string> targetsIDs = new List<string>();

                        //target0 = 0;
                        entry.TargetID0 = 0;

                        //for (int i = 4; i < strs.Count; i += 2)
                        for (int i = 3; i < strs.Count; i += 2)
                        {
                            //FormattedString fStr = FormatString(strs[i]);
                            //string str2 = strs[i];
                            //targets.Add(fStr);

                            //targets.Add(strs[i]);
                            if (strs[i].IndexOf(',') > -1)
                            {
                                string[] targetsGrouped = strs[i].Split(",  ");
                                //targets.Add(targetsGrouped[0]);
                                //targetsES.Add(targetsGrouped[1]);
                                //if (i == 4)
                                if (i == 3)
                                {
                                    entry.TargetGroupsNames0 = targetsGrouped[0];
                                    entry.TargetGroupsNames0ES = targetsGrouped[1];
                                }
                                else
                                {
                                    entry.TargetGroupsNames0 += "+  " + targetsGrouped[0];
                                    entry.TargetGroupsNames0ES += "+  " + targetsGrouped[1];
                                }
                            }
                            else
                            {
                                //targets.Add(strs[i]);
                                //targetsES.Add(strs[i]);
                                //if (i == 4)
                                if (i == 3)
                                {
                                    entry.TargetGroupsNames0 = strs[i];
                                    entry.TargetGroupsNames0ES = strs[i];
                                }
                                else
                                {
                                    entry.TargetGroupsNames0 += "+  " + strs[i];
                                    entry.TargetGroupsNames0ES += "+  " + strs[i];
                                }
                            }

                            //targetsIDs.Add(int.Parse(strs[i + 1]));
                            //targetsIDs.Add(strs[i + 1]);
                            //if (i == 4) entry.TargetGroupsIDs0 = strs[i + 1];
                            if (i == 3) entry.TargetGroupsIDs0 = strs[i + 1];
                            else entry.TargetGroupsIDs0 += "+  " + strs[i + 1];
                        }

                        //targetIDs0 = string.Join("+  ", targetsIDs);
                        //targetNames0 = string.Join("+  ", targets);
                        //if (targetsES.Count > 1) targetNamesES0 = string.Join("+  ", targetsES);
                        //else targetNamesES0 = targetsES[0];

                        //targetNamesES0 = string.Join("+  ", targetsES);
                    }

                    //entry = new KeyEntriesOld(IDNum, DecisionNum, EntryText, target, targets, targetsIDs);
                    //entry = new KeyEntry { KeyEntryID = IDNum, DecisionNum = Decision, Text0};

                    //keyEntries.Add(entry);

                    count--;
                }
                else if (count == 1)
                {
                    List<string> strs = line.Split("+  ").ToList();

                    //IDNum = int.Parse(strs[1]);

                    //Decision = int.Parse(strs[1]);

                    //text1 = strs[2];
                    entry.Text1 = strs[2];

                    //textES1 = strs[3];
                    //entry.Text1ES = strs[3];

                    //target1 = 0;
                    //entry.TargetID1 = 0;


                    //List<FormattedString> targets = new List<FormattedString>();

                    if (strs.Count == 3)
                    {
                        //target1 = 0;
                        entry.TargetID1 = 0;

                        entry.TargetGroupsIDs1 = emptyStr;
                        entry.TargetGroupsNames1 = emptyStr;
                        entry.TargetGroupsNames1ES = emptyStr;
                    }
                    else if (strs.Count == 4)
                    //if (strs.Count == 4)
                    {
                        //target1 = int.Parse(strs[4]);
                        entry.TargetID1 = int.Parse(strs[3]);

                        entry.TargetGroupsIDs1 = emptyStr;
                        entry.TargetGroupsNames1 = emptyStr;
                        entry.TargetGroupsNames1ES = emptyStr;
                    }
                    else if (strs.Count == 5)
                    {
                        //target1 = 0;
                        entry.TargetID1 = 0;

                        //FormattedString fStr2 = FormatString(strs[4]);
                        //string str2 = strs[4];
                        //targets.Add(fStr2);
                        if (strs[3].IndexOf(',') > -1)
                        {
                            string[] targetsGrouped = strs[3].Split(",  ");
                            //targets.Add(targetsGrouped[0]);
                            //targetsES.Add(targetsGrouped[1]);
                            //targetNames1 = targetsGrouped[0];
                            //targetNamesES1 = targetsGrouped[1];
                            entry.TargetGroupsNames1 = targetsGrouped[0];
                            entry.TargetGroupsNames1ES = targetsGrouped[1];
                        }
                        //else targets.Add(strs[4]);
                        //else targetNames1 = strs[4];
                        else
                        {
                            entry.TargetGroupsNames1 = strs[3];
                            entry.TargetGroupsNames1ES = strs[3];
                        }

                        //targetsIDs.Add(int.Parse(strs[5]));
                        //targetIDs1 = strs[5];
                        entry.TargetGroupsIDs1 = strs[4];
                    }
                    else if (strs.Count > 5)
                    {
                        List<string> targets1 = new List<string>();
                        List<string> targetsES1 = new List<string>();
                        //List<int> targetsIDs1 = new List<int>();
                        List<string> targetsIDs1 = new List<string>();

                        //target1 = 0;
                        entry.TargetID1 = 0;

                        for (int i = 3; i < strs.Count; i += 2)
                        {
                            //FormattedString fStr = FormatString(strs[i]);
                            //string str2 = strs[i];
                            //targets.Add(fStr);

                            //targets.Add(strs[i]);
                            if (strs[i].IndexOf(',') > -1)
                            {
                                string[] targetsGrouped = strs[i].Split(",  ");
                                //targets1.Add(targetsGrouped[0]);
                                //targetsES1.Add(targetsGrouped[1]);
                                if (i == 3)
                                {
                                    entry.TargetGroupsNames1 = targetsGrouped[0];
                                    entry.TargetGroupsNames1ES = targetsGrouped[1];
                                }
                                else
                                {
                                    entry.TargetGroupsNames1 += "+  " + strs[i];
                                    entry.TargetGroupsNames1ES += "+  " + strs[i];
                                }
                            }
                            else
                            {
                                //targets1.Add(strs[i]);
                                //targetsES1.Add(strs[i]);
                                if (i == 3)
                                {
                                    entry.TargetGroupsNames1 = strs[i];
                                    entry.TargetGroupsNames1ES = strs[i];
                                }
                                else
                                {
                                    entry.TargetGroupsNames1 += "+  " + strs[i];
                                    entry.TargetGroupsNames1ES += "+  " + strs[i];
                                }
                            }

                            //targetsIDs1.Add(int.Parse(strs[i + 1]));
                            //targetsIDs1.Add(strs[i + 1]);
                            if (i == 3) entry.TargetGroupsIDs1 = strs[i + 1];
                            else entry.TargetGroupsIDs1 += "+  " + strs[i + 1];
                        }

                        //targetIDs1 = string.Join("+  ", targetsIDs1);
                        //targetNames1 = string.Join("+  ", targets1);
                        //targetNamesES1 = string.Join("+  ", targetsES1);
                    }

                    //entry = new KeyEntriesOld(IDNum, DecisionNum, EntryText, target, targets, targetsIDs);
                    //entry = new KeyEntry { KeyEntryID = IDNum, DecisionNum = Decision, Text0};

                    keyEntries.Add(entry);
                    Console.WriteLine("AddDBData     keyEntry created: " + entry.KeyEntryID.ToString());

                    //keyEntries.Add(new KeyEntry
                    //{
                    //    KeyEntryID = IDNum,
                    //    DecisionNum = Decision,
                    //    Text0 = text0,
                    //    Text0ES = textES0,
                    //    TargetID0 = target0,
                    //    TargetGroupsIDs0 = targetIDs0,
                    //    TargetGroupsNames0 = targetNames0,
                    //    TargetGroupsNames0ES = targetNamesES0,
                    //    Text1 = text1,
                    //    Text1ES = textES1,
                    //    TargetID1 = target1,
                    //    TargetGroupsIDs1 = targetIDs1,
                    //    TargetGroupsNames1 = targetNames1,
                    //    TargetGroupsNames1ES = targetNamesES1
                    //});

                    count--;
                }
            }
            //return speciesGroups;
            //KeyEntriesList = keyEntries;

            //return KeyEntriesList;
            return keyEntries;
        }


        private static async Task<List<KeyHistory>> CreateKeyHistories()
        {
            //List<KeyHistoriesOld> keyHistories = new List<KeyHistoriesOld>();

            //KeyHistoriesOld hist = new KeyHistoriesOld();

            List<KeyHistory> keyHistories = new List<KeyHistory>();

            KeyHistory hist = new KeyHistory();

            //using var stream1 = await FileSystem.OpenAppPackageFileAsync("KeyEntriesHistory.txt");
            //using var stream1 = await FileSystem.OpenAppPackageFileAsync("KeyEntriesHistoryAdjusted.txt");
            using var stream1 = await FileSystem.OpenAppPackageFileAsync("KeyEntriesHistory.txt");
            using var reader1 = new StreamReader(stream1);

            //var contents = reader.ReadToEnd();

            //int 

            int count1 = 2;
            string line1;
            while ((line1 = reader1.ReadLine()) != null)
            {
                if (count1 <= 0 || line1 == string.Empty || line1 == null || line1 == " ")
                {
                    count1 = 2;
                    //hist = new KeyHistoriesOld();
                    hist = new KeyHistory();
                }
                else
                {
                    List<string> strs1 = line1.Split("+  ").ToList<string>();

                    //int IDNum1 = int.Parse(strs1[0]);

                    //List<string> values = strs1[1].Split(", ").ToList<string>();

                    //hist.Key = IDNum1;
                    //hist.KeyHistoryID = IDNum1;
                    hist.KeyHistoryID = int.Parse(strs1[0]);
                    hist.KeyEntryID = int.Parse(strs1[0]);

                    if (count1 == 2)
                    {
                        //hist.prev = values;
                        hist.PrevDecisions = strs1[1];
                    }
                    else if (count1 == 1)
                    {
                        //hist.grey = values;
                        hist.GreyDecisions = strs1[1];

                        keyHistories.Add(hist);
                        Console.WriteLine("AddDBData     keyHistory created: " + hist.KeyHistoryID.ToString());

                        //hist = new KeyHistoriesOld();
                        hist = new KeyHistory();
                    }

                    count1--;
                }
            }
            //KeyHistoriesList = keyHistories;

            //return KeyHistoriesList;
            return keyHistories;
        }


        private static async Task<List<SpeciesGroup>> CreateSpeciesGroups()
        {
            List<SpeciesGroup> speciesGroups = new List<SpeciesGroup>();
            localSpeciesGroups = new List<SpeciesGroup>();

            SpeciesGroup group = new SpeciesGroup();

            //using var stream = await FileSystem.OpenAppPackageFileAsync("SpeciesGroupsData.txt");
            using var stream = await FileSystem.OpenAppPackageFileAsync("SpeciesPagesData.txt");
            //using var stream = await FileSystem.OpenAppPackageFileAsync("SpeciesGroupsDataOld_Copy.txt");
            using var reader = new StreamReader(stream);

            //using var streamES = await FileSystem.OpenAppPackageFileAsync("SpeciesGroupsDataES.txt");
            //using var readerES = new StreamReader(streamES);

            //var contents = reader.ReadToEnd();

            string line;
            //string lineES;
            while ((line = reader.ReadLine()) != null)
            {
                //lineES = readerES.ReadLine();
                //string line = reader.ReadLine();
                string[] strs = line.Split("+  ");
                //string[] strsES = lineES.Split("+  ");

                int IDNum = int.Parse(strs[0]);

                bool sppBool = true;

                if (strs.Count() == 18)
                {
                    string sName = " ";
                    if (strs[2] == "true")
                    {
                        sppBool = true;
                        sName = strs[4];
                    }
                    //else if (strs[1].Contains("spp.") || IDNum == 79 || IDNum == 121)
                    else if (strs[1].Contains("spp."))
                    {
                        sppBool = false;
                        sName = strs[4];
                    }
                    else if (strs[2] == "false")
                    {
                        sppBool = false;
                        //sName = strs[4];
                    }
                    if (!string.IsNullOrEmpty(strs[4])) sName = strs[4];

                    //FormattedString fName = new();
                    string fName = string.Empty;

                    //fName = FormatTitle(strs[1], sppBool);
                    //if (sppBool) fName = strs[1] + " spp.";
                    //else fName = strs[1];
                    fName = strs[1];
                    //if (!string.IsNullOrEmpty(strs[5]) && strs[5] != " ") fName += Environment.NewLine + strs[5];

                    //List<string> lineSpecies = strs[5].Split("* ").ToList<string>();
                    //List<string> lineFiles = strs[6].Split("* ").ToList<string>();
                    string lineSpecies = strs[5];
                    string lineFiles = strs[6];

                    //FormattedString fStr14 = FormatString(strs[14]);
                    //FormattedString fStr15 = FormatString(strs[15]);
                    string fStr14 = strs[14];
                    string fStr15 = strs[15];

                    //List<string> lineSimSpecies = strs[16].Split("* ").ToList<string>();
                    //List<string> lineSimFiles = strs[17].Split("* ").ToList<string>();
                    string lineSimSpecies = strs[16];
                    string lineSimFiles = strs[17];

                    //spec = new SpeciesGroupsOld(IDNum, fName, sppBool, strs[3], strs[4], lineSpecies, lineFiles,
                    //    strs[7], strs[8], strs[9], strs[10], strs[11], strs[12], strs[13], fStr14, fStr15,
                    //    lineSimSpecies, lineSimFiles);
                    group = new SpeciesGroup
                    {
                        SpeciesGroupID = IDNum,
                        Name = fName,
                        Spp = sppBool,
                        Family = strs[3],
                        //Species = strs[4],
                        Species = sName,
                        //ImageNames = strs[5],
                        TransverseES = strs[5],
                        NameES = strs[6],
                        FileNames = strs[7],
                        Transverse = strs[8],
                        Porosity = strs[9],
                        Vessels = strs[10],
                        Rays = strs[11],
                        Parenchyma = strs[12],
                        Tangential = strs[13],
                        OtherCharacters = strs[14],
                        AdditionalComments = strs[15],
                        SimilarWoods = strs[16],
                        //SimilarImageNames = strs[16],
                        SimilarFileNames = strs[17]
                    };
                    //group = new SpeciesGroup
                    //{
                    //    SpeciesGroupID = IDNum,
                    //    Name = fName,
                    //    NameES = strsES[1],
                    //    Spp = sppBool,
                    //    Family = strs[3],
                    //    //Species = strs[4],
                    //    Species = sName,
                    //    ImageNames = strs[5],
                    //    FileNames = strs[6],
                    //    Transverse = strs[7],
                    //    TransverseES = strsES[5],
                    //    Porosity = strs[8],
                    //    PorosityES = strsES[6],
                    //    Vessels = strs[9],
                    //    VesselsES = strsES[7],
                    //    Rays = strs[10],
                    //    RaysES = strsES[8],
                    //    Parenchyma = strs[11],
                    //    ParenchymaES = strsES[9],
                    //    Tangential = strs[12],
                    //    TangentialES = strsES[10],
                    //    OtherCharacters = strs[13],
                    //    OtherCharactersES = strsES[11],
                    //    AdditionalComments = strs[14],
                    //    AdditionalCommentsES = strsES[12],
                    //    SimilarWoods = strs[15],
                    //    SimilarWoodsES = strsES[13],
                    //    SimilarImageNames = strs[16],
                    //    SimilarFileNames = strs[17]
                    //};
                }
                else if (strs.Count() == 19)
                {
                    string sName = " ";
                    if (strs[2] == "true")
                    {
                        sppBool = true;
                        sName = strs[4];
                    }
                    //else if (strs[1].Contains("spp.") || IDNum == 79 || IDNum == 121)
                    else if (strs[1].Contains("spp."))
                    {
                        sppBool = false;
                        sName = strs[4];
                    }
                    else if (strs[2] == "false")
                    {
                        sppBool = false;
                        //sName = strs[4];
                    }
                    if (!string.IsNullOrEmpty(strs[4])) sName = strs[4];

                    //FormattedString fName = new();
                    string fName = string.Empty;

                    //fName = FormatTitle(strs[1], sppBool);
                    //if (sppBool) fName = strs[1] + " spp.";
                    //else fName = strs[1];
                    fName = strs[1];
                    //if (!string.IsNullOrEmpty(strs[5]) && strs[5] != " ") fName += Environment.NewLine + strs[5];

                    //List<string> lineSpecies = strs[5].Split("* ").ToList<string>();
                    //List<string> lineFiles = strs[6].Split("* ").ToList<string>();
                    string lineSpecies = strs[5];
                    string lineFiles = strs[6];

                    //FormattedString fStr14 = FormatString(strs[14]);
                    //FormattedString fStr15 = FormatString(strs[15]);
                    string fStr14 = strs[14];
                    string fStr15 = strs[15];

                    //List<string> lineSimSpecies = strs[16].Split("* ").ToList<string>();
                    //List<string> lineSimFiles = strs[17].Split("* ").ToList<string>();
                    string lineSimSpecies = strs[16];
                    string lineSimFiles = strs[17];

                    //spec = new SpeciesGroupsOld(IDNum, fName, sppBool, strs[3], strs[4], lineSpecies, lineFiles,
                    //    strs[7], strs[8], strs[9], strs[10], strs[11], strs[12], strs[13], fStr14, fStr15,
                    //    lineSimSpecies, lineSimFiles);
                    group = new SpeciesGroup
                    {
                        SpeciesGroupID = IDNum,
                        Name = fName,
                        Spp = sppBool,
                        Family = strs[3],
                        //Species = strs[4],
                        Species = sName,
                        //ImageNames = strs[5],
                        TransverseES = strs[5],
                        NameES = strs[6],
                        FileNames = strs[7],
                        Transverse = strs[8],
                        Porosity = strs[9],
                        Vessels = strs[10],
                        Rays = strs[11],
                        Parenchyma = strs[12],
                        Tangential = strs[13],
                        OtherCharacters = strs[14],
                        AdditionalComments = strs[15],
                        SimilarWoods = strs[16],
                        //SimilarImageNames = strs[16],
                        SimilarFileNames = strs[17],
                        OtherCharactersES = strs[18]
                    };
                }
                else if (strs.Count() == 16)
                {
                    string sName = " ";
                    if (strs[2] == "true")
                    {
                        sppBool = true;
                        sName = strs[4];
                    }
                    else if (strs[1].Contains("spp.") || IDNum == 79 || IDNum == 121)
                    {
                        sppBool = false;
                        sName = strs[4];
                    }
                    else if (strs[2] == "false")
                    {
                        sppBool = false;
                        //sName = strs[4];
                    }

                    //FormattedString fName = new();
                    string fName = string.Empty;

                    //fName = FormatTitle(strs[1], sppBool);
                    //if (sppBool) fName = strs[1] + " spp.";
                    //else fName = strs[1];
                    fName = strs[1];

                    //string lineSpecies = strs[5];
                    //string lineFiles = strs[6];
                    string lineFiles = strs[5];

                    //FormattedString fStr14 = FormatString(strs[14]);
                    //FormattedString fStr15 = FormatString(strs[15]);
                    string fStr14 = strs[13];
                    string fStr15 = strs[14];

                    //string lineSimSpecies = strs[16];
                    //string lineSimFiles = strs[17];
                    string lineSimFiles = strs[15];

                    //spec = new SpeciesGroupsOld(IDNum, fName, sppBool, strs[3], strs[4], lineSpecies, lineFiles,
                    //    strs[7], strs[8], strs[9], strs[10], strs[11], strs[12], strs[13], fStr14, fStr15,
                    //    lineSimSpecies, lineSimFiles);
                    group = new SpeciesGroup
                    {
                        SpeciesGroupID = IDNum,
                        Name = fName,
                        Spp = sppBool,
                        Family = strs[3],
                        //Species = strs[4],
                        Species = sName,
                        ImageNames = string.Empty,
                        FileNames = strs[5],
                        Transverse = strs[6],
                        Porosity = strs[7],
                        Vessels = strs[8],
                        Rays = strs[9],
                        Parenchyma = strs[10],
                        Tangential = strs[11],
                        OtherCharacters = strs[12],
                        AdditionalComments = strs[13],
                        SimilarWoods = strs[14],
                        SimilarImageNames = string.Empty,
                        SimilarFileNames = strs[15]
                    };
                }
                else if (strs.Count() == 6)
                {
                    if (strs[2] == "true") sppBool = true;
                    else if (strs[2] == "false") sppBool = false;

                    //FormattedString fName = new();
                    string fName = string.Empty;

                    //fName = FormatTitle(strs[1], sppBool);
                    //if (sppBool) fName = strs[1] + " spp.";
                    //else fName = strs[1];
                    fName = strs[1];

                    string plH = "placeholder";
                    //List<string> plHlist = ["placeholder", "placeholder", "placeholder"];
                    //List<string> plHfiles = ["spgr0n0i.png", "spgr0n1i.png", "spgr0n2i.png"];
                    string plHlist = "placeholder* placeholder* placeholder";
                    string plHfiles = "g0n0i.jpg* g0n1i.jpg* g0n2i.jpg";

                    FormattedString plHfs = new();
                    Span span = new()
                    {
                        Text = "placeholder"
                    };
                    plHfs.Spans.Add(span);
                    //spec = new SpeciesGroups(IDNum, strs[1], sppBool, strs[3], strs[4], new List<string>(), new List<string>(),
                    //    plH, strs[8], strs[9], strs[10], strs[11], strs[12], strs[13], new FormattedString(), new FormattedString(),
                    //    new List<string>(), new List<string>());
                    //spec = new SpeciesGroupsOld(IDNum, fName, sppBool, strs[3], strs[4], plHlist, plHfiles,
                    //    " ", plH, plH, plH, plH, plH, plH, plHfs, plHfs,
                    //    plHlist, plHfiles);
                    group = new SpeciesGroup
                    {
                        SpeciesGroupID = IDNum,
                        Name = fName,
                        Spp = sppBool,
                        Family = strs[3],
                        Species = strs[4],
                        ImageNames = plHlist,
                        FileNames = plHfiles,
                        Transverse = " ",
                        Porosity = plH,
                        Vessels = plH,
                        Rays = plH,
                        Parenchyma = plH,
                        Tangential = plH,
                        OtherCharacters = plH,
                        AdditionalComments = plH,
                        SimilarWoods = plH,
                        SimilarImageNames = plHlist,
                        SimilarFileNames = plHfiles
                    };
                }
                else
                {
                    string plH = "placeholder";
                    //List<string> plHlist = ["placeholder", "placeholder", "placeholder"];
                    //List<string> plHfiles = ["spgr0n0i.png", "spgr0n1i.png", "spgr0n2i.png"];
                    string plHlist = "placeholder* placeholder* placeholder";
                    string plHfiles = "g0n0i.jpg* g0n1i.jpg* g0n2i.jpg";
                    FormattedString plHfs = new();
                    Span span = new()
                    {
                        Text = "placeholder"
                    };
                    plHfs.Spans.Add(span);
                    //FormattedString plHt = FormatTitle("Placeholder", true);
                    string plHt = "Placeholder";
                    //spec = new SpeciesGroupsOld(IDNum, plHt, true, plH, "P. holder", plHlist, plHfiles,
                    //    " ", plH, plH, plH, plH, plH, plH, plHfs, plHfs,
                    //    plHlist, plHfiles);
                    group = new SpeciesGroup
                    {
                        SpeciesGroupID = IDNum,
                        Name = plHt,
                        Spp = true,
                        Family = plH,
                        Species = "P. holder",
                        ImageNames = plHlist,
                        FileNames = plHfiles,
                        Transverse = " ",
                        Porosity = plH,
                        Vessels = plH,
                        Rays = plH,
                        Parenchyma = plH,
                        Tangential = plH,
                        OtherCharacters = plH,
                        AdditionalComments = plH,
                        SimilarWoods = plH,
                        SimilarImageNames = plHlist,
                        SimilarFileNames = plHfiles
                    };
                }

                //group.NameES = strsES[1];
                //group.TransverseES = strsES[5];
                //group.PorosityES = strsES[6];
                //group.VesselsES = strsES[7];
                //group.RaysES = strsES[8];
                //group.ParenchymaES = strsES[9];
                //group.TangentialES = strsES[10];
                //group.OtherCharactersES = strsES[11];
                //group.AdditionalCommentsES = strsES[12];
                //group.SimilarWoodsES = strsES[13];

                //speciesGroups.Add(spec);
                speciesGroups.Add(group);
                localSpeciesGroups.Add(group);
                Console.WriteLine("AddDBData     speciesGroup created: " + group.SpeciesGroupID.ToString());

                //Console.WriteLine("SpeciesGroup test:   " + group.SpeciesGroupID.ToString());
            }

            //return speciesGroups;
            //SpeciesGroupsList = speciesGroups;
            //return SpeciesGroupsList;
            return speciesGroups;
        }


        private static async Task<List<SpeciesTbl>> CreateSpeciesTbls()
        {
            List<SpeciesTbl> speciesTbls = new List<SpeciesTbl>();
            localSpeciesTbls = new List<SpeciesTbl>();

            SpeciesTbl species = new SpeciesTbl();

            //using var stream = await FileSystem.OpenAppPackageFileAsync("KeyEntriesDataAdjCombined.txt");
            using var stream = await FileSystem.OpenAppPackageFileAsync("SpeciesDataNew.txt");
            using var reader = new StreamReader(stream);

            string line = string.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                List<string> strs = line.Split("+  ").ToList<string>();

                species.SpeciesTblID = int.Parse(strs[0]);
                species.Name = strs[1];
                species.LocalName = strs[2];
                species.TradeName = strs[3];
                species.SpeciesGroupID = int.Parse(strs[4]);
                species.Type = int.Parse(strs[5]);
                //if (strs.Count == 5) species.Features = strs[4];
                if (strs.Count == 7) species.Features = strs[6];
                else species.Features = string.Empty;

                speciesTbls.Add(species);
                localSpeciesTbls.Add(species);
                Console.WriteLine("AddDBData     speciesTbl created: " + species.SpeciesTblID.ToString());

                species = new SpeciesTbl();
            }

            return speciesTbls;
            //return localSpeciesTbls;
        }


        private static async Task<List<ImageTbl>> CreateImageTbls()
        //private static async Task<ImageTbl[]> CreateImageTbls()
        {
            List<ImageTbl> imageTbls = new List<ImageTbl>();
            //ImageTbl[] imageTbls = new ImageTbl[929];

            ImageTbl image = new ImageTbl();

            //using var stream = await FileSystem.OpenAppPackageFileAsync("ImagesDataNew.txt");
            using var stream = await FileSystem.OpenAppPackageFileAsync("ImagesData.txt");
            using var reader = new StreamReader(stream);

            int imageID = 1;
            string line = string.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                //List<string> strs = line.Split("+  ").ToList();
                string[] strs = line.Split("+  ");

                image.ImageTblID = imageID;
                image.FileName = strs[0];
                //int nIndex = strs[0].IndexOf('n');
                //image.SpeciesPageID = int.Parse(strs[0].Substring(1, nIndex - 1));

                // Set cutoff for species images, beginning at example images from book
                //if (imageID < 930)
                if (imageID < 262)
                {
                    if (strs[0].Contains('i'))
                    {
                        image.Origin = 3;
                        int nIndex = strs[0].IndexOf('n');
                        image.SpeciesPageID = int.Parse(strs[0].Substring(1, nIndex - 1));

                        if (strs[2] == "Rename")
                        {
                            image.Duplicate = false;
                            image.ImageName = strs[1];

                            image.SpeciesTblID = int.Parse(strs[3]);
                            //SpeciesTbl species = App.DB.GetSpeciesTblByID(int.Parse(strs[3]));
                        }
                        else
                        {
                            image.Duplicate = false;
                            image.SpeciesGroupID = image.SpeciesPageID;

                            //int speciesID = int.Parse(strs[3]);
                            int speciesGroupID = (int)image.SpeciesPageID;
                            //image.SpeciesTblID = speciesID;
                            //image.SpeciesTblID = image.SpeciesPageID;

                            //SpeciesTbl species = App.DB.GetSpeciesTblByID(speciesID);
                            //SpeciesTbl species = localSpeciesTbls[speciesID - 1];
                            SpeciesGroup sGroup = localSpeciesGroups[speciesGroupID - 1];
                            string iName = "";
                            //int endIndex = species.Name.IndexOf('(');
                            //int endIndex = sGroup.Name.IndexOf('(');

                            //if (endIndex > -1) image.ImageName = species.Name.Substring(0, endIndex - 1);
                            //else image.ImageName = species.Name;
                            //if (endIndex > -1) iName = sGroup.Name.Substring(0, endIndex - 1);
                            //else iName = sGroup.Name;
                            
                            //iName = sGroup.Name;
                            if (sGroup.Spp) iName = "[" + sGroup.Name + "] spp.";
                            else
                            {
                                if (sGroup.Name.Contains("[")) iName = sGroup.Name;
                                else iName = "[" + sGroup.Name + "]";
                            }
                            //iName += Environment.NewLine;

                            //if (!string.IsNullOrEmpty(sGroup.NameES) && sGroup.NameES != " ") iName += Environment.NewLine + sGroup.NameES;
                            List<SpeciesTbl> list = new List<SpeciesTbl>();
                            //for (int i = 0; i < localSpeciesTbls.Count; i++)
                            //{
                            //    if ()
                            //}
                            list = localSpeciesTbls.Where(k => k.SpeciesGroupID == sGroup.SpeciesGroupID).OrderBy(k => k.LocalName).ToList();
                            string lNames = "";
                            string tNames = "";
                            //for (int i = 0; i < list.Count; i++)
                            //{
                            //    if (i == 0)
                            //    {
                            //        if (list[0].LocalName != " ") lNames += list[0].LocalName;
                            //        if (list[0].TradeName != "Celtis") if (list[0].TradeName != " ") tNames += list[0].TradeName;
                            //    }
                            //    else
                            //    {
                            //        if (list[i].LocalName != " ")
                            //        {
                            //            if (!lNames.Contains(list[i].LocalName)) lNames += ", " + list[i].LocalName;
                            //        }
                            //        if (list[i].TradeName != " ")
                            //        {
                            //            if (!list[i].TradeName.Contains("Celtis"))
                            //            {
                            //                if (!tNames.Contains(list[i].TradeName)) tNames += ", " + list[i].TradeName;
                            //            }
                            //            else
                            //            {
                            //                tNames += list[i].TradeName;
                            //            }
                            //            //if (list[i].TradeName == "Celtis") 
                            //            //{
                            //            //    //if (list[1].TradeName.Contains(list[0].TradeName)) tNames = list[1].TradeName;
                            //            //}
                            //        }
                            //    }
                            //}
                            switch (sGroup.SpeciesGroupID)
                            {
                                case 19:
                                    lNames = "Esa";
                                    tNames = "Celtis, Ohia";
                                    break;

                                case 36:
                                    lNames = "Edinam, Efobrodedwo, Penkwa";
                                    tNames = "Utile, Sipo, Sapele, Candolei, Kosipo";
                                    break;

                                case 46:
                                    lNames = "Dubini, Krumben, Kuntunkuri";
                                    tNames = "African Mahogany";
                                    break;

                                case 57:
                                    lNames = "Odum";
                                    tNames = "Iroko, Odum";
                                    break;

                                case 61:
                                    lNames = "Kusia";
                                    tNames = "Opepe, Kusia";
                                    break;

                                case 72:
                                    lNames = "Fotie";
                                    tNames = "Hotrohotro";
                                    break;

                                case 75:
                                    lNames = "Ohaa, Sofo, Wawabima";
                                    break;

                                case 81:
                                    lNames = "Baku, Makore";
                                    break;

                                default:
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            if (list[0].LocalName != " ") lNames += list[0].LocalName;
                                            if (list[0].TradeName != " ") tNames += list[0].TradeName;
                                        }
                                        else
                                        {
                                            if (list[i].LocalName != " ")
                                            {
                                                if (!string.IsNullOrEmpty(lNames))
                                                {
                                                    if (!lNames.Contains(list[i].LocalName)) lNames += ", " + list[i].LocalName;
                                                }
                                                else lNames += list[i].LocalName;
                                            }
                                            if (list[i].TradeName != " ")
                                            {
                                                if (!string.IsNullOrEmpty(tNames))
                                                {
                                                    if (!tNames.Contains(list[i].TradeName)) tNames += ", " + list[i].TradeName;
                                                }
                                                else tNames += list[i].TradeName;
                                            }
                                        }
                                    }
                                    break;
                            }

                            if ((!string.IsNullOrEmpty(lNames) && lNames != " ") || (!string.IsNullOrEmpty(tNames) && tNames != " ")) iName += Environment.NewLine;
                            if (!string.IsNullOrEmpty(lNames) && lNames != " ")
                            {
                                iName += lNames;
                            }
                            if (!string.IsNullOrEmpty(tNames) && tNames != " ")
                            {
                                if (lNames != tNames)
                                {
                                    if (!string.IsNullOrEmpty(lNames) && lNames != " ")
                                    {
                                        iName += "; ";
                                    }
                                    iName += tNames;
                                }
                            }

                            //image.ImageName = species.Name;
                            //image.ImageName = sGroup.Name;
                            image.ImageName = iName;

                            //image.SpeciesTblID = 1;
                        }
                    }
                    //else
                    //else if (strs[0].Contains('s'))
                    //{
                    //    image.Origin = 4;
                    //    int nIndex = strs[0].IndexOf('n');
                    //    image.SpeciesPageID = int.Parse(strs[0].Substring(1, nIndex - 1));

                    //    //if (strs[3] == string.Empty || strs[3] == null || strs[3] == "")
                    //    if (string.IsNullOrEmpty(strs[3]) || strs[3] == string.Empty || strs[3] == " ")
                    //    {
                    //        image.Duplicate = true;
                    //        image.ImageName = strs[1];
                    //    }
                    //    else
                    //    {
                    //        if (strs[1] == " " || strs[1] == string.Empty || string.IsNullOrEmpty(strs[1]))
                    //        {
                    //            image.Duplicate = false;
                    //            image.SpeciesGroupID = image.SpeciesPageID;

                    //            int speciesID = int.Parse(strs[3]);
                    //            image.SpeciesTblID = speciesID;
                    //            //SpeciesTbl species = App.DB.GetSpeciesTblByID(speciesID);
                    //            //SpeciesTbl species = localSpeciesTbls[speciesID - 1];
                    //            //int endIndex = species.Name.IndexOf('(');
                    //            //if (endIndex > -1) image.ImageName = species.Name.Substring(0, endIndex - 1);
                    //            //else image.ImageName = species.Name;
                    //            //image.ImageName = species.Name;
                    //        }
                    //        else
                    //        {
                    //            image.Duplicate = true;
                    //            image.ImageName = strs[1];
                    //        }
                    //    }
                    //}
                }
                //else if (strs[0].Contains('p'))
                else
                {
                    image.Origin = 2;
                    image.Duplicate = false;
                    if (strs[0].Contains("not")) image.Duplicate = true;
                    image.ImageName = strs[1];
                }

                if (image.Origin == 3)
                {
                    //Console.WriteLine("CreateImageTbls  " + image.ImageTblID.ToString() + "  Image  Species:  " + image.SpeciesTblID.ToString() + "  ImageName:  " + image.ImageName);
                    Console.WriteLine("CreateImageTbls  " + image.ImageTblID.ToString() + "  Image  Group:  " + image.SpeciesGroupID.ToString() + "  ImageName:  " + image.ImageName);
                }
                //else if (image.Origin == 4)
                //{
                //    //Console.WriteLine("CreateImageTbls  " + image.ImageTblID.ToString() + "  Similar  Species:  " + image.SpeciesTblID.ToString() + "  ImageName:  " + image.ImageName);
                //    Console.WriteLine("CreateImageTbls  " + image.ImageTblID.ToString() + "  Similar  Group:  " + image.SpeciesGroupID.ToString() + "  ImageName:  " + image.ImageName);
                //}

                //Console.WriteLine("Testing CreateImageTbls, imageID =   " + imageID.ToString());
                imageTbls.Add(image);
                //imageTbls[imageID - 1] = image;
                imageID += 1;
                image = new ImageTbl();
            }

            return imageTbls;
        }


        private static async Task<List<EntryImagesTbl>> CreateEntryImagesTbls()
        {
            List<EntryImagesTbl> entryImagesTbls = new List<EntryImagesTbl>();

            EntryImagesTbl entryImage = new EntryImagesTbl();
            string fillerImages = "g1n0i.jpg* g1n1i.jpg* g1n2i.jpg";

            using var stream = await FileSystem.OpenAppPackageFileAsync("EntryImagesData.txt");
            using var reader = new StreamReader(stream);

            int count = 2;
            string line = string.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                if (count <= 0 || line == string.Empty || line == null || line == " ")
                {
                    count = 2;
                    
                    entryImage = new EntryImagesTbl();
                }
                else
                {
                    List<string> strs = line.Split("+  ").ToList<string>();

                    entryImage.EntryImagesID = int.Parse(strs[0]);
                    entryImage.KeyEntryID = int.Parse(strs[0]);

                    if (count == 2)
                    {
                        if (strs[1] == "t")
                        {
                            entryImage.ExampleImages = true;

                            if (strs[2] == string.Empty || strs[2] == "") entryImage.FileNames = fillerImages;
                            else entryImage.FileNames = strs[2];

                            entryImage.SpeciesGroup = string.Empty;
                            entryImage.Species = null;
                        }
                        else if (strs[1] == "f")
                        {
                            entryImage.ExampleImages = false;

                            if (strs.Count < 4)
                            {
                                entryImage.SpeciesGroup = strs[2];
                                entryImage.Species = null;
                            }
                            else if (strs.Count == 4)
                            {
                                entryImage.SpeciesGroup = strs[2];
                                entryImage.Species = int.Parse(strs[3]);
                            }

                            entryImage.FileNames = string.Empty;
                        }
                    }
                    else if (count == 1)
                    {
                        if (strs[1] == "t")
                        {
                            entryImage.ExampleImages1 = true;

                            if (strs[2] == string.Empty || strs[2] == "") entryImage.FileNames1 = fillerImages;
                            else entryImage.FileNames1 = strs[2];

                            entryImage.SpeciesGroup1 = string.Empty;
                            entryImage.Species1 = null;
                        }
                        else if (strs[1] == "f")
                        {
                            entryImage.ExampleImages1 = false;

                            if (strs.Count < 4)
                            {
                                entryImage.SpeciesGroup1 = strs[2];
                                entryImage.Species1 = null;
                            }
                            else if (strs.Count == 4)
                            {
                                entryImage.SpeciesGroup1 = strs[2];
                                entryImage.Species1 = int.Parse(strs[3]);
                            }

                            entryImage.FileNames1 = string.Empty;
                        }

                        entryImagesTbls.Add(entryImage);
                        Console.WriteLine("Testing CreateEntryImagesTbls, KeyEntryID =   " + entryImage.KeyEntryID.ToString());

                        entryImage = new EntryImagesTbl();
                    }

                    count--;
                }
            }

            return entryImagesTbls;
        }
    }
}
