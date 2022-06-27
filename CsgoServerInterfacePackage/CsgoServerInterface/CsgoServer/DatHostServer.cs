using CsgoServerInterface.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer;

/// <summary>
/// This is the class for CS:GO Servers from Dathost.net, these servers make use of the DatHost Api. Therefore these servers does not intergrate the rcon protocol.
/// </summary>
public class DatHostServer : ICsgoServer
{
    public DatHostServer(CsgoSettings csgoSettings, string game, string id, string ip, string name, Ports ports, string rawIp)
    {
        CsgoSettings = csgoSettings;
        Game = game ?? throw new ArgumentNullException(nameof(game));
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Ip = ip ?? throw new ArgumentNullException(nameof(ip));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Ports = ports ?? throw new ArgumentNullException(nameof(ports));
        RawIp = rawIp ?? throw new ArgumentNullException(nameof(rawIp));
    }

    [JsonProperty("booting")]
    public bool Booting { get; set; }

    [JsonProperty("csgo_settings")]
    public CsgoSettings? CsgoSettings { get; set; }

    [JsonProperty("game")]
    public string? Game { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("ip")]
    public string Ip { get; set; }

    [JsonProperty("match_id")]
    public string? MatchId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("on")]
    public bool On { get; set; }

    [JsonProperty("players_online")]
    public int PlayersOnline { get; set; }

    [JsonProperty("ports")]
    public Ports Ports { get; set; }

    [JsonProperty("raw_ip")]
    public string RawIp { get; set; }

    /// <summary>
    /// This method runs any command you could run in the cs:go console.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="command"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public async Task<ICsgoServer> RunCommand(HttpClient httpClient, string command)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", $"sm_prac" },
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
                responseMessage.ReasonPhrase = $"Could not run command: {command}";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    /// <summary>
    /// This method starts a nade practice.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public async Task<ICsgoServer> StartNadePractice(HttpClient httpClient, string? cfg)
    {
        if (cfg == null)
            cfg = ServerHelper.GetCfg("pracc.txt");

        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", cfg },
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
                responseMessage.ReasonPhrase = "Could not start nade-practice";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    /// <summary>
    /// This method starts a knife round.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="cfg"></param>
    /// <returns>CsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public async Task<ICsgoServer> StartKnife(HttpClient httpClient, string? cfg)
    {
        if (cfg == null)
            cfg = ServerHelper.GetCfg("knife.txt");

        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", cfg },
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
                responseMessage.ReasonPhrase = "Could not start knife";

            throw new CsgoServerException(responseMessage.ReasonPhrase, this, responseMessage.StatusCode);
        }
    }

    /// <summary>
    /// This method starts a quick match using the yousee esportleague cfg.
    /// 
    /// The parameter withOvertime specifies whether the match should be with overtime or not (using the default config).
    /// E.g. true = overtime.
    /// 
    /// The parameter cfg specifies a custom config which must be a string containing all the cs:go commands.
    /// E.g. ammo_grenade_limit_default 1; ammo_grenade_limit_flashbang 2; ... mp_restartgame 10;
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="withOvertime"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public async Task<ICsgoServer> StartQuickmatch(HttpClient httpClient, string? cfg, bool withOvertime = false)
    {
        if (cfg == null)
        {
            if (withOvertime)
                cfg = ServerHelper.GetCfg("esportliga_start_med_overtime.txt");
            else
                cfg = ServerHelper.GetCfg("esportliga_start.txt");
        }

        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        var values = new Dictionary<string, string>
            {
                { "line", cfg },
            };
        var content = new FormUrlEncodedContent(values);

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);

        if (responseMessage.IsSuccessStatusCode)
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

    /// <summary>
    /// This method start the cs:go server.
    /// 
    /// You can also use the method to restart the server if it is allready running.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public async Task<ICsgoServer> StartServer(HttpClient httpClient)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/start";

        using HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, null);

        if (responseMessage.IsSuccessStatusCode)
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

    /// <summary>
    /// This method stops the server.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
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

    /// <summary>
    /// Gets a string of the current status of the server.
    /// </summary>
    /// <returns>string</returns>
    public string GetStatus()
    {
        if (Booting)
            return "Booting";
        else if (On)
            return "On";
        else
            return "Off";
    }

    /// <summary>
    /// Gets a string of the connection ip, used for connection to the server.
    /// </summary>
    /// <returns>string</returns>
    public string GetConnectionIp()
    {
        if (CsgoSettings.Password == "")
            return $"connect {Ip}:{Ports.Game}";
        else
            return $"connect {Ip}:{Ports.Game}; password {CsgoSettings.Password}";
    }
}
