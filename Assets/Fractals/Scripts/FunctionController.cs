using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionController : MonoBehaviour
{
    //UI Elements
    public GameObject functionPrefab;
    public GameObject functionContainer;
    public GameObject functionScroll;

    //Items list
    public Function[] functionList;

    void Start()
    {
        LoadList();
    }

    //Creates shape views in ScrollView 
    public void LoadList()
    {
        for (int i = 0; i < functionList.Length; i++)
        {
            GameObject prefabItem = Instantiate(functionPrefab, functionContainer.transform);
            FunctionView data = prefabItem.GetComponent<FunctionView>();
            data.LoadData(functionList[i]);
        }
    }

    public void ToggleFunctionScroll()
    {
        functionScroll.SetActive(!functionScroll.activeSelf);
    }

    public void HideFunctionScroll()
    {
        functionScroll.SetActive(false);
    }

    public Function FirstFunction()
    {
        return functionList[0];
    }
}
