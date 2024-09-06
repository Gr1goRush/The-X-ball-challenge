using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAreaBackground : MonoBehaviour
{
    [SerializeField] private Image _image;

    void Start()
    {
        _image.sprite = BackgroundsManager.Instance.LoadSelectedBackground(GameModeManager.Instance.GameType);
    }
}
