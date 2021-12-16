using UnityEngine;
using System.Collections.Generic;

public class BaseWorld {

    public BaseWorld() {
        _mobs = new List<BaseMob>();
    }


    public void update() {
        for ( int i = 0; i < _mobs.Count; i++ ) {
              _mobs[i].update();
        }
    }


    public BaseMob spawnMob(Propmob props) {
        BaseMob mob = new BaseMob();
        mob.init(props);
        addMob(mob);
        return mob;
    }


    public void addMob( BaseMob mob ) {       
        _mobs.Add(mob);
    }


    public void removeMob(BaseMob mob) {
        for (int i = 0; i < _mobs.Count; i++) {
             if ( _mobs[i].target == mob ) {
                  _mobs[i].setTarget(null);
            }
        }

        mob.onRemove();
        _mobs.Remove(mob);
    }

    public void clear() {
        for (int i = 0; i < _mobs.Count; i++) {
            _mobs[i].onRemove();
        }
        _mobs = new List<BaseMob>();
    }



    private List<BaseMob> _mobs;

}
