using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct SwitchRectTransformParameters
{
    public bool setAnchorMax;
    public Vector2 anchorMax;

    public bool setAnchorMin;
    public Vector2 anchorMin;

    public bool setPivot;
    public Vector2 pivot;

    public bool setAnchoredPosition;
    public Vector2 anchoredPosition;

    public bool setSizeDelta;
    public Vector2 sizeDelta;

    public bool setOffsetMin;
    public Vector2 offsetMin;

    public bool setOffsetMax;
    public Vector2 offsetMax;
}

[System.Serializable]
public struct RectTransformParameters
{
    public Vector2 anchorMax;
    public Vector2 anchorMin;

    public Vector2 pivot;
    public Vector2 anchoredPosition;
    public Vector2 sizeDelta;

    public Vector2 offsetMin;
    public Vector2 offsetMax;
}

[System.Serializable]
public struct GameModeBasedRectTransformParameters
{
    public GameMode[] modes;
    public SwitchRectTransformParameters parameters;
}

[System.Serializable]
public struct PlayersCountBasedRectTransformParameters
{
    public PlayersCount playersCount;
    public SwitchRectTransformParameters parameters;
}

public class GameModeBasedRectTransform : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private RectTransformParameters defaultParameters;

    [SerializeField] private GameModeBasedRectTransformParameters[] gameModeParameters;
    [SerializeField] private PlayersCountBasedRectTransformParameters[] playersCountParameters;

    void Start()
    {
        bool setAnchorMax = false;
        bool setAnchorMin = false;
        bool setPivot = false;
        bool setAnchoredPosition = false;
        bool setSizeDelta = false;
        bool setOffsetMin = false;
        bool setOffsetMax = false;

        void AcceptSwitchParameters(SwitchRectTransformParameters switchParameters)
        {
            if (switchParameters.setAnchorMax)
            {
                rectTransform.anchorMax = switchParameters.anchorMax;
                setAnchorMax = true;
            }
            if (switchParameters.setAnchorMin)
            {
                rectTransform.anchorMin = switchParameters.anchorMin;
                setAnchorMin = true;
            }
            if (switchParameters.setPivot)
            {
                rectTransform.pivot = switchParameters.pivot;
                setPivot = true;
            }
            if (switchParameters.setAnchoredPosition)
            {
                rectTransform.anchoredPosition = switchParameters.anchoredPosition;
                setAnchoredPosition = true;
            }
            if (switchParameters.setSizeDelta)
            {
                rectTransform.sizeDelta = switchParameters.sizeDelta;
                setSizeDelta = true;
            }
            if (switchParameters.setOffsetMax)
            {
                rectTransform.offsetMax = switchParameters.offsetMax;
                setOffsetMax = true;
            }
            if (switchParameters.setOffsetMin)
            {
                rectTransform.offsetMin = switchParameters.offsetMin;
                setOffsetMin = true;
            }
        }

        PlayersCount playersCount = GameModeManager.Instance.PlayersCount;
        if(playersCountParameters != null)
        {
            foreach (PlayersCountBasedRectTransformParameters _parameters in playersCountParameters)
            {
                if(_parameters.playersCount == playersCount)
                {
                    AcceptSwitchParameters(_parameters.parameters);
                }
            }
        }

        GameMode gameMode = GameModeManager.Instance.GameMode;
        if (gameModeParameters != null)
        {
            foreach (GameModeBasedRectTransformParameters _parameters in gameModeParameters)
            {
                if (_parameters.modes.Contains(gameMode))
                {
                    AcceptSwitchParameters(_parameters.parameters);
                }
            }
        }

        if (!setAnchorMax)
        {
            rectTransform.anchorMax = defaultParameters.anchorMax;
        }
        if (!setAnchorMin)
        {
            rectTransform.anchorMin = defaultParameters.anchorMin;
        }
        if (!setPivot)
        {
            rectTransform.pivot = defaultParameters.pivot;
        }
        if (!setAnchoredPosition)
        {
            rectTransform.anchoredPosition = defaultParameters.anchoredPosition;
        }
        if (!setSizeDelta)
        {
            rectTransform.sizeDelta = defaultParameters.sizeDelta;
        }
        if (!setOffsetMax)
        {
            rectTransform.offsetMax = defaultParameters.offsetMax;
        }
        if (!setOffsetMin)
        {
            rectTransform.offsetMin = defaultParameters.offsetMin;
        }
    }

#if UNITY_EDITOR
    [SerializeField] private SwitchRectTransformParameters tempSwitchParameters;

    [ContextMenu("Set Parameters To Temp Switch...")]
    private void SetToTempSwitch()
    {
        tempSwitchParameters.anchorMin = rectTransform.anchorMin;
        tempSwitchParameters.anchorMax = rectTransform.anchorMax;
        tempSwitchParameters.pivot = rectTransform.pivot;
        tempSwitchParameters.anchoredPosition = rectTransform.anchoredPosition;
        tempSwitchParameters.sizeDelta = rectTransform.sizeDelta;
        tempSwitchParameters.offsetMin = rectTransform.offsetMin;
        tempSwitchParameters.offsetMax = rectTransform.offsetMax;
    }

    [ContextMenu("Set Parameters To Default")]
    private void SetToDefault()
    {
        defaultParameters.anchorMin = rectTransform.anchorMin;
        defaultParameters.anchorMax = rectTransform.anchorMax;
        defaultParameters.pivot = rectTransform.pivot;
        defaultParameters.anchoredPosition = rectTransform.anchoredPosition;
        defaultParameters.sizeDelta = rectTransform.sizeDelta;
        defaultParameters.offsetMin = rectTransform.offsetMin;
        defaultParameters.offsetMax = rectTransform.offsetMax;
    }

#endif

    private void Reset()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
