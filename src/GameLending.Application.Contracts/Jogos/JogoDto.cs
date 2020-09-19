using GameLending.Amigos;
using System;
using Volo.Abp.Application.Dtos;

namespace GameLending.Jogos
{
    public class JogoDto : EntityDto<Guid>
    {
        public string Nome { get; set; }

        public Guid? AmigoId { get; set; }

        public AmigoDto Amigo { get; set; }
    }
}
