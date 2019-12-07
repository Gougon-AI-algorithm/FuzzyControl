namespace FuzzyControl
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this._aimTmpTextBox = new System.Windows.Forms.TextBox();
            this._aimTmpLabel = new System.Windows.Forms.Label();
            this._aimHpLabel = new System.Windows.Forms.Label();
            this._onButton = new System.Windows.Forms.Button();
            this._curTmpLabel = new System.Windows.Forms.Label();
            this._curTmpTextBox = new System.Windows.Forms.TextBox();
            this._co2Label = new System.Windows.Forms.Label();
            this._co2TextBox = new System.Windows.Forms.TextBox();
            this._tmpStatuLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _aimTmpTextBox
            // 
            this._aimTmpTextBox.Location = new System.Drawing.Point(95, 39);
            this._aimTmpTextBox.Name = "_aimTmpTextBox";
            this._aimTmpTextBox.Size = new System.Drawing.Size(100, 22);
            this._aimTmpTextBox.TabIndex = 1;
            // 
            // _aimTmpLabel
            // 
            this._aimTmpLabel.AutoSize = true;
            this._aimTmpLabel.Location = new System.Drawing.Point(12, 42);
            this._aimTmpLabel.Name = "_aimTmpLabel";
            this._aimTmpLabel.Size = new System.Drawing.Size(53, 12);
            this._aimTmpLabel.TabIndex = 1;
            this._aimTmpLabel.Text = "期望溫度";
            // 
            // _aimHpLabel
            // 
            this._aimHpLabel.AutoSize = true;
            this._aimHpLabel.Location = new System.Drawing.Point(12, 114);
            this._aimHpLabel.Name = "_aimHpLabel";
            this._aimHpLabel.Size = new System.Drawing.Size(62, 12);
            this._aimHpLabel.TabIndex = 2;
            this._aimHpLabel.Text = "輸出馬力 : ";
            // 
            // _onButton
            // 
            this._onButton.Location = new System.Drawing.Point(195, 145);
            this._onButton.Name = "_onButton";
            this._onButton.Size = new System.Drawing.Size(75, 23);
            this._onButton.TabIndex = 3;
            this._onButton.Text = "啟動";
            this._onButton.UseVisualStyleBackColor = true;
            this._onButton.Click += new System.EventHandler(this._onButton_Click);
            // 
            // _curTmpLabel
            // 
            this._curTmpLabel.AutoSize = true;
            this._curTmpLabel.Location = new System.Drawing.Point(12, 12);
            this._curTmpLabel.Name = "_curTmpLabel";
            this._curTmpLabel.Size = new System.Drawing.Size(53, 12);
            this._curTmpLabel.TabIndex = 5;
            this._curTmpLabel.Text = "目前溫度";
            // 
            // _curTmpTextBox
            // 
            this._curTmpTextBox.Location = new System.Drawing.Point(95, 9);
            this._curTmpTextBox.Name = "_curTmpTextBox";
            this._curTmpTextBox.Size = new System.Drawing.Size(100, 22);
            this._curTmpTextBox.TabIndex = 0;
            // 
            // _co2Label
            // 
            this._co2Label.AutoSize = true;
            this._co2Label.Location = new System.Drawing.Point(12, 70);
            this._co2Label.Name = "_co2Label";
            this._co2Label.Size = new System.Drawing.Size(49, 12);
            this._co2Label.TabIndex = 7;
            this._co2Label.Text = "Co2濃度";
            // 
            // _co2TextBox
            // 
            this._co2TextBox.Location = new System.Drawing.Point(95, 67);
            this._co2TextBox.Name = "_co2TextBox";
            this._co2TextBox.Size = new System.Drawing.Size(100, 22);
            this._co2TextBox.TabIndex = 2;
            // 
            // _tmpStatuLabel
            // 
            this._tmpStatuLabel.AutoSize = true;
            this._tmpStatuLabel.Location = new System.Drawing.Point(12, 156);
            this._tmpStatuLabel.Name = "_tmpStatuLabel";
            this._tmpStatuLabel.Size = new System.Drawing.Size(59, 12);
            this._tmpStatuLabel.TabIndex = 8;
            this._tmpStatuLabel.Text = "溫度狀態 :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 193);
            this.Controls.Add(this._tmpStatuLabel);
            this.Controls.Add(this._co2Label);
            this.Controls.Add(this._co2TextBox);
            this.Controls.Add(this._curTmpLabel);
            this.Controls.Add(this._curTmpTextBox);
            this.Controls.Add(this._onButton);
            this.Controls.Add(this._aimHpLabel);
            this.Controls.Add(this._aimTmpLabel);
            this.Controls.Add(this._aimTmpTextBox);
            this.Name = "MainForm";
            this.Text = "FuzzyControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _aimTmpTextBox;
        private System.Windows.Forms.Label _aimTmpLabel;
        private System.Windows.Forms.Label _aimHpLabel;
        private System.Windows.Forms.Button _onButton;
        private System.Windows.Forms.Label _curTmpLabel;
        private System.Windows.Forms.TextBox _curTmpTextBox;
        private System.Windows.Forms.Label _co2Label;
        private System.Windows.Forms.TextBox _co2TextBox;
        private System.Windows.Forms.Label _tmpStatuLabel;
    }
}

