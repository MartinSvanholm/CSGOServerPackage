using Newtonsoft.Json;

namespace CsgoServerInterface.CsgoServer;

public class Ports
{
    public Ports(int game)
    {
        Game = game;
    }

    [JsonProperty("game")]
    public int Game { get; set; }

    [JsonProperty("gotv")]
    public int? Gotv { get; set; }

    [JsonProperty("gotv_secondary")]
    public int? GotvSecondary { get; set; }

    [JsonProperty("query")]
    public int? Query { get; set; }
}
