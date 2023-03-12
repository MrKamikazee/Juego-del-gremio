/*
 * Created by: MrKamikazeee
 * Created on: 12/03/2023
 * 
 * Last Modified: 12/03/2023
 */

using UnityEngine;

namespace JuegoGremio
{
    [CreateAssetMenu(fileName = "New room type data", menuName = "ScriptableObjects/Room Type Data", order = 2)]
    public class RoomTypeData : ScriptableObject
    {
        public RoomType roomType;
        public GameObject smallVersion, mediumVersion, largeVersion;
        
        public enum RoomType
        {
            None,
            Stairs,
            Reception
        }
    }
}
