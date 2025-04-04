using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhanaWoods.Database
{
    //internal class DataAccess
    //{
    //}

    public class KeyEntry
    {
        [Key]
        public int KeyEntryID { get; set; }
        public int DecisionNum { get; set; }
        public string? Text0 { get; set; }
        public string? Text0ES { get; set; }
        public int? TargetID0 { get; set; }
        public string? TargetGroupsIDs0 { get; set; }
        public string? TargetGroupsNames0 { get; set; }
        public string? TargetGroupsNames0ES { get; set; }
        public string? Text1 { get; set; }
        public string? Text1ES { get; set; }
        public int? TargetID1 { get; set; }
        public string? TargetGroupsIDs1 { get; set; }
        public string? TargetGroupsNames1 { get; set; }
        public string? TargetGroupsNames1ES { get; set; }
        public virtual KeyHistory? KeyHistory { get; set; }
        //public virtual ICollection<EntryImages>? EntryImages { get; set; }
        public virtual EntryImagesTbl? EntryImages { get; set; }
    }

    public class KeyHistory
    {
        [Key]
        public int KeyHistoryID { get; set; }
        public string? PrevDecisions { get; set; }
        public string? GreyDecisions { get; set; }
        public int KeyEntryID { get; set; }
        public virtual KeyEntry? KeyEntry { get; set; }
    }

    public class SpeciesGroup
    {
        [Key]
        public int SpeciesGroupID { get; set; }
        public string Name { get; set; }
        public string? NameES { get; set; }
        public bool Spp { get; set; }
        public string Family { get; set; }
        public string? Species { get; set; }
        public string? ImageNames { get; set; }
        public string FileNames { get; set; }
        public string? Transverse { get; set; }
        public string? TransverseES { get; set; }
        public string? Porosity { get; set; }
        public string? PorosityES { get; set; }
        public string? Vessels { get; set; }
        public string? VesselsES { get; set; }
        public string? Rays { get; set; }
        public string? RaysES { get; set; }
        public string? Parenchyma { get; set; }
        public string? ParenchymaES { get; set; }
        public string Tangential { get; set; }
        public string? TangentialES { get; set; }
        public string OtherCharacters { get; set; }
        public string? OtherCharactersES { get; set; }
        public string AdditionalComments { get; set; }
        public string? AdditionalCommentsES { get; set; }
        public string SimilarWoods { get; set; }
        public string? SimilarWoodsES { get; set; }
        public string? SimilarImageNames { get; set; }
        public string SimilarFileNames { get; set; }
        public virtual ICollection<SpeciesTbl>? SpeciesTbl { get; set; }
        public virtual ICollection<ImageTbl>? ImageTbl { get; set; }
        public virtual ICollection<ImageTbl>? ImageTbls { get; set; }

        public SpeciesGroup()
        {
            Name = " ";
            Family = " ";
            FileNames = " ";
            Tangential = " ";
            OtherCharacters = " ";
            AdditionalComments = " ";
            SimilarWoods = " ";
            SimilarFileNames = " ";
        }
    }

    public class SpeciesTbl
    {
        [Key]
        public int SpeciesTblID { get; set; }
        public string Name { get; set; }
        public string? LocalName { get; set; }
        public string? TradeName { get; set; }
        public int SpeciesGroupID { get; set; }
        public int Type { get; set; }
        public string? Features { get; set; }
        public virtual SpeciesGroup? SpeciesGroup { get; set; }
        public virtual ICollection<ImageTbl>? ImageTbl { get; set; }

        public SpeciesTbl()
        {
            Name = " ";
        }
    }

    public class ImageTbl
    {
        [Key]
        public int ImageTblID { get; set; }
        public string FileName { get; set; }
        public string ImageName { get; set; }
        public int Origin { get; set; }
        //public int? Sort { get; set; }
        public bool? Duplicate { get; set; }
        public int? SpeciesPageID { get; set; }
        public int? SpeciesGroupID { get; set; }
        public int? SpeciesTblID { get; set; }
        public virtual SpeciesGroup? SpeciesPageGroup { get; set; }
        public virtual SpeciesGroup? SpeciesGroup { get; set; }
        public virtual SpeciesTbl? SpeciesTbl { get; set; }
        //public virtual ICollection<EntryImages>? EntryImage { get; set; }

        public ImageTbl()
        {
            FileName = " ";
            ImageName = " ";
        }
    }

    //public class EntryImage
    //{
    //    [Key]
    //    public int EntryImageID { get; set; }
    //    public string? ImageName { get; set; }
    //    public string? ImageNameES { get; set; }
    //    public int KeyEntryID { get; set; }
    //    public int? ImageTblID { get; set; }
    //    public virtual KeyEntry? KeyEntry { get; set; }
    //    public virtual ImageTbl? ImageTbl { get; set; }
    //}

    public class EntryImagesTbl
    {
        [Key]
        public int EntryImagesID { get; set; }
        public string? FileNames { get; set; }
        public string? FileNames1 { get; set; }
        public string? SpeciesGroup { get; set; }
        public string? SpeciesGroup1 { get; set; }
        public int? Species { get; set; }
        public int? Species1 { get; set; }
        public bool ExampleImages { get; set; }
        public bool ExampleImages1 { get; set; }
        public int KeyEntryID { get; set; }
        public virtual KeyEntry? KeyEntry { get; set; }
    }
}
