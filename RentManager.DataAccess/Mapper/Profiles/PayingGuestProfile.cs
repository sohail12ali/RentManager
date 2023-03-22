using AutoMapper;

using RentManager.Common.Models;
using RentManager.DataAccess.Entities;

namespace RentManager.DataAccess.Mapper.Profiles;

public class PayingGuestProfile  : Profile
{
    public PayingGuestProfile()
    {
        CreateMap<PayingGuest, PayingGuestEntity>();
        CreateMap<PayingGuestEntity, PayingGuest>();
    }
}
