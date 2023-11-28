using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapProjectile : MonoBehaviour
{
    public int gauge;
    public LineRenderer line;

    public GameObject beamStart;
    public GameObject beamEnd;
    public GameObject beam;

    private float textureScrollOffset;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Start() 
    {
        Invoke("Explosion",0.1f);
    }

    public void Explosion()
    {
        beam.SetActive(false);

        // StartCoroutine(CheckExit());
    }

    private void Update() 
    {
        ScrollBeam();
    }

    private IEnumerator CheckExit()
    {
        ParticleSystem ps = beamStart.GetComponent<ParticleSystem>();
        ParticleSystem ps2 = beamEnd.GetComponent<ParticleSystem>();
    
        while (ps2.IsAlive() && ps.IsAlive())
        {
            yield return null;
        }

        Destroy(transform.gameObject);
    }

    public void ShootBeamInDir(Vector3 start, Vector3 target)
    {
        startPoint = start;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, target - start, out hit))
        {
            beamEnd.transform.position = endPoint = end = hit.point;

            // 충돌한 오브젝트의 정면을 바라봅니다.
            Debug.Log(hit.transform.gameObject.name);
            Debug.Log(hit.normal);
            // beamEnd.transform.rotation = Quaternion.LookRotation(hit.normal);
            beamEnd.transform.LookAt(beamEnd.transform.position + hit.normal);
        }
        else
        {
            beamEnd.transform.position = endPoint = end = target;
        }

        line.SetPosition(1, target);

        beamStart.transform.LookAt(beamEnd.transform.position);
    }

    private void ScrollBeam()
    {
        float distance = Vector3.Distance(startPoint, endPoint);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / 3, 1);
        textureScrollOffset -= Time.deltaTime * 8;
        if (textureScrollOffset < 0f)
            textureScrollOffset += 1f;
        line.sharedMaterial.mainTextureOffset = new Vector2(textureScrollOffset, 0);
    }
}
