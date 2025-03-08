using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile blank;
    public Tile obstacle;
    public Tile dropshadow;

    int[][] gridArray;

    int rows, cols;

    int xoffset, yoffset;

    public void initGrid(int[][] gridArray, int xoffset, int yoffset) {
        this.gridArray = gridArray;
        rows = gridArray.Length;
        cols = gridArray[0].Length;
        this.xoffset = xoffset;
        this.yoffset = yoffset;
        clearTileMap();
    }

    void clearTileMap() {
        // TODO need to rotate clockwise by 90 degrees when converting from array to grid & account for offset
        for (int i = 0; i < gridArray.Length; ++i) {
            for (int j = 0; j < gridArray[i].Length; ++j) {
                Vector3Int cell = arrayToCell(new Vector3Int(i, j, 0));
                switch (gridArray[i][j]) {
                    case -1:
                        tilemap.SetTile(cell, null);
                        break;
                    case -2:
                        tilemap.SetTile(cell, obstacle);
                        break;
                    default:
                        tilemap.SetTile(cell, blank);
                        break;
                }
            }
        }
    }

    public UnityEngine.Vector3 snapToGrid(UnityEngine.Vector3 world) {
        // TODO test this
        // UnityEngine.Vector3 conv = arrayToCell(worldToArray(world));
        // return new UnityEngine.Vector3(conv.x, conv.y);
        return tilemap.WorldToCell(world);
    }

    Vector3Int worldToArray(UnityEngine.Vector3 world) {
        Vector3Int conv = tilemap.WorldToCell(world);
        return new Vector3Int(yoffset - conv.y, conv.x - xoffset);
    }

    Vector3Int arrayToCell(Vector3Int grid) {
        return new Vector3Int(xoffset + grid.y, yoffset - grid.x);
    }

    public void drawDropShadow(UnityEngine.Vector3 position, BlockType blockType) {
        clearTileMap();
        Vector3Int off = worldToArray(position);
        bool[][] shape = blockType.shape;
        for (int i = 0; i < shape.Length; ++i) {
            for (int j = 0; j < shape[i].Length; ++j) {
                if (!shape[i][j]) {
                    continue;
                }
                if (off.x + i >= rows || off.y + j >= cols || off.x + i < 0 || off.y + j < 0) {
                    continue;
                }
                int g = gridArray[off.x + i][off.y + j];
                if (g >= 0) {
                    Vector3Int cell = arrayToCell(new Vector3Int(off.x + i, off.y + j, 0));
                    tilemap.SetTile(cell, dropshadow);
                }
            }
        }
    }

    public int checkBlockPosition(int id, UnityEngine.Vector3 position, BlockType blockType) {
        clearTileMap();
        Vector3Int off = worldToArray(position);
        bool[][] shape = blockType.shape;
        bool doLogs = false;
        if (doLogs)
            Debug.Log(tilemap.WorldToCell(position) + " " + off);
        for (int i = 0; i < shape.Length; ++i) {
            for (int j = 0; j < shape[i].Length; ++j) {
                if (!shape[i][j]) {
                    continue;
                }
                if (off.x + i >= rows || off.y + j >= cols || off.x + i < 0 || off.y + j < 0) {
                    if (doLogs)
                        Debug.Log(tilemap.WorldToCell(position) + " " + off + " false " + blockType.name + " out of bounds");
                    return -1;
                }
                int g = gridArray[off.x + i][off.y + j];
                if (g == -2 || g == -1 || (g > 0 && g != id)) {
                    if (doLogs)
                        Debug.Log(tilemap.WorldToCell(position) + " " + off + " false " + blockType.name + " filled " + g);
                    return -2;
                }
            }
        }
        if (doLogs)
            Debug.Log(tilemap.WorldToCell(position) + " " + off + " true " + blockType.name);
        return 0;
    }

    public void addBlock(int id, UnityEngine.Vector3 position, BlockType blockType) {
        Vector3Int off = worldToArray(position);
        bool[][] shape = blockType.shape;
        for (int i = 0; i < shape.Length; ++i) {
            for (int j = 0; j < shape[i].Length; ++j) {
                if (shape[i][j]) {
                    gridArray[off.x + i][off.y + j] = id;
                }
            }
        }
    }

    public void removeBlock(int id) {
        for (int i = 0; i < gridArray.Length; ++i) {
            for (int j = 0; j < gridArray[i].Length; ++j) {
                if (gridArray[i][j] == id) {
                    gridArray[i][j] = 0;
                }
            }
        }
    }

    public void updateBlock(int id, UnityEngine.Vector3 position, BlockType blockType) {
        removeBlock(id);
        addBlock(id, position, blockType);
    }
}
