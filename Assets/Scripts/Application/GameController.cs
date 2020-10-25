using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public static class GameController 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void GameStart()
    {
        Debug.Log("GameStart!");
        Win32Help.SetImeEnable(false);
        SceneManager.LoadScene(8);
    }

    public static void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1f;
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
                characterNumber = 1;
                break;
            case 3:
                characterNumber = 1;
                break;
            case 4:
                characterNumber = 2;
                break;
            case 5:
                characterNumber = 2;
                break;
            case 6:
                characterNumber = 2;
                break;
            case 7:
                characterNumber = 2;
                break;
            default:
                break;
        }
        return characterNumber;
    }
}