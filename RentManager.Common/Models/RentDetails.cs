namespace RentManager.Common.Models;

public class RentDetails
{
    public int RentId { get; set; }
    public int GuestId { get; set; }
    public float RentAmount { get; set; }
    public DateOnly RentForMonth { get; set; }
    public DateOnly MonthStartDate { get; set; }
    public DateOnly MonthEndDate { get; set; }
    public float OldBalance { get; set; }
    public float PaidBalance { get; set; }
    public float CurrentBalance { get; set; }

}
