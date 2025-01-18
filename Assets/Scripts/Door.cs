using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        if(player.collectedYellows >= 7)
        {
            Destroy(this.gameObject);
        }
    }
}
