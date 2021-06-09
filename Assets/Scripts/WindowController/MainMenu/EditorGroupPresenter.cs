using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorGroupPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _button;

    public void Init(string text, Action onClick)
    {
        _title.text = text;
        _button.onClick.AddListener(() => onClick?.Invoke());
    }
}
