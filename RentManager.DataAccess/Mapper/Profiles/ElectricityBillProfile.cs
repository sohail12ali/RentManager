using AutoMapper;

using RentManager.Common.Models;
using RentManager.DataAccess.Entities;

namespace RentManager.DataAccess.Mapper.Profiles;

public class ElectricityBillProfile : Profile
{
    public ElectricityBillProfile()
    {
        CreateMap<ElectricityBill, ElectricityBillEntity>();
        CreateMap<ElectricityBillEntity, ElectricityBill>();
    }
}