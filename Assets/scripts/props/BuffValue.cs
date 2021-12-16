using System.Json;

public class BuffValue  {

    public static BuffValue fromJson( JsonObject json ) {
        BuffValue val = new BuffValue();
        val.type = json["statId"];
        val.value1 = json["value"];
        return val;
    }

    public BuffValue clone() {
        BuffValue clon = new BuffValue();
        clon.type = type;
        clon.value1 = value1;
        return clon;
    }

    public int type;
    public int value1;

}
