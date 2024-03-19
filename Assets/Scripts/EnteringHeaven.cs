using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnteringHeaven : MonoBehaviour
{
    // Start is called before the first frame update
    public FadeView fader;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("colliding w trigger");
            fader.SetFaderColor();
            fader.SetFadeLength(5.0f);
            fader.FadeOut();
            StartCoroutine(SwitchToEndScene());
        }
    }

    private IEnumerator SwitchToEndScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("EndScreen");
    }
        
}
