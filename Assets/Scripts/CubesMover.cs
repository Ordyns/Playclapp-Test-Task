using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesMover : MonoBehaviour
{
    public event System.Action<Cube> CubeMoved;

    private LinkedList<MoveOperation> _moveOperations = new LinkedList<MoveOperation>();

    public void AddCube(Cube cube, float speed, float distance){
        MoveOperation moveOperation = new MoveOperation(cube, speed, distance);
        moveOperation.Finished += OnOperationFinished;
        _moveOperations.AddLast(moveOperation);
    }

    private void Update() {
        foreach(MoveOperation operation in _moveOperations){
            operation.Update();
        }

        RemoveFinishedOperations();
    }

    private void RemoveFinishedOperations(){
        var current = _moveOperations.First;
        while(current != null){
            var next = current.Next;

            if(current.Value.IsFinished)
                _moveOperations.Remove(current);

            current = next;
        }
    }

    private void OnOperationFinished(MoveOperation moveOperation){
        CubeMoved?.Invoke(moveOperation.TargetObject);
    }

    private class MoveOperation
    {
        public bool IsFinished { get; private set; }
        public event System.Action<MoveOperation> Finished;

        public readonly Cube TargetObject;

        public float _speed;
        public float _distance;

        private Vector3 _startPosition;
        private float _progress;

        public MoveOperation(Cube targetObject, float speed, float distance){
            IsFinished = false;

            TargetObject = targetObject;
            _speed = speed;
            _distance = distance; // TODO: axis selection

            _startPosition = targetObject.transform.position;
            _progress = 0;
        }

        public void Update(){
            if(IsFinished)
                return;

            _progress += Time.deltaTime * _speed;

            TargetObject.transform.position = new Vector3(_progress, 0, 0);
            if(Vector3.Distance(_startPosition, TargetObject.transform.position) >= _distance){
                IsFinished = true;
                Finished?.Invoke(this);
            }
        }
    }
}
