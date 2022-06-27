using CsgoServerInterface.CsgoServer;
using System.Net.Http.Headers;
using System.Text;

List<ICsgoServer> csgoServerList = new();
HttpClient client = new HttpClient();

client.BaseAddress = new Uri("https://dathost.net");
client.DefaultRequestHeaders.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"ms@hobrovikings.dk:hobrovikings1212")));

string uri = client.BaseAddress.ToString() + "/api/0.1/game-servers";

using HttpResponseMessage responseMessage = await client.GetAsync(uri);

if (responseMessage.IsSuccessStatusCode)
{
    try
    {
        csgoServerList.AddRange(await responseMessage.Content.ReadAsAsync<List<DatHostServer>>());
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
else
{
    Console.WriteLine(responseMessage.ReasonPhrase);
}

try
{
    CsgoServer csgoServer = new(csgoSettings: new("hvk1212", "vikings"), "1", "rodrik.dathost.net", "Hobro Vikings", ports: new(28145), "51.77.68.119");

    await csgoServer.RunCommand(client, "echo hi");
    csgoServer.Rcon.Dispose();
    csgoServerList.Add(csgoServer);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

async Task<DatHostServer> GetDatHostServerAsync()
{
    ArgumentNullException.ThrowIfNull(client.BaseAddress, nameof(client.BaseAddress));

    DatHostServer datHostServer;

    string uri = client.BaseAddress.ToString() + "/api/0.1/game-servers/{SERVER ID}";

    using HttpResponseMessage responseMessage = await client.GetAsync(uri);

    if (responseMessage.IsSuccessStatusCode)
    {
        try
        {
            datHostServer = await responseMessage.Content.ReadAsAsync<DatHostServer>();
            return datHostServer;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    else
    {
        Console.WriteLine(responseMessage.ReasonPhrase);
        throw new Exception(responseMessage.ReasonPhrase);
    }
}