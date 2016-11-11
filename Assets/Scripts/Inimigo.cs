using UnityEngine;
using System.Collections;

public class Inimigo : MonoBehaviour
{
	private Rigidbody2D _rb;
	private SpriteRenderer _rend;
	public float timeOut;

	[FMODUnity.EventRef]
	public string explosionFx;
	
	[Range(0, 1)]
	public int
		tipo = 0;
	public InimigoInfo[] infos;
	
	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_rend = GetComponentInChildren<SpriteRenderer>();
		_rend.sprite = infos [tipo].visual;
		Destroy(gameObject, timeOut);
	}
	
	void FixedUpdate()
	{
		var velocidade = new Vector2(infos [tipo].velocidadeX, -infos [tipo].velocidadeY) * SpaceShooter.VelocidadeBase();
		_rb.velocity = new Vector2(
			Mathf.Sin(Time.realtimeSinceStartup * infos [tipo].saude) * velocidade.x,
			velocidade.y);
	}
	
	void OnTriggerEnter2D(Collider2D outro)
	{
		if (outro.CompareTag("Bala"))
		{
			PlaySfx(explosionFx);
			Destroy(outro.gameObject);
			Destroy(gameObject);
			SpaceShooter.CriarPowerUp(transform.position);
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
