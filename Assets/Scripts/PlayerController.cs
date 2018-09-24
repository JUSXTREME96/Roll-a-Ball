using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text scoreText;
	public Text countText;
	public Text winText;
	public Vector3 position;

	private Rigidbody rb;
	public int count;
	public int score;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		score = 0;
		SetAllText ();
		SetScoreText ();
		winText.text = "";
	}
	private void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float colorR = Mathf.Abs (transform.position.x / 10);
		float colorG = Mathf.Abs (transform.position.y / 10);
		float colorB = Mathf.Abs (transform.position.z / 10);

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		Color colornow = new Vector4(colorR, colorG, colorB, 0.0f);
		print (transform.position);

		GetComponent<Renderer>().material.color = colornow;
		rb.AddForce (movement * speed);
		if (Input.GetKey("escape"))
			Application.Quit();
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			score = score + 1;
			SetAllText ();
			SetScoreText ();
			if (count == 12) 
			{
				transform.position = new Vector3 (36.0f,transform.position.y,3.0f);
			}
		}
		else if (other.gameObject.CompareTag ("BadPickUp")) 
		{
			other.gameObject.SetActive (false);
			score = score - 1;
			SetScoreText ();
		}
	}

	void SetAllText ()
	{
		
		countText.text = "COUNT: " + count.ToString ();
		if (count >= 24)
		{
			winText.text = "You won the game with a score of: " + score.ToString();
		}
	}
	void SetScoreText ()
	{
		scoreText.text = "SCORE: " + score.ToString ();
	}
}
