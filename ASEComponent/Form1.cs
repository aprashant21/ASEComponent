using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Paint;

namespace ASEComponent
{
    /// <summary>
    /// This is the main class where entire code are written.
    /// </summary>
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            g = ShapeOutput.CreateGraphics();

        }
        /// <summary>
        /// This is the instance variables declared.
        /// </summary>
        Graphics g;
        int x, y = -1;
        public int radius = 0;
        public int width = 0;
        public int height = 0;
        int mouseX, mouseY = 0;
        public int dSize = 0;
        public int count = 0;
        Color c;

        int loopCount = 0;
        Boolean hasDrawOrMoveValue = false;
        /// <summary>
        /// calling shapesfactory class
        /// </summary>
        CreateShapes factory = new ShapesFactory();

        /// <summary>
        /// This method is method to exit main window..
        /// </summary>
        /// <param name="sender">Reference to the control object</param>
        /// <param name="e">event data</param>
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
        /// <summary>
        /// This method is method to open about window.
        /// </summary>
        /// <param name="sender">Reference to the control object</param>
        /// <param name="e">event data</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About obj = new About();
            obj.Show();
        }
        /// <summary>
        /// This method is method to browser text in the program textbox and load data.
        /// </summary>
        /// <param name="sender">Reference to the control object</param>
        /// <param name="e">event data</param>
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
                MultilineProgramInput.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }
        /// <summary>
        /// This method is method to save program in the specific location.
        /// </summary>
        /// <param name="sender">Reference to the control object</param>
        /// <param name="e">event data</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.WriteLine(MultilineProgramInput.Text);
                write.Close();
                MessageBox.Show("Saved successfully!!!");
            }

        }
        /// <summary>
        /// This is the method for execute button where multiline program is executed.
        /// </summary>
        /// <param name="sender">Reference to the control object</param>
        /// <param name="e">event data</param>
        private void buttonExecute_Click(object sender, EventArgs e)
        {
            hasDrawOrMoveValue = false;
            loadexecute();
        }
        /// <summary>
        /// This is the method for loading moveto/drawto and loop count command.
        /// </summary>
            private void loadexecute()
        {
            int commandline = MultilineProgramInput.Lines.Length;

            for (int i = 0; i < commandline; i++)
            {
                String singleLineCommand = MultilineProgramInput.Lines[i];
                singleLineCommand = singleLineCommand.Trim();
                if (!singleLineCommand.Equals(""))
                {
                    Boolean hasDrawto = Regex.IsMatch(singleLineCommand.ToLower(), @"\bdrawto\b");
                    Boolean hasMoveto = Regex.IsMatch(singleLineCommand.ToLower(), @"\bmoveto\b");
                    if (hasDrawto || hasMoveto)
                    {
                        String args = singleLineCommand.Substring(6, (singleLineCommand.Length - 6));
                        String[] parms = args.Split(',');
                        for (int j = 0; j < parms.Length; j++)
                        {
                            parms[j] = parms[j].Trim();
                        }
                        mouseX = int.Parse(parms[0]);
                        mouseY = int.Parse(parms[1]);
                        hasDrawOrMoveValue = true;
                        xlabel.Text = mouseX.ToString();
                        ylabel.Text = mouseY.ToString();
                        
                    }
                    else
                    {
                        hasDrawOrMoveValue = false;
                    }
                    if (hasMoveto)
                    {
                        ShapeOutput.Refresh();
                        
                    }
                }

            }
            for (loopCount = 0; loopCount < commandline; loopCount++)
            {
                String singleLineCommand = MultilineProgramInput.Lines[loopCount];
                singleLineCommand = singleLineCommand.Trim();
                if (!singleLineCommand.Equals(""))
                {
                    RunCommand(singleLineCommand);
                }

            }
        }
        /// <summary>
        /// This is the method to run the commands like if-condition, loop and repeat.
        /// </summary>
        /// <param name="singleLineCommand">This parameter is used to create + and =</param>

        public void RunCommand(string singleLineCommand)
        {
            Boolean hasPlus = singleLineCommand.Contains('+');
            Boolean hasEquals = singleLineCommand.Contains("=");
            if (hasEquals)
            {
                singleLineCommand = Regex.Replace(singleLineCommand, @"\s+", " ");
                string[] cmd = singleLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int i = 0; i < cmd.Length; i++)
                {
                    cmd[i] = cmd[i].Trim();
                }
                String firstWord = cmd[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }

                    }
                  
                    
                    else if (cmd[1].ToLower().Equals("count"))
                    {
                        if(count == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }

                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCount = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string singleLineCommand1 = MultilineProgramInput.Lines[j];
                            singleLineCommand1 = singleLineCommand1.Trim();
                            if (!singleLineCommand1.Equals(""))
                            {
                                RunCommand(singleLineCommand1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your if Statement is incorrect");
                    }
                }
                else
                {
                    string[] cmd2 = singleLineCommand.Split('=');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("count"))
                    {
                        count = int.Parse(cmd2[1]);
                    }
                  
                }
            }
            else if (hasPlus)
            {
                singleLineCommand = System.Text.RegularExpressions.Regex.Replace(singleLineCommand, @"\s+", " ");
                string[] cmd = singleLineCommand.Split(' ');
                if (cmd[0].ToLower().Equals("repeat"))
                {
                    count = int.Parse(cmd[1]);
                    if (cmd[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = GetSize(singleLineCommand);
                        radius = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            Circle c = new Circle();
                            DrawCircle(radius);
                            radius += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = GetSize(singleLineCommand);
                        dSize = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            DrawRectangle(dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = GetSize(singleLineCommand);
                        dSize = increaseValue;
                        for (int j = 0; j < count; j++)
                        {
                            DrawTriangle(dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                }
                else
                {
                    string[] cmd2 = singleLineCommand.Split('+');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(cmd2[1]);
                    }
                  
                }
            }
            else
            {
                sendDrawCommand(singleLineCommand);
            }
        }
        /// <summary>
        /// This is the method to specify specific size for the shapes.
        /// </summary>
        /// <param name="lineCommand">linecommand is for line of codes.</param>
        /// <returns></returns>
        private int GetSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }
        /// <summary>
        /// This is the method to initialize and build shapes.
        /// </summary>
        /// <param name="lineOfCommand">This parameter is initialize for shapes and variables.</param>
        private void sendDrawCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle" };
            String[] variable = { "radius", "width", "height", "count", "size"};

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] cmd = lineOfCommand.Split(' ');
            //removing white spaces in between cmd
            for (int i = 0; i < cmd.Length; i++)
            {
                cmd[i] = cmd[i].Trim();
            }
            String firstWord = cmd[0].ToLower();
            Boolean firstwordshape = shapes.Contains(firstWord);
            if (firstwordshape)
            {

                if (firstWord.Equals("circle"))
                {
                    Boolean secondWordIsVariable = variable.Contains(cmd[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (cmd[1].ToLower().Equals("radius"))
                        {
                            DrawCircle(radius);
                        }
                    }
                    else
                    {
                        DrawCircle(Int32.Parse(cmd[1]));
                    }

                }
                else if (firstWord.Equals("rectangle"))
                {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(width, height);
                        }
                        else
                        {
                            DrawRectangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            DrawRectangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }
                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawTriangle(width, height);
                        }
                        else
                        {
                            DrawTriangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawTriangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            DrawTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }

            }
        
            
            else
            {
                if (firstWord.Equals("loop"))
                {
                    count = int.Parse(cmd[1]);
                    int loopStartLine = (GetLoopStartLineNumber());
                    int loopEndLine = (GetLoopEndLineNumber() - 1);
                    loopCount = loopEndLine;
                    for (int i = 0; i < count; i++)
                    {
                        for (int j = loopStartLine; j <= loopEndLine; j++)
                        {
                            String singleLineCommand = MultilineProgramInput.Lines[j];
                            singleLineCommand = singleLineCommand.Trim();
                            if (!singleLineCommand.Equals(""))
                            {
                                RunCommand(singleLineCommand);
                            }
                        }
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                        
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("count"))
                    {
                        if (count == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("rbase"))
                    {
                        if (count == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("adj"))
                    {
                        if (count == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("hyp"))
                    {
                        if (count == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCount = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            String singleLineCommand = MultilineProgramInput.Lines[j];
                            singleLineCommand = singleLineCommand.Trim();
                            if (!singleLineCommand.Equals(""))
                            {
                                RunCommand(singleLineCommand);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This is the method to initilize loop.
        /// </summary>
        /// <returns>This returns line numbers</returns>
        private int GetEndifEndLineNumber()
        {
            int commandline = MultilineProgramInput.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < commandline; i++)
            {
                String singleLineCommand = MultilineProgramInput.Lines[i];
                singleLineCommand = singleLineCommand.Trim();
                if (singleLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
        /// <summary>
        /// This method is initiate if there is "if" command in the commandline.
        /// </summary>
        /// <returns>This returns line numbers.</returns>
        private int GetIfStartLineNumber()
        {
            int commandline = MultilineProgramInput.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < commandline; i++)
            {
                String singleLineCommand = MultilineProgramInput.Lines[i];
                singleLineCommand = Regex.Replace(singleLineCommand, @"\s+", " ");
                string[] cmd = singleLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                singleLineCommand = singleLineCommand.Trim();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
        /// <summary>
        /// This is the method to initialize loop.
        /// </summary>
        /// <returns>This returns linenumber and 0 in exception.</returns>
        private int GetLoopEndLineNumber()
        {
            try
            {
                int commandline = MultilineProgramInput.Lines.Length;
                int lineNum = 0;

                for (int i = 0; i < commandline; i++)
                {
                    String singleLineCommand = MultilineProgramInput.Lines[i];
                    singleLineCommand = singleLineCommand.Trim();
                    if (singleLineCommand.ToLower().Equals("endloop"))
                    {
                        lineNum = i + 1;

                    }
                }
                return lineNum;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// Loops are started in this method.
        /// </summary>
        /// <returns>This returns line number.</returns>
        private int GetLoopStartLineNumber()
        {
            int commandline = MultilineProgramInput.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < commandline; i++)
            {
                String singleLineCommand = MultilineProgramInput.Lines[i];
                singleLineCommand = Regex.Replace(singleLineCommand, @"\s+", " ");
                string[] cmd = singleLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                singleLineCommand = singleLineCommand.Trim();
                if (firstWord.Equals("loop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        
        }
        /// <summary>
        /// This is the variable which is used in "X-AXIS" group.
        /// </summary>
        public int size1, size2;

        /// <summary>
        /// This is the method where user can type "run","clear" and "reset " command to execute, clear and reset commandline simulteneouly.
        /// </summary>
        /// <param name="sender">Reference to the control object</param>
        /// <param name="e">event data</param>
        private void executeInput_TextChanged(object sender, EventArgs e)
        {
            if (executeInput.Text.ToLower().Trim() == "run")
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
                                IShapes rectangle = factory.getShape("rectangle");
                                Rectangle r = new Rectangle();
                                r.set(Color.Black, x, y, width, height);
                                r.Draw(g);


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
                                c.Draw(g);
                            }
                            else
                            {
                                Int32.TryParse(cmd[1], out radius);
                                IShapes circle = factory.getShape("circle");
                                Circle c = new Circle();
                                c.set(Color.AliceBlue, x, y, radius);
                                c.Draw(g);
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
                            IShapes triangle = factory.getShape("triangle");
                            Triangle r = new Triangle();
                            r.set(Color.AliceBlue, x, y, width, height);
                            r.Draw(g);
                        }
                    }
                    else if (cmd[0].Equals("filltriangle") == true)
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
                            IShapes circle = factory.getShape("filltriangle");
                            Triangle r = new Triangle();
                            r.set(Color.AliceBlue, x, y, width, height);
                            r.draw(g);
                        }
                    }
                    else if (cmd[0].Equals("pen") == true)
                    {

                        
                        if (cmd[1] == "red")//if red then color changes to red
                        {
                            c = Color.Red;
                        }
                        else if (cmd[1] == "blue")//if blue then color changes to blue
                        {
                            c = Color.Blue;
                        }
                        else if (cmd[1] == "yellow")//if yellow then color changes to yellow
                        {
                            c = Color.Yellow;
                        }
                        else
                        {
                            c = Color.AliceBlue;//default color
                        }
                    }
                    else if (cmd[0].Equals("fillrectangle") == true)
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
                                IShapes rectangle = factory.getShape("fillrectangle");
                                Rectangle r = new Rectangle();
                                r.set(Color.Black, x, y, width, height);
                                r.draw(g);


                            }
                        }
                    }
                    else if (cmd[0].Equals("fillcircle") == true)
                    {
                        if (cmd.Length != 2)
                        {
                            MessageBox.Show("Please Input Valid Parameter!!!");
                        }
                        else
                        {
                            if (cmd[1].Equals("radius") == true)
                            {
                                IShapes circle = factory.getShape("fillcircle");
                                Circle c = new Circle();
                                c.set(Color.AliceBlue, x, y, radius);
                                c.draw(g);
                            }
                            else
                            {
                                Int32.TryParse(cmd[1], out radius);
                                IShapes circle = factory.getShape("fillcircle");
                                Circle c = new Circle();
                                c.set(Color.AliceBlue, x, y, radius);
                                c.draw(g);
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
            else if (executeInput.Text.ToLower().Trim() == "clear")
                {

                    ShapeOutput.Invalidate();


                }
            else if (executeInput.Text.ToLower().Trim() == "reset")
            {
                ProgramInput.Clear();
                MultilineProgramInput.Clear();
                ShapeOutput.Invalidate();

                size1 = 0;
                size2 = 0;
                xlabel.Text = size1.ToString();
                ylabel.Text = size2.ToString();


            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ShapeOutput_Paint(object sender, PaintEventArgs e)
        {
            
        }
        
        /// <summary>
        /// This method can draw the blue color rectangle shape.
        /// </summary>
        /// <param name="width">width of rectangle</param>
        /// <param name="height">height of rectangle</param>
        private void DrawRectangle(int width, int height)
        {
            Pen myPen = new Pen(Color.Blue);
            g.DrawRectangle(myPen, mouseX - width / 2, mouseY - height / 2, width, height);
        }
        /// <summary>
        /// This is the method to draw circle using radius parameter.
        /// </summary>
        /// <param name="radius">radius of circle</param>
        private void DrawCircle(int radius)
        {
            Pen myPen = new Pen(Color.Green);
            g.DrawEllipse(myPen, mouseX - radius, mouseY - radius, radius * 2, radius * 2);
        }
        /// <summary>
        /// This is the method to draw Triangle.
        /// </summary>
        /// <param name="width">base ko triangle</param>
        /// <param name="height">adjective of triangle</param>
        private void DrawTriangle(int width, int height)
        {
            Pen myPen = new Pen(Color.Red);
            Point[] p = new Point[3];

            p[0].X = mouseX;
            p[0].Y = mouseY;

            p[1].X = mouseX - width;
            p[1].Y = mouseY;

            p[2].X = mouseX;
            p[2].Y = mouseY - height;
            g.DrawPolygon(myPen, p);
        }
        /// <summary>
        /// This is the method to move axis location from singleline program textbox.
        /// </summary>
        /// <param name="toX">move to x axis</param>
        /// <param name="toY">move to y axis</param>
        public void MoveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
            xlabel.Text = x.ToString();
            ylabel.Text = y.ToString();
        }

        /// <summary>
        /// This method is used to draw the shapes in specific location.
        /// </summary>
        /// <param name="toX">to x-axis</param>
        /// <param name="toY">to y-axis</param>
        public void DrawTo(int toX, int toY)
        {
            x = toX;
            y = toY;

        }
        /// <summary>
        /// This is the mouse click event method in which if user click on the output panel then he/she can see the actual axis in the label of the group box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShapeOutput_MouseClick(object sender, MouseEventArgs e)
        {
            xlabel.Text = (e.X).ToString();
            ylabel.Text = (e.Y).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBoxSingle_KeyDown(object sender, KeyEventArgs e)
        {

    }

    }
}
