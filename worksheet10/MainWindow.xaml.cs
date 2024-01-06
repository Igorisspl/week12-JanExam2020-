//https://github.com/Igorisspl/week12-JanExam2020-.git 

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace worksheet10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Account> accounts  = new List<Account>();
        List<Account> filteredAccounts = new List<Account>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //create 4 accounts
            CurrentAccount ca1= new CurrentAccount("Joe","Doe",1000,DateTime.Now.AddYears(-2),"12345678");
            CurrentAccount ca2 = new CurrentAccount("Jone", "Doe", 2000, DateTime.Now.AddYears(-3), "12345678");

            CurrentAccount ca3 = new CurrentAccount("John", "Smith", 3000, DateTime.Now.AddYears(-4), "12345678");
            CurrentAccount ca4 = new CurrentAccount("Jane", "Smith", 4000, DateTime.Now.AddYears(-5), "12345678");

            //add to account list
            accounts.Add(ca1);
            accounts.Add(ca2);
            accounts.Add(ca3);
            accounts.Add(ca4);

            //display
            lbxAccounts.ItemsSource = accounts;

        }

        private void lbxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //determine what accounts is selected
            Account selected = lbxAccounts.SelectedItem as Account;

            //check for null
            if(selected != null)
            {
                //update display
                UpdateDisplay(selected);

            }



        }

        private void UpdateDisplay(Account selected)
        {
            tblkFirstName.Text = selected.FirstName;
            tblkLastName.Text = selected.LastName;
            tblkBalance.Text = selected.Balance.ToString("c");
            tblkAccountType.Text = selected.GetType().Name;
            tblkInterestDate.Text = selected.InterestDate.ToString("d");
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            //reset the listbox display
            lbxAccounts.ItemsSource = null;

            //clear the filter
            filteredAccounts.Clear();

            //determine what checkbox was selected
            bool savings = false, current = false;

            if(cbCurrent.IsChecked.Value)
            current= true;

            if(cbSavings.IsChecked.Value)
               savings= true;

            //update display
            if(current && savings)
            {
                lbxAccounts.ItemsSource= accounts;
            }
            //search for current accounts
            else if (current)
            {
                foreach(Account account in accounts)
                {
                    if(account is  CurrentAccount)
                        filteredAccounts.Add(account);
                }
                lbxAccounts.ItemsSource= filteredAccounts;
            }
            //search for savings accounts
            else if(savings)
            {
                foreach (Account account in accounts)
                {
                    if (account is SavingsAccount)
                        filteredAccounts.Add(account);
                }
                lbxAccounts.ItemsSource = filteredAccounts;
            }
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            //read amount to add
            decimal amount = 0;
            if(Decimal.TryParse(tbxTransactionAmount.Text, out amount))
            {
                //determine selected amount
                Account selected = lbxAccounts.SelectedItem as Account;

                if(selected != null)
                {
                    //Add amount
                    selected.Deposits(amount);
                    UpdateDisplay(selected);
                }
            }
        }

        private void tbxTransactionAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxTransactionAmount.Clear();
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            //read amount to withdraw
            decimal amount = 0;
            if (Decimal.TryParse(tbxTransactionAmount.Text, out amount))
            {
                //determine selected amount
                Account selected = lbxAccounts.SelectedItem as Account;

                if (selected != null)
                {
                    //Add amount
                    selected.Withdraw(amount);
                    UpdateDisplay(selected);
                }
            }
        }

        private void btnInterest_Click(object sender, RoutedEventArgs e)
        {
            Account selected = lbxAccounts.SelectedItem as Account;

            if (selected != null)
            {

                selected.CalculateInterest();
                UpdateDisplay(selected);
            }
        }
    }
}
