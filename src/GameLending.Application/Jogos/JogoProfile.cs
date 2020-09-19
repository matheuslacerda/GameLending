using AutoMapper;

namespace GameLending.Jogos
{
    public class JogoProfile : Profile
    {
        public JogoProfile()
        {
            CreateMap<Jogo, JogoDto>();
            CreateMap<JogoDto, Jogo>();
        }
    }
}
