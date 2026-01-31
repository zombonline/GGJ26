using System;
using UnityEngine;

public class MboyChartReader : MonoBehaviour
{
    public TextAsset mboyJson;
    public SongChart chart;

    [SerializeField] private GameObject jumpObstacle, attackObstacle;

    [ContextMenu("Print Note Times")]
    public void PrintNoteTimes()
    {
        if (mboyJson == null)
        {
            Debug.LogError("No JSON file assigned");
            return;
        }

        var data = JsonUtility.FromJson<MboyRoot>(mboyJson.text);

        float tempo = data.tempo;
        float secondsPerBeat = 60f / tempo;

        const float ticksPerBeat = 400f;
        const float beatsPerBar = 4f;
        float ticksPerBar = ticksPerBeat * beatsPerBar;
        
        
        chart.markers.Clear();
        foreach (var track in data.tracks)
        {
            foreach (var bar in track.bars)
            {
                foreach (var note in bar.notes)
                {
                    float absoluteTicks = bar.index * ticksPerBar + note.pos;
                    float beats = absoluteTicks / ticksPerBeat;
                    float timeSeconds = beats * secondsPerBeat;

                    Debug.Log(
                        $"Track {track.name} | Bar {bar.index} | Tick {note.pos} -> {timeSeconds:F3}s");
                    Debug.Log(track.name);
                    GameObject obstacle = null;
                    switch (track.name)
                    {
                        case "Jump":
                            obstacle = jumpObstacle;
                            break;
                        case "Attack":
                            obstacle = attackObstacle;
                            break;
                    }
                    if(obstacle == null) continue; // skip if no obstacle (e.g. Jump track)
                    var newMarker = new SongChart.Marker{time = timeSeconds, obstacle = obstacle};
                    chart.markers.Add(newMarker);
                }
            }
        }
    }
    
    

    [Serializable]
    public class MboyRoot
    {
        public float tempo;
        public Track[] tracks;
    }

    [Serializable]
    public class Track
    {
        public string name;
        public Bar[] bars;
    }

    [Serializable]
    public class Bar
    {
        public int index;
        public Note[] notes;
    }

    [Serializable]
    public class Note
    {
        public int pos;
        public int len;
    }
}