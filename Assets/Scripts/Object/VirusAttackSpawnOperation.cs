using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class VirusAttackSpawnOperation : MonoBehaviour
{
    //public bool useTarget;
    //private float timeToFire = 0f;
    //private GameObject effectToSpawn;
    //public GameObject firePoint;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButton(0) && Time.time >= timeToFire)
    //    {
    //        timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
    //        SpawnVFX();
    //    }
    //}

    //public void SpawnVFX()
    //{
    //    GameObject vfx;


    //    if (firePoint != null)
    //    {
    //        vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
    //            if (target != null)
    //            {
    //                vfx.GetComponent<ProjectileMoveScript>().SetTarget(target, rotateToMouse);
    //                rotateToMouse.RotateToMouse(vfx, target.transform.position);
    //            }
    //            else
    //            {
    //                Destroy(vfx);
    //                Debug.Log("No target assigned.");
    //            }
    //    }
    //    else
    //        vfx = Instantiate(effectToSpawn);
    //}
}
