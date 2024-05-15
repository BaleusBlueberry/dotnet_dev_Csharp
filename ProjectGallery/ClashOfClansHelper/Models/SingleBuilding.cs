namespace ClashOfClansHelper.Controls;


public class SingleBuilding
{
    public string level { get; set; }
    public string picture { get; set; }
    public Dictionary<string, string> bulding { get; set; }

    public SingleBuilding(string level, string picture, Dictionary<string, string> bulding)
    {
        this.level = level;
        this.picture = picture;
        this.bulding = bulding;
    }
}


