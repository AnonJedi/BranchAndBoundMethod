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
            this.label1 = new System.Windows.Forms.Label();
            this.FTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.AddConButton = new System.Windows.Forms.Button();
            this.CTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Name = "label1";
            // 
            // FTextBox
            // 
            this.FTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.FTextBox.ForeColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.FTextBox, "FTextBox");
            this.FTextBox.Name = "FTextBox";
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.AddButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.AddButton, "AddButton");
            this.AddButton.Name = "AddButton";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // AddConButton
            // 
            this.AddConButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.AddConButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.AddConButton, "AddConButton");
            this.AddConButton.Name = "AddConButton";
            this.AddConButton.UseVisualStyleBackColor = false;
            this.AddConButton.Click += new System.EventHandler(this.AddConButton_Click);
            // 
            // CTextBox
            // 
            this.CTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            resources.ApplyResources(this.CTextBox, "CTextBox");
            this.CTextBox.Name = "CTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Name = "label2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::BranchAndBoundMethod.Properties.Resources.Random_HD_Wallpapers_6;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CTextBox);
            this.Controls.Add(this.AddConButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.FTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FTextBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button AddConButton;
        private System.Windows.Forms.TextBox CTextBox;
        private System.Windows.Forms.Label label2;
    }
}

