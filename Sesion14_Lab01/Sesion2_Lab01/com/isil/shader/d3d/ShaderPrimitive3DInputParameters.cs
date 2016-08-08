using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace Sesion2_Lab01 {
    public struct ShaderPrimitive3DInputParameters {
        public static ShaderPrimitive3DInputParameters EMPTY = new ShaderPrimitive3DInputParameters();

        public float ambientIntensity;
	    public Vector3 lightDirection;
	    public Vector4 ambientColor;
        public Vector4 diffuseLighting;
        public Matrix transformation;
    }
}
