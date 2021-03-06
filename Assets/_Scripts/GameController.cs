﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private static readonly Dictionary<EnumLevels, int> levels = new Dictionary<EnumLevels, int>
        {
            {EnumLevels.Easy, 4},
            {EnumLevels.Medium, 6},
            {EnumLevels.Hard, 8},
            {EnumLevels.Extreme, 10}
        };

        public GameObject prefabGameObject;
        public Vector3 spawnLimits;

        public Material[] materials;
        private PairMaterialValue[] pairMaterialValues;
        private List<GameObject> gameObjects;
        private Vector3 nextSpawnPosition;
        private int points;

        // Use this for initialization
        [UsedImplicitly]
        private void Awake()
        {
            int size;
            //levels.TryGetValue(MenuController.Level, out size);

            pairMaterialValues = new PairMaterialValue[2];
            gameObjects = new List<GameObject>();
            points = 0;
            GenerateRandomPairValues();
            nextSpawnPosition = new Vector3(spawnLimits.x, -spawnLimits.y + 0.5f, -spawnLimits.z + 1);
        }

        [UsedImplicitly]
        private void Start()
        {
            for (var i = 0; i < numberOfGameObjects(); i++)
            {
                gameObjects.Add(Instantiate(prefabGameObject, nextSpawnPosition, Quaternion.identity));
                CalculateNextPosition();
            }
            AssociateGameObjectsToValue();
        }

        private int numberOfGameObjects()
        {
            return pairMaterialValues.Length;
        }

        private void AssociateGameObjectsToValue()
        {
            for (var i = 0; i < numberOfGameObjects(); i++)
            {
                var boxInteractiveItem = GetScriptBoxInteractiveItem(gameObjects[i]);
                boxInteractiveItem.hiddenValue = pairMaterialValues[i].value;
                boxInteractiveItem.m_ClickedMaterial = pairMaterialValues[i].material;
            }
        }

        private void GenerateRandomPairValues()
        {
            for (var i = 0; i < numberOfGameObjects() / 2; i++)
                pairMaterialValues[i] = new PairMaterialValue(materials[i], i);
            Array.Copy(pairMaterialValues, 0, pairMaterialValues, numberOfGameObjects() / 2, numberOfGameObjects() / 2);
            pairMaterialValues = pairMaterialValues.OrderBy(a => Guid.NewGuid()).ToArray();
        }

        private BoxInteractiveItem GetScriptBoxInteractiveItem(GameObject item)
        {
            return item.GetComponent(typeof(BoxInteractiveItem)) as BoxInteractiveItem;
        }

        private void CalculateNextPosition()
        {
            if (nextSpawnPosition.z + 2 > spawnLimits.z)
            {
                nextSpawnPosition.z = -spawnLimits.z - 1;
                nextSpawnPosition.y += 2;
            }
            nextSpawnPosition = new Vector3(nextSpawnPosition.x, nextSpawnPosition.y, nextSpawnPosition.z + 2);
        }

        // Update is called once per frame
        private void Update()
        {
            StartCoroutine(CheckSelectedItems());
        }

        private IEnumerator CheckSelectedItems()
        {
            var selectedItems = gameObjects.Where(item => GetScriptBoxInteractiveItem(item).IsSelected).ToList();
            if (selectedItems.Count < 2) yield break;
            if (sameHiddenValue(selectedItems))
            {
                points += 10;
                selectedItems.ForEach(Destroy);
                gameObjects.RemoveAll(a => selectedItems.Contains(a));
                Debug.Log(points);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
            selectedItems.ForEach(a => GetScriptBoxInteractiveItem(a).IsSelected = false);
            if (gameObjects.Count == 0) SceneManager.LoadScene(0);
        }

        private bool sameHiddenValue(List<GameObject> selectedItems)
        {
            return
                GetScriptBoxInteractiveItem(selectedItems[0])
                    .hiddenValue.Equals(GetScriptBoxInteractiveItem(selectedItems[1]).hiddenValue);
        }

        public class PairMaterialValue
        {
            public Material material;

            public int value;

            public PairMaterialValue(Material material, int value)
            {
                this.material = material;
                this.value = value;
            }
        }
    }
}
