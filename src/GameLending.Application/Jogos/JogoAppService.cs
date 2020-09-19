using GameLending.Amigos;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace GameLending.Jogos
{
    public class JogoAppService : CrudAppService<Jogo, JogoDto, Guid, PagedAndSortedResultRequestDto>, IJogoAppService
    {
        private readonly IReadOnlyRepository<Amigo, Guid> _amigoRepository;

        public JogoAppService(IRepository<Jogo, Guid> repository, IReadOnlyRepository<Amigo, Guid> amigoRepository)
            : base(repository)
        {
            _amigoRepository = amigoRepository;
        }

        public override async Task<PagedResultDto<JogoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var jogos = await base.GetListAsync(input);

            foreach (var jogo in jogos.Items)
            {
                await GetAmigo(jogo);
            }

            return jogos;
        }

        private async Task GetAmigo(JogoDto jogo)
        {
            if (jogo.AmigoId.HasValue)
            {
                jogo.Amigo = ObjectMapper.Map<Amigo, AmigoDto>(await _amigoRepository.GetAsync(jogo.AmigoId.Value));
            }
        }

        public override async Task<JogoDto> GetAsync(Guid id)
        {
            var jogo = await base.GetAsync(id);

            await GetAmigo(jogo);

            return jogo;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        public override async Task<JogoDto> UpdateAsync(Guid id, JogoDto input)
        {
            Check.NotNull(input, nameof(input));

            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            if (!entity.Nome.Equals(input.Nome, StringComparison.InvariantCulture))
            {
                entity.TrocarNome(input.Nome);
            }

            if (entity.AmigoId != input.AmigoId)
            {
                entity.Emprestar(input.AmigoId);
            }

            await Repository.UpdateAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
