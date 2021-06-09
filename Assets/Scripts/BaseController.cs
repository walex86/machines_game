using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class BaseController
{
    protected static CoreApplication CoreApplication;

    public static void Init(CoreApplication application)
    {
        CoreApplication = application;
    }
}
