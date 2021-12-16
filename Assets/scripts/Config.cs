using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Json;

public class Config  {
    // Start is called before the first frame update
    public Config() {

        shared = this;

        TextAsset data = Resources.Load("data", typeof(TextAsset)) as TextAsset;
        jsondata = JsonObject.Parse(data.text) as JsonObject;

        _allstats = new Dictionary<int, StatProps>();

        JsonArray jsonstats = jsondata["stats"] as JsonArray;
        for ( int i = 0; i < jsonstats.Count; i++) {
              JsonObject jstat = jsonstats[i] as JsonObject;
              StatProps stat = new StatProps();
              stat.fromJson(jstat);
              _allstats.Add(stat.id, stat );
        }

        _allBuffs = new Dictionary<int, BuffProps>();
        JsonArray jsonbuffs = jsondata["buffs"] as JsonArray;
        for ( int i = 0; i < jsonbuffs.Count; i++) {
              JsonObject jsonbuff = jsonbuffs[i] as JsonObject;
              BuffProps buff = new BuffProps();
              buff.fromJson(jsonbuff);
              _allBuffs.Add(buff.id, buff);
        }

        _settings = jsondata["settings"] as JsonObject;

    }


    public List<BuffProps> getRandomBuffs( int count, bool duplicateBuffs = false ) {
        List<int> all_indexes = new List<int>();
        List<BuffProps> result = new List<BuffProps>();
        foreach (KeyValuePair<int, BuffProps> k in _allBuffs) {
            all_indexes.Add(k.Key);
        }

        int i = 0;
        while ( i < count && all_indexes.Count > 0 ) {
            int rnd = (int)(all_indexes.Count * UnityEngine.Random.value);
            int id = all_indexes[rnd];
            result.Add( _allBuffs[id].clone());
            if ( duplicateBuffs == false ) {
                 all_indexes.RemoveAt(rnd);
            }
            i++;
        }

        return result;
    }


    public Dictionary<int, StatProps> getAllStats() {
        Dictionary<int, StatProps> stats = new Dictionary<int, StatProps>();
        foreach(KeyValuePair<int, StatProps> k in _allstats) {
            stats.Add(k.Key, k.Value.clone());
        }
        return stats;
    }

    public StatProps getStat( int id ) {
        if ( _allstats.ContainsKey(id) ) {
             return _allstats[id].clone();
        }
        return null;
    }

    public BuffProps getBuff(int id) {
        if (_allBuffs.ContainsKey(id)) {
            return _allBuffs[id].clone();
        }
        return null;
    }

    public JsonObject getSettings() {
        return _settings;
    }


    private Dictionary<int, StatProps> _allstats;
    private Dictionary<int, BuffProps> _allBuffs;

    public static Config shared;
    private JsonObject jsondata;
    private JsonObject _settings;


   

}
