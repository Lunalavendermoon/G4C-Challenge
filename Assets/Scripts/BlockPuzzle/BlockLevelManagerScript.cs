using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;

public class BlockLevelManagerScript : MonoBehaviour
{
    public GameObject blockPrefab;

    public GameObject gameGrid;

    public TMP_Text vegText;
    public TMP_Text proteinText;
    public TMP_Text carbText;

    public GameObject sizeBar;

    GridScript grid;

    Dictionary<int, GameObject> blocks = new Dictionary<int, GameObject>();

    int selectedBlock = -1;
    int orderCount = 1;

    int size = 0;
    int maxSize;

    int[] nutrition = {0,0,0};
    int[] maxNutrition;

    void Start()
    {
        GameManager.LoadDay1BlockData();

        maxSize = GameManager.blockMaxSize;
        maxNutrition = GameManager.blockMaxGroupSize;

        grid = gameGrid.GetComponent<GridScript>();

        BlockType[] blocksToSpawn = GameManager.blockSpawnList;

        float ycarb = 2.5f;
        
        // BLOCK ID MUST BE 1 OR GREATER
        int id = 1;
        foreach (BlockType b in blocksToSpawn) {
            spawnBlock(id, b, id - 1, ycarb);
            id++;
        }

        // TODO placeholder grid array - should put this in central static class
        // 0 = empty space, -1 = out of bounds, -2 = obstacle
        grid.initGrid(GameManager.blockGridArray, GameManager.blockXOffset, GameManager.blockYOffset);

        updateUI();
    }
    
    void spawnBlock(int id, BlockType type, int count, float yoffset) {
        int x = count < 15 ? count % 5 : (count - 15) % 4;
        int y = count < 15 ? count / 5 : 3 + (count - 15) / 4;
        Vector3 position = new Vector3(-7.0f + 1.3f * x, yoffset - 1.4f * y);
        Vector3 jitter;
        if (count == 4) {
            jitter = Vector3.zero;
        } else {
            jitter = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }
        GameObject block = Instantiate(blockPrefab, position + jitter, Quaternion.identity);
        block.GetComponent<BlockScript>().initBlock(id, type, this, grid);
        blocks.Add(id, block);
    }

    BlockScript getBlockScript(int id) {
        GameObject block = blocks[id];
        return block.GetComponent<BlockScript>();
    }

    public void playerAddBlock(int id) {
        blocks[id].GetComponent<Renderer>().sortingOrder = 0;
        BlockScript block = getBlockScript(id);
        size += block.blockType.size;
        switch (block.blockType.foodGroup) {
            case "veg":
                nutrition[0] += block.blockType.size;
                break;
            case "carb":
                nutrition[1] += block.blockType.size;
                break;
            case "protein":
                nutrition[2] += block.blockType.size;
                break;
        }
        updateUI();
    }

    public void playerRemoveBlock(int id) {
        BlockScript block = getBlockScript(id);
        size -= block.blockType.size;
        switch (block.blockType.foodGroup) {
            case "veg":
                nutrition[0] -= block.blockType.size;
                break;
            case "carb":
                nutrition[1] -= block.blockType.size;
                break;
            case "protein":
                nutrition[2] -= block.blockType.size;
                break;
        }
        updateUI();
    }

    void updateUI() {
        sizeBar.transform.localScale = new Vector3(0.93f * Mathf.Min(((float)size) / maxSize, 1), 0.93f, 1);
        vegText.SetText(nutrition[0] + "/" + maxNutrition[0]);
        carbText.SetText(nutrition[1] + "/" + maxNutrition[1]);
        proteinText.SetText(nutrition[2] + "/" + maxNutrition[2]);
    }

    public void selectBlock(int id) {
        selectedBlock = id;
        blocks[id].GetComponent<Renderer>().sortingOrder = orderCount++;
    }

    public void deselectBlock(int id) {
        selectedBlock = -1;
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
            } else if (Input.GetKeyDown(KeyCode.H)) {
                getBlockScript(selectedBlock).flip(true);
            } else if (Input.GetKeyDown(KeyCode.V)) {
                getBlockScript(selectedBlock).flip(false);
            }
        }
        // M = finish gameplay and go to Map scene
        if (Input.GetKeyDown(KeyCode.M)) {
            GameManager.LoadMapScene(size, nutrition);
        }
    }
}
