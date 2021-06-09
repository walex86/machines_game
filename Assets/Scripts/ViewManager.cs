using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ViewManager : BaseController
{
    private Canvas _mainCanvas;
    private Dictionary<Window, BaseWindow> _openWindows = new Dictionary<Window, BaseWindow>();
    private Queue<Window> _windowQueue = new Queue<Window>();
    
    public ViewManager(Canvas mainCanvas)
    {
        _mainCanvas = mainCanvas;
        CoreApplication.UpdateAction += CheckWindows;
    }

    private void CheckWindows()
    {
        if (_openWindows.Count == 0 && _windowQueue.Count != 0)
        {
           Show(_windowQueue.Dequeue()); 
        }
    }

    public void Show(Window window)
    {
        Show<BaseWindow>(window);
    }

    public async Task<TWindow> Show<TWindow>(Window window) where TWindow : BaseWindow
    {
        if (_openWindows.ContainsKey(window))
        {
            return (TWindow) _openWindows[window];
        }

        if (_openWindows.Count > 0)
        {
            _windowQueue.Enqueue(window);
            return null;
        }
        
        var windowPrefab = await Addressables.LoadAssetAsync<GameObject>(window.ToString()).Task;
        var obj = GameObject.Instantiate(windowPrefab, _mainCanvas.transform);
        var windowObj = obj.GetComponent<BaseWindow>();
        _openWindows.Add(window, windowObj);
        await windowObj.Show();
        return windowObj as TWindow;
    }
    
    public async Task<TWindow> Show<TWindow, TParams>(Window window, TParams @params) where TWindow : BaseWindow<TParams>
    {
        var windowObj = await Show<TWindow>(window) as TWindow;
        windowObj.HandleArguments(@params);
        return windowObj;
    }

    public async void Hide(Window window)
    {
        if (!_openWindows.ContainsKey(window))
        {
            return;
        }

        var windowObj = _openWindows[window];
        await windowObj.Hide();
        GameObject.Destroy(windowObj.gameObject);
        _openWindows.Remove(window);
    }
}

public enum Window
{
    Title,
    MainMenu,
    EditorWindow,
    PlayerWindow,
    WinWindow,
    AchievmentWindow
}
