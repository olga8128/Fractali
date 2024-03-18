using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionView : MonoBehaviour
{
    //UI Elements
    public Button functionButton;

    //Controllers
    FractalJulia fractalJulia;

    private void Start()
    {
        fractalJulia = GameObject.FindAnyObjectByType<FractalJulia>();
    }

    public void LoadData(Function function)
    {
        functionButton.GetComponent<Image>().sprite = function.image;
        functionButton.onClick.AddListener(() => fractalJulia.SetFunction(function));
    }
    public void LoadOnlyData(Function function)
    {
        functionButton.GetComponent<Image>().sprite = function.image;
    }
}
