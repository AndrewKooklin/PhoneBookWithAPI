using PhoneBookWPF.View;
using System;
using System.Net.Mail;
using System.Windows.Controls;

namespace PhoneBookWPF.HelpMethods
{
    public class CheckInputFields
    {
        public bool CheckFieldsRecord(ActionsWithRecordView recordView, object fields)
        {
            var fieldElements = (object[])fields;
            string recordId = fieldElements[0].ToString();
            string recordLastName = fieldElements[1].ToString();
            string recordFirstName = fieldElements[2].ToString();
            string recordFathersName = fieldElements[3].ToString();
            string recordPhoneNumber = fieldElements[4].ToString();
            string recordAddress = fieldElements[5].ToString();
            string recordDescription = fieldElements[6].ToString();

            if (String.IsNullOrEmpty(recordLastName))
            {
                recordView.tbErrorLastName.Text = "Заполните поле \"Фамилия\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(recordLastName) && recordLastName.Length < 3)
            {
                recordView.tbErrorLastName.Text = "Длина не менее 3 символов";
                return false;
            }
            else
            {
                recordView.tbErrorLastName.Text = "";
            }
            if (String.IsNullOrEmpty(recordFirstName))
            {
                recordView.tbErrorFirstName.Text = "Заполните поле \"Имя\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(recordFirstName) && recordFirstName.Length < 3)
            {
                recordView.tbErrorFirstName.Text = "Длина не менее 3 символов";
                return false;
            }
            else
            {
                recordView.tbErrorFirstName.Text = "";
            }
            if (String.IsNullOrEmpty(recordFathersName))
            {
                recordView.tbErrorFathersName.Text = "Заполните поле \"Отчество\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(recordFathersName) && recordFathersName.Length < 3)
            {
                recordView.tbErrorFathersName.Text = "Длина не менее 3 символов";
                return false;
            }
            else
            {
                recordView.tbErrorFathersName.Text = "";
            }
            if (String.IsNullOrEmpty(recordPhoneNumber))
            {
                recordView.tbErrorPhoneNumber.Text = "Заполните поле \"Телефон\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(recordPhoneNumber) && recordPhoneNumber.Length < 11)
            {
                recordView.tbErrorPhoneNumber.Text = "Длина не менее 11 символов";
                return false;
            }
            else
            {
                recordView.tbErrorLastName.Text = "";
                recordView.tbErrorFirstName.Text = "";
                recordView.tbErrorFathersName.Text = "";
                recordView.tbErrorPhoneNumber.Text = "";
                return true;
            }
        }

        public bool CheckFieldsRole(ActionsWithRoleView roleView, object fields)
        {
            var fieldElements = (object[])fields;
            string roleId = fieldElements[0].ToString();
            string roleName = fieldElements[1].ToString();

            if (String.IsNullOrEmpty(roleName))
            {
                roleView.tbErrorRoleName.Text = "Заполните поле \"Роль\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(roleName) && roleName.Length < 3)
            {
                roleView.tbErrorRoleName.Text = "Длина не менее 3 символов";
                return false;
            }
            else
            {
                roleView.tbRoleName.Text = "";
                return true;
            }
        }

        public bool CheckFieldsAddUser(ActionAddUserView userView, object fields)
        {
            var fieldElements = (object[])fields;
            string userEmail = fieldElements[0].ToString();
            string userPassword = fieldElements[1].ToString();

            if (String.IsNullOrEmpty(userEmail))
            {
                userView.tbErrorEmail.Text = "Заполните поле \"Email\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(userEmail))
            {
                var valid = true;

                try
                {
                    var emailAddress = new MailAddress(userEmail);
                }
                catch
                {
                    valid = false;
                }
                if(valid == false)
                {
                    userView.tbErrorEmail.Text = "Поле \"Email\" формата" +
                                                 "\nname@site.com";
                    return false;
                }
                else
                {
                    userView.tbErrorEmail.Text = "";
                    //return true;
                }
            }
            if (String.IsNullOrEmpty(userPassword))
            {
                userView.tbErrorPassword.Text = "Заполните поле \"Password\"";
                return false;
            }
            if (!String.IsNullOrEmpty(userPassword) && userPassword.Length < 6)
            {
                userView.tbErrorPassword.Text = "Длина не менее 6 символов";
                return false;
            }
            else
            {
                userView.tbErrorEmail.Text = "";
                userView.tbErrorPassword.Text = "";
                return true;
            }
        }

        public bool CheckFieldsDeleteUser(ActionDeleteUserView userView, object fields)
        {
            var fieldElements = (object[])fields;
            string userId = fieldElements[0].ToString();
            string userEmail = fieldElements[1].ToString();

            if (String.IsNullOrEmpty(userId))
            {
                userView.tbResult.Text = "Выберите пользователя !";
                return false;
            }
            if (String.IsNullOrEmpty(userEmail))
            {
                userView.tbErrorEmail.Text = "Заполните поле \"Email\"";
                return false;
            }
            else if (!String.IsNullOrEmpty(userEmail))
            {
                var valid = true;

                try
                {
                    var emailAddress = new MailAddress(userEmail);
                }
                catch
                {
                    valid = false;
                }
                if (valid == false)
                {
                    userView.tbErrorEmail.Text = "Поле \"Email\" формата name@site.com";
                    return false;
                }
                else
                {
                    userView.tbErrorEmail.Text = "";
                    return true;
                }
            }
            else
            {
                userView.tbErrorEmail.Text = "";
                userView.tbResult.Text = "";
                return true;
            }
        }

        public bool CheckFieldsAddRoleUser(ActionsRoleUserView userView, object fields)
        {
            var fieldElements = (object[])fields;
            string userId = fieldElements[0].ToString();
            ComboBox cbRole = (ComboBox)fieldElements[1];
            

            if (String.IsNullOrEmpty(userId))
            {
                userView.tbResult.Text = "Выберите пользователя!";
                return false;
            }
            if (cbRole.SelectedItem == null)
            {
                userView.tbResult.Text = "Выберите роль!";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
