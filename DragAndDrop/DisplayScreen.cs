using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragAndDrop
{
    public partial class DisplayScreen : Form
    {
        public DisplayScreen()
        {
            InitializeComponent();
        }

        private void DisplayScreen_Load(object sender, EventArgs e)
        {
            //DisplayScreen.
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            this.ShowInTaskbar = false;
          //  pictureBox1.Image.Size.Height = pictureBox1.Height;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
