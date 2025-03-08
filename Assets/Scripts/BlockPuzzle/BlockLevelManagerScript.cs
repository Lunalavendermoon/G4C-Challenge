using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class BlockLevelManagerScript : MonoBehaviour
{
    public GameObject blockPrefab;

    public GameObject gameGrid;
    GridScript grid;

    Dictionary<int, GameObject> blocks = new Dictionary<int, GameObject>();

    int selectedBlock = -1;

    int size = 0;

    int[] groupCounts = {0,0,0};

    void Awake()
    {
        grid = gameGrid.GetComponent<GridScript>();

        // TODO spawn the actually correct amount/type of blocks lmao
        // BLOCK ID MUST BE 1 OR GREATER
        for (int i = 0; i < 5; ++i) {
            spawnBlock(i + 1, i % 3 == 0 ? BlockType.apple() : BlockType.rice());
        }

        // TODO placeholder grid array - should put this in central static class
        // 0 = empty space, -1 = out of bounds, -2 = obstacle
        grid.initGrid(new int[][] {
            new int[] {-1, -1,  0,  0,  0, -1},
            new int[] {-1,  0,  0,  0,  0, -1},
            new int[] {-1,  0,  0,  0, -2, -1},
            new int[] { 0,  0,  0,  0, -2,  0},
            new int[] { 0,  0,  0,  0,  0,  0}
        }, 0, 3);
    }

    void initManager(int levelSize, int[] levelGroupCounts, int[][] gridArray) {
        // this will be called from the central static class
        // TODO spawn blocks in here
        // also init other stuff
    }
    
    void spawnBlock(int id, BlockType type) {
        GameObject block = Instantiate(blockPrefab, new Vector3(id, 0, 0), Quaternion.identity);
        block.GetComponent<BlockScript>().initBlock(type, this, grid);
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
