using AutoMapper;

using CommunityToolkit.Diagnostics;

using Realms;
using Realms.Exceptions;

using RentManager.Common.Constants;
using RentManager.Common.Helpers;
using RentManager.Common.Models;
using RentManager.DataAccess.Entities;

using System.Diagnostics;

namespace RentManager.DataAccess.DataServices;

public class DataService : IDataService
{
    private readonly IMapper mapper;
    private readonly Realm realm;

    private readonly RealmConfiguration realmConfig;

    public DataService(IMapper mapper)
    {
        this.mapper = mapper;
        try
        {
            realmConfig = new()
            {
                SchemaVersion = AppConstants.RealmDatabase.CurrentVersion,
                ShouldDeleteIfMigrationNeeded = false,
            };

            realm = Realm.GetInstance(realmConfig);
        }
        catch (RealmMigrationNeededException ex)
        {
            Debug.WriteLine(ex);
#if DEBUG
            Realm.DeleteRealm(realmConfig);
            realm = Realm.GetInstance(realmConfig);
#endif
        }
        catch (RealmException ex)
        {
            Debug.WriteLine(ex);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private byte[] GetEncryptionKey()
    {
        string key = SecureStorage.Default.GetAsync(AppConstants.RealmDatabase.EnKey).Result;
        if (string.IsNullOrEmpty(key))
        {
            var bytes = EncryptionHelper.GetEncryptionKey();
            key = EncryptionHelper.ConvertByteArrayToBase64String(bytes);
            SecureStorage.Default.SetAsync(AppConstants.RealmDatabase.EnKey, key).Wait();
            return bytes;
        }
        else
        {
            var bytes = EncryptionHelper.ConvertBase64StringToByteArray(key);
            return bytes;
        }
    }

    public async Task<bool> AddGuest(PayingGuest guest)
    {
        try
        {
            Guard.IsNotNull(guest);
            var guestEntity = mapper.Map<PayingGuestEntity>(guest);
            Guard.IsNotNull(guestEntity);

            int max = realm.All<PayingGuestEntity>()?.OrderByDescending(x => x.GuestId)?.FirstOrDefault()?.GuestId ?? 0;
            guestEntity.GuestId = max + 1;
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

    public async Task<bool> AddElectricityBill(ElectricityBill bill)
    {
        try
        {
            Guard.IsNotNull(bill);
            var entities = mapper.Map<ElectricityBillEntity>(bill);
            Guard.IsNotNull(entities);

            int max = realm.All<ElectricityBillEntity>()?.OrderByDescending(x => x.BillId)?.FirstOrDefault()?.BillId ?? 0;
            entities.BillId = max + 1;
            await realm.WriteAsync(() =>
            {
                realm.Add(entities);
            });

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public Task<IEnumerable<ElectricityBill>> GetAllElectricityBills()
    {
        try
        {
            var payingGuestEntities = realm.All<ElectricityBillEntity>();
            IEnumerable<ElectricityBill> guests = mapper.Map<IEnumerable<ElectricityBill>>(payingGuestEntities).OrderByDescending(x => x.GuestId);
            return Task.FromResult(guests);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            throw;
        }
    }
}