using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCollectionAnimation : MonoBehaviour
{
    public bool isPlaying = false;

    [SerializeField] private Transform newPosition;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            transform.position = Vector2.Lerp(transform.position, newPosition.position, 5 * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
    }
}
