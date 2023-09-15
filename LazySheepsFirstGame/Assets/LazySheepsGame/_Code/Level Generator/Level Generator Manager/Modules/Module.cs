using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    public class Module : MonoBehaviour
    {
        #region SerializedFields
        [SerializeField] private ModuleData moduleData;
        [SerializeField] private Connector[] connectors;
        [SerializeField] private string connectionsIDs;
        
        #endregion

        #region public variables
        public ModuleData MyData => moduleData;
        public Connector[] MyConnectors => connectors;
        
        
        
        #endregion

        
        #region private variables

        

        #endregion

        #region Unity Methods
        #endregion
        

        #region public Methods
        
        public void RepositionModule(Connector moduleToConnect)
        {
            Connector myConnector = GetConnector();
            if (myConnector.ModuleDirection != moduleToConnect.ModuleDirection)
            {
                Vector3 positionOffset = moduleToConnect.transform.position - myConnector.transform.position;
                transform.position += positionOffset;
                myConnector.IsConnected = true;
            }
            
        }
        
        #endregion
        
        
        #region private Methods

        private Connector GetConnector()
        {
            foreach (var connector in connectors)
            {
                if (!connector.IsConnected)
                {
                    return connector;
                }
            }

            return null;
        }
        #endregion
    }
}