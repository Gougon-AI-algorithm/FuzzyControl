using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyControl
{
    public partial class MainForm : Form
    {
        private const string HP_TEXT = "輸出馬力:";
        private const string TMP_STATU = "溫度狀態:";

        private Fuzzy fuzzy = new Fuzzy();

        public MainForm()
        {
            InitializeComponent();
        }

        // 啟動
        private void _onButton_Click(object sender, EventArgs e)
        {
            string tmpStatu = null;
            _aimHpLabel.Text = HP_TEXT + fuzzy.UpdateTemperatures(float.Parse(_curTmpTextBox.Text), float.Parse(_aimTmpTextBox.Text), float.Parse(_co2TextBox.Text), ref tmpStatu).ToString();
            _tmpStatuLabel.Text = TMP_STATU + tmpStatu;
        }
    }
}
