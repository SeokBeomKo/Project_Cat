using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchIndividualOperation : MonoBehaviour, IInteractable
{
    public SwitchesOperation switchesOperation;

    public int IndividualIndex;
    public GameObject Switchbone;


    private bool collisionPossible = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            Interaction();
        }
    }

    private IEnumerator DisableCollisionForSeconds(float seconds)
    {
        collisionPossible = false;
        yield return new WaitForSeconds(seconds);
        collisionPossible = true;
    }

    public void Interaction(Vector3 interPos = new Vector3(), float damage = 0)
    {
        if (!collisionPossible) return;

        SoundManager.Instance.PlaySFX("Switch");
        switchesOperation.SetSwitch(IndividualIndex, true);
        StartCoroutine(DisableCollisionForSeconds(1.0f));
    }
}