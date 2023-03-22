using RentManager.Common.Enums;

namespace RentManager.Common.Models;

public class RentDetail
{
    public int RentId { get; set; }
    public int GuestId { get; set; }
    public int BillId { get; set; }
    public float RentAmount { get; set; }
    public Month RentForMonth { get; set; }
    public DateOnly MonthStartDate { get; set; }
    public DateOnly MonthEndDate { get; set; }
    public float OldBalance { get; set; }
    public float PaidBalance { get; set; }
    public float CurrentBalance { get; set; }
    public DateTime? PaidOn { get; set; }
    public string? PaidBy { get; set; }
    public string? PaymentMode { get; set; }
}
