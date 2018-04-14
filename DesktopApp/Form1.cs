using System;
using System.Windows.Forms;
using JogoDeTenis;

namespace DesktopApp
{
    public partial class JogoDeTenisForm : Form
    {
        private readonly System.Timers.Timer _temporizador;
        private readonly JogoDeTenisEngine _engine;
        private readonly Jogo _jogo;
        private bool FimDeJogo =>  _jogo.ObterPlacar().Contains("venceu");


        public JogoDeTenisForm()
        {
            InitializeComponent();
            _jogo = new Jogo();
            _engine = new JogoDeTenisEngine(ClientSize, _jogo);

            _temporizador = new System.Timers.Timer { Interval = 10 };

            _temporizador.Elapsed += OnTimedEvent;
            _temporizador.Enabled = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            ReniciarJogada();
            _engine.Atualizar();
            PausarJogoPorSegundos(2);
            Invalidate();
            if (FimDeJogo)
                _temporizador.Enabled = false;
        }

        private void ReniciarJogada()
        {
            if (!_engine.EstaPausado)
            {
                _engine.PausarJogo = 0;
                _temporizador.Interval = 10;
                _engine.ResetarBola();
            }
        }

        private void PausarJogoPorSegundos(int segundos)
        {
            if (!_engine.EstaPausado)
            {
                _temporizador.Enabled = false;
                _temporizador.Interval = segundos * 1000;
                _temporizador.Enabled = true;
            }
        }

        private void AtualizarTeclaPressionada(object sender, KeyEventArgs e)
        {
            _engine.AtualizarTecla(e, true);
        }

        private void AtualizarTeclaQueFoiPressionada(object sender, KeyEventArgs e)
        {
            _engine.AtualizarTecla(e, false);
        }

        private void Desenhar(object sender, PaintEventArgs e)
        {
            _engine.InserirCores(e);
            Placar.Text = _jogo.ObterPlacar();
        }
    }
}
