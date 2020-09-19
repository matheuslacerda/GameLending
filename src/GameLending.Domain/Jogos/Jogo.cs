using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace GameLending.Jogos
{
    public class Jogo : FullAuditedEntity<Guid>
    {
        public virtual string Nome { get; private set; }
        public virtual Guid? AmigoId { get; private set; }

        protected Jogo() { }

        public Jogo([NotNull] string nome)
        {
            Nome = nome;
        }

        /// <summary>
        /// Registra um empréstimo ou devolução (amigoId = null).
        /// </summary>
        /// <param name="amigoId"></param>
        public void Emprestar(Guid? amigoId)
        {
            if (AmigoId != null && amigoId != null)
            {
                throw new BusinessException("Não é possível emprestar esse jogo, ele já está emprestado!", nameof(amigoId));
            }
            if (AmigoId == null && amigoId == null)
            {
                throw new BusinessException("Não é possível devolver esse jogo, ele já foi devolvido!", nameof(amigoId));
            }

            AmigoId = amigoId;
        }

        public void TrocarNome(string nome)
        {
            Nome = nome;
        }
    }
}
