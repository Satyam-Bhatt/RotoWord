using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterCollectionAnimation : MonoBehaviour
{
    [HideInInspector] public bool isPlaying = false;

    [SerializeField] private Transform newPosition;
    //[SerializeField] private bool flip = false;

    //public bool isPersistent;

    private Animator animator;
    private LetterRotation letterRotation;
    private LetterRotation_8 LetterRotation_8;
    private RectTransform rectTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();

        letterRotation = GetComponent<LetterRotation>();

        LetterRotation_8 = GetComponent<LetterRotation_8>();

        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {

        if (isPlaying)
        {
/*            if (isPersistent)
            {
                GameObject thisLetter = transform.gameObject;
                GameObject newLetter = Instantiate(thisLetter, transform.position, transform.rotation);
                newLetter.GetComponent<LetterCollectionAnimation>().isPlaying = false;
                newLetter.GetComponent<LetterCollectionAnimation>().isPersistent = true;
                newLetter.transform.SetParent(thisLetter.transform.parent);
                isPersistent = false;
            }*/
/*            if (flip)
            {
                GameObject thisLetter2 = transform.gameObject;
                TMP_Text text = thisLetter2.GetComponent<TMP_Text>();
                if (text != null) { text.isRightToLeftText = true; }
                flip = false;
            }*/
            if(letterRotation != null) letterRotation.enabled = false;
            if(LetterRotation_8 != null) LetterRotation_8.enabled = false;

            rectTransform.pivot = new Vector2(0.5f, 0.0f);

            transform.position = Vector2.Lerp(transform.position, newPosition.position, 5 * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, newPosition.position) < 0.2f && isPlaying)
        {
            AnimationPlay();
        }
    }

    private void AnimationPlay()
    {
        animator.SetBool("shouldShrink", true);
    }
}
