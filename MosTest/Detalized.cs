using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MosTest
{
    public partial class Detalized : Form
    {
        string recievedText = string.Empty;
        //List<String> data;
        First parent;

        public Detalized()
        {
            InitializeComponent();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;

        }


        public Detalized(First parent)
        {
            this.parent = parent;
            
            InitializeComponent();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;

        }

        public Detalized(First parent, string text1)
        {
            this.parent = parent;
            recievedText = text1;
            InitializeComponent();
            string[] labels = text1.Split();
            textBox1.Text = labels[0];
            textBox2.Text = labels[1];
            textBox3.Text = labels[2] + " " + labels[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(recievedText == string.Empty && parent != null)
            {
                parent.data.Add(textBox1.Text + "," + textBox2.Text + "," + textBox3.Text);
                parent.writeInfo();
                parent.update();
            }
            else
            {
                for (int i = parent.page * 10 + 1; i < Math.Min(parent.data.Count, 10 * (parent.page + 1) + 1); i++)
                {
                    if (parent.data[i].StartsWith(recievedText.Split()[0]))
                    {
                        parent.data[i] = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text;
                    }
                    break;
                }
                parent.writeInfo();
                parent.update();
            }

            this.Hide();

        }
    }
}
