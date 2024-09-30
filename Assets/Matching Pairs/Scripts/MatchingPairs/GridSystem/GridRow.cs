using UnityEngine;

namespace MatchingPairs.GridSystem
{
    public class GridRow : MonoBehaviour
    {
        public void ShuffleChildren()
        {
            var totalChildren = transform.childCount;
            for (var i = 0; i < totalChildren; i++)
            {
                transform.GetChild(i).SetSiblingIndex(Random.Range(0,totalChildren));
            }
        }
    }
}
