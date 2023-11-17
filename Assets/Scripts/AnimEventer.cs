using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWITHFACTORY.CYJ
{

    public class AnimEventer : MonoBehaviour
    {
        Player_S2 _player;
        Player_S2_2 _player_S2_2;
        Player_IVShot _player_IVShot;
 
        private void Awake()
        {
            _player = FindObjectOfType<Player_S2>();
            _player_S2_2 = FindObjectOfType<Player_S2_2>();
            _player_IVShot = FindObjectOfType<Player_IVShot>();
        }

        public void End()
        {
            if (_player) _player.FadeOutEnd();
            else if (_player_S2_2) _player_S2_2.FadeOutEnd();
            else if (_player_IVShot) _player_IVShot.FadeOutEnd();
        }
    }



}
