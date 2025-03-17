
namespace SistemaFinanceiro.Vendas
{
    partial class FrmTelaTaxa
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
            this.btnTaxa = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTaxa = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTaxa
            // 
            this.btnTaxa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaxa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaxa.ForeColor = System.Drawing.Color.Black;
            this.btnTaxa.Image = global::SistemaFinanceiro.Properties.Resources.salve;
            this.btnTaxa.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnTaxa.Location = new System.Drawing.Point(214, 41);
            this.btnTaxa.Name = "btnTaxa";
            this.btnTaxa.Size = new System.Drawing.Size(43, 38);
            this.btnTaxa.TabIndex = 309;
            this.btnTaxa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTaxa.UseVisualStyleBackColor = true;
            this.btnTaxa.Click += new System.EventHandler(this.btnTaxa_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(11, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 308;
            this.label6.Text = "Taxa:";
            // 
            // txtTaxa
            // 
            this.txtTaxa.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxa.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtTaxa.Location = new System.Drawing.Point(11, 41);
            this.txtTaxa.Name = "txtTaxa";
            this.txtTaxa.ReadOnly = true;
            this.txtTaxa.Size = new System.Drawing.Size(197, 38);
            this.txtTaxa.TabIndex = 307;
            this.txtTaxa.Text = "0,00";
            this.txtTaxa.TextChanged += new System.EventHandler(this.txtTaxa_TextChanged);
            this.txtTaxa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxa_KeyPress);
            // 
            // FrmTelaTaxa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(269, 101);
            this.Controls.Add(this.btnTaxa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTaxa);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTelaTaxa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aplicar Taxa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTaxa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTaxa;
    }
}