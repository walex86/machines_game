using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorView : MainMenuView
{
    [SerializeField] private EditorGroupPresenter _groupPresenterPrefab;
    private List<EditorGroupPresenter> _presenters = new List<EditorGroupPresenter>();
    
    public override void Active()
    {
        var createNewPresenter = GameObject.Instantiate(_groupPresenterPrefab, transform);
        createNewPresenter.Init("Создать новый уровень", CreateNewLevel);
        _presenters.Add(createNewPresenter);

        int index = 1;
        foreach (var level in DataManager.LevelsData.FileData.Levels)
        {
            var levelPresenter = GameObject.Instantiate(_groupPresenterPrefab, transform);
            levelPresenter.Init($"Уровень {index}", () =>
            {
                HideParent();
                SceneManager.Show(Scene.Editor, level);
            });
            _presenters.Add(levelPresenter);
            index++;
        }
    }

    public override void Disable()
    {
        foreach (var presenter in _presenters)
        {
           GameObject.Destroy(presenter.gameObject); 
        }
        
        _presenters.Clear();
    }

    private void CreateNewLevel()
    {
       HideParent(); 
        SceneManager.Show(Scene.Editor, new Level()
        {
            LevelIndex = DataManager.LevelsData.FileData.Levels.Count,
            Elements = new List<Element>()
        }); 
    }
}
