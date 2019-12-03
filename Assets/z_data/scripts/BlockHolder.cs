using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolder : MonoBehaviour
{
    public int id;
    public List<Block> allblocks;

    public void TurnOffAll()
    {
        foreach (Block b in allblocks)
        {
            b.TurnOff();
        }
    }

    public void TurnOnAll()
    {
        foreach (Block b in allblocks)
        {
            b.TurnOn();
        }
    }
}
