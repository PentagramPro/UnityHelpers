﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumericFieldController : MonoBehaviour {

	public Color NegativeColor = new Color(1,0.3f,0.3f);
	public Color PositiveColor = new Color(0.3f,1,0.3f);

	float Speed = 1000;
	public float TimeToEquality = 1;
	Text text;

	[StoreThis]
	int fieldValue = 0;

	float displayedValue = 0;

	public bool runMode = true;
	public bool useColors = false;
	protected void Awake ()
	{
	
		text = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		UpdateText();
	}

	void OnEnable()
	{
		displayedValue = 0;
		UpdateSpeed();
	}

	[ExecuteAfterLoad]
	void OnDeserialized()
	{
		UpdateSpeed();
	}

	// Update is called once per frame
	void Update () {
		if(displayedValue!=fieldValue)
		{
			if(runMode)
			{
				float  iDelta = fieldValue - displayedValue;
				float increment = Speed*Time.smoothDeltaTime;

				iDelta=Mathf.Sign(iDelta)*Mathf.Min( increment,Mathf.Abs(iDelta));

				displayedValue+=iDelta;
			}
			else
			{
				displayedValue = fieldValue;
			}
			UpdateText();
		}
	}

	public int Value{
		get{
			return fieldValue;
		}
		set{
			if(value!=fieldValue)
			{
				fieldValue = value;
				UpdateSpeed();
			}
		}
	}
	public void AddValue(int delta)
	{
		fieldValue+=delta;
		UpdateSpeed();
	}

	protected void UpdateSpeed()
	{
		float delta = Mathf.Abs(fieldValue-displayedValue);
		Speed = delta/TimeToEquality;
	}

	protected void UpdateText()
	{
		text.text = ((int)displayedValue).ToString("#,##0");
		if(useColors)
		{
			if(displayedValue>=0)
				text.color = PositiveColor;
			else
				text.color = NegativeColor;
		}
	}
}
