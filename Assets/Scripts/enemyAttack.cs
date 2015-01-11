﻿using UnityEngine;
using System.Collections;

public class enemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	GameObject player;
	playerHP playerHealth;
	//EnemyHealth enemyHealth;
	bool playerInRange;
	float timer;
	
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <playerHP> ();
		//enemyHealth = GetComponent<EnemyHealth>();
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
		{
			animation.CrossFade("Attack");
			Attack ();
		}

		if(playerHealth.currentHealth <= 0)
		{
			animation.CrossFade("Death");
		}
	}
	
	
	void Attack ()
	{
		timer = 0f;
		
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}
}