using System;
using System.Collections.Generic;

class Solution {
    public int solution(int[] A) {
        // Implemented solution
        var startPositions = new List<int>();
        var endPositions = new List<int>();
        var disksCount = A.Length;
        for (int i = 0; i < disksCount; i++)
        {
            var currentRadius = A[i];
            startPositions.Add(i - currentRadius);
            endPositions.Add(i + currentRadius);
        }

        startPositions.Sort();
        endPositions.Sort();
        
        var intersections = 0;
        var startedDiscsCount = 0;
        var endPosShift = 0;
        
        for (var i = 0; i < disksCount; i++)  // for beginning points
        {
            for (var j = endPosShift; j < disksCount; j++)  // for end points
            {
                if (startPositions[i] <= endPositions[j])  // if this happens the disk is started
                {
                    intersections += startedDiscsCount;
                    if (intersections > 10000000)
                    {
                        return -1;
                    }

                    startedDiscsCount += 1;
                    break;
                }

                startedDiscsCount -= Math.Min(1, startedDiscsCount);  // disk is ended
                endPosShift += 1;  // we dont need to iterate through the ended disks
            }
        }

        return intersections;
    }
}