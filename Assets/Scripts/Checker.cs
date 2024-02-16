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
    
    //Hit Storage Area
    private List<Animator> animatorFromHit;
    private List<GameObject> objectHit;

    private void Start()
    {
        texts = new List<TMP_Text>();
        animatorFromHit = new List<Animator>();
        objectHit = new List<GameObject>();
        winManager.win_Counter += 1;
        animator = GetComponentInChildren<Animator>();
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
            Invoke("Check", 0.3f); //change value if you want to change the delay in checking the right letter
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
            foreach (Animator a in animatorFromHit) { a.enabled = true; }
            Invoke("DestroyThis", 0.7f);
            runOnce = false;
        }


    }

    private void Check()
    {
        texts.Clear();
        objectHit.Clear();
        animatorFromHit.Clear();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.position + (direction_Ray * transform.right), lenght);
        foreach (RaycastHit2D h in hit)
        {
            TMP_Text text = h.transform.GetComponent<TMP_Text>();

            if (text != null)
            {
                animatorFromHit.Add(text.GetComponent<Animator>());
                objectHit.Add(h.transform.gameObject);
                texts.Add(text);
            }
        }
    }

    public void DestroyThis()
    {
        foreach(GameObject o in objectHit) { Destroy(o); }
        Destroy(gameObject);
    }
}
