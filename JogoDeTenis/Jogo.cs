using System.Linq;

namespace JogoDeTenis
{
    public class Jogo
    {
        private int[] Pontuacao { get; }
        private const string Deuce = "40";
        private const string Advantage = "advantage";
        private const string Win = "win";
        private static string[] PlacarDoJogo => new[] { "0", "15", "30", Deuce, Advantage, Win };

        public Jogo()
        {
            Pontuacao = new[] {0, 0};
        }

        public void Pontuar(params Jogador[] jogadas)
        {
            foreach (var jogador in jogadas)
                    Pontuacao[(int) jogador]++;

            RetormarParaDeuceQuandoOsJogadoresSeIgualarComoAdvantage();
        }

        private void RetormarParaDeuceQuandoOsJogadoresSeIgualarComoAdvantage()
        {
            if (EstaComPontuacaoDeAdvantage(Jogador.Esquerdo) && 
                EstaComPontuacaoDeAdvantage(Jogador.Direito))
            {
                Pontuacao[(int)Jogador.Esquerdo]--;
                Pontuacao[(int)Jogador.Direito]--;
            }
        }

        private bool EstaComPontuacaoDeAdvantage(Jogador jogador)
        {
            var indiceDaPontuacaoDoDeuce = ObterIndiceDaPontuacao(Deuce);
            return Pontuacao[(int)jogador] > indiceDaPontuacaoDoDeuce;
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
            return indiceDaPontuacaoDoJogadorVencedor == ObterIndiceDaPontuacao(Advantage) &&
                   indiceDaPontuacaoAdversario < ObterIndiceDaPontuacao(Deuce) ||
                   indiceDaPontuacaoDoJogadorVencedor == ObterIndiceDaPontuacao(Win) &&
                   indiceDaPontuacaoAdversario < ObterIndiceDaPontuacao(Advantage);
        }

        private int ObterIndiceDaPontuacaoDo(Jogador jogador) => Pontuacao[(int) jogador];

        private static bool EhDeuce(int indiceDaPontuacaoDoJogadorEsquerdo, int indiceDaPontuacaoDoJogadorDireito)
        {
            var indiceDaPontuacaoDoDeuce = ObterIndiceDaPontuacao(Deuce);
            return indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoJogadorDireito &&
                   indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoDeuce;
        }

        private static int ObterIndiceDaPontuacao(string nomeDaPontuacao)
        {
            return PlacarDoJogo.Select((value, index) => new { value, index })
                .First(pontuacao => pontuacao.value == nomeDaPontuacao).index;
        }
    }
}
