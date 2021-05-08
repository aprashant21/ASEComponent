using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ASEComponent
{
    public partial class Form1 : Form
    {
        const string FileSavePath = @"d:\data\Save.xml";
        public string a;
        public string b;
        public string c;
        

        public Form1()
        {
            InitializeComponent();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About obj = new About();
            obj.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var xs = new XmlSerializer(typeof(Form1));

            using (TextWriter sw = new StreamWriter(FileSavePath))
            {
                xs.Serialize(sw, this);
            }

        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            a = textBoxWrite.Text;

            textBoxRead.Text = a;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxSingle_KeyDown(object sender, KeyEventArgs e)
        {
            a = textBoxWrite.Text;
            b = textBoxRead.Text;
            c = textBoxSingle.Text;

            if(c=="Move to")
            {
                
            }

    }
    }
}
