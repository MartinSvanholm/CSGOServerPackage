using Newtonsoft.Json;

namespace CsgoServerInterface.CsgoServer;

public class CsgoSettings
{
    public CsgoSettings(string password, string rcon)
    {
        Password = password ?? throw new ArgumentNullException(nameof(password));
        Rcon = rcon ?? throw new ArgumentNullException(nameof(rcon));
    }

    [JsonProperty("autoload_configs")]
    public List<string>? AutoloadConfigs { get; set; }

    [JsonProperty("disable_1v1_warmup_arenas")]
    public bool Disable1v1WarmupArenas { get; set; }

    [JsonProperty("disable_bots")]
    public bool DisableBots { get; set; }

    [JsonProperty("enable_csay_plugin")]
    public bool EnableCsayPlugin { get; set; }

    [JsonProperty("enable_gotv")]
    public bool EnableGotv { get; set; }

    [JsonProperty("enable_gotv_secondary")]
    public bool EnableGotvSecondary { get; set; }

    [JsonProperty("enable_sourcemod")]
    public bool EnableSourcemod { get; set; }

    [JsonProperty("game_mode")]
    public string? GameMode { get; set; }

    [JsonProperty("mapgroup")]
    public string? Mapgroup { get; set; }

    [JsonProperty("mapgroup_start_map")]
    public string? MapgroupStartMap { get; set; }

    [JsonProperty("maps_source")]
    public string? MapsSource { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("rcon")]
    public string Rcon { get; set; }

    [JsonProperty("sourcemod_admins")]
    public string? SourcemodAdmins { get; set; }

    [JsonProperty("sourcemod_plugins")]
    public List<string>? SourcemodPlugins { get; set; }

    [JsonProperty("steam_game_server_login_token")]
    public string? SteamGameServerLoginToken { get; set; }

    [JsonProperty("tickrate")]
    public double Tickrate { get; set; }

    [JsonProperty("workshop_authkey")]
    public string? WorkshopAuthkey { get; set; }

    [JsonProperty("workshop_id")]
    public string? WorkshopId { get; set; }

    [JsonProperty("workshop_start_map_id")]
    public string? WorkshopStartMapId { get; set; }
}
