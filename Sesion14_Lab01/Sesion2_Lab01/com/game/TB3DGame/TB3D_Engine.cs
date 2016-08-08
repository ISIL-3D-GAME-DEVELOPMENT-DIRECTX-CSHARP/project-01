using Sesion2_Lab01.com.game.configuration;
using Sesion2_Lab01.com.game.configuration.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_Engine {

        private float mTileSize;
        private TB3D_WorldConfig mWConfig;

        private TB3D_Game mGame;
        private TB3D_World mWorld;
        private TB3D_Camera mCamera;
        private TB3D_Player mPlayer;

        public TB3D_Camera Camera   { get { return mCamera; } }
        public TB3D_Player Player   { get { return mPlayer; } }
        public TB3D_World World     { get { return mWorld; } }

        public TB3D_WorldConfig WConfig { get { return mWConfig; } }
        public float TileSize           { get { return mTileSize; } }

        public TB3D_Engine(TB3D_Game game, string levelID) {
            mGame = game;

            mWConfig = TB3D_Configuration.Instance.GetWorld(levelID);

            mTileSize = mWConfig.TileSize;

            mCamera = new TB3D_Camera(this);
            mPlayer = new TB3D_Player(this);
            mWorld = new TB3D_World(this);
        }

        public void OnKeyDown(int keyCode) {
            mPlayer.OnKeyDown(keyCode);

        }

        public void OnKeyUp(int keyCode) {
            mPlayer.OnKeyUp(keyCode);

        }

        public void UpdateDraw(int dt) {
            mPlayer.UpdateDraw(dt);
            mWorld.UpdateDraw(dt);
        }
    }
}
