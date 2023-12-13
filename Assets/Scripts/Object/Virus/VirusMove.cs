using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VirusMove : MonoBehaviour
{
    [Header("속도, 길이")]
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
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