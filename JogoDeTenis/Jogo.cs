namespace JogoDeTenis
{
    public class Jogo
    {
        private int[] Pontuacao { get; }
        private static int[] Placar => new[] {0, 15, 30, 40};

        public Jogo()
        {
            Pontuacao = new[] {0, 0};
        }

        public void Pontuar(params Jogador[] jogadas)
        {
            foreach (var jogador in jogadas)
                Pontuacao[(int) jogador]++;
        }

        public string ObterPlacar()
        {
            var pontuacaoDoJogadorAzul = ObterPontuacaoDo(Jogador.Azul);
            var pontuacaoDoJogadorVermelho = ObterPontuacaoDo(Jogador.Vermelho);

            if (EhDeuce(pontuacaoDoJogadorAzul, pontuacaoDoJogadorVermelho))
                return "deuce";
            return $"{Placar[pontuacaoDoJogadorAzul]} {Placar[pontuacaoDoJogadorVermelho]}";
        }

        private int ObterPontuacaoDo(Jogador jogador) => Pontuacao[(int) jogador];

        private static bool EhDeuce(int pontuacaoDoJogadorAzul, int pontuacaoDoJogadorVermelho)
        {
            var indiceDaPontuacaoMaxima = Placar.Length - 1;
            return pontuacaoDoJogadorAzul == pontuacaoDoJogadorVermelho && pontuacaoDoJogadorAzul == indiceDaPontuacaoMaxima;
        }
    }
}
