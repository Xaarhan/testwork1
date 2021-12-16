using UnityEngine;
using System.Collections.Generic;
using System.Json;

public class Propmob
{

    public Propmob() {
        atime = 250;
        cooldown = 500;
        stats = new Dictionary<int, StatProps>();
    }

    
    public void addStat( StatProps s ) {
        stats.Add(s.id, s);
    }

    public StatProps getStat( int type ) {
        if ( !stats.ContainsKey(type) ) {
              return null;
        }
        return stats[type];
    }


    public Propmob clone() {
        Propmob clon = new Propmob();
        clon.model = model;
        for ( int i = 0; i < stats.Count; i++ ) {
              addStat( stats[i].clone() );
        }
        return clon;
    }


    public int armor {
        get {
            StatProps ar = getStat(StatsId.ARMOR_ID);
            if (ar != null) return ar.value;
              else return 0; 
        }
    }

    public int lifes {
        get {
            StatProps ar = getStat(StatsId.LIFE_ID);
            if (ar != null) return ar.value;
            else return 0;
        }
        set {
            StatProps ar = getStat(StatsId.LIFE_ID);
            if (ar == null) return;
            if (ar.value == value) return;
            ar.value = value;
            ar.onChange();
        }
    }

    public int maxLifes {
        get {
            StatProps ar = getStat(StatsId.LIFE_ID);
            if (ar != null) return ar.maxvalue;
            else return 0;
        }
        set {
            StatProps ar = getStat(StatsId.LIFE_ID);
            if (ar != null) return;
            ar.maxvalue = value;
        }
    }



    public Dictionary< int, StatProps> stats;
    public string model;
    public int atime; // время до нанесения урона
    public int cooldown; // перезарядка

}
