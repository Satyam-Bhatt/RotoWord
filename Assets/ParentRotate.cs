using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentRotate : MonoBehaviour
{
    [SerializeField] private Transform thingToParent;

    private Transform parent;
    private AutoRotator autoRotator;
    private bool isAutoRotating = false;
    private RaycastHit2D hit;

    private void Start()
    {
        autoRotator = GetComponent<AutoRotator>();
        parent = thingToParent.parent;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(mousePosition, Vector3.forward, 0f, 5);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("OtherTakerCircle"))
                {
                    thingToParent.SetParent(this.transform);
                }
            }

        }
        if (Input.GetKey(KeyCode.Mouse0))
        {

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isAutoRotating = true;
        }

        if (isAutoRotating && transform.rotation == autoRotator.newRot)//Causing bug in level 11
        {
            thingToParent.SetParent(parent);
            isAutoRotating = false;
            Debug.Log("Parented");
        }
    }
}
