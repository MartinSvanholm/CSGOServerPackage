using CsgoServerInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer;

/// <summary>
/// This is the class for CS:GO Servers from Dathost.net, these servers make use of the DatHost Api. Therefore these servers does not intergrate the rcon protocol.
/// </summary>
public class DatHostServer : AbstractCsgoServer
{
    private bool IsPracmodePratice = false;

    /// <summary>
    /// This method runs any command you could run in the cs:go console.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="command"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public override async Task<AbstractCsgoServer> RunCommand(HttpClient httpClient, string command)
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
    /// This method starts a nade practice using the sourcemod plugin csgo-practice-mode.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public override async Task<AbstractCsgoServer> StartNadePractice(HttpClient httpClient, string cfg = "sm_prac")
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        if(cfg == "sm_prac")
            IsPracmodePratice = true;

        var values = new Dictionary<string, string>
            {
                { "line", cfg },
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

    /// <summary>
    /// This method stops the ongoing nade practice.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public override async Task<AbstractCsgoServer> StopNadePractice(HttpClient httpClient)
    {
        string uri = httpClient.BaseAddress + $"/api/0.1/game-servers/{Id}/console";

        if (!IsPracmodePratice)
            throw new CsgoServerException("Can only stop nade-practice if it is a practice-mode practice", this);

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

    /// <summary>
    /// This method starts a quick match using the yousee esportleague cfg.
    /// 
    /// The parameter withOvertime specifies whether the match should be with overtime or not.
    /// E.g. true = overtime.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="withOvertime"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public override async Task<AbstractCsgoServer> StartQuickmatch(HttpClient httpClient, bool withOvertime = false, string cfg = "esportliga_start")
    {
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

    /// <summary>
    /// This method start the cs:go server.
    /// 
    /// You can also use the method to restart the server if it is allready running.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public override async Task<AbstractCsgoServer> StartServer(HttpClient httpClient)
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

    /// <summary>
    /// This method stops the server.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns>AbstractCsgoServer</returns>
    /// <exception cref="CsgoServerException"></exception>
    public override async Task<AbstractCsgoServer> StopServer(HttpClient httpClient)
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
