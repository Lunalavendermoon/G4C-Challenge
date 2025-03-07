using System;
using NUnit.Framework.Constraints;

public class BlockType {
    public string name;

    public string foodGroup;

    public int size;

    public bool[][] shape;

    public BlockType(string _name, string _foodGroup, int _size) {
        name = _name;
        foodGroup = _foodGroup;
        size = _size;
        if (name.Equals("apple")) {
            shape = new bool[][] {
                new bool[] {true, true},
                new bool[] {true, true}
            };
        } else if (name.Equals("rice")) {
            shape = new bool[][] {
                new bool[] {true, false, true},
                new bool[] {true, true, true}
            };
        } else if (name.Equals("chicken")) {
            shape = new bool[][] {
                new bool[] {true, false},
                new bool[] {true, true}
            };
        }
        else {
            shape = new bool[1][];
        }

        // shape = new bool[_shape.GetLength(0)][];
        // for (int i = 0; i < _shape.GetLength(0); ++i) {
        //     for (int j = 0; j < _shape[i].GetLength(0); ++j) {
        //         shape[i][j] = (_shape[i][j] == 1) ? true : false;
        //     }
        // }
    }

    public static BlockType apple() {
        return new BlockType("apple", "veg", 1);
    }

    public static BlockType rice() {
        return new BlockType("rice", "carb", 2);
    }

    public static BlockType chicken() {
        return new BlockType("chicken", "protein", 1);
    }
}