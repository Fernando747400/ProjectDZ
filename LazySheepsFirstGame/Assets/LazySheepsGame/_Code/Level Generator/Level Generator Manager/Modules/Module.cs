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
        
        public void RepositionModule(Module moduleToConnect)
        {
            Connector otherConnector = moduleToConnect.GetConnector();
            Connector myConnector = GetConnector(otherConnector.ModuleDirection);
            
            Debug.Log("Re positioning Module Type = ".SetColor("#F37817") + moduleData.ModuleType);
            if (myConnector.ModuleDirection != otherConnector.ModuleDirection)
            {
                Debug.Log("Direction previous connector = ".SetColor("#F37817") + otherConnector.ModuleDirection + " = "+ myConnector.ModuleDirection);

                Vector3 positionOffset = otherConnector.transform.position - myConnector.transform.position;
                transform.position += positionOffset;
                myConnector.IsConnected = true;
                otherConnector.IsConnected = true;
            }
            
        }
        #endregion
        
        
        #region private Methods

        public Connector GetConnector(ModuleDirection oppositeDirection = ModuleDirection.None)
        {
            if(oppositeDirection == ModuleDirection.None)
                return connectors[Random.Range(0, connectors.Length)];
            
            foreach (var connector in connectors)
            {
                if (connector.ModuleDirection != oppositeDirection)
                {
                    return connector;
                }
            }
            
            return null;
        }
        
      
        #endregion
    }
}