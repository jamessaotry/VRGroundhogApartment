using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoveDucks : MonoBehaviour
{
    private Rigidbody[] ducks;
    private Vector3 forceVector = new Vector3(-4, 0, 0);
    private bool hasFired = false;

    public Transform explosionPos;
    public GameObject ducksParent;
    public ShowInspectText cerealInspect; // so we can swap the text after the duck event
    public TextAsset spookyCerealText;
    // Start is called before the first frame update
    void Start()
    {
        ducks = ducksParent.GetComponentsInChildren<Rigidbody>();
    }

    public void PushDucks()
    {
        foreach (Rigidbody duck in ducks)
        {
            //duck.AddExplosionForce(2f, explosionPos.position, 1f, 0, ForceMode.Impulse);
            duck.gameObject.GetComponent<OnDuckHit>().SetActive(true);
            duck.AddForce(forceVector, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasFired && !other.CompareTag("Player"))
        {
            hasFired = true;
            PushDucks();
            cerealInspect.description = spookyCerealText;
            GameStateManager.Instance.AteBreakfast();
        }
    }
}
