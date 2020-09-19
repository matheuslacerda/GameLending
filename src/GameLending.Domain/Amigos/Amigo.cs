using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace GameLending.Amigos
{
    public class Amigo : FullAuditedEntity<Guid>
    {
        public virtual string Nome { get; private set; }

        protected Amigo() { }

        public Amigo([NotNull] string nome)
        {
            Nome = nome;
        }
    }
}
