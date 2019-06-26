﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private String firstGuessPuzzle, secondGuessPuzzle;
    private int firstGuessIndex, secondGuessIndex;

    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/Candy");
    }

    void Start() {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        gameGuesses = gamePuzzles.Count / 2;
        Shuffle(gamePuzzles);
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }

    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for(int i =0; i <looper; i++)
        {
            if (index == looper / 2)
                index = 0;

            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }



    public void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }

    }



    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;


            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];


        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;


            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());

        }

    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        if(firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0);

            CheckIfThePuzzlesMatchTheGameFinished();

        }
        else
        {
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;




    }



    void CheckIfThePuzzlesMatchTheGameFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses== gameGuesses)
        {
            Debug.Log("Finished");
            Debug.Log("Tahmin sayısı:" +countGuesses);

        }
    }


    void Shuffle(List <Sprite> list)
    {
        for(int i=0; i <list.Count; i++ )
        {
            Sprite temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;


        }
    }
}
