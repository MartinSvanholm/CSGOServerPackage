using CsgoServerInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer;

public class DatHostServer : ICsgoServer
{
    public DatHostServer(HttpClient httpClient, bool booting, CsgoSettings csgoSettings, string game, string id, string ip, string location, string matchId, string name, bool on, int playersOnline, Ports ports, string rawIp)
    {
        Booting = booting;
        CsgoSettings = csgoSettings ?? throw new ArgumentNullException(nameof(csgoSettings));
        Game = game ?? throw new ArgumentNullException(nameof(game));
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Ip = ip ?? throw new ArgumentNullException(nameof(ip));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        MatchId = matchId ?? throw new ArgumentNullException(nameof(matchId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        On = on;
        PlayersOnline = playersOnline;
        Ports = ports ?? throw new ArgumentNullException(nameof(ports));
        RawIp = rawIp ?? throw new ArgumentNullException(nameof(rawIp));
    }

    public bool Booting { get; set; }
    public CsgoSettings CsgoSettings { get; set; }
    public string Game { get; set; }
    public string Id { get; set; }
    public string Ip { get; set; }
    public string Location { get; set; }
    public string MatchId { get; set; }
    public string Name { get; set; }
    public bool On { get; set; }
    public int PlayersOnline { get; set; }
    public Ports Ports { get; set; }
    public string RawIp { get; set; }

    public async Task<ICsgoServer> StartNadePractice(HttpClient httpClient)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", $"sm_prac" },
            };
        var content = new FormUrlEncodedContent(values);

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);

        if(responseMessage.IsSuccessStatusCode)
        {
            return this;
        }
        else
        {
            if (responseMessage.ReasonPhrase == null)
                responseMessage.ReasonPhrase = "Could not start nade-practice";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    public async Task<ICsgoServer> StopNadePractice(HttpClient httpClient)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", $"sm_exitpractice" },
            };
        var content = new FormUrlEncodedContent(values);

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);

        if (responseMessage.IsSuccessStatusCode)
        {
            return this;
        }
        else
        {
            if (responseMessage.ReasonPhrase == null)
                responseMessage.ReasonPhrase = "Could not stop nade-practice";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    public async Task<ICsgoServer> StartQuickmatch(HttpClient httpClient, bool withOvertime)
    {
        string cfg = "esportliga_start";
        if (withOvertime)
            cfg = "esportliga_start_med_overtime";

        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", $"exec {cfg}" },
            };
        var content = new FormUrlEncodedContent(values);

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);

        if(responseMessage.IsSuccessStatusCode)
        {
            MatchId = "Quickmaatch";
            return this;
        }
        else
        {
            if (responseMessage.ReasonPhrase == null)
                responseMessage.ReasonPhrase = "Could not start match";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    public async Task<ICsgoServer> StartServer(HttpClient httpClient)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/start";

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, null);

        if(responseMessage.IsSuccessStatusCode)
        {
            return this;
        }
        else
        {
            if (responseMessage.ReasonPhrase == null)
                responseMessage.ReasonPhrase = $"Could not start server: {Name}";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    public async Task<ICsgoServer> StopServer(HttpClient httpClient)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/stop";

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, null);

        if (responseMessage.IsSuccessStatusCode)
        {
            return this;
        }
        else
        {
            if (responseMessage.ReasonPhrase == null)
                responseMessage.ReasonPhrase = $"Could not stop server: {Name}";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }
}
