/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InviteManager : MonoBehaviour
{
    private DBManager DB;

    private UserData user;
    private RoomData room;

    public Text inviteMessage;

    public Button acceptInvite;
    public Button rejectInvite;

    public string otherUserId;

    private void Start()
    {
        DB = DBManager.Instance;
        user = UserData.Instance;
        room = RoomData.Instance;

        acceptInvite.onClick.AddListener(AcceptInvite);
        rejectInvite.onClick.AddListener(RejectInvite);
    }

    void AcceptInvite()
    {
        DB.AcceptInvite(otherUserId);
    }

    void RejectInvite()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }
}
