using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checker_Dictionary : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private int direction_Ray = 1;
    [SerializeField] private ReadFromJSON readFromJSON;

    public List<string> letter = new List<string>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, (direction_Ray * transform.right) * length);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Invoke("Check", 0.3f);
        }
    }

    private void Check()
    {
        letter.Clear();

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction_Ray * transform.right, length);

        if(hit != null)
        {
            foreach(RaycastHit2D h in hit)
            {
                if(h.transform.GetComponent<TMP_Text>() != null)
                {
                    letter.Add(h.transform.GetComponent<TMP_Text>().text);
                }
            }
        }

        string myText = GetComponent<TMP_Text>().text;

        foreach(string l in letter)
        {
            myText = myText + l;
        }
        
        foreach(var l in readFromJSON.commonWordsList)
        {
            if(l.Key == myText)
            {

                Debug.Log("match found");
                break;
            }
        }
    }
}
