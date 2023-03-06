using System;
using UnityEngine;

namespace TowerDefense
{
    public class GemDatabase : MonoBehaviour
    {
        public static GemDatabase Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}