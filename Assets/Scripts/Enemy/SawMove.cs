using UnityEngine;

public class SawMove : MonoBehaviour
{
    [Header("Destino e Velocidade")]
    public Transform point;   // Transform de destino
    public float speed = 5f;  // Velocidade do movimento

    void Update()
    {
        if (point != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                point.position,
                speed * Time.deltaTime
            );

            // Verifica se chegou no ponto
            if (Vector2.Distance(transform.position, point.position) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().FallLose();

        }
    }
}
