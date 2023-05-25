using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string sceneName; // Scene name to teleport to

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}