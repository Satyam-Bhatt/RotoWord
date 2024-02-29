using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOtherCircle : MonoBehaviour
{
    [SerializeField] private Transform circleToRotate = null;

    private RaycastHit2D hit;
    private float angleOffset;
    private AutoRotator autoRotator;
    private bool colliderFound = false;

    int newRotation = 0;

    //---------------------- If the Circle does not rotate after adding the code below ----------------------
    //|_|_|_|_|_|_|_|_|_|_|_| Add the "InfluentialCircle" Tag |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|

    private void Awake()
    {
         autoRotator = circleToRotate.GetComponent<AutoRotator>();
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(mousePosition, Vector3.forward, 0f, 5);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.CompareTag("InfluentialCircle"))
                {
                    autoRotator.isAutoRotating = false;

                    colliderFound = true;

                    Vector2 direction = (mousePosition - hit.transform.position).normalized;

                    angleOffset = AngleOffsetCalculation(direction, circleToRotate);
                }
            }
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (hit.collider != null )
            {
                if (hit.collider.gameObject.CompareTag("InfluentialCircle"))
                {
                    Vector2 direction = (mousePosition - hit.transform.position).normalized;
                    RotateObject(direction, circleToRotate);
                }
            }

        }

        if (colliderFound && Input.GetKeyUp(KeyCode.Mouse0))
        {

            int roll_Z = Mathf.RoundToInt(Ang(circleToRotate.transform.rotation.eulerAngles.z - autoRotator.offset) / 90);
            newRotation = roll_Z * 90;

            autoRotator.newRot = Quaternion.AngleAxis(newRotation + autoRotator.offset, Vector3.forward);

            float num = newRotation + autoRotator.offset;
            Debug.Log(newRotation + "-> new rotation " + autoRotator.offset +"--> Offset Total-->"+ num + "    - Another Script");
            
            autoRotator.isAutoRotating = true;
            colliderFound = false;

        }
    }

    private float AngleOffsetCalculation(Vector2 direction, Transform transform_1)
    {
        return (Mathf.Atan2(transform_1.right.y, transform_1.right.x) - Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
    }

    private void RotateObject(Vector3 direction, Transform transform_1)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform_1.rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
    }

    private float Ang(float a)
    {
        if (a >= 180)
        {
            return a - 360;
        }
        return a;
    }
}
