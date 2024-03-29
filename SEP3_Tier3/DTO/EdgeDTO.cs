﻿namespace SEP3_TIER3.DTO
{
    public class EdgeDTO
    {
        public int EdgeId { get; set; }
        public int Length { get; set; }
        public int FromNodeIndex { get; set; }
        public int ToNodeIndex { get; set; }

        public bool IsGroundEdge { get; set; }

        public override string ToString()
        {
            return $"IdEdge {EdgeId} \nLength {Length} \nFromNodeIndex {FromNodeIndex} \nToNodeIndex {ToNodeIndex}";
        }
    }
}
