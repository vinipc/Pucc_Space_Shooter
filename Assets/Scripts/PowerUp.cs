using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	public float velocidade;
	public int pontos = 1;
	public float timeOut = 15;

	[FMODUnity.EventRef]
	public string pickUpFX;
	
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, -SpaceShooter.VelocidadeBase());
		Destroy(gameObject, timeOut);
	}
	
	void OnTriggerEnter2D(Collider2D outro)
	{
		if (outro.CompareTag("NaveJogador"))
		{
			PlaySfx(pickUpFX);
			SpaceShooter.AdicionarPontos(pontos);
			Destroy(gameObject);
		}
	}

	private void PlaySfx(string sfx)
	{
		Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
		if (!string.IsNullOrEmpty(sfx) &&
			0f <= viewportPosition.x && viewportPosition.x <= 1f &&
			0f <= viewportPosition.y && viewportPosition.y <= 1f)
		{
			FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(sfx);
			e.start();
			e.release();
		}
	}
}