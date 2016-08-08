using Sesion2_Lab01.com.game.configuration;
using Sesion2_Lab01.com.game.configuration.data;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_World {

        public const int TILE_EMPTY = 0;
        public const int TILE_BLOCK = 1;
        public const int TILE_PLAYER = 9;

        private TB3D_WorldPhysics mWorldPhysics;
        private TB3D_Engine mEngine;
        private float mTileSize;

        private List<NPrimitiveCube3D> mCubes;

        public float TileSize                   { get { return mTileSize; } }
        public TB3D_WorldPhysics WorldPhysics   { get { return mWorldPhysics; } }
        public TB3D_Engine Engine               { get { return mEngine; } }

        public TB3D_World(TB3D_Engine engine) {
            mEngine = engine;

            mCubes = new List<NPrimitiveCube3D>();


            int[,] worldCollisions = mEngine.WConfig.WorldCollisions;
            mTileSize = mEngine.WConfig.TileSize;

            int rows = worldCollisions.GetLength(0);
            int cols = worldCollisions.GetLength(1);

            for (int c = 0; c < cols; c++) {
                for (int r = 0; r < rows; r++) {
                    int tileData = worldCollisions[r, c];

                    this.CreateObject(tileData, c, r);
			    }
            }

            mWorldPhysics = new TB3D_WorldPhysics(this, worldCollisions);
        }

        private void CreateObject(int tileID, int c, int r) {
            float posX = c * mTileSize;
            float posY = 0f;
            float posZ = r * mTileSize;

            switch (tileID) {
            case TB3D_World.TILE_BLOCK:
                NPrimitiveCube3D cube = new NPrimitiveCube3D(posX, posY, posZ, mTileSize);
                cube.RotationX = 3.14f;
                cube.RotationZ = 3.14f;
                mCubes.Add(cube);
                break;
            case TB3D_World.TILE_EMPTY:
                //NPrimitiveCube3D cube_empty = new NPrimitiveCube3D(posX, posY, posZ, mTileSize);
                //cube_empty.AmbientColor = Color.Green;
                //cube_empty.ScaleY = 0.01f;

                //mCubes.Add(cube_empty);
                break;
            case TB3D_World.TILE_PLAYER:
                mEngine.Player.SetPosition(posX + mTileSize / 2, posY, posZ + mTileSize / 2);
                break;
            }
        }
        
        public void UpdateDraw(int dt) {
            for (int i = 0, length = mCubes.Count; i < length; i++) {
                mCubes[i].UpdateAndDraw(mEngine.Camera.RenderCamera, dt);
            }
        }
    }
}
