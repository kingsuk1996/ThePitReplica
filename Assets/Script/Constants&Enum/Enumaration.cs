namespace RedApple.ThePit
{
    public enum GameState
    {
        NotStart,
        Instruction,
        Start,
        Finish
    }

    public enum PoolObjectType
    {
        Road1,
        Road2,
        Road3,
        Spike1,
        Spike2,
        Spike3,
        BigStone,
        Door,
        Door3,
        Blade,
        Blade3,
        SlidingBox,
        FloorSpike,
        Pipe,
        CoinChunk1,
        CoinChunk2,
        none,
        InstructionRoad
    }

    public enum InstructionState
    {
        jump,
        doublejump,
        slide
    }

    public enum GameMode
    {
        easy,
        medium,
        hard
    }
}