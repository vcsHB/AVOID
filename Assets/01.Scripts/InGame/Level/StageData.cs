[System.Serializable]
public class StageData
{
    public int id;
    public bool isOpened;
    public bool isCleared;

    public StageData(int id)
    {
        this.id = id;
    }
    
}