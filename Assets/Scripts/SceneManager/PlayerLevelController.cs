using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.SceneManager
{
    public class PlayerLevelController : BaseSceneController
    {
        private PlayWindow _playWindow;
        private bool _levelCompleted;
        private bool _anyNewElementCreated;
        private Coroutine _tutorialCoroutine;

        public override async void LoadLevel(Level level, List<AvailableElement> availableElements)
        {
            base.LoadLevel(level, availableElements);
            
            _playWindow = await CoreApplication.ViewManager.Show<PlayWindow, PlayWindowParams>(Window.PlayerWindow, new PlayWindowParams()
            {
                AvailableElements = availableElements
            });

            _playWindow.Play += OnPlayClick;

            foreach (var element in CurrentLevel.Elements.Where(x => !x.HelpElement))
            {
                SetupElement(element);
            }
            
            
            _playWindow.OnClickElement += data =>
            {
                if (_play) return;
                _anyNewElementCreated = true;
                var newElement = CreateElement(data);
                newElement.StartDrag();
            };

            _tutorialCoroutine = StartCoroutine(CreateTutorialElements());
        }

        private void SetupElement(Element element)
        {
            var elementObj = _elementsDataBase.Elements.
                FirstOrDefault(x => x.ElementDisplayName.Equals(element.ElementName));

            var newElement = CreateElement(elementObj, null, null, element.HelpElement, Complete);
            newElement.transform.position = element.Position;
        }

        private IEnumerator CreateTutorialElements()
        {
            yield return new WaitForSeconds(5);

            if (_anyNewElementCreated) yield break;
            
            foreach (var element in CurrentLevel.Elements.Where(x => x.HelpElement))
            {
                SetupElement(element);
            }
        }

        private void StopTutorial()
        {
            if (_tutorialCoroutine != null)
            {
                StopCoroutine(_tutorialCoroutine);
                _tutorialCoroutine = null;
            }

            if (_onSceneElements.Count(x => x.TutorialElement) < 0) return;

            var tutorialElements = _onSceneElements.Where(x => x.TutorialElement).ToList();

            _onSceneElements.RemoveAll(x => x.TutorialElement);

            foreach (var tutorialElement in tutorialElements)
            {
                Destroy(tutorialElement.gameObject);
            }
        }
        
        private LevelElement CreateElement(ElementData elementData)
        {
            StopTutorial();
            return CreateElement(elementData, levelElement =>
            {
                levelElement.StopDrag();
                if (_playWindow.MouseUnderRemoveZone)
                {
                    _onSceneElements.Remove(levelElement);
                    _playWindow.IncrementElement(levelElement.name);
                    GameObject.Destroy(levelElement.gameObject);
                }
            }, levelElement =>
            {
                if (_play) return;
                levelElement.StartDrag();
            }, false, Complete);
        }

        protected override void Pause()
        {
            _levelCompleted = false;
            base.Pause();
        }

        protected override void OnPlayClick()
        {
            if (_levelCompleted) return;
            base.OnPlayClick();
        }


        private void Complete()
        {
            if (_levelCompleted || !_play) return;
            _levelCompleted = true;
            CoreApplication.DataManager.PlayerData.PlayerState
                .PlayerChapterStates[CoreApplication.DataManager.PlayerData.CurrentChapter].CompletedLevels++;
            CoreApplication.DataManager.PlayerData.Save();
            
            
            CoreApplication.ViewManager.Show(Window.WinWindow);
            CoreApplication.ViewManager.Hide(Window.PlayerWindow);
            CoreApplication.SceneManager.HideAll();
        }
    }
}