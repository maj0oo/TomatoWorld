using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public MenuOptions selectedOption;

    public Text newGameObj;
    public Text continueObj;
    public Text saveObj;
    public Text exitObj;
    public Color selectedColor;
    public Color unselectedColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedOption == MenuOptions.cont)
            {
                selectedOption = MenuOptions.exit;
                UpdateColors(selectedOption);
                return;
            }
            selectedOption--;
            UpdateColors(selectedOption);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedOption == MenuOptions.exit)
            {
                selectedOption = MenuOptions.cont;
                UpdateColors(selectedOption);
                return;
            }
            selectedOption++;
            UpdateColors(selectedOption);
        }
    }
    private void UpdateColors(MenuOptions option)
    {
        ResetColors();
        switch (option)
        {
            case MenuOptions.cont:
                continueObj.color = selectedColor;
                break;
            case MenuOptions.newGame:
                newGameObj.color = selectedColor;
                break;
            case MenuOptions.exit:
                exitObj.color = selectedColor;
                break;
            case MenuOptions.save:
                saveObj.color = selectedColor;
                break;
        }
    }
    private void ResetColors()
    {
        newGameObj.color = unselectedColor;
        continueObj.color = unselectedColor;
        saveObj.color = unselectedColor;
        exitObj.color = unselectedColor;
    }
    public enum MenuOptions
    {
        cont = 0,
        newGame = 1,
        save = 2,
        exit = 3
    }
}
