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
using System.Reflection;
using System.Windows.Media.Animation;

namespace HexagonHotels
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string ClientDatabasePath;
        string BookingsDatabasePath;
        string SessionUsername;
        string SessionPassword;
        string SessionPermission;
        public Window1()
        {
            InitializeComponent();
            DatabaseConnections();
        }
        private void DatabaseConnections()
        {
            Type typeObject = typeof(Window1);
            Assembly assemblyObject = typeObject.Assembly;
            ClientDatabasePath = System.IO.Path.GetDirectoryName(assemblyObject.Location) + @"\ClientDatabase.csv";
            BookingsDatabasePath = System.IO.Path.GetDirectoryName(assemblyObject.Location) + @"\BookingsDatabase.csv";
            if(!File.Exists(ClientDatabasePath))
            {
                StreamWriter Writer = new StreamWriter(ClientDatabasePath);
                Writer.WriteLine("Username,Password,Forename,Surname,Email Address,Phone Number,Address Line 1,Address Line 2,Permission Level");
                Writer.WriteLine("HexagonAdmin,Sonny161,Hexagon,Hotels,HexagonHotels@Gmail.com,01489892771,Hexagon Drive,Hampshire,Admin");
                Writer.Flush();
                Writer.Close();
            }
            if (!File.Exists(BookingsDatabasePath))
            {
                StreamWriter Writer = new StreamWriter(BookingsDatabasePath);
                Writer.WriteLine("Username,Hotel Name,Room Type,BedCount,Date Start,Date End,Active,Cause");
                Writer.Flush();
                Writer.Close();
            }
        }

        //For user interface sake
        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text.Equals("Username")) { TextBox_Username.Text = ""; }
        }
        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text.Equals("")) { TextBox_Username.Text = "Username"; }
        }
        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Password.Text.Equals("Password")) { TextBox_Password.Text = ""; }
        }
        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Password.Text.Equals("")) { TextBox_Password.Text = "Password"; }
        }

        //On Button Clicks
        private void AdaptFormMode(bool LoggedIn)
        {
            if(LoggedIn.Equals(true))
            {
                if(SessionPermission == "Admin")
                {
                    Label_Output.Foreground = new SolidColorBrush(Colors.White);
                    Label_Output.Content = "Welcome, " + SessionUsername;
                    TextBox_Username.Visibility = Visibility.Collapsed;
                    TextBox_Password.Visibility = Visibility.Collapsed;
                    Button_Register.Visibility = Visibility.Visible;
                    
                    Button_ViewClientData.Visibility = Visibility.Visible;
                    Button_BookingDetails.Visibility = Visibility.Visible;
                    Button_ViewHotels.Visibility = Visibility.Visible;

                    Storyboard sb = this.FindResource("MoveLoginDown") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveClientUp") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveBookingsUp") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveHotelsUp") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveRegisterUp") as Storyboard;
                    sb.Begin();

                    Button_ViewClientData.Content = "Client Info";
                    Button_BookingDetails.Content = "Booking Info";

                    Button_Register.Content = "Reg Client";
                    Button_Login.Content = "Logout";
                }
                else
                {
                    Label_Output.Foreground = new SolidColorBrush(Colors.White);
                    Label_Output.Content = "Welcome, " + SessionUsername;
                    TextBox_Username.Visibility = Visibility.Collapsed;
                    TextBox_Password.Visibility = Visibility.Collapsed;
                    //Button_Login.Visibility = Visibility.Collapsed;
                    Button_Register.Visibility = Visibility.Collapsed;

                    Button_ViewClientData.Visibility = Visibility.Visible;
                    Button_BookingDetails.Visibility = Visibility.Visible;
                    Button_ViewHotels.Visibility = Visibility.Visible;
                    //Button_Login.Margin = new Thickness(87, 175, 86.6, 72.4);

                    Storyboard sb = this.FindResource("MoveLoginDown") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveClientUp") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveBookingsUp") as Storyboard;
                    sb.Begin();
                    sb = this.FindResource("MoveHotelsUp") as Storyboard;
                    sb.Begin();

                    Button_Login.Content = "Logout";
                }
               
            }
            else
            {
                //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                //Application.Current.Shutdown();
                TextBox_Username.Visibility = Visibility.Visible;
                TextBox_Password.Visibility = Visibility.Visible;
                Storyboard sb = this.FindResource("MoveLoginUp") as Storyboard;
                sb.Begin();
                sb = this.FindResource("MoveClientDown") as Storyboard;
                sb.Begin();
                sb = this.FindResource("MoveBookingsDown") as Storyboard;
                sb.Begin();
                sb = this.FindResource("MoveHotelsDown") as Storyboard;
                sb.Begin();
                sb = this.FindResource("MoveRegisterDown") as Storyboard;
                sb.Begin();
                Button_ViewClientData.Visibility = Visibility.Collapsed;
                Button_BookingDetails.Visibility = Visibility.Collapsed;
                Button_ViewHotels.Visibility = Visibility.Collapsed;
                Button_Login.Visibility = Visibility.Visible;
                Button_Register.Visibility = Visibility.Visible;
                TextBox_Username.Text = "Username";
                TextBox_Password.Text = "Password";
                Button_Login.Content = "Login";
                Button_Register.Content = "Register";
                Button_ViewClientData.Content = "My Details";
                Button_BookingDetails.Content = "My bookings";
                Label_Output.Foreground = new SolidColorBrush(Colors.White);
                Label_Output.Content = "";
                SessionUsername = "";
                SessionPassword = "";
                SessionPermission = "";
            }
            
        }
        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm newForm = new RegisterForm(ClientDatabasePath);
            this.Hide();
            newForm.ShowDialog();
            this.Show();
            if (newForm.LoggedUsername != null && newForm.LoggedPassword != null && !newForm.LoggedUsername.Equals("") && !newForm.LoggedPassword.Equals(""))
            {
                SessionUsername = newForm.LoggedUsername;
                SessionPassword = newForm.LoggedPassword;
                SessionPermission = newForm.LoggedPermission;
                AdaptFormMode(true);
            }
        }
        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if(Button_Login.Content.Equals("Login") && !TextBox_Username.Text.Equals("Username"))
            {
                bool FoundEm = false;
                foreach (string Line in File.ReadLines(ClientDatabasePath))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(TextBox_Username.Text) && SplitLine[1].Contains(TextBox_Password.Text))
                    {
                        FoundEm = true;
                        SessionUsername = SplitLine[0];
                        SessionPassword = SplitLine[1];
                        SessionPermission = SplitLine[8];
                        AdaptFormMode(true);
                        return;
                    }
                }
                if (FoundEm.Equals(false))
                {
                    Label_Output.Foreground = new SolidColorBrush(Colors.Red);
                    Label_Output.Content = "INCORRECT USERNAME OR PASSWORD";
                }
            }
            else if(Button_Login.Content.Equals("Logout"))
            {
                AdaptFormMode(false);
            }
            else
            {
                Label_Output.Foreground = new SolidColorBrush(Colors.Red);
                Label_Output.Content = "INCORRECT LOGIN DETAILS!";
            }
        }

        private void Button_ViewHotels_Click(object sender, RoutedEventArgs e)
        {
            ViewHotelsForm newForm = new ViewHotelsForm(SessionPermission, SessionUsername, BookingsDatabasePath);
            this.Hide();
            newForm.ShowDialog();
            this.Show();
        }

        private void Button_BookingDetails_Click(object sender, RoutedEventArgs e)
        {
            BookingDataForm newForm = new BookingDataForm(BookingsDatabasePath, SessionUsername);
            this.Hide();
            newForm.ShowDialog();
            this.Show();
        }

        private void Button_ClientDetails_Click(object sender, RoutedEventArgs e)
        {
            ClientDataForm newForm = new ClientDataForm(ClientDatabasePath, SessionUsername, SessionPassword, SessionPermission);
            this.Hide();
            newForm.ShowDialog();
            this.Show();
            if (newForm.ClientSessionUsername != null && newForm.ClientSessionPermission != null && !newForm.ClientSessionUsername.Equals("") && !newForm.ClientSessionPassword.Equals(""))
            {
                SessionUsername = newForm.ClientSessionUsername;
                SessionPassword = newForm.ClientSessionPassword;
                SessionPermission = newForm.ClientSessionPermission;
                AdaptFormMode(true);
            }
            else
            {
                AdaptFormMode(false);
            }
        }
    }
}
