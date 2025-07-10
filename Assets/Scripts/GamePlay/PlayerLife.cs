using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    private int life;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject losePanel;

    [Header("Audio")]
    public AudioSource aud;
    public AudioClip hit;

    void Start()
    {
        life = 5;
        lifeText.text = life.ToString();
    }

    public void TakeDamage()
    {
        if(life < 0)
        {
            return;
        }
        aud.PlayOneShot(hit);
        life--;
        anim.SetTrigger("Hit");
        if(life <= 0)
        {
            anim.SetTrigger("Die");
            GetComponent<PlayerController>().SetKilled();
            losePanel.SetActive(true);
            life = 0;
            
        }
        lifeText.text = life.ToString();
    }


    public void FallLose()
    {
        anim.SetTrigger("Die");
        GetComponent<PlayerController>().SetKilled();
        aud.PlayOneShot(hit);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        life = 0;
        lifeText.text = life.ToString();
        losePanel.SetActive(true);
    }
}
