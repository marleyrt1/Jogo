using UnityEngine;

public class Killer : MonoBehaviour
{
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Destroy(transform.parent.gameObject);
        }
    }
}
