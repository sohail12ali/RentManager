using System.ComponentModel.DataAnnotations;

namespace RentManager.Common.Models;

public class PayingGuest
{
    [Key]
    public int GuestId { get; set; }
    [Required]
    [StringLength(50), MinLength(3)]
    public string GuestName { get; set; }

    [StringLength(50)]
    [EmailAddress]
    public string? GuestEmail { get; set; }
    [Required]
    [StringLength(20)]
    [Phone]
    public string GuestPhone { get; set; }
    [Required]
    [StringLength(250)]
    public string? GuestAddress { get; set; }
    [StringLength(20)]
    public string? GuestVerifyDocType { get; set; }
    [StringLength(50)]
    public string? GuestVerifyDoc { get; set; }

    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}