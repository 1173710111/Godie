using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera_man;
    public GameObject camera_dog;
    public GameObject character_man;
    public GameObject character_dog;
    [Tooltip("当前主视角，0表示人，1表示狗子")]
    public int mainCamera;

    private void LookAtDog()
    {
        camera_man.SetActive(false);
        camera_dog.SetActive(true);
        character_man.GetComponent<PlayerActions>().enabled=false;
        character_dog.GetComponent<PlayerActions>().enabled = true;
    }

    private void LookAtMan()
    {
        camera_man.SetActive(true);
        camera_dog.SetActive(false);
        character_man.GetComponent<PlayerActions>().enabled = true;
        character_dog.GetComponent<PlayerActions>().enabled = false;
    }

    private void ChangeCamera()
    {
        if (mainCamera == 0)
        {
            LookAtMan();
        }
        else
        {
            LookAtDog();
        }
    }

    private void Start()
    {
        ChangeCamera();
        Physics2D.IgnoreCollision(character_man.GetComponent<Collider2D>(), character_dog.GetComponent<Collider2D>(), true);
    }
}
