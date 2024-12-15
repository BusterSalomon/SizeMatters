using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Machine : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    public GameObject window;
    public GameObject character;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(){

        Debug.Log("Interacting");
        window.SetActive(true);
        character.GetComponent<Movement>().enabled = false;
    }

    public bool CanInteract(){
        return true;
    }
}
