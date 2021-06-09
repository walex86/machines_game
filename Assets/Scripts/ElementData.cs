using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element data")]
public class ElementData : ScriptableObject
{
    public Sprite ElementSprite;
    public GameObject ElementObject;
    public string ElementDisplayName;
}
