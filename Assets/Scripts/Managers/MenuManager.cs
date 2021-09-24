using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainScreen;
    private GameObject _currentScreen;
    private GameObject _previousScreen;

    private void Start()
    {
        _currentScreen = _mainScreen;
        _currentScreen.SetActive(true);
    }

    public void SwitchScreen(GameObject newScreen)
    {
        _previousScreen = _currentScreen;
        _currentScreen.SetActive(false);
        newScreen.SetActive(true);
        _currentScreen = newScreen;
    }
    public void GoBack()
    {
        if (_previousScreen != null)
        {
            _currentScreen.SetActive(false);
            _currentScreen = _previousScreen;
            _currentScreen.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
