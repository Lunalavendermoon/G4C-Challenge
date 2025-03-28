using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class BlockHintScript : MonoBehaviour
{
    public float hintTimer;

    public Button hintButton;

    float timer = 0;

    public GameObject blockPrefab;

    int curId = -1;

    Dictionary<int, GameObject> blocks = new Dictionary<int, GameObject>();

    public void initBlock(int id, BlockType type, Vector3 position, bool hflip, bool vflip, int rot, BlockLevelManagerScript script, GridScript grid, int scalefact) {
        GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);
        block.GetComponent<BlockScript>().initBlock(id, type, script, grid, scalefact);

        SpriteRenderer renderer = block.GetComponent<SpriteRenderer>();
        if (hflip) {
            renderer.flipX = !renderer.flipX;
        }
        if (vflip) {
            renderer.flipY = !renderer.flipY;
        }

        for (int i = 0; i < rot; ++i) {
            block.transform.Rotate(0, 0, -90f);
        }

        // make sure block is in correct position after rotating
        block.GetComponent<BlockScript>().placeBlockAt(position);
        block.GetComponent<Renderer>().sortingOrder = 29999;

        // shouldn't be draggable
        block.GetComponent<BlockScript>().setEnabled(false);

        // hide for now
        block.SetActive(false);

        blocks.Add(id, block);
    }

    void Update()
    {
        if (timer > 0.0f) {
            timer -= Time.deltaTime;
            return;
        }
        if (curId != -1) {
            blocks[curId].SetActive(false);
            hintButton.interactable = true;
            curId = -1;
        }

    }

    public void showBlock(int id) {
        timer = hintTimer;
        hintButton.interactable = false;
        curId = id;
        blocks[curId].SetActive(true);
    }
}
