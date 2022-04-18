using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private GameInputs _input;
    [SerializeField] private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _input = new GameInputs();
        _input.Player.Enable();
        _input.Player.SlowTime.performed += SlowTime_performed;
        _input.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.TogglePause();
    }

    private void SlowTime_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.SlowDownTime();
    }

    // Update is called once per frame
    void Update()
    {
        _player.Movement(_input.Player.Movement.ReadValue<Vector2>());
        if (_input.Player.Shoot.ReadValue<float>() == 1)
            _player.Shoot();
    }

    private void OnDisable()
    {
        _input.Player.SlowTime.performed -= SlowTime_performed;
        _input.Player.Pause.performed -= Pause_performed;
    }
}
