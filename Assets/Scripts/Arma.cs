using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour
{
	public Bala prefabBala;
	public float velocidade;
	private float tempoUltimoTiro;
	public float intervaloTiro;

	[FMODUnity.EventRef]
	public string shotFX;

	void Start()
	{
		tempoUltimoTiro = 0;
	}
	
	void Update()
	{
		tempoUltimoTiro -= Time.deltaTime;
	}
	
	void Disparar()
	{
		if (tempoUltimoTiro <= 0)
		{
			var bala = Instantiate(prefabBala, transform.position, transform.rotation) as Bala;
			bala.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidade);
			tempoUltimoTiro = intervaloTiro;

			if (!string.IsNullOrEmpty(shotFX))
			{
				FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(shotFX);
				e.start();
				e.release();
			}
		}
	}
}
