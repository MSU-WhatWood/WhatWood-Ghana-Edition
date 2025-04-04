using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GhanaWoods.Database
{
    public class GhanaWoodsContext : DbContext
    {
        private static GhanaWoodsContext instance;

        public static GhanaWoodsContext Create()
        {
            instance = new GhanaWoodsContext();

            instance.Database.EnsureDeleted();
            instance.Database.EnsureCreated();

            //instance.Database.Migrate();

            return instance;
        }
        //public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Post> Posts { get; set; }
        public DbSet<KeyEntry> KeyEntries { get; set; }
        public DbSet<KeyHistory> KeyHistories { get; set; }
        public DbSet<SpeciesGroup> SpeciesGroups { get; set; }
        public DbSet<SpeciesTbl> Species { get; set; }
        public DbSet<ImageTbl> Images { get; set; }
        public DbSet<EntryImagesTbl> EntryImages { get; set; }

        public string DbPath { get; }

        public GhanaWoodsContext() : base()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "ghanawoods.db");

            //Create();
        }

        public GhanaWoodsContext(DbContextOptions<GhanaWoodsContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "ghanawoods.db");

            //Create();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        #region Model Initialization
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<KeyEntry>()
            //    .Property(b => b.DecisionNum)
            //    .HasColumnType("INT")
            //    .IsRequired();

            modelBuilder.Entity<KeyEntry>(entity =>
            {
                entity.HasKey(e => e.KeyEntryID);
                entity.Property(e => e.DecisionNum).HasColumnType("int");
                entity.Property(e => e.Text0).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Text0ES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TargetID0).HasColumnType("int").IsRequired(false);
                entity.Property(e => e.TargetGroupsIDs0).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TargetGroupsNames0).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TargetGroupsNames0ES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Text1).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Text1ES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TargetID1).HasColumnType("int").IsRequired(false);
                entity.Property(e => e.TargetGroupsIDs1).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TargetGroupsNames1).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TargetGroupsNames1ES).HasColumnType("string").IsRequired(false);
                entity.HasOne(e => e.KeyHistory).WithOne(f => f.KeyEntry).HasForeignKey("KeyHistory", "KeyEntryID");
                //entity.HasMany(e => e.EntryImage).WithOne(f => f.KeyEntry).HasForeignKey("KeyEntryID");
                entity.HasOne(e => e.EntryImages).WithOne(f => f.KeyEntry).HasForeignKey("EntryImagesTbl", "KeyEntryID");
            });
            modelBuilder.Entity<KeyHistory>(entity =>
            {
                entity.HasKey(e => e.KeyHistoryID);
                entity.Property(e => e.PrevDecisions).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.GreyDecisions).HasColumnType("string").IsRequired(false);
                //entity.Property(e => e.KeyEntryID).HasColumnType("int");
                //entity.HasOne(e => e.KeyEntry).WithOne(f => f.KeyHistory).HasForeignKey(g => g.KeyEntryID);
                entity.HasOne(e => e.KeyEntry).WithOne(f => f.KeyHistory).HasForeignKey("KeyHistory", "KeyEntryID");
                //entity.HasData(
                //    new KeyHistory { KeyHistoryID = 1, PrevDecisions = "", GreyDecisions = "", KeyEntryID = 1 },
                //    new KeyHistory { KeyHistoryID = 2, PrevDecisions = "1a", GreyDecisions = "", KeyEntryID = 2 },
                //    new KeyHistory { KeyHistoryID = 3, PrevDecisions = "1a, 2b", GreyDecisions = "", KeyEntryID = 3 }
                //);
            });
            modelBuilder.Entity<SpeciesGroup>(entity =>
            {
                entity.HasKey(e => e.SpeciesGroupID);
                entity.Property(e => e.Name).HasColumnType("string");
                entity.Property(e => e.NameES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Spp).HasColumnType("bool");
                entity.Property(e => e.Family).HasColumnType("string");
                entity.Property(e => e.Species).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.ImageNames).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.FileNames).HasColumnType("string");
                entity.Property(e => e.Transverse).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TransverseES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Porosity).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.PorosityES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Vessels).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.VesselsES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Rays).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.RaysES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Parenchyma).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.ParenchymaES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Tangential).HasColumnType("string");
                entity.Property(e => e.TangentialES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.OtherCharacters).HasColumnType("string");
                entity.Property(e => e.OtherCharactersES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.AdditionalComments).HasColumnType("string");
                entity.Property(e => e.AdditionalCommentsES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.SimilarWoods).HasColumnType("string");
                entity.Property(e => e.SimilarWoodsES).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.SimilarImageNames).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.SimilarFileNames).HasColumnType("string");
                entity.HasMany(e => e.SpeciesTbl).WithOne(f => f.SpeciesGroup).HasForeignKey("SpeciesGroupID");
                entity.HasMany(e => e.ImageTbl).WithOne(f => f.SpeciesGroup).HasForeignKey("SpeciesGroupID").IsRequired(false);
                entity.HasMany(e => e.ImageTbls).WithOne(f => f.SpeciesPageGroup).HasForeignKey("SpeciesPageID").IsRequired(false);
            });
            modelBuilder.Entity<SpeciesTbl>(entity =>
            {
                entity.HasKey(e => e.SpeciesTblID);
                entity.Property(e => e.Name).HasColumnType("string");
                entity.Property(e => e.LocalName).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.TradeName).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Type).HasColumnType("int");
                entity.Property(e => e.Features).HasColumnType("string").IsRequired(false);
                entity.HasOne(e => e.SpeciesGroup).WithMany(f => f.SpeciesTbl).HasForeignKey("SpeciesGroupID");
                entity.HasMany(e => e.ImageTbl).WithOne(f => f.SpeciesTbl).HasForeignKey("SpeciesTblID").IsRequired(false);
            });
            modelBuilder.Entity<ImageTbl>(entity =>
            {
                entity.HasKey(e => e.ImageTblID);
                entity.Property(e => e.FileName).HasColumnType("string");
                entity.Property(e => e.ImageName).HasColumnType("string");
                entity.Property(e => e.Origin).HasColumnType("int");
                //entity.Property(e => e.Sort).HasColumnType("int").IsRequired(false);
                entity.Property(e => e.Duplicate).HasColumnType("bool");
                //entity.Property(e => e.SpeciesGroupID).HasColumnType("int");
                entity.HasOne(e => e.SpeciesPageGroup).WithMany(f => f.ImageTbls).HasForeignKey("SpeciesPageID").IsRequired(false);
                entity.HasOne(e => e.SpeciesGroup).WithMany(f => f.ImageTbl).HasForeignKey("SpeciesGroupID").IsRequired(false);
                entity.HasOne(e => e.SpeciesTbl).WithMany(f => f.ImageTbl).HasForeignKey("SpeciesTblID").IsRequired(false);
                //entity.HasMany(e => e.EntryImage).WithOne(f => f.ImageTbl).HasForeignKey("KeyEntryID");
            });
            //modelBuilder.Entity<EntryImage>(entity =>
            //{
            //    entity.HasKey(e => e.EntryImageID);
            //    entity.Property(e => e.ImageName).HasColumnType("string").IsRequired(false);
            //    entity.Property(e => e.ImageNameES).HasColumnType("string").IsRequired(false);
            //    entity.HasOne(e => e.KeyEntry).WithMany(f => f.EntryImage).HasForeignKey("KeyEntryID");
            //    entity.HasOne(e => e.ImageTbl).WithMany(f => f.EntryImage).HasForeignKey("ImageTblID");
            //});
            modelBuilder.Entity<EntryImagesTbl>(entity =>
            {
                entity.HasKey(e => e.EntryImagesID);
                entity.Property(e => e.FileNames).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.FileNames1).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.SpeciesGroup).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.SpeciesGroup1).HasColumnType("string").IsRequired(false);
                entity.Property(e => e.Species).HasColumnType("int").IsRequired(false);
                entity.Property(e => e.Species1).HasColumnType("int").IsRequired(false);
                entity.Property(e => e.ExampleImages).HasColumnType("bool");
                entity.Property(e => e.ExampleImages1).HasColumnType("bool");
                entity.HasOne(e => e.KeyEntry).WithOne(f => f.EntryImages).HasForeignKey("EntryImagesTbl", "KeyEntryID");
            });

            //AddDBdata addDBdata = new AddDBdata();
            AddDBdata.AddDataToDB(modelBuilder);
        }
        #endregion


        #region Add Data
        //private void AddData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<KeyHistory>(entity =>
        //    {
        //        entity.HasData(
        //            new KeyHistory { KeyHistoryID = 1, PrevDecisions = "", GreyDecisions = "", KeyEntryID = 1 },
        //            new KeyHistory { KeyHistoryID = 2, PrevDecisions = "1a", GreyDecisions = "", KeyEntryID = 2 },
        //            new KeyHistory { KeyHistoryID = 3, PrevDecisions = "1a, 2b", GreyDecisions = "", KeyEntryID = 3 }
        //        );
        //    });
        //    modelBuilder.Entity<KeyEntry>(entity =>
        //    {
        //        entity.HasData(
        //            //new KeyEntry()
        //        );
        //    });
        //}
        #endregion


        #region KeyEntry queries
        public KeyEntry GetKeyEntryByID(int IDnum)
        {
            KeyEntry entry = new KeyEntry();

            entry = KeyEntries.Where(k => k.KeyEntryID == IDnum).FirstOrDefault();

            return entry;
        }

        public List<KeyEntry> GetKeyEntriesList()
        {
            List<KeyEntry> entriesList = new List<KeyEntry>();

            entriesList = KeyEntries.ToList();

            return entriesList;
        }
        #endregion


        #region KeyHistory queries
        public List<string> GetKeyHistoryPrev(int IDnum)
        {
            List<string> prev = new List<string>();

            var kH = KeyHistories.Where(k => k.KeyHistoryID == IDnum).FirstOrDefault();

            if (kH != null)
            {
                if (kH.PrevDecisions != null || kH.PrevDecisions != string.Empty)
                {
                    prev = kH.PrevDecisions.Split(", ").ToList();
                }
            }

            return prev;
        }

        public KeyHistory GetKeyHistoryByID(int IDnum)
        {
            KeyHistory history = new KeyHistory();

            history = KeyHistories.Where(k => k.KeyHistoryID == IDnum).FirstOrDefault();

            return history;
        }

        public List<KeyHistory> GetKeyHistoriesList()
        {
            List<KeyHistory> historiesList = new List<KeyHistory>();

            historiesList = KeyHistories.ToList();

            return historiesList;
        }
        #endregion


        #region SpeciesGroup queries
        public SpeciesGroup GetSpeciesGroupByID(int IDnum)
        {
            SpeciesGroup group = new SpeciesGroup();

            group = SpeciesGroups.Where(k => k.SpeciesGroupID == IDnum).FirstOrDefault();

            return group;
        }

        public List<SpeciesGroup> GetSpeciesGroupsList()
        {
            List<SpeciesGroup> groupsList = new List<SpeciesGroup>();

            groupsList = SpeciesGroups.ToList();

            return groupsList;
        }

        public List<SpeciesGroup> GetHWGroupsByFeature(int featureNum)
        {
            List<SpeciesGroup> HWspecies = SpeciesGroups.Where(k => !string.IsNullOrEmpty(k.OtherCharactersES) && k.OtherCharactersES != " ").ToList();

            List<SpeciesGroup> TrueSpecies = new List<SpeciesGroup>();


            TrueSpecies = HWspecies.Where(k => (k.OtherCharactersES.Split(",").ToList().Contains(" " + featureNum.ToString()))).ToList();

            if (TrueSpecies.Count < 1)
            {
                TrueSpecies.Add(HWspecies.Where(k => k.SpeciesGroupID == 80).FirstOrDefault());
                TrueSpecies.Add(HWspecies.Where(k => k.SpeciesGroupID == 81).FirstOrDefault());
            }
            //else if (TrueSpecies.Count == 1) TrueSpecies.Add(HWspecies.Where(k => k.SpeciesGroupID == 79).FirstOrDefault());

            //List<SpeciesTbl> FalseSpecies = SWspecies.Where(k => !(k.Features.Split(",").ToList().Contains(' ' + featureNum.ToString())))
            //    .Where(k => !(k.Features.Split(",").ToList().Contains('-' + featureNum.ToString()))).ToList();            
            List<SpeciesGroup> FalseSpecies = new List<SpeciesGroup>();

            FalseSpecies = HWspecies.Where(k => !(k.OtherCharactersES.Split(",").ToList().Contains(" " + featureNum.ToString()))
                && !(k.OtherCharactersES.Split(",").ToList().Contains("-" + featureNum.ToString()))).ToList();

            if (FalseSpecies.Count < 1)
            {
                FalseSpecies.Add(HWspecies.Where(k => k.SpeciesGroupID == 80).FirstOrDefault());
                FalseSpecies.Add(HWspecies.Where(k => k.SpeciesGroupID == 81).FirstOrDefault());
            }
            //else if (FalseSpecies.Count == 1) FalseSpecies.Add(HWspecies.Where(k => k.SpeciesGroupID == 79).FirstOrDefault());

            List<SpeciesGroup> selectedSpecies = new List<SpeciesGroup>();
            Random rand = new Random();
            int trueIndex = rand.Next(0, TrueSpecies.Count);
            selectedSpecies.Add(TrueSpecies[trueIndex]);

            int falseIndex = rand.Next(0, FalseSpecies.Count);
            selectedSpecies.Add(FalseSpecies[falseIndex]);

            return selectedSpecies;
        }

        public SpeciesGroup SelectHWGroupForFeatures()
        {
            SpeciesGroup species = new SpeciesGroup();

            List<SpeciesGroup> HWspecies = new List<SpeciesGroup>();

            HWspecies = SpeciesGroups.Where(k => !string.IsNullOrEmpty(k.OtherCharactersES) && k.OtherCharactersES != " ").ToList();

            Random rand = new Random();
            int selectedSpecies = rand.Next(0, HWspecies.Count);
            species = HWspecies[selectedSpecies];

            return species;
        }
        #endregion


        #region SpeciesTbl queries
        public SpeciesTbl GetSpeciesTblByID(int IDnum)
        {
            SpeciesTbl species = new SpeciesTbl();

            species = Species.Where(k => k.SpeciesTblID == IDnum).FirstOrDefault();

            return species;
        }

        public List<SpeciesTbl> GetSpeciesList()
        {
            List<SpeciesTbl> speciesList = new List<SpeciesTbl>();

            speciesList = Species.ToList();

            return speciesList;
        }

        public List<SpeciesTbl> GetSpeciesListByLocalName()
        {
            List<SpeciesTbl> speciesList = new List<SpeciesTbl>();

            speciesList = Species.Where(k => k.LocalName != " ").OrderBy(k => k.LocalName).ToList();

            return speciesList;
        }

        public List<SpeciesTbl> GetSpeciesListByFeature(int featureNum)
        {
            List<SpeciesTbl> speciesList = new List<SpeciesTbl>();

            speciesList = Species.Where(k => k.Features.Split(", ", StringSplitOptions.None).ToList().Contains(featureNum.ToString())).ToList();

            return speciesList;
        }

        public List<SpeciesTbl> GetGroupOfSpeciesByFeature(int featureNum)
        {
            List<SpeciesTbl> speciesList = GetSpeciesListByFeature(featureNum);

            List<int> speciesGroupIDList = new List<int>();

            speciesGroupIDList = speciesList.Select(k => k.SpeciesGroupID).ToList();

            Random ram = new Random();
            int index = ram.Next(speciesGroupIDList.Count);
            int randomSG = speciesGroupIDList[index];

            //SpeciesGroup group = GetSpeciesGroupByID(randomSG);
            List<SpeciesTbl> groupOfSpeciesByFeature = new List<SpeciesTbl>();
            groupOfSpeciesByFeature = speciesList.Where(k => k.SpeciesGroupID == randomSG).ToList();

            //return group;
            return groupOfSpeciesByFeature;
        }

        public List<SpeciesTbl> GetSWSpeciesByFeature(int featureNum)
        {
            List<SpeciesTbl> SWspecies = Species.Where(k => k.Type == 1).ToList();

            List<SpeciesTbl> TrueSpecies = new List<SpeciesTbl>();
                
            if (featureNum == 41)
            {
                TrueSpecies.Add(SWspecies.Where(k => k.SpeciesTblID == 271).FirstOrDefault());
            }
            else
            {
                TrueSpecies = SWspecies.Where(k => (k.Features.Split(",").ToList().Contains(" " + featureNum.ToString()))).ToList();
            }

            if (TrueSpecies.Count < 1 && featureNum == 40) TrueSpecies = SWspecies.Where(k => k.SpeciesTblID != 271).ToList();
            if (TrueSpecies.Count < 1) TrueSpecies.Add(SWspecies.Where(k => k.SpeciesTblID == 271).FirstOrDefault());

            //List<SpeciesTbl> FalseSpecies = SWspecies.Where(k => !(k.Features.Split(",").ToList().Contains(' ' + featureNum.ToString())))
            //    .Where(k => !(k.Features.Split(",").ToList().Contains('-' + featureNum.ToString()))).ToList();            
            List<SpeciesTbl> FalseSpecies = new List<SpeciesTbl>();
                
            if (featureNum == 40)
            {
                FalseSpecies.Add(SWspecies.Where(k => k.SpeciesTblID == 271).FirstOrDefault());
            }
            else
            {
                FalseSpecies = SWspecies.Where(k => !(k.Features.Split(",").ToList().Contains(" " + featureNum.ToString()))
                    && !(k.Features.Split(",").ToList().Contains("-" + featureNum.ToString()))).ToList();
            }

            if (FalseSpecies.Count < 1) FalseSpecies.Add(SWspecies.Where(k => k.SpeciesTblID == 271).FirstOrDefault());


            List<SpeciesTbl> selectedSpecies = new List<SpeciesTbl>();
            Random rand = new Random();
            int trueIndex = 0;
            if (TrueSpecies.Count <= 1) trueIndex = 0;
            else trueIndex = rand.Next(0, TrueSpecies.Count);
            selectedSpecies.Add(TrueSpecies[trueIndex]);

            int falseIndex = 0;
            if (FalseSpecies.Count <= 1) falseIndex = 0;
            else falseIndex = rand.Next(0, FalseSpecies.Count);
            selectedSpecies.Add(FalseSpecies[falseIndex]);

            return selectedSpecies;
        }

        public List<SpeciesTbl> GetHWSpeciesByFeature(int featureNum)
        {
            List<SpeciesTbl> HWspecies = Species.Where(k => k.Type == 0 && !string.IsNullOrEmpty(k.Features)).ToList();

            List<SpeciesTbl> TrueSpecies = new List<SpeciesTbl>();


            TrueSpecies = HWspecies.Where(k => (k.Features.Split(",").ToList().Contains(" " + featureNum.ToString()))).ToList();

            if (TrueSpecies.Count < 1)
            {
                TrueSpecies.Add(HWspecies.Where(k => k.SpeciesTblID == 373).FirstOrDefault());
                TrueSpecies.Add(HWspecies.Where(k => k.SpeciesTblID == 374).FirstOrDefault());
            }
            else if (TrueSpecies.Count == 1) TrueSpecies.Add(HWspecies.Where(k => k.SpeciesTblID == 365).FirstOrDefault());
            //List<SpeciesTbl> FalseSpecies = SWspecies.Where(k => !(k.Features.Split(",").ToList().Contains(' ' + featureNum.ToString())))
            //    .Where(k => !(k.Features.Split(",").ToList().Contains('-' + featureNum.ToString()))).ToList();            
            List<SpeciesTbl> FalseSpecies = new List<SpeciesTbl>();

            FalseSpecies = HWspecies.Where(k => !(k.Features.Split(",").ToList().Contains(" " + featureNum.ToString()))
                && !(k.Features.Split(",").ToList().Contains("-" + featureNum.ToString()))).ToList();

            if (FalseSpecies.Count < 1)
            {
                FalseSpecies.Add(HWspecies.Where(k => k.SpeciesTblID == 373).FirstOrDefault());
                FalseSpecies.Add(HWspecies.Where(k => k.SpeciesTblID == 374).FirstOrDefault());
            }
            else if (FalseSpecies.Count == 1) FalseSpecies.Add(HWspecies.Where(k => k.SpeciesTblID == 365).FirstOrDefault());

            List<SpeciesTbl> selectedSpecies = new List<SpeciesTbl>();
            Random rand = new Random();
            int trueIndex = rand.Next(0, TrueSpecies.Count);
            selectedSpecies.Add(TrueSpecies[trueIndex]);

            int falseIndex = rand.Next(0, FalseSpecies.Count);
            selectedSpecies.Add(FalseSpecies[falseIndex]);

            return selectedSpecies;
        }


        public SpeciesTbl SelectSWSpeciesForFeatures()
        {
            SpeciesTbl species = new SpeciesTbl();

            List<SpeciesTbl> SWspecies = new List<SpeciesTbl>();

            SWspecies = Species.Where(k => k.Type == 1 && !string.IsNullOrEmpty(k.Features)).ToList();

            Random rand = new Random();
            int selectedSpecies = rand.Next(0, SWspecies.Count);
            species = SWspecies[selectedSpecies];

            return species;
        }

        public SpeciesTbl SelectHWSpeciesForFeatures()
        {
            SpeciesTbl species = new SpeciesTbl();

            List<SpeciesTbl> HWspecies = new List<SpeciesTbl>();

            HWspecies = Species.Where(k => k.Type == 0 && !string.IsNullOrEmpty(k.Features)).ToList();

            Random rand = new Random();
            int selectedSpecies = rand.Next(0, HWspecies.Count);
            species = HWspecies[selectedSpecies];

            return species;
        }

        public List<int> GetCorrectAnswersForFeatures(string features)
        {
            List<int> answers = new List<int>();
            List<string> featuresList = new List<string>();

            featuresList = features.Split(", ").ToList();
            for (int i = 0; i < featuresList.Count; i++)
            {
                if (!featuresList[i].Contains("-")) answers.Add(int.Parse(featuresList[i]));
            }

            return answers;
        }

        public List<int> GetBlockedAnswersForFeatures(string features)
        {
            List<int> answers = new List<int>();
            List<string> featuresList = new List<string>();

            featuresList = features.Split(", ").ToList();
            for (int i = 0; i < featuresList.Count; i++)
            {
                if (featuresList[i].Contains("-")) answers.Add(int.Parse(featuresList[i].Substring(1, featuresList[i].Length - 1)));
            }

            return answers;
        }

        public List<int> GetSWCategoriesForFeatures(List<int> answers)
        {
            List<int> categories = new List<int>();

            for (int i = 0; i < answers.Count; i++)
            {
                if (40 <= answers[i] && answers[i] <= 41)
                {
                    if (!categories.Contains(0)) categories.Add(0);
                }
                else if (42 <= answers[i] && answers[i] <= 43)
                {
                    if (!categories.Contains(1)) categories.Add(1);
                }           
                else if (72 <= answers[i] && answers[i] <= 74)
                {
                    if (!categories.Contains(2)) categories.Add(2);
                }
                else if (109 == answers[i] || 111 == answers[i])
                {
                    if (!categories.Contains(3)) categories.Add(3);
                }
            }

            return categories;
        }

        public List<int> GetHWCategoriesForFeatures(List<int> answers)
        {
            List<int> categories = new List<int>();

            for (int i = 0; i < answers.Count; i++)
            {
                if (1 == answers[i] || answers[i] == 2)
                {
                    if (!categories.Contains(0)) categories.Add(0);
                }
                else if (3 <= answers[i] && answers[i] <= 5)
                {
                    if (!categories.Contains(1)) categories.Add(1);
                }
                else if (6 <= answers[i] && answers[i] <= 8)
                {
                    if (!categories.Contains(2)) categories.Add(2);
                }
                else if (9 <= answers[i] && answers[i] <= 11)
                {
                    if (!categories.Contains(3)) categories.Add(3);
                }
                else if (56 == answers[i] || 58 == answers[i])
                {
                    if (!categories.Contains(4)) categories.Add(4);
                }
                else if (96 <= answers[i] && answers[i] <= 99)
                {
                    if (!categories.Contains(5)) categories.Add(5);
                }
                else if (114 <= answers[i] && answers[i] <= 116)
                {
                    if (!categories.Contains(6)) categories.Add(6);
                }
                else if (76 == answers[i] || answers[i] == 77)
                {
                    if (!categories.Contains(7)) categories.Add(7);
                }
                //else if (78 <= answers[i] && answers[i] <= 84)
                else if (78 < answers[i] && answers[i] < 84)
                {
                    if (!categories.Contains(8)) categories.Add(8);
                }
                else if (85 <= answers[i] && answers[i] <= 87 || answers[i] == 89)
                {
                    if (!categories.Contains(9)) categories.Add(9);
                }
            }

            return categories;
        }
        #endregion


        #region ImageTbl queries
        public ImageTbl GetImageTblByID(int IDnum)
        {
            ImageTbl image = new ImageTbl();

            image = Images.Where(k => k.ImageTblID == IDnum).FirstOrDefault();

            return image;
        }

        public ImageTbl GetImageTblByFileName(string fileName)
        {
            ImageTbl image = new ImageTbl();

            image = Images.Where(k => k.FileName == fileName).FirstOrDefault();

            return image;
        }

        public List<ImageTbl> GetImageTblsBySpeciesID(int IDnum)
        {
            List<ImageTbl> results = new List<ImageTbl>();

            results = Images.Where(k => k.SpeciesTblID == IDnum).OrderBy(k => k.ImageTblID).ToList();

            return results;
        }

        public List<ImageTbl> GetUniqueImageTblsBySpeciesID(int IDnum)
        {
            List<ImageTbl> results = new List<ImageTbl>();

            results = Images.Where(k => k.SpeciesTblID == IDnum && k.Duplicate == false).OrderBy(k => k.Origin).ThenBy(k => k.ImageTblID).ToList();

            return results;
        }

        public List<ImageTbl> GetUniqueImageTblsByGroupID(int IDnum)
        {
            List<ImageTbl> results = new List<ImageTbl>();

            results = Images.Where(k => k.SpeciesGroupID == IDnum && k.Duplicate == false).OrderBy(k => k.Origin).ThenBy(k => k.ImageTblID).ToList();

            return results;
        }

        public List<ImageTbl> GetImageTblsByFileNames(List<string> fileNames)
        {
            List<ImageTbl> images = new List<ImageTbl>();

            //images = Images.Where(k => fileNames.All(j => j == k.FileName)).ToList();
            images = Images.Where(k => fileNames.Contains(k.FileName)).OrderBy(k => k.ImageTblID).ToList();

            return images;
        }

        public string GetImageNameByFileName(string fileName)
        {            
            string imageName = string.Empty;

            imageName = Images.Where(k => k.FileName == fileName).Select(k => k.ImageName).FirstOrDefault();

            return imageName;
        }

        public List<string> GetImageNamesByFileNames(List<string> fileNames)
        {
            List<string> names = new List<string>();

            //names = Images.Where(k => fileNames.Contains(k.FileName)).OrderBy(k => k.ImageTblID).Select(o => o.ImageName).ToList();
            for (int i = 0; i < fileNames.Count; i++)
            {
                names.Add(Images.Where(k => k.FileName == fileNames[i]).Select(k => k.ImageName).FirstOrDefault());
            }

            return names;
        }

        public List<ImageTbl> GetImagesList()
        {
            List<ImageTbl> imagesList = new List<ImageTbl>();

            imagesList = Images.ToList();

            return imagesList;
        }
        #endregion


        #region EntryImage queries
        public EntryImagesTbl GetEntryImageByID(int IDnum)
        {
            EntryImagesTbl entryImage = new EntryImagesTbl();

            entryImage = EntryImages.Where(k => k.EntryImagesID == IDnum).FirstOrDefault();

            return entryImage;
        }

        public List<EntryImagesTbl> GetEntryImagesList()
        {
            List<EntryImagesTbl> entryImagesList = new List<EntryImagesTbl>();

            entryImagesList = EntryImages.ToList();

            return entryImagesList;
        }
        #endregion


    }
    //public class ImageTbl
    //{
    //    [Key]
    //    public int ImageTblID { get; set; }
    //    public string FileName { get; set; }
    //    public string ImageName { get; set; }
    //    public int Origin { get; set; }
    //    public int? Sort { get; set; }
    //    public int SpeciesGroupID { get; set; }
    //    public int SpeciesID { get; set; }
    //    public virtual SpeciesGroup? SpeciesGroup { get; set; }
    //    public virtual SpeciesTbl? SpeciesTbl { get; set; }
    //}

    //public class Blog
    //{
    //    public int BlogId { get; set; }
    //    public string Url { get; set; }

    //    public List<Post> Posts { get; } = new();
    //}

    //public class Post
    //{
    //    public int PostId { get; set; }
    //    public string Title { get; set; }
    //    public string Content { get; set; }

    //    public int BlogId { get; set; }
    //    public Blog Blog { get; set; }
    //}

}
