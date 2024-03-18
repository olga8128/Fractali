using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FractalJulia : MonoBehaviour
{
	//Objects
	public GameObject decorObject;
	public Slider sliderRange;
	public Slider sliderSize;
	public Slider sliderResolution;
	public Slider sliderColorRange;
	public Slider sliderHue;
	public Toggle toggleSoftColors;
	public Button buttonShape;
	public Button buttonFunction;
	public TMP_InputField inputCx;
	public TMP_InputField inputCy;
	public GameObject parentObject;
	public GameObject newParent;

	//Controllers
	public MenuController menuController;
	public AudioController audioController;
	public FunctionController functionController;
	public ShapeController shapeController;

	// Parameters
	[Tooltip("Value taken as escape to infinite")]
	public float maximum;
	[Tooltip("Maximum number of iterations in the calculation")]
	public int iterations;
	[Tooltip("Average size of shapes")]
	public float adjustSize;
	//Value equal to half of resolution
	float halfResolution;

	//Modifiable parameters
	[Header("===== FRACTAL PARAMETERS =====")]
	public int resolution;
	public int colorRange;
	public int hue;
	public float range;
	public float size;
	public bool softColors;
	[Header("==============================")]

	//Shape and function
	Shape shape;
	Function function;
	Complex functionConstant;
	void Start()
	{
		Initialize();
		GeneratePoints(resolution);
	}

    public void Initialize()
    {
		sliderRange.onValueChanged.AddListener(SetRange);
		sliderSize.onValueChanged.AddListener(SetSize);
		sliderResolution.onValueChanged.AddListener(SetResolution);
		sliderColorRange.onValueChanged.AddListener(SetColorRange);
		sliderHue.onValueChanged.AddListener(SetHue);
		toggleSoftColors.onValueChanged.AddListener(SetSoftColors);
		function = functionController.FirstFunction();
		shape = shapeController.FirstShape();
		//Function constant values - Example fractal
		functionConstant = new Complex(-0.382f, 0.618f);
		inputCx.text = "-0.382";
		inputCy.text = "0.618";
		inputCx.onValueChanged.AddListener(ClicSound);
		inputCy.onValueChanged.AddListener(ClicSound);
		decorObject.transform.localScale = new Vector3(size * adjustSize, size * adjustSize, size * adjustSize);
		halfResolution = resolution / 2f;
	}

	//CHANGE PARAMETERS LISTENERS
	public void SetRange(float value)
    {
		range = value;
		HideScrolls();
		audioController.EchoSound();
	}

	public void SetSize(float value)
	{
		size = value;
		decorObject.transform.localScale = new Vector3(size* adjustSize, size* adjustSize, size* adjustSize);
		HideScrolls();
		audioController.EchoSound();
	}

	public void SetResolution(float value)
	{
		resolution = Mathf.FloorToInt(value);
		halfResolution = resolution / 2f;
		HideScrolls();
		audioController.EchoSound();
	}

	public void SetColorRange(float value)
	{
		colorRange = Mathf.FloorToInt(value);
		HideScrolls();
		audioController.EchoSound();
	}
	public void SetHue(float value)
	{
		hue = Mathf.FloorToInt(value);
		HideScrolls();
		audioController.EchoSound();
	}
	public void SetSoftColors(bool value)
	{
		softColors = value;
		HideScrolls();
		audioController.ClicSound();
	}

	public void SetShape(Shape sh)
	{
		shape = sh;
		decorObject = shape.shapeObject;
		decorObject.transform.localScale = new Vector3(size * adjustSize, size * adjustSize, size * adjustSize);
		buttonShape.GetComponent<ShapeView>().LoadOnlyData(shape);
		HideScrolls();
		audioController.ClicSound();
	}

	public void SetFunction(Function fn)
	{
		function = fn;
		buttonFunction.GetComponent<FunctionView>().LoadOnlyData(function);
		functionConstant = new Complex(fn.cX, fn.cY);
		inputCx.text = functionConstant.x.ToString();
		inputCy.text = functionConstant.y.ToString();
		HideScrolls();
		audioController.ClicSound();
	}

	public void SetShapeFirst(Shape sh)
	{
		shape = sh;
		decorObject = shape.shapeObject;
		decorObject.transform.localScale = new Vector3(size * adjustSize, size * adjustSize, size * adjustSize);
		buttonShape.GetComponent<ShapeView>().LoadOnlyData(shape);
		HideScrolls();
	}

	public void SetFunctionFirst(Function fn)
	{
		function = fn;
		buttonFunction.GetComponent<FunctionView>().LoadOnlyData(function);
		functionConstant = new Complex(fn.cX, fn.cY);
		inputCx.text = functionConstant.x.ToString();
		inputCy.text = functionConstant.y.ToString();
		HideScrolls();
	}

	private void ClicSound(string value)
	{
		audioController.ClicSound();
	}
	//END CHANGE PARAMETERS LISTENERS

	//GENERATE FRACTAL
	public void GenerateFractal()
    {
		functionConstant.x = float.Parse(inputCx.text.Replace(".", ","));
		functionConstant.y = float.Parse(inputCy.text.Replace(".", ","));
		HideScrolls();
		menuController.HidePanelParameters();
		audioController.OkSound();
		GeneratePoints(resolution);
	}

	public void GeneratePoints(int count)
	{
		float adjustStep = 200f /(float)resolution;
		//Clear previous design
		if (newParent != null)
			Destroy(newParent);
		//Create new design
		newParent = Instantiate(parentObject, new Vector3(0, 0, 0), Quaternion.identity);
		for (int i = 0; i < count; i++)
			for (int j = 0; j < count; j++)
			{
				int escapeLevel = GetEscapeLevel(range * (i - halfResolution) / halfResolution, range * (j - halfResolution) / halfResolution);
				GameObject newObject = Instantiate(decorObject, new Vector3(i * adjustStep, j * adjustStep, -escapeLevel * 0.01f), decorObject.transform.rotation);
				newObject.transform.parent = newParent.transform;
				var renderer = newObject.GetComponent<Renderer>();
				Color color = GetColor(escapeLevel);
				renderer.material.SetColor("_Color", color);
			}
	}

	public int GetEscapeLevel(float x, float y)
	{
		Complex z = new Complex(x, y);
		for (int k = 0; k < iterations; k++)
		{
			z = F(z);
			if (Mathf.Abs(z.x) > maximum || Mathf.Abs(z.y) > maximum)
			{
				if (softColors)
					return k / (iterations / colorRange);
				else
					return k % (iterations / colorRange);
			}
		}
		return iterations;
	}

	public Complex F(Complex complex)
	{
		return function.Execute(complex, functionConstant);
	}

	public Color GetColor(int escapeLevel)
	{
		Color color = Color.HSVToRGB((Mathf.FloorToInt(hue + (escapeLevel * colorRange * 0.8f)) % 255) / 255f, 0.5f, 0.9f);
		color.a = 0.8f;
		return color;
	}
	//END GENERATE FRACTAL

	//SCROLLS
	public void HideScrolls()
    {
		shapeController.HideShapeScroll();
		functionController.HideFunctionScroll();
    }

	public void ToggleShapeScroll()
    {
		shapeController.ToggleShapeScroll();
		functionController.HideFunctionScroll();
		audioController.ClicSound();
	}

	public void ToggleFunctionScroll()
	{
		functionController.ToggleFunctionScroll();
		shapeController.HideShapeScroll();
		audioController.ClicSound();
	}
	//END SCROLLS
}
