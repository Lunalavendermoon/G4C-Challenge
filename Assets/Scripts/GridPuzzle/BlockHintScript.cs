using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BlockHintScript : MonoBehaviour
{
    public float hintTimer;

    public GameObject helpButton;

    float timer = 0;

    public GameObject blockPrefab;
    public TextMeshProUGUI hintText;

    int curId = 0;

    Dictionary<int, GameObject> blocks = new Dictionary<int, GameObject>();

    void Start()
    {
        hintText.text = "";
    }

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
        block.GetComponent<BlockScript>().hintColor();
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
        if (curId != 0) {
            if (GameManager.blockPositionArray.ContainsKey(curId)) {
                blocks[curId].SetActive(false);
            } else {
                hintText.text = "";
            }
            helpButton.GetComponent<HelpButtonScript>().ButtonClickable(true);
            curId = 0;
        }
    }

    public void showBlock(int id) {
        timer = hintTimer;
        helpButton.GetComponent<HelpButtonScript>().ButtonClickable(false);
        curId = id;
        if (id == -1) {
            hintText.text = "All blocks are correctly placed!";
        } else {
            if (GameManager.blockPositionArray.ContainsKey(curId)) {
                blocks[curId].SetActive(true);
            } else {
                hintText.text = blocks[curId].GetComponent<BlockScript>().blockType.displayName + " should not be on the grid!";
            }
        }
    }
}
