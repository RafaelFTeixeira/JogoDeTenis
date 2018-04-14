using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class JogoDeTenisForm : Form
    {
        private readonly System.Timers.Timer _temporizador;
        private readonly JogoDeTenisEngine _engine;
        public JogoDeTenisForm()
        {
            InitializeComponent();
            _engine = new JogoDeTenisEngine(ClientSize);

            _temporizador = new System.Timers.Timer { Interval = 10 };

            _temporizador.Elapsed += OnTimedEvent;
            _temporizador.Enabled = true;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (_engine.PausarJogo > 0)
            {
                _engine.PausarJogo = 0;
                _temporizador.Interval = 10;
                _engine.ResetarBola();
            }

            _engine.Atualizar();

            if (_engine.PausarJogo > 0)
            {
                _temporizador.Enabled = false;
                _temporizador.Interval = 2000;
                _temporizador.Enabled = true;
            }

            this.Invalidate();
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
            _engine.Desenhar(e);
            Placar.Text = $"Esquerda vs Direita\r\n       {_engine.Player1Score} - {_engine.Player2Score}";
            Placar.Visible = (_engine.PausarJogo > 0);
        }
    }
}
