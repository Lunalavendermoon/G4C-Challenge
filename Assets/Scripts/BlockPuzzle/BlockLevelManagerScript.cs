using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class BlockLevelManagerScript : MonoBehaviour
{
    public GameObject blockPrefab;

    Dictionary<int, GameObject> blocks = new Dictionary<int, GameObject>();

    int selectedBlock = -1;

    int size = 0;

    int[] groupCounts = {0,0,0};

    void Awake()
    {
        // TODO spawn the actually correct amount/type of blocks lmao
        for (int i = 0; i < 5; ++i) {
            spawnBlock(i, i % 3 == 0 ? BlockType.apple() : BlockType.rice());
        }
    }

    void initManager(int levelSize, int[] levelGroupCounts) {
        // this will be called from the central static class
        // TODO spawn blocks in here
        // also init other stuff
    }
    
    void spawnBlock(int id, BlockType type) {
        GameObject block = Instantiate(blockPrefab, new Vector3(id, 0, 0), Quaternion.identity);
        block.GetComponent<BlockScript>().initBlock(type, this);
        blocks.Add(id, block);
    }

    BlockScript getBlockScript(int id) {
        GameObject block;
        blocks.TryGetValue(selectedBlock, out block);
        return block.GetComponent<BlockScript>();
    }

    public void playerAddBlock(int id) {
        BlockScript block = getBlockScript(id);
        size += block.blockType.size;
        switch (block.blockType.foodGroup) {
            case "veg":
                ++groupCounts[0];
                break;
            case "carb":
                ++groupCounts[1];
                break;
            default:
                ++groupCounts[2];
                break;
        }
    }

    public void playerRemoveBlock(int id) {
        BlockScript block = getBlockScript(id);
        size -= block.blockType.size;
        switch (block.blockType.foodGroup) {
            case "veg":
                --groupCounts[0];
                break;
            case "carb":
                --groupCounts[1];
                break;
            default:
                --groupCounts[2];
                break;
        }
    }

    public void selectBlock(int id) {
        selectedBlock = id;
    }

    public void flipBlock(bool isHorizontal) {
        if (selectedBlock < 0) {
            return;
        }
        getBlockScript(selectedBlock).flip(isHorizontal);
    }

    public void rotateBlock(bool isClockwise) {
        if (selectedBlock < 0) {
            return;
        }
        getBlockScript(selectedBlock).rotate(isClockwise);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
