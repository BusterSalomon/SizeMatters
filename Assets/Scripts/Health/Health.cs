using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float startingHealth = 10f; // Setze den Startwert für die Gesundheit
    public float currentHealth { get; private set; }

    // Referenz zum GameOverScreen
    public GameOverScreen gameOverScreen;

    private void Awake() 
    {
        currentHealth = startingHealth; // Initialisiere die Gesundheit
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth); // Gesundheit begrenzen (maximal 0)
        

        
        if(currentHealth > 0){
            //player hurt
            AudioManager.instance.Play("TimHurt");
        }else{
            //Dead
            AudioManager.instance.Play("TimDie");
        }
    
    }    

    public void AddHealth(float _health){
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
    }

    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.E)){
    //         TakeDamage(1);
    //     }
    // }

}
