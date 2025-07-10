using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    private int index;
    [SerializeField] private Button loadBtn;

    public void Start()
    {
        index = PlayerPrefs.GetInt("Save", 0);

        if(index == 0)
        {
            
            loadBtn.interactable = false;
        }
        else
        {
            loadBtn.interactable = true;
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(index);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
