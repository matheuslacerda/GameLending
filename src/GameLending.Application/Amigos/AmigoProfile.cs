using AutoMapper;

namespace GameLending.Amigos
{
    public class AmigoProfile : Profile
    {
        public AmigoProfile()
        {
            CreateMap<Amigo, AmigoDto>();
            CreateMap<AmigoDto, Amigo>();
        }
    }
}
