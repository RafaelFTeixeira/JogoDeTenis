using Xunit;

namespace JogoDeTenis.Teste
{
    public class JogoTeste
    {
        private Jogo _jogo;
        private const Jogador JogadorEsquerdo = Jogador.Esquerdo;
        private const Jogador JogadorDireito = Jogador.Direito;


        public JogoTeste()
        {
            _jogo = new Jogo();
        }

        [Fact]
        public void Deve_inicializar_um_placar_zerado()
        {
            const string placarZerado = "0 0";

            Assert.Equal(placarZerado, _jogo.ObterPlacar());
        }

        [Fact]
        public void Deve_pontuar_um_jogador()
        {
            const string placarDoJogo = "15 0";

            _jogo.Pontuar(JogadorEsquerdo);

            Assert.Equal(placarDoJogo, _jogo.ObterPlacar());
        }

        [Fact]
        public void Deve_acumular_a_pontuacao_do_jogador()
        {
            const string placarDoJogo = "0 30";
            
            _jogo.Pontuar(JogadorDireito, JogadorDireito);

            Assert.Equal(placarDoJogo, _jogo.ObterPlacar());
        }

        [Theory]
        [InlineData(new[] { JogadorDireito }, "0 15")]
        [InlineData(new[] { JogadorDireito, JogadorDireito }, "0 30")]
        [InlineData(new[] { JogadorDireito, JogadorDireito, JogadorDireito }, "0 40")]
        [InlineData(new[] { JogadorEsquerdo }, "15 0")]
        [InlineData(new[] { JogadorEsquerdo, JogadorEsquerdo }, "30 0")]
        [InlineData(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo }, "40 0")]
        [InlineData(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorDireito }, "30 15")]
        [InlineData(new[] { JogadorEsquerdo, JogadorDireito, JogadorDireito }, "15 30")]
        [InlineData(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorDireito }, "40 15")]
        [InlineData(new[] { JogadorEsquerdo, JogadorDireito, JogadorDireito, JogadorDireito }, "15 40")]
        [InlineData(new[] { JogadorDireito, JogadorEsquerdo }, "15 15")]
        [InlineData(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorDireito, JogadorDireito }, "30 30")]
        public void Deve_atualizar_o_placar_do_jogo_para_cada_jogada(Jogador[] jogadas, string placarDoJogo)
        {
            _jogo.Pontuar(jogadas);

            Assert.Equal(placarDoJogo, _jogo.ObterPlacar());
        }

        [Fact]
        public void Quando_der_um_empate_de_quarentena_no_placar_deve_informar_deuce()
        {
            var jogadas = new[] {
                JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
                JogadorDireito, JogadorDireito, JogadorDireito
            };

            _jogo.Pontuar(jogadas);

            Assert.Equal("deuce", _jogo.ObterPlacar());
        }

        [Fact]
        public void deve_pontuar_advantage_para_o_jogador_que_realizar_uma_pontuacao_no_placar_deuce()
        {
            var jogadas = new[] {
                JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
                JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito
            };

            _jogo.Pontuar(jogadas);

            Assert.Equal("40 advantage", _jogo.ObterPlacar());
        }

        [Fact]
        public void deve_voltar_a_ser_deuce_quando_os_dois_jogadores_estiver_com_a_pontuacao_acima_de_quarentena()
        {
            var jogadas = new[] {
                JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
                JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito
            };

            _jogo.Pontuar(jogadas);

            Assert.Equal("deuce", _jogo.ObterPlacar());
        }
    }
}
