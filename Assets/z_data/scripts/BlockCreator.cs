using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] BlockHolder blockHolderPrefab;
    [SerializeField] GameObject dynamicObjects;
    [SerializeField] Material blockMaterial;

    [Header("Color Palette")]
    [SerializeField] Color[] colorPaletteOne;

    [SerializeField] float blocksOnColumn = 20f;
    [SerializeField] float blocksYOffset = 0f;
    [SerializeField] float blockRotateAmt = 0f;

    List<BlockHolder> allBlockHolders;

    int blocksOnRow = 12;

    // Start is called before the first frame update
    void Start()
    {
        CreateBlocks();
        ApplyColors();
        RotateBlocks();
    }

    // void Update()
    // {
    //     for (int i = 0; i < allBlockHolders.Count; i++)
    //     {
    //         allBlockHolders[i].transform.position = new Vector3(0f, i * (blocksYOffset), 0f);
    //     }
    // }

    void RotateBlocks()
    {
        for (int i = 0; i < allBlockHolders.Count; i += 2)
        {
            allBlockHolders[i].transform.rotation = Quaternion.Euler(0f, blockRotateAmt, 0f);
        }
    }

    void ApplyColors()
    {
        for (int i = 0; i < blocksOnRow; i++)
        {
            for (int j = 0; j < blocksOnColumn; j++)
            {
                BlockHolder bh = allBlockHolders[j];
                Color randomColor = colorPaletteOne[Random.Range(0, colorPaletteOne.Length)];
                bh.allblocks[i].Setup(blockMaterial, randomColor);
            }

        }
    }

    void CreateBlocks()
    {
        allBlockHolders = new List<BlockHolder>();
        GameObject blocksWrap = new GameObject();
        for (int i = 0; i < blocksOnColumn; i++)
        {
            Vector3 newYpos = new Vector3(0f, i * blocksYOffset, 0f);
            BlockHolder bh = Instantiate(blockHolderPrefab, newYpos, Quaternion.identity);
            bh.transform.parent = blocksWrap.transform;
            allBlockHolders.Add(bh);
        }
        blocksWrap.transform.position = transform.position;
        blocksWrap.transform.parent = dynamicObjects.transform;
    }

    // void CreateRowBlocks()
    // {
    //     int blocksOnRow = 12;
    //     float baseRadius = 3f;
    //     for (int i = 0; i < blocksOnRow; i++)
    //     {
    //         float angle = i * Mathf.PI * 2f / blocksOnRow;
    //         float yPos = 1f * (transform.position.y + blockYOffset);
    //         Vector3 newPos = new Vector3(Mathf.Cos(angle) * baseRadius, yPos, Mathf.Sin(angle) * baseRadius);
    //         Block block = Instantiate(blockPrefab, newPos, Quaternion.identity).GetComponent<Block>();
    //         block.transform.parent = dynamicObjects.transform;
    //     }
    // }
}