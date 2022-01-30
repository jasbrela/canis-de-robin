using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MenuHandler : MonoBehaviour
    {
        public void OnClickMainMenu()
        {
            SoundManager.Instance.ResetSound();
            SceneManager.LoadScene(Scenes.MainMenu.ToString());
        }
    
        public void OnClickStart()
        {
            SoundManager.Instance.ResetSound();
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
}
