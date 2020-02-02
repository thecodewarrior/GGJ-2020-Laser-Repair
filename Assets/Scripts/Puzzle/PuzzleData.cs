using System;

namespace Puzzle
{
    [Serializable]
    public class PuzzleData
    {
        public int width;
        public int height;
        public PuzzleObjectData[] objects;
    }

    [Serializable]
    public class PuzzleObjectData
    {
        public float x;
        public float y;
        public float rotation;
        public string type;
        public string[] data;
    }
}