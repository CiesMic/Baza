using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace MainCheck
{
    /// <summary>
    /// Interaction logic for XmasCard.xaml
    /// </summary>
    public partial class XmasCard : Window
    {
        private int a = 0;
        private ImageBrush treeoff = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\tree_off.png", UriKind.Relative)));
        private ImageBrush treeon = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\tree_on.png", UriKind.Relative)));
        private DispatcherTimer timer;
        public XmasCard()
        {
            InitializeComponent();
            Background = treeoff;
            xmastime();
        }
        private void xmastime()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            a = DateTime.Now.Second;
            if (a % 2 == 0)
            {
                Background = treeon;
            }
            else
            {
                Background = treeoff;
            }
        }
    }
}
