using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(AudioSource))]

//GameManager: Dicta el control del juegos, cual es la siguiente pregunta, cuando se termina el juego, etc.
public class GameManager : MonoBehaviour
{
    [SerializeField] static int puntajeTotal = 0;
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 1.0f;
    [SerializeField] private TextMeshProUGUI numeroPregunta;
    private static float iFinish = 0;
    private QuizDB m_quizDB = null;
    private QuizUI m_quizUI = null;
    private AudioSource m_audioSource = null;


    //Mensaje puntaje final
    public string textValue;
    public TextMeshProUGUI textElement;

    private void Start()
    {
        m_quizDB = GameObject.FindObjectOfType<QuizDB>();
        m_quizUI = GameObject.FindObjectOfType<QuizUI>();
        m_audioSource = GameObject.FindObjectOfType<AudioSource>();
        
        NextQuestion();
    }

    private void Update()
    {
        FinalGame();
    }

    private void NextQuestion()
    {
        m_quizUI.Construtc(m_quizDB.GetRandom(), GiveAnswer); 
    }

    private void GiveAnswer(OptionButton optionButton) //Cuando el jugador seleccione una respuesta
    {
        StartCoroutine(GiveAnswerRoutine(optionButton)  );
    }

    private IEnumerator GiveAnswerRoutine(OptionButton optionButton)//Pintar la opci�n dependiendo si es correcto.
    {
        if (m_audioSource.isPlaying) //Paramos cualquier sonido
            m_audioSource.Stop();

        optionButton.SetColor(optionButton.Option.correct ? m_correctColor : m_incorrectColor);
        m_audioSource.Play();
        yield return new WaitForSeconds(m_waitTime);

        if(optionButton.Option.correct)
        {
            puntajeTotal += 10;
            FinishGame();
            NextQuestion();
        }
        else
        {
            FinishGame();
            NextQuestion();
        }

    }

    public void FinishGame()
    {
        iFinish += 1;
        float valorText = iFinish + 1;
        numeroPregunta.text = "Pregunta: " + valorText.ToString() + "/10";
        if (iFinish == 10)
        {
            //Mostrar pantalla final y puntaje
            SceneManager.LoadScene(2);
        }
    }

    public void FinalGame()
    {
        textElement.SetText(puntajeTotal.ToString());
    }

    public void CinematicGame()
    {
        SceneManager.LoadScene(0);
        puntajeTotal = 0;
        iFinish = 0;
    }
}
