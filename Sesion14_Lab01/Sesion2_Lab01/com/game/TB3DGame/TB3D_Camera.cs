using Sesion2_Lab01.com.isil.render.camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_Camera {

        private RenderCamera mRenderCamera;

        public RenderCamera RenderCamera { get { return mRenderCamera; } }

        public TB3D_Camera(TB3D_Engine engine) {
            mRenderCamera = NativeApplication.instance.RenderCamera;
        }
    }
}
