/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 12/03/2023
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JuegoGremio.Build
{
    public class BuildMechanic : MonoBehaviour
    {
        [Header("Building")]
        public GameObject canBuildPrefab;
        public Ray ray1, ray2, ray3;
        private List<GameObject> _canBuildObject;
        private bool _isStairs;

        void Start()
        {
            _canBuildObject = new List<GameObject>();
            ray1 = new Ray(transform.position, Vector3.left);
            ray2 = new Ray(transform.position, Vector3.right);
            if (_isStairs)
                ray3 = new Ray(transform.position, Vector3.up);
        }

        // Show the places to build
        public void ShowCanBuild()
        {
            if (!Physics.Raycast(ray1))
                _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position - new Vector3(1,0,0), Quaternion.identity));
            if (!Physics.Raycast(ray2))
                _canBuildObject.Add(Instantiate(canBuildPrefab, transform.position + new Vector3(1,0,0), Quaternion.identity));
            if (_isStairs && !Physics.Raycast(ray3))
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
