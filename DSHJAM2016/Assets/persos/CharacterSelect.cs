﻿using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class CharacterSelect : MonoBehaviour
{

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

    private int nbPlayers = 1;
    private int nbMaxPlayers = 2;

    private Stack players;
    private pair[] input;
     
    // Use this for initialization
    void Start()
    {
        players = new Stack();
        players.Push(new Player(0,generateInputs()));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            addPlayer();
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            delPlayer();


        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("characterSelect", players);
            
        }
            



    }

    void addPlayer()
    {

        if (nbPlayers < nbMaxPlayers)
        {
            nbPlayers++;
            GetComponent<SpriteRenderer>().sprite = numbers[nbPlayers - 1];
            players.Push(new Player(0,generateInputs()));
        }
    }

    void delPlayer()
    {
        if (nbPlayers > 1)
        {
            nbPlayers--;
            GetComponent<SpriteRenderer>().sprite = numbers[nbPlayers - 1];
            players.Pop();
        }
    }

    private string generateInputs()
    {
        return "KB" + "," +((int)(Random.value * 4)).ToString();
    }
}