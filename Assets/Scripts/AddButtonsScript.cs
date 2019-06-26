using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonsScript : MonoBehaviour {

    [SerializeField]
    private Transform puzzlefield;

    [SerializeField]
    private GameObject btn;

	void Awake() {

        for (int i = 0; i < 12 ; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzlefield, false);

        }

	}
	
	
}
