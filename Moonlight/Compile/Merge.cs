using System;
using System.Collections.Generic;
using Moonlight.Variable;
using Terraria.ModLoader;

namespace Moonlight.Compile{
    class Merge{
        static public MLMod tt = new MLMod("TerraTweaker");

        public static int Priority(char action){
            switch(action){
                case ':':
                case '$': return 9;
                case '*': return 8;
                case ')': return -10;
                default: return 0;
        }}

        public static IVariable Work(Cell curr, ref int index, List<Cell> cells, bool onlyOne = false){
            while(index < cells.Count){
                Cell next = cells[index++];
                while(!curr.CanMerge(next)) Work(next, ref index, cells, true);
                curr.MergeCell(next);
                if(onlyOne) return curr.Value;
            }
            return curr.Value;
        }
    }
}