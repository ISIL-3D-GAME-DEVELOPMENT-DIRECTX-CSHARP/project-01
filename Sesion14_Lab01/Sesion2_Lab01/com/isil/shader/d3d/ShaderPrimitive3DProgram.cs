using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX;

using Device = SharpDX.Direct3D11.Device;
using D3DBuffer = SharpDX.Direct3D11.Buffer;
using D3DMapFlags = SharpDX.Direct3D11.MapFlags;
using System.Runtime.InteropServices;
using SharpDX.Direct3D;

namespace Sesion2_Lab01 {
    public class ShaderPrimitive3DProgram {

        public float mRotationX;
        public float mRotationY;
        public float mRotationZ;

        private Color mAmbientColor;
        private Color mDiffuseLightColor;
        private Vector3 mLightDirection;

        public PrimitiveTopology PrimitiveTopology;

        private Device mDevice;
        private DeviceContext mDeviceContext;

        private VertexShader mVertexShader;
        private PixelShader mPixelShader;
        private InputLayout mInputLayout;

        private D3DBuffer mVertexBuffer;
        private D3DBuffer mIndexBuffer;
        private VertexBufferBinding mVertexBufferBinding;

        private int mIndicesCount;

        private D3DBuffer mMiscInputBuffer;
        private BufferDescription mMiscInputBufferDescription;

        private Matrix mWorld;
        private Matrix mRotationXMatrix;
        private Matrix mRotationYMatrix;
        private Matrix mRotationZMatrix;

        public VertexShader VertexShader    { get { return mVertexShader; } }
        public PixelShader PixelShader      { get { return mPixelShader; } }
        public InputLayout InputLayout      { get { return mInputLayout; } }

        public float X {
            get { return mWorld.M41; }
            set { mWorld.M41 = value; }
        }

        public float Y {
            get { return mWorld.M42; }
            set { mWorld.M42 = value; }
        }

        public float Z {
            get { return mWorld.M43; }
            set { mWorld.M43 = value; }
        }

        public float ScaleX {
            get { return mWorld.M11; }
            set { mWorld.M11 = value; }
        }

        public float ScaleY {
            get { return mWorld.M22; }
            set { mWorld.M22 = value; }
        }

        public float ScaleZ {
            get { return mWorld.M33; }
            set { mWorld.M33 = value; }
        }

        public float RotationX {
            get { return mRotationX; }
            set {
                if (mRotationX != value) {
                    mRotationX = value;

                    mRotationXMatrix = Matrix.RotationX(mRotationX);
                }
            }
        }

        public float RotationY {
            get { return mRotationY; }
            set {
                if (mRotationY != value) {
                    mRotationY = value;

                    mRotationYMatrix = Matrix.RotationY(mRotationY);
                }
            }
        }

        public float RotationZ {
            get { return mRotationZ; }
            set {
                if (mRotationZ != value) {
                    mRotationZ = value;

                    mRotationZMatrix = Matrix.RotationZ(mRotationZ);
                }
            }
        }

        public Color AmbientColor {
            get { return mAmbientColor; }
            set { mAmbientColor = value; }
        }

        public Color DiffuseLightColor {
            get { return mDiffuseLightColor; }
            set { mDiffuseLightColor = value; }
        }

        public Vector3 LightDirection {
            get { return mLightDirection; }
            set { mLightDirection = value; }
        }

        public ShaderPrimitive3DProgram(Device device) {
            mDevice = device;
            mDeviceContext = device.ImmediateContext;

            mAmbientColor = Color.White;
            mDiffuseLightColor = Color.White;
            mLightDirection = Vector3.UnitX; 

            this.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;

            mWorld = mRotationXMatrix = mRotationYMatrix = mRotationZMatrix = Matrix.Identity;
        }

        public void Load(string path) {
            CompilationResult vertexShaderByteCode = ShaderBytecode.CompileFromFile(path,
                "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            mVertexShader = new VertexShader(mDevice, vertexShaderByteCode);

            // compilamos nuestro fragment shader
            CompilationResult pixelShaderByteCode = ShaderBytecode.CompileFromFile(path,
                "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            mPixelShader = new PixelShader(mDevice, pixelShaderByteCode);

            ShaderSignature shaderSignature = ShaderSignature.GetInputSignature(vertexShaderByteCode);

            // creamos el contenedor de nuestras variables de entrada
            InputElement[] inputElements = new InputElement[2];
            // segun nuestro shader es: Posicion de los vertices
            inputElements[0] = new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0);
            // segun nuestro shader es: El color de cada vertice
            inputElements[1] = new InputElement("NORMAL", 0, Format.R32G32B32A32_Float, 12, 0);

            // construimos el Input Layout, le pasamos el Shader Signature que contiene la referencia
            // de nuestro Shader, el Input Element que contiene nuestro parametros de entrada, y 
            // mediante esto crea un Objeto para poder enviarlo a la tarjeta de video para poder usarlo
            mInputLayout = new InputLayout(mDevice, shaderSignature, inputElements);

            // ahora creamos una Descripcion para almacenar la informacion de nuestro nuevo bloque
            // de parametros en nuestro Shader
            mMiscInputBufferDescription.Usage = ResourceUsage.Dynamic;
            mMiscInputBufferDescription.SizeInBytes = Utilities.SizeOf<ShaderPrimitive3DInputParameters>();
            mMiscInputBufferDescription.BindFlags = BindFlags.ConstantBuffer;
            mMiscInputBufferDescription.CpuAccessFlags = CpuAccessFlags.Write;
            mMiscInputBufferDescription.OptionFlags = 0;
            mMiscInputBufferDescription.StructureByteStride = 0;

            // creamos un Buffer para almacenar nuestro parametros sueltos
            mMiscInputBuffer = new D3DBuffer(mDevice, mMiscInputBufferDescription);
        }

        public void SetConstantInputParameter<T>(D3DBuffer inputBuffer,
            int subResource, int slot, ref T parametersStruct) {

            // Retrieve the Data Stream from the Shader, so now can we access the Memory Pointer
            // to modify the Input Parameters from the Shader
            DataStream outData = null;
            mDevice.ImmediateContext.MapSubresource(inputBuffer,
                MapMode.WriteDiscard, D3DMapFlags.None, out outData);

            // Now that we set the new values, let's say to the Memory Pointer that we
            // changed the values and update the Memory Pointer
            Marshal.StructureToPtr(parametersStruct, outData.DataPointer, true);

            // Now we re-enable the Buffer
            mDevice.ImmediateContext.UnmapSubresource(inputBuffer, subResource);
            // Now send the Buffer to the Vertex Shader
            mDevice.ImmediateContext.VertexShader.SetConstantBuffer(slot, inputBuffer);
        }

        public void Update(float[] vertices, ushort[] indices) {
            // ahora creamos nuestro Buffer para poder almacenar los Vertice de una manera
            // que la tarjeta de video pueda leer y transferir los vertices a los Shaders
            if (mVertexBuffer != null) {
                mVertexBuffer.Dispose();
                mVertexBuffer = null;
            }

            mVertexBuffer = D3DBuffer.Create(mDevice, BindFlags.VertexBuffer, vertices);

            // ahora mandamos nuestro Buffer a la tarjeta de video para que lo pueda transferir 
            // al Shader
            mVertexBufferBinding.Buffer = mVertexBuffer;
            mVertexBufferBinding.Stride = Vector3.SizeInBytes * 2;
            mVertexBufferBinding.Offset = 0;

            // mandamos vertices al GPU
            mDeviceContext.InputAssembler.SetVertexBuffers(0, mVertexBufferBinding);

            mIndicesCount = indices.Length;

            // ahora vamos a definir nuestros vertices y mandar al GPU
            if (mIndexBuffer != null) {
                mIndexBuffer.Dispose();
                mIndexBuffer = null;
            }

            mIndexBuffer = D3DBuffer.Create(mDevice, BindFlags.IndexBuffer, indices);

            mDeviceContext.InputAssembler.SetIndexBuffer(mIndexBuffer, Format.R16_UInt, 0);
        }

        public void Draw(Matrix transformation) {
            transformation = mRotationXMatrix * mRotationYMatrix * mRotationZMatrix * mWorld * transformation;
            transformation.Transpose();

            ShaderPrimitive3DInputParameters inputParameters = ShaderPrimitive3DInputParameters.EMPTY;
            inputParameters.transformation = transformation;
            inputParameters.ambientColor = mAmbientColor.ToVector4();
            inputParameters.diffuseLighting = mDiffuseLightColor.ToVector4();
            inputParameters.ambientIntensity = 0.1f;
            inputParameters.lightDirection = mLightDirection;

            // Set the Shader Constant Parameters
            SetConstantInputParameter<ShaderPrimitive3DInputParameters>(mMiscInputBuffer,
                0, 0, ref inputParameters);


            mDeviceContext.InputAssembler.InputLayout = mInputLayout;
            // definimos ahora de que manera se va a tratar la data que entra y como se dibuja
            mDeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // aqui vamos a transferir el Vertex y Fragment Shader que ya habiamos creado
            mDeviceContext.VertexShader.Set(mVertexShader);
            mDeviceContext.PixelShader.Set(mPixelShader);

            // dibujamos nuestros vertices!
            mDeviceContext.DrawIndexed(mIndicesCount, 0, 0);
        }
    }
}
