using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationScript : MonoBehaviour
{
    public bool isPlaying = false;    

    [SerializeField] private Vector2 newPosition = Vector2.zero;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            transform.position = Vector2.Lerp(transform.position, newPosition, 5 * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
    }
}
