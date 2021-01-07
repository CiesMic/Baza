using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace MainCheck
{
    /// <summary>
    /// Interaction logic for XmasCard.xaml
    /// </summary>
    public partial class XmasCard : Window
    {
        public XmasCard()
        {
            InitializeComponent();
        }
        private void XmasCard_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 500; // milli seconds
            myTimer.Tick += myTimer_Tick;


            int a = 0;
            if (a == 0)
            {
                myTimer.Start();
                button6.BackColor = Color.Red;
            }
            else
            {
                button6.BackColor = Color.Green;

            }
        }

        void myTimer_Tick(object sender, EventArgs e)
        {
            button6.BackColor = Color.White;
        }
    }
}
