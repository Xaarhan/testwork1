using UnityEngine;
using System.Collections.Generic;
using System.Json;

public class BuffProps : IStatParam
{

    public BuffProps() {
        values = new List<BuffValue>();
        name = "emptybuff";
    }

    public void fromJson( JsonObject data ) {
        id = data["id"];
        name = data["title"];
        icon = data["icon"];

        JsonArray jsonvalues = data["stats"] as JsonArray;
        for ( int i = 0; i < jsonvalues.Count; i++) {
              values.Add( BuffValue.fromJson(jsonvalues[i] as JsonObject) );
        }
    }

    public BuffProps clone() {
        BuffProps clon = new BuffProps();
        clon.id = id;
        clon.name = name;
        clon.icon = icon;
        for ( int i = 0; i < values.Count; i++) {
              clon.values.Add( values[i].clone() );
        }
        return clon;
    }


    public string getText() {
        return name;
    }

    public string getIcon() {
        return icon;
    }

    public int changed() {
        return 0;
    }

    

    public int id;
    public List<BuffValue> values;
    public string name;
    public string icon;

}
