﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShow.Data.DTO.PointDTO
{
    public class PointResponseDTO
    {
        public int Id { get; set; }

        public string? AnimalName { get; set; }

        public string? ContestName { get; set; }

        public int? ShapePoint { get; set; }

        public int? ColorPoint { get; set; }

        public int? PatternPoint { get; set; }

        public int? TotalScore { get; set; }

        public string? Comment { get; set; }

        public int? JuryId { get; set; }

        public string JuryName { get; set; }

        public int? RegisterFormId { get; set; }

        public int? PointStatus { get; set; }

        public string? JudgeRank { get; set; }

        public int? Penalties { get; set; }
    }
}
