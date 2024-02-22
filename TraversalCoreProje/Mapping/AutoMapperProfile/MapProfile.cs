using AutoMapper;
using DTOLayer.DTOs.AnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.CityDTOs;
using DTOLayer.DTOs.ContactDTOs;
using EntityLayer.Concrete;


namespace TraversalCoreProje.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<AnouncementAddDTOs, Announcement>();
            CreateMap<Announcement, AnouncementAddDTOs>();

            CreateMap<AppUserRegisterDTOs, AppUser>();
            CreateMap<AppUser, AppUserRegisterDTOs>();

            CreateMap<AppUserLoginDTOs, AppUser>();
            CreateMap<AppUser, AppUserLoginDTOs>();

            CreateMap<AnouncementListDTO, Announcement>();
            CreateMap<Announcement, AnouncementListDTO>();

            CreateMap<AnouncementUpdateDto, Announcement>();
            CreateMap<Announcement, AnouncementUpdateDto>();

            CreateMap<SendMessageDto,ContactUs>().ReverseMap();
 
        }
    }
}
