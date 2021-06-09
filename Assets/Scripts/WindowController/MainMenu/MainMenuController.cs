using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseWindow
{
    [SerializeField] private Button _campaginButton;
    [SerializeField] private Button _editorButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private EditorView _editorView;
    [SerializeField] private CampaignView _campaignView;
    [SerializeField] private Button _achievmentButton;

    private List<MainMenuView> _views = new List<MainMenuView>();
    
    private void Start()
    {
        void SetupView(MainMenuView view, Button button)
        {
            _views.Add(view);
            view.ViewManager = CoreApplication.ViewManager;
            view.SceneManager = CoreApplication.SceneManager;
            view.DataManager = CoreApplication.DataManager;
            button.onClick.AddListener(() =>
            {
                _views.ForEach(x => x.Disable());
                view.Active();
            });
        }
        
        SetupView(_editorView, _editorButton);
        SetupView(_campaignView, _campaginButton);
        _editorView.Active();
        
        _achievmentButton.onClick.AddListener((() =>
        {
            CoreApplication.ViewManager.Show(Window.AchievmentWindow);
            CoreApplication.ViewManager.Hide(Window.MainMenu);
        }));
        
        _exitButton.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        });
    }
}

public class MainMenuView : MonoBehaviour
{
    public ViewManager ViewManager;
    public SceneManager SceneManager;
    public DataManager DataManager;

    public virtual void Active()
    {
        
    }

    public virtual void Disable()
    {
        
    }
    
    public void HideParent()
    {
       ViewManager.Hide(Window.MainMenu); 
    }
}
