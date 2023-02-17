// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;

List<string> MSIDN = new List<string>();
MSIDN.Add("255711429525");
MSIDN.Add("255674146544");
MSIDN.Add("255653163930");
MSIDN.Add("255653735039");
MSIDN.Add("255651511248");

var httpClient = new HttpClient();

var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");
var commentValue = new ProductInfoHeaderValue("(+http://www.example.com/ScraperBot.html)");
var userIP = new ProductInfoHeaderValue("(5.62.61.175)");


httpClient.DefaultRequestHeaders.UserAgent.Add(productValue);
httpClient.DefaultRequestHeaders.UserAgent.Add(commentValue);
httpClient.DefaultRequestHeaders.UserAgent.Add(userIP);

foreach (var key in MSIDN)
{
    var request = new HttpRequestMessage(HttpMethod.Get, $"http://52.32.103.173/TigoWapPromotion/TigoTanzaniaServlet?product=TigoAcademy&service=TigoTanzania&type=direct&PID=null&msisdn={key}");
    var resp = await httpClient.SendAsync(request);
    Console.WriteLine(resp);
}
Console.ReadLine();
