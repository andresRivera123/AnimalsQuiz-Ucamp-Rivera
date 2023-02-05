using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
	[SerializeField] private Text m_quest = null; //Texto para la pregunta
	[SerializeField] private List<OptionButton> m_buttonList = null; //Referencia a los botones creados.
	
	public void Construtc(Question q, Action<OptionButton> callback)//Callback: Metodo que pasamos a una función que tiene de valor de refreso, lo que tiene entre los simbolos <>
	{ 
		m_quest.text = q.text;
		for(int n = 0; n < m_buttonList.Count; n++)
		{
			m_buttonList[n].Build(q.options[n], callback);
		}
	}
}
