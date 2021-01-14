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
        private bool active = false;
        private ImageBrush treeoff = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\tree_off.png", UriKind.Relative)));
        private ImageBrush treeon = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\tree_on.png", UriKind.Relative)));
        private DispatcherTimer timer;
        public XmasCard()
        {
            InitializeComponent();
            Background = treeoff;
            xmastime();
        }
        private void Activated()
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }
        private void xmastime()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(900);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Activated();
            if (active)
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
