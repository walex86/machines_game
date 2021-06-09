using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PlayWindow : BaseWindow<PlayWindowParams>
{
    [SerializeField] private Button _goHome;
    [SerializeField] private Button _playButton;
    [SerializeField] private PlayerContentPresenter _playerContentPresenterPrefab;
    [SerializeField] private Transform _presenterParent;
    [SerializeField] private EditorScroll _editorScroll;
    
    private List<AvailableElement> _availableElements;
    public event Action<ElementData> OnClickElement;

    private Dictionary<string, PlayerContentPresenter> _presenters = new Dictionary<string, PlayerContentPresenter>();

    public bool _playMode;
    public event Action Play;
    public bool MouseUnderRemoveZone => _editorScroll.MouseUnderDeleteZone;

    private void Start()
    {
        _goHome.onClick.AddListener(() =>
        {
            CoreApplication.ViewManager.Show(Window.MainMenu);
            CoreApplication.ViewManager.Hide(Window.PlayerWindow);
            CoreApplication.SceneManager.HideAll();
        });
        
        _playButton.onClick.AddListener(() =>
        {
            Play?.Invoke();
            _playMode = !_playMode;
            _playButton.GetComponent<SwapSprite>().SetSprite(_playMode ? 1 : 0);
        });
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            _canvasGroup.alpha = 1;
        }
    }

    public override void HandleArguments(PlayWindowParams param)
    {
        _availableElements = param.AvailableElements;
        foreach (var element in _availableElements)
        {
            var presenter = GameObject.Instantiate(_playerContentPresenterPrefab, _presenterParent);
            var elementData = element;
            presenter.SetCount(element.Count);
            presenter.Init(element.Element, () =>
            {
                if (presenter.CountLeft > 0 && !_playMode)
                {
                    presenter.DecrementCount();
                    OnClickElement?.Invoke(elementData.Element);
                    _canvasGroup.alpha = 0;
                }
            });
            _presenters[elementData.Element.ElementDisplayName] = presenter;
        }
    }

    public void IncrementElement(string elementName)
    {
        _presenters[elementName].IncrementCount();
    }
}

public class PlayWindowParams
{
    public List<AvailableElement> AvailableElements;
}
