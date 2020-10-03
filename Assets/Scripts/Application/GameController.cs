using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController 
{
    private static bool can_move=true;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void GameStart()
    {
        Debug.Log("GameStart!");
        SceneManager.LoadScene(2);
    }

    public static void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1f;
    }

    public static bool GetCanMove()
    {
        return can_move;
    }

    public static void SetCanMove(bool canMove)
    {
        can_move = canMove;
    }

    public static int GetCharacterNumber()
    {
        int characterNumber=0;
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                characterNumber = 1;
                break;
            case 2:
                characterNumber = 2;
                break;
            default:
                break;
        }
        return characterNumber;
    }

    
}
