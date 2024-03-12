using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class nextlevel : MonoBehaviour
{
   public string nextlevelName;
     private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("next-btn").clicked += () => NewGame();
        root.Q<Button>("main-btn").clicked += () => MainMenu();
        root.Q<Button>("quit-btn").clicked += () => QuitGame();
    }
    private void NewGame()
    {
        SceneManager.LoadScene(nextlevelName);
    }
    private void MainMenu()
    {

    }
    private void QuitGame()
    {

    }
}
