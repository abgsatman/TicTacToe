using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transaction : MonoBehaviour
{
    private DBManager DB;
    private AuthManager auth;

    public Button readyButton;

    private UserData user;
    private RoomData room;

    public Text playerAReadyText;
    public Text playerBReadyText;

    public Text noticeText;

    public GameObject inviteObject;

    private void Start()
    {
        DB = DBManager.Instance;
        auth = AuthManager.Instance;

        user = UserData.Instance;
        room = RoomData.Instance;

        user.gameState = GameState.Transaction;
        DB.SetReady();

        readyButton.onClick.AddListener(DoReady);

        StartCoroutine(CheckReadyStatus());
    }

    void DoReady()
    {
        DB.SetReady();
    }

    IEnumerator CheckReadyStatus()
    {
        yield return new WaitUntil(() => room.PlayerAReady && room.PlayerBReady);
        noticeText.text = "Oyun yükleniyor..... Lütfen bekleyin...";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Gameplay");
    }
}
