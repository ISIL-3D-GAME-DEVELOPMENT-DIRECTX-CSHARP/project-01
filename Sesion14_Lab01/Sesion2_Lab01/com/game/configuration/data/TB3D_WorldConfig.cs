using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sesion2_Lab01.com.game.configuration.data {
    public struct TB3D_WorldConfig {

        private static TB3D_WorldConfig M_Default = new TB3D_WorldConfig();
        public static TB3D_WorldConfig Default { get { return TB3D_WorldConfig.M_Default; } }

        private string mIdentifier;
        private float mTileSize;
        private int[,] mWorldCollisions;

        public string Identifier        { get { return mIdentifier; } }
        public float TileSize           { get { return mTileSize; } }
        public int[,] WorldCollisions   { get { return mWorldCollisions; } }

        internal void Parse(XElement data) {
            IEnumerable<XAttribute> nodeATT = data.Attributes();
            IEnumerable<XElement> rows = data.Elements();

            // parseamos los atributos
            for (int i = 0, length = nodeATT.Count(); i < length; i++) {
                XAttribute att = nodeATT.ElementAt<XAttribute>(i);

                switch (att.Name.LocalName) {
                case "id":
                    mIdentifier = att.Value;
                    break;
                case "tileSize":
                    mTileSize = float.Parse(att.Value);
                    break;
                }
            }

            // parseamos las filas
            for (int i = 0, length = rows.Count(); i < length; i++) {
                XElement rowElement = rows.ElementAt<XElement>(i);
                string rowRaw = rowElement.Attribute(XName.Get("data")).Value;
                string[] rowArray = rowRaw.Split(new char[1] { ',' });

                // inicializar el arreglo bidimensional
                if (mWorldCollisions == null) {
                    mWorldCollisions = new int[length, rowArray.Length];
                }

                for (int k = 0, rLength = rowArray.Length; k < rLength; k++) {
                    mWorldCollisions[i,k] = int.Parse(rowArray[k]);
                }
            }
        }
    }
}
