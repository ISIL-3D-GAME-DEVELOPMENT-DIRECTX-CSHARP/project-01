using Core.Model;
using Sesion2_Lab01.com.game.configuration;
using Sesion2_Lab01.com.game.TB3DGame;
using Sesion2_Lab01.com.isil.data;
using Sesion2_Lab01.com.isil.math;
using Sesion2_Lab01.com.isil.render.camera;
using Sesion2_Lab01.com.isil.system.screenManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.gui {
    public class GuiGame_AntiVodo : Screen {

        private TB3D_Game mGame;

        public GuiGame_AntiVodo()
            : base() { 

            TB3D_Configuration.New();

            
        }

        public override void Initialize() {
            base.Initialize();

            mGame = new TB3D_Game(this);
        }

        public override void OnKeyDown(int keyCode) {
            base.OnKeyDown(keyCode);

            mGame.OnKeyDown(keyCode);
        }

        public override void OnKeyUp(int keyCode) {
            base.OnKeyUp(keyCode);

            mGame.OnKeyUp(keyCode);
        }

        public override void Draw(int dt) {
            mGame.UpdateDraw(dt);

            base.Draw(dt);
        }
    }
}
