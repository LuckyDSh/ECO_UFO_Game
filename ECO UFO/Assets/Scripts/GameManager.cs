/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
