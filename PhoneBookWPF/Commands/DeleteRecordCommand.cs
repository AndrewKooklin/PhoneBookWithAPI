using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.View;
using PhoneBookWPF.ViewModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class DeleteRecordCommand : ICommand
    {

        public DeleteRecordCommand()
        {
        }

        public event EventHandler CanExecuteChanged;
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        private string urlRequest = "";
        private HttpResponseMessage response = new HttpResponseMessage();
        private Records records = new Records();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if(parameter != null)
            {
                var fieldElements = (object[])parameter;
                TextBox tbrecordId = (TextBox)fieldElements[0];

                if (String.IsNullOrEmpty(tbrecordId.Text))
                {
                    App.ActionsWithRecordView.tbResult.Text = "Выберите запись!";
                    return;
                }
                else
                {
                    App.ActionsWithRecordView.tbResult.Text = "";
                }

                TextBox tbRecordLastName = (TextBox)fieldElements[1];
                TextBox tbRecordFirstName = (TextBox)fieldElements[2];
                TextBox tbRecordFathersName = (TextBox)fieldElements[3];
                TextBox tbRecordPhoneNumber = (TextBox)fieldElements[4];
                TextBox tbRecordAddress = (TextBox)fieldElements[5];
                TextBox tbRecordDescription = (TextBox)fieldElements[6];

                urlRequest = $"{url}" + "DeleteRecord/DeleteRecord/" + $"{tbrecordId.Text}";
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await _httpClient.DeleteAsync(urlRequest);
                }
                if (!response.IsSuccessStatusCode)
                {
                    App.ActionsWithRecordView.tbResult.Text = "Ошибка, проверьте работу" +
                                                              "\nAPI сервера!";
                    return;
                }
                else
                {
                    tbrecordId.Text = "";
                    tbRecordLastName.Text = "";
                    tbRecordFirstName.Text = "";
                    tbRecordFathersName.Text = "";
                    tbRecordPhoneNumber.Text = "";
                    tbRecordAddress.Text = "";
                    tbRecordDescription.Text = "";

                    App.RecordsView.lbPhoneBookRecords.ItemsSource = null;
                    App.RecordsView.lbPhoneBookRecords.ItemsSource = records.GetRecords().GetAwaiter().GetResult();

                    App.ActionsWithRecordView.tbResult.Text = "Запись удалена!";
                }
            }
        }
    }
}
