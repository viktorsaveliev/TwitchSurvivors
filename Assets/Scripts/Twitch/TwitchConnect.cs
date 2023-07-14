using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class TwitchConnect
{
    private readonly string _username = "justinfan1923";
    private readonly string _password = "pass";
    private readonly string _channelName = "evelone2004"; // evelone192

    public TcpClient TwitchClient { get; private set; }
    public StreamReader Reader { get; private set; }
    public StreamWriter Writer { get; private set; }

    public void ConnectToTwitch()
    {
        TwitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        Reader = new StreamReader(TwitchClient.GetStream());
        Writer = new StreamWriter(TwitchClient.GetStream());

        Writer.WriteLine("PASS " + _password);
        Writer.WriteLine("NICK " + _username);
        Writer.WriteLine("USER " + _username + " 8 * :" + _username);
        Writer.WriteLine("JOIN #" + _channelName);
        Writer.Flush();

        if (TwitchClient.Connected)
        {
            Debug.Log("Connected to Twitch");
        }
    }
}
