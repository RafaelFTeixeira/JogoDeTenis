using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    public class Jogador
    {
        public Rectangle Retangulo;
        public bool MoverParaCima = false;
        public bool MoverParaBaixo = false;
        private readonly Keys _teclaParaCima;
        private readonly Keys _teclaParaBaixo;
        private Size _enclosing;
        private const int Velocidade = 3;

        public Jogador(Rectangle retangulo, Size enclosing, Keys teclaParaCima, Keys teclaParaBaixo)
        {
            Retangulo = retangulo;
            _enclosing = enclosing;
            _teclaParaCima = teclaParaCima;
            _teclaParaBaixo = teclaParaBaixo;
        }

        public void Executar(Bola bola)
        {
            if (MoverParaCima && MoverParaBaixo) return;
            if (MoverParaCima)
            {
                if (Retangulo.Top - Velocidade >= 0)
                {
                    Retangulo.Y -= Velocidade;
                    if (this.Retangulo.IntersectsWith(bola.Retangulo))
                    {
                        Retangulo.Y += Velocidade;
                    }
                }
                else
                {
                    Retangulo.Y = 0;
                }
            }
            else if (MoverParaBaixo)
            {
                if (Retangulo.Bottom + Velocidade <= _enclosing.Height)
                {
                    Retangulo.Y += Velocidade;
                    if (this.Retangulo.IntersectsWith(bola.Retangulo))
                    {
                        Retangulo.Y -= Velocidade;
                    }
                }
                else
                {
                    Retangulo.Y = _enclosing.Height - Retangulo.Height;
                }
            }
        }

        public void AtualizarTecla(Keys tecla, bool pressionouParaBaixo)
        {
            if (tecla == _teclaParaCima)
            {
                MoverParaCima = pressionouParaBaixo;
            }
            else if (tecla == _teclaParaBaixo)
            {
                MoverParaBaixo = pressionouParaBaixo;
            }
        }
    }
}