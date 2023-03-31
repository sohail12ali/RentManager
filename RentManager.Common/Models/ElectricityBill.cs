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