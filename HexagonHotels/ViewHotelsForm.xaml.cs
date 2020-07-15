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

namespace HexagonHotels
{
    /// <summary>
    /// Interaction logic for ViewHotelsForm.xaml
    /// </summary>
    public partial class ViewHotelsForm : Window
    {
        string PermissionLevel = "Client";
        string CurrentUsername;
        string BookingDatabaseArguement;
        public ViewHotelsForm(string LoggedPermission, string LoggedUsername, string BookingsDatabasePath)
        {
            InitializeComponent();
            PermissionLevel = LoggedPermission;
            CurrentUsername = LoggedUsername;
            BookingDatabaseArguement = BookingsDatabasePath;
        }



        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(PermissionLevel.Equals("Client"))
            {
                Grid OurGrid = (Grid)sender;
                this.Hide();
                BookHotelForm newForm = new BookHotelForm(OurGrid.Name.Replace("Overlay", ""), CurrentUsername, BookingDatabaseArguement);
                newForm.ShowDialog();
                this.Close();
            }
        }
    }
}
