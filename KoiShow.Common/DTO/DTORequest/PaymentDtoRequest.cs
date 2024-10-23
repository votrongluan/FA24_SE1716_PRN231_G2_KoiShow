using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShow.Common.DTO.DTORequest
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int RegisterFormId { get; set; }
        public string TransactionId { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string OrderType { get; set; }
    }
}
