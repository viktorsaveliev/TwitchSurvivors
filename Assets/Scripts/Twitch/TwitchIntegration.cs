using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TwitchIntegration : MonoBehaviour
{
    public IReadOnlyList<string> ChatNicknames => _chatNicknames;
    public string RandomNicknameForReinsurance { get; private set; }
    
    private readonly List<string> _chatNicknames = new();

    private TwitchConnect _twitchConnect;
    private bool _isRunning = true;

    private void Awake()
    {
        if (!PlayerData.Settings.TwitchIntegration)
        {
            _isRunning = false;
            gameObject.SetActive(false);
        }
        else
        {
            Connect();
        }
    }

    public async void Connect()
    {
        _twitchConnect = new();
        _twitchConnect.ConnectToTwitch();

        StartCoroutine(ClearTimer());

        await ProcessChatAsync();
    }

    private IEnumerator ClearTimer()
    {
        WaitForSeconds waitForSeconds = new(62);
        while (_isRunning)
        {
            yield return waitForSeconds;

            if (_twitchConnect.TwitchClient == null || !_twitchConnect.TwitchClient.Connected)
            {
                _twitchConnect.ConnectToTwitch();
            }
            else if (_twitchConnect.TwitchClient.Connected)
            {
                print("Cleared " + _chatNicknames.Count + " rows");

                if (_chatNicknames.Count > 0)
                {
                    RandomNicknameForReinsurance = _chatNicknames[Random.Range(0, _chatNicknames.Count)];
                }

                _chatNicknames.Clear();
            }
        }
    }

    private async Task ProcessChatAsync()
    {
        while (_isRunning)
        {
            await Task.Delay(20);

            if (_twitchConnect.TwitchClient.Available > 0)
            {
                string message = _twitchConnect.Reader.ReadLine();
                if (message.Contains("PRIVMSG"))
                {
                    int splitPoint = message.IndexOf("!", 1);
                    string chatName = message[1..splitPoint];

                    await AddNewNicknameToListAsync(chatName);
                }
            }
        }
    }

    private async Task AddNewNicknameToListAsync(string user)
    {
        if (user == string.Empty) return;

        await Task.Run(() =>
        {
            lock (_chatNicknames)
            {
                _chatNicknames.Add(user);
            }
        });
    }
}
