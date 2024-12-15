using TMPro;
using UnityEngine;

public class InputFieldGrabber : MonoBehaviour
{

    [Header ("The Value we got from input field")]
    [SerializeField] private string inputText;
    [Header ("Showing the reaction to the player")]
    [SerializeField]private GameObject reactionGroup;
    [SerializeField]private TMP_Text reactionTextBox;

    public GameObject window;
    private string CODE = "A42B";

    public void GrabFromInputField(string input){
        inputText = input;
        DisplayReactionToInput();
    }

    private void DisplayReactionToInput(){

        if(inputText == CODE){
            //reactionTextBox.text = "Your Input was correct ! Enter self-destruct sequence beginning...";
            window.SetActive(false);
            reactionGroup.GetComponent<Level5Manager>().WinConditionMet = true;
        }
        else{
            reactionGroup.GetComponent<Level5Manager>().LoseConditionMet = true;
        }
        
    }

}
