using System;
using System.Collections.Generic;
using Volo.Abp;
using Xunit;

namespace GameLending.Jogos
{
    public class JogoTests : GameLendingDomainTestBase
    {
        public static IEnumerable<object[]> GuidsSucesso
        {
            get
            {
                yield return new object[] { Guid.NewGuid(), null };
                yield return new object[] { null, Guid.NewGuid() };
            }
        }

        [Theory, MemberData(nameof(GuidsSucesso))]
        public void ShouldEmprestar(Guid? antigoAmigo, Guid? novoAmigo)
        {
            var jogo = new Jogo("Civilization IV");
            if (antigoAmigo != null)
            {
                jogo.Emprestar(antigoAmigo);
            }

            jogo.Emprestar(novoAmigo);

            Assert.Equal(novoAmigo, jogo.AmigoId);
        }

        public static IEnumerable<object[]> GuidsFail
        {
            get
            {
                yield return new object[] { null, null };
                yield return new object[] { Guid.NewGuid(), Guid.NewGuid() };
            }
        }

        [Theory, MemberData(nameof(GuidsFail))]
        public void ShouldFailEmprestar(Guid? antigoAmigo, Guid? novoAmigo)
        {
            var jogo = new Jogo("Civilization IV");
            if (antigoAmigo != null)
            {
                jogo.Emprestar(antigoAmigo);
            }

            Assert.Throws<BusinessException>(() => jogo.Emprestar(novoAmigo));
        }
    }
}
