using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BlockLevelManagerScript : MonoBehaviour
{
    public GameObject blockPrefab;

    public GameObject gameGrid;

    public TMP_Text sizeText;

    public TMP_Text nutritionText;

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
        for (int i = 0; i < 6; ++i) {
            spawnBlock(i + 1, i % 3 == 0 ? BlockType.apple() : i % 3 == 1 ? BlockType.rice() : BlockType.chickenLeg(), new Vector3(-3, 5 - (1.5f*(i+1)), 0));
        }

        // TODO placeholder grid array - should put this in central static class
        // 0 = empty space, -1 = out of bounds, -2 = obstacle
        grid.initGrid(new int[][] {
            new int[] {-1, -1,  0,  0,  0, -1},
            new int[] {-1,  0,  0,  0,  0, -1},
            new int[] {-1,  0,  0,  0, -2, -1},
            new int[] { 0,  0,  0,  0, -2,  0},
            new int[] { 0,  0,  0,  0,  0,  0}
        }, 2, 3);

        updateUI();
    }

    void initManager(int maxSize, int[] maxGroupCounts, int[][] gridArray, BlockType[] blocksToSpawn) {
        // this will be called from the central static class
        // TODO spawn blocks in here
        // also init other stuff
    }
    
    void spawnBlock(int id, BlockType type, Vector3 position) {
        GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);
        block.GetComponent<BlockScript>().initBlock(id, type, this, grid);
        blocks.Add(id, block);
    }

    BlockScript getBlockScript(int id) {
        GameObject block = blocks[id];
        return block.GetComponent<BlockScript>();
    }

    public void playerAddBlock(int id) {
        BlockScript block = getBlockScript(id);
        size += block.blockType.size;
        switch (block.blockType.foodGroup) {
            case "veg":
                groupCounts[0] += block.blockType.size;
                break;
            case "carb":
                groupCounts[1] += block.blockType.size;
                break;
            case "protein":
                groupCounts[2] += block.blockType.size;
                break;
        }
        updateUI();
    }

    public void playerRemoveBlock(int id) {
        BlockScript block = getBlockScript(id);
        size -= block.blockType.size;
        switch (block.blockType.foodGroup) {
            case "veg":
                groupCounts[0] -= block.blockType.size;
                break;
            case "carb":
                groupCounts[1] -= block.blockType.size;
                break;
            case "protein":
                groupCounts[2] -= block.blockType.size;
                break;
        }
        updateUI();
    }

    void updateUI() {
        sizeText.SetText("Size: " + size);
        nutritionText.SetText("veg " + groupCounts[0] + "\ncarb " + groupCounts[1] + "\nprotein " + groupCounts[2]);
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

    public void rotateBlock() {
        if (selectedBlock < 0) {
            return;
        }
        getBlockScript(selectedBlock).rotate();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedBlock != -1) {
            // space = confirm placement
            // R = rotate CW
            // D,F = flip horiz,vert
            if (Input.GetKeyDown(KeyCode.Space)) {
                getBlockScript(selectedBlock).placeBlock();
            } else if (Input.GetKeyDown(KeyCode.R)) {
                getBlockScript(selectedBlock).rotate();
            } else if (Input.GetKeyDown(KeyCode.D)) {
                getBlockScript(selectedBlock).flip(true);
            } else if (Input.GetKeyDown(KeyCode.F)) {
                getBlockScript(selectedBlock).flip(false);
            }
        }
    }
}
