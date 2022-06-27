using CoreRCON;
using CoreRCON.Parsers.Standard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer;

public class CsgoServer : ICsgoServer
{
    public CsgoServer(CsgoSettings csgoSettings, string id, string ip, string name, Ports ports, string rawIp)
    {
        CsgoSettings = csgoSettings;
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Ip = ip ?? throw new ArgumentNullException(nameof(ip));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Ports = ports ?? throw new ArgumentNullException(nameof(ports));
        RawIp = rawIp ?? throw new ArgumentNullException(nameof(rawIp)); RCON rcon = new RCON(IPAddress.Parse(RawIp), (ushort)Ports.Game, CsgoSettings.Rcon);
        Rcon = new RCON(IPAddress.Parse(RawIp), (ushort)Ports.Game, CsgoSettings.Rcon);

        try
        {
            Init().Wait();
        }
        catch (Exception e)
        {
            if(e.InnerException != null)
                throw e.InnerException;
            else
                throw;
        }

    }

    public RCON Rcon { get; set; }

    [JsonProperty("booting")]
    public bool Booting { get; set; }

    [JsonProperty("csgo_settings")]
    public CsgoSettings CsgoSettings { get; set; }

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

    public async Task Init()
    {
        await Rcon.ConnectAsync();
    }

    /// <summary>
    /// This method runs any command you could run in the cs:go console.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="command"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public async Task<ICsgoServer> RunCommand(HttpClient httpClient, string command)
    {
        try
        {
            string response = await Rcon.SendCommandAsync(command);
            return this;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<ICsgoServer> StartServer(HttpClient httpClient)
    {
        throw new NotImplementedException("Can only start DatHostServers, please start the server manually.");
    }

    public Task<ICsgoServer> StopServer(HttpClient httpClient)
    {
        throw new NotImplementedException("Can only stop DatHostServers, please stop the server manually.");
    }
}