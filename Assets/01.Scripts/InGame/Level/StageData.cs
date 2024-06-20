[System.Serializable]
public class StageData
{
    public int id;
    public int moveCount;
    public bool isOpened;
    public bool isCleared;
    public bool dieCount;
    
    public StageData(int id)
    {
        this.id = id;
    }
    
}