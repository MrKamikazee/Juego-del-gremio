/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 12/03/2023
 */

using JuegoGremio.Scrolls;
using UnityEngine;
using TMPro;

namespace JuegoGremio.Scrolls
{
    [RequireComponent(typeof(ParchmentDragAndDrop))]
    public class Parchment : MonoBehaviour
    {
        [Header("Data")] 
        public ParchmentData _data;
        private TextMeshProUGUI tittle;

        private void Start()
        {
            tittle = GetComponentInChildren<TextMeshProUGUI>();
            tittle.text = _data.GetTittle();
        }
        
    }
}
