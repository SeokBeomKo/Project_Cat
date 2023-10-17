using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void PlayerRunInputHandle(Vector3 _dir);
    public event PlayerRunInputHandle OnPlayerRunInput;
}
