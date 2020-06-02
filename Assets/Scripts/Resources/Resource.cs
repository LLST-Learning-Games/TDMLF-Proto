
[System.Serializable]
public class Resource
{

    public ResourceType rType;
    public int amt;

    public Resource(ResourceType setRType, int setAmt)
    {
        rType = setRType;
        amt = setAmt;
    }



}