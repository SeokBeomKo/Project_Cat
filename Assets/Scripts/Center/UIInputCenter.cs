using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputCenter : MonoBehaviour
{
    [Header("UI ÀÎÇ² ÇÚµé")]
    public UIInputHandler uiHandle;

    [Header("ÀÏ½ÃÁ¤Áö ÆË¾÷")]
    public PausePopUp pause;

    [Header("¼¼Ã´µµ ÆË¾÷")]
    public CleanlinessPopUpObserver clean;

    private void Start()
    {
        uiHandle.OnPausePopUp += ActivePausePopUp;
        if (clean != null)
        {
            uiHandle.OnCleanlinessPopUpTrue += ActiveCleanPopUp;
            uiHandle.OnCleanlinessPopUpFalse += DeactiveCleanPopUp;
        }
    }

    public void ActivePausePopUp()
    {
        pause.UpdatePause();
    }

    public void ActiveCleanPopUp()
    {
        clean.ActivateCleanliness();
    }

    public void DeactiveCleanPopUp()
    {
        clean.DeactivateCleanliness();
    }
}
