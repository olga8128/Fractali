using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeView : MonoBehaviour
{
    //UI Elements
    public Button shapeButton;

    //Controllers
    FractalJulia fractalJulia;

    private void Start()
    {
        fractalJulia = GameObject.FindAnyObjectByType<FractalJulia>();
    }

    public void LoadData(Shape shape)
    {
        shapeButton.GetComponent<Image>().sprite = shape.image;
        shapeButton.onClick.AddListener(() => fractalJulia.SetShape(shape));
    }

    public void LoadOnlyData(Shape shape)
    {
        shapeButton.GetComponent<Image>().sprite = shape.image;
    }
}
