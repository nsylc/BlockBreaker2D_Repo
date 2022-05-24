using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] int breakableBlocks;

    Scenes scenes;


    private void Start()
    {
        scenes = FindObjectOfType<Scenes>(); 
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            scenes.LoadNextScene();
        }
               
    }
}
