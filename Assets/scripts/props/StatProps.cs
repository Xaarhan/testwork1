using UnityEngine;
using System.Collections;
using System.Json;

public class StatProps : IStatParam
{

    public StatProps() {

    }

    public void fromJson(JsonObject data) {
        id = data["id"];
        value = data["value"];
        maxvalue = value;
        icon = data["icon"];
        name = data["title"];
    }

    public StatProps clone() {
        StatProps clon = new StatProps();
        clon.id = id;
        clon.value = value;
        clon.name = name;
        clon.icon = icon;
        clon.maxvalue = maxvalue;
        return clon;
    }


    public string getText() {
        return value.ToString();
    }

    public string getIcon() {
        return icon;
    }


    
    public int changed() {
        return _changed;
    }

    public void onChange() {
        _changed++;
    }

    private int _changed;

    public int id;
    public int value;
    public int maxvalue;
    public string name;
    public string icon;

}
