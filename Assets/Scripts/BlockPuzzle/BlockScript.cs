using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public GameObject levelManager;

    public int id {get; set;}

    public BlockType blockType {get; set;}

    public bool isOnGrid {get; set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void initBlock(BlockType type) {
        isOnGrid = false;
        blockType = type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flip(bool isHorizontal) {
        // TODO
    }

    public void rotate(bool isClockwise) {
        // TODO
    }
}
