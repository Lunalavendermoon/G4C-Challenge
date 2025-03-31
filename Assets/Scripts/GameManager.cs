using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{

    public static float foodGiven = 6;

    public static int blockMaxSize, blockXOffset, blockYOffset;

    public static int[] blockMaxGroupSize;

    public static int[][] blockGridArray;

    public static int[][] blockSolutionArray;

    public static Dictionary<int, object[]> blockPositionArray = new Dictionary<int, object[]>();

    public static BlockType[] blockSpawnList;

    public static int gridFoodAmount;
    public static int[] gridFoodNutrition = new int[]{0,0,0};
    
    [RuntimeInitializeOnLoadMethod]
    static void LoadFirstScene() {
        // TODO load main menu?
    }

    public static void LoadBlockData(int currentDay) {
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
            blockSolutionArray = new int[][] {
                new int[] {-1, -1,  1,  1,  2, -1},
                new int[] {-1,  9,  1,  1,  2, -1},
                new int[] {-1,  9,  9,  8, -2, -1},
                new int[] {12, 12, 12,  8, -2,  6},
                new int[] {12, 12, 12,  8,  6,  6}
            };
            blockPositionArray.Add( 1, new object[] {false, false, 0, 0, 2});
            blockPositionArray.Add( 2, new object[] {false, false, 0, 0, 4});
            blockPositionArray.Add( 6, new object[] { true, false, 0, 3, 4});
            blockPositionArray.Add( 8, new object[] {false, false, 1, 3, 2});
            blockPositionArray.Add( 9, new object[] {false, false, 0, 1, 1});
            blockPositionArray.Add(12, new object[] {false, false, 0, 3, 0});
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
                new int[] {-1,  0,  0,  0, -2, -1},
                new int[] { 0,  0,  0,  0, -2,  0},
                new int[] { 0,  0,  0,  0,  0,  0}
            };
            // TODO fill this out
            blockSolutionArray = new int[][] {
                new int[] {-1, -1,  0,  0,  0, -1},
                new int[] {-1,  0,  0,  0,  0, -1},
                new int[] {-1,  0,  0,  0, -2, -1},
                new int[] { 0,  0,  0,  0, -2,  0},
                new int[] { 0,  0,  0,  0,  0,  0}
            };
            blockPositionArray.Clear();
            // TODO Add solution to blockPositionArray
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
            // TODO fill this out
            blockSolutionArray = new int[][] {
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
            blockPositionArray.Clear();
            // TODO Add solution to blockPositionArray
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

    public static void StoreNutritionInfo(int playerSize, int[] playerNutrition) {
        gridFoodAmount = playerSize;
        for (int i = 0; i < 3; ++i) {
            gridFoodNutrition[i] = playerNutrition[i];
        }
    }
}
