using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class walkAround : MonoBehaviour, ITrackableEventHandler
{
    public GameObject model;
    private Bounds bounds;
    private Mesh mesh;
    public float speed = 0.5f;
    private bool isTracked = false;
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    public GameObject[] turnpoints;
    int current = 0;
    public float rotSpeed;
    public float TPRadius = 1;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
         mesh = GetComponent<MeshFilter>().mesh;
         bounds = mesh.bounds;
         mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            anim = model.GetComponent<Animator>();
        if (mTrackableBehaviour) {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    
    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }
 public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + 
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            isTracked = true;
            OnTrackingFound();
           
        }

        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                    newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            isTracked = false;
             OnTrackingLost();
            
        }else{
            isTracked = false;
             OnTrackingLost();
        }
    }


        protected virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;
        }

    }


    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isTracked)
        {  
            if (Vector3.Distance(turnpoints[current].transform.localPosition, model.transform.localPosition) < TPRadius)
            {
                current++;
                if(current >= turnpoints.Length)
                 {
                     current = 0;
                 }
            }
            model.transform.localPosition = Vector3.MoveTowards(model.transform.localPosition, turnpoints[current].transform.localPosition, Time.deltaTime * speed);
            
            //model.transform.LookAt(turnpoints[current].transform);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation,Quaternion.LookRotation(turnpoints[current].transform.localPosition - model.transform.localPosition), Time.deltaTime * rotSpeed);
           // Debug.Log("Current: "+turnpoints[current].name);

           if(mTrackableBehaviour.TrackableName == "Oxygen" && speed < speed*0.9f)
           {
               speed++;
           }



        
        }

    }

}
