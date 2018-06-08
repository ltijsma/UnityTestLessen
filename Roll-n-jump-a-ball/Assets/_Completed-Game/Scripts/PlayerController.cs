using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed
	public float speed;


	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;
	}

	void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);


		/*
			GetAxis gebruik je wanneer je input nodig hebt die je kunt vergelijken met een as van een Joystick.
			GetButton gebruik je wanneer je daadwerkelijk een knop wilt indrukken
			Met GetButtonDown registreert alleen een input op het moment dat de speler de knop indrukt, dus niet 
			als de speler de knop ingedrukt houdt.

			De String "Jump" is standaard gedefinieerd in de Input Manager van Unity en deze kun je vinden 
			het volgende menu:
			Edit > Project Settings > Input

			Als je goed test, dan merk je dat de bal soms niet springt en soms hoger. Dat komt doordat FixedUpdate 
			soms niet wordt uitgevoerd en soms vaker in één iteratie door de game loop
			(en hier heb je als Unity programmeur nauwelijks controle over). 
			
			Als je deze code in Update zet, dan los je het eerste probleem op, maar dan zal de speler soms
			nog steeds hoger springen. Dit kun je dan weer oplossen met een extra controle in 
			FixedUpdate. Maar dit alles lijkt me veel te ingewikkeld voor deze opdracht, dus laten we het maar
			houden bij deze oplossing ;).
		 */
		if (Input.GetButtonDown("Jump")) {
			
			/*
				Oplossing 1:
				ForceMode.Impulse gebruik voor een kracht die je instantaan wel toepassen en daarna niet meer, 
				zoals het afvuren van een kogel of, in dit geval, springen.
				In feite zorg je er hiermee voor dat je de snelheid aan direct aanpast ipv een versnelling
				toe te passen (zie de natuurkundige definitie van impulse). 
				Het getal 5 levert een sprong op die goed voelt tijdens het spelen :)
			*/
			rb.AddForce(Vector3.up * 5, ForceMode.Impulse); 

			/*
				Oplossing 2:
				Waarschijnlijk komt niet iedereen daarop. Het is wel mogelijk om het op deze manier te doen
				Alleen moet je de kracht dan vermenigvuldigen met een groter getal om de de sprong te zien.
				Dit getal heb ik afgeleid met de volgende formule: a = v / t (dus a = 5 / 0.02). Hierbij is t
				de tijdsduur van een FixedUpdate frame.
 			*/
			//rb.AddForce(Vector3.up * 250); 
			
			/*
				Oplossing 3:
				Ik vind zelf die shortcuts van Vector3 (Vector3.up, Vector3.right, ect) erg comfortabel omdat ze 
				precies aangeven welke richting je bedoelt. Maar het kan natuurlijk ook zoals hieronder.
			 */
			// rb.AddForce(new Vector3(0, 250, 0)); 


			/*
				Vermenigvuldig in AddForce de vector die je meegeeft niet met Time.deltaTime.
				Doe dit bijvoorbeeld niet: rb.AddForce(Vector3.up * 250 * Time.deltaTime). 
				Als je AddForce gebruikt laat je Unity de nieuwe positie berekenen op basis van de krachten die je 
				meegeeft en bij deze berekening gebruikt Unity al de waarde van Time.deltaTime.
			 */

		}
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;
	
		}
	}

	
}