using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class CampaignView : MainMenuView
{
    [SerializeField] private ChaptersDataBase _chaptersDataBase;
    [SerializeField] private PlayerGroupPresenter _playerGroupPresenter;
    private List<PlayerGroupPresenter> _presenters = new List<PlayerGroupPresenter>();

    private void Start()
    {
        if (_chaptersDataBase.Chapters.Count > DataManager.PlayerData.PlayerState.PlayerChapterStates.Count)
        {
            DataManager.PlayerData.PlayerState.PlayerChapterStates =
                _chaptersDataBase.Chapters.Select(x => new PlayerChapterState()).ToList();
            DataManager.PlayerData.Save();
        }
    }

    public override void Active()
    {
        int index = 0;
        foreach (var chapter in _chaptersDataBase.Chapters)
        {
            var levelPresenter = GameObject.Instantiate(_playerGroupPresenter, transform);
            var i = index;
            var currentProgress = Mathf.Clamp(DataManager.PlayerData.PlayerState.PlayerChapterStates[i].CompletedLevels,
                0, chapter.Levels.Count);
            levelPresenter.Init($"Глава {index + 1}", chapter.Image, $"{currentProgress} / {chapter.Levels.Count}", () =>
            {
                HideParent();
                var currentLevelIndex = Mathf.Clamp(DataManager.PlayerData.PlayerState.PlayerChapterStates[i].CompletedLevels, 0,
                                   chapter.Levels.Count - 1);
                var currentLevel = chapter.Levels[currentLevelIndex];
                DataManager.PlayerData.CurrentChapter = i;
                SceneManager.Show(Scene.PlayMode, currentLevel.Level, currentLevel.AvailableElements);
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
}
