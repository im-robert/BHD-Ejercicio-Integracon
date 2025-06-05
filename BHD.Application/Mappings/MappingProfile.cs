using AutoMapper;
using BHD.Application.DTOs;
using BHD.Domain.Entities;

namespace BHD.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PhoneDto, Phone>();
        CreateMap<User, RegisterUserResponseDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src=> src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}
