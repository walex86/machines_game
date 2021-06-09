using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class EditorLevelController : BaseSceneController
{
    private EditorWindow _editorWindow;

    public override async void LoadLevel(Level level, List<AvailableElement> availableElements)
    {
        base.LoadLevel(level, availableElements);

        foreach (var element in CurrentLevel.Elements)
        {
            var elementObj = _elementsDataBase.Elements.
                FirstOrDefault(x => x.ElementDisplayName.Equals(element.ElementName));

            var newElement = CreateElement(elementObj, element.HelpElement);
            newElement.transform.position = element.Position;
        }
        
        _editorWindow = await CoreApplication.ViewManager.Show<EditorWindow>(Window.EditorWindow);
        _editorWindow.Play += OnPlayClick;
        _editorWindow.Save += () =>
        {
            CurrentLevel.Elements = _onSceneElements.Select(x => new Element()
            {
                ElementName = x.name,
                Position = x.transform.position,
                HelpElement =  x.TutorialElement
            }).ToList();
            
            CoreApplication.DataManager.LevelsData.CheckNeedToAdd(CurrentLevel);
            CoreApplication.DataManager.LevelsData.Save();
        };
        
        _editorWindow.OnClickElement += (data, isTutorial) =>
        {
            if (_play) return;
            var newElement = CreateElement(data, isTutorial);
            newElement.StartDrag();
        };
    }

    private LevelElement CreateElement(ElementData elementData, bool isTutorial)
    {
        return CreateElement(elementData, levelElement =>
        {
            levelElement.StopDrag();
            if (_editorWindow.MouseUnderRemoveZone)
            {
                _onSceneElements.Remove(levelElement);
                GameObject.Destroy(levelElement.gameObject);
            }
        }, levelElement =>
        {
            if (_play) return;
            levelElement.StartDrag();
        }, isTutorial);
    }
}