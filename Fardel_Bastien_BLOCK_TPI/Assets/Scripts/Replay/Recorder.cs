using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Queue<Rewind_Data> recording_queue { get; private set; }

    private void Awake()
    {
        recording_queue = new Queue<Rewind_Data>();
    }

    public void RecordRewindFrame(Rewind_Data data)
    {
        recording_queue.Enqueue(data);
        //Debug.Log("Recorded data: " + data.position);
    }
}
