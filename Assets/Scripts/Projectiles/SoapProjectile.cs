using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapProjectile : MonoBehaviour, IAttackable, IProjectile
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
        StartCoroutine(CheckExit());
    }

    private float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void Explosion()
    {
        SoundManager.Instance.PlaySFX("ExplosionSoapRifle");
        beam.SetActive(false);
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

    public void SetScale(int charge)
    {
        switch(charge)
        {
            case 1:
                line.startWidth = 0.2f;
                line.endWidth = 0.2f;
                transform.localScale *= 2f;
                break;
            case 2:
                line.startWidth = 0.3f;
                line.endWidth = 0.3f;
                transform.localScale *= 3f;
                break;
            case 3:
                line.startWidth = 0.4f;
                line.endWidth = 0.4f;
                transform.localScale *= 4f;
                break;
            default:
                break;
        }
    }

    public void ShootBeamInDir(Vector3 start, Vector3 target, int charge = 0)
    {
        SetScale(charge);
        startPoint = start;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, target - start, out hit))
        {
            beamEnd.transform.position = endPoint = end = hit.point;
            if (null != hit.collider.GetComponentInChildren<IDamageable>())
                hit.collider.GetComponentInChildren<IDamageable>().BeAttacked(damage);

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
