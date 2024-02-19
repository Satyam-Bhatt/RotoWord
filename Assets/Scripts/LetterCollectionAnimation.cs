using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterCollectionAnimation : MonoBehaviour
{
    [HideInInspector] public bool isPlaying = false;

    [SerializeField] private Transform newPosition;
    [SerializeField] private bool flip = false;

    public bool isPersistent;

    private Animator animator;
    private LetterRotation letterRotation;
    private RectTransform rectTransform;
    private GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
        animator = GetComponent<Animator>();
        if(flip == false)  letterRotation = GetComponent<LetterRotation>();
        else letterRotation = parent.GetComponent<LetterRotation>();
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {

        if (isPlaying)
        {
            if (isPersistent)
            {
                GameObject thisLetter = transform.gameObject;
                GameObject newLetter = Instantiate(thisLetter, transform.position, transform.rotation);
                newLetter.GetComponent<LetterCollectionAnimation>().isPlaying = false;
                newLetter.GetComponent<LetterCollectionAnimation>().isPersistent = true;
                newLetter.transform.SetParent(thisLetter.transform.parent);
                isPersistent = false;
            }
            if (flip)
            {
                GameObject thisLetter2 = transform.gameObject;
                TMP_Text text = thisLetter2.GetComponent<TMP_Text>();
                if (text != null) { text.isRightToLeftText = true; }
                flip = false;
            }
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
