/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 11/03/2023
 */

using System.Collections.Generic;
using JuegoGremio.Build;
using TMPro;
using UnityEngine;

namespace JuegoGremio.Scrolls
{
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

        public void CanBuild()
        {
            GameObject[] buildMechanic = new GameObject[GameObject.FindGameObjectsWithTag("Rooms").Length];
            buildMechanic = GameObject.FindGameObjectsWithTag("Rooms");
            for (int i = 0; i < buildMechanic.Length; i++)
                buildMechanic[i].GetComponent<BuildMechanic>().CanBuild();
            
            // BuildMechanic.instance.CanBuild();
        }

        public void CancelBuild()
        {
            GameObject[] buildMechanic = new GameObject[GameObject.FindGameObjectsWithTag("Rooms").Length];
            buildMechanic = GameObject.FindGameObjectsWithTag("Rooms");
            for (int i = 0; i < buildMechanic.Length; i++)
                buildMechanic[i].GetComponent<BuildMechanic>().CancelBuild();
            
            // BuildMechanic.instance.CancelBuild();
        }
    }
}
