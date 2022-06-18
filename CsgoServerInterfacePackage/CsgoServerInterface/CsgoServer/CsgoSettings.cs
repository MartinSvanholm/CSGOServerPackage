using Newtonsoft.Json;

namespace CsgoServerInterface.CsgoServer;

public class CsgoSettings
{
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

    [JsonProperty("insecure")]
    public bool Insecure { get; set; }

    [JsonProperty("mapgroup")]
    public string? Mapgroup { get; set; }

    [JsonProperty("mapgroup_start_map")]
    public string? MapgroupStartMap { get; set; }

    [JsonProperty("maps_source")]
    public string? MapsSource { get; set; }

    [JsonProperty("password")]
    public string? Password { get; set; }

    [JsonProperty("private_server")]
    public bool PrivateServer { get; set; }

    [JsonProperty("pure_server")]
    public bool PureServer { get; set; }

    [JsonProperty("rcon")]
    public string? Rcon { get; set; }

    [JsonProperty("slots")]
    public int Slots { get; set; }

    [JsonProperty("sourcemod_admins")]
    public string? SourcemodAdmins { get; set; }

    [JsonProperty("sourcemod_plugins")]
    public List<string>? SourcemodPlugins { get; set; }

    [JsonProperty("steam_game_server_login_token")]
    public string? SteamGameServerLoginToken { get; set; }

    [JsonProperty("tickrate")]
    public int Tickrate { get; set; }

    [JsonProperty("workshop_authkey")]
    public string? WorkshopAuthkey { get; set; }

    [JsonProperty("workshop_id")]
    public string? WorkshopId { get; set; }

    [JsonProperty("workshop_start_map_id")]
    public string? WorkshopStartMapId { get; set; }
}
