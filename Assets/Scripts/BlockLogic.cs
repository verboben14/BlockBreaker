using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip blockAudioClip;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    int maximumHits;

    // state variables
    [SerializeField] int timesHit = 0;   // only serialized for debugging
    bool isInDisappearing;

    // cached reference
    LevelLogic level;
    SpriteRenderer mySpriteRenderer;


    private void Start()
    {
        CountBreakableBlocks();
        maximumHits = hitSprites.Length + 1;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (tag == "Disappearing")
        {
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear()
    {
        if (!isInDisappearing)
        {
            isInDisappearing = true;
            yield return new WaitForSeconds(0.2f);
            mySpriteRenderer.enabled = false;
            yield return new WaitForSeconds(1f);
            mySpriteRenderer.enabled = true;
            isInDisappearing = false;
        }
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<LevelLogic>();
        if (IsBreakable())
        {
            level.AddBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBreakable())
        {
            HandleHit();
        }
    }
    
    private bool IsBreakable()
    {
        return tag == "Breakable" || tag == "Disappearing";
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maximumHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites != null && hitSprites.Length > spriteIndex && hitSprites[spriteIndex] != null)
        {
            mySpriteRenderer.sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array for " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSessionLogic>().AddPointsPerBlockDestroyedToPoint();
        AudioSource.PlayClipAtPoint(blockAudioClip, Camera.main.transform.position);
        TriggerSparklesVFX();
        level.RemoveBreakableBlock();
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
