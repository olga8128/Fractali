using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    //UI Elements
    public GameObject shapePrefab;
    public GameObject shapeContainer;
    public GameObject shapeScroll;

    //Items list
    public Shape[] shapeList;

    void Start()
    {
        LoadList();
    }

    //
    //Creates shape views in ScrollView 
    public void LoadList()
    {
        for (int i = 0; i < shapeList.Length; i++)
        {
            GameObject prefabItem = Instantiate(shapePrefab, shapeContainer.transform);
            ShapeView data = prefabItem.GetComponent<ShapeView>();
            data.LoadData(shapeList[i]);
        }
    }

    public void ToggleShapeScroll()
    {
        shapeScroll.SetActive(!shapeScroll.activeSelf);
    }

    public void HideShapeScroll()
    {
        shapeScroll.SetActive(false);
    }

    public Shape FirstShape()
    {
        return shapeList[0];
    }
}
