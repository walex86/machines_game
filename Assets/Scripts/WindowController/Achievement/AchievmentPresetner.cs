using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentPresetner : MonoBehaviour
{
    [SerializeField] private Image _image;
    private bool _active;
    public Achievement Achievement { get; private set; }
    
    public void Init(Achievement achievement, Action<AchievmentPresetner> onClickCallback)
    {
        Achievement = achievement;
        _image.sprite = achievement.Img;

        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (_active) onClickCallback?.Invoke(this);
        });
    }

    public void Active(bool active)
    {
        _active = active;
        _image.color = active ? Color.white : new Color(0.15f, 0.15f, 0.15f, 1);
    }
}
