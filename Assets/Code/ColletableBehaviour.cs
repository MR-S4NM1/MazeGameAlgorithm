using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletableBehaviour : MonoBehaviour
{
    #region References



    #endregion

    #region Unity Methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerBehaviour>().playerNum == Players.ONE)
            {
                GameManager.Instance.Player1CollectedItems++;
                SceneUIManager.Instance.UpdatePlayerText(true);
            }
            else
            {
                GameManager.Instance.Player2CollectedItems++;
                SceneUIManager.Instance.UpdatePlayerText(false);
            }
            gameObject.SetActive(false);
        }
    }

    #endregion
}
