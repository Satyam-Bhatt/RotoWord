using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checker : MonoBehaviour
{
    [SerializeField] private string[] letters;
    [SerializeField] private int direction_Ray = 1;
    [SerializeField] private WinManager winManager;

    private List<TMP_Text> texts;
    private bool runOnce = true;

    private void Start()
    {
        texts = new List<TMP_Text>();
        winManager.win_Counter += 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, direction_Ray * (transform.position + transform.right * 10f));
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            texts.Clear();
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position,transform.position + (direction_Ray * transform.right), 10f);
            foreach (RaycastHit2D h in hit)
            {
                TMP_Text text = h.transform.GetComponent<TMP_Text>();

                if (text != null)
                {
                    texts.Add(text);
                }
            }
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
            runOnce = false;
        }
    }
}
