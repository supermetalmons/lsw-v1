using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public PhotonView photonView;
    public int coins;
    private PlayFabManager playFabManager;


    void Start()
    {
        string Nickname = PlayerPrefs.GetString("Nickname","Player");
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = Nickname;
        playFabManager = FindObjectOfType<PlayFabManager>();

        if (photonView.IsMine)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<PlayerMovement>().enabled = true;
            coins = PlayerPrefs.GetInt("coins",0);
        } else
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}