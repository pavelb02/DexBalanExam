using AutoMapper;
using BankSystem.App.Dto;
using Domain;

namespace APP.Mapping;

public class SensorProfile : Profile
{
    public SensorProfile()
    {
        CreateMap<Sensor,SensorDto>();

        CreateMap<SensorDto, Sensor>();
    }
}