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

    private List<TMP_Text> texts = new List<TMP_Text>();
    private bool runOnce = true;

    [HideInInspector]
    public LetterCollectionAnimation letterCollectionAnimation;

    //Hit Storage Area
    private List<GameObject> objectHit = new List<GameObject>();

    [HideInInspector]
    public List<LetterCollectionAnimation> letterCollectionAnimationFromHit = new List<LetterCollectionAnimation>();

    private void Start()
    { 
        winManager.win_Counter += 1;
        letterCollectionAnimation = GetComponentInChildren<LetterCollectionAnimation>();
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
            winManager.checkers.Add(this);
            winManager.testCase();
            runOnce = false;
        }
    }

    private void Check()
    {
        texts.Clear();
        objectHit.Clear();
        letterCollectionAnimationFromHit.Clear();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.localPosition + (direction_Ray * transform.right), lenght);
        foreach (RaycastHit2D h in hit)
        {
            TMP_Text text = h.transform.GetComponent<TMP_Text>();

            if (text != null)
            {
                objectHit.Add(h.transform.gameObject);
                letterCollectionAnimationFromHit.Add(h.transform.GetComponent<LetterCollectionAnimation>());
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
