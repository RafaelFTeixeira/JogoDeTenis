using Xunit;

namespace JogoDeTenis.Teste
{
    public class JogoTeste
    {
        private const Jogador JogadorAzul = Jogador.Azul;
        private const Jogador JogadorVermelho = Jogador.Vermelho;

        [Fact]
        public void Deve_inicializar_um_placar_zerado()
        {
            const string placarZerado = "0 0";

            var jogo = new Jogo();

            Assert.Equal(placarZerado, jogo.ObterPlacar());
        }

        [Fact]
        public void Deve_pontuar_um_jogador()
        {
            const string placarDoJogo = "15 0";
            var jogo = new Jogo();

            jogo.Pontuar(JogadorAzul);

            Assert.Equal(placarDoJogo, jogo.ObterPlacar());
        }

        [Fact]
        public void Deve_acumular_a_pontuacao_do_jogador()
        {
            const string placarDoJogo = "0 30";
            var jogo = new Jogo();

            jogo.Pontuar(JogadorVermelho, JogadorVermelho);

            Assert.Equal(placarDoJogo, jogo.ObterPlacar());
        }

        [Theory]
        [InlineData(new[] { JogadorVermelho }, "0 15")]
        [InlineData(new[] { JogadorVermelho, JogadorVermelho }, "0 30")]
        [InlineData(new[] { JogadorVermelho, JogadorVermelho, JogadorVermelho }, "0 40")]
        [InlineData(new[] { JogadorAzul }, "15 0")]
        [InlineData(new[] { JogadorAzul, JogadorAzul }, "30 0")]
        [InlineData(new[] { JogadorAzul, JogadorAzul, JogadorAzul }, "40 0")]
        [InlineData(new[] { JogadorAzul, JogadorAzul, JogadorVermelho }, "30 15")]
        [InlineData(new[] { JogadorAzul, JogadorVermelho, JogadorVermelho }, "15 30")]
        [InlineData(new[] { JogadorAzul, JogadorAzul, JogadorAzul, JogadorVermelho }, "40 15")]
        [InlineData(new[] { JogadorAzul, JogadorVermelho, JogadorVermelho, JogadorVermelho }, "15 40")]
        [InlineData(new[] { JogadorVermelho, JogadorAzul }, "15 15")]
        [InlineData(new[] { JogadorAzul, JogadorAzul, JogadorVermelho, JogadorVermelho }, "30 30")]
        public void Deve_atualizar_o_placar_do_jogo_para_cada_jogada(Jogador[] jogadas, string placarDoJogo)
        {
            var jogo = new Jogo();
            
            jogo.Pontuar(jogadas);

            Assert.Equal(placarDoJogo, jogo.ObterPlacar());
        }

        [Fact]
        public void Quando_der_um_empate_de_pontuacao_maxima_o_placar_deve_informar_deuce()
        {
            var jogo = new Jogo();
            var jogadas = new[] {
                JogadorAzul, JogadorAzul, JogadorAzul,
                JogadorVermelho, JogadorVermelho, JogadorVermelho
            };

            jogo.Pontuar(jogadas);

            Assert.Equal("deuce", jogo.ObterPlacar());
        }
    }
}
