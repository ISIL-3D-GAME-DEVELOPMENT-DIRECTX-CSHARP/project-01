using Sesion2_Lab01.com.isil.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {

    public class TB3D_PlayerControl {

        private TB3D_Player mPlayer;
        
        private bool mCanGoLeft;
        private bool mCanGoRight;
        private bool mCanGoForward;
        private bool mCanGoBackward;

        private float mPlayerSpeed;

        private int mDirectionX;
        private int mDirectionY;

        public TB3D_PlayerControl(TB3D_Player player) {
            mPlayer = player;

            mCanGoLeft = false;
            mCanGoRight = false;
            mCanGoForward = false;
            mCanGoBackward = false;

            mDirectionX = 0;
            mDirectionY = 0;

            mPlayerSpeed = 0.01f;
        }

        public void OnKeyDown(int keyCode) {
            switch (keyCode) {
            case EnumNKeyCode.Left:
                mCanGoBackward = true;
                mDirectionX = -1;
                break;
            case EnumNKeyCode.Right:
                mCanGoForward = true;
                mDirectionX = 1;
                break;
            case EnumNKeyCode.Up:
                mCanGoLeft = true;
                mDirectionY = -1;
                break;
            case EnumNKeyCode.Down:
                mCanGoRight = true;
                mDirectionY = 1;
                break;
            }
        }

        public void OnKeyUp(int keyCode) {
            switch (keyCode) {
            case EnumNKeyCode.Left:
                mCanGoBackward = false;
                break;
            case EnumNKeyCode.Right:
                mCanGoForward = false;
                break;
            case EnumNKeyCode.Up:
                mCanGoLeft = false;
                break;
            case EnumNKeyCode.Down:
                mCanGoRight = false;
                break;
            }
        }

        public void Update(int dt) {
            float newX = 0;
            float newY = 0;

            mPlayer.ComputeNewPosition(mPlayerSpeed * dt, mDirectionX, mDirectionY, out newX, out newY);

            mPlayer.X = newX;
            mPlayer.Z = newY;
            
            if (!mCanGoForward && !mCanGoBackward) {  mDirectionX = 0; }
            if (!mCanGoLeft && !mCanGoRight) { mDirectionY = 0; }
        }
    }
}
