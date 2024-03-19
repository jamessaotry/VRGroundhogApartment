using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    // singleton implementation of a gamestate.
    public static GameStateManager Instance { get; private set; }

    public TextUpdater whiteboardManager;
    public FadeView fader;

    public GameObject endingSequence;

    // these are game state variables- objects can access these
    public const int FINAL_DAY = 3; // assuming this is zero-indexed
    public bool grabbedKeys { get; private set; }
    public bool checkedPhone { get; private set; }
    public bool ateBreakfast { get; private set; }

    public bool didSpookyTask1 { get; private set; }
    public bool didSpookyTask2 { get; private set; }
    public bool didSpookyTask3 { get; private set; }
    public int day { get; private set; }
    public bool releaseMode = true; // if true, enable annoying-for-testing things like alarm clock

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // check if already exists
        if (Instance == null)
        {
            // set to this
            Instance = this;

            Debug.Log("Scene #: " + SceneManager.GetActiveScene().buildIndex);
            whiteboardManager = findTextUpdater();
            fader = FindFader();
            // so this exists between scene changes
            DontDestroyOnLoad(gameObject);

            day = SceneManager.GetActiveScene().buildIndex;
            endingSequence = GameObject.Find("EndingRoom");
            endingSequence.SetActive(false);
        }
        else if (Instance != this)
        {
            // destroy this if this script happened to be ran when a GameStateManager already exists
            Destroy(gameObject);
        }
    }

    
    // these are all just game state changers for doing tasks 
    // that other scripts can call
    public void GrabbedKey()
    {
        if (!grabbedKeys)
        {
            whiteboardManager.UpdateTextDisplay(TextUpdater.TaskType.Keys);
        }
        grabbedKeys = true;
    }

    public void PickedUpPhone()
    {
        if (!checkedPhone)
        {
            whiteboardManager.UpdateTextDisplay(TextUpdater.TaskType.Phone);
        }
        checkedPhone = true;
    }

    public void AteBreakfast()
    {
        if (!ateBreakfast)
        {
            whiteboardManager.UpdateTextDisplay(TextUpdater.TaskType.Breakfast);
        }
        ateBreakfast = true;
        
    }

    public void DoSpookyTask1()
    {
        if (!didSpookyTask1)
        {
            whiteboardManager.UpdateTextDisplay(TextUpdater.TaskType.Spooky1);
        }
        didSpookyTask1 = true;
    }

    public void DoSpookyTask2()
    {
        if (!didSpookyTask2)
        {
            whiteboardManager.UpdateTextDisplay(TextUpdater.TaskType.Spooky2);
        }
        didSpookyTask2 = true;
    }


    // call this when we want to the change the day on some interaction
    public bool ChangeDay()
    {


        if (day == FINAL_DAY && grabbedKeys && ateBreakfast && checkedPhone)
        {
            grabbedKeys = false;
            checkedPhone = false;
            ateBreakfast = false;
           
           
            endingSequence.SetActive(true);
            Debug.Log("in ending sequence");
            return true;
        }
        else if (grabbedKeys && ateBreakfast && checkedPhone)
        {
            grabbedKeys = false;
            checkedPhone = false;
            ateBreakfast = false;
            // may need to do something with whiteboardManager here. set to the new one in the new scene?
            day++;

            StartCoroutine(newScene(day));
            Debug.Log("changing day to " + day);
            return true;
        }
        else if (day == 2 && didSpookyTask1 && didSpookyTask2) // haven't figured out all the tasks yet
        {
            didSpookyTask1 = false;
            didSpookyTask2 = false;
            day++;

            StartCoroutine(newScene(day));
            Debug.Log("Successfully changed day");
            return true;
        }
        else
        {
            Debug.Log("Failed to change the day");
            Debug.Log("grabbedKeys: " + grabbedKeys);
            Debug.Log("ateBreakfast: " + ateBreakfast);
            Debug.Log("checkedPhone: " + checkedPhone);
            return false;
        }
    }


    // helper method to find the text updater after we start a new day
    private TextUpdater findTextUpdater()
    {
        Debug.Log("finding whiteboard");
        GameObject whiteboard = GameObject.Find("Whiteboard");
        return whiteboard.GetComponent<TextUpdater>();
    }

    private FadeView FindFader()
    {
        return GameObject.Find("XR Origin/Camera Offset/Main Camera/Fader").GetComponent<FadeView>();
    }


    // Need coroutine to load scene before attempting to find the whiteboard
    // or else findTextUpdater() runs too soon.
    public IEnumerator newScene(int sceneNumber)
    {
        GetComponent<AudioSource>().Play();
        fader.FadeOut();
        yield return new WaitForSeconds(4); // wait for the fade before actually changing scene
        Debug.Log("loading scene " + sceneNumber);
        SceneManager.LoadScene(sceneNumber);

        if (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            StartCoroutine("waitForSceneLoad", sceneNumber);
        }
    }

    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
        {
            /*Debug.Log(SceneManager.GetActiveScene().buildIndex);*/
            whiteboardManager = findTextUpdater();
            endingSequence = GameObject.Find("EndingRoom");
            endingSequence.SetActive(false);
            fader = FindFader();
            Debug.Log("successfully loaded scene");
            
        }

        // after loading, day and sceneIndex should be the same.
        Debug.Log("SceneIndex: " + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Day " + Instance.day);
    }
}
