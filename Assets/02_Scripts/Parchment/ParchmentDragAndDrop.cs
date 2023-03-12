/*
 * Created by: MrKamikazeee
 * Created on: 12/03/2023
 * 
 * Last Modified: 12/03/2023
 */

using DG.Tweening;
using JuegoGremio.Build;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.EventSystems;

namespace JuegoGremio.Scrolls
{
    public class ParchmentDragAndDrop : Parchment, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [Header("Drag and Drop system")] 
        private bool _canBuild;
        private Vector3 _tempPos;
        private Transform _placeToBuild;  
        
        // Ejecute this when the player clicks
        public void OnPointerDown(PointerEventData eventData)
        {
            _tempPos = transform.position;
            ShowCanBuild();
        }

        // Ejecute this when the player move the mouse
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            RaycastHit hit = CastRay();
            if (hit.collider != null)
            {
                if (!hit.collider.CompareTag("Rooms Build"))
                {
                    _canBuild = false;
                    return;
                }
                _canBuild = true;
                _placeToBuild = hit.collider.gameObject.transform;
            }
        }

        // Ejecute this when the player release the click
        public void OnEndDrag(PointerEventData eventData)
        {
            TryBuild();
        }
        
        // Show the places where the player can build
        public void ShowCanBuild()
        {
            GameObject[] buildMechanic = new GameObject[GameObject.FindGameObjectsWithTag("Rooms").Length];
            buildMechanic = GameObject.FindGameObjectsWithTag("Rooms");
            for (int i = 0; i < buildMechanic.Length; i++)
                buildMechanic[i].GetComponent<BuildMechanic>().ShowCanBuild();
        }

        // Try to build the room
        public void TryBuild()
        {
            GameObject[] buildMechanic = new GameObject[GameObject.FindGameObjectsWithTag("Rooms").Length];
            buildMechanic = GameObject.FindGameObjectsWithTag("Rooms");
            if (!_canBuild)
                transform.DOMove(_tempPos, .2f);
            else
            {
                Instantiate( _data.room, _placeToBuild.position, quaternion.identity);
                for (int i = 0; i < buildMechanic.Length; i++)
                    buildMechanic[i].GetComponent<BuildMechanic>().CancelBuild();
                Destroy(this.gameObject);
            }
            for (int i = 0; i < buildMechanic.Length; i++)
                buildMechanic[i].GetComponent<BuildMechanic>().CancelBuild();
        }

        // Create the raycast on the mouse to do the magic
        private RaycastHit CastRay()
        {
            Vector3 screenMousePosFar = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.farClipPlane);
            Vector3 screenMousePosNear = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.nearClipPlane);
            Vector3 worldMousePorFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
            Vector3 worldMousePorNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
            RaycastHit hit;
            Physics.Raycast(worldMousePorNear, worldMousePorFar - worldMousePorNear, out hit);
            return hit;
        }
    }
}
