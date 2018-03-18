using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public enum Estado {Afuera, EnArea, Lejos, Sintoniza}; 

	AudioSource ambiente;

	AudioSource efectos;

	public AudioClip[] sonidos;

	private Estado estadoActual;

	private bool rango;

	private bool cumbion;

	// Use this for initialization
	void Start () {
		ambiente = GetComponent<AudioSource>();
		efectos = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
		estadoActual = Estado.Afuera;
		ambiente.clip = sonidos[0];
		ambiente.Play();
		rango = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void marcarRango(bool rang, bool cum)
	{
		rango = rang;
		cumbion = cum;
	}
	public void entraEnArea()
	{
		estadoActual = Estado.EnArea;
		efectos.clip = sonidos[1];
		efectos.Play();
	}

	public void saleDeArea()
	{
		estadoActual = Estado.EnArea;
		efectos.Pause();
	}

	public void reproducir()
	{
		if(!rango)
		{
			estadoActual = Estado.Lejos;
			efectos.clip = sonidos[2];
			efectos.Play();
		}
		else
		{
			efectos.clip = sonidos[3];
			if(cumbion)
			{
				efectos.clip = sonidos[5];
			}
			estadoActual = Estado.Sintoniza;
			efectos.Play();
		}
	}

	public void cambiarVolumen(float vol)
	{
		efectos.volume = vol;
	}
}
