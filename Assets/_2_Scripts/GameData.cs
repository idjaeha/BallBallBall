using System.Collections;
using System.Collections.Generic;

public static class GameData
{
    public static float playtime = 0.0f;
    private static bool _gameover = false;
    public static float mapSizeX
    {
        get; set;
    }
    public static float mapSizeY
    {
        get; set;
    }
    public static bool gameover
    {
        get => _gameover;
        set => _gameover = value;
    }
}
