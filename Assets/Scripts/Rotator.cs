using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float angleOffset;
    private Transform transform_col = null;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward);
            if(hit.collider != null)
            {
                transform_col = hit.collider.transform;
                Vector2 direction = (mousePosition - transform_col.position).normalized;
                angleOffset = AngleOffsetCalculation(direction, transform_col);
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 direction = (mousePosition - transform_col.position).normalized;
            RotateObject(direction, transform_col);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)) 
        { 
            transform_col = null;
        }

        Debug.Log(transform_col.gameObject.name);

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
