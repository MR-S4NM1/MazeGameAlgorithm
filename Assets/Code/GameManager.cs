using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    #region Runtime Variables

    private int _player1CollectedItems;
    private int _player2CollectedItems;

    #endregion

    #region Getters And Setters

    public int Player1CollectedItems
    {
        get { return _player1CollectedItems; }
        set { _player1CollectedItems = value; }
    }
    public int Player2CollectedItems
    {
        get { return _player2CollectedItems; }
        set { _player2CollectedItems = value; }
    }
    #endregion
}
