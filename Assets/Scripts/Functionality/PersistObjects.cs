using UnityEngine;

namespace Functionality
{
    //This class will check the ProjectSettings class to see if it has already persisted these objects.
    //If it has: It will destroy the objects and itself (because they already exists).
    //If it has not: It will persist the object and destroy itself when the job is done.
    public class PersistObjects : MonoBehaviour
    {
        private static bool FirstLoad => !ProjectStatistics.PersistantObjectsInitialisedPreviously;
        [SerializeField]
        private Transform[] persistantObjects;
    
        private void Awake()
        {
            SetInstance();
            SetObjectsToPersistAcrossScenes();
        }
    
        private void SetInstance()
        {
            if (!ProjectStatistics.PersistantObjectsInitialisedPreviously)
            {
                return;
            }
            DestroyAllObjects();
        }

        private void SetObjectsToPersistAcrossScenes()
        {
            if (!FirstLoad) return;
            
            foreach (var objectToPersist in persistantObjects)
            {
                DontDestroyOnLoad(objectToPersist);
            }
            ProjectStatistics.PersistantObjectsInitialisedPreviously = true;
            Destroy(GetComponent<PersistObjects>());
        }
        
        private void DestroyAllObjects()
        {
            foreach (var objectToPersist in persistantObjects)
            {
                Destroy(objectToPersist.gameObject);
            }
        }
    }
}
