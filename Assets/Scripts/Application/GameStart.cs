using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public float wait_time = 3f;
    public GameObject anyDownStart;
    public GameObject leftButton;
    public GameObject rightButton;
    public Transform playerStartPosition;
    public GameObject player;

    private bool waitFinished = false;
    private GameObject newAnyDownStart;
    private GameObject newLeftButton;
    private GameObject newRightButton;
    private void Awake()
    {
        Invoke("CanStart", wait_time);
    }

    private void CanStart()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.Find("TrackCamera").SetActive(false);
            newAnyDownStart = Instantiate(anyDownStart, new Vector3(playerStartPosition.position.x, playerStartPosition.position.y -2f, playerStartPosition.position.z), Quaternion.identity);
            }
        waitFinished = true;
    }
    private void LateUpdate()
    {
        if (waitFinished)
        {
            InputController.GetKey();
            if (InputController.anyDown)
            {
                Destroy(newAnyDownStart);
                waitFinished = false;
                Invoke("CanMove", 0.2f);
            }
        }
    }
    private void CanMove()
    {
        GameController.SetCanMove(true);
        leftButton = Instantiate(leftButton, new Vector3(playerStartPosition.position.x - 1.5f, playerStartPosition.position.y - 2f, playerStartPosition.position.z), Quaternion.identity);
        rightButton = Instantiate(rightButton, new Vector3(playerStartPosition.position.x + 1.5f, playerStartPosition.position.y - 2f, playerStartPosition.position.z), Quaternion.identity);
        leftButton.transform.parent = player.transform;
        rightButton.transform.parent = player.transform;
    }
}
