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

			PlaySfx(shotFX);
		}
	}

	private void PlaySfx(string fx)
	{
		Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
		if (!string.IsNullOrEmpty(fx) &&
			0f <= viewportPosition.x && viewportPosition.x <= 1f &&
			0f <= viewportPosition.y && viewportPosition.y <= 1f)
		{
			FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(fx);
			e.start();
			e.release();
		}
	}
}
