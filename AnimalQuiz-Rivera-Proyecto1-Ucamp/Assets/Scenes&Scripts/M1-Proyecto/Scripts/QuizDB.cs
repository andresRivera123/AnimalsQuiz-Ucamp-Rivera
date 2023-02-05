using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDB : MonoBehaviour
{
	[SerializeField] private List<Question> m_questionList = null;
    private List<Question> m_backup = null; //m_backup: Si nos quedamos sin preguntas, colocamos nuevamente las preguntas.

    private void Awake()
    {
        m_backup = m_questionList.ToList(); //ToList(): Forma de copiar una lista a otra nueva.
    }

    public Question GetRandom(bool remove = true) //remove: Para que la pregunta no se repita.
    {
        if(m_questionList.Count == 0)
        {
            RestoreBackup();
        }

        int index = Random.Range(0, m_questionList.Count);
        
        if(!remove)
        {
            return m_questionList[index];
        }

        Question q = m_questionList[index];
        m_questionList.RemoveAt(index);
        return q;
    }

    private void RestoreBackup()
    {
        m_questionList = m_backup.ToList();
    }
}
