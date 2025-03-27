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
    public static void LoadTestingBlockData() {
        currentDay = 2; // change the day to test different setups

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
            blockYOffset = 1;
            blockSpawnList = new BlockType[] {
                BlockType.apple(), BlockType.bacon(), BlockType.bean(),
                BlockType.cabbage(), BlockType.carrot(), BlockType.chickenLeg(),
                BlockType.corn(), BlockType.cucumber(), BlockType.eggplant(),
                BlockType.ham(), BlockType.mushroom(), BlockType.noodle(),
                BlockType.rice()
            };
        } else if (currentDay == 2) {
            blockMaxSize = 30;
            blockMaxGroupSize = new int[] {15,7,7};
            blockGridArray = new int[][] {
                new int[] {-1, -1,  0,  0,  0, -1},
                new int[] {-1,  0,  0,  0,  0, -1},
                new int[] {-1,  0,  0,  0,  0, -1},
                new int[] { 0, -2,  0,  0,  0, -2},
                new int[] { 0,  0,  0,  0,  0,  0},
                new int[] { 0,  0,  0, -2,  0,  0},
                new int[] { 0,  0,  0,  0,  0,  0}
            };
            blockXOffset = 5;
            blockYOffset = 2;
            blockSpawnList = new BlockType[] {
                BlockType.apple(), BlockType.mushroom(), BlockType.banana(),
                BlockType.lettuce(), BlockType.cabbage(), BlockType.broccoli(),
                BlockType.eggplant(), BlockType.carrot(), BlockType.cucumber(),
                BlockType.noodle(), BlockType.rice(), BlockType.corn(),
                BlockType.bread(), BlockType.bacon(), BlockType.chickenLeg(),
                BlockType.ham(), BlockType.fish(), BlockType.egg(),
                BlockType.cheese()
            };
        } else if (currentDay == 3) {
            blockMaxSize = 40;
            blockMaxGroupSize = new int[] {20,10,10};
            blockGridArray = new int[][] {
                new int[] { 0,  0, -1, -1, -1, -1, -1, -1, -1},
                new int[] { 0,  0, -2, -1, -1,  0,  0,  0, -1},
                new int[] { 0,  0,  0, -1,  0,  0,  0,  0, -1},
                new int[] {-1, -1, -1,  0,  0, -2,  0,  0, -1},
                new int[] {-1, -1, -1,  0,  0,  0,  0,  0, -2},
                new int[] {-1, -1, -1,  0,  0,  0,  0,  0,  0},
                new int[] {-1, -1, -1,  0,  0,  0, -2,  0,  0},
                new int[] {-1, -1, -1,  0,  0,  0, -2,  0,  0},
                new int[] {-1, -1, -1, -1,  0,  0,  0,  0,  0}
            };
            blockXOffset = 4;
            blockYOffset = 3;
            blockSpawnList = new BlockType[] {
                BlockType.mushroom(), BlockType.banana(), BlockType.lettuce(),
                BlockType.cabbage(), BlockType.bread4(), BlockType.tomato(),
                BlockType.carrot(), BlockType.eggplant(), BlockType.broccoli(),
                BlockType.bread3(), BlockType.rice(), BlockType.oats(),
                BlockType.bread2(), BlockType.egg(), BlockType.ham(),
                BlockType.fish(), BlockType.chickenLeg(), BlockType.tofu(),
                BlockType.bacon(), BlockType.fishSlice()
            };
        }
    }

    public static void LoadBlockScene() {
        currentDay = 1; //TODO change this
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
        SceneManager.LoadScene("Grid Day " + currentDay, LoadSceneMode.Single);
    }

    public static void LoadMapScene(int playerSize, int[] playerNutrition) {
        if (currentDay == 1) {
            // TODO set variables
        }
        Debug.Log("Player size: " + playerSize + ", Nutrition amounts: " + playerNutrition[0] + " " + playerNutrition[1] + " " + playerNutrition[2]);
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
