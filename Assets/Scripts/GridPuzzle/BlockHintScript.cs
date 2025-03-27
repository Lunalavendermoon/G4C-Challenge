using System.Collections.Generic;
using UnityEngine;

public class BlockHintScript : MonoBehaviour
{
    public GameObject blockPrefab;

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

        // shouldn't be draggable
        block.GetComponent<BlockScript>().setEnabled(false);

        // hide for now
        block.SetActive(false);

        blocks.Add(id, block);
    }
}
