using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchObserver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form1_Resize);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                // прячем наше окно из панели
                this.ShowInTaskbar = false;
                // делаем нашу иконку в трее активной
                //notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(3000, "Ho", "Hi", ToolTipIcon.Info);
            }
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        checkedListBox1.Items.Add(textBox1.Text);
                        textBox1.Clear();
                    }
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                checkedListBox1.Items.Add(textBox1.Text);
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkedListBox1.CheckedItems.OfType<string>().ToList().ForEach(checkedListBox1.Items.Remove);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!checkedListBox1.Sorted)
            {
                var temp = new List<string>(checkedListBox1.Items.OfType<string>());
                temp.Sort();
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(temp.ToArray());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var temp = new HashSet<string>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                temp.Add((string) checkedListBox1.Items[i]);
            }

            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(temp.ToArray());
        }
    }
}