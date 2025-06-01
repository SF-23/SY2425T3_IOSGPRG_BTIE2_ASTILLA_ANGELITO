using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Background Stuff")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private float bgSpeed = 0.1f;
    private Vector2 bgOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
