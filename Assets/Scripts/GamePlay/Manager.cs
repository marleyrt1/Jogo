using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public string aux;

    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aux = SceneManager.GetActiveScene().name;
        if(aux == "Level2")
        {
            playerController.UnlockDoubleJump();
            PlayerPrefs.SetInt("Save", 2);
        }else if(aux == "Level3")
        {
            PlayerPrefs.SetInt("Save", 3);
        }
        else if (aux == "Level4")
        {
            PlayerPrefs.SetInt("Save", 4);
            playerController.UnlockSprint();
        }
    }

   public void NextScene()
   {
        SceneManager.LoadScene(sceneName);
   }


    public void ReloadScene()
    {
        
        SceneManager.LoadScene(aux);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
