/*
 * Created by: MrKamikazeee
 * Created on: 11/03/2023
 * 
 * Last Modified: 11/03/2023
 */

using DG.Tweening;
using UnityEngine;

namespace JuegoGremio.Chest
{
    public class Chest : MonoBehaviour
    {
        [Header("Chest")]
        [SerializeField] private GameObject _coverChest, _baseChest;
        [SerializeField] private Transform _chestClosePos, _chestOpenPos;
        private bool _isChestOpen;

        private void Start()
        {
            DOTween.Init();
        }

        public void OnClick()
        {
            if (!_isChestOpen)
            {
                _coverChest.transform.DOMoveY(_chestOpenPos.position.y, 1f);
                _isChestOpen = true;
            }
            else
            {
                _isChestOpen = false;
                _coverChest.transform.DOMoveY(_chestClosePos.position.y, 1f);
            }
        }
    }
}
