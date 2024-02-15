using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checker : MonoBehaviour
{
    [SerializeField] private string[] letters;
    [SerializeField] private int direction_Ray = 1;
    [SerializeField] private WinManager winManager;
    [SerializeField] private float lenght = 5f;

    private List<TMP_Text> texts;
    private bool runOnce = true;
    private Animator animator;
    private Animator animatorFromHit;
    private GameObject objectHit;

    private void Start()
    {
        texts = new List<TMP_Text>();
        winManager.win_Counter += 1;
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (direction_Ray * transform.right) * lenght);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Invoke("Check", 0.3f);
        }

        bool value = false;

        for (int i = 0; i < texts.Count; i++)
        {
            if (texts.Count != letters.Length) break;

            if (texts[i].text == letters[i]) value = true;
            else
            {
                value = false;
                break;
            }
        }

        if (value == true && runOnce)
        {
            winManager.win_Counter -= 1;
            animator.enabled = true;
            animatorFromHit.enabled = true;
            runOnce = false;
        }
    }

    private void Check()
    {
        texts.Clear();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.position + (direction_Ray * transform.right), lenght);
        foreach (RaycastHit2D h in hit)
        {
            TMP_Text text = h.transform.GetComponent<TMP_Text>();

            if (text != null)
            {
                animatorFromHit = text.GetComponent<Animator>();
                objectHit = h.transform.gameObject;
                texts.Add(text);
            }
        }
    }

    public void DestroyThis()
    {
        Destroy(objectHit);
        Destroy(gameObject);
    }
}
