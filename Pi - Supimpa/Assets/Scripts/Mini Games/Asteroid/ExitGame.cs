using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void EndMiniGame(GameObject minigame)
    {
        minigame.gameObject.SetActive(false);
    }
}
