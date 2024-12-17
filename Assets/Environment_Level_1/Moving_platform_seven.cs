using UnityEngine;



public class Moving_platform_seven : MonoBehaviour
{
    public Transform posC, posD;
    public float speed; 
    Vector3 targetPos;

private void Start()
{
    targetPos = posD.position;
}
    private void Update()
    {
        if (Vector2.Distance(transform.position, posC.position) < 0.05f) 
        {
            targetPos = posD.position;
        }

        if (Vector2.Distance(transform.position, posD.position) < 0.05f) 
        {
            targetPos = posC.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            collision.transform.parent=this.transform; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            collision.transform.parent=null; 
        }
    }
}
