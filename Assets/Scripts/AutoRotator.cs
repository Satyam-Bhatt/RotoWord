using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    [HideInInspector] public Quaternion newRot = Quaternion.identity;

    [SerializeField] private int perSectionAngle;
    [SerializeField] public int offset = 0;

    [HideInInspector]
    public bool isAutoRotating = true;

    private void Start()
    {
        newRot = Quaternion.AngleAxis(offset, Vector3.forward);
    }

    private void Update()
    {
        if(!isAutoRotating) return;
        Vector3 ra = new Vector3(1,0,0);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            int roll_Z = Mathf.RoundToInt(Ang(transform.rotation.eulerAngles.z - offset) / perSectionAngle);
            int newRotation = roll_Z * (perSectionAngle);

            newRot = Quaternion.AngleAxis(newRotation + offset, Vector3.forward);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 10f * Time.deltaTime);
    }
    private float Ang(float a)
    {
        if (a >= 180)
        {
            return a - 360;
        }
        return a;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.right * 10);
    }
}
