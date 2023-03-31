using Realms;

namespace RentManager.DataAccess.Entities;

public partial class RentDetailEntity : RealmObject
{
    [PrimaryKey]
    public int RentId { get; set; }

    public int GuestId { get; set; }
    public int BillId { get; set; }
    public float RentAmount { get; set; }
    public int RentForMonth { get; set; }
    public DateTimeOffset MonthStartDate { get; set; }
    public DateTimeOffset MonthEndDate { get; set; }
    public float OldBalance { get; set; }
    public float PaidBalance { get; set; }
    public float CurrentBalance { get; set; }
    public DateTimeOffset? PaidOn { get; set; }
    public string? PaidBy { get; set; }
    public string? PaymentMode { get; set; }
}