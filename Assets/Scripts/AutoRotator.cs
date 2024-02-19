using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    private Quaternion newRot = Quaternion.identity;

    [SerializeField] private int perSectionAngle;
    [SerializeField] private int offset = 0;

    private void Start()
    {
        newRot = Quaternion.AngleAxis(offset, Vector3.forward);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            int roll_Z = Mathf.RoundToInt(transform.rotation.eulerAngles.z / perSectionAngle);
            int newRotation = roll_Z * perSectionAngle;

            newRot = Quaternion.AngleAxis(newRotation + offset, Vector3.forward);          
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 10f * Time.deltaTime);
    }
}
