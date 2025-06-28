using System;
using System.Collections.Generic;

namespace PRN_SafeDrive_Aplication.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int RegistrationId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? TransactionCode { get; set; }

    public virtual Registration Registration { get; set; } = null!;
}
