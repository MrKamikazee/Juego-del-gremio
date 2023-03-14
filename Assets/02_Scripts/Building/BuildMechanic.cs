/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 13/03/2023
 */

using Unity.Mathematics;
using UnityEngine;

namespace JuegoGremio.Room
{
    public class BuildMechanic : MonoBehaviour
    {
        [HideInInspector]
        public bool upgradeRoom = false;
        
        // Build a new room
        public void BuildNewRoom(GameObject room)
        {
            GameObject roomCreated = Instantiate(room, transform.position, quaternion.identity);
            roomCreated.GetComponent<BuildMechanic>().StartVerifyRoom(roomCreated);
            FinishBuild(roomCreated);
        }

        // Start the upgrade verification
        private void StartVerifyRoom(GameObject roomCreated)
        {
            Ray ray1, ray2;
            RaycastHit hit1, hit2;
            ray1 = new Ray(transform.position, Vector3.left);
            ray2 = new Ray(transform.position, Vector3.right);
            Physics.Raycast(ray1, out hit1);
            Physics.Raycast(ray2, out hit2);
            if (hit1.collider != null)
                VerifyRoom(hit1, roomCreated);
            if (hit2.collider != null)
                VerifyRoom(hit2, roomCreated);
        }

        // Verify if the room can upgrade or not
        private void VerifyRoom(RaycastHit hit, GameObject roomCreated)
        {
            if (hit.collider.GetComponent<Rooms>()._roomData.typeRoom != roomCreated.GetComponent<Rooms>()._roomData.typeRoom ||
                hit.collider.GetComponent<Rooms>().largeRoom == Rooms.LargeRoom.Large)
                return;

            if (hit.collider.GetComponent<Rooms>().largeRoom == Rooms.LargeRoom.Small)
            {
                Instantiate(hit.collider.GetComponent<Rooms>()._roomData.mediumVersion, transform.position + new Vector3(.5f,0,0),
                    quaternion.identity);
                upgradeRoom = true;
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.GetComponent<Rooms>().largeRoom == Rooms.LargeRoom.Medium)
            {
                Instantiate(hit.collider.GetComponent<Rooms>()._roomData.largeVersion, transform.position + new Vector3(1f,0,0),
                    quaternion.identity);
                upgradeRoom = true;
                Destroy(hit.collider.gameObject);
            }
        }

        // Finish the build event
        private void FinishBuild(GameObject roomCreated)
        {
            if (roomCreated.GetComponent<BuildMechanic>().upgradeRoom)
            {
                Debug.Log(roomCreated.gameObject.name);
                Destroy(roomCreated.gameObject);
            }
            GameObject[] roomsBuild = new GameObject[GameObject.FindGameObjectsWithTag("Rooms Build").Length];
            roomsBuild = GameObject.FindGameObjectsWithTag("Rooms Build");
            for (int i = 0; i < roomsBuild.Length; i++)
                Destroy(roomsBuild[i]);
        }
    }
}
