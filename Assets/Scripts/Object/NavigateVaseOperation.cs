using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateVaseOperation : MonoBehaviour
{
    [Header("데이터")]
    public WaterChargeAmountData data;
    private int waterCharge;

    public delegate void BottleHandle();
    public event BottleHandle OnCharge;

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

    public int GetChargeAmount()
    {
        return waterCharge;
    }
}
