using GameLending.Jogos;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace GameLending.Amigos
{
    public class AmigoAppService : CrudAppService<Amigo, AmigoDto, Guid, PagedAndSortedResultRequestDto>, IAmigoAppService
    {
        private readonly IReadOnlyRepository<Jogo, Guid> _jogoRepository;

        public AmigoAppService(IRepository<Amigo, Guid> repository, IReadOnlyRepository<Jogo, Guid> jogoRepository)
            : base(repository)
        {
            _jogoRepository = jogoRepository;
        }

        public override async Task<PagedResultDto<AmigoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var amigos = await base.GetListAsync(input);

            foreach (var amigo in amigos.Items)
            {
                FillJogos(amigo);
            }

            return amigos;
        }

        private void FillJogos(AmigoDto amigo)
        {
            var jogos = _jogoRepository.Where(p => p.AmigoId == amigo.Id).ToList();

            foreach (var jogo in jogos)
            {
                amigo.Jogos.Add(ObjectMapper.Map<Jogo, JogoDto>(jogo));
            }
        }

        public override async Task<AmigoDto> GetAsync(Guid id)
        {
            var amigo = await base.GetAsync(id);

            FillJogos(amigo);

            return amigo;
        }
    }
}
