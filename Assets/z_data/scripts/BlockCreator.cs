using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] BlockHolder blockHolderPrefab;
    [SerializeField] GameObject dynamicObjects;
    [SerializeField] Material blockMaterial;
    [SerializeField] Transform boxCasterTr;
    [SerializeField] LayerMask boxCastLayerMask;
    [SerializeField] Transform blockEnablerTr;

    [Header("Color Palette")]
    [SerializeField] Color[] colorPaletteOne;
    [SerializeField] Color[] colorPaletteTwo;

    [SerializeField] int blocksOnColumn = 20;
    [SerializeField] float blocksYOffset = 0f;
    [SerializeField] float blockRotateAmt = 0f;

    List<BlockHolder> allBlockHolders;
    Color[] activeColorPalette;
    int blocksOnRow = 12;
    int defaultBlockEnableCount = 8;
    float maxDistance = 80f;
    bool scanAndEnableBlocks = false;

    public void Setup()
    {
        SetActiveColorPalatte();
        scanAndEnableBlocks = false;
        CreateBlocks();
        ApplyColors();
        RotateBlocks();
    }

    void Update()
    {
        if (scanAndEnableBlocks)
        {
            RaycastHit hit;
            bool isHit = Physics.BoxCast(boxCasterTr.position, (boxCasterTr.lossyScale) / 2, -transform.up, out hit,
                boxCasterTr.rotation, maxDistance, boxCastLayerMask);

            if (isHit)
            {
                if (hit.transform.position.y < blockEnablerTr.position.y)
                    blockEnablerTr.position = new Vector3(0f, hit.transform.position.y, 0f);
            }
        }
    }

    // void OnDrawGizmos()
    // {
    //     if (scanAndEnableBlocks)
    //     {
    //         RaycastHit hit;

    //         bool isHit = Physics.BoxCast(boxCasterTr.position, (boxCasterTr.lossyScale) / 2, -transform.up, out hit,
    //             boxCasterTr.rotation, maxDistance, boxCastLayerMask);
    //         if (isHit)
    //         {
    //             Gizmos.color = Color.red;
    //             Gizmos.DrawRay(boxCasterTr.position, -boxCasterTr.up * hit.distance);
    //             Gizmos.DrawWireCube(boxCasterTr.position + -boxCasterTr.up * hit.distance, boxCasterTr.lossyScale);
    //             blockEnablerTr.position = new Vector3(0f, hit.transform.position.y, 0f);
    //         }
    //         else
    //         {
    //             Gizmos.color = Color.green;
    //             Gizmos.DrawRay(boxCasterTr.position, -boxCasterTr.up * maxDistance);
    //         }
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
                Color randomColor = activeColorPalette[Random.Range(0, activeColorPalette.Length)];
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
            bh.id = i;
            bh.gameObject.name = "bh_" + i;
            allBlockHolders.Add(bh);
        }
        blocksWrap.transform.position = transform.position;
        blocksWrap.transform.parent = dynamicObjects.transform;
    }

    public void CamSetupCompleted()
    {
        TurnOnAllBlocks();
        TurnOffBlocks(0, blocksOnColumn - 8);

        StopAllCoroutines();
        StartCoroutine(StartScanAndEnableBlocks());
    }

    IEnumerator StartScanAndEnableBlocks()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            scanAndEnableBlocks = true;
            yield return new WaitForSeconds(0.1f);
            scanAndEnableBlocks = false;
        }
    }

    public void TurnOffBlocks(int _from, int _to)
    {
        for (int i = _from; i < _to; i++)
        {
            BlockHolder bh = allBlockHolders[i];
            foreach (Block b in bh.allblocks)
            {
                b.TurnOff();
            }
        }
    }

    public void TurnOnAllBlocks()
    {
        foreach (BlockHolder bh in allBlockHolders)
        {
            bh.TurnOnAll();
        }
    }

    public void TurnOffAllBlocks()
    {
        foreach (BlockHolder bh in allBlockHolders)
        {
            bh.TurnOffAll();
        }
    }

    public int GetBlocksOnColumn()
    {
        return blocksOnColumn;
    }

    public int GetDefaultBlockEnableCount()
    {
        return defaultBlockEnableCount;
    }

    public Material GetBlockMaterial()
    {
        return blockMaterial;
    }

    public Block GetTheTopBlock()
    {
        Block returnBlock = null;

        foreach (BlockHolder bh in allBlockHolders)
        {
            foreach (Block block in bh.allblocks)
            {
                if (block.isOn)
                {
                    if (returnBlock != null)
                    {
                        if (block.transform.position.y > returnBlock.transform.position.y)
                            returnBlock = block;
                    }
                    else
                    {
                        returnBlock = block;
                    }
                }
            }
        }
        return returnBlock;
    }

    public Color[] GetActiveColorPalette()
    {
        return activeColorPalette;
    }

    void SetActiveColorPalatte()
    {
        if (GameManager.Instance.levelIndex == 0)
            activeColorPalette = colorPaletteOne;
        if (GameManager.Instance.levelIndex == 1)
            activeColorPalette = colorPaletteTwo;
        else
            activeColorPalette = colorPaletteOne;
    }

    // void Update()
    // {
    //     for (int i = 0; i < allBlockHolders.Count; i++)
    //     {
    //         allBlockHolders[i].transform.position = new Vector3(0f, i * (blocksYOffset), 0f);
    //     }
    // }

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