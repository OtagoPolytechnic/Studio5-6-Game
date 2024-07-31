public class ShopItem 
{
    public float value;
    bool percentageUpgrade;
    public float cost;


    public void UpgradeStat(float stat)
    {
        if (percentageUpgrade)
        {
            stat *= value;
        }
        else
        {
            stat += value;
        }
    }
}