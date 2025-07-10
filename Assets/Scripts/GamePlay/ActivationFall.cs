using UnityEngine;
using System.Collections;

public class ActivationFall : MonoBehaviour
{
    public GameObject[] fallHead;
    public float delayBetweenFalls = 0.5f; 
    public float gravityToApply = 1f;      

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated && collision.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(ActivateFallSequence());
        }
    }

    IEnumerator ActivateFallSequence()
    {
        foreach (GameObject obj in fallHead)
        {
            if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.gravityScale = gravityToApply;
            }

            yield return new WaitForSeconds(delayBetweenFalls);
        }
    }
}
