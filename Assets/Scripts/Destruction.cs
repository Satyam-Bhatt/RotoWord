using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("Destruction");
    }
}
