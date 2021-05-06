using MTFO.Managers;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CustomLevelProgression
{
    public class CompletionData
    {
        public LevelCompletions A1 { get; set; }
        public LevelCompletions A2 { get; set; }
        public LevelCompletions A3 { get; set; }
        public LevelCompletions A4 { get; set; }
        public LevelCompletions A5 { get; set; }

        public LevelCompletions B1 { get; set; }
        public LevelCompletions B2 { get; set; }
        public LevelCompletions B3 { get; set; }
        public LevelCompletions B4 { get; set; }
        public LevelCompletions B5 { get; set; }

        public LevelCompletions C1 { get; set; }
        public LevelCompletions C2 { get; set; }
        public LevelCompletions C3 { get; set; }
        public LevelCompletions C4 { get; set; }
        public LevelCompletions C5 { get; set; }

        public LevelCompletions D1 { get; set; }
        public LevelCompletions D2 { get; set; }
        public LevelCompletions D3 { get; set; }
        public LevelCompletions D4 { get; set; }
        public LevelCompletions D5 { get; set; }

        public LevelCompletions E1 { get; set; }
        public LevelCompletions E2 { get; set; }
        public LevelCompletions E3 { get; set; }
        public LevelCompletions E4 { get; set; }
        public LevelCompletions E5 { get; set; }

        [JsonIgnore] public int TotalTierACompletes_High
        {
            get
            {
                int result = 0;
                if (A1.highCompletes > 0)
                    result++;
                if (A2.highCompletes > 0)
                    result++;
                if (A3.highCompletes > 0)
                    result++;
                if (A4.highCompletes > 0)
                    result++;
                if (A5.highCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierBCompletes_High
        {
            get
            {
                int result = 0;
                if (B1.highCompletes > 0)
                    result++;
                if (B2.highCompletes > 0)
                    result++;
                if (B3.highCompletes > 0)
                    result++;
                if (B4.highCompletes > 0)
                    result++;
                if (B5.highCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierCCompletes_High
        {
            get
            {
                int result = 0;
                if (C1.highCompletes > 0)
                    result++;
                if (C2.highCompletes > 0)
                    result++;
                if (C3.highCompletes > 0)
                    result++;
                if (C4.highCompletes > 0)
                    result++;
                if (C5.highCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierDCompletes_High
        {
            get
            {
                int result = 0;
                if (D1.highCompletes > 0)
                    result++;
                if (D2.highCompletes > 0)
                    result++;
                if (D3.highCompletes > 0)
                    result++;
                if (D4.highCompletes > 0)
                    result++;
                if (D5.highCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierECompletes_High
        {
            get
            {
                int result = 0;
                if (E1.highCompletes > 0)
                    result++;
                if (E2.highCompletes > 0)
                    result++;
                if (E3.highCompletes > 0)
                    result++;
                if (E4.highCompletes > 0)
                    result++;
                if (E5.highCompletes > 0)
                    result++;

                return result;
            }
        }

        [JsonIgnore] public int TotalTierACompletes_Extreme
        {
            get
            {
                int result = 0;
                if (A1.extremeCompletes > 0)
                    result++;
                if (A2.extremeCompletes > 0)
                    result++;
                if (A3.extremeCompletes > 0)
                    result++;
                if (A4.extremeCompletes > 0)
                    result++;
                if (A5.extremeCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierBCompletes_Extreme
        {
            get
            {
                int result = 0;
                if (B1.extremeCompletes > 0)
                    result++;
                if (B2.extremeCompletes > 0)
                    result++;
                if (B3.extremeCompletes > 0)
                    result++;
                if (B4.extremeCompletes > 0)
                    result++;
                if (B5.extremeCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierCCompletes_Extreme
        {
            get
            {
                int result = 0;
                if (C1.extremeCompletes > 0)
                    result++;
                if (C2.extremeCompletes > 0)
                    result++;
                if (C3.extremeCompletes > 0)
                    result++;
                if (C4.extremeCompletes > 0)
                    result++;
                if (C5.extremeCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierDCompletes_Extreme
        {
            get
            {
                int result = 0;
                if (D1.extremeCompletes > 0)
                    result++;
                if (D2.extremeCompletes > 0)
                    result++;
                if (D3.extremeCompletes > 0)
                    result++;
                if (D4.extremeCompletes > 0)
                    result++;
                if (D5.extremeCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierECompletes_Extreme
        {
            get
            {
                int result = 0;
                if (E1.extremeCompletes > 0)
                    result++;
                if (E2.extremeCompletes > 0)
                    result++;
                if (E3.extremeCompletes > 0)
                    result++;
                if (E4.extremeCompletes > 0)
                    result++;
                if (E5.extremeCompletes > 0)
                    result++;

                return result;
            }
        }

        [JsonIgnore] public int TotalTierACompletes_Overload
        {
            get
            {
                int result = 0;
                if (A1.overloadCompletes > 0)
                    result++;
                if (A2.overloadCompletes > 0)
                    result++;
                if (A3.overloadCompletes > 0)
                    result++;
                if (A4.overloadCompletes > 0)
                    result++;
                if (A5.overloadCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierBCompletes_Overload
        {
            get
            {
                int result = 0;
                if (B1.overloadCompletes > 0)
                    result++;
                if (B2.overloadCompletes > 0)
                    result++;
                if (B3.overloadCompletes > 0)
                    result++;
                if (B4.overloadCompletes > 0)
                    result++;
                if (B5.overloadCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierCCompletes_Overload
        {
            get
            {
                int result = 0;
                if (C1.overloadCompletes > 0)
                    result++;
                if (C2.overloadCompletes > 0)
                    result++;
                if (C3.overloadCompletes > 0)
                    result++;
                if (C4.overloadCompletes > 0)
                    result++;
                if (C5.overloadCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierDCompletes_Overload
        {
            get
            {
                int result = 0;
                if (D1.overloadCompletes > 0)
                    result++;
                if (D2.overloadCompletes > 0)
                    result++;
                if (D3.overloadCompletes > 0)
                    result++;
                if (D4.overloadCompletes > 0)
                    result++;
                if (D5.overloadCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierECompletes_Overload
        {
            get
            {
                int result = 0;
                if (E1.overloadCompletes > 0)
                    result++;
                if (E2.overloadCompletes > 0)
                    result++;
                if (E3.overloadCompletes > 0)
                    result++;
                if (E4.overloadCompletes > 0)
                    result++;
                if (E5.overloadCompletes > 0)
                    result++;

                return result;
            }
        }

        [JsonIgnore] public int TotalTierACompletes_PE
        {
            get
            {
                int result = 0;
                if (A1.peCompletes > 0)
                    result++;
                if (A2.peCompletes > 0)
                    result++;
                if (A3.peCompletes > 0)
                    result++;
                if (A4.peCompletes > 0)
                    result++;
                if (A5.peCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierBCompletes_PE
        {
            get
            {
                int result = 0;
                if (B1.peCompletes > 0)
                    result++;
                if (B2.peCompletes > 0)
                    result++;
                if (B3.peCompletes > 0)
                    result++;
                if (B4.peCompletes > 0)
                    result++;
                if (B5.peCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierCCompletes_PE
        {
            get
            {
                int result = 0;
                if (C1.peCompletes > 0)
                    result++;
                if (C2.peCompletes > 0)
                    result++;
                if (C3.peCompletes > 0)
                    result++;
                if (C4.peCompletes > 0)
                    result++;
                if (C5.peCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierDCompletes_PE
        {
            get
            {
                int result = 0;
                if (D1.peCompletes > 0)
                    result++;
                if (D2.peCompletes > 0)
                    result++;
                if (D3.peCompletes > 0)
                    result++;
                if (D4.peCompletes > 0)
                    result++;
                if (D5.peCompletes > 0)
                    result++;

                return result;
            }
        }
        [JsonIgnore] public int TotalTierECompletes_PE
        {
            get
            {
                int result = 0;
                if (E1.peCompletes > 0)
                    result++;
                if (E2.peCompletes > 0)
                    result++;
                if (E3.peCompletes > 0)
                    result++;
                if (E4.peCompletes > 0)
                    result++;
                if (E5.peCompletes > 0)
                    result++;

                return result;
            }
        }

        [JsonIgnore] public int TotalCompletes_High => TotalTierACompletes_High + TotalTierBCompletes_High + TotalTierCCompletes_High + TotalTierDCompletes_High + TotalTierECompletes_High;
        [JsonIgnore] public int TotalCompletes_Extreme => TotalTierACompletes_Extreme + TotalTierBCompletes_Extreme + TotalTierCCompletes_Extreme + TotalTierDCompletes_Extreme + TotalTierECompletes_Extreme;
        [JsonIgnore] public int TotalCompletes_Overload => TotalTierACompletes_Overload + TotalTierBCompletes_Overload + TotalTierCCompletes_Overload + TotalTierDCompletes_Overload + TotalTierECompletes_Overload;
        [JsonIgnore] public int TotalCompletes_PE => TotalTierACompletes_PE + TotalTierBCompletes_PE + TotalTierCCompletes_PE + TotalTierDCompletes_PE + TotalTierECompletes_PE;

        [JsonIgnore]
        public LevelCompletions Total
        {
            get
            {
                var result = new LevelCompletions();

                result.highCompletes = TotalCompletes_High;
                result.extremeCompletes = TotalCompletes_Extreme;
                result.overloadCompletes = TotalCompletes_Overload;
                result.peCompletes = TotalCompletes_PE;

                return result;
            }
        }

        public CompletionData()
        {
            A1 = new LevelCompletions();
            A2 = new LevelCompletions();
            A3 = new LevelCompletions();
            A4 = new LevelCompletions();
            A5 = new LevelCompletions();

            B1 = new LevelCompletions();
            B2 = new LevelCompletions();
            B3 = new LevelCompletions();
            B4 = new LevelCompletions();
            B5 = new LevelCompletions();

            C1 = new LevelCompletions();
            C2 = new LevelCompletions();
            C3 = new LevelCompletions();
            C4 = new LevelCompletions();
            C5 = new LevelCompletions();

            D1 = new LevelCompletions();
            D2 = new LevelCompletions();
            D3 = new LevelCompletions();
            D4 = new LevelCompletions();
            D5 = new LevelCompletions();

            E1 = new LevelCompletions();
            E2 = new LevelCompletions();
            E3 = new LevelCompletions();
            E4 = new LevelCompletions();
            E5 = new LevelCompletions();
        }

        public LevelCompletions GetData(eRundownTier tier, int expeditionIndex)
        {
            switch (tier)
            {
                case eRundownTier.TierA:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.A1;
                        case 1:
                            return this.A2;
                        case 2:
                            return this.A3;
                        case 3:
                            return this.A4;
                        case 4:
                            return this.A5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierB:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.B1;
                        case 1:
                            return this.B2;
                        case 2:
                            return this.B3;
                        case 3:
                            return this.B4;
                        case 4:
                            return this.B5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierC:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.C1;
                        case 1:
                            return this.C2;
                        case 2:
                            return this.C3;
                        case 3:
                            return this.C4;
                        case 4:
                            return this.C5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierD:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.D1;
                        case 1:
                            return this.D2;
                        case 2:
                            return this.D3;
                        case 3:
                            return this.D4;
                        case 4:
                            return this.D5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                case eRundownTier.TierE:
                    switch (expeditionIndex)
                    {
                        case 0:
                            return this.E1;
                        case 1:
                            return this.E2;
                        case 2:
                            return this.E3;
                        case 3:
                            return this.E4;
                        case 4:
                            return this.E5;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(expeditionIndex));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(tier));
            }
        }

        private static CompletionData cache;

        public void Save()
        {
            var filePath = Path.Combine(ConfigManager.CustomPath, "Completions.data");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this));
        }

        public static CompletionData LoadFromCache()
        {
            if (cache == null)
            {
                if (ConfigManager.HasCustomContent)
                {
                    var filePath = Path.Combine(ConfigManager.CustomPath, "Completions.data");
                    CompletionData data;

                    if (!File.Exists(filePath))
                    {
                        data = new CompletionData();
                        File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
                    }
                    else
                    {
                        data = JsonConvert.DeserializeObject<CompletionData>(File.ReadAllText(filePath));
                    }

                    cache = data;
                }
            }

            return cache;
        }
    }
}