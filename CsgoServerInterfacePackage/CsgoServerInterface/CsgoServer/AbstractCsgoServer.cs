using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer;

public abstract class AbstractCsgoServer
{
    [JsonProperty("booting")]
    public bool Booting { get; set; }

    [JsonProperty("csgo_settings")]
    public CsgoSettings? CsgoSettings { get; set; }

    [JsonProperty("game")]
    public string? Game { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("ip")]
    public string? Ip { get; set; }

    [JsonProperty("location")]
    public string? Location { get; set; }

    [JsonProperty("match_id")]
    public string? MatchId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("on")]
    public bool On { get; set; }

    [JsonProperty("players_online")]
    public int PlayersOnline { get; set; }

    [JsonProperty("ports")]
    public Ports? Ports { get; set; }

    [JsonProperty("raw_ip")]
    public string? RawIp { get; set; }

    public virtual Task<AbstractCsgoServer> RunCommand(HttpClient httpClient, string command)
    {
        throw new NotImplementedException();
    }

    public virtual Task<AbstractCsgoServer> StartServer(HttpClient httpClient)
    {
        throw new NotImplementedException();
    }
    public virtual Task<AbstractCsgoServer> StopServer(HttpClient httpClient)
    {
        throw new NotImplementedException();
    }

    public virtual Task<AbstractCsgoServer> StartQuickmatch(HttpClient httpClient, bool withOvertime = false, string cfg = "esportliga_start")
    {
        throw new NotImplementedException();
    }

    public virtual Task<AbstractCsgoServer> StartNadePractice(HttpClient httpClient, string cfg = "sm_prac")
    {
        throw new NotImplementedException();
    }

    public virtual Task<AbstractCsgoServer> StopNadePractice(HttpClient httpClient)
    {
        throw new NotImplementedException();
    }

    public string GetConnectionIp()
    {
        if (CsgoSettings.Password == "")
            return $"connect {Ip}:{Ports.Game}";
        else
            return $"connect {Ip}:{Ports.Game}; password {CsgoSettings.Password}";
    }

    public string GetStatus()
    {
        if (Booting == true)
            return "Booting";
        else if (On == true)
            return "On";
        else
            return "Off";
    }
}