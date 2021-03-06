﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHP : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public AudioClip hurt1;
	public AudioClip hurt2;
	public AudioClip hurt3;
	public AudioClip healed;
	
	
	CharacterController cc;
	MouseLook ms;
	bool isDead;
	bool damaged;
	int hurtSound = 0;
	
	
	void Awake ()
	{
		currentHealth = startingHealth;
		cc = GetComponent <CharacterController> ();
		ms = GetComponent <MouseLook> ();
	}
	
	
	void Update ()
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void Heal (int amount)
	{
			currentHealth += amount;
			healthSlider.value = currentHealth;
			audio.PlayOneShot (healed);
			
			if (currentHealth > startingHealth) {
				currentHealth = startingHealth;
			}
		
	}

	public void TakeDamage (int amount)
	{
		damaged = true;
		
		currentHealth -= amount;
		
		healthSlider.value = currentHealth;

		hurtSound += 1;

		if(hurtSound%3==0){
			audio.PlayOneShot(hurt1);
		}else if(hurtSound%2==0){
			audio.PlayOneShot(hurt2);
		}else{
			audio.PlayOneShot(hurt3);
		}
		
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}
	
	
	void Death ()
	{
		isDead = true;
		audio.PlayOneShot (deathClip);


		cc.enabled = false;
		ms.sensitivityX = 0;
		ms.sensitivityY = 0;

		GameObject manager = GameObject.Find ("_manager");
		manager.GetComponent<gameManager> ().gameOver ();

	}
}
