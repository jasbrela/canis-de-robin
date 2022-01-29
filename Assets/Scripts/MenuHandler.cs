using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene(Scenes.Game.ToString());
    }
    
    public void OnClickOptions()
    {
        // TODO: Implement configs
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
