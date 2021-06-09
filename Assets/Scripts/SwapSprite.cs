using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;

    public void SetSprite(int index)
    {
        GetComponent<Image>().sprite = _sprites[index];
    }
}
