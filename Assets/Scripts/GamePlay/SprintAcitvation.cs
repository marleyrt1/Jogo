using UnityEngine;

public class SprintAcitvation : MonoBehaviour
{
    public GameObject hint;
    public GameObject Trap;
    public Transform pointFinal;
    public Transform point;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().isSprint = true;
            hint.SetActive(true);
            GameObject _trap =  Instantiate(Trap,point.position,point.rotation);
            _trap.GetComponent<SawMove>().point = pointFinal;

        }
    }
}
