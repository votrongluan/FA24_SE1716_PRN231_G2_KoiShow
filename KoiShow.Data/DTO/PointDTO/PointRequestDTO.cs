using System.ComponentModel.DataAnnotations;

namespace KoiShow.Data.DTO.PointDTO
{
    public class PointRequestDTO
    {
        [Required(ErrorMessage = "Register Form is required.")]
        public int RegisterFormId { get; set; }

        public int? JuryId { get; set; }

        [Range(1, 10, ErrorMessage = "Shape Point must be between 1 and 10.")]
        public int ShapePoint { get; set; }

        [Range(1, 10, ErrorMessage = "Color Point must be between 1 and 10.")]
        public int ColorPoint { get; set; }

        [Range(1, 10, ErrorMessage = "Pattern Point must be between 1 and 10.")]
        public int PatternPoint { get; set; }

        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Point Status is required.")]
        public int PointStatus { get; set; }

        [StringLength(100, ErrorMessage = "Judge Rank cannot exceed 100 characters.")]
        public string JudgeRank { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Penalties must be a positive integer.")]
        public int Penalties { get; set; }

        public int? TotalScore { get; set; }
    }
}
