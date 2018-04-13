using System.Linq;

namespace JogoDeTenis
{
    public class Jogo
    {
        private int[] Pontuacao { get; }
        private static string[] Placar => new[] {"0", "15", "30", "40", "advantage" };

        public Jogo()
        {
            Pontuacao = new[] {0, 0};
        }

        public void Pontuar(params Jogador[] jogadas)
        {
            foreach (var jogador in jogadas)
                Pontuacao[(int) jogador]++;

            ValidaSeEhDeuce();
        }

        private void ValidaSeEhDeuce()
        {
            if (Pontuacao[(int) Jogador.Esquerdo] > Placar.Length - 2 && Pontuacao[(int)Jogador.Esquerdo] > Placar.Length - 2)
            {
                Pontuacao[(int) Jogador.Esquerdo]--;
                Pontuacao[(int) Jogador.Direito]--;
            }
        }

        public string ObterPlacar()
        {
            var indiceDaPontuacaoDoJogadorEsquerdo = ObterIndiceDaPontuacaoDo(Jogador.Esquerdo);
            var indiceDaPontuacaoDoJogadorDireito = ObterIndiceDaPontuacaoDo(Jogador.Direito);

            if (EhDeuce(indiceDaPontuacaoDoJogadorEsquerdo, indiceDaPontuacaoDoJogadorDireito))
                return "deuce";
            return $"{Placar[indiceDaPontuacaoDoJogadorEsquerdo]} {Placar[indiceDaPontuacaoDoJogadorDireito]}";
        }

        private int ObterIndiceDaPontuacaoDo(Jogador jogador) => Pontuacao[(int) jogador];

        private static bool EhDeuce(int indiceDaPontuacaoDoJogadorEsquerdo, int indiceDaPontuacaoDoJogadorDireito)
        {
            const string pontuacaoDeDeuce = "40";
            var indiceDaPontuacaoDoDeuce = Placar.Select((value, index) => new { value, index }).First(pontuacao => pontuacao.value == pontuacaoDeDeuce);
            return indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoJogadorDireito && indiceDaPontuacaoDoJogadorEsquerdo == indiceDaPontuacaoDoDeuce.index;
        }
    }
}
