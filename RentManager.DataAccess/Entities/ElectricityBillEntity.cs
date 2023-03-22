using Realms;

using RentManager.Common.Enums;

namespace RentManager.DataAccess.Entities;

public class ElectricityBillEntity : RealmObject
{
    [PrimaryKey]
    public int BillId { get; set; }
    public int GuestId { get; set; }
    public float BillAmount { get; set; }
    public Month BillForMonth { get; set; }
    public DateOnly BillStartDate { get; set; }
    public DateOnly BillEndDate { get; set; }
    public int BilledUnits { get; set; }
    public float PricePerUnit { get; set; }
    public int LastUnit { get; set; }
    public int CurrentUnit { get; set; }
    public int TotalPayableUnits { get; set; }
}
