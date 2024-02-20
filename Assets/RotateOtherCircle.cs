using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOtherCircle : MonoBehaviour
{
    [SerializeField] private Transform circleToRotate = null;

    private RaycastHit2D hit;
    private float angleOffset;

    //---------------------- If the Circle does not rotate after adding the code below ----------------------
    //|_|_|_|_|_|_|_|_|_|_|_| Add the "InfluentialCircle" Tag |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|

    private void Update()
    {
        AutoRotator autoRotator = circleToRotate.GetComponent<AutoRotator>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(mousePosition, Vector3.forward, 0f, 5);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.CompareTag("InfluentialCircle"))
                {
                    autoRotator.isAutoRotating = false;
                    Vector2 direction = (mousePosition - transform.position).normalized;
                    angleOffset = AngleOffsetCalculation(direction, transform);
                }
            }
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (hit.collider != null )
            {
                if (hit.collider.gameObject.CompareTag("InfluentialCircle"))
                {
                    Vector2 direction = (mousePosition - transform.position).normalized;
                    RotateObject(direction, circleToRotate);
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            autoRotator.isAutoRotating = true;
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
}
