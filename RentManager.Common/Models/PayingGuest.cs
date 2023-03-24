namespace RentManager.Common.Models;

public class PayingGuest
{
    public int GuestId { get; set; }
    public string GuestName { get; set; }
    public string? GuestEmail { get; set; }
    public string GuestPhone { get; set; }
    public string? GuestAddress { get; set; }

    public string? GuestVerifyDocType { get; set; }
    public string? GuestVerifyDoc { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
}