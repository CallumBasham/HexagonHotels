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
using System.IO;

namespace HexagonHotels
{
    /// <summary>
    /// Interaction logic for BookingDataForm.xaml
    /// </summary>
    public partial class BookingDataForm : Window
    {
        string BookingDatabaseLocation;
        string CurrentUser;
        public BookingDataForm(string BookingDatabasePath, string SessionUser)
        {
            CurrentUser = SessionUser;
            BookingDatabaseLocation = BookingDatabasePath;
            InitializeComponent();
            bool HasBookings = false;
            foreach(string Line in File.ReadLines(BookingDatabasePath))
            {
                List<string> SplitLine = new List<string>(Line.Split(','));
                if(SplitLine[0].Contains(CurrentUser) && SplitLine[6].Contains("true"))
                {
                    HasBookings = true;
                    TextBox_Username.Text = SplitLine[0];
                    TextBox_HotelName.Text = SplitLine[1];
                    TextBox_RoomType.Text = SplitLine[2];
                    TextBox_BedCount.Text = SplitLine[3];
                    TextBox_DateStart.Text = SplitLine[4];
                    TextBox_DateLeave.Text = SplitLine[5];
                }
            }
            if (HasBookings.Equals(false))
            {
                TextBox_Username.Visibility = Visibility.Collapsed;
                TextBox_HotelName.Visibility = Visibility.Collapsed;
                TextBox_RoomType.Visibility = Visibility.Collapsed;
                TextBox_BedCount.Visibility = Visibility.Collapsed;
                TextBox_DateStart.Visibility = Visibility.Collapsed;
                TextBox_DateLeave.Visibility = Visibility.Collapsed;
                Label_Username.Content = "You have no active bookings";
                Label_HotelName.Visibility = Visibility.Collapsed;
                Label_RoomType.Visibility = Visibility.Collapsed;
                Label_BedCount.Visibility = Visibility.Collapsed;
                Label_DateStart.Visibility = Visibility.Collapsed;
                Label_DateLeave.Visibility = Visibility.Collapsed;
                Button_Delete.Visibility = Visibility.Collapsed;
            }
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(Button_Delete.Content.Equals("Delete Booking"))
            {
                Button_Delete.Content = "Confirm";
            }
            else if(Button_Delete.Content.Equals("Confirm"))
            {
                List<string> toSave = new List<string>();
                //toSave.Add("Username,Password,Forename,Surname,Email Address,Phone Number,Address Line 1,Address Line 2,Permission Level");
                foreach (string Line in File.ReadLines(BookingDatabaseLocation))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(CurrentUser) && SplitLine[6].Contains("true"))
                    {

                    }
                    else
                    {
                        toSave.Add(Line);
                    }
                }
                using (StreamWriter Writer = new StreamWriter(BookingDatabaseLocation, false))
                {
                    foreach (string Line in toSave)
                    {
                        Writer.WriteLine(Line);
                    }
                }
                this.Close();
            }
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


    
}
