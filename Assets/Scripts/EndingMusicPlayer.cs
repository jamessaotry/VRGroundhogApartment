using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor.Build;


public class EndingMusicPlayer : MonoBehaviour
{
    // singleton implementation of a ending music.
    public static EndingMusicPlayer Instance { get; private set; }
    public GameObject player;
    private AudioSource musicSource;
    private bool musicPlaying;
    private bool endingSpace;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // check if already exists
        if (Instance == null)
        {
            // set to this
            Instance = this;
            musicPlaying = false;
            musicSource = GetComponent<AudioSource>();
            /*player = GameObject.Find("XR Origin");*/
            Debug.Log(player);
            // so this exists between scene changes
            DontDestroyOnLoad(gameObject);


        }
        else if (Instance != this)
        {
            // destroy this if this script happened to be ran when a GameStateManager already exists
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!endingSpace)
        {
            gameObject.transform.position = player.transform.position;
        }
        if (musicPlaying)
        {
            return;
        }

        if (GameStateManager.Instance.grabbedKeys && GameStateManager.Instance.checkedPhone && GameStateManager.Instance.ateBreakfast)
        {
            
            musicSource.Play();
            musicPlaying = true;
            endingSpace = true;
        }
    }
}