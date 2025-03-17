using System;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    BlockLevelManagerScript levelManager;
    GridScript grid;
    new SpriteRenderer renderer;

    public int id {get; set;}

    public BlockType blockType {get; set;}

    public bool isOnGrid {get; set;}
    
    Vector3 resetPosition;
    Vector2 difference = Vector2.zero;

    int orientation = 0;

    float scalefact;

    float xoffset;

    public Sprite appleSprite;
    public Sprite baconSprite;
    public Sprite beanSprite;
    public Sprite cabbageSprite;
    public Sprite carrotSprite;
    public Sprite chickenLegSprite;
    public Sprite cornSprite;
    public Sprite cucumberSprite;
    public Sprite eggplantSprite;
    public Sprite hamSprite;
    public Sprite mushroomSprite;
    public Sprite noodleSprite;
    public Sprite riceSprite;

    bool isEnabled = true;

    public void initBlock(int id, BlockType type, BlockLevelManagerScript levelManager, GridScript grid, int scaling) {
        this.id = id;
        isOnGrid = false;
        blockType = type;
        this.levelManager = levelManager;
        this.grid = grid;
        resetPosition = transform.position;

        renderer = GetComponent<SpriteRenderer>();
        switch (type.name) {
            case "apple":
                renderer.sprite = appleSprite;
                break;
            case "bacon":
                renderer.sprite = baconSprite;
                break;
            case "bean":
                renderer.sprite = beanSprite;
                break;
            case "cabbage":
                renderer.sprite = cabbageSprite;
                break;
            case "carrot":
                renderer.sprite = carrotSprite;
                break;
            case "chicken leg":
                renderer.sprite = chickenLegSprite;
                break;
            case "corn":
                renderer.sprite = cornSprite;
                break;
            case "cucumber":
                renderer.sprite = cucumberSprite;
                break;
            case "eggplant":
                renderer.sprite = eggplantSprite;
                break;
            case "ham":
                renderer.sprite = hamSprite;
                break;
            case "mushroom":
                renderer.sprite = mushroomSprite;
                break;
            case "noodle":
                renderer.sprite = noodleSprite;
                break;
            case "rice":
                renderer.sprite = riceSprite;
                break;
            default:
                renderer.sprite = appleSprite;
                break;
        }


        if (scaling == 3) {
            transform.localScale = new Vector3(1.5f,1.5f,1);
            scalefact = 0.75f;
            xoffset = 0.5f;
        } else {
            scalefact = 0.5f;
            xoffset = 0;
        }

        Vector2 S = renderer.sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
    }

    public void setEnabled(bool enabled) {
        isEnabled = enabled;
    }

    private void OnMouseDown() {
        if (!isEnabled) {
            return;
        }
        makeTransparent();
        grid.clearTileMap();
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        levelManager.selectBlock(id);
    }

    private void OnMouseDrag() {
        if (!isEnabled) {
            return;
        }
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        grid.drawDropShadow(getSpriteTopLeft(), blockType);
    }

    private void OnMouseUp() {
        if (!isEnabled) {
            return;
        }
        if (isOnGrid) {
            removeFromGrid();
        }
        int status = grid.checkBlockPosition(id, getSpriteTopLeft(), blockType);
        if (status == -1) {
            transform.position = resetPosition;
            makeOpaque();
        } else {
            makeTransparent();
        }
    }

    public Vector3 getSpriteTopLeft() {
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

    void removeFromGrid() {
        isOnGrid = false;
        grid.removeBlock(id);
        levelManager.playerRemoveBlock(id);
        grid.drawDropShadow(getSpriteTopLeft(), blockType);
    }

    void makeTransparent() {
        Color col = renderer.color;
        col.a = 0.8f;
        renderer.color = col;
    }

    void makeOpaque() {
        Color col = renderer.color;
        col.a = 1;
        renderer.color = col;
    }

    public void placeBlock() {
        int status = grid.checkBlockPosition(id, getSpriteTopLeft(), blockType);
        if (status == 0) {
            makeOpaque();

            if (isOnGrid) {
                grid.updateBlock(id, getSpriteTopLeft(), blockType);
            } else {
                isOnGrid = true;
                grid.addBlock(id, getSpriteTopLeft(), blockType);
                levelManager.playerAddBlock(id);
                levelManager.deselectBlock();
            }
            // snap to grid
            transform.position = (grid.snapToGrid(getSpriteTopLeft()) * scalefact)
            + new Vector3(
                blockType.shape[0].Length / (2.0f / scalefact) + xoffset,
                orientation % 2 == 0 ? 0 : -Math.Min(1, Math.Abs(blockType.shape.Length - blockType.shape[0].Length)) / (2.0f / scalefact)
            );
        } else {
            // TODO send error message
        }
    }

    public void flip(bool isHorizontal) {
        if (isOnGrid) {
            return;
        }
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

        if (orientation % 2 == 1) {
            isHorizontal = !isHorizontal;
        }

        blockType.shape = shape;
        if (isHorizontal) {
            renderer.flipX = !renderer.flipX;
        } else {
            renderer.flipY = !renderer.flipY;
        }
        grid.drawDropShadow(getSpriteTopLeft(), blockType);
    }

    public void rotate() {
        if (isOnGrid) {
            return;
        }

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
        orientation = (orientation + 1) % 4;
        transform.Rotate(0, 0, -90f);
        grid.drawDropShadow(getSpriteTopLeft(), blockType);
    }
}
