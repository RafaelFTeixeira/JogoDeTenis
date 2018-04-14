using System;
using System.Drawing;

namespace DesktopApp
{
    public class Bola
    {
        public Rectangle Retangulo;
        private int _posicaoDaBolaEmHorizontal;
        private int _posicaoDaBolaEmVertical;
        private Size _enclosing;
        private readonly Rectangle _paredeSuperior;
        private readonly Rectangle _paredeInferior;

        public Bola(Rectangle retangulo, Size enclosing)
        {
            Retangulo = retangulo;
            _enclosing = enclosing;
            const int espressuraDaParede = 100;
            _paredeSuperior = new Rectangle(0, -espressuraDaParede, enclosing.Width, espressuraDaParede);
            _paredeInferior = new Rectangle(0, enclosing.Height, enclosing.Width, espressuraDaParede);
            Resetar();
        }

        public void Resetar()
        {
            var random = new Random();
            Retangulo.X = _enclosing.Width / 2;
            Retangulo.Y = _enclosing.Height / 2;
            _posicaoDaBolaEmHorizontal = random.Next(0, 7) - 3;
            _posicaoDaBolaEmVertical = random.Next(0, 7) - 3;
            _posicaoDaBolaEmHorizontal = (_posicaoDaBolaEmHorizontal == 0) ? 1 : _posicaoDaBolaEmHorizontal;
            _posicaoDaBolaEmVertical = (_posicaoDaBolaEmVertical == 0) ? -1 : _posicaoDaBolaEmVertical;
        }

        public int Atualizar(Jogador jogadorDaEsquerda, Jogador jogadorDaDireita)
        {
            Retangulo.X += _posicaoDaBolaEmHorizontal;
            Retangulo.Y += _posicaoDaBolaEmVertical;

            if (Retangulo.Left < 0)
            {
                return 2;
            }
            if (Retangulo.Right > _enclosing.Width)
            {
                return 1;
            }

            if (Retangulo.IntersectsWith(_paredeSuperior) || Retangulo.IntersectsWith(_paredeInferior))
            {
                _posicaoDaBolaEmVertical = -_posicaoDaBolaEmVertical;
            }

            if (Retangulo.IntersectsWith(jogadorDaEsquerda.Retangulo))
            {
                CalcularLarguraDoSalto(jogadorDaEsquerda);
            }
            else if (Retangulo.IntersectsWith(jogadorDaDireita.Retangulo))
            {
                CalcularLarguraDoSalto(jogadorDaDireita);
            }
            return 0;

        }

        private void CalcularLarguraDoSalto(Jogador jogador)
        {
            _posicaoDaBolaEmHorizontal = -_posicaoDaBolaEmHorizontal;

            if (_posicaoDaBolaEmHorizontal > 0 && Retangulo.Left + _posicaoDaBolaEmHorizontal < jogador.Retangulo.Right)
            {
                _posicaoDaBolaEmHorizontal = -Math.Abs(_posicaoDaBolaEmHorizontal);
                _posicaoDaBolaEmVertical = -_posicaoDaBolaEmVertical;
            }
            else if (_posicaoDaBolaEmHorizontal < 0 && Retangulo.Right + _posicaoDaBolaEmHorizontal > jogador.Retangulo.Left)
            {
                _posicaoDaBolaEmHorizontal = Math.Abs(_posicaoDaBolaEmHorizontal);
                _posicaoDaBolaEmVertical = -_posicaoDaBolaEmVertical;
            }
            else
            {
                if (jogador.MoverParaBaixo && !jogador.MoverParaCima)
                {
                    if (_posicaoDaBolaEmVertical > 0)
                    {
                        _posicaoDaBolaEmVertical += 1;
                    }
                    else
                    {
                        _posicaoDaBolaEmVertical += 1;
                        if (_posicaoDaBolaEmVertical == 0)
                        {
                            _posicaoDaBolaEmVertical = -1;
                        }
                        else
                        {
                            _posicaoDaBolaEmHorizontal = Math.Sign(_posicaoDaBolaEmHorizontal) * (Math.Abs(_posicaoDaBolaEmHorizontal) + 1);
                        }
                    }
                }
                else if (jogador.MoverParaCima && !jogador.MoverParaBaixo)
                {
                    if (_posicaoDaBolaEmVertical > 0)
                    {
                        _posicaoDaBolaEmVertical += -1;
                        if (_posicaoDaBolaEmVertical == 0)
                        {
                            _posicaoDaBolaEmVertical = 1;
                        }
                        else
                        {
                            _posicaoDaBolaEmHorizontal = Math.Sign(_posicaoDaBolaEmHorizontal) * (Math.Abs(_posicaoDaBolaEmHorizontal) + 1);
                        }
                    }
                    else
                    {
                        _posicaoDaBolaEmVertical += -1;
                    }
                }
            }
        }


    }
}