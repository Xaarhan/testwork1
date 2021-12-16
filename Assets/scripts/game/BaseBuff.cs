using UnityEngine;
using System.Collections;

public class BaseBuff 
{

    public BaseBuff() {

    }


    public virtual void init(BuffProps p) {
        props = p;
    }


    public virtual void update() {
        duration -= Main.delta;
    }


    public virtual void onApply( BaseMob mob ) {
        target = mob;
    }


    public virtual void onDissapply() {
        target = null;
    }


    public int duration;
    public BaseMob target;
    public BuffProps props;


}
