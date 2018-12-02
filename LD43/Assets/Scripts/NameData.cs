/* 
 * Class used to convert from JSON to name list
 */


[System.Serializable]
public class NameData {
    public NameItem[] names;
}

[System.Serializable]
public class NameItem {
    public string name;
}
