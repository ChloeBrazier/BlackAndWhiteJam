using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputMap inputMap;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        inputMap = new InputMap();
    }

    private void OnEnable()
    {
        inputMap.Enable();
    }

    private void OnDisable()
    {
        inputMap.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float moveDirection = inputMap.Gameplay.Move.ReadValue<float>();

        if (moveDirection > 0.0f)
        {
            transform.position += new Vector3(moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        else if (moveDirection < 0.0f)
        {
            transform.position += new Vector3(-moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }
}
