/*
 * Created by: MrKamikazeee
 * Created on: 13/03/2023
 * 
 * Last Modified: 13/03/2023
 */

using UnityEngine;

namespace JuegoGremio.Room
{
    [CreateAssetMenu(fileName = "New room data", menuName = "ScriptableObjects/Room Data", order = 1)]
    public class RoomsDataSO : ScriptableObject
    {
        public TypeRoom typeRoom = TypeRoom.None;
        public GameObject smallVersion, mediumVersion, largeVersion;
        
        public enum TypeRoom
        {
            None,
            Stairs,
            Reception
        }
    }
}
