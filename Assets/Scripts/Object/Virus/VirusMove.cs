using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VirusMove : MonoBehaviour
{
    [Header("데이터")]
    public SpeedData data;
    private float speed;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        speed = data.speed;
    }

    private float length;
    private float runningTime;
    private float yPos = 0f;
    private Vector3 pos;
    // Update is called once per frame
    private void Start()
    {
        pos = transform.parent.position;
        runningTime = Random.value * 10f;
        length = Random.Range(0.05f, 0.2f);

    }
    void Update()
    {
        runningTime += Time.deltaTime * speed;
        yPos = Mathf.Sin(runningTime) * length;
        this.transform.parent.position = new Vector3(pos.x, pos.y + yPos, pos.z);
    }
}