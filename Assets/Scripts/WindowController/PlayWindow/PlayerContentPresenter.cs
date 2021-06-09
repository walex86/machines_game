using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerContentPresenter : BaseContentPresenter
{
    [SerializeField] private TMP_Text _countLeft;
    public int CountLeft { get; private set; }

    public void SetCount(int count)
    {
        CountLeft = count;
        _countLeft.text = count.ToString();
    }

    public void IncrementCount()
    {
        SetCount(CountLeft + 1);
    }

    public void DecrementCount()
    {
        SetCount(CountLeft - 1);
    }
}
