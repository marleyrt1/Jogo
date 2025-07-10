using UnityEngine;

public class BeeController : MonoBehaviour
{
    public float amplitude = 1f;        
    public float cycleDuration = 2f;   
    public float moveSpeed = 1f;       

    private float startY;
    private float timeOffset;

    void Start()
    {
        startY = transform.position.y;
        timeOffset = Random.Range(0f, Mathf.PI * 2f); 
    }

    void Update()
    {
        float time = (Time.time + timeOffset) * moveSpeed;
        float cycle = Mathf.Sin((time / cycleDuration) * Mathf.PI * 2);
        float offsetY = cycle * amplitude;

        transform.position = new Vector3(transform.position.x, startY + offsetY, transform.position.z);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeDamage();

        }
    }
}
