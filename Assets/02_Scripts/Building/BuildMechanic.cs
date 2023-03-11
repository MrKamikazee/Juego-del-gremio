/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 11/03/2023
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JuegoGremio.Build
{
    public class BuildMechanic : MonoBehaviour
    {
        /*#region Singleton
        
        private static BuildMechanic _instance;
        public static BuildMechanic instance
        {
            get
            {
                if (!_instance)
                    Debug.LogWarningFormat("Accesing {0} before its Awake phase", typeof(BuildMechanic).Name);
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this || FindObjectsOfType<BuildMechanic>().Length > 1)
            {
                Debug.LogWarningFormat("Please make sure there is only one {0} in the scene", typeof(BuildMechanic).Name);
                Destroy(this);
                return;
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion*/
        
        [Header("Building")]
        public GameObject canBuildPrefab;
        private Ray _ray1, _ray2, _ray3;
        public RoomType roomType;
        private List<GameObject> _canBuildObject;

        public enum RoomType
        {
            None,
            Stairs,
            Reception_Short,
            Reception_Medium,
            Reception_Large
        }

        void Start()
        {
            _canBuildObject = new List<GameObject>();
            _ray1 = new Ray(transform.position, Vector3.left);
            _ray2 = new Ray(transform.position, Vector3.right);
            if (roomType == RoomType.Stairs)
                _ray3 = new Ray(transform.position, Vector3.up);
        }
        private void Update()
        {
            Debug.DrawRay(_ray1.origin, _ray1.direction * 1f, Color.green);
            Debug.DrawRay(_ray2.origin, _ray2.direction * 1f, Color.red);
            if (roomType == RoomType.Stairs)
                Debug.DrawRay(_ray3.origin,_ray3.direction * 1f, Color.cyan);
        }

        public void CanBuild()
        {
            if (!Physics.Raycast(_ray1))
                _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(1,0,0), Quaternion.identity));
            if (!Physics.Raycast(_ray2))
                _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position + new Vector3(1,0,0), Quaternion.identity));
            if (roomType == RoomType.Stairs && !Physics.Raycast(_ray3))
                _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity));
        }

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
