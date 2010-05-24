using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DebugTool
{

    public delegate void ApplyValue(float value);

    public partial class DebugWindow : Form
    {
        public event ApplyValue ApplyValue;

        private float value;
        public float Value {
            set { this.value = value;
                   SetupComponet();
            }
        }

        private float barTick;
        private float oldBarValue;

        private void SetupComponet()
        {
            valueText.Text = this.value.ToString();
            valueBar.Value = 0;
            valueBar.Minimum = -10;
            valueBar.Maximum = 10;

            barTick= float.Parse(this.tickText.Text);
        }

        public DebugWindow()
        {
            InitializeComponent();
            this.Value = 10;
        }

        private void DebugWindow_Load(object sender, EventArgs e)
        {

        }

        private void valueBar_Scroll(object sender, EventArgs e)
        {
            this.value += (this.valueBar.Value - oldBarValue) * barTick;

            if (this.alwaysApplyCheckBox.Checked) {
                ApplyValue(this.value);
            }
            this.valueText.Text = this.value.ToString();
            this.oldBarValue = this.valueBar.Value;
        }

        private void valueText_TextChanged(object sender, EventArgs e)
        {
        }

        private void alwaysApplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.valueBar.Value = 0;
            this.oldBarValue = 0;
            this.value = float.Parse(this.valueText.Text);
            ApplyValue(this.value);
        }

        private void tickButton_Click(object sender, EventArgs e)
        {
            barTick = float.Parse(this.tickText.Text);
            this.valueBar.Value = 0;
            this.oldBarValue = 0;
        }

        private void topMostCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }
    }
}
