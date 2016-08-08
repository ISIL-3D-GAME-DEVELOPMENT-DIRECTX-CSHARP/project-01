using Sesion2_Lab01.com.isil.system.screenManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_Game {

        private TB3D_Engine mEngine;
        private Screen mScreenParent;

        public TB3D_Game(Screen screenParent) {
            mScreenParent = screenParent;

            mEngine = new TB3D_Engine(this, "0");
        }

        public void OnKeyDown(int keyCode) {
            mEngine.OnKeyDown(keyCode);

        }

        public void OnKeyUp(int keyCode) {
            mEngine.OnKeyUp(keyCode);

        }

        public void UpdateDraw(int dt) {
            mEngine.UpdateDraw(dt);
        }
    }
}
