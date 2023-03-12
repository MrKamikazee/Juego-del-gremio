/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 12/03/2023
 */

using UnityEngine;

namespace JuegoGremio.Scrolls
{
    [CreateAssetMenu(fileName = "New parchment data", menuName = "ScriptableObjects/Parchment Data", order = 2)]
    public class ParchmentData : ScriptableObject
    {
        [Header("Visual")] 
        public string tittle;
        public Sprite sprite;
        public GameObject room;

        public string GetTittle()
        {
            return tittle;
        }
    }
}
