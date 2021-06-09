using DefaultNamespace;

public class DataManager
{
    public LevelsData LevelsData { get; private set; }
    public PlayerData PlayerData { get; private set; }

    public DataManager()
    {
        LevelsData = new LevelsData();
        PlayerData = new PlayerData();
    }
}