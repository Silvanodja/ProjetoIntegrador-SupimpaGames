using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    Rigidbody2D shot;
    public float speed;
    public float originalSpeed;

    void OnEnable()
    {
        shot = GetComponent<Rigidbody2D>();
        Invoke(nameof(Deactivate), 2);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        shot.velocity = new Vector2(speed, 0);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
