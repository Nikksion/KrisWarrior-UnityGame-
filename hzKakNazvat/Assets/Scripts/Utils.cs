using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils {
    
    public static class Utils {

        public static Vector3 GetRandomDir() {
            return new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));
        }

        public static float GetRandomTime(float waitingTimeMin , float waitingTimeMax) {
            return (Random.Range(waitingTimeMin, waitingTimeMax));
        }
    }
}