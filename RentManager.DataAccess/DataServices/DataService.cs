using AutoMapper;

using CommunityToolkit.Diagnostics;

using Realms;

using RentManager.Common.Models;
using RentManager.DataAccess.Entities;

using System.Diagnostics;

namespace RentManager.DataAccess.DataServices;

public class DataService : IDataService
{
    private readonly IMapper mapper;
    private readonly Realm realm;

    public DataService(IMapper mapper)
    {
        this.mapper = mapper;
        realm = Realm.GetInstance();
    }

    public async Task<bool> AddGuest(PayingGuest guest)
    {
        try
        {
            Guard.IsNotNull(guest);
            var guestEntity = mapper.Map<PayingGuestEntity>(guest);
            Guard.IsNotNull(guestEntity);

            await realm.WriteAsync(() =>
            {
                realm.Add(guestEntity);
            });

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public Task<IEnumerable<PayingGuest>> GetAllGuests()
    {
        try
        {
            var payingGuestEntities = realm.All<PayingGuestEntity>();
            IEnumerable<PayingGuest> guests = mapper.Map<IEnumerable<PayingGuest>>(payingGuestEntities);
            return Task.FromResult(guests);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            throw;
        }
    }
}