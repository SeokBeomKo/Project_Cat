using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetItem : MonoBehaviour
{
    public TextMeshProUGUI item;

    public IEnumerator ShowGetItemText(string itemName)
    {
        item.text = itemName;

        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }

}
