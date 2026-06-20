using System;
using System.Collections.Generic;
using Brick;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class BrickManager : Singleton<BrickManager>
    {
        [Header("References")] [SerializeField]
        private BrickController brickPrefab;

        [Header("Spawn Settings")] [SerializeField]
        private int minRow = 3;

        [SerializeField] private int maxRow = 6;
        [SerializeField] private int minCol = 5;
        [SerializeField] private int maxCol = 7;

        [Header("Grid Layout")] [SerializeField]
        private Vector2 spacing = new(0.7f, 0.5f);

        [SerializeField] private Vector2 startPosition = new(0f, 3f);

        private List<BrickController> _brickPool;
        private int _activeBricks;

        protected override void Start()
        {
            base.Start();

            SpawnBrickGrid();
        }

        private void Update()
        {
            if (_activeBricks <= 0) SpawnBrickGrid();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();

            LoadBrickPool();
        }

        private void LoadBrickPool()
        {
            _brickPool = new List<BrickController>();
            var maxBrick = maxCol * maxRow;
            for (var i = 0; i < maxBrick; i++)
            {
                var brick = Instantiate(brickPrefab, transform);
                brick.gameObject.SetActive(false);
                _brickPool.Add(brick);
            }
        }

        private void SpawnBrickGrid()
        {
            var rows = Random.Range(minRow, maxRow + 1);
            var cols = Random.Range(minCol, maxCol + 1);

            _activeBricks = rows * cols;

            var halfWidth = (cols - 1) * spacing.x / 2f;
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < cols; c++)
            {
                var brick = GetBrickFromPool();
                var posX = -halfWidth + c * spacing.x;
                var posY = startPosition.y - r * spacing.y;
                brick.transform.position = new Vector2(posX, posY);
            }
        }

        private BrickController GetBrickFromPool()
        {
            var lastIndex = _brickPool.Count - 1;
            var brick = _brickPool[lastIndex];
            brick.gameObject.SetActive(true);
            _brickPool.RemoveAt(lastIndex);
            return brick;
        }

        public void ReturnBrickToPool(BrickController brick)
        {
            brick.gameObject.SetActive(false);
            _brickPool.Add(brick);
            _activeBricks--;
        }
    }
}