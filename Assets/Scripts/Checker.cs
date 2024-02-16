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

        Transform parent = transform.parent.transform.parent;
        int angle = Mathf.RoundToInt(parent.transform.rotation.eulerAngles.z);

        if(angle > 0 && angle <= 90)
        {
            float angle_Complex = parent.transform.rotation.eulerAngles.z / 90;
            float zAngle_Overtime = 45 - 90 * angle_Complex;
            transform.rotation = Quaternion.Euler(0, 0, zAngle_Overtime);
        }
        else if(angle-360 > -180 && angle - 360 <= -90)
        {
            float angle_Complex = (parent.transform.rotation.eulerAngles.z - 180) / 90;
            float zAngle_Overtime = -315 - 90 * angle_Complex;
            transform.rotation = Quaternion.Euler(0, 0, zAngle_Overtime);
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
