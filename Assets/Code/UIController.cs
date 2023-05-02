using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private Button continueButton;
    private Button optionsButton;
    //private Button savebutton;
    //private Button loadButton;
    private Button quitToMainMenuButton;
    private Button quitGameButton;
    public InputReader inputReader;
    private bool menuIsActive = false;

    void Start()
    {
        //inputReader = GetComponent<InputReader>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        continueButton = root.Q<Button>("continueButton");
        optionsButton = root.Q<Button>("optionsButton");
        quitToMainMenuButton = root.Q<Button>("quitToMainMenuButton");
        quitGameButton = root.Q<Button>("quitGameButton");

        continueButton.clicked += ContinuePressed;
        //quitToMainMenuButton += QuitToMainMenu;
        quitGameButton.clicked += QuitGame;
    }

    private void ContinuePressed()
    {
        Debug.Log("Continue pressed!");
        menuIsActive = !menuIsActive;
        gameObject.SetActive(false);
    }

    private void QuitToMainMenu()
    {
        Debug.Log("To main menu we go!");
        //TODO: change scene to main menu
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
