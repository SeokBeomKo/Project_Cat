using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchIndividualOperation : MonoBehaviour
{
    public SwitchesOperation switchesOperation;

    public int IndividualIndex;
    public GameObject Switchbone;


    private bool collisionPossible = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack") && collisionPossible)
        {
            SoundManager.Instance.PlaySFX("Switch");
            switchesOperation.SetSwitch(IndividualIndex, true);
            StartCoroutine(DisableCollisionForSeconds(2.0f));
        }
    }

    private IEnumerator DisableCollisionForSeconds(float seconds)
    {
        collisionPossible = false;
        yield return new WaitForSeconds(seconds);
        collisionPossible = true;
    }

}