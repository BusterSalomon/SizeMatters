using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> _interactableInRange = new List<IInteractable>();
    //public GameObject Character;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact")  && _interactableInRange.Count > 0 ){
            var interactable  = _interactableInRange[0];
            interactable.Interact();
            if(interactable.CanInteract()){
                _interactableInRange.Remove(interactable);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var interactable  = other.GetComponent<IInteractable>();

        if(interactable != null && interactable.CanInteract()){
            Debug.Log("Interactable Added");
            _interactableInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var interactable  = other.GetComponent<IInteractable>();

        if(_interactableInRange.Contains(interactable)){
            Debug.Log("Interactable Removed");
            _interactableInRange.Remove(interactable);
        }        
    }
}
