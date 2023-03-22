using AutoMapper;

using RentManager.Common.Models;
using RentManager.DataAccess.Entities;

namespace RentManager.DataAccess.Mapper.Profiles;
public class RentDetailProfile : Profile
{
    public RentDetailProfile()
    {
        CreateMap<RentDetail, RentDetailEntity>();
        CreateMap<RentDetailEntity, RentDetail>();
    }
}
