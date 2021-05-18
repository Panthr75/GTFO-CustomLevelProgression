using CustomLevelProgression.Networking;
using MTFO.Managers;
using System;
using System.IO;

namespace CustomLevelProgression
{
    public class ProgressionData
    {
        public int a1;
        public int a2;
        public int a3;
        public int a4;
        public int a5;
        public int b1;
        public int b2;
        public int b3;
        public int b4;
        public int b5;
        public int c1;
        public int c2;
        public int c3;
        public int c4;
        public int c5;
        public int d1;
        public int d2;
        public int d3;
        public int d4;
        public int d5;
        public int e1;
        public int e2;
        public int e3;
        public int e4;
        public int e5;

        public ProgressionData() { }
        public ProgressionData(pProgressionData data)
        {
            this.a1 = data.a1State;
            this.a2 = data.a2State;
            this.a3 = data.a3State;
            this.a4 = data.a4State;
            this.a5 = data.a5State;
            this.b1 = data.b1State;
            this.b2 = data.b2State;
            this.b3 = data.b3State;
            this.b4 = data.b4State;
            this.b5 = data.b5State;
            this.c1 = data.c1State;
            this.c2 = data.c2State;
            this.c3 = data.c3State;
            this.c4 = data.c4State;
            this.c5 = data.c5State;
            this.d1 = data.d1State;
            this.d2 = data.d2State;
            this.d3 = data.d3State;
            this.d4 = data.d4State;
            this.d5 = data.d5State;
            this.e1 = data.e1State;
            this.e2 = data.e2State;
            this.e3 = data.e3State;
            this.e4 = data.e4State;
            this.e5 = data.e5State;
        }

        public pProgressionData ToPacket()
        {
            return new pProgressionData()
            {
                a1State = this.a1,
                a2State = this.a2,
                a3State = this.a3,
                a4State = this.a4,
                a5State = this.a5,

                b1State = this.b1,
                b2State = this.b2,
                b3State = this.b3,
                b4State = this.b4,
                b5State = this.b5,

                c1State = this.c1,
                c2State = this.c2,
                c3State = this.c3,
                c4State = this.c4,
                c5State = this.c5,

                d1State = this.d1,
                d2State = this.d2,
                d3State = this.d3,
                d4State = this.d4,
                d5State = this.d5,

                e1State = this.e1,
                e2State = this.e2,
                e3State = this.e3,
                e4State = this.e4,
                e5State = this.e5,
            };
        }

        public int GetProgressionValue(eRundownTier tier, int expeditionIndex)
        {
            switch (tier)
            {
                case eRundownTier.TierA:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.a1;
                        case 1:
                            return this.a2;
                        case 2:
                            return this.a3;
                        case 3:
                            return this.a4;
                        case 4:
                            return this.a5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierB:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.b1;
                        case 1:
                            return this.b2;
                        case 2:
                            return this.b3;
                        case 3:
                            return this.b4;
                        case 4:
                            return this.b5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierC:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.c1;
                        case 1:
                            return this.c2;
                        case 2:
                            return this.c3;
                        case 3:
                            return this.c4;
                        case 4:
                            return this.c5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierD:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.d1;
                        case 1:
                            return this.d2;
                        case 2:
                            return this.d3;
                        case 3:
                            return this.d4;
                        case 4:
                            return this.d5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierE:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.e1;
                        case 1:
                            return this.e2;
                        case 2:
                            return this.e3;
                        case 3:
                            return this.e4;
                        case 4:
                            return this.e5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(tier));
            }
        }

        public void SetProgressionValue(eRundownTier tier, int expeditionIndex, int value)
        {
            switch (tier)
            {
                case eRundownTier.TierA:
                    switch (expeditionIndex)
                    {
                        case 0:
                            this.a1 = value;
                            break;
                        case 1:
                            this.a2 = value;
                            break;
                        case 2:
                            this.a3 = value;
                            break;
                        case 3:
                            this.a4 = value;
                            break;
                        case 4:
                            this.a5 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                    break;
                case eRundownTier.TierB:
                    switch (expeditionIndex)
                    {
                        case 0:
                            this.b1 = value;
                            break;
                        case 1:
                            this.b2 = value;
                            break;
                        case 2:
                            this.b3 = value;
                            break;
                        case 3:
                            this.b4 = value;
                            break;
                        case 4:
                            this.b5 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                    break;
                case eRundownTier.TierC:
                    switch (expeditionIndex)
                    {
                        case 0:
                            this.c1 = value;
                            break;
                        case 1:
                            this.c2 = value;
                            break;
                        case 2:
                            this.c3 = value;
                            break;
                        case 3:
                            this.c4 = value;
                            break;
                        case 4:
                            this.c5 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                    break;
                case eRundownTier.TierD:
                    switch (expeditionIndex)
                    {
                        case 0:
                            this.d1 = value;
                            break;
                        case 1:
                            this.d2 = value;
                            break;
                        case 2:
                            this.d3 = value;
                            break;
                        case 3:
                            this.d4 = value;
                            break;
                        case 4:
                            this.d5 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                    break;
                case eRundownTier.TierE:
                    switch (expeditionIndex)
                    {
                        case 0:
                            this.e1 = value;
                            break;
                        case 1:
                            this.e2 = value;
                            break;
                        case 2:
                            this.e3 = value;
                            break;
                        case 3:
                            this.e4 = value;
                            break;
                        case 4:
                            this.e5 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tier));
            }
        }

        public void Save()
        {
            if (ConfigManager.HasCustomContent)
            {
                var filePath = Path.Combine(ConfigManager.CustomPath, "Rundown_Progression.data");

                File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(this));
            }
        }

        public static ProgressionData LoadFromFile()
        {
            if (ConfigManager.HasCustomContent)
            {
                var filePath = Path.Combine(ConfigManager.CustomPath, "Rundown_Progression.data");
                ProgressionData data;

                if (!File.Exists(filePath))
                {
                    data = new ProgressionData();
                    File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
                else
                {
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<ProgressionData>(File.ReadAllText(filePath));
                }

                return data;
            }

            return null;
        }
    }
}