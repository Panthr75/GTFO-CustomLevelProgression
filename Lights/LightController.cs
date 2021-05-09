using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using GameData;
using LevelGeneration;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLevelProgression.Lights
{
    public class LightController : MonoBehaviour
    {
        public LightController(System.IntPtr value) : base(value)
        { }

        internal static List<LightController> controllers = new List<LightController>();

        private LG_Light light;
        private LG_LightAnimator animator;
        private Color currentColor;
        private float currentIntensity;
        private LightState currentState;
        private bool setup = false;

        private LG_Area area;
        private LG_Zone zone;
        private LG_Layer layer;

        public LG_Area Area => area;
        public LG_Zone Zone => zone;
        public LG_Layer Layer => layer;

        public bool HasArea => area != null;

        public string AreaName => area.m_navInfo.Suffix;
        public eLocalZoneIndex ZoneIndex => zone.LocalIndex;
        public LG_LayerType LayerType => layer.m_type;

        private void Start()
        {
            if (!setup)
            {
                this.light = GetComponent<LG_Light>();
                this.animator = GetComponent<LG_LightAnimator>() ?? this.light?.GetC_Light()?.gameObject?.GetComponent<LG_LightAnimator>();
                this.currentColor = this.light.m_color;
                this.currentIntensity = this.light.GetIntensity();

                this.area = this.light?.GetC_Light()?.m_sourceNode?.CourseNode?.m_area;
                this.zone = this.area?.m_zone;
                this.layer = this.zone?.Layer;

                controllers.Add(this);
                this.setup = true;
            }
        }

        private void Update()
        {
            this.currentState?.Tick();
        }

        private void OnDestroy()
        {
            controllers.Remove(this);
        }

        public float GetIntensity() => this.currentIntensity;
        public Color GetCurrentColor() => this.currentColor;

        public void SetAnimatorEnabled(bool value)
        {
            if (this.animator != null)
                this.animator.enabled = value;
        }

        public void SetCurrentColor(Color color)
        {
            this.currentColor = color;
            this.light.ChangeColor(color);
        }

        public void SetCurrentIntensity(float intensity)
        {
            this.currentIntensity = intensity;
            this.light.ChangeIntensity(intensity);
        }

        public void ChangeState(LightStateDataBlock data, float extraTime = 0f)
        {
            if (this.currentState != null && !this.currentState.transitioned)
            {
                this.SetCurrentIntensity(this.currentState.m_intensity);
                this.SetCurrentColor(this.currentState.m_color);
            }

            if (data != null)
            {
                this.currentState = new LightState(data, this);
                this.currentState.Init(extraTime);
            }
            else
            {
                this.currentState = null;
            }
        }
    }
}
