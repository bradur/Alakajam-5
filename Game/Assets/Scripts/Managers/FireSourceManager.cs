// Date   : 23.02.2019 09:54
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireSourceManager : MonoBehaviour {

    public static FireSourceManager main;

    void Awake() {
        main = this;
    }

    private List<FireSource> fireSources = new List<FireSource>();
    public void AddFireSource(FireSource fireSource) {
        fireSources.Add(fireSource);
    }

    static int SortByDistance(FireSource sourceA, FireSource sourceB)
     {
         return sourceA.distanceToCursor.CompareTo(sourceB.distanceToCursor);
     }

    // Returns list of sources sorted by distance to player
    public List<FireSource> GetLitNearSources(Vector3 position) {
        List<FireSource> nearSources = new List<FireSource>();
        foreach(FireSource source in fireSources) {
            if (source.IsLit) {
                source.distanceToCursor = Vector3.Distance(position, source.transform.position);
                if (source.distanceToCursor <= source.Range) {
                    nearSources.Add(source);
                }
            }
        }
        nearSources.Sort(SortByDistance);
        return nearSources;
    }
}
