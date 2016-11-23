using UnityEngine;
using System.Collections;

public class Jogador : MonoBehaviour
{
	private Rigidbody2D _rb;
	private float _input;
	public float aceleracao;
	public float maxVelocidade;

	[FMODUnity.EventRef]
	public string explosionFx;

	[FMODUnity.EventRef]
	public string rockHitFx;
	
	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		_input = Input.GetAxis("Horizontal");
	}
	
	void FixedUpdate()
	{
		if (_input < 0 && _rb.velocity.x > 0)
		{
			_rb.velocity = Vector2.zero;
		} else if (_input > 0 && _rb.velocity.x < 0)
		{
			_rb.velocity = Vector2.zero;
		}
		_rb.AddForce(new Vector2(_input * aceleracao, 0), ForceMode2D.Force);
		_rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxVelocidade);
	}
	
	void OnTriggerEnter2D(Collider2D outro)
	{
		if (outro.CompareTag("Bala") || outro.CompareTag("NaveInimigo"))
		{
			PlaySfx(explosionFx);
			Destroy(gameObject);
			Destroy(outro.gameObject);
			SpaceShooter.AtivarGameOver();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Parede"))
		{
			PlaySfx(rockHitFx);
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
