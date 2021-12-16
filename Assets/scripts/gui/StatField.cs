using UnityEngine;

public class StatField {

    public StatField() {
        anim = Main.shared.addGameObject("ui/statparams");
        _helper = anim.GetComponent<StatParamHelper>();
    }


    public void init( IStatParam p ) {
        props = p;
        _helper.icon.sprite = Resources.Load<Sprite>("Icons/" + props.getIcon());
        updateValues();
    }

    public void destroy() {
        Main.shared.removeGameObject(anim);
        props = null;
        _helper = null;
    }

    public void update() {
        if ( props.changed() != _lastChanged) {
             _lastChanged = props.changed();
             updateValues();
        }
    }


    public void updateValues() {
        _helper.label.text = props.getText();
    }

    private int _lastChanged;
    public IStatParam props;
    private StatParamHelper _helper;
    public GameObject anim;



}
