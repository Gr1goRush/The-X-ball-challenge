using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public event Action OnDisabled;

    [HideInInspector] public bool allowBackToMenu = false;
    [SerializeField] private CustomToggle musicToggle, vibrationToggle;
    [SerializeField] private string privacyPolicyURL;

    private bool allowBack = false;

    private void Start()
    {
        musicToggle.IsOn = !AudioManager.Instance.SoundsMuted;
        musicToggle.onSwitched.AddListener(OnMusicSwitched);

        vibrationToggle.IsOn = VibrationManager.Instance.VibrationEnabled;
        vibrationToggle.onSwitched.AddListener(OnVibrationSwitched);
    }

    private void OnEnable()
    {
        allowBack = false;

        StartCoroutine(WaitFrameToEnableBack());
    }

    IEnumerator WaitFrameToEnableBack()
    {
        yield return new WaitForEndOfFrame();

        allowBack = true;
    }

    private void OnMusicSwitched(bool v)
    {
        AudioManager.Instance.SetSoundsMuted(!v);
    }

    private void OnVibrationSwitched(bool v)
    {
        VibrationManager.Instance.SetVibrationEnabled(v);
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyURL);
    }

    public void Back()
    {
        if (!allowBack)
        {
            return;
        }

        Disable();
    }

    private void Disable() 
    {
        gameObject.SetActive(false);

        OnDisabled?.Invoke();
    }

    public void Close()
    {
        if (allowBackToMenu)
        {
            ScenesLoader.LoadMenu();
            gameObject.SetActive(false);
        }
        else
        {
            Disable();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        allowBack = false;
    }
}
