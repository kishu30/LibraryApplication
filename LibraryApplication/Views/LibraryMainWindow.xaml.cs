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

namespace LibraryApplication.Views
{
    /// <summary>
    /// Interaction logic for LibraryMainWindow.xaml
    /// </summary>
    public partial class LibraryMainWindow : Window
    {
        public LibraryMainWindow()
        {
            InitializeComponent();
        }

        private void BookIssue_Click(object sender, RoutedEventArgs e)
        {
            BookIssueWindow bookIssueWindow = new BookIssueWindow();
            //Set the main form of the system


            App.Current.MainWindow = bookIssueWindow;


            //this.Close();

            ////Display the new main form

            bookIssueWindow.Show();
        }

        private void BookReturn_Click(object sender, RoutedEventArgs e)
        {
            BookReturnWindow bookReturnWindow = new BookReturnWindow();

            App.Current.MainWindow = bookReturnWindow;

            //this.Close();
            bookReturnWindow.Show();
        }
    }
}
