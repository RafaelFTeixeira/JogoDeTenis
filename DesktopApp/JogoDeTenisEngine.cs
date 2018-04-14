using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    public class JogoDeTenisEngine
    {
        private readonly Jogador _jogadorEsquerda;
        private readonly Jogador _jogadorDireita;
        private readonly Bola _bola;
        public int Player1Score = 0;
        public int Player2Score = 0;
        public int PausarJogo = 0;

        public JogoDeTenisEngine(Size clientSize)
        {
            const int espacoForaDaParede = 100;
            var tamanhoDoJogador = new Size(20, 100);
            var tamanhoDaBola = new Size(20, 20);

            _jogadorEsquerda = new Jogador(
                new Rectangle(espacoForaDaParede, clientSize.Height / 2 - tamanhoDoJogador.Height / 2, tamanhoDoJogador.Width, tamanhoDoJogador.Height),
                clientSize,
                Keys.W,
                Keys.S);
            _jogadorDireita = new Jogador(
                new Rectangle(clientSize.Width - espacoForaDaParede - tamanhoDoJogador.Width,
                    clientSize.Height / 2 - tamanhoDoJogador.Height / 2, tamanhoDoJogador.Width, tamanhoDoJogador.Height),
                clientSize,
                Keys.Up,
                Keys.Down);
            _bola = new Bola(
                new Rectangle(espacoForaDaParede, clientSize.Height - espacoForaDaParede, tamanhoDaBola.Width, tamanhoDaBola.Height),
                clientSize);
        }

        public void Atualizar()
        {
            PausarJogo = _bola.Atualizar(_jogadorEsquerda, _jogadorDireita);
            if (PausarJogo > 0)
            {
                if (PausarJogo == 1)
                {
                    Player1Score++;
                }
                else if (PausarJogo == 2)
                {
                    Player2Score++;
                }
                return;
            }
            _jogadorEsquerda.Executar(_bola);
            _jogadorDireita.Executar(_bola);
        }

        public void ResetarBola()
        {
            _bola.Resetar();
        }

        public void AtualizarTecla(KeyEventArgs e, bool pressinouParaBaixo)
        {
            _jogadorEsquerda.AtualizarTecla(e.KeyCode, pressinouParaBaixo);
            _jogadorDireita.AtualizarTecla(e.KeyCode, pressinouParaBaixo);
        }

        public void Desenhar(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DarkRed, _jogadorDireita.Retangulo);
            e.Graphics.FillRectangle(Brushes.DarkBlue, _jogadorEsquerda.Retangulo);
            e.Graphics.FillRectangle(Brushes.Green, _bola.Retangulo);
        }
    }
}