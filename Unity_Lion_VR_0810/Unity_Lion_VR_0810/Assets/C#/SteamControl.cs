
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;


namespace agi
{
    public class SteamControl : MonoBehaviour
    {
        private UIElement uiReplay;
        private UIElement uiQuit;
        private string currentScene = "Äx²y³õ";


        private void Awake()
        {
            uiReplay = GameObject.Find("Replay").GetComponent<UIElement>();
            uiQuit = GameObject.Find("Quit").GetComponent<UIElement>();
            uiReplay.onHandClick.AddListener((Action) => { SceneManager.LoadScene(currentScene); });
            uiQuit.onHandClick.AddListener((Action) => { Application.Quit(); });
        }
    }
}