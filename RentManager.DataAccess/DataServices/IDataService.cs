using RentManager.Common.Models;

namespace RentManager.DataAccess.DataServices;

public interface IDataService
{
    Task<bool> AddGuest(PayingGuest guest);

    Task<IEnumerable<PayingGuest>> GetAllGuests();
}