using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static int currentDay;


    public static int blockMaxSize, blockXOffset, blockYOffset;

    public static int[] blockMaxGroupSize;

    public static int[][] blockGridArray;

    public static BlockType[] blockSpawnList;
    
    [RuntimeInitializeOnLoadMethod]
    static void LoadFirstScene() {
        // TODO load w/ main menu in the final version
        currentDay = 0;

        // Uncomment this to start from the very beginning scene
        // LoadDialogueScene();
    }

    public static void LoadDialogueScene() {
        // currentDay increases every time we show the day cutscene
        ++currentDay;
        if (currentDay == 1) {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }

    // FOR TESTING ONLY
    public static void LoadDay1BlockData() {
        blockMaxSize = 20;
        blockMaxGroupSize = new int[] {10,5,5};
        blockGridArray = new int[][] {
            new int[] {-1, -1,  0,  0,  0, -1},
            new int[] {-1,  0,  0,  0,  0, -1},
            new int[] {-1,  0,  0,  0, -2, -1},
            new int[] { 0,  0,  0,  0, -2,  0},
            new int[] { 0,  0,  0,  0,  0,  0}
        };
        blockXOffset = 4;
        blockYOffset = 3;
        blockSpawnList = new BlockType[] {
            BlockType.apple(), BlockType.noodle(), BlockType.apple(),
            BlockType.noodle(), BlockType.apple(), BlockType.noodle(),
            BlockType.noodle(), BlockType.apple(), BlockType.noodle(),
            BlockType.noodle(), BlockType.apple(), BlockType.noodle(),
            BlockType.noodle(), BlockType.apple(), BlockType.noodle(),
            BlockType.noodle(), BlockType.apple(), BlockType.noodle(),
            BlockType.noodle(), BlockType.apple(), BlockType.noodle(),
            BlockType.noodle(), BlockType.apple()
        };
    }

    public static void LoadBlockScene() {
        if (currentDay == 1) {
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

    public static void LoadMapScene(int playerSize, int[] playerNutrition) {
        if (currentDay == 1) {
            // TODO set variables
        }
        Debug.Log("Player size: " + playerSize + ", Nutrition amounts: " + playerNutrition[0] + " " + playerNutrition[1] + " " + playerNutrition[2]);
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
