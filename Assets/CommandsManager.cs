using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandsManager : MonoBehaviour
{
    private bool isActive => CheckIsChildActive();
    private GameObject canvas;
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.transform.GetChild(0).gameObject;
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Question))
        {
            if (!isActive)
            {
                canvas.SetActive(true);
                input.Select();
                input.ActivateInputField();
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }
    private bool CheckIsChildActive()
    {
        return canvas.activeInHierarchy;
    }
}
