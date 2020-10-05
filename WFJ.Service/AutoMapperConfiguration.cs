using AutoMapper;
using WFJ.Models;
using WFJ.Repository.EntityModel;
namespace WFJ.Service
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Client, ClientModel>();
                cfg.CreateMap<User, UserModel>()
                    .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Client.ClientName))
                    .ForMember(dest => dest.LevelName, act => act.MapFrom(src => src.Level.Name))
                    .ForMember(dest => dest.AccessLevelName, act => act.MapFrom(src => src.AccessLevel.AccessLevel1));
            });

            Mapper = MapperConfiguration.CreateMapper();

        }
        public static IMapper Mapper { get; set; }

        public static MapperConfiguration MapperConfiguration { get; set; }
    }
}
