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
using System.Data;

namespace HexagonHotels
{
    /// <summary>
    /// Interaction logic for ClientDataForm.xaml
    /// </summary>
    //public class  Client
    //{
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string Forename { get; set; }
    //    public string Surname { get; set; }
    //    public string Email { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Permission { get; set; }
    //    public CheckBox Delete { get; set; }

    //    public Client(string username, string password, string forename, string surname, string email, string phonenumber, string address1, string address2, string permission)
    //    {
    //        Username = username;
    //        Password = password;
    //        Forename = forename;
    //        Surname = surname;
    //        Email = email;
    //        PhoneNumber = phonenumber;
    //        Address1 = address1;
    //        Address2 = address2;
    //        Permission = permission;
    //        Delete = new CheckBox();
    ////        Delete.Content = "Delete";
    ////}
    //}
    public partial class ClientDataForm : Window
    {
        public string ClientSessionUsername;
        public string ClientSessionPassword;
        public string ClientSessionPermission;
        string ClientDatabasePathArguement;
        public ClientDataForm(string ClientDatatbasePath, string LoggedUsername, string LoggedPassword, string LoggedPermission)
        {
            InitializeComponent();
            ClientSessionUsername = LoggedUsername;
            ClientSessionPassword = LoggedPassword;
            ClientSessionPermission = LoggedPermission;
            ClientDatabasePathArguement = ClientDatatbasePath;
            AdjustDisplayPermission();
        }
        //public IEnumerable<Client> ReadCSV()
        //{
        //    string[] Lines = File.ReadAllLines(ClientDatabasePathArguement);

        //    return Lines.Select(line =>
        //    {
        //        string[] SplitLine = line.Split(',');
        //        return new Client(SplitLine[0], SplitLine[1], SplitLine[2], SplitLine[3], SplitLine[4], SplitLine[5], SplitLine[6], SplitLine[7], SplitLine[8]);
        //    }
        //    );
        //}
        private void EditAccountClicked(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button)sender;
            if(buttonClicked.Content.Equals("Edit"))
            {
                buttonClicked.Content = "Save";
                Grid parent = (Grid)(buttonClicked).Parent;//this is the grid

                foreach (var item in parent.Children)
                {
                    if (item is TextBox)
                    {
                        TextBox ItemText = (TextBox)(item);
                        ItemText.IsEnabled = true;
                    }
                    else if (item is Button)
                    {

                    }

                }
            }
            else if(buttonClicked.Content.Equals("Save"))
            {

                buttonClicked.Content = "Edit";
                Grid parent = (Grid)(buttonClicked).Parent;//this is the grid
                List<string> CurrentAccount = new List<string>();
                foreach (var item in parent.Children)
                {
                    if (item is TextBox)
                    {
                        TextBox ItemText = (TextBox)(item);
                        ItemText.IsEnabled = false;
                        CurrentAccount.Add(ItemText.Text);
                    }
                    else if (item is Button)
                    {

                    }

                }

                List<string> toSave = new List<string>();
                //toSave.Add("Username,Password,Forename,Surname,Email Address,Phone Number,Address Line 1,Address Line 2,Permission Level");
                foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(CurrentAccount[0]) && SplitLine[1].Contains(CurrentAccount[1]))
                    {
                        string NewSave = CurrentAccount[0] + "," + CurrentAccount[1] + "," + CurrentAccount[2] + "," + CurrentAccount[3] + "," + CurrentAccount[4] + "," + CurrentAccount[5] + "," + CurrentAccount[6] + "," + CurrentAccount[7] + "," + CurrentAccount[8];
                        toSave.Add(NewSave);
                    }
                    else
                    {
                        toSave.Add(Line);
                    }
                }
                using (StreamWriter Writer = new StreamWriter(ClientDatabasePathArguement, false))
                {
                    foreach (string Line in toSave)
                    {
                        Writer.WriteLine(Line);
                    }
                }

            }
        }
        private void DeleteAccountClicked(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button)sender;
            if(buttonClicked.Content.Equals("Delete"))
            {
                buttonClicked.Content = "Confirm";
            }
            else if(buttonClicked.Content.Equals("Confirm"))
            {
                Grid parent = (Grid)(buttonClicked).Parent;//this is the grid

                //parent.Visibility = Visibility.Collapsed;
                List<string> CurrentAccount = new List<string>();
                foreach (var item in parent.Children)
                {
                    if (item is TextBox)
                    {
                        TextBox ItemText = (TextBox)(item);
                        CurrentAccount.Add(ItemText.Text);
                    }
                    else if (item is Button)
                    {

                    }

                }


                List<string> toSave = new List<string>();
                //toSave.Add("Username,Password,Forename,Surname,Email Address,Phone Number,Address Line 1,Address Line 2,Permission Level");
                foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(CurrentAccount[0]) && SplitLine[1].Contains(CurrentAccount[1]))
                    {

                    }
                    else
                    {
                        toSave.Add(Line);
                    }
                }
                using (StreamWriter Writer = new StreamWriter(ClientDatabasePathArguement, false))
                {
                    foreach (string Line in toSave)
                    {
                        Writer.WriteLine(Line);
                    }
                }
                parent.Visibility = Visibility.Collapsed;
            }

            

        }
        private void AdjustDisplayPermission()
        {
            if (ClientSessionPermission == "Admin")
            {
                Button_Edit.Visibility = Visibility.Collapsed;
                Button_Delete.Visibility = Visibility.Collapsed;
                TextBox_Username.Visibility = Visibility.Collapsed;
                TextBox_Password.Visibility = Visibility.Collapsed;
                TextBox_Forename.Visibility = Visibility.Collapsed;
                TextBox_Surname.Visibility = Visibility.Collapsed;
                TextBox_Email.Visibility = Visibility.Collapsed;
                TextBox_PhoneNumber.Visibility = Visibility.Collapsed;
                TextBox_Address1.Visibility = Visibility.Collapsed;
                TextBox_Address2.Visibility = Visibility.Collapsed;
                TextBox_AccountType.Visibility = Visibility.Collapsed;
                Label_Username.Visibility = Visibility.Collapsed;
                Label_Password.Visibility = Visibility.Collapsed;
                Label_Forename.Visibility = Visibility.Collapsed;
                Label_Surname.Visibility = Visibility.Collapsed;
                Label_Email.Visibility = Visibility.Collapsed;
                Label_PhoneNumber.Visibility = Visibility.Collapsed;
                Label_Address1.Visibility = Visibility.Collapsed;
                Label_Address2.Visibility = Visibility.Collapsed;
                Label_AccountType.Visibility = Visibility.Collapsed;
                //DataGrid_ClientData.Visibility = Visibility.Visible;


                


                foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
                {
                    Grid newGrid = new Grid();
                    newGrid.ShowGridLines = false;

                    GridLength length = new GridLength(100, GridUnitType.Pixel);

                    ColumnDefinition Column1 = new ColumnDefinition();
                    Column1.Width = length;
                    newGrid.ColumnDefinitions.Add(Column1);
                    ColumnDefinition Column2 = new ColumnDefinition();
                    Column2.Width = length;
                    newGrid.ColumnDefinitions.Add(Column2);
                    ColumnDefinition Column3 = new ColumnDefinition();
                    Column3.Width = length;
                    newGrid.ColumnDefinitions.Add(Column3);
                    ColumnDefinition Column4 = new ColumnDefinition();
                    Column4.Width = length;
                    newGrid.ColumnDefinitions.Add(Column4);
                    ColumnDefinition Column5 = new ColumnDefinition();
                    Column5.Width = length;
                    newGrid.ColumnDefinitions.Add(Column5);
                    ColumnDefinition Column6 = new ColumnDefinition();
                    Column6.Width = length;
                    newGrid.ColumnDefinitions.Add(Column6);
                    ColumnDefinition Column7 = new ColumnDefinition();
                    Column7.Width = length;
                    newGrid.ColumnDefinitions.Add(Column7);
                    ColumnDefinition Column8 = new ColumnDefinition();
                    Column8.Width = length;
                    newGrid.ColumnDefinitions.Add(Column8);
                    ColumnDefinition Column9 = new ColumnDefinition();
                    Column9.Width = length;
                    newGrid.ColumnDefinitions.Add(Column9);
                    ColumnDefinition Column10 = new ColumnDefinition();
                    Column10.Width = length;
                    newGrid.ColumnDefinitions.Add(Column10);
                    ColumnDefinition Column11 = new ColumnDefinition();
                    Column11.Width = length;
                    newGrid.ColumnDefinitions.Add(Column11);
                    RowDefinition Row1 = new RowDefinition();
                    newGrid.RowDefinitions.Add(Row1);

                    List<string> SplitLine = new List<string>(Line.Split(','));
                    int i = 0;
                    foreach (string Value in SplitLine)
                    {
                        TextBox newTextBox = new TextBox();
                        newTextBox.Text = Value;
                        newTextBox.Background = TextBox_Username.Background;
                        newTextBox.Foreground = TextBox_Username.Foreground;
                        newTextBox.IsEnabled = false;
                        Grid.SetRow(newTextBox, 0);
                        Grid.SetColumn(newTextBox, i);
                        newGrid.Children.Add(newTextBox);
                        if (!SplitLine[0].Equals("Username"))
                        {
                            //LinearGradientBrush BackgroundColours = new LinearGradientBrush(
                            //       Color.FromArgb(255, 0, 0, 0),
                            //       Color.FromArgb(255, 255, 0, 0),
                            //       new Point(0, 10),
                            //       new Point(200, 10)
                            //    );
                            Button newButton = new Button();
                            newButton.Content = "Edit";
                            //newButton.Background = BackgroundColours;
                            newButton.Background = Button_Cancel.Background;
                            newButton.Foreground = Button_Cancel.Foreground;
                            newButton.Click += EditAccountClicked;
                            Grid.SetRow(newButton, 0);
                            Grid.SetColumn(newButton, 9);
                            newGrid.Children.Add(newButton);
                            
                            Button newButton2 = new Button();
                            newButton2.Content = "Delete";
                            newButton2.Background = Button_Cancel.Background;
                            newButton2.Foreground = Button_Cancel.Foreground;
                            newButton2.Click += DeleteAccountClicked;
                            Grid.SetRow(newButton2, 0);
                            Grid.SetColumn(newButton2, 10);
                            newGrid.Children.Add(newButton2);
                        }

                         //< Button  x: Name = "Button_Cancel" Visibility = "Visible" HorizontalAlignment = "Center" Height = "24" Margin = "305,392,289,55"  VerticalAlignment = "Center" Width = "100" BorderBrush = "Black" Foreground = "White" AutomationProperties.Name = "TextBoxUsername" HorizontalContentAlignment = "Center" VerticalContentAlignment = "Center"  AutomationProperties.IsColumnHeader = "True" FontSize = "14" FontFamily = "Arial" FontWeight = "Bold" Content = "Cancel" ScrollViewer.VerticalScrollBarVisibility = "Disabled" Click = "Button_Cancel_Click" >
                                      
                         //                         < Button.Background >
                                      
                         //                             < RadialGradientBrush >
                                      
                         //                                 < GradientStop Color = "#FF313131" />
                                       
                         //                                  < GradientStop Color = "#BF5900FF" Offset = "1" />
                                          
                         //                                 </ RadialGradientBrush >
                                          
                         //                             </ Button.Background >
                                          
                         //                             < Button.Style >
                                                                  i++;
                    }

                    StackPannel_ClientData.Children.Add(newGrid);
                 }



            //    List<string> SplitLine = new List<string>(Line.Split(','));
            //foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
            //{

            //    List<string> SplitLine = new List<string>(Line.Split(','));

            //    Grid newGrid = new Grid();
            //    newGrid.ShowGridLines = true;

            //    ColumnDefinition gridCol1 = new ColumnDefinition();

            //    ColumnDefinition gridCol2 = new ColumnDefinition();

            //    ColumnDefinition gridCol3 = new ColumnDefinition();

            //    newGrid.ColumnDefinitions.Add(gridCol1);

            //    newGrid.ColumnDefinitions.Add(gridCol2);

            //    newGrid.ColumnDefinitions.Add(gridCol3);

            //   // foreach (string Value in SplitLine)
            //   // {
            //        TextBox newTextBox = new TextBox();
            //        newTextBox.Text = SplitLine[0];
            //        newGrid.Children.Add(newTextBox);
            //        Grid.SetRow(newTextBox, 0);
            //        Grid.SetColumn(newTextBox, 0);

            //    TextBox newTextBox2 = new TextBox();
            //    newTextBox2.Text = SplitLine[1];
            //    newGrid.Children.Add(newTextBox2);
            //    Grid.SetRow(newTextBox2, 1);
            //    Grid.SetColumn(newTextBox2, 1);

            //    newGrid.Children.Add(newTextBox);
            //   // newGrid.Children.Add(newTextBox2);

            //    // }
            //    StackPannel_ClientData.Children.Add(newGrid);
            //    //StackPannel_ClientData.Children.Add(newButton);
            //}

            //foreach(string Line in File.ReadLines(ClientDatabasePathArguement))
            //{
            //    List<string> SplitLine = new List<string>(Line.Split(','));
            //    ListView_ClientData.Items.Add(new Client(SplitLine[0], SplitLine[1], SplitLine[2], SplitLine[3], SplitLine[4], SplitLine[5], SplitLine[6], SplitLine[7], SplitLine[8]));

            //}
            //DataGrid_ClientData.ItemsSource = ReadCSV();
        }
            else
            {
                //DataGrid_ClientData.Visibility = Visibility.Collapsed;
                ScrollViewer_ClientData.Visibility = Visibility.Collapsed;
                Button_Edit.Visibility = Visibility.Visible;
                Button_Delete.Visibility = Visibility.Visible;
                TextBox_Username.Visibility = Visibility.Visible;
                TextBox_Password.Visibility = Visibility.Visible;
                TextBox_Forename.Visibility = Visibility.Visible;
                TextBox_Surname.Visibility = Visibility.Visible;
                TextBox_Email.Visibility = Visibility.Visible;
                TextBox_PhoneNumber.Visibility = Visibility.Visible;
                TextBox_Address1.Visibility = Visibility.Visible;
                TextBox_Address2.Visibility = Visibility.Visible;
                TextBox_AccountType.Visibility = Visibility.Visible;
                Label_Username.Visibility = Visibility.Visible;
                Label_Password.Visibility = Visibility.Visible;
                Label_Forename.Visibility = Visibility.Visible;
                Label_Surname.Visibility = Visibility.Visible;
                Label_Email.Visibility = Visibility.Visible;
                Label_PhoneNumber.Visibility = Visibility.Visible;
                Label_Address1.Visibility = Visibility.Visible;
                Label_Address2.Visibility = Visibility.Visible;
                Label_AccountType.Visibility = Visibility.Visible;

                foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(ClientSessionUsername) && SplitLine[1].Contains(ClientSessionPassword))
                    {
                        TextBox_Username.Text = SplitLine[0];
                        TextBox_Password.Text = SplitLine[1];
                        TextBox_Forename.Text = SplitLine[2];
                        TextBox_Surname.Text = SplitLine[3];
                        TextBox_Email.Text = SplitLine[4];
                        TextBox_PhoneNumber.Text = SplitLine[5];
                        TextBox_Address1.Text = SplitLine[6];
                        TextBox_Address2.Text = SplitLine[7];
                        TextBox_AccountType.Text = SplitLine[8];

                        return;
                    }
                }
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Button_Delete.Content.Equals("Discard"))
            {
                Button_Edit.Content = "Edit Account";
                Button_Delete.Content = "Delete Account";
                TextBox_Username.IsEnabled = false;
                TextBox_Password.IsEnabled = false;
                TextBox_Forename.IsEnabled = false;
                TextBox_Surname.IsEnabled = false;
                TextBox_Email.IsEnabled = false;
                TextBox_PhoneNumber.IsEnabled = false;
                TextBox_Address1.IsEnabled = false;
                TextBox_Address2.IsEnabled = false;
                TextBox_AccountType.IsEnabled = false;
                AdjustDisplayPermission();
            }
            else if(Button_Delete.Content.Equals("Delete Account"))
            {
                Button_Delete.Content = "Confirm";
                Button_Edit.Content = "Cancel";
                
            }
            else if(Button_Delete.Content.Equals("Confirm"))
            {
                List<string> toSave = new List<string>();
                //toSave.Add("Username,Password,Forename,Surname,Email Address,Phone Number,Address Line 1,Address Line 2,Permission Level");
                foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(ClientSessionUsername) && SplitLine[1].Contains(ClientSessionPassword))
                    {
                        
                    }
                    else
                    {
                        toSave.Add(Line);
                    }
                }
                using (StreamWriter Writer = new StreamWriter(ClientDatabasePathArguement, false))
                {
                    foreach (string Line in toSave)
                    {
                        Writer.WriteLine(Line);
                    }
                }
                ClientSessionUsername = "";
                ClientSessionPassword = "";
                this.Close();
            }
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if(Button_Edit.Content.Equals("Edit Account"))
            {
                Button_Edit.Content = "Save";
                Button_Delete.Content = "Discard";
                TextBox_Username.IsEnabled = false;
                TextBox_Password.IsEnabled = true;
                TextBox_Forename.IsEnabled = true;
                TextBox_Surname.IsEnabled = true;
                TextBox_Email.IsEnabled = true;
                TextBox_PhoneNumber.IsEnabled = true;
                TextBox_Address1.IsEnabled = true;
                TextBox_Address2.IsEnabled = true;
                TextBox_AccountType.IsEnabled = false;
            }
            else if(Button_Edit.Content.Equals("Cancel"))
            {
                Button_Edit.Content = "Edit Account";
                Button_Delete.Content = "Delete Account";
            }
            else if(Button_Edit.Content.Equals("Save"))
            {
                Button_Edit.Content = "Edit Account";
                Button_Delete.Content = "Delete Account";
                TextBox_Username.IsEnabled = false;
                TextBox_Password.IsEnabled = false;
                TextBox_Forename.IsEnabled = false;
                TextBox_Surname.IsEnabled = false;
                TextBox_Email.IsEnabled = false;
                TextBox_PhoneNumber.IsEnabled = false;
                TextBox_Address1.IsEnabled = false;
                TextBox_Address2.IsEnabled = false;
                TextBox_AccountType.IsEnabled = false;

                List<string> toSave = new List<string>();
                //toSave.Add("Username,Password,Forename,Surname,Email Address,Phone Number,Address Line 1,Address Line 2,Permission Level");
                foreach (string Line in File.ReadLines(ClientDatabasePathArguement))
                {
                    List<string> SplitLine = new List<string>(Line.Split(','));
                    if (SplitLine[0].Contains(ClientSessionUsername) && SplitLine[1].Contains(ClientSessionPassword))
                    {
                        string NewSave = TextBox_Username.Text + "," + TextBox_Password.Text + "," + TextBox_Forename.Text + "," + TextBox_Surname.Text + "," + TextBox_Email.Text + "," + TextBox_PhoneNumber.Text + "," + TextBox_Address1.Text + "," + TextBox_Address2.Text + "," + TextBox_AccountType.Text;
                        ClientSessionUsername = TextBox_Username.Text;
                        ClientSessionPassword = TextBox_Password.Text;
                        toSave.Add(NewSave);
                    }
                    else
                    {
                        toSave.Add(Line);
                    }
                }
                using (StreamWriter Writer = new StreamWriter(ClientDatabasePathArguement, false))
                {
                    foreach(string Line in toSave)
                    {
                        Writer.WriteLine(Line);
                    }
                }

            }
            
        }

        private void RowButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    MessageBox.Show(row.ToString());
                    MessageBox.Show(row.Resources.Count.ToString());
                    foreach (var item in row.Resources.Values)
                    {
                        MessageBox.Show(item.ToString());
                    }
                    foreach (var item in row.Resources.Keys)
                    {
                        MessageBox.Show(item.ToString());
                    }
                    foreach (var item in row.Resources.MergedDictionaries)
                    {
                        MessageBox.Show(item.ToString());
                    }


                    //row.Visibility = Visibility.Collapsed;
                    break;
                }
            }
        }
    }
}
