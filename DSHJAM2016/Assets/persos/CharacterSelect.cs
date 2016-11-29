﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Inputs;


public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    private Transform[] m_Characters;

    struct pair
    {
        public string pre;
        public string suf;
        public pair(string p, string s)
        {
            pre = p;
            suf = s;
        }
    }

    [SerializeField]
    public Sprite[] numbers;

    private int nbPlayers = 0;
    private static int nbMaxPlayers = 4;
	private int[] slots;

	private List<Player> players;
    private pair[] input;

    private int cpt = 0;
    // Use this for initialization
    void Start()
    {
		players = new List<Player>(nbMaxPlayers); // I hate stacks
		slots = new int[nbMaxPlayers];
		for ( int i = 0; i < nbMaxPlayers;i++ ) // I hate C#
			slots[i] = -1;
    }


    // Update is called once per frame
    void Update()
    {
		// Listen for jumps on JS
		for (int i = 0; i < 4; i++)
			if (Input.GetButtonDown ("JS" + "Jump" + i))
				addPlayerJS (i);

		// Listen for items on JS
		for (int i = 0; i < 4; i++)
			if (Input.GetButtonDown ("JS" + "Item" + i))
				delPlayerJS (i);

	
        if (Input.GetButtonDown("Submit"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("characterSelect", players);

        }
			
    }

	void addPlayerJS(int id=0) {
		// First, check slot status
		if (slots[id] != -1) return;

		// Then create a player with his own JS, shitty gameplay will come soon
		players.Add(new Player(nbPlayers, new LeftHand("JS",id.ToString()), new RightHand("JS",id.ToString())));
		slots [id] = nbPlayers;
		nbPlayers++;
		Debug.Log ("New player with " + Input.GetJoystickNames()[id] + "! Current players : " + nbPlayers);
	}

	void delPlayerJS(int id=0) {
		// First, check slot status
		if (slots[id] == -1) return;

		// Then create a player with his own JS, shitty gameplay will come soon
		players.RemoveAt(slots[id]);
		slots [id] = -1;
		nbPlayers--;
		Debug.Log ("Player abandon :( Current players : " + nbPlayers);
	}
}
