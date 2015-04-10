namespace BranchAndBoundMethod
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ExitButton = new System.Windows.Forms.Button();
            this.FuncLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CalcBtn = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.ExitButton, "ExitButton");
            this.ExitButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // FuncLbl
            // 
            resources.ApplyResources(this.FuncLbl, "FuncLbl");
            this.FuncLbl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FuncLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FuncLbl.Name = "FuncLbl";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Name = "label1";
            // 
            // CalcBtn
            // 
            this.CalcBtn.BackColor = System.Drawing.SystemColors.ControlText;
            this.CalcBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.CalcBtn, "CalcBtn");
            this.CalcBtn.Name = "CalcBtn";
            this.CalcBtn.UseVisualStyleBackColor = false;
            this.CalcBtn.Click += new System.EventHandler(this.CalcBtn_Click);
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.SystemColors.MenuText;
            this.display.ForeColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.display, "display");
            this.display.Name = "display";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::BranchAndBoundMethod.Properties.Resources.Random_HD_Wallpapers_6;
            this.Controls.Add(this.display);
            this.Controls.Add(this.CalcBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FuncLbl);
            this.Controls.Add(this.ExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label FuncLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CalcBtn;
        private System.Windows.Forms.TextBox display;
    }
}

