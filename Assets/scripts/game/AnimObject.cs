using UnityEngine;
using System.Collections;

public class AnimObject
{

    public AnimObject() {

    }


    public void init( string model_name  ) {
        model = Main.shared.addGameObject(model_name);
        _animator = model.GetComponent<Animator>();
        transform = model.transform;
    }


    public void playAnim(string anim_name) {
        _animator.Play(anim_name);
    }


    public void remove() {
        Main.shared.removeGameObject(model);
    }

    private Animator _animator;
    public GameObject model;
    public Transform transform;

   
}
