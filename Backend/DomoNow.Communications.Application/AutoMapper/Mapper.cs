using AutoMapper;
using DomoNow.Communications.Application.Dtos;
using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
