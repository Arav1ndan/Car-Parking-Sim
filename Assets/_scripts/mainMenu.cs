using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string nextlevelName;
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("new-game-btn").clicked += () => NewGame();
        root.Q<Button>("high-score-btn").clicked += () => HighScore();
        root.Q<Button>("quit-btn").clicked += () => QuitGame();
        root.style.backgroundImage =  Resources.Load<Texture2D>("imageFile");
    }
    private void NewGame()
    {
        SceneManager.LoadScene(nextlevelName);
    }
    private void HighScore()
    {

    }
    private void QuitGame()
    {

    }
    
}
