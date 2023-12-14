using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateVaseOperation : MonoBehaviour
{
    [Header("데이터")]
    public WaterChargeAmountData data;
    private float waterCharge;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        waterCharge = data.amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            Debug.Log("탄약 충전량 : " + waterCharge);
            SoundManager.Instance.PlaySFX("GetItem");
            transform.parent.gameObject.SetActive(false);
        }
    }
}
