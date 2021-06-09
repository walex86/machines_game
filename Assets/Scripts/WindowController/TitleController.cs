using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : BaseWindow
{
    [SerializeField] private Button _playButton;

    private void Start()
    {
       _playButton.onClick.AddListener(() =>
       {
            CoreApplication.ViewManager.Hide(Window.Title);
            CoreApplication.ViewManager.Show(Window.MainMenu);
       }); 
    }
}
