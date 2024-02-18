using System.Collections;
using System.Collections.Generic;
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
                GameObject newLetter = Instantiate(thisLetter, transform.position, Quaternion.identity);
                newLetter.GetComponent<LetterCollectionAnimation>().isPlaying = false;
                newLetter.GetComponent<LetterCollectionAnimation>().isPersistent = true;
                newLetter.transform.SetParent(thisLetter.transform.parent);
                isPersistent = false;
            }
            if (flip)
            {
                if(parent!= null) parent.transform.localScale = new Vector3(parent.transform.localScale.x * -1, parent.transform.localScale.y, parent.transform.localScale.z);
                else Debug.Log("Parent is null");
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
