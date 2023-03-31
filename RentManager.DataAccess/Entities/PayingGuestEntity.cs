using Realms;

namespace RentManager.DataAccess.Entities;

public partial class PayingGuestEntity : RealmObject
{
    [PrimaryKey]
    public int GuestId { get; set; }

    public string GuestName { get; set; }
    public string? GuestEmail { get; set; }
    public string GuestPhone { get; set; }
    public string? GuestAddress { get; set; }

    public string? GuestVerifyDocType { get; set; }
    public string? GuestVerifyDoc { get; set; }

    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}