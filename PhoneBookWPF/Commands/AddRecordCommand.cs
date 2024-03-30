using Newtonsoft.Json;
using PhoneBookWPF.HelpMethods;
using PhoneBookWPF.Model;
using PhoneBookWPF.View;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Input;

namespace PhoneBookWPF.Commands
{
    public class AddRecordCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HttpClient _httpClient { get; set; }
        private string url = @"https://localhost:44379/api/";
        private string urlRequest = "";
        private HttpResponseMessage response = new HttpResponseMessage();
        private bool apiResponseConvert;
        private Records records = new Records();
        private CheckInputFields checkInputFields = new CheckInputFields();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            var checkInput = checkInputFields.CheckFieldsRecord(App.ActionsWithRecordView, parameter);
            if (!checkInput)
            {
                return;
            }
            else
            {
                var fieldElements = (object[])parameter;
                string recordId = fieldElements[0].ToString();
                string recordLastName = fieldElements[1].ToString();
                string recordFirstName = fieldElements[2].ToString();
                string recordFathersName = fieldElements[3].ToString();
                string recordPhoneNumber = fieldElements[4].ToString();
                string recordAddress = fieldElements[5].ToString();
                string recordDescription = fieldElements[6].ToString();

                PhoneBookRecord record = new PhoneBookRecord
                {
                    LastName = recordLastName,
                    FirstName = recordFirstName,
                    FathersName = recordFathersName,
                    PhoneNumber = recordPhoneNumber,
                    Address = recordAddress,
                    Description = recordDescription
                };

                urlRequest = $"{url}" + "CreateRecordAPI/CreateRecord/" + $"{record}";
                using (_httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (response = await _httpClient.PostAsJsonAsync(urlRequest, record))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        apiResponseConvert = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }

                if (!apiResponseConvert)
                {
                    App.ActionsWithRecordView.tbResult.Text = "Ошибка, проверьте работу" +
                                                              "\nAPI сервера!";
                    return;
                }
                else
                {
                    App.RecordsView.lbPhoneBookRecords.ItemsSource = null;
                    App.RecordsView.lbPhoneBookRecords.ItemsSource = records.GetRecords().GetAwaiter().GetResult();
                    App.ActionsWithRecordView.tbResult.Text = "Запись добавлена!";
                }
            }
        }
    }
}
