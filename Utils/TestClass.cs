using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    public void DebugMessage(int message) => Debug.Log(message);
    public void DebugMessage(string message) => Debug.Log(message);
}
