using Core.Model;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.TB3DGame {
    public class TB3D_Actor {

        protected float mX;
        protected float mY;
        protected float mZ;

        protected float mOffsetX;
        protected float mOffsetY;
        protected float mOffsetZ;

        protected float mScaleX;
        protected float mScaleY;
        protected float mScaleZ;

        protected float mRotationX;
        protected float mRotationY;
        protected float mRotationZ;

        protected bool mDebugMode;

        public bool CanGoUpLeft;
        public bool CanGoDownLeft;
        public bool CanGoUpRight;
        public bool CanGoDownRight;
                      
        public int TileDownY;
        public int TileUpY;
        public int TileLeftX;
        public int TileRightX;

        protected float mTileWidth;
        protected float mTileHeight;

        protected TB3D_Engine mEngine;
        protected SkinnedModelInstance mSkinnedModel;
        protected NPrimitiveCube3D mCollisionCube;

        public virtual float X {
            get { return mX; }
            set { mX = value; }
        }

        public virtual float Y {
            get { return mY; }
            set { mY = value; }
        }

        public virtual float Z {
            get { return mZ; }
            set { mZ = value; }
        }

        public virtual float ScaleX {
            get { return mScaleX; }
            set { 
                mScaleX = value;

                if (mSkinnedModel != null) {
                    mSkinnedModel.ScaleX = mScaleX;
                }
            }
        }

        public virtual float ScaleY {
            get { return mScaleY; }
            set { 
                mScaleY = value;

                if (mSkinnedModel != null) {
                    mSkinnedModel.ScaleY = mScaleY;
                }
            }
        }

        public virtual float ScaleZ {
            get { return mScaleZ; }
            set { 
                mScaleZ = value;

                if (mSkinnedModel != null) {
                    mSkinnedModel.ScaleZ = mScaleZ;
                }
            }
        }

        public virtual float RotationX {
            get { return mRotationX; }
            set { 
                mRotationX = value;

                if (mSkinnedModel != null) {
                    mSkinnedModel.RotationX = mRotationX;
                }
            }
        }

        public virtual float RotationY {
            get { return mRotationY; }
            set { 
                mRotationY = value;

                if (mSkinnedModel != null) {
                    mSkinnedModel.RotationY = mRotationY;
                }
            }
        }

        public virtual float RotationZ {
            get { return mRotationZ; }
            set { 
                mRotationZ = value;

                if (mSkinnedModel != null) {
                    mSkinnedModel.RotationZ = mRotationZ;
                }
            }
        }

        public virtual NPrimitiveCube3D CollisionCube {
            get { return mCollisionCube; }
        }

        public virtual TB3D_Engine Engine {
            get { return mEngine; }
        }

        public TB3D_Actor(TB3D_Engine engine, string model_path, string texture_path, bool flipY) {
            mEngine = engine;

            this.CanGoUpLeft = false;
            this.CanGoDownLeft = false;
            this.CanGoUpRight = false;
            this.CanGoDownRight = false;

            mDebugMode = false;
            mScaleX = 1;
            mScaleY = 1;
            mScaleZ = 1;
            mTileWidth = mEngine.WConfig.TileSize;
            mTileHeight = mEngine.WConfig.TileSize;

            if (model_path != string.Empty) {
                mSkinnedModel = new SkinnedModelInstance(model_path, texture_path, flipY);
            }
        }

        protected void CreateCollision(float size) {
            mCollisionCube = new NPrimitiveCube3D(0, 0, 0, size);
            mCollisionCube.DiffuseLightColor = Color.Yellow;
            mCollisionCube.LightDirection = -Vector3.UnitZ;
        }

        public virtual void GotoAnimation(string clipName, bool loopeable) {
            if (mSkinnedModel != null) {
                mSkinnedModel.GotoAnimation(clipName, loopeable);
            }
        }

        public void ComputeNewPosition(float speed, float directionX, float directionY, out float outX, out float outY) {
            float size = mCollisionCube.Size / 2;
            float newX = mX;
            float newY = mZ;
            float tileSize = mEngine.TileSize;
            int tileX = (int)Math.Floor(newX / tileSize);
            int tileY = (int)Math.Floor(newY / tileSize);

            // vertical
            mEngine.World.WorldPhysics.GetCorners(size, newX, newY + (speed * directionY), this);

            if (directionY == -1) {
                if (this.CanGoUpLeft && this.CanGoUpRight) {
                    newY += speed * directionY;
                }
                else {
                    newY = tileY * tileSize + size;
                }
            }

            if (directionY == 1) {
                if (this.CanGoDownLeft && this.CanGoDownRight) {
                    newY += speed * directionY;
                }
                else {
                    newY = (tileY + 1) * tileSize - size;
                }
            }

            // horizontal
            mEngine.World.WorldPhysics.GetCorners(size, newX + (speed * directionX), newY, this);

            if (directionX == -1) {
                if (this.CanGoDownLeft && this.CanGoUpLeft) {
                    newX += speed * directionX;
                }
                else {
                    newX = tileX * tileSize + size;
                }
            }

            if (directionX == 1) {
                if (this.CanGoUpRight && this.CanGoDownRight) {
                    newX += speed * directionX;
                }
                else {
                    newX = (tileX + 1) * tileSize - size;
                }
            }

            outX = newX;
            outY = newY;
        }

        public virtual void UpdateDraw(int dt) {
            if (mCollisionCube != null) {
                mCollisionCube.X = mX + mOffsetX;
                mCollisionCube.Y = mY + mOffsetY;
                mCollisionCube.Z = mZ + mOffsetZ;

                if (mDebugMode) {
                    mCollisionCube.UpdateAndDraw(mEngine.Camera.RenderCamera, dt);
                }
            }

            if (mSkinnedModel != null) {
                mSkinnedModel.X = mX + mOffsetX;
                mSkinnedModel.Y = mY + mOffsetY;
                mSkinnedModel.Z = mZ + mOffsetZ;
                mSkinnedModel.UpdateDraw(mEngine.Camera.RenderCamera, dt);
            }

            int px = (int)Math.Floor(mX / mEngine.TileSize);
            int pz = (int)Math.Floor(mZ / mEngine.TileSize);

            

            /*System.Diagnostics.Debug.WriteLine("======================================");
            System.Diagnostics.Debug.WriteLine("CanGoUpLeft: " + CanGoUpLeft);
            System.Diagnostics.Debug.WriteLine("CanGoDownLeft: " + CanGoDownLeft);
            System.Diagnostics.Debug.WriteLine("CanGoUpRight: " + CanGoUpRight);
            System.Diagnostics.Debug.WriteLine("CanGoDownRight: " + CanGoDownRight);*/

            
        }

        public virtual void Free() {
            mSkinnedModel = null;
        }
    }
}
