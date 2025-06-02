using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Background Stuff")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private float bgSpeed = 0.1f;
    private Vector2 bgOffset;

    [Header("Player Ref")]
    [SerializeField] private Player player;
    [SerializeField] private float _dashValue;

    // Update is called once per frame
    void Update()
    {
        MoveBackGround();
    }

    private void MoveBackGround()
    {
        bgOffset.x += bgSpeed * Time.deltaTime;

        backGround.GetComponent<SpriteRenderer>().material.mainTextureOffset = bgOffset;
    }

    public void PlayerDashPlus()
    {
        player._getSetDashV += _dashValue;
    }

    
}
