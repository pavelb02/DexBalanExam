using AutoMapper;
using BankSystem.App.Dto;
using Domain;

namespace APP.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<UserDto, User>();
    }
}