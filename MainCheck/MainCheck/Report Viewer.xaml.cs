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

using System.Windows.Forms;

using System.Windows.Forms.Integration;

using Microsoft.Reporting; 

using Microsoft.ReportingServices;

using Microsoft.Reporting.WinForms;

namespace MainCheck
{
    /// <summary>
    /// Interaction logic for Report_Viewer.xaml
    /// </summary>
    public partial class Report_Viewer : Window
    {
        public Report_Viewer()
        {
            InitializeComponent();
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://desktop-delc1r0:80/ReportServer", UriKind.Absolute);

            rptViewer.ServerReport.ReportPath = "/Test/Report1";
            rptViewer.RefreshReport();
        }
    }
}
