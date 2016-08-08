using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sesion2_Lab01;

using SharpDX;
using Sesion2_Lab01.com.isil.shader.d3d;
using Sesion2_Lab01.com.isil.shader.skinnedModel;
using SharpDX.Direct3D11;
using Sesion2_Lab01.com.isil.graphics;
using Sesion2_Lab01.com.isil.render.camera;
using Sesion2_Lab01.com.isil.utils;

namespace CrossXDK.com.digitalkancer.modules.moderlLoaders.assimp {
    public class NModel {

        private List<NModelMesh> mModelMeshes;

        private Shader3DProgram mShader;
        private DeviceContext mDeviceContext;
        private BlendState mBlendState;

        private float mX;
        private float mY;
        private float mZ;

        private float mScaleX;
        private float mScaleY;
        private float mScaleZ;

        private float mRotationX;
        private float mRotationY;
        private float mRotationZ;

        private Matrix mWorld;
        private NCrossModelLoader mModelLoader;

        public float X {
            get { return mX; }
            set { mX = value; }
        }

        public float Y {
            get { return mY; }
            set { mY = value; }
        }

        public float Z {
            get { return mZ; }
            set { mZ = value; }
        }

        public float ScaleX {
            get { return mScaleX; }
            set { mScaleX = value; }
        }

        public float ScaleY {
            get { return mScaleY; }
            set { mScaleY = value; }
        }

        public float ScaleZ {
            get { return mScaleZ; }
            set { mScaleZ = value; }
        }

        public float RotationX {
            get { return mRotationX; }
            set { mRotationX = value; }
        }

        public float RotationY {
            get { return mRotationY; }
            set { mRotationY = value; }
        }

        public float RotationZ {
            get { return mRotationZ; }
            set { mRotationZ = value; }
        }

        public NModel() {
            mModelMeshes = new List<NModelMesh>();

            mDeviceContext = NativeApplication.instance.Device.ImmediateContext;

            NBlend opaqueBlend = NBlend.Opaque();
            mBlendState = new BlendState(mDeviceContext.Device, opaqueBlend.BlendStateDescription);

            mShader = new Shader3DProgram(NativeApplication.instance.Device);
            mShader.Load("Content/Fx_PrimitiveTexture3D.fx");

            mScaleX = 1f;
            mScaleY = 1f;
            mScaleZ = 1f;
        }

        internal void AddMesh(ref NModelMesh modelMesh) {
            mModelMeshes.Add(modelMesh);
        }

        public void UpdateDraw(RenderCamera renderCamera, int dt) {
            // ahora seteamos el tipo de blend
            mDeviceContext.OutputMerger.SetBlendState(mBlendState);

            mWorld = Matrix.Identity;
            mWorld.M41 = mX;
            mWorld.M42 = mY;
            mWorld.M43 = mZ;

            mWorld = Matrix.Scaling(mScaleX, mScaleY, mScaleZ) *
                Matrix.RotationYawPitchRoll(mRotationX, mRotationY, mRotationZ) *
                mWorld;

            Matrix wit = NCommon.InverseTranspose(mWorld);

            Matrix transformation = renderCamera.transformed;
            transformation = mWorld * transformation;
            transformation.Transpose();

            for (int i = 0; i < mModelMeshes.Count; i++) {
                NModelMesh mesh = mModelMeshes[i];

                mShader.Update(mesh.IndexBuffer, mesh.VertexBufferBinding);
                mShader.Draw(mesh.IndicesCount, transformation, mesh.Texture2D, mesh.PrimitiveTopology);
            }
        }
    }
}
