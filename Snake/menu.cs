using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;


namespace Snake
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();


        }

        

    private void menu_Load(object sender, EventArgs e)
        {
            
        }

        private void Start_MouseClick(object sender, MouseEventArgs e)
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\Users\Piotr\Documents\Visual Studio 2015\Projects\Snake\Snake\Wygrana1.mp3";
            wplayer.controls.play();  //    <--    dziala bez this.Close()
            
                this.Close();
            







        }

        private void Start_Click(object sender, EventArgs e)        // ??
        {
            
        }

        
}
}
