using System;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas;

    private CoreApplication _application;

    void Start()
    {
        _application = new CoreApplication();
        _application.Init(_mainCanvas);
    }

    private void Update()
    {
       _application.Update(); 
    }
}
