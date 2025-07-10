using UnityEngine;

public class collectible : MonoBehaviour
{
    public int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().AddScore(score);
            Destroy(gameObject);
        }
    }
}
