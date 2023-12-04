/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cleanliness : MonoBehaviour
{
    public CleanlinessProgressBar cleanProgress;
    public CleanlinessPopUp cleanPopUp;

    // ÃÑ ¼¼Ã´µµ
    private float _cleanliness;
    public float cleanliness
    {
        get { return _cleanliness; }
        set
        {
            _cleanliness = value;
            cleanProgress.UpdateProgress(_cleanliness);
        }
    }

    // ÆÄÃ÷º° ¼¼Ã´µµ
    // 1. »óÃ¼
    private float _upperBodyCleanliness;
    public float upperBodyCleanliness
    {
        get { return _upperBodyCleanliness; }
        set 
        {
            _upperBodyCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.UPPERBODY, _upperBodyCleanliness);
        }
    }

    // 2. ÇÏÃ¼
    private float _lowerBodyCleanliness;
    public float lowerBodyCleanliness
    {
        get { return _lowerBodyCleanliness; }
        set
        {
            _lowerBodyCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.LOWERBODY, _lowerBodyCleanliness);
        }
    }

    // 3. µÞ¹ß (¿À)
    private float _rearPawRightCleanliness;
    public float rearPawRightCleanliness
    {
        get { return _rearPawRightCleanliness; }
        set
        {
            _rearPawRightCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.REARPAWRIGHT, _rearPawRightCleanliness);
        }
    }

    // 4. µÞ¹ß (¿Þ)
    private float _rearPawLeftCleanliness;
    public float rearPawLeftCleanliness
    {
        get { return _rearPawLeftCleanliness; }
        set
        {
            _rearPawLeftCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.REARPAWLEFT, _rearPawLeftCleanliness);
        }
    }

    // 5. ¾Õ¹ß (¿À)
    private float _forePawRightCleanliness;
    public float forePawRightCleanliness
    {
        get { return _forePawRightCleanliness; }
        set
        {
            _forePawRightCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.FOREPAWRIGHT, _forePawRightCleanliness);
        }
    }

    // 6. ¾Õ¹ß (¿Þ)
    private float _forePawLeftCleanliness;
    public float forePawLeftCleanliness
    {
        get { return _forePawLeftCleanliness; }
        set
        {
            _forePawLeftCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.FOREPAWLEFT, _forePawLeftCleanliness);
        }
    }

    // 7. µî
    private float _backCleanliness;
    public float backCleanliness
    {
        get { return _backCleanliness; }
        set
        {
            _backCleanliness = value;
            cleanPopUp.CleanCat(CleanEnums.BACK, _backCleanliness);
        }
    }
    void Start()
    {
        upperBodyCleanliness = 0;
        lowerBodyCleanliness = 0;
        rearPawRightCleanliness = 0;
        rearPawLeftCleanliness = 0;
        forePawRightCleanliness = 0;
        forePawLeftCleanliness = 0;
        backCleanliness = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // »óÃ¼
        {
            if(upperBodyCleanliness < 100)
               upperBodyCleanliness += 1;
        }
        else if(Input.GetKeyDown(KeyCode.X)) // ÇÏÃ¼
        {
            if (lowerBodyCleanliness < 100)
                lowerBodyCleanliness += 1;
        }
        else if(Input.GetKeyDown(KeyCode.C)) // ¾Õ¹ß ¿À
        {
            if (forePawRightCleanliness < 100)
                forePawRightCleanliness += 1;
        }
        else if(Input.GetKeyDown(KeyCode.V)) // ¾Õ¹ß ¿Þ
        {
            if (forePawLeftCleanliness < 100)
                forePawLeftCleanliness += 1;
        }
        else if(Input.GetKeyDown(KeyCode.B)) // µÞ¹ß ¿À
        {
            if (rearPawRightCleanliness < 100)
                rearPawRightCleanliness += 1;
        }
        else if(Input.GetKeyDown(KeyCode.N)) // µÞ¹ß ¿Þ
        {
            if (rearPawLeftCleanliness < 100)
                rearPawLeftCleanliness += 1;
        }
        else if(Input.GetKeyDown(KeyCode.M)) // µî
        {
            if (backCleanliness < 100)
            {
                backCleanliness += 1;
            }
        }

        cleanliness = upperBodyCleanliness + lowerBodyCleanliness + forePawRightCleanliness + forePawLeftCleanliness +
            rearPawRightCleanliness + rearPawLeftCleanliness + backCleanliness;
    }
}
*/