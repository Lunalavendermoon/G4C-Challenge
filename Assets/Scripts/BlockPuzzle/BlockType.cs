using System;
using NUnit.Framework.Constraints;

public class BlockType {
    public string name;

    public string foodGroup;

    public int size;

    public bool[][] shape;

    public BlockType(string _name, string _foodGroup, int _size, bool[][] _shape) {
        name = _name;
        foodGroup = _foodGroup;
        size = _size;

        shape = _shape;
    }

    public static BlockType apple() {
        return new BlockType("apple", "veg", 1, new bool[][] {
            new bool[] {true, true},
            new bool[] {true, true}
        });
    }

    public static BlockType rice() {
        return new BlockType("rice", "carb", 2, new bool[][] {
            new bool[] {true, false, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType chicken() {
        return new BlockType("chicken", "protein", 1, new bool[][] {
            new bool[] {true, false},
            new bool[] {true, true}
        });
    }
}