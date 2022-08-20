using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBall : MonoBehaviour
{
    private MyVector position;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [Range(0f, 1f)] [SerializeField] private float dampingFactor;
    [SerializeField] Camera camera;

    private MyVector temp;
    private float temp2 = 1;

    void Start() {
        position = new MyVector(transform.position.x, transform.position.y);
    }
    private void FixedUpdate() {
        Move();
    }

    private void Update() {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            //acceleration = directions[(currentAcceleration++) % directions.Lenght]
            temp2 *= -1;
            temp.y = acceleration.y;
            temp.x = acceleration.x;
            acceleration.y = temp.x * temp2;
            acceleration.x = temp.y * temp2;
            velocity *= 0;
        }
    }

    public void Move() {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;        //Euler integrator

        if (position.x > camera.orthographicSize) {             //Right
            velocity.x *= -1;
            position.x = camera.orthographicSize;
            velocity *= dampingFactor; //Damping factor
        } else if (position.x < -camera.orthographicSize) {     //Left
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor; //Damping factor
        } else if (position.y < -camera.orthographicSize) {     //Down
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor; //Damping factor
        } else if (position.y > camera.orthographicSize) {      //Up
            velocity.y *= -1;
            position.y = camera.orthographicSize;
            velocity *= dampingFactor; //Damping factor
        }
        transform.position = new Vector3(position.x, position.y);
    }
}
