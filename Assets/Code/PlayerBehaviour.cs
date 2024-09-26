using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums

public enum Players
{
    ONE,
    TWO
}

#endregion

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerBehaviour : MonoBehaviour
{
    public Players playerNum;

    #region Knobs

    [SerializeField] private float playerMovSpeed;

    #endregion

    #region RuntimeVariables

    [SerializeField, HideInInspector] Rigidbody _rb;
    [SerializeField, HideInInspector] MeshRenderer _meshRenderer;
    [SerializeField, HideInInspector] MeshFilter _meshFilter;
    [SerializeField, HideInInspector] GameObject _playerModel;

    #endregion

    #region References

    [SerializeField, HideInInspector] private Animator _animator;

    #endregion

    #region UnityMethods
    private void Start()
    {
        InitializePlayer();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        AnimatorHandler();
        ModelDirectionRotation();
    }
    #endregion

    #region LocalMethods

    void InitializePlayer()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        if (_meshFilter == null)
        {
            _meshFilter = GetComponent<MeshFilter>();
        }
    }

    void PlayerMovement()
    {
        switch (playerNum)
        {
            case Players.ONE:
                float horInput1 = Input.GetAxis("Horizontal");
                float verInput1 = Input.GetAxis("Vertical");
                Vector3 velocityInput = new Vector3(horInput1, 0.0f, verInput1);

                _rb.velocity = velocityInput * playerMovSpeed;
                break;

            case Players.TWO:
                float horInput2 = Input.GetAxis("Horizontal2");
                float verInput2 = Input.GetAxis("Vertical2");
                Vector3 velocityInput2 = new Vector3(horInput2, 0.0f, verInput2);

                _rb.velocity = velocityInput2 * playerMovSpeed;
                break;

            default:
                float horInputDefault = Input.GetAxis("Horizontal");
                float verInputDefault = Input.GetAxis("Vertical");
                Vector3 velocityInputDefault = new Vector3(horInputDefault, 0.0f, verInputDefault);

                _rb.velocity = velocityInputDefault * playerMovSpeed;
                break;
        }
    }

    private void AnimatorHandler()
    {
        _animator.SetFloat("VelocityMagnitude", _rb.velocity.magnitude);
    }

    private void ModelDirectionRotation()
    {
        if (_rb.velocity.magnitude > 0.1f)
        {
            Vector3 velocityDirection = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            Quaternion modelRotation = Quaternion.LookRotation(velocityDirection);
            _playerModel.transform.rotation = Quaternion.Euler(0, modelRotation.eulerAngles.y, 0);
        }
        
    }
    #endregion
}
