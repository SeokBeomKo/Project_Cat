using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputCenter : MonoBehaviour
{
    [Header("UI ÀÎÇ² ÇÚµé")]
    public UIInputHandler uiHandle;

    [Header("¼¼ÆÃÃ¢")]
    public Setting setting;

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
        setting.UpdatePause();
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
