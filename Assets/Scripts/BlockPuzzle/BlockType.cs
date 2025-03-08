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

    // FRUITS VEGETABLES

    public static BlockType apple() {
        return new BlockType("apple", "veg", 1, new bool[][] {
            new bool[] {true, true},
            new bool[] {true, true}
        });
    }

    public static BlockType mushroom() {
        return new BlockType("mushroom", "veg", 1, new bool[][] {
            new bool[] {true, true, true},
            new bool[] {false, true, false}
        });
    }

    public static BlockType banana() {
        return new BlockType("banana", "veg", 1, new bool[][] {
            new bool[] {true, false, false},
            new bool[] {true, true, true}
        });
    }

    public static BlockType lettuce() {
        return new BlockType("lettuce", "veg", 1, new bool[][] {
            new bool[] {false, false, true},
            new bool[] {false, true, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType lettuce2() {
        return new BlockType("lettuce2", "veg", 1, new bool[][] {
            new bool[] {false, true, false},
            new bool[] {true, true, false},
            new bool[] {true, true, true}
        });
    }

    public static BlockType cucumber() {
        return new BlockType("cucumber", "veg", 1, new bool[][] {
            new bool[] {true},
            new bool[] {true},
            new bool[] {true}
        });
    }

    public static BlockType cabbage() {
        return new BlockType("cabbage", "veg", 1, new bool[][] {
            new bool[] {true, true, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType broccoli() {
        return new BlockType("broccoli", "veg", 1, new bool[][] {
            new bool[] {true, true, true},
            new bool[] {false, true, false},
            new bool[] {false, true, false}
        });
    }

    public static BlockType eggplant() {
        return new BlockType("eggplant", "veg", 1, new bool[][] {
            new bool[] {true, false},
            new bool[] {true, true}
        });
    }

    public static BlockType carrot() {
        return new BlockType("carrot", "veg", 1, new bool[][] {
            new bool[] {true},
            new bool[] {true}
        });
    }

    // CARB

    public static BlockType noodle() {
        return new BlockType("noodle", "carb", 1, new bool[][] {
            new bool[] {true, true, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType rice() {
        return new BlockType("rice", "carb", 1, new bool[][] {
            new bool[] {true, false, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType rice2() {
        return new BlockType("rice2", "carb", 1, new bool[][] {
            new bool[] {false, true},
            new bool[] {true, true},
            new bool[] {true, true}
        });
    }

    public static BlockType corn() {
        return new BlockType("corn", "carb", 1, new bool[][] {
            new bool[] {true},
            new bool[] {true},
            new bool[] {true}
        });
    }

    public static BlockType bread() {
        return new BlockType("bread", "carb", 1, new bool[][] {
            new bool[] {false, true},
            new bool[] {false, true},
            new bool[] {true, true}
        });
    }

    public static BlockType bread2() {
        return new BlockType("bread2", "carb", 1, new bool[][] {
            new bool[] {true, false, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType bread3() {
        return new BlockType("bread3", "carb", 1, new bool[][] {
            new bool[] {true},
            new bool[] {true},
            new bool[] {true},
            new bool[] {true}
        });
    }

    public static BlockType bread4() {
        return new BlockType("bread4", "carb", 1, new bool[][] {
            new bool[] {true, false, false},
            new bool[] {true, true, false},
            new bool[] {false, true, true}
        });
    }

    public static BlockType bread5() {
        return new BlockType("bread5", "carb", 1, new bool[][] {
            new bool[] {false, true},
            new bool[] {false, true},
            new bool[] {true, true},
            new bool[] {true, false}
        });
    }

    // PROTEINS

    public static BlockType chickenLeg() {
        return new BlockType("chicken leg", "protein", 1, new bool[][] {
            new bool[] {true, false},
            new bool[] {true, true}
        });
    }

    public static BlockType chickenBreast() {
        return new BlockType("chicken breast", "protein", 1, new bool[][] {
            new bool[] {false, true, false},
            new bool[] {true, true, false},
            new bool[] {true, true, true}
        });
    }

    public static BlockType ham() {
        return new BlockType("ham", "protein", 1, new bool[][] {
            new bool[] {true, true, true},
            new bool[] {false, true, true}
        });
    }

    public static BlockType fish() {
        return new BlockType("fish", "protein", 1, new bool[][] {
            new bool[] {true, true, false},
            new bool[] {false, true, true}
        });
    }

    public static BlockType fish2() {
        return new BlockType("fish2", "protein", 1, new bool[][] {
            new bool[] {true, false, false},
            new bool[] {true, true, false},
            new bool[] {false, true, true}
        });
    }

    public static BlockType bean() {
        return new BlockType("bean", "protein", 1, new bool[][] {
            new bool[] {true, true},
            new bool[] {true, true}
        });
    }

    public static BlockType bacon() {
        return new BlockType("bacon", "protein", 1, new bool[][] {
            new bool[] {true},
            new bool[] {true}
        });
    }

    public static BlockType lambLeg() {
        return new BlockType("lamb leg", "protein", 1, new bool[][] {
            new bool[] {false, false, true},
            new bool[] {false, false, true},
            new bool[] {true, true, true}
        });
    }

    public static BlockType cheese() {
        return new BlockType("cheese", "protein", 1, new bool[][] {
            new bool[] {true, true},
            new bool[] {true, true},
            new bool[] {true, true}
        });
    }
}