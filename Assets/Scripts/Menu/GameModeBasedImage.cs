using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ImageParameters
{
    public Sprite sprite;
}

[System.Serializable]
public struct SwitchImageParameters
{
    public GameType gameType;
    public ImageParameters parameters;
}

public class GameModeBasedImage : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private SwitchImageParameters[] gameTypeParameters;

    void OnEnable()
    {
        GameType gameType = GameModeManager.Instance.GameType;

        foreach (SwitchImageParameters _parameters in gameTypeParameters)
        {
            if(_parameters.gameType == gameType)
            {
                _image.sprite = _parameters.parameters.sprite;
                return;
            }
        }
    }

    private void Reset()
    {
        _image = GetComponent<Image>(); 
    }
}
