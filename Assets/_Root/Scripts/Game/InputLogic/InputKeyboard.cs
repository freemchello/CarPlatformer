using Game.InputLogic;
using JoostenProductions;
using UnityEngine;

internal class InputKeyboard : BaseInputView
{
    [SerializeField] private float _inputMultiplier = 0.01f;


    private void Start() =>
        UpdateManager.SubscribeToUpdate(Move);

    private void OnDestryoy() =>
        UpdateManager.UnsubscribeFromUpdate(Move);

    private void Move()
    {
        float moveValue = _speed * _inputMultiplier * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            OnLeftMove(moveValue);

        if (Input.GetKey(KeyCode.RightArrow))
            OnRightMove(moveValue);
    }
}
