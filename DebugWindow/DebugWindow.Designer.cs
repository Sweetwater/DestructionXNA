namespace DebugTool
{
    partial class DebugWindow
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.applyButton = new System.Windows.Forms.Button();
            this.alwaysApplyCheckBox = new System.Windows.Forms.CheckBox();
            this.valueBar = new System.Windows.Forms.TrackBar();
            this.valueText = new System.Windows.Forms.TextBox();
            this.tickButton = new System.Windows.Forms.Button();
            this.tickText = new System.Windows.Forms.TextBox();
            this.topMostCheck = new System.Windows.Forms.CheckBox();
            this.positionText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.valueBar)).BeginInit();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(132, 94);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 0;
            this.applyButton.Text = "適用";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // alwaysApplyCheckBox
            // 
            this.alwaysApplyCheckBox.AutoSize = true;
            this.alwaysApplyCheckBox.Location = new System.Drawing.Point(35, 98);
            this.alwaysApplyCheckBox.Name = "alwaysApplyCheckBox";
            this.alwaysApplyCheckBox.Size = new System.Drawing.Size(69, 16);
            this.alwaysApplyCheckBox.TabIndex = 2;
            this.alwaysApplyCheckBox.Text = "常に適用";
            this.alwaysApplyCheckBox.UseVisualStyleBackColor = true;
            this.alwaysApplyCheckBox.CheckedChanged += new System.EventHandler(this.alwaysApplyCheckBox_CheckedChanged);
            // 
            // valueBar
            // 
            this.valueBar.Location = new System.Drawing.Point(24, 35);
            this.valueBar.Name = "valueBar";
            this.valueBar.Size = new System.Drawing.Size(206, 42);
            this.valueBar.TabIndex = 3;
            this.valueBar.Scroll += new System.EventHandler(this.valueBar_Scroll);
            // 
            // valueText
            // 
            this.valueText.Location = new System.Drawing.Point(35, 12);
            this.valueText.Name = "valueText";
            this.valueText.Size = new System.Drawing.Size(145, 19);
            this.valueText.TabIndex = 4;
            this.valueText.TextChanged += new System.EventHandler(this.valueText_TextChanged);
            // 
            // tickButton
            // 
            this.tickButton.Location = new System.Drawing.Point(242, 37);
            this.tickButton.Name = "tickButton";
            this.tickButton.Size = new System.Drawing.Size(38, 23);
            this.tickButton.TabIndex = 5;
            this.tickButton.Text = "変更";
            this.tickButton.UseVisualStyleBackColor = true;
            this.tickButton.Click += new System.EventHandler(this.tickButton_Click);
            // 
            // tickText
            // 
            this.tickText.Location = new System.Drawing.Point(242, 12);
            this.tickText.Name = "tickText";
            this.tickText.Size = new System.Drawing.Size(38, 19);
            this.tickText.TabIndex = 6;
            this.tickText.Text = "0.1";
            // 
            // topMostCheck
            // 
            this.topMostCheck.AutoSize = true;
            this.topMostCheck.Checked = true;
            this.topMostCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topMostCheck.Location = new System.Drawing.Point(220, 245);
            this.topMostCheck.Name = "topMostCheck";
            this.topMostCheck.Size = new System.Drawing.Size(60, 16);
            this.topMostCheck.TabIndex = 7;
            this.topMostCheck.Text = "最前面";
            this.topMostCheck.UseVisualStyleBackColor = true;
            this.topMostCheck.CheckedChanged += new System.EventHandler(this.topMostCheck_CheckedChanged);
            // 
            // positionText
            // 
            this.positionText.Location = new System.Drawing.Point(12, 200);
            this.positionText.Name = "positionText";
            this.positionText.ReadOnly = true;
            this.positionText.Size = new System.Drawing.Size(168, 19);
            this.positionText.TabIndex = 8;
            this.positionText.TextChanged += new System.EventHandler(this.positionText_TextChanged);
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.positionText);
            this.Controls.Add(this.topMostCheck);
            this.Controls.Add(this.tickText);
            this.Controls.Add(this.tickButton);
            this.Controls.Add(this.valueText);
            this.Controls.Add(this.valueBar);
            this.Controls.Add(this.alwaysApplyCheckBox);
            this.Controls.Add(this.applyButton);
            this.Name = "DebugWindow";
            this.Text = "DebugWindow";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DebugWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.valueBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.CheckBox alwaysApplyCheckBox;
        private System.Windows.Forms.TrackBar valueBar;
        private System.Windows.Forms.TextBox valueText;
        private System.Windows.Forms.Button tickButton;
        private System.Windows.Forms.TextBox tickText;
        private System.Windows.Forms.CheckBox topMostCheck;
        private System.Windows.Forms.TextBox positionText;
    }
}

