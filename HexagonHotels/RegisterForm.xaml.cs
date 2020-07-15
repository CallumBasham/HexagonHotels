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
using System.Text.RegularExpressions;
using System.IO;
namespace HexagonHotels
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        List<string> ExistingUsernames = new List<string>();
        string ClientDatabasePathArguement;

        public string LoggedUsername;
        public string LoggedPassword;
        public string LoggedPermission;
        public RegisterForm(string ClientDatabasePath)
        {
            InitializeComponent();
            ClientDatabasePathArguement = ClientDatabasePath;
            foreach (string Line in File.ReadLines(ClientDatabasePath))
            {
                List<string> SplitLine = new List<string>(Line.Split(','));
                ExistingUsernames.Add(SplitLine[0]);
            }
        }


        //For UI sake
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
        private void Forename_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Forename.Text.Equals("Forename")) { TextBox_Forename.Text = ""; }
        }
        private void Forename_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Forename.Text.Equals("")) { TextBox_Forename.Text = "Forename"; }
        }
        private void Surname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Surname.Text.Equals("Surname")) { TextBox_Surname.Text = ""; }
        }
        private void Surname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Surname.Text.Equals("")) { TextBox_Surname.Text = "Surname"; }
        }
        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Email.Text.Equals("Email (Optional)")) { TextBox_Email.Text = ""; }
        }
        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Email.Text.Equals("")) { TextBox_Email.Text = "Email (Optional)"; }
        }
        private void PhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_PhoneNumber.Text.Equals("Phone Number (Optional)")) { TextBox_PhoneNumber.Text = ""; }
        }
        private void PhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_PhoneNumber.Text.Equals("")) { TextBox_PhoneNumber.Text = "Phone Number (Optional)"; }
        }
        private void Address1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Address1.Text.Equals("Address Line 1 (Optional)")) { TextBox_Address1.Text = ""; }
        }
        private void Address1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Address1.Text.Equals("")) { TextBox_Address1.Text = "Address Line 1 (Optional)"; }
        }
        private void Address2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Address2.Text.Equals("Address Line 2 (Optional)")) { TextBox_Address2.Text = ""; }
        }
        private void Address2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Address2.Text.Equals("")) { TextBox_Address2.Text = "Address Line 2 (Optional)"; }
        }

        //Button clicks
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private string CheckData()
        {
            if(TextBox_Username.Text.Length < 5 || TextBox_Username.Text == "Username")
            {
                return "Please enter valid USERNAME details!\n-Must be at least 5 characters long";
            }
            else if(ExistingUsernames.Contains(TextBox_Username.Text))
            {
                Random random = new Random();
                int randomKey = random.Next(1000, 9999);
                return "Usename taken, you could try:\n" + TextBox_Username.Text + randomKey + " or " + TextBox_Username.Text.Replace('a', '@').Replace('A', '@').Replace('i', '1').Replace('I', '1').Replace('o', '0').Replace('O', '0').Replace('e', '3').Replace('E', '3');
            }
            else if(TextBox_Password.Text.Length < 5 || TextBox_Password.Text == "Password" || !Regex.IsMatch(TextBox_Password.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            {
                return "Please enter valid PASSWORD details!\n-Must be at least 5 characters long\n-One upper and lower case character\n-One diget";
            }
            else if (TextBox_Forename.Text.Length < 1 || TextBox_Forename.Text == "Forename")
            {
                return "Please enter valid FORENAME details!";
            }
            else if (TextBox_Surname.Text.Length < 1 || TextBox_Surname.Text == "Surname")
            {
                return "Please enter valid SURNAME details!";
            }
            else
            {
                return "Passed!";
            }

            
        }
        private void SendData()
        {
            StreamWriter Writer = new StreamWriter(ClientDatabasePathArguement, true);
            Writer.WriteLine(TextBox_Username.Text + "," + TextBox_Password.Text + "," + TextBox_Forename.Text + "," + TextBox_Surname.Text + "," + TextBox_Email.Text + "," + TextBox_PhoneNumber.Text + "," + TextBox_Address1.Text + "," + TextBox_Address2.Text + ",Client");
            Writer.Flush();
            Writer.Close();
            Label_Output.Content = "Completed";
            LoggedUsername = TextBox_Username.Text;
            LoggedPassword = TextBox_Password.Text;
            LoggedPermission = "Client";
            this.Close();
        }
        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            string ReturnText = CheckData();
            if(ReturnText == "Passed!")
            {
                Label_Output.Foreground = new SolidColorBrush(Colors.White);
                Label_Output.Content = "Saving details...";
                SendData();
            }
            else
            {
                Label_Output.Foreground = new SolidColorBrush(Colors.Red);
                Label_Output.Content = ReturnText;
            }
            

        }
    }
}
