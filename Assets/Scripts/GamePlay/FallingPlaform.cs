using UnityEngine;

public class FallingPlaform : MonoBehaviour
{   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Invoke("Action", .8f);
        }
    }

    public void Action()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
}
