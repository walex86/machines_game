using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreApplication
{
    public event Action UpdateAction;
    
    public ViewManager ViewManager { get; private set; }
    public SceneManager SceneManager { get; private set; }
    public DataManager DataManager { get; private set; }

    public void Init(Canvas mainCanvas)
    {
        BaseController.Init(this);
        BaseWindow.BaseInit(this);
        ViewManager = new ViewManager(mainCanvas);
        SceneManager = new SceneManager();
        DataManager = new DataManager();
        
        StartGame();
    }

    public void StartGame()
    {
       ViewManager.Show(Window.Title); 
    }

    public void Update()
    {
       UpdateAction?.Invoke(); 
    }
}
