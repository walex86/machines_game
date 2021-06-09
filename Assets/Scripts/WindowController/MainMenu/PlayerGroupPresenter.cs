using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGroupPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private Button _button;
    [SerializeField] private Image _mainImg;

    public void Init(string text, Sprite img, string progress, Action onClick)
    {
        _title.text = text;
        _mainImg.sprite = img;
        _progress.text = progress;
        _button.onClick.AddListener(() => onClick?.Invoke());
    }
}
