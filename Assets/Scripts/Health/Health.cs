using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}

    private void Awake() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage){

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        currentHealth -= _damage;

        Debug.Log("I took damage");
        
        if(currentHealth > 0){
            //player hurt
        }else{
            //Dead
        }
    
    }    

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }
    }

}
