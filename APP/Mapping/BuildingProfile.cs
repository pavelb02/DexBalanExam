using AutoMapper;
using BankSystem.App.Dto;
using Domain;

namespace APP.Mapping;

public class BuildingProfile : Profile
{
    public BuildingProfile()
    {
        CreateMap<Building, BuildingDto>();

        CreateMap<BuildingDto, Building>();
    }
}