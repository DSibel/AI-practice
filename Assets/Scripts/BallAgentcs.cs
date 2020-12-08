
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BallAgentcs : Agent
{


    Rigidbody rBody;
    public Transform target;
    public float speed =25;
    // Start is called before the first frame update
    void Start()
    {
        rBody=GetComponent<Rigidbody>();
    }

public override void OnEpisodeBegin(){
    this.rBody.angularVelocity = Vector3.zero;
     this.rBody.velocity = Vector3.zero;
     this.transform.localPosition= new Vector3(-9,0.5f,0);




     //move target
     target.localPosition = new Vector3(12+Random.value*8,Random.value*3,Random.value*10 -5);

}
public override void CollectObservations(VectorSensor sensor){
    sensor.AddObservation(target.localPosition);
    sensor.AddObservation(this.transform.localPosition);
    sensor.AddObservation(rBody.velocity);
}
public override void OnActionReceived(float[] vectorAction){
    Vector3 controlSignal =Vector3.zero;
    //controlSignal= 0 or 1| 1 moves it forward, 0 doesnt
    controlSignal.x = vectorAction[0];

    if(vectorAction[1]==2){
        controlSignal.z =1;
    }else{
        controlSignal.z =-vectorAction[1];
    }

    if(this.transform.localPosition.x<8.25){
    rBody.AddForce(controlSignal*speed);
    }
float distanceToTarget =Vector3.Distance(this.transform.localPosition,target.localPosition);
        if(distanceToTarget <1.42f){
            SetReward(1f);
            EndEpisode();
        }




        if(this.transform.localPosition.y<0){
            EndEpisode();
        }

}

public override void Heuristic(float[] actionsOut){
    actionsOut[0]=Input.GetAxis("Vertical");
    actionsOut[1]=Input.GetAxis("Horizontal");
}







}
