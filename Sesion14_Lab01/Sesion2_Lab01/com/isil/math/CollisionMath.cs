using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.isil.math {
    public class CollisionMath {

        public static bool CheckCubeCollision(NPrimitiveCube3D a, NPrimitiveCube3D b) {
            bool hasCollision = false;

            float sizeXA = (a.Size / 2) * a.ScaleX;
            float sizeXB = (b.Size / 2) * b.ScaleX;

            float sizeYA = (a.Size / 2) * a.ScaleY;
            float sizeYB = (b.Size / 2) * b.ScaleY;

            float sizeZA = (a.Size / 2) * a.ScaleZ;
            float sizeZB = (b.Size / 2) * b.ScaleZ;

            //check the X axis
            if (Math.Abs(a.X - b.X) < sizeXA + sizeXB) {
                //check the Y axis
                if (Math.Abs(a.Y - b.Y) < sizeYA + sizeYB) {
                    //check the Z axis
                    if (Math.Abs(a.Z - b.Z) < sizeZA + sizeZB) {
                        hasCollision = true;
                    }
                }
            }

           return hasCollision;
        } 
    }
}
