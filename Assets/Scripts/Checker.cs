using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class Checker : MonoBehaviour
{
    [SerializeField] private string[] letters;
    [SerializeField] private int direction_Ray = 1;
    [SerializeField] private WinManager winManager;
    [SerializeField] private float lenght = 5f;
    [SerializeField] private bool up_DownCheck = false;

    [Header("Activation Quadrant")]
    [SerializeField] private ActivationQuadrant activationQuadrant;
    [SerializeField] private int startAngle = 0;
    [SerializeField] private Transform parent;

    [Header("Ray Extender")]
    [SerializeField] private bool RayExtender = false;
    [SerializeField] private bool top_Left = true;

    [Header("Disabled Letter")]
    [SerializeField] private DisabledLetterController disabledLetterController;
    [SerializeField] private bool mySpecialLetterCheck = false;
    [SerializeField] private GameObject[] mySpecialLetter;
    [SerializeField] private int numberOfLetterToSpawn = 0;

    [Header("Dictionary")]
    [SerializeField] private ReadFromJSON readFromJSON;

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
                if(g.GetComponent<Checker>() != null) g.GetComponent<Checker>().enabled = false;
                if(g.GetComponent<BoxCollider2D>() != null) g.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        int angle = Mathf.RoundToInt(parent.transform.rotation.eulerAngles.z) + startAngle;

        if (activationQuadrant == ActivationQuadrant.First_Quadrant)
        {
            if(AngleNormalize(angle) >= 40 && AngleNormalize(angle) <= 140)
            {
                if (!up_DownCheck)
                Gizmos.DrawRay(transform.position, (direction_Ray * transform.right) * lenght);
                else
                Gizmos.DrawRay(transform.position, (direction_Ray * transform.up) * lenght);
            }
        }
        else if(activationQuadrant == ActivationQuadrant.Second_Quadrant)
        {
            if (AngleNormalize(angle) >= 220 && AngleNormalize(angle) <= 320) 
            {
                if (!up_DownCheck)
                Gizmos.DrawRay(transform.position, (direction_Ray * transform.right) * lenght);
                else
                Gizmos.DrawRay(transform.position, (direction_Ray * transform.up) * lenght);
            }
        }
        else
        {
            if (!up_DownCheck)
                Gizmos.DrawRay(transform.position, (direction_Ray * transform.right) * lenght);
            else
                Gizmos.DrawRay(transform.position, (direction_Ray * transform.up) * lenght);
        }
           
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && RayExtender)
        {
            lenght = 0.3f;
        }

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
        RaycastHit2D[] hit_1 = null;

        int angle = Mathf.RoundToInt(parent.transform.rotation.eulerAngles.z) + startAngle;

        //Dirty Code Starts here

        if (top_Left && RayExtender)
        {
            RaycastHit2D[] hit_2 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.right, lenght);
            if (hit_2 != null)
            {
                foreach (RaycastHit2D h in hit_2)
                {
                    if (h.collider.CompareTag("Top_Left"))
                    {
                        lenght = 1.5f;
                        break;
                    }
                    else
                    {
                        lenght = 0.3f;
                    }
                }
            }
        }
        else if (!top_Left && RayExtender)
        {
            RaycastHit2D[] hit_2 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.right, lenght);
            if (hit_2 != null)
            {
                foreach (RaycastHit2D h in hit_2)
                {
                    if (h.collider.CompareTag("Bottom_Right"))
                    {
                        lenght = 1.5f;
                        break;
                    }
                    else
                    {
                        lenght = 0.3f;
                    }
                }
            }
        }

        //Dirty Code Ends here

        if (activationQuadrant == ActivationQuadrant.First_Quadrant)
        {
            if (AngleNormalize(angle) >= 40 && AngleNormalize(angle) <= 140)
            {
                if (!up_DownCheck) hit_1 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.right, lenght);
                else if (up_DownCheck) hit_1 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.up, lenght);
            }
        }
        else if (activationQuadrant == ActivationQuadrant.Second_Quadrant)
        {
            if (AngleNormalize(angle) >= 220 && AngleNormalize(angle) <= 320)
            {
                if (!up_DownCheck) hit_1 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.right, lenght);
                else if (up_DownCheck) hit_1 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.up, lenght);

            }
        }
        else
        {
            if (!up_DownCheck) hit_1 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.right, lenght);
            else if (up_DownCheck) hit_1 = Physics2D.RaycastAll(transform.position, direction_Ray * transform.up, lenght);
        }

        List<string> letter = new List<string>();

        if (hit_1 != null)
        {
            foreach (RaycastHit2D h in hit_1)
            {
                TMP_Text text = h.transform.GetComponent<TMP_Text>();

                if (text != null)
                {
                    objectHit.Add(h.transform.gameObject);
                    letterCollectionAnimationFromHit.Add(h.transform.GetComponent<LetterCollectionAnimation>());
                    //texts.Add(text);

                    letter.Add(text.text);
                }
            }
        }

        string myText = null;
        string myText_Reverse = null;
        string myText_Forward = null;

        if (GetComponent<TMP_Text>() != null)
        {
            myText = GetComponent<TMP_Text>().text;
        }
        else
        {
            myText = GetComponentInChildren<TMP_Text>().text;
        }

        foreach(string l in letter)
        {
            myText_Forward = myText + l;
            myText_Reverse = l + myText;
        }

        Debug.Log("Forward-----> " + myText_Forward);
        Debug.Log("Reverse-----> " + myText_Reverse);

        foreach(var l in readFromJSON.commonWordsList)
        {
            if(l.Key == myText_Forward || l.Key == myText_Reverse)
            {
                winManager.win_Counter -= 1;
                winManager.checkers.Add(this);
                winManager.AnimationRoutineCaller();
                Debug.Log("match found");
                break;
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
                if (g.GetComponent<Checker>() != null) g.GetComponent<Checker>().enabled = true;
                if (g.GetComponent<BoxCollider2D>() != null) g.GetComponent<BoxCollider2D>().enabled = true;
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

    private float AngleNormalize(float angle)
    {
        if (angle >= 360)
        {
            return angle -= 360;
        }
        return angle;
    }

    private enum ActivationQuadrant
    {
        None,
        First_Quadrant,
        Second_Quadrant
    }
}
