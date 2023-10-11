namespace mekashron_test;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public class SoapRequestSender
{
    public async Task<string> SendSoapRequestAsync()
    {
        string soapRequestContent = @"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://isapi.icu-tech.com/icutech-test.dll"">
                <soapenv:Header/>
                <soapenv:Body>
                    <web:RegisterNewCustomer>
                        <!-- Ваши параметры метода RegisterNewCustomer здесь -->
                    </web:RegisterNewCustomer>
                </soapenv:Body>
            </soapenv:Envelope>";

        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://isapi.icu-tech.com/icutech-test.dll"),
                Content = new StringContent(soapRequestContent, Encoding.UTF8, "text/xml")
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }
}
