using UnityEngine;

public class NamedValues : MonoBehaviour
{
    public string[] Names;
    public string[] Values;

    public string Get(string name)
    {
        for (int i = 0; i < Names.Length; ++i)
        {
            if (Names[i] == name)
            {
                return Values[i];
            }
        }
        return null;
    }

    public int GetAsInt(string name)
    {
        string v = Get(name);
        if(v != null)
        {
            return int.Parse(v);
        }
        else
        {
            return 0;
        }
    }
}
