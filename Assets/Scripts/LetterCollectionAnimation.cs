using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCollectionAnimation : MonoBehaviour
{
    public bool isPlaying = false;

    [SerializeField] private Transform newPosition;

    private Animator animator;
    private LetterRotation letterRotation;
    private RectTransform rectTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        letterRotation = GetComponent<LetterRotation>();
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {

        if (isPlaying)
        {
            letterRotation.enabled = false;
            rectTransform.pivot = new Vector2(0.5f, 0.0f);

            transform.position = Vector2.Lerp(transform.position, newPosition.position, 5 * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, newPosition.position) < 0.2f)
        {
            AnimationPlay();
        }
    }

    private void AnimationPlay()
    {
        animator.SetBool("shouldShrink", true);
    }
}
