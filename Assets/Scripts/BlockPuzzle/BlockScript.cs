using System;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    BlockLevelManagerScript levelManager;
    GridScript grid;

    SpriteRenderer renderer;

    public int id {get; set;}

    public BlockType blockType {get; set;}

    public bool isOnGrid {get; set;}

    bool isDragged = false;
    Vector3 prevPosition, resetPosition;
    Vector2 difference = Vector2.zero;

    // weird floating point error when flipping sprites causing getTopLeft to return wrong coordinates(?)
    // store last dragged position (guaranteed to be in the correct grid i think...)
    Vector3 refPos;

    int orientation = 0;

    public Sprite appleSprite;
    public Sprite riceSprite;
    public Sprite chickenSprite;

    public void initBlock(int id, BlockType type, BlockLevelManagerScript levelManager, GridScript grid) {
        this.id = id;
        isOnGrid = false;
        blockType = type;
        this.levelManager = levelManager;
        this.grid = grid;
        resetPosition = transform.position;
        prevPosition = transform.position;

        renderer = GetComponent<SpriteRenderer>();
        switch (type.name) {
            case "apple":
                renderer.sprite = appleSprite;
                break;
            case "rice":
                renderer.sprite = riceSprite;
                break;
            case "chicken leg":
                renderer.sprite = chickenSprite;
                break;
            default:
                renderer.sprite = appleSprite;
                break;
        }

        Vector2 S = renderer.sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;

        refPos = getSpriteTopLeft();
    }

    private void OnMouseDown() {
        isDragged = true;
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        levelManager.selectBlock(id);
        prevPosition = transform.position;
    }

    private void OnMouseDrag() {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        grid.drawDropShadow(getSpriteTopLeft(), blockType);
        refPos = getSpriteTopLeft();
    }

    Vector3 getSpriteTopLeft() {
        switch (orientation) {
            case 0:
                return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.max.y, 0));
            case 1:
                return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.min.y, 0));
            case 2:
                return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.max.x, renderer.sprite.bounds.min.y, 0));
            case 3:
                return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.max.x, renderer.sprite.bounds.max.y, 0));
            default:
                return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.max.y, 0));
        };
    }

    private void OnMouseUp() {
        isDragged = false;
        int status = grid.checkBlockPosition(id, getSpriteTopLeft(), blockType);
        if (status == 0) {
            if (isOnGrid) {
                grid.updateBlock(id, getSpriteTopLeft(), blockType);
            } else {
                isOnGrid = true;
                grid.addBlock(id, getSpriteTopLeft(), blockType);
                levelManager.playerAddBlock(id);
            }
            // snap to grid
            transform.position = grid.snapToGrid(getSpriteTopLeft()) + new Vector3(
                blockType.shape[0].Length / 2.0f,
                orientation % 2 == 0 ? 0 : -Math.Abs(blockType.shape.Length - blockType.shape[0].Length) / 2.0f
            );
        } else {
            if (status == -1 || (!isOnGrid && status == -2)) {
                transform.position = resetPosition;
                if (isOnGrid) {
                    isOnGrid = false;
                    grid.removeBlock(id);
                    levelManager.playerRemoveBlock(id);
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
            shape[i] = new bool[oldShape[0].Length];
        }

        if (isHorizontal) {
            for (int i = 0; i < shape.Length; ++i) {
                for (int j = 0; j < shape[0].Length; ++j) {
                    shape[i][shape[0].Length - j - 1] = oldShape[i][j];
                }
            }
        } else {
            for (int i = 0; i < shape.Length; ++i) {
                for (int j = 0; j < shape[0].Length; ++j) {
                    shape[shape.Length - i - 1][j] = oldShape[i][j];
                }
            }
        }

        blockType.shape = shape;
        if (isDragged || !isOnGrid || grid.checkBlockPosition(id, refPos, blockType) == 0) {
            if (isHorizontal) {
                renderer.flipX = !renderer.flipX;
            } else {
                renderer.flipY = !renderer.flipY;
            }
            if (isOnGrid && !isDragged) {
                grid.updateBlock(id, refPos, blockType);
            }
        } else {
            blockType.shape = oldShape;
            // TODO send error
        }
    }

    public void rotate() {
        bool[][] oldShape = blockType.shape;

        int rows = oldShape.Length, cols = oldShape[0].Length;

        bool[][] shape = new bool[cols][];
        for (int i = 0; i < cols; ++i) {
            shape[i] = new bool[rows];
            for (int j = 0; j < rows; ++j) {
                shape[i][j] = oldShape[rows - j - 1][i];
            }
        }
        

        blockType.shape = shape;
        if (isDragged || !isOnGrid || grid.checkBlockPosition(id, refPos, blockType) == 0) {
            orientation = (orientation + 1) % 4;
            transform.Rotate(0, 0, -90f);

            if (isOnGrid && !isDragged) {
                grid.updateBlock(id, refPos, blockType);
            }
        } else {
            blockType.shape = oldShape;
            // TODO send error
        }
    }
}
