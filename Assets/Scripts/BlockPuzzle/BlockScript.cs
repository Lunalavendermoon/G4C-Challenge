using UnityEngine;

public class BlockScript : MonoBehaviour
{
    BlockLevelManagerScript levelManager;
    GridScript grid;

    SpriteRenderer renderer;

    public int id {get; set;}

    public BlockType blockType {get; set;}

    public bool isOnGrid {get; set;}

    Vector3 prevPosition, resetPosition;
    Vector2 difference = Vector2.zero;

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void initBlock(BlockType type, BlockLevelManagerScript levelManager, GridScript grid) {
        isOnGrid = false;
        blockType = type;
        this.levelManager = levelManager;
        this.grid = grid;
        resetPosition = transform.position;
        prevPosition = transform.position;
    }

    private void OnMouseDown() {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        levelManager.selectBlock(id);
        prevPosition = transform.position;
    }

    private void OnMouseDrag() {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    Vector3 getSpriteTopLeft() {
        return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.max.x, renderer.sprite.bounds.min.y, 0));
    }

    private void OnMouseUp() {
        int status = grid.checkBlockPosition(id, getSpriteTopLeft(), blockType);
        if (status == 0) {
            if (isOnGrid) {
                grid.updateBlock(id, getSpriteTopLeft(), blockType);
            } else {
                isOnGrid = true;
                grid.addBlock(id, getSpriteTopLeft(), blockType);
            }
        } else {
            if (status == -1 || (!isOnGrid && status == -2)) {
                transform.position = resetPosition;
                if (isOnGrid) {
                    isOnGrid = false;
                    grid.removeBlock(id);
                }
            } else if (isOnGrid && status == -2) {
                transform.position = prevPosition;
            }
        }
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
        if (grid.checkBlockPosition(id, getSpriteTopLeft(), blockType) == 0) {
            blockType.shape = shape;
            grid.updateBlock(id, getSpriteTopLeft(), blockType);
        } else {
            // TODO send error
        }
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
