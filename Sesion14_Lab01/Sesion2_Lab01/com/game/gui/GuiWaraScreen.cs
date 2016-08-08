using Sesion2_Lab01.com.isil.content;
using Sesion2_Lab01.com.isil.render.camera;
using Sesion2_Lab01.com.isil.render.components;
using Sesion2_Lab01.com.isil.shader.d2d;
using Sesion2_Lab01.com.isil.system.screenManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.gui {

    public class GuiWaraScreen : Screen {

        private RenderCamera mRenderCamera;

        public GuiWaraScreen() : base() {
            mRenderCamera = NativeApplication.instance.RenderCamera;
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(int dt) {
            base.Update(dt);


        }

        public override void Draw(int dt) {
            base.Draw(dt);

        }
    }
}
