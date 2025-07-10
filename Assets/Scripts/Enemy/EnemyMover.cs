using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Pontos de patrulha")]
    public Transform pointA;
    public Transform pointB;

    [Header("Velocidade do inimigo")]
    public float speed = 3f;

    private Transform target;

    void Start()
    {
        target = pointA; // Começa indo para o ponto A
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );

            // Se chegou no destino, troca o alvo
            if (Vector2.Distance(transform.position, target.position) < 0.01f)
            {
                target = (target == pointA) ? pointB : pointA;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeDamage();

        }
    }
}
