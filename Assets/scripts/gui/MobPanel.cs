using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MobPanel  {

    public MobPanel() {
        _fields = new List<StatField>();
        anim = Main.shared.addGameObject("ui/mobpanel");
        _helper = anim.GetComponent<MobMenuHelper>();
        _helper.attackBtn.onClick.AddListener(onAttackBtn);
    }

    public void update() {
        if ( _selected == null ) return;
        if ( _mobUiChanged != _selected.uiChanged) {
             updateFields();
        }
        for ( int i = 0; i< _fields.Count; i++) {
              _fields[i].update();
        }
    }


    public void selectMob( BaseMob mob ) {
        _selected = mob;
        updateFields();
    }

    public void updateFields() {
        clearFields();
        Propmob props = _selected.props;
        Dictionary<int, StatProps> stats = props.stats;
        foreach (KeyValuePair<int, StatProps> k in stats) {
            StatProps s = k.Value;
            StatField field = new StatField();
            field.init(s);
            addField(field);
        }

        for ( int i = 0; i < _selected._buffs.Count; i++ ) {
              BuffProps b = _selected._buffs[i].props;
              StatField field = new StatField();
              field.init(b);
              addField(field);
        }
    }


    public void clearFields() {
        for (int i = 0; i < _fields.Count; i++ ) {
            _fields[i].destroy();
        }
        _fields = new List<StatField>();
    }


    private void addField(StatField field) {
        _fields.Add(field);
        field.anim.transform.SetParent(_helper.statsPanel);
    }


    private void onAttackBtn() {
        if ( _selected != null ) {
             _selected.doAttack();
        }
    }



    private int _mobUiChanged;

    public GameObject anim;
    private BaseMob _selected;
    private MobMenuHelper _helper;
    private List<StatField> _fields;

}
