using UnityEngine;

public class BeginMusic : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
    }

}
