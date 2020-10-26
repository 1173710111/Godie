using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public float wait_time = 3f;
    public GameObject anyDownStart;
    public Transform playerStartPosition;
    public GameObject player;
    public GameObject trackCamera;

    private bool waitFinished = false;
    private GameObject newAnyDownStart;
    private void Awake()
    {
        InputController.BanButton(true);
        InputController.BanMouse(true);
        Invoke("CanStart", wait_time);
    }

    private void CanStart()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            trackCamera.SetActive(false);
            newAnyDownStart = Instantiate(anyDownStart, new Vector3(playerStartPosition.position.x, playerStartPosition.position.y -2f, playerStartPosition.position.z), Quaternion.identity);
            Invoke("CanMove", 0.2f);
            waitFinished = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            StartCoroutine(DelayToInvoke.DelayToInvokeDo(() => 
            {
                trackCamera.SetActive(false);
                Invoke("CanMove", 0.2f);
                waitFinished = true;
            }, 2f));
        }
    }
    private void LateUpdate()
    {
        if (waitFinished&& SceneManager.GetActiveScene().buildIndex == 1)
        {
            InputController.GetKey();
            if (InputController.anyDown)
            {
                Destroy(newAnyDownStart);
                waitFinished = false;
            }
        }
    }
    void CanMove()
    {
        InputController.BanButton(false);
        InputController.BanMouse(false);
        
    }
}
