namespace Tennis
{
    partial class GameForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameFieldPictBox = new System.Windows.Forms.PictureBox();
            this.gameStepTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldPictBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameFieldPictBox
            // 
            this.gameFieldPictBox.BackColor = System.Drawing.Color.White;
            this.gameFieldPictBox.Location = new System.Drawing.Point(0, 0);
            this.gameFieldPictBox.Name = "gameFieldPictBox";
            this.gameFieldPictBox.Size = new System.Drawing.Size(500, 300);
            this.gameFieldPictBox.TabIndex = 0;
            this.gameFieldPictBox.TabStop = false;
            // 
            // gameStepTimer
            // 
            this.gameStepTimer.Enabled = true;
            this.gameStepTimer.Interval = 5;
            this.gameStepTimer.Tick += new System.EventHandler(this.gameStepTimer_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 320);
            this.Controls.Add(this.gameFieldPictBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Теннис";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldPictBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gameFieldPictBox;
        private System.Windows.Forms.Timer gameStepTimer;
    }
}

