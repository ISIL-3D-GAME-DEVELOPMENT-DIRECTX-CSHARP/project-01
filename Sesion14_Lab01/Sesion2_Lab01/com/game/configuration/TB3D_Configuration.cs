using Sesion2_Lab01.com.game.configuration.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sesion2_Lab01.com.game.configuration {
    public class TB3D_Configuration {

        private static TB3D_Configuration M_Instance = null;

        public static TB3D_Configuration Instance { get { return TB3D_Configuration.M_Instance; } }

        public static void New() {
            if (TB3D_Configuration.M_Instance == null) {
                TB3D_Configuration.M_Instance = new TB3D_Configuration();
            }
        }

        private TB3D_WorldConfig[] mWorldConfigurations;

        public TB3D_Configuration() {
            XDocument xDoc = XDocument.Load("Content/configuration/TB3D_WorldConfig.xml");
            IEnumerable<XElement> elementsLevels = xDoc.Elements().Elements();

            // creamos el arreglo de configuraciones del mundo
            mWorldConfigurations = new TB3D_WorldConfig[elementsLevels.Count()];

            for (int i = 0, length = mWorldConfigurations.Length; i < length; i++) {
                TB3D_WorldConfig wConfig = TB3D_WorldConfig.Default;
                wConfig.Parse(elementsLevels.ElementAt<XElement>(i));

                mWorldConfigurations[i] = wConfig;
            }
        }

        public TB3D_WorldConfig GetWorld(string identifier) {
            TB3D_WorldConfig result = TB3D_WorldConfig.Default;

            for (int i = 0, length = mWorldConfigurations.Length; i < length; i++) {
                if (mWorldConfigurations[i].Identifier == identifier) {
                    result = mWorldConfigurations[i];
                    break;
                }
            }

            return result;
        }
    }
}
