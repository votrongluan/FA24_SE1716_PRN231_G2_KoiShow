using System;

namespace KoiShow.Common.DTO.DtoResponse
{
    public class PaymentDtoResponse
    {
        public int Id { get; set; }

        public int? RegisterFormId { get; set; }

        public string TransactionId { get; set; }

        public double? PaymentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string PaymentStatus { get; set; }

        public string Description { get; set; }

        public double? Vatrate { get; set; }

        public double? ActualCost { get; set; }

        public double? DiscountAmount { get; set; }

        public double? FinalAmount { get; set; }

        public string Currency { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public bool Success { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedTime { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
