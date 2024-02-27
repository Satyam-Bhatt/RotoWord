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
    [SerializeField] private bool up_DownCheck = false;

    [Header("Disabled Letter")]
    [SerializeField] private DisabledLetterController disabledLetterController;
    [SerializeField] private bool mySpecialLetterCheck = false;
    [SerializeField] private GameObject[] mySpecialLetter;
    [SerializeField] private int numberOfLetterToSpawn = 0;

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

        if (mySpecialLetterCheck)
        {
            foreach (GameObject g in mySpecialLetter)
            {
                if (g.GetComponent<TMP_Text>() != null) g.GetComponent<TMP_Text>().enabled = false;
                else g.GetComponentInChildren<TMP_Text>().enabled = false;
                g.GetComponent<Checker>().enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!up_DownCheck)
        Gizmos.DrawRay(transform.position,(direction_Ray * transform.right) * lenght);
        else if(up_DownCheck)
        Gizmos.DrawRay(transform.position,(direction_Ray * transform.up) * lenght);
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
            winManager.AnimationRoutineCaller();
            runOnce = false;
        }
    }

    private void Check()
    {
        texts.Clear();
        objectHit.Clear();// <-- this causes a bug where the letters are not destroyed if the player does not wait
        letterCollectionAnimationFromHit.Clear();
        RaycastHit2D[] hit = null;
        if(!up_DownCheck) hit = Physics2D.RaycastAll(transform.position,direction_Ray * transform.right, lenght);
        else if(up_DownCheck) hit = Physics2D.RaycastAll(transform.position,direction_Ray * transform.up, lenght);
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
        foreach(GameObject o in objectHit) 
        { 
            Destroy(o); 
        }
        Destroy(gameObject);
    }

    public void LetterEnablerCheck()
    {
        if(mySpecialLetterCheck)
        {
            foreach(GameObject g in mySpecialLetter)
            {
                if(g.GetComponent<TMP_Text>() != null) g.GetComponent<TMP_Text>().enabled = true;
                else g.GetComponentInChildren<TMP_Text>().enabled = true;
                g.GetComponent<Checker>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i <= numberOfLetterToSpawn; i++)
            {
                if (disabledLetterController != null) disabledLetterController.EnableLetter();
            }
        }

    }
}
