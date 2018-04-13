using System.Linq;

namespace JogoDeTenis
{
    public class Jogo
    {
        private int[] Pontuacao { get; }
        private static string[] PlacarDoJogo => new[] { "0", "15", "30", "40", "advantage", "win" };

        public Jogo()
        {
            Pontuacao = new[] {0, 0};
        }

        public void Pontuar(params Jogador[] jogadas)
        {
            foreach (var jogador in jogadas)
                    Pontuacao[(int) jogador]++;

            RetormarParaDeuceSeOsJogadoresTiveremComoAdvantage();
        }

        private void RetormarParaDeuceSeOsJogadoresTiveremComoAdvantage()
        {
            if (EstaComPonutacaoDeAdvantage(Jogador.Esquerdo) && 
                EstaComPonutacaoDeAdvantage(Jogador.Direito))
            {
                Pontuacao[(int)Jogador.Esquerdo]--;
                Pontuacao[(int)Jogador.Direito]--;
            }
        }

        private bool EstaComPonutacaoDeAdvantage(Jogador jogador)
        {
            var indiceDaPontuacaoDoDeuce = ObterIndiceDaPontuacao("40");
            return Pontuacao[(int)jogador] > indiceDaPontuacaoDoDeuce;
        }

        public string ObterPlacar()
        {
            var indiceDaPontuacaoDoJogadorEsquerdo = ObterIndiceDaPontuacaoDo(Jogador.Esquerdo);
            var indiceDaPontuacaoDoJogadorDireito = ObterIndiceDaPontuacaoDo(Jogador.Direito);

            if (EhDeuce(indiceDaPontuacaoDoJogadorEsquerdo, indiceDaPontuacaoDoJogadorDireito))
                return "deuce";
            if (indiceDaPontuacaoDoJogadorEsquerdo == ObterIndiceDaPontuacao("advantage") && indiceDaPontuacaoDoJogadorDireito < ObterIndiceDaPontuacao("40") ||
                indiceDaPontuacaoDoJogadorEsquerdo == ObterIndiceDaPontuacao("win") && indiceDaPontuacaoDoJogadorDireito < ObterIndiceDaPontuacao("advantage"))
                return "Jogador da esquerda venceu";
            if (indiceDaPontuacaoDoJogadorDireito == ObterIndiceDaPontuacao("advantage") && indiceDaPontuacaoDoJogadorEsquerdo < ObterIndiceDaPontuacao("40") ||
                indiceDaPontuacaoDoJogadorDireito == ObterIndiceDaPontuacao("win") && indiceDaPontuacaoDoJogadorEsquerdo < ObterIndiceDaPontuacao("advantage"))
                return "Jogador da direita venceu";
            return $"{PlacarDoJogo[indiceDaPontuacaoDoJogadorEsquerdo]} {PlacarDoJogo[indiceDaPontuacaoDoJogadorDireito]}";
        }

        private int ObterIndiceDaPontuacaoDo(Jogador jogador) => Pontuacao[(int) jogador];

        private static bool EhDeuce(int indiceDaPontuacaoDoJogadorEsquerdo, int indiceDaPontuacaoDoJogadorDireito)
        {
            var indiceDaPontuacaoDoDeuce = ObterIndiceDaPontuacao("40");
            return indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoJogadorDireito && indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoDeuce;
        }

        private static int ObterIndiceDaPontuacao(string nomeDaPontuacao)
        {
            return PlacarDoJogo.Select((value, index) => new { value, index }).First(pontuacao => pontuacao.value == nomeDaPontuacao).index;
        }
    }
}
