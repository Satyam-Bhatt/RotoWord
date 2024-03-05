using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class Checker : MonoBehaviour
{
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
    [SerializeField] private GameObject[] mySpecialLetter;

    [Header("Dictionary")]
    [SerializeField] private ReadFromJSON readFromJSON;

    [Header("Normal Word Collection")]


    [HideInInspector]
    public LetterCollectionAnimation letterCollectionAnimation;


    [HideInInspector] public List<LetterCollectionAnimation> letterCollectionAnimationFromHit = new List<LetterCollectionAnimation>();
    [HideInInspector] public string wordCompleted = null;

    //Hit Storage Area
    private List<GameObject> objectHit = new List<GameObject>();
    
    private void Start()
    { 
        winManager.win_Counter += 1;
        letterCollectionAnimation = GetComponentInChildren<LetterCollectionAnimation>();

        foreach (GameObject g in mySpecialLetter)
        {
            if (g.GetComponent<TMP_Text>() != null) g.GetComponent<TMP_Text>().enabled = false;
            else g.GetComponentInChildren<TMP_Text>().enabled = false;
            if (g.GetComponent<Checker>() != null) g.GetComponent<Checker>().enabled = false;
            if (g.GetComponent<BoxCollider2D>() != null) g.GetComponent<BoxCollider2D>().enabled = false;
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
    }

    private void Check()
    {
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
                    letter.Add(text.text);
                }
            }
        }

        if (letter.Count < 1) return;

        string myText = null;
        string myText_Store = null;
        string myText_Reverse = null;
        string myText_Forward = null;

        //----> Commented out this because we added box collider to the all the words <----------

/*        if (GetComponent<TMP_Text>() != null)
        {
            myText = GetComponent<TMP_Text>().text;
        }
        else
        {
            myText = GetComponentInChildren<TMP_Text>().text;
        }*/

        myText_Store = myText;

        foreach(string l in letter)
        {
            myText = myText + l;
        }
        myText_Forward = myText;
        myText = myText_Store;

        foreach (string l in letter)
        {
            myText = l + myText;
        }
        myText_Reverse = myText;

        foreach(var l in readFromJSON.commonWordsList)
        {
            if(l.Key == myText_Forward)
            {
                winManager.win_Counter -= 1;
                winManager.checkers.Add(this);
                winManager.AnimationRoutineCaller();
                
                wordCompleted = l.Key;
                Debug.Log(l.Key);

                readFromJSON.commonWordsList.Remove(l.Key);

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
        //Destroy(gameObject);
    }

    public void LetterEnablerCheck()
    {
        foreach (GameObject g in mySpecialLetter)
        {
            if (g.GetComponent<TMP_Text>() != null) g.GetComponent<TMP_Text>().enabled = true;
            else g.GetComponentInChildren<TMP_Text>().enabled = true;
            if (g.GetComponent<Checker>() != null) g.GetComponent<Checker>().enabled = true;
            if (g.GetComponent<BoxCollider2D>() != null) g.GetComponent<BoxCollider2D>().enabled = true;
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
