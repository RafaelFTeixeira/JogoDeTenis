using System.Linq;

namespace JogoDeTenis
{
    public class Jogo
    {
        private readonly int[] _pontuacao;
        private const string Deuce = "40";
        private const string Advantage = "advantage";
        private const string Win = "win";
        private static string[] PlacarDoJogo => new[] { "0", "15", "30", Deuce, Advantage, Win };
        private static int IndiceDaPontuacao(string nomeDaPontuacao) =>
            PlacarDoJogo.Select((value, index) => new { value, index })
                .First(pontuacao => pontuacao.value == nomeDaPontuacao).index;


        public Jogo()
        {
            _pontuacao = new[] { 0, 0 };
        }

        public void Pontuar(params Jogador[] jogadas)
        {
            foreach (var jogador in jogadas)
                _pontuacao[(int)jogador]++;

            RetormarParaDeuceQuandoOsJogadoresSeIgualarComoAdvantage();
        }

        private void RetormarParaDeuceQuandoOsJogadoresSeIgualarComoAdvantage()
        {
            if (EstaComPontuacaoDeAdvantage(Jogador.Esquerdo) &&
                EstaComPontuacaoDeAdvantage(Jogador.Direito))
            {
                _pontuacao[(int)Jogador.Esquerdo]--;
                _pontuacao[(int)Jogador.Direito]--;
            }
        }

        private bool EstaComPontuacaoDeAdvantage(Jogador jogador)
        {
            var indiceDaPontuacaoDoDeuce = IndiceDaPontuacao(Deuce);
            return _pontuacao[(int)jogador] > indiceDaPontuacaoDoDeuce;
        }

        public string ObterPlacar()
        {
            var indiceDaPontuacaoDoJogadorEsquerdo = ObterIndiceDaPontuacaoDo(Jogador.Esquerdo);
            var indiceDaPontuacaoDoJogadorDireito = ObterIndiceDaPontuacaoDo(Jogador.Direito);

            if (EhDeuce(indiceDaPontuacaoDoJogadorEsquerdo, indiceDaPontuacaoDoJogadorDireito))
                return "deuce";
            if (EhVencedor(indiceDaPontuacaoDoJogadorEsquerdo, indiceDaPontuacaoDoJogadorDireito))
                return "Jogador da esquerda venceu";
            if (EhVencedor(indiceDaPontuacaoDoJogadorDireito, indiceDaPontuacaoDoJogadorEsquerdo))
                return "Jogador da direita venceu";
            return $"{PlacarDoJogo[indiceDaPontuacaoDoJogadorEsquerdo]} {PlacarDoJogo[indiceDaPontuacaoDoJogadorDireito]}";
        }

        private static bool EhVencedor(int indiceDaPontuacaoDoJogadorVencedor, int indiceDaPontuacaoAdversario)
        {
            return indiceDaPontuacaoDoJogadorVencedor == IndiceDaPontuacao(Advantage) &&
                   indiceDaPontuacaoAdversario < IndiceDaPontuacao(Deuce) ||
                   indiceDaPontuacaoDoJogadorVencedor == IndiceDaPontuacao(Win) &&
                   indiceDaPontuacaoAdversario < IndiceDaPontuacao(Advantage);
        }

        private int ObterIndiceDaPontuacaoDo(Jogador jogador) => _pontuacao[(int)jogador];


        private static bool EhDeuce(int indiceDaPontuacaoDoJogadorEsquerdo, int indiceDaPontuacaoDoJogadorDireito)
        {
            return indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoJogadorDireito &&
                   indiceDaPontuacaoDoJogadorEsquerdo == IndiceDaPontuacao(Deuce);
        }
    }
}
