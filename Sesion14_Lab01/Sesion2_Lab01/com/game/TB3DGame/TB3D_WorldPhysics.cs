using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_WorldPhysics {

        private TB3D_World mWorld;
        private int[,] mCollisionData;

        public TB3D_WorldPhysics(TB3D_World world, int[,] collisionData) {
            mWorld = world;
            mCollisionData = collisionData;
        }

        public void GetCorners(float size, float x, float y, TB3D_Actor actor) {
            float cubeSize = size;
            float offsetSize = 0.1f; // don't move this, is a constant

            int tileX = (int)Math.Floor(x / mWorld.Engine.TileSize);
            int tileY = (int)Math.Floor(y / mWorld.Engine.TileSize);

            actor.TileDownY = (int)Math.Floor((y + cubeSize - offsetSize) / mWorld.TileSize);
            actor.TileUpY = (int)Math.Floor((y - cubeSize) / mWorld.TileSize);
            actor.TileLeftX = (int)Math.Floor((x - cubeSize) / mWorld.TileSize);
            actor.TileRightX = (int)Math.Floor((x + cubeSize - offsetSize) / mWorld.TileSize);

            if (actor.TileDownY < 0) { actor.TileDownY = 0; }
            if (actor.TileUpY < 0) { actor.TileUpY = 0; }
            if (actor.TileLeftX < 0) { actor.TileLeftX = 0; }
            if (actor.TileRightX < 0) { actor.TileRightX = 0; }

            //check if they are walls
            actor.CanGoUpLeft = mCollisionData[actor.TileUpY, actor.TileLeftX] != 1;
            actor.CanGoDownLeft = mCollisionData[actor.TileDownY, actor.TileLeftX] != 1;
            actor.CanGoUpRight = mCollisionData[actor.TileUpY, actor.TileRightX] != 1;
            actor.CanGoDownRight = mCollisionData[actor.TileDownY, actor.TileRightX] != 1;
        }
    }
}
