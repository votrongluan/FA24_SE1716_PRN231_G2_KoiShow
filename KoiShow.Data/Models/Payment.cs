﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using KoiShow.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiShow.Data.Models;

#nullable enable
public partial class Payment : BaseEntity
{
    public int? RegisterFormId { get; set; }

    public string? TransactionId { get; set; }

    public double? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Description { get; set; }

    public double? Vatrate { get; set; }

    public double? ActualCost { get; set; }

    public double? DiscountAmount { get; set; }

    public double? FinalAmount { get; set; }

    public string? Currency { get; set; }

    public string? OrderType { get; set; }

    [ForeignKey(nameof(RegisterFormId))]
    public virtual RegisterForm? RegisterForm { get; set; }
}