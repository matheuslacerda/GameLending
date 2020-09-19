using GameLending.Jogos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace GameLending.Amigos
{
    public class AmigoDto : EntityDto<Guid>
    {
        public string Nome { get; set; }

        public IList<JogoDto> Jogos { get; }

        public AmigoDto()
        {
            Jogos = new List<JogoDto>();
        }
    }
}
