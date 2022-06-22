using CsgoServerInterface.CsgoServer;
using System.Net.Http.Headers;
using System.Text;

List<DatHostServer> csgoServers = new List<DatHostServer>();
List<AbstractCsgoServer> csgoServerList = new List<AbstractCsgoServer>();
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
        csgoServers = await responseMessage.Content.ReadAsAsync<List<DatHostServer>>();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    int i = 0;
    foreach (DatHostServer server in csgoServers)
    {
        csgoServerList.Add(server);
        Console.WriteLine(server.Name);

        if (i == 0)
        {
            AbstractCsgoServer csgoServer = await server.StartServer(client);

            Console.WriteLine(csgoServer.Booting);
        }
        i++;
    }
}
else
{
    Console.WriteLine(responseMessage.ReasonPhrase);
}