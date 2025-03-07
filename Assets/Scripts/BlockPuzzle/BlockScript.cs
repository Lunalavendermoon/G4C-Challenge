using UnityEngine;

public class BlockScript : MonoBehaviour
{
    BlockLevelManagerScript levelManager;

    public int id {get; set;}

    public BlockType blockType {get; set;}

    public bool isOnGrid {get; set;}

    Vector2 difference = Vector2.zero;


    public void initBlock(BlockType type, BlockLevelManagerScript levelManager) {
        isOnGrid = false;
        blockType = type;
        this.levelManager = levelManager;
    }

    private void OnMouseDown()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        levelManager.selectBlock(id);
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    public void flip(bool isHorizontal) {
        bool[][] oldShape = blockType.shape;

        bool[][] shape = new bool[oldShape.Length][];
        for (int i = 0; i < oldShape.Length; ++i) {
            shape[i] = (bool[]) oldShape[i].Clone();
        }

        if (isHorizontal) {
            int len = oldShape[0].Length;
            for (int i = 0; i < oldShape.Length; ++i) {
                for (int j = 0; j <= len / 2; ++j) {
                    bool temp = oldShape[i][len - j - 1];
                    oldShape[i][len - j - 1] = oldShape[i][j];
                    oldShape[i][j] = temp;
                }
            }
        } else {
            int len = oldShape.Length;
            for (int i = 0; i <= oldShape.Length / 2; ++i) {
                for (int j = 0; j <= oldShape[i].Length; ++j) {
                    bool temp = oldShape[len - i - 1][j];
                    oldShape[len - i - 1][j] = oldShape[i][j];
                    oldShape[i][j] = temp;
                }
            }
        }

        // TODO validate and update blockType.shape accordingly
    }

    public void rotate(bool isClockwise) {
        bool[][] oldShape = blockType.shape;

        int rows = oldShape.Length, cols = oldShape[0].Length;

        bool[][] shape = new bool[cols][];
        if (isClockwise) {
            for (int i = 0; i < cols; ++i) {
                shape[i] = new bool[rows];
                for (int j = 0; j < rows; ++j) {
                    shape[i][j] = oldShape[rows - j - 1][i];
                }
            }
        } else {
            for (int i = 0; i < cols; ++i) {
                shape[i] = new bool[rows];
                for (int j = 0; j < rows; ++j) {
                    shape[i][j] = oldShape[j][cols - i - 1];
                }
            }
        }

        // TODO validate and update blockType.shape accordingly
    }
}
