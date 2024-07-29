using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
    public class PipeManager : MonoBehaviour
    {
        [Tooltip("The prefab of the pipe pair to spawn")]
        [SerializeField] private GameObject _pipePrefab;
        [Tooltip("How far up or down the pipes can spawn from centre")]
        [SerializeField] private float _pipeSpawnRange;
        [Tooltip("How long to wait between pipes")]
        [SerializeField] private float _pipeSpawnDelay;

        // Track the last moment a pipe is spawned in
        private float _pipeLastSpawnTime;
        //Track all the pipes within our scene (max of 4)
        private PipePair[] _pipesInScene = new PipePair[4];
        // Track the last pipe we spawned
        private int _pipeIndex;

        // 
        public PipePair CurrentPipe => _pipesInScene[_pipeIndex];

        void Update()
        {
            if (RoundManager.RoundActive &&
                Time.time > _pipeLastSpawnTime + _pipeSpawnDelay)
            {
                // Caches the last spawn time
                SpawnPipe();
            }
        }

        private void SpawnPipe()
        {
            // Set the last spawned time to now
            _pipeLastSpawnTime = Time.time;
            // Randomise the y-position of the pipes
            float yOffset = Random.Range(-_pipeSpawnRange, _pipeSpawnRange);
            // Instantiate the pipes if we need to
            if (CurrentPipe == null)
            {
                _pipesInScene[_pipeIndex] = Instantiate(_pipePrefab, transform.position,
                    Quaternion.identity).GetComponent<PipePair>();
            }
            // Apply the y-position to the current pair  
            CurrentPipe.transform.position = transform.position + Vector3.up * yOffset;

            // Start the pipe
            CurrentPipe.Go();

            // Change our index to the next set of pipes
            _pipeIndex++;
            if (_pipeIndex == _pipesInScene.Length)
            {
                _pipeIndex = 0;
            }
        }

        public void Stop()
        {
            foreach (PipePair pair in _pipesInScene)
            {
                if (pair == null) 
                    continue;

                pair.Stop();
            }
        }
        public void Reset()
        {
            _pipeLastSpawnTime = Time.time;
            foreach (PipePair pair in _pipesInScene)
            {
                if (pair == null) 
                    continue;

                pair.transform.position = transform.position;
            }
        }
    }
}


