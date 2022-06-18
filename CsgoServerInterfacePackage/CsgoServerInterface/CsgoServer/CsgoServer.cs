using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer
{
    public class CsgoServer : ICsgoServer
    {
        public CsgoServer(HttpClient httpClient, bool booting, CsgoSettings csgoSettings, string game, string id, string ip, string location, string matchId, string name, bool on, int playersOnline, Ports ports, string rawIp)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
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

        public HttpClient HttpClient { get; set; }
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

        public Task<ICsgoServer> StartNadePractice(HttpClient httpClient)
        {
            throw new NotImplementedException();
        }

        public Task<ICsgoServer> StartQuickmatch(HttpClient httpClient, bool withOvertime)
        {
            throw new NotImplementedException();
        }

        public Task<ICsgoServer> StartServer(HttpClient httpClient)
        {
            throw new NotImplementedException();
        }

        public Task<ICsgoServer> StopNadePractice(HttpClient httpClient)
        {
            throw new NotImplementedException();
        }

        public Task<ICsgoServer> StopServer(HttpClient httpClient)
        {
            throw new NotImplementedException();
        }
    }
}
