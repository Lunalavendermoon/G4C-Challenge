using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile blank;
    public Tile obstacle;

    int[][] gridArray;

    int rows, cols;

    int xoffset, yoffset;

    public void initGrid(int[][] gridArray, int xoffset, int yoffset) {
        this.gridArray = gridArray;
        rows = gridArray.Length;
        cols = gridArray[0].Length;
        this.xoffset = xoffset;
        this.yoffset = yoffset;
    }

    void redrawTileMap() {
        // for (int i = 0; i < gridArray.Length; ++i) {
        //     for (int j = 0; j < gridArray[i].Length; ++j) {
        //         Vector3Int cell = new Vector3Int(i + xoffset, j + yoffset, 0);
        //         switch (gridArray[i][j]) {
        //             case -1:
        //                 tilemap.SetTile(cell, null);
        //                 break;
        //             case -2:
        //                 tilemap.SetTile(cell, obstacle);
        //                 break;
        //             default:
        //                 tilemap.SetTile(cell, blank);
        //                 break;
        //         }
        //     }
        // }
    }

    Vector3Int getGridPos(Vector3 world) {
        return tilemap.WorldToCell(world);
    }

    Vector2Int getOffset(Vector3Int pos) {
        return new Vector2Int(pos.x - xoffset, yoffset - pos.y);
    }

    public int checkBlockPosition(int id, Vector3 position, BlockType blockType) {
        Vector3Int pos = getGridPos(position);
        Vector2Int off = getOffset(pos);
        bool[][] shape = blockType.shape;
        // Debug.Log(pos + " " + off.x + " " + off.y);
        for (int i = 0; i < shape.Length; ++i) {
            for (int j = 0; j < shape[i].Length; ++j) {
                if (!shape[i][j]) {
                    continue;
                }
                if (off.x + i >= rows || off.y + j >= cols || off.x + i < 0 || off.y + j < 0) {
                    // Debug.Log(pos + " " + off.x + " " + off.y + " false " + blockType.name + " out of bounds " + i + " " + j);
                    return -1;
                }
                int g = gridArray[off.x + i][off.y + j];
                if (g == -2 || g == -1 || (g > 0 && g != id)) {
                    // Debug.Log(pos + " " + off.x + " " + off.y + " false " + blockType.name + " filled " + i + " " + j);
                    return -2;
                }
            }
        }
        // Debug.Log(pos + " " + off.x + " " + off.y + " true " + blockType.name);
        return 0;
    }

    public void addBlock(int id, Vector3 position, BlockType blockType) {
        Vector3Int pos = getGridPos(position);
        Vector2Int off = getOffset(pos);
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

    public void updateBlock(int id, Vector3 position, BlockType blockType) {
        removeBlock(id);
        addBlock(id, position, blockType);
    }
}
