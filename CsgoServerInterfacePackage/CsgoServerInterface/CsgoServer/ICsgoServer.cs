using Newtonsoft.Json;

namespace CsgoServerInterface.CsgoServer
{
    public interface ICsgoServer
    {
        [JsonProperty("booting")]
        bool Booting { get; set; }

        [JsonProperty("csgo_settings")]
        CsgoSettings CsgoSettings { get; set; }

        [JsonProperty("game")]
        string? Game { get; set; }

        [JsonProperty("id")]
        string Id { get; set; }

        [JsonProperty("ip")]
        string Ip { get; set; }

        [JsonProperty("match_id")]
        string? MatchId { get; set; }

        [JsonProperty("name")]
        string Name { get; set; }

        [JsonProperty("on")]
        bool On { get; set; }

        [JsonProperty("players_online")]
        int PlayersOnline { get; set; }

        [JsonProperty("ports")]
        Ports Ports { get; set; }

        [JsonProperty("raw_ip")]
        string RawIp { get; set; }

        Task<ICsgoServer> RunCommand(HttpClient httpClient, string command);
        Task<ICsgoServer> StartServer(HttpClient httpClient);
        Task<ICsgoServer> StopServer(HttpClient httpClient);
        string GetStatus();
        string GetConnectionIp();
    }
}