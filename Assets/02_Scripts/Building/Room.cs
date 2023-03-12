/*
 * Created by: MrKamikazeee
 * Created on: 12/03/2023
 * 
 * Last Modified: 12/03/2023
 */

using System;
using JuegoGremio.Build;
using Unity.Mathematics;
using UnityEngine;

namespace JuegoGremio
{
    public class Room : BuildMechanic
    {
        [Header("Fusion Rooms")]
        public RoomLarge roomLarge;
        private RoomTypeData _roomTypeData;
        private RoomLarge _roomLargeTemporal;
        private RaycastHit hit1, hit2;
        private bool _buildLeft;
        
        public enum RoomLarge
        {
            None,
            Small,
            Medium,
            Large
        }

        private void Update()
        {
            VerifyRoom();  
        }

        // Verify if the room next to this one it's the same type or not
        private void VerifyRoom()
        {
            Physics.Raycast(ray1, out hit1);
            Physics.Raycast(ray2, out hit2);
            if (hit1.collider == null && hit2.collider == null)
                return;
            if (hit1.collider.GetComponent<Room>()._roomTypeData.roomType != _roomTypeData.roomType &&
                hit2.collider.GetComponent<Room>()._roomTypeData.roomType != _roomTypeData.roomType)
                return;
            CanFusion();
        }
        
        // Verify if the room can be bigger or not
        private void CanFusion()
        {
            if (hit1.collider.GetComponent<Room>()._roomTypeData.roomType == _roomTypeData.roomType)
            {
                _roomLargeTemporal = hit1.collider.GetComponent<Room>().roomLarge;
                _buildLeft = true;
            }
            else if (hit2.collider.GetComponent<Room>()._roomTypeData.roomType == _roomTypeData.roomType)
            {
                _roomLargeTemporal = hit2.collider.GetComponent<Room>().roomLarge;
                _buildLeft = false;
            }
            if (_roomLargeTemporal == RoomLarge.Large)
                return;
            
            if (_roomLargeTemporal == RoomLarge.Small)
                BuildMediumRoom();
            else
                BuildLargeRoom();
        }

        // Make the room bigger
        private void BuildMediumRoom()
        {
            Vector3 distanceCorrection;
            if (_buildLeft)
                distanceCorrection = new Vector3(.5f, 0, 0);
            else
                distanceCorrection = new Vector3(-.5f, 0, 0);

            Instantiate(_roomTypeData.mediumVersion, transform.position + distanceCorrection, quaternion.identity);
            Destroy(this);
        }

        // Make the room bigger
        private void BuildLargeRoom()
        {
            Vector3 distanceCorrection;
            if (_buildLeft)
                distanceCorrection = new Vector3(1f, 0, 0);
            else
                distanceCorrection = new Vector3(-1f, 0, 0);

            Instantiate(_roomTypeData.mediumVersion, transform.position + distanceCorrection, quaternion.identity);
            Destroy(this); 
        }
    }
}
