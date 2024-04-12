using CsvHelper.Configuration;
using CsvHelper;
using OrderDotzCatalogByDiscount;
using System.Text.Json;
using System.Globalization;
using System.Text;

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://api.dotz.com.br/rewards/api/default/");
var lista = new List<Item>();
int lastPage = 2;
int pageSize = 100;

for (int i = 1; i < lastPage; i++)
{
    try
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"products?_page={i}&_pageSize={pageSize}");
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        string responsejson = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<Rootobject>(responsejson);

            if (result is not null)
            {
                lastPage = result.products._total / pageSize;
                Console.WriteLine($" Reading {lista.Count} of {result.products._total} products.");
                foreach (var item in result.products.items)
                {
                    lista.Add(item);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message.ToString());
        continue;
    }
}

CsvConfiguration _configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = ";",
    HasHeaderRecord = true,
    Encoding = Encoding.UTF8,
    IgnoreBlankLines = true
};

lista = lista.OrderByDescending(o => o.discountRate).ToList();

var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
var currentDirectory = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent;
using (var writer = new StreamWriter($"{currentDirectory?.FullName}\\file.csv", false, Encoding.UTF8))
using (var csv = new CsvWriter(writer, _configuration))
{
    csv.WriteRecords(lista);
}

Console.WriteLine("The end...");

