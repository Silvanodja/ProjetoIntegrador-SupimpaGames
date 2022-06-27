using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunShot : MonoBehaviourPunCallbacks
{
    Rigidbody2D shot;
    public float speedHorizontal;
    public float speedVertical;
    public float originalSpeed;

    void OnEnable()
    {
        shot = GetComponent<Rigidbody2D>();
        Invoke(nameof(Desactivate), 2);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        shot.velocity = new Vector2(speedHorizontal, speedVertical);
    }

    

    [PunRPC]
    void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
