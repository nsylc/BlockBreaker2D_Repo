using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip blockBreak;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int timeHit;
    [SerializeField] Sprite[] hitSprites;

    LevelSystem levelSystem;
    GameStatus gameStatus;
    private void Start()
    {
        CountWhat();
    }

    private void CountWhat()
    {
        levelSystem = FindObjectOfType<LevelSystem>();
        if (tag == "Broken")
        {
            levelSystem.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Broken")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timeHit++;
        int maxHit = hitSprites.Length + 1;
        if (timeHit >= maxHit)
        {
            DestroyBlock();
        }
        else
        {
            NextSprite();
        }
    }

    private void NextSprite()
    {
        int spriteIndex = timeHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Array kaçtı " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        DestroySFX(); 
        Destroy(gameObject);
        levelSystem.BlockDestroyed();
        TriggerSparkles();
    }

    private void DestroySFX()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(blockBreak, Camera.main.transform.position);
    }

    private void TriggerSparkles()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
