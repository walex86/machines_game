using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EditorWindow : BaseWindow
{
    [SerializeField] private ElementsDataBase _elementsDataBase;
    [SerializeField] private EditorContentPresenter _editorContentPresenterPrefab;
    [SerializeField] private Transform _presenterParent;
    [SerializeField] private Button _goHome;
    [SerializeField] private Button _saveButton;
    [SerializeField] private EditorScroll _editorScroll;
    [SerializeField] private Button _playButton;
    
    public event Action<ElementData, bool> OnClickElement;
    public event Action Save;
    public bool MouseUnderRemoveZone => _editorScroll.MouseUnderDeleteZone;
    public bool _playMode;
    public event Action Play;

    private void Start()
    {
        _goHome.onClick.AddListener(() =>
        {
            CoreApplication.ViewManager.Show(Window.MainMenu);
            CoreApplication.ViewManager.Hide(Window.EditorWindow);
            CoreApplication.SceneManager.HideAll();
        });
        
        _saveButton.onClick.AddListener(() =>
        {
            Save?.Invoke();
        });
        
        _playButton.onClick.AddListener(() =>
        {
            Play?.Invoke();
            _playMode = !_playMode;
            _playButton.GetComponent<SwapSprite>().SetSprite(_playMode ? 1 : 0);
        });
    }

    public override Task Show()
    {
        foreach (var element in _elementsDataBase.Elements)
        {
            var presenter = GameObject.Instantiate(_editorContentPresenterPrefab, _presenterParent);
            var elementData = element;
            presenter.Init(element, () =>
            {
                OnClickElement?.Invoke(elementData, false);
                _canvasGroup.alpha = 0;
            }, () =>
            {
                OnClickElement?.Invoke(elementData, true);
                _canvasGroup.alpha = 0;
            });
        }
        return base.Show();
    }
    
    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            _canvasGroup.alpha = 1;
        }
    }
}
