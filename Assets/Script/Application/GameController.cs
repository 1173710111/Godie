using UnityEngine.SceneManagement;
using UnityEngine;

public static class GameController 
{
    private static bool can_move=false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void GameStart()
    {
        Debug.Log("GameStart!");
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

    public static void AnyDownStart()
    {
        //................
        GameObject.Find("TrackCamera").SetActive(false);
        can_move = true;

    }
}
