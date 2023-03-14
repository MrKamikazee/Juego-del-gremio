/*
 * Created by: MrKamikazeee
 * Created on: 13/03/2023
 * 
 * Last Modified: 13/03/2023
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace JuegoGremio.Room
{
    public class Rooms : MonoBehaviour
    {
        [Header("Building")]
        public GameObject canBuildPrefab;
        private Ray _ray1, _ray2, _ray3;
        private List<GameObject> _canBuildObject;

        [Header("Type Room")] 
        public RoomsDataSO _roomData;
        public LargeRoom largeRoom = LargeRoom.None;
        
        public enum LargeRoom
        {
            None,
            Small,
            Medium,
            Large
        }

        void Start()
        {
            _canBuildObject = new List<GameObject>();
            _ray1 = new Ray(transform.position, Vector3.left);
            _ray2 = new Ray(transform.position, Vector3.right);
            if (_roomData.typeRoom == RoomsDataSO.TypeRoom.Stairs)
                _ray3 = new Ray(transform.position, Vector3.up);
        }

        // Show the places to build
        public void ShowCanBuild()
        {
            if (!Physics.Raycast(_ray1))
                switch (largeRoom)
                {
                    case LargeRoom.Small:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(1,0,0), Quaternion.identity));
                        break;
                    case LargeRoom.Medium:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(1.5f,0,0), Quaternion.identity));
                        break;
                    case LargeRoom.Large:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(2,0,0), Quaternion.identity));
                        break;
                    default:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(1,0,0), Quaternion.identity));
                        break;
                }
            if (!Physics.Raycast(_ray2))
                switch (largeRoom)
                {
                    case LargeRoom.Small:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(-1,0,0), Quaternion.identity));
                        break;
                    case LargeRoom.Medium:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(-1.5f,0,0), Quaternion.identity));
                        break;
                    case LargeRoom.Large:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(-2,0,0), Quaternion.identity));
                        break;
                    default:
                        _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(-1,0,0), Quaternion.identity));
                        break;
                }
            if (_roomData.typeRoom == RoomsDataSO.TypeRoom.Stairs && !Physics.Raycast(_ray3))
                _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity));
        }

        // Cancel the build
        public void CancelBuild()
        {
            for (int i = 0; i < _canBuildObject.Count; i++)
            {
                Destroy(_canBuildObject[i]);
            }
            _canBuildObject.Clear();
        }
    }
}
