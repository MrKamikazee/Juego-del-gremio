/*
 * Created by: MrKamikazeee
 * Created on: 12/03/2023
 * 
 * Last Modified: 13/03/2023
 */

using DG.Tweening;
using JuegoGremio.Room;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JuegoGremio.Scrolls
{
    public class ParchmentDragAndDrop : Parchment, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [Header("Drag and Drop system")] 
        private bool _canBuild;
        private Vector3 _tempPos;
        private RaycastHit hit;
        
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
            hit = CastRay();
        }

        // Ejecute this when the player release the click
        public void OnEndDrag(PointerEventData eventData)
        {
            TryBuild();
        }
        
        // Show the places where the player can build
        public void ShowCanBuild()
        {
            GameObject[] showBuild = new GameObject[GameObject.FindGameObjectsWithTag("Rooms").Length];
            showBuild = GameObject.FindGameObjectsWithTag("Rooms");
            for (int i = 0; i < showBuild.Length; i++)
                showBuild[i].GetComponent<Rooms>().ShowCanBuild();
        }

        // Try to build the room
        public void TryBuild()
        {
            if (hit.collider == null)
            {
                transform.DOMove(_tempPos, .2f);
                return;
            }
            
            if (hit.collider.CompareTag("Rooms Build"))
            {
                hit.collider.GetComponent<BuildMechanic>().BuildNewRoom(_data.room);
                Destroy(this.gameObject);
            }
            else
            {
                transform.DOMove(_tempPos, .2f);
            }
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
