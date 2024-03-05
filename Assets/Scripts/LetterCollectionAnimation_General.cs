using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCollectionAnimation_General : MonoBehaviour
{
    [HideInInspector] public Transform newPosition_General;

    private Animator _anim_General;
    private RectTransform _rectTransform_General;

    // Start is called before the first frame update
    void Start()
    {
        _anim_General = GetComponent<Animator>();
        _rectTransform_General = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newPosition_General == null) return;

         _rectTransform_General.pivot = new Vector2(0.5f, 0.0f);
        transform.position = Vector2.Lerp(transform.position, newPosition_General.position, 5 * Time.deltaTime);
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        if(Vector3.Distance(transform.position, newPosition_General.position) < 0.1f)
        {
            _anim_General.SetBool("shouldShrink", true);
        }
    }
}
