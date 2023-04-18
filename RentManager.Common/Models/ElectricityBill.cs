using RentManager.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentManager.Common.Models;

public class ElectricityBill
{
    [Key]
    public int BillId { get; set; }

    [ForeignKey(nameof(PayingGuest.GuestId))]
    public int GuestId { get; set; }

    [Required]
    public int BillForMonth { get; set; }

    [Required]
    public DateTimeOffset BillStartDate { get; set; }

    [Required]
    public DateTimeOffset BillEndDate { get; set; }

    [Required]
    public int BilledUnits { get; set; }

    [Required]
    public float PricePerUnit { get; set; }

    [Required]
    public int LastUnit { get; set; }

    [Required]
    public int CurrentUnit { get; set; }

    [Required]
    public int TotalPayableAmount { get; set; }
}

public class EBill : ElectricityBill
{
    public PayingGuest Guest { get; set; }

    public Month BillMonth { get; set; }

    public EBill(ElectricityBill bill, IEnumerable<PayingGuest> guests)
    {
        BillId = bill.BillId;
        GuestId = bill.GuestId;
        BillMonth = (Month)bill.BillForMonth;
        BillStartDate = bill.BillStartDate;
        BillEndDate = bill.BillEndDate;
        BilledUnits = bill.BilledUnits;
        PricePerUnit = bill.PricePerUnit;
        LastUnit = bill.LastUnit;
        CurrentUnit = bill.CurrentUnit;
        TotalPayableAmount = bill.TotalPayableAmount;
        Guest = guests.First(x => x.GuestId.Equals(GuestId));
    }
}