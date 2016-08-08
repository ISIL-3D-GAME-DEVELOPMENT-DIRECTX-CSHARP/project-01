using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_Player : TB3D_Actor {

        private TB3D_PlayerControl mPlayerControl;

        public TB3D_Player(TB3D_Engine engine)
            : base(engine, "Content/magician/magician.X", 
            "Content/magician/", true) {

            this.GotoAnimation("Run", true);

            this.ScaleX = this.ScaleY = this.ScaleZ = 0.48f;
            this.RotationX = (float)(Math.PI / 2f);

            mEngine.Camera.RenderCamera.LeftRightRotation = -0.0300000086f;
            mEngine.Camera.RenderCamera.UpDownRotation = -0.7549999f;
            Vector3 posCamera = mEngine.Camera.RenderCamera.Position;
            posCamera.X = 8.243079f;
            posCamera.Y = 18.89694f;
            posCamera.Z = 27.41047f;
            mEngine.Camera.RenderCamera.Position = posCamera;

            mPlayerControl = new TB3D_PlayerControl(this);
            
            this.CreateCollision(1f);

            mDebugMode = true;
            mOffsetX = -(mEngine.TileSize / 2);
            mOffsetZ = -(mEngine.TileSize / 2);
        }

        public void SetPosition(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public void OnKeyDown(int keyCode) {
            mPlayerControl.OnKeyDown(keyCode);
        }

        public void OnKeyUp(int keyCode) {
            mPlayerControl.OnKeyUp(keyCode);
        }

        public override void UpdateDraw(int dt) {
            base.UpdateDraw(dt);

            mPlayerControl.Update(dt);
        }
    }
}
