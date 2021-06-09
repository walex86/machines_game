using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentWindow : BaseWindow
{
    [SerializeField] private Image _mainImg;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private AchievmentPresetner _achievmentPrefab;
    [SerializeField] private ChaptersDataBase _chaptersDataBase;
    [SerializeField] private Transform _presentersParent;
    [SerializeField] private Button _goToMainMenu;

    public override Task Show()
    {
        var userState = CoreApplication.DataManager.PlayerData.PlayerState.PlayerChapterStates;

        for (int i = 0; i < userState.Count; i++)
        {
            bool chapterComplete =
                _chaptersDataBase.Chapters[i].Levels.Count == userState[i].CompletedLevels;

            var newPresenter = GameObject.Instantiate(_achievmentPrefab, _presentersParent);
            
            newPresenter.Init(_chaptersDataBase.Chapters[i].AchievementToComplete, (presenter) =>
            {
                ActiveAchievment(presenter.Achievement, chapterComplete);
            });
            
            newPresenter.Active(chapterComplete);

            if (i == 0)
            {
                ActiveAchievment(newPresenter.Achievement, chapterComplete);
            }
        }
        
        _goToMainMenu.onClick.AddListener(() =>
        {
            CoreApplication.ViewManager.Show(Window.MainMenu);
            CoreApplication.ViewManager.Hide(Window.AchievmentWindow);
        });

        
        return base.Show();
    }

    private void ActiveAchievment(Achievement achievement, bool chapterComplete)
    {
        if (chapterComplete)
        {
            _description.text = achievement.Description;
            _titleText.text = achievement.Name;
            _mainImg.color = Color.white;
            _mainImg.sprite = achievement.Img;
        }
        else
        {
            _titleText.text = "Недоступно";
            _description.text = "";
            _mainImg.color = Color.black;
        }
    }
}
