using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBall2 : MonoBehaviour
{
    private MyVector position;
    private MyVector bhPosition;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [SerializeField] private Transform blackHole;

    void Start() {
        position = new MyVector(transform.position.x, transform.position.y);
    }
    private void FixedUpdate() {
        Move();
        bhPosition = new MyVector(blackHole.position.x, blackHole.position.y);
        acceleration = bhPosition - position;
    }

    private void Update() {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);
    }

    public void Move() {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;        //Euler integrator

        transform.position = new Vector3(position.x, position.y);
        //transform.position = blackHole.position - transform.position;
    }
}
