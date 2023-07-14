using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchIntegration : MonoBehaviour
{
    public IReadOnlyList<string> ChatNicknames => _chatNicknames;
    private readonly List<string> _chatNicknames = new();

    private TwitchConnect _twitchConnect;
    private Coroutine _timer;

    private readonly float _delayBetweenAddNewNicknames = 0.1f;
    private float _currentDelay;

    private void Start()
    {
        _twitchConnect = new();
        _twitchConnect.ConnectToTwitch();

        _timer ??= StartCoroutine(ClearTimer());
    }

    private void FixedUpdate()
    {
        if (_currentDelay > Time.time) return;
        ReadChat();
    }

    private IEnumerator ClearTimer()
    {
        WaitForSeconds waitForSeconds = new(17f);
        while (_twitchConnect.TwitchClient.Connected)
        {
            yield return waitForSeconds;
            print("Cleared " + _chatNicknames.Count + " rows");
            _chatNicknames.Clear();
        }
    }

    private void ReadChat()
    {
        if(_twitchConnect.TwitchClient.Available > 0)
        {
            string message = _twitchConnect.Reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                int splitPoint = message.IndexOf("!", 1);
                string chatName = message[1..splitPoint];

                AddNewNicknameToList(chatName);
            }
        }
    }

    private void AddNewNicknameToList(string user)
    {
        if (user == string.Empty) return;

        _chatNicknames.Add(user);
        _currentDelay = Time.time + _delayBetweenAddNewNicknames;
    }
}
