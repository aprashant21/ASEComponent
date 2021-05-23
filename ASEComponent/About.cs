using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEComponent
{
    /// <summary>
    /// This is the about class where user can get the information about the product.
    /// </summary>
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This is the okay button to close this about window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
