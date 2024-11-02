using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance {
        get; private set;
    }
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private bool _isRunSide = false;
    [SerializeField] private bool _isRunBack = false;
    [SerializeField] private bool _isRunFront = false;
    [SerializeField] private bool _isMove = false;
    private Player _player;
    private float _dirX, _dirY;
    private float _angle;
    private float _angleDegrees; 
    private bool _isFlip = false;
    private void Awake() {
        Instance = this;
        GetComponentInChildren<Weapon>();
        _player = FindObjectOfType<Player>();
    }
    private void Update() {
        PlayerAnimanion();
    }
    public void FlipPlayer() {
        if (_dirX > 0 && !_isFlip)
            _isFlip = true;
        else if (_dirX < 0 && _isFlip)
            _isFlip = false;
    }
    public bool Flip() {
        return _isFlip;
    }
    public bool IsRunningBack() {
        return _isRunBack;
    }
    public bool IsRunningFront() {
        return _isRunFront;
    }
    public bool IsRunningSide() {
        return _isRunSide;
    }
    public bool IsMove() {
        if (_dirX == 0 && _dirY == 0)
            _isMove = false;
        else
            _isMove = true;
        return _isMove;
    }
    //¬озвращает вектор передевидени€ со стика
    public Vector2 GetMovmentPlayer() {
        return new Vector2(_dirX, _dirY).normalized;
    }
    //ќтвечает за состо€ни€ при движении стика со секторам
  private void PlayerAnimanion() {
        if (_player.GetInputFlag()) {
            _dirX = _joystick.Horizontal;
            _dirY = _joystick.Vertical;
            _angle = Mathf.Atan2(_dirY, _dirX);
            _angleDegrees = _angle * (180 / Mathf.PI);
            _isRunBack = false;
            _isRunSide = false;
            _isRunFront = false;
        if (_angleDegrees <= -45 && _angleDegrees > -135) {
                _isRunFront = true;
                _weapon.WeaponFlipQuaternion(0, 0, -90);
        }
        else if (_angleDegrees >= 45 && _angleDegrees < 135) {
                _isRunBack = true;
                _weapon.WeaponFlipQuaternion(0, 0, 90);
        }
        else if (_angleDegrees >= 135 && _angleDegrees <= 180 || _angleDegrees >= -180 && _angleDegrees <= -135) {
                _isRunSide = true;
                _weapon.WeaponFlipQuaternion(0, 180, 0);
            FlipPlayer();
        }
        else if (_angleDegrees > -45 && _angleDegrees < 0 || _angleDegrees > 0 && _angleDegrees < 45) {
                _isRunSide = true;
                _weapon.WeaponFlipQuaternion(0, 0, 0);
            FlipPlayer();
        }
        }
    }
}
