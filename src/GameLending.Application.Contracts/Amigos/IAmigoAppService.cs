using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace GameLending.Amigos
{
    public interface IAmigoAppService : ICrudAppService<AmigoDto, Guid, PagedAndSortedResultRequestDto>
    {
    }
}
