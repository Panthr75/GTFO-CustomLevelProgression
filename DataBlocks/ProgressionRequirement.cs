using Newtonsoft.Json;

namespace CustomLevelProgression.DataBlocks
{
    public class ProgressionRequirement
    {
        public eRundownTier Tier { get; set; }
        public int ExpeditionIndex { get; set; }
        public int Value { get; set; }
        public string Check { get; set; }
        public ProgressionCheckType CheckType { get; set; }

        [JsonIgnore]
        private bool m_isSetup;

        public void Setup()
        {
            if (!this.m_isSetup)
            {
                if (Check != null)
                {
                    if (Check == "==")
                        CheckType = ProgressionCheckType.Equals;
                    else if (Check == "!=")
                        CheckType = ProgressionCheckType.NotEquals;
                    else if (Check == ">")
                        CheckType = ProgressionCheckType.GreaterThan;
                    else if (Check == ">=")
                        CheckType = ProgressionCheckType.GreaterThanOrEqual;
                    else if (Check == "<")
                        CheckType = ProgressionCheckType.LessThan;
                    else if (Check == "<=")
                        CheckType = ProgressionCheckType.LessThanOrEqual;
                    else
                    {
                        CheckType = ProgressionCheckType.NotEquals;
                    }
                }
                else
                {
                    switch (CheckType)
                    {
                        case ProgressionCheckType.Equals:
                            Check = "==";
                            break;
                        case ProgressionCheckType.NotEquals:
                            Check = "!=";
                            break;
                        case ProgressionCheckType.GreaterThan:
                            Check = ">";
                            break;
                        case ProgressionCheckType.GreaterThanOrEqual:
                            Check = ">=";
                            break;
                        case ProgressionCheckType.LessThan:
                            Check = "<";
                            break;
                        case ProgressionCheckType.LessThanOrEqual:
                            Check = "<=";
                            break;
                        default:
                            Check = "!=";
                            break;
                    }
                }

                this.m_isSetup = true;
            }
        }
    }
}