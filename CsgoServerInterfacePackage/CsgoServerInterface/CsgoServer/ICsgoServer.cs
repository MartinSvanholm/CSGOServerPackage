using Newtonsoft.Json;

namespace CsgoServerInterface.CsgoServer;

public interface ICsgoServer
{
    public Task<ICsgoServer> StartServer(HttpClient httpClient);
    public Task<ICsgoServer> StopServer(HttpClient httpClient);
    public Task<ICsgoServer> StartQuickmatch(HttpClient httpClient, bool withOvertime);
    public Task<ICsgoServer> StartNadePractice(HttpClient httpClient);
    public Task<ICsgoServer> StopNadePractice(HttpClient httpClient);

    [JsonProperty("booting")]
    public bool Booting { get; set; }

    [JsonProperty("csgo_settings")]
    public CsgoSettings CsgoSettings { get; set; }

    [JsonProperty("game")]
    public string Game { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("ip")]
    public string Ip { get; set; }

    [JsonProperty("location")]
    public string Location { get; set; }

    [JsonProperty("match_id")]
    public string MatchId { get; set; }

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
}