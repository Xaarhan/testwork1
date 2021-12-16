using UnityEngine;
using System.Collections.Generic;

public class StatsBuff : BaseBuff 
{

    public StatsBuff() {

    }



    public override void onApply( BaseMob mob ) {
        base.onApply(mob);
        List<BuffValue> values = props.values;
        for ( int i = 0; i < values.Count; i++) {
            BuffValue val = values[i];
            StatProps stat = mob.props.getStat(val.type);
            if (stat != null) {
                stat.value += val.value1;
                stat.maxvalue += val.value1;
                stat.onChange();
            }
        }
    }


    public override void onDissapply() {
        List<BuffValue> values = props.values;
        for (int i = 0; i < values.Count; i++) {
            BuffValue val = values[i];
            StatProps stat = target.props.getStat(val.type);
            if (stat != null) {
                stat.value -= val.value1;
                stat.maxvalue -= val.value1;
                stat.onChange();
            }
            
        }
        target = null;
    }





}
