using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float startingHealth = 10f; // Setze den Startwert für die Gesundheit
    public float currentHealth { get; private set; }

    // Referenz zum GameOverScreen
    public GameOverScreen gameOverScreen;

    private AudioManager am;

    private void Awake() 
    {
        currentHealth = startingHealth; // Initialisiere die Gesundheit
        am = FindObjectOfType<AudioManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth); // Gesundheit begrenzen (maximal 0)
        

        
        if(currentHealth > 0){
            //player hurt
            am.Play("TimHurt");
        }else{
            //Dead
            am.Play("TimDie");
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
