namespace DesktopApp
{
    partial class JogoDeTenisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Placar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Placar
            // 
            this.Placar.AutoSize = true;
            this.Placar.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Placar.Location = new System.Drawing.Point(230, 182);
            this.Placar.Name = "Placar";
            this.Placar.Size = new System.Drawing.Size(518, 112);
            this.Placar.TabIndex = 0;
            this.Placar.Text = "Esquerda vs Direita\r\n       0 - 0";
            this.Placar.Visible = false;
            // 
            // JogoDeTenisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.Placar);
            this.Name = "JogoDeTenisForm";
            this.Text = "Jogo de tênis";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Desenhar);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AtualizarTeclaPressionada);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AtualizarTeclaQueFoiPressionada);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Placar;
    }
}

