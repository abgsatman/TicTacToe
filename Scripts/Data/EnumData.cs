/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

public enum Positions
{
    s1,
    s2,
    s3,
    s4,
    s5,
    s6,
    s7,
    s8,
    s9,
    none
}

public enum Symbols
{
    none,
    X,
    O
}

public enum PlayerID
{
    none,
    PlayerA,
    PlayerB
}

public enum GameState
{
    Login,
    Signup,
    Lobby,
    Gameplay,
    Result,
    Transaction
}