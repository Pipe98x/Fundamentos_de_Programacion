﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ecosistema : MonoBehaviour {

	public GameObject[] plataformasprefab;  // arreglo que contiene las secciones 
	private Transform jugadorTransform;
	private float spawn = 0.0f;  
	private float distancia = 18.52f; // a que distancia debe aparecer la siguiente seccion
	private int cantidad = 5; // cuantas secciones habran al tiempo
	private float zonasegura = 20.0f; // a que distancia (relativa al jugador) debe agregarse y eliminarse una seccion
	private int randomprefab = 0; 
	private List<GameObject> plataformasactivas; // lista para almacenar las secciones que esten activas


	// Use this for initialization
	void Start () {

		plataformasactivas = new List<GameObject> (); 
		jugadorTransform = GameObject.FindGameObjectWithTag ("Player").transform; // defir la variable como los componentes "transform" del jugador

		for (int i = 0; i < cantidad; i++) {    // un ciclo for que generara infinitas plataformas
			if (i < 3) {						// nos asegurara que las dos primeras plataformas seran la plataforma vacia
				generarPlataformas (0);
			} else {
				generarPlataformas ();			// generara la plataforma al azar con la funcion "generarPlataformas"
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (jugadorTransform.position.z - zonasegura > (spawn - cantidad * distancia)) {  // con este if definimos cuando se agregará y eliminara una seccion

			generarPlataformas (randomprefab);  // agrega una seccion al azar
			borrarPlataforma ();				// elimina la primera seccion de la lista
		}
	}

	private void generarPlataformas(int prefabindex = -1) // funcion para crear secciones
	{
		GameObject go;
		go = Instantiate (plataformasprefab[randomindex()]) as GameObject; // crea la plataforma como un gameobject
		go.transform.position= (new Vector3 (-13,-1,1*spawn));				// le da los valores del "transform" que tenga el padre (en este caso el padre es el objeto "manager")						// en que lugar de z lo generara
		spawn+= distancia;													// se le suma la distancia para que la siguiente seccion se genere adelante de la ultima
		plataformasactivas.Add (go);										// se agrega la seccion creada a la lista
	}

	private void borrarPlataforma()   // funcion para borrar secciones
	{
		Destroy (plataformasactivas [0]);  // se destruye la seccion que se encuentre en el indice 0 de la lista 
		plataformasactivas.RemoveAt (0);	// se remueve el indice 0 de la lista
	}

	private int randomindex()   // funcion para generar numeros aleatorios segun la cantidad de elementos que contenga el arreglo
	{
		if (plataformasactivas.Count <= 1) {  // esto para que las dos primeras secciones en crearse esten vacias
			return 0;
		} else {
			randomprefab = Random.Range (0, plataformasprefab.Length); // numero random entre 0 y la cantidad de elementos de el arreglo
			return randomprefab;
		}

	}
}
