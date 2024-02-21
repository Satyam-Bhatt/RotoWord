using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentRotate : MonoBehaviour
{
    [SerializeField] private Transform thingToParent;

    private Transform parent;
    private AutoRotator autoRotator_Child;
    private RaycastHit2D hit;

    private void Start()
    {
        autoRotator_Child = thingToParent.GetComponent<AutoRotator>();
        parent = thingToParent.parent;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            thingToParent.SetParent(parent);

            hit = Physics2D.Raycast(mousePosition, Vector3.forward, 0f, 5);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("OtherTakerCircle"))
                {
                    autoRotator_Child.isAutoRotating = false;
                    thingToParent.SetParent(this.transform);
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            autoRotator_Child.isAutoRotating = true;
        }
    }
}
