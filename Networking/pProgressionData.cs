using Agents;
using System.Runtime.InteropServices;

namespace CustomLevelProgression.Networking
{
    [StructLayout(LayoutKind.Sequential)]
    public struct pProgressionData
    {
        public int a1State;
        public int a2State;
        public int a3State;
        public int a4State;
        public int a5State;
        public int b1State;
        public int b2State;
        public int b3State;
        public int b4State;
        public int b5State;
        public int c1State;
        public int c2State;
        public int c3State;
        public int c4State;
        public int c5State;
        public int d1State;
        public int d2State;
        public int d3State;
        public int d4State;
        public int d5State;
        public int e1State;
        public int e2State;
        public int e3State;
        public int e4State;
        public int e5State;
        public pPlayerAgent to;

        public ProgressionData Create()
        {
            return new ProgressionData(this);
        }
    }
}