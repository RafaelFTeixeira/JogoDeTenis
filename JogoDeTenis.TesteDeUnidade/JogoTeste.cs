using NUnit.Framework;

namespace JogoDeTenis.TesteDeUnidade
{
    [TestFixture]
    public class JogoTeste
    {
        private Jogo _jogo;
        private const Jogador JogadorEsquerdo = Jogador.Esquerdo;
        private const Jogador JogadorDireito = Jogador.Direito;
        
        [SetUp]
        public void Init()
        {
            _jogo = new Jogo();
        }

        [Test]
        public void Deve_inicializar_um_placar_zerado()
        {
            const string placarZerado = "0 0";

            Assert.AreEqual(placarZerado, _jogo.ObterPlacar());
        }

        [Test]
        public void Deve_pontuar_um_jogador()
        {
            const string placarDoJogo = "15 0";

            _jogo.Pontuar(JogadorEsquerdo);

            Assert.AreEqual(placarDoJogo, _jogo.ObterPlacar());
        }

        [Test]
        public void Deve_acumular_a_pontuacao_do_jogador()
        {
            const string placarDoJogo = "0 30";
            
            _jogo.Pontuar(JogadorDireito, JogadorDireito);

            Assert.AreEqual(placarDoJogo, _jogo.ObterPlacar());
        }

        [TestCase(new[] { JogadorDireito }, "0 15")]
        [TestCase(new[] { JogadorDireito, JogadorDireito }, "0 30")]
        [TestCase(new[] { JogadorDireito, JogadorDireito, JogadorDireito }, "0 40")]
        [TestCase(new[] { JogadorEsquerdo }, "15 0")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo }, "30 0")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo }, "40 0")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorDireito }, "30 15")]
        [TestCase(new[] { JogadorEsquerdo, JogadorDireito, JogadorDireito }, "15 30")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorDireito }, "40 15")]
        [TestCase(new[] { JogadorEsquerdo, JogadorDireito, JogadorDireito, JogadorDireito }, "15 40")]
        [TestCase(new[] { JogadorDireito, JogadorEsquerdo }, "15 15")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorDireito, JogadorDireito }, "30 30")]
        public void Deve_atualizar_o_placar_do_jogo_para_cada_jogada(Jogador[] jogadas, string placarDoJogo)
        {
            _jogo.Pontuar(jogadas);

            Assert.AreEqual(placarDoJogo, _jogo.ObterPlacar());
        }

        [Test]
        public void Quando_der_um_empate_de_quarentena_no_placar_deve_informar_deuce()
        {
            var jogadas = new[] {
                JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
                JogadorDireito, JogadorDireito, JogadorDireito
            };

            _jogo.Pontuar(jogadas);

            Assert.AreEqual("deuce", _jogo.ObterPlacar());
        }

        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
            JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito }, "40 advantage")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
            JogadorDireito, JogadorDireito, JogadorDireito }, "advantage 40")]
        public void Deve_pontuar_advantage_para_o_jogador_que_realizar_uma_pontuacao_no_placar_deuce(Jogador[] jogadas, string placarDoJogo)
        {
            _jogo.Pontuar(jogadas);

            Assert.AreEqual(placarDoJogo, _jogo.ObterPlacar());
        }

        [Test]
        public void Deve_voltar_a_ser_deuce_quando_os_dois_jogadores_estiver_com_a_pontuacao_acima_de_quarentena()
        {
            var jogadas = new[] {
                JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
                JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito
            };

            _jogo.Pontuar(jogadas);

            Assert.AreEqual("deuce", _jogo.ObterPlacar());
        }

        [TestCase(new[] { JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito }, "Jogador da direita venceu")]
        [TestCase(new[] { JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo }, "Jogador da esquerda venceu")]
        public void Deve_ganhar_o_jogo_se_o_jogador_com_40_pontos_pontuar_novamente(Jogador[] jogadas, string placarDoJogo)
        {
            _jogo.Pontuar(jogadas);

            Assert.AreEqual(placarDoJogo, _jogo.ObterPlacar());
        }

        [TestCase(new[]
        {
            JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo,
            JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito, JogadorDireito
        },"Jogador da direita venceu")]
        [TestCase(new[]
        {
            JogadorDireito, JogadorDireito, JogadorDireito,
            JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo, JogadorEsquerdo
        }, "Jogador da esquerda venceu")]
        public void Deve_vencer_o_jogador_que_realizar_duas_pontuacoes_seguidas_apos_de_um_deuce(Jogador[] jogadas, string placarDoJogo)
        {
            _jogo.Pontuar(jogadas);

            Assert.AreEqual(placarDoJogo, _jogo.ObterPlacar());
        }
    }
}
