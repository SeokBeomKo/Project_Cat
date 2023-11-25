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
        Destroy(transform.gameObject);
    }

    private void Update() 
    {
        ScrollBeam();
    }

    public void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        startPoint = start;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
            endPoint = end = hit.point - (dir.normalized);
        else
            endPoint = end = transform.position + (dir * 100);

        beamEnd.transform.position = dir;
        line.SetPosition(1, dir);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);
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
