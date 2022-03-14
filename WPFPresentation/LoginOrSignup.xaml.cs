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
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using DataObjects;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for LoginOrSignup.xaml
    /// </summary>
    public partial class LoginOrSignup : Window
    {
        UserManager _userManager = null;
        User _user = null;

        public LoginOrSignup(UserManager userManager)
        {
            _userManager = userManager;
            InitializeComponent();
        }

        private void btnLoginNotSignup_Click(object sender, RoutedEventArgs e)
        {
            var email = this.txtLoginEmail.Text;
            var pwd = this.pwdLoginPassword.Password;

            try
            {
                setUser(_userManager.LoginUser(email, pwd));
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void setUser(User user)
        {
            _user = user;
        }

        public User getUser()
        {
            return _user;
        }

        private void btnSignupNotLogin_Click(object sender, RoutedEventArgs e)
        {
            if(pwdSignupPassword.Password != pwdSignupRetypePassword.Password)
            {
                MessageBox.Show("New password and retyped password must match.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                pwdSignupPassword.Password = "";
                pwdSignupRetypePassword.Password = "";
                pwdSignupPassword.Focus();
                return;
            }
            if(txtSignupEmail.Text == "" || txtSignupUsername.Text == "")
            {
                MessageBox.Show("Sign up failed. Missing information.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var username = this.txtSignupUsername.Text;
                var email = this.txtSignupEmail.Text;
                var pwd = this.pwdSignupPassword.Password;
                var retypePwd = this.pwdSignupRetypePassword.Password;
                List<String> roles = new List<String>();
                roles.Add("Logged in");

                int result = _userManager.InsertNewUser(username, email, pwd, roles);
                if (result == 1)
                {
                    setUser(_userManager.LoginUser(email, pwd));
                    this.DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
