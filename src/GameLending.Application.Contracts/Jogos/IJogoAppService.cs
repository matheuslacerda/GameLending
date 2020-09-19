using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace GameLending.Jogos
{
    public interface IJogoAppService : ICrudAppService<JogoDto, Guid, PagedAndSortedResultRequestDto>
    {
    }
}
