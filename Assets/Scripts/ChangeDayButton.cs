using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDayButton : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    public TMP_Text failText;
    public GameObject DoorUi;
    
    public float displayTime = 3f;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        failText.enabled = false;
    }

    void TaskOnClick()
    {
        bool isDayChanged = GameStateManager.Instance.ChangeDay();

        if (isDayChanged && GameStateManager.FINAL_DAY == GameStateManager.Instance.day)
        {
            Debug.Log("inside");
            myDoor.Play("DoorOpen", 0, 0.0f);
            DoorUi.SetActive(false);
        }
        else if (!isDayChanged)
        {
            StartCoroutine(DisplayTextCoroutine());
        } 
        Debug.Log("You have clicked the button!");
    }

    

    private IEnumerator DisplayTextCoroutine()
    {
        failText.enabled = true; 
        yield return new WaitForSeconds(displayTime);
        failText.enabled = false; 
    }
}
