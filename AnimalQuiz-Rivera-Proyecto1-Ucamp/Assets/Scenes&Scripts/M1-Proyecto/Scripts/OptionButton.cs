using System; //Para utilizar el Action en la función Build
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] //RequireComponet: Obliga al objeto que tenga un componente button.
[RequireComponent(typeof(Image))]

public class OptionButton : MonoBehaviour
{
	private Text m_text = null;
	private Button m_button = null;
	private Image m_image = null;
	private Color m_originalColor = Color.black;
	
	
	public Option Option {get; set;} 
	
	private void Awake()
	{
		m_button = GetComponent<Button>();
		m_image = GetComponent<Image>();
		m_text = transform.GetChild(0).GetComponent<Text>();
		m_originalColor = m_image.color;
	}
	
	public void Build(Option option, Action<OptionButton> callback)
	{
		m_text.text = option.text;//Colocamos el texto de la opciòn para que el jugador sepa que opciòn esta seleccionando.
		m_button.onClick.RemoveAllListeners();
		m_button.enabled = true;
		m_image.color = m_originalColor;
		Option = option;
		
		m_button.onClick.AddListener(delegate
		{
			callback(this);
		});
	}
	
	public void SetColor(Color color)//Color dependiendo si es correcto o incorrecto.
	{
		m_button.enabled = false;//Desabilitamos el boton para que no controle el color de la imagen.
		m_image.color = color;
		
	}
}
