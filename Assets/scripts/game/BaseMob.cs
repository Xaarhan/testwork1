using UnityEngine;
using System.Collections.Generic;

public class BaseMob
{

    public const int STATE_STAY = 0;
    public const int STATE_ATTACK = 1;


    public BaseMob() {
        _buffs = new List<BaseBuff>();
        _state = STATE_STAY;
    }

    public void init( Propmob p ) {
        _isDead = false;
        props = p;
        if (anim != null) anim.remove();

        anim = new AnimObject();
        anim.init(p.model);
        anim.playAnim("basePlayer_Idle");

        if ( _lifebar == null) {
             _lifebar = new LifeBar();
        }
        uiChanged++;
    }


    public void update() {
        if (_isDead) return;
        updateLifebar();

        for ( int i = 0; i < _buffs.Count; i++ ) {
              BaseBuff buff = _buffs[i];
              buff.update();
              if ( buff.duration <= 0 ) {
                   dissapplyBuff(buff);
                   i--;
              }               
        }

        if ( _cooldown > 0 ) {
             _cooldown = _cooldown - Main.delta;
        }

        switch (state) {
            case STATE_STAY: {
                updateStay();
                break;
            }

            case STATE_ATTACK: {
                updateAttack();
                break;
            }


            default: {

                break;
            }
        }
        
    }


    public void updateStay() {

    }

    private void updateLifebar() {
        _lifebar.updateLifes(props.maxLifes, props.lifes);

        Vector3 pos3d = anim.transform.position;
        pos3d.y += 4f;
        _lifebar.setPos3D(pos3d);
    }

    public void doAttack() {
        if (!canAttack()) return;
        _cooldown = props.cooldown;
        _atime = props.atime;
        _state = STATE_ATTACK;
        anim.playAnim("basePlayer_attack");
    }


    public bool canAttack() {
        if ( _isDead || _target == null || _target.isDead() || _cooldown > 0) return false;
        return true;
    }


    public int takeDamage( StatProps dmg, BaseMob owner ) {
        int armor = props.armor;
        int damage = (int)( dmg.value * ( 100 - props.armor) * 0.01f);
        lifes -= damage;
        return damage;
    }


    public int lifes {
        set {
            if (value == props.lifes) return;
            if (value < 0) value = 0;
            if (value > props.maxLifes) value = props.maxLifes;
            int diff = value - props.lifes;
            props.lifes = value;
            _lifebar.updateLifes(props.maxLifes, props.lifes);
            spawnFlyText(diff);
            if (value <= 0) {
                dead();
            } else {
                if (_isDead) {
                    _isDead = false;
                    anim.playAnim("basePlayer_idle");
                }
            }
        }
        get {
            return props.lifes;
        }
    }


    private void spawnFlyText( int val ) {
        GameObject flytext = Main.shared.addGameObject( "ui/flyingtext");
        FlyTextHelper helper = flytext.GetComponent<FlyTextHelper>();
        helper.transform.SetParent(Main.shared.uiBottomLayer);
        helper.setValue(val);
        helper.time = 1000;
        Vector3 pos3d = anim.transform.position;
        pos3d.y += 4f;
        helper.pos3d = pos3d;
    }


    private void updateAttack() {
        if ( _atime > 0 ) {
             _atime = _atime - Main.delta;
             if ( _atime <= 0) {
                  attackComplete();
             }
        }
    }


    private void attackComplete() {
        if (_target == null) return;
        int dmg = _target.takeDamage(props.getStat(StatsId.DAMAGE_ID), this);

        StatProps lifesteal = props.getStat(StatsId.LIFE_STEAL_ID);
        int stealedLifes = (int)(dmg * lifesteal.value * 0.01f);
        lifes += stealedLifes;

        _state = STATE_STAY;
    }


  


    public void setTarget( BaseMob mob ) {
        _target = mob;
        if (_target == null) return;
        lookTo(_target.anim.transform.position);
    }


    public void lookTo( Vector3 point ) {
        anim.transform.LookAt(point);
    }

    public void onRemove() {
        anim.remove();
        _lifebar.remove();
    }

    public void dead() {
        _isDead = true;
        _lifebar.visible = false;
        anim.playAnim("basePlayer_death");
    }


    public bool isDead() {
        return _isDead;
    }
    private bool _isDead;


    public void applyBuff( BaseBuff buff ) {
        buff.onApply(this);
        _buffs.Add(buff);
        uiChanged++;
    }


    public void dissapplyBuff(BaseBuff buff) {
        buff.onDissapply();
        _buffs.Remove(buff);
        uiChanged++;
    }


    public void removeAllBuffs() {
        while ( _buffs.Count > 0) { 
            dissapplyBuff(_buffs[0]);
        }
    }


    public BaseMob target {
        get {
            return _target;
        }
    }


    public int state {
        get {
            return _state;
        }
    }

    private LifeBar _lifebar;

    private BaseMob _target;
    private int _state;
    private int _atime;
    private int _cooldown;


    public int uiChanged;  // счетчик увеличивается если надо обновить UI
    public bool needUpdateStats;

    public List<BaseBuff> _buffs;

    public Propmob props;
    public AnimObject anim;

    private Animator _animator;
    

}
