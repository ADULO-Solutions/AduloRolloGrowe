using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AduloRolloGrowe
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        Login log = new Login();
        public frmLogin()
        {
            InitializeComponent();
            txtUser.Text = log.User;
            txtPassword.Password = log.Password;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            log.User = txtUser.Text;
            log.Password = txtPassword.Password;
            log.Save();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
