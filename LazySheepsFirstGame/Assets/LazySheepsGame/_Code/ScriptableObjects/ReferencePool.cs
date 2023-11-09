using com.LazyGames.Dio;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "ScriptableObject/Systems/Reference Pool")]
    public class ReferencePool : ScriptableObject
    {
        public GameObjectEventChannelSO NewReferenceRaiserChannel;   
        public List<GameObject> CurrentReferences;

        public void AddReference(GameObject reference)
        {
            CurrentReferences.Add(reference);
            NewReferenceRaiserChannel.GameObjectEvent(reference);
        }

        public void RemoveReference(GameObject reference)
        {
            CurrentReferences.Remove(reference);
        }

        public void ClearAllReferences()
        {
            CurrentReferences.Clear();
        }

        public List<GameObject> SearchAllReferencesByGameObject(GameObject objectReference)
        {
            List<GameObject> references = new List<GameObject>();

            foreach (GameObject reference in CurrentReferences)
            {
                if (reference == objectReference)
                {
                    references.Add(reference);
                }
            }
            return references;
        }

        public List<GameObject> SearchAllReferencesByComponent(System.Type componentType)
        {
            List<GameObject> foundReferences = new List<GameObject>();

            foreach (GameObject reference in CurrentReferences)
            {
                Component[] components = reference.GetComponents(componentType);
                if (components != null && components.Length > 0)
                {
                    foundReferences.Add(reference);
                }

                Component[] childComponents = reference.GetComponentsInChildren(componentType);
                foreach (Component childComponent in childComponents)
                {
                    if (CurrentReferences.Contains(childComponent.gameObject)) continue;
                    foundReferences.Add(childComponent.gameObject);
                }

                Component[] parentComponents = reference.GetComponentsInParent(componentType);
                foreach (Component parentComponent in parentComponents)
                {
                    if (CurrentReferences.Contains(parentComponent.gameObject)) continue;
                    foundReferences.Add(parentComponent.gameObject);
                }
            }

            return foundReferences;
        }


        public GameObject SearchReferenceByGameObject(GameObject objectReference)
        {
            GameObject foundReference = null;

            foreach (GameObject reference in CurrentReferences)
            {
                if (objectReference == reference)
                {
                    foundReference = reference;
                    break;
                }
            }
            return foundReference;
        }

        public GameObject SearchReferenceByComponent(System.Type componentType)
        {
            GameObject foundReference = null;

            foreach (GameObject reference in CurrentReferences)
            {
                if(reference.GetComponents(componentType) != null)
                {
                    foundReference = reference;
                    break;
                } else if (reference.GetComponentsInChildren(componentType) != null) 
                {
                    foundReference = reference;
                    break;
                } else if (reference.GetComponentsInParent(componentType) != null)
                {
                    foundReference = reference;
                    break;
                }
            }

            return foundReference;
        }
    }
}
