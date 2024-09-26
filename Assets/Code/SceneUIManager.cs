using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    public static SceneUIManager Instance;


    #region References

    [SerializeField, HideInInspector] private TextMeshProUGUI _player1TextMesh;
    [SerializeField, HideInInspector] private TextMeshProUGUI _player2TextMesh;

    #endregion

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

    #region Public Methods

    public void UpdatePlayerText(bool player1)
    {
        if (player1 == true)
        {
            _player1TextMesh.text = GameManager.Instance.Player1CollectedItems.ToString();
        }
        else
        {
            _player2TextMesh.text = GameManager.Instance.Player2CollectedItems.ToString();
        }
    }

    #endregion
}
