using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [HideInInspector] public int win_Counter = 0;
    [HideInInspector] public List<Checker> checkers = new List<Checker>();
    [HideInInspector] public float timeCounter = 0f;
    [HideInInspector] public bool isRunning = false;

    [SerializeField] private GameObject[] levels;
    [SerializeField] private Transform[] newPositions; 

    private bool moveToPosition = false;
    private int levelCounter = 1;

    void Start()
    {
        for(int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (win_Counter == 0)
        {
            if(levelCounter <= levels.Length - 1)
            {
                Debug.Log(checkers.Count);
                levels[levelCounter].SetActive(true);
                Invoke("LevelDisabler_Enabler", 1.8f); //Time to switch between the next level
            }
            else
            {
                Debug.Log("All Levels Cleared");
            }
        }

        if (moveToPosition)
        {
            levels[levelCounter - 1].transform.position = Vector2.Lerp(levels[levelCounter - 1].transform.position, newPositions[0].position, 5 * Time.deltaTime);
            levels[levelCounter].transform.position = Vector2.Lerp(levels[levelCounter].transform.position, newPositions[1].position, 5 * Time.deltaTime);
            if(Vector2.Distance(newPositions[0].position, levels[levelCounter - 1].transform.position) < 0.1f && Vector2.Distance(newPositions[1].position, levels[levelCounter].transform.position) < 0.1f)
            {
                moveToPosition = false;
                levelCounter++;
            }
        }
    }

    private void LevelDisabler_Enabler()
    {
        moveToPosition = true;
    }

    public async void AnimationRoutineCaller()
    {
        if(checkers.Count > 0)
        {
            await AnimationRoutine();
        }
        else isRunning = false;
    }

    public async Task AnimationRoutine()
    {
        timeCounter = Time.time + 1.8f; //to control the amount of time next word comes and previous one is destroyed
        isRunning = true;
        while (Time.time < timeCounter)
        {
            if(checkers[0].letterCollectionAnimation != null)
            {
                checkers[0].letterCollectionAnimation.isPlaying = true;
            }
            else
            {
                checkers[0].transform.parent.gameObject.GetComponent<LetterCollectionAnimation>().isPlaying = true;
            }
            foreach (LetterCollectionAnimation l in checkers[0].letterCollectionAnimationFromHit) { l.isPlaying = true; }
            await Task.Yield();
        }

        if (checkers.Count > 0)
        {
            checkers[0].DestroyThis();
            checkers.Remove(checkers[0]);
            AnimationRoutineCaller();
        }
        else {
            isRunning = false; 
        }
    }
}
