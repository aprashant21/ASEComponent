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
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }
        CreateShapes factory = new ShapesFactory();
        Pen myPen = new Pen(Color.Red);
        public Color newcolor;
        int x = 0, y = 0, width, height, radius;

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
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Open";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
            openFileDialog1.Filter = "DOCX files (.docx)|*.docx|All files (.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //Browse .txt file from computer             
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.                        
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: Could not read file. Original error: " + ex.Message);
                }
                //displays the text inside the file on TextBox named as txtInput                
                ProgramInput.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.WriteLine(ProgramInput.Text);
                write.Close();
                MessageBox.Show("Saved successfully!!!");
            }

        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            g = ShapeOutput.CreateGraphics();
            string command = ProgramInput.Text.ToLower();
            string[] commandline = command.Split(new String[] { "\n" },
            StringSplitOptions.RemoveEmptyEntries);

            for (int k = 0; k < commandline.Length; k++)
            {
                string[] cmd = commandline[k].Split(' ');
                if (cmd[0].Equals("moveto") == true)
                {
                    ShapeOutput.Refresh();
                    string[] param = cmd[1].Split(',');
                    if (param.Length != 2)
                    { MessageBox.Show("Please Input Valid Parameter!!!"); }
                    else
                    {
                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        MoveTo(x, y);
                    }

                }
                else if (cmd[0].Equals("drawto") == true)
                {
                    string[] param = cmd[1].Split(',');
                    int x = 0, y = 0;
                    if (param.Length != 2)
                    {
                        MessageBox.Show("Please Input Valid Parameter!!!");
                    }
                    else
                    {
                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        DrawTo(x, y);
                    }
                }
                else if (cmd[0].Equals("rectangle") == true)
                {
                    if (cmd.Length < 2)
                    {
                        MessageBox.Show("Please Input Valid Parameter!!!");

                    }
                    else
                    {
                        string[] param = cmd[1].Split(',');
                        if (param.Length < 2)
                        {
                            MessageBox.Show("Please Input Valid Parameter!!!");

                        }
                        else
                        {
                            Int32.TryParse(param[0], out width);
                            Int32.TryParse(param[1], out height);
                            IShapes circle = factory.getShape("rectangle");

                            Rectangle r = new Rectangle();
                            r.set(Color.Black, x, y, width, height);
                            r.draw(g);


                        }
                    }
                }

                else if (cmd[0].Equals("circle") == true)
                {
                    if (cmd.Length != 2)
                    {
                        MessageBox.Show("Please Input Valid Parameter!!!");
                    }
                    else
                    {
                        if (cmd[1].Equals("radius") == true)
                        {
                            IShapes circle = factory.getShape("circle");
                            Circle c = new Circle();
                            c.set(Color.AliceBlue, x, y, radius);
                            c.draw(g);
                        }
                        else
                        {
                            Int32.TryParse(cmd[1], out radius);
                            IShapes circle = factory.getShape("circle");
                            Circle c = new Circle();
                            c.set(Color.AliceBlue, x, y, radius);
                            c.draw(g);
                        }
                    }
                }

                else if (cmd[0].Equals("triangle") == true)
                {
                    string[] param = cmd[1].Split(',');
                    if (param.Length != 2)
                    {
                        MessageBox.Show("Please Input Valid Parameter!!!");

                    }
                    else
                    {
                        Int32.TryParse(param[0], out width);
                        Int32.TryParse(param[1], out height);
                        IShapes circle = factory.getShape("triangle");
                        Triangle r = new Triangle();
                        r.set(Color.AliceBlue, x, y, width, height);
                        r.draw(g);
                    }
                }
                else if (cmd[0].Equals("pen") == true)
                {
                    if (cmd.Length != 2)
                    {
                        MessageBox.Show("Please Input Valid Parameter!!!");
                    }
                    else
                    {
                        if (cmd[1].Equals("red") == true)
                        {
                            Pen p = new Pen(Color.Red, 2);

                        }
                        else if (cmd[1].Equals("blue") == true)
                        {
                            Pen p = new Pen(Color.Blue, 2);
                        }
                        else if (cmd[1].Equals("green") == true)
                        {
                            Pen p = new Pen(Color.Green, 2);
                        }
                    }
                }
                else if (!cmd[0].Equals(null))
                {
                    int errorLine = k + 1;
                    MessageBox.Show("Invalid command recognised on line " + errorLine, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShapeOutput.Invalidate();
            ProgramInput.Clear();

        }

        public void MoveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }

        public void DrawTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBoxSingle_KeyDown(object sender, KeyEventArgs e)
        {

    }

    }
}
