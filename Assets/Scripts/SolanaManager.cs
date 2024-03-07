using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solana.Unity.SDK;
using Solana.Unity.Wallet;
using TMPro;

public class SolanaManager : MonoBehaviour
{
    public GameObject ConnectPanel;
    public GameObject NicknamePanel;
    public GameObject CharacterSelectionScreen;
    //public PhotonManager photonManager;

    private void OnEnable()
    {
        Web3.OnLogin += OnLogin;
    }


    private void OnDisable()
    {
        Web3.OnLogin -= OnLogin;
    }


    private void OnLogin(Account account)
    {
        PlayFabManager.instance.AuthenticateUser(account.PublicKey);
        PlayFabManager.PublicKey = account.PublicKey;
        ConnectPanel.SetActive(false);
        PhotonManager.instance.Connect();
        NicknamePanel.SetActive(true);
        //photonManager.Connect();
        CharacterSelectionScreen.SetActive(true);
    }
}
