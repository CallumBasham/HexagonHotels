using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for BookHotelForm.xaml
    /// </summary>
     
    public partial class BookHotelForm : Window
    {
        int HotelNumber = 0;
        int BasePrice = 0;
        string CurrentUser;
        string BookingDatabasePath;
        string CurrentHotel;
        private void CalculateCost()
        {
            if(DatePicker_Start.SelectedDate != null && DatePicker_Leave.SelectedDate != null && ComboBox_RoomType.SelectedIndex != -1 && ComboBox_BedCount.SelectedIndex != -1)
            {
                BasePrice = 45 + HotelNumber;

                int RoomTypeIndex = ComboBox_RoomType.SelectedIndex + 1;
                int BedTypeIndex = ComboBox_BedCount.SelectedIndex + 1;
                BasePrice = BasePrice + (RoomTypeIndex * 6);
                BasePrice = BasePrice + (BedTypeIndex * 4);

                DateTime StartDate = DatePicker_Start.SelectedDate.Value;
                DateTime LeaveDate = DatePicker_Leave.SelectedDate.Value;

                TimeSpan Difference =  LeaveDate - StartDate;

                BasePrice = BasePrice * Difference.Days;

                
                Label_Price.Content = "£" + BasePrice.ToString() + ".00";
            }
            
        }
        public BookHotelForm(string SelectedHotel, string LoggedUser, string BookingDatabaseLogg)
        {
            InitializeComponent();
            CurrentUser = LoggedUser;
            CurrentHotel = SelectedHotel;
            BookingDatabasePath = BookingDatabaseLogg;
            LabelHotel.Content = SelectedHotel + " Hotel";
            BitmapImage image = new BitmapImage(new Uri(@"MediaSources\" + SelectedHotel + "Hotel.jpg", UriKind.Relative));
            ImageHotel.Source = image;

            ComboBox_RoomType.Items.Add("Traveller");
            ComboBox_RoomType.Items.Add("Comfort");
            ComboBox_RoomType.Items.Add("Business");
            ComboBox_RoomType.Items.Add("Luxury");

            ComboBox_BedCount.Items.Add("Single");
            ComboBox_BedCount.Items.Add("Double");
            ComboBox_BedCount.Items.Add("Two Singles");
            ComboBox_BedCount.Items.Add("Three Singles");
            ComboBox_BedCount.Items.Add("One Single, One Double");
            ComboBox_BedCount.Items.Add("Two Single, One Double");

            switch(SelectedHotel)
            {
                case "North":
                    HotelNumber = 11;
                    break;
                case "East":
                    HotelNumber = 17;
                    break;
                case "South":
                    HotelNumber = 22;
                    break;
            }
        }

        private void ComboBox_BedCount_LostFocus(object sender, RoutedEventArgs e)
        { 
            CalculateCost();
        }

        private void ComboBox_RoomType_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateCost();
        }

        private void DatePicker_Start_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateCost();
        }

        private void DatePicker_Leave_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateCost();
        }

        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(Button_Confirm.Content.Equals("Make booking"))
            {
                if(BasePrice < 1)
                {
                    Label_Price.Content = "Incorrect data";
                }
                else
                {
                    Button_Confirm.Content = "Confirm";
                }
            }
            else if (Button_Confirm.Content.Equals("Confirm"))
            {
                foreach (string Line in File.ReadLines(BookingDatabasePath))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(CurrentUser) && SplitLine[6].Contains("true"))
                    {
                        Label_Price.Content = "Already have active booking!";
                    }
                }

                if(!Label_Price.Content.Equals("Already have active booking!"))
                {
                    StreamWriter Writer = new StreamWriter(BookingDatabasePath, true);
                    Writer.WriteLine(CurrentUser + "," + CurrentHotel + "," + ComboBox_RoomType.SelectedItem + "," + ComboBox_BedCount.SelectedItem + "," + DatePicker_Start.SelectedDate + "," + DatePicker_Leave.SelectedDate + "," + "true");
                    Writer.Flush();
                    Writer.Close();
                    this.Close();
                }
                
            }
        }
    }
}
