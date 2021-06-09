using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class BaseSceneController : MonoBehaviour
{
    public static CoreApplication CoreApplication;
    protected Level CurrentLevel;
    protected bool _play;
    
    protected List<LevelElement> _onSceneElements = new List<LevelElement>();
    [SerializeField] protected ElementsDataBase _elementsDataBase;
    
    public virtual void LoadLevel(Level level, List<AvailableElement> availableElements)
    {
        CurrentLevel = level;
    }
    
    
    protected LevelElement CreateElement(ElementData elementData, Action<LevelElement> onMouseUp, Action<LevelElement> onMouseDown, 
        bool tutorialElement = false, Action complete = null)
    {
        var newElement = GameObject.Instantiate(elementData.ElementObject, transform);

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        newElement.transform.position = pos;

        newElement.name = elementData.ElementDisplayName;
        var levelElement = newElement.GetComponent<LevelElement>();
        levelElement.Init(() => onMouseDown?.Invoke(levelElement),
            () => onMouseUp?.Invoke(levelElement), tutorialElement, complete);
        levelElement.Freeze(true);
        _onSceneElements.Add(levelElement);
        return levelElement;
    }
    
    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            foreach (var onSceneElement in _onSceneElements)
            {
                onSceneElement.StopDrag();
            }
        }
    }
    
    
        protected virtual void OnPlayClick()
        {
            if (_play)
            {
                Pause();
            }
            else
            {
                Play();
            }

            _play = !_play;
        }

        private Dictionary<Transform, (Vector3, Quaternion)> _elementsLastPos = new Dictionary<Transform, (Vector3, Quaternion)>();
        private void Play()
        {
            _elementsLastPos = _onSceneElements.ToDictionary(x => x.transform, x => (x.transform.position, x.transform.rotation));
            _onSceneElements.ForEach(x => x.Freeze(false));
            _onSceneElements.Where(x => x.TutorialElement).ToList().ForEach(x => x.gameObject.SetActive(false));
        }

        protected virtual void Pause()
        {
            _onSceneElements.ForEach(x => x.Freeze(true));
            foreach (var elementsLastPos in _elementsLastPos)
            {
                elementsLastPos.Key.position = elementsLastPos.Value.Item1;
                elementsLastPos.Key.rotation = elementsLastPos.Value.Item2;
            }
            _onSceneElements.Where(x => x.TutorialElement).ToList().ForEach(x => x.gameObject.SetActive(true));
        }
}
