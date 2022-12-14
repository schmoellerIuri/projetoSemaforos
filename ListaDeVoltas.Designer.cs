namespace ProjetoSemaforos
{
    partial class ListaDeVoltas
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
            this.lv_carro = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lv_carro
            // 
            this.lv_carro.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lv_carro.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_carro.GridLines = true;
            this.lv_carro.HideSelection = false;
            this.lv_carro.Location = new System.Drawing.Point(14, 12);
            this.lv_carro.Name = "lv_carro";
            this.lv_carro.Size = new System.Drawing.Size(213, 281);
            this.lv_carro.TabIndex = 0;
            this.lv_carro.UseCompatibleStateImageBehavior = false;
            this.lv_carro.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tempo Volta (ms)";
            this.columnHeader1.Width = 208;
            // 
            // ListaDeVoltas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(239, 307);
            this.Controls.Add(this.lv_carro);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListaDeVoltas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListaDeVoltas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_carro;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}