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

    // weird floating point error when flipping sprites causing getTopLeft to return wrong coordinates(?)
    // store last dragged position (guaranteed to be in the correct grid i think...)
    Vector3 refPos;

    bool ogOrientation = true;

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
        return GetComponent<Renderer>().transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.max.y, 0));
    }

    private void OnMouseUp() {
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
            transform.position = grid.snapToGrid(getSpriteTopLeft()) + new Vector3(blockType.shape[0].Length / 2.0f, 0);
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
        if (!isOnGrid || grid.checkBlockPosition(id, refPos, blockType) == 0) {
            if (isHorizontal) {
                renderer.flipX = !renderer.flipX;
            } else {
                renderer.flipY = !renderer.flipY;
            }
            if (isOnGrid) {
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
        if (!isOnGrid || grid.checkBlockPosition(id, refPos, blockType) == 0) {
            // Vector3 tl = getSpriteTopLeft();
            // // TODO FIGURE OUT HOW TO ROTATE THE SPRITE WTF
            // // best case scenario is to keep it anchored at top left position so the getTopLeft calcs still work
            // if (isClockwise) {
            //     // transform.Rotate(Vector3.forward * -90);
            // } else {
            //     // transform.Rotate(Vector3.forward * 90);
            // }
            // // if (ogOrientation) {
            // //     transform.position = tl + new Vector3(blockType.shape.Length / 2.0f, blockType.shape[0].Length / 2.0f);
            // // } else {
            // //     transform.position = tl + new Vector3(blockType.shape[0].Length / 2.0f, blockType.shape.Length / 2.0f);
            // // }

            // // Vector2 S = renderer.sprite.bounds.size;
            // // gameObject.GetComponent<BoxCollider2D>().size = ogOrientation ? S : new Vector2(S.y, S.x);

            // transform.position = grid.snapToGrid(getSpriteTopLeft()) + new Vector3(
            //     (ogOrientation ? blockType.shape[0].Length : blockType.shape.Length) / 2.0f,
            //     0
            // );

            // ogOrientation = !ogOrientation;
            transform.Rotate(0, 0, -90f);

            if (isOnGrid) {
                grid.updateBlock(id, refPos, blockType);
            }
        } else {
            blockType.shape = oldShape;
            // TODO send error
        }
    }
}
