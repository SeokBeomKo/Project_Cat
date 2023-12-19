using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingTooltip : MonoBehaviour
{
    [SerializeField]
    List<string> tooltip;

    [SerializeField]
    TextMeshProUGUI tooltipText;

    void Start()
    {
        ShowToolTip(Random.Range(0,tooltip.Count));
    }

    public void ShowToolTip(int index)
    {
        tooltipText.text = "Tip. " + tooltip[index];
    }
}
