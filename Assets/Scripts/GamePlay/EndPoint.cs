using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject winPanel;
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Action();
            collision.gameObject.GetComponent<PlayerController>().SetKilled();
        }
    }


    public void Action()
    {
        anim.SetTrigger("Action");
        Invoke("ActivePanel", 2f);
    }

    void ActivePanel()
    {
        winPanel.SetActive(true);
    }
}
