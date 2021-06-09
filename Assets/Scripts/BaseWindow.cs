using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;
    protected static CoreApplication CoreApplication;
    public static void BaseInit(CoreApplication application)
    { 
        CoreApplication = application;
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual Task Show()
    {
        return Task.CompletedTask;
    }

    public virtual Task Hide()
    {
        return Task.CompletedTask;
    }
}

public abstract class BaseWindow<T> : BaseWindow
{
    public abstract void HandleArguments(T param);
}
