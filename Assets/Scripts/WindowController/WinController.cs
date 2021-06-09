using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class WinController : BaseWindow
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private ChaptersDataBase _chaptersDataBase;

    private void Start()
    {
        _nextLevelButton.onClick.AddListener(PlayNextLevel);
    }

    private void PlayNextLevel()
    {
        var currentPlayerChapter = CoreApplication.DataManager.PlayerData.CurrentChapter;

        if (currentPlayerChapter >= _chaptersDataBase.Chapters.Count)
        {
            CoreApplication.ViewManager.Hide(Window.WinWindow);
            CoreApplication.ViewManager.Show(Window.MainMenu);
            return;
        }
        
        var currentLevelsCompleted = CoreApplication.DataManager.PlayerData.PlayerState
            .PlayerChapterStates[currentPlayerChapter].CompletedLevels;
        var currentChapterLevelsLeft =
            _chaptersDataBase.Chapters[currentPlayerChapter].Levels.Count - currentLevelsCompleted;
        if (currentChapterLevelsLeft <= 0)
        {
            if (++CoreApplication.DataManager.PlayerData.CurrentChapter >= _chaptersDataBase.Chapters.Count)
            {
                CoreApplication.ViewManager.Hide(Window.WinWindow);
                CoreApplication.ViewManager.Show(Window.MainMenu);
                return;
            }

            PlayNextLevel();
            return;
        }

        currentPlayerChapter = CoreApplication.DataManager.PlayerData.CurrentChapter;
        var chapter = _chaptersDataBase.Chapters[currentPlayerChapter];
        var currentLevelIndex = Mathf.Clamp(
            CoreApplication.DataManager.PlayerData.PlayerState.PlayerChapterStates[currentPlayerChapter]
                .CompletedLevels, 0,
            chapter.Levels.Count - 1);
        var currentLevel = chapter.Levels[currentLevelIndex];
        CoreApplication.ViewManager.Hide(Window.WinWindow);
        CoreApplication.SceneManager.Show(Scene.PlayMode, currentLevel.Level, currentLevel.AvailableElements);
    }
}