using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int blockMaxSize, blockXOffset, blockYOffset;

    public static int[] blockMaxGroupSize;

    public static int[][] blockGridArray;

    public static BlockType[] blockSpawnList;
    
    [RuntimeInitializeOnLoadMethod]
    static void LoadFirstScene() {
        // TODO replace this w/ main menu in the final version
        LoadDialogueScene();
    }

    public static void LoadDialogueScene() {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public static void LoadBlockScene(int day) {
        if (day == 1) {
            blockMaxSize = 20;
            blockMaxGroupSize = new int[] {10,5,5};
            blockGridArray = new int[][] {
                new int[] {-1, -1,  0,  0,  0, -1},
                new int[] {-1,  0,  0,  0,  0, -1},
                new int[] {-1,  0,  0,  0, -2, -1},
                new int[] { 0,  0,  0,  0, -2,  0},
                new int[] { 0,  0,  0,  0,  0,  0}
            };
            blockXOffset = 2;
            blockYOffset = 3;
            blockSpawnList = new BlockType[] {
                BlockType.apple(), BlockType.rice(), BlockType.chickenLeg(),
                BlockType.apple(), BlockType.rice(), BlockType.chickenLeg()
            };
        }
        SceneManager.LoadScene("Grid", LoadSceneMode.Single);
    }
}
