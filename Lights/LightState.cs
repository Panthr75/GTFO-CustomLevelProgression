using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using UnityEngine;

namespace CustomLevelProgression.Lights
{
    public class LightState
    {
        public LightStateDataBlock m_nextState;

        public float m_nextStateTime;
        public float m_transitionTime;
        public float m_activationDelay;

        public Color initialColor;
        public float initialIntensity;

        public Color m_color;
        public float m_intensity;

        public bool m_lightAnimatorEnabled;

        public LightController Controller { get; set; }

        public float currentActivationDelay;
        public bool activated;
        public float currentTransitionTime;
        public bool transitioned;
        public float currentNextStateDelay;
        public bool moveToNextState;

        public LightState(LightStateDataBlock data, LightController controller)
        {
            if (data == null)
                throw new System.ArgumentNullException(nameof(data));

            this.Controller = controller;

            this.m_nextState = LightStateDataBlock.GetBlock(data.NextStateID);

            this.m_transitionTime = Random.Range(data.MinTransitionTime, data.MaxTransitionTime);
            this.m_nextStateTime = Random.Range(data.MinNextStateTime, data.MaxNextStateTime);
            this.m_activationDelay = Random.Range(data.MinActivationDelay, data.MaxActivationDelay);

            this.m_color = data.ChangeColor ? data.Color : controller.GetCurrentColor();

            if (data.ChangeIntensity)
            {
                this.m_intensity = Random.Range(data.MinIntensity, data.MaxIntensity);

                if (data.RelativeIntensity)
                    this.m_intensity *= controller.GetIntensity();
            }
            else
            {
                this.m_intensity = controller.GetIntensity();
            }

            this.m_lightAnimatorEnabled = data.LightAnimatorEnabled;
        }

        public virtual void Init(float extraTime = 0f)
        {
            this.currentActivationDelay = extraTime;
            this.currentNextStateDelay = 0;
            this.currentTransitionTime = 0;
            this.activated = false;
            this.transitioned = false;
            this.moveToNextState = false;
        }

        public virtual void Tick()
        {
            float delta = Time.deltaTime;

            if (!this.activated)
            {
                this.currentActivationDelay += delta;
                if (this.currentActivationDelay >= this.m_activationDelay)
                {
                    this.currentTransitionTime += this.currentActivationDelay - this.m_activationDelay;
                    this.currentActivationDelay = this.m_activationDelay;
                    this.activated = true;

                    Controller.SetAnimatorEnabled(this.m_lightAnimatorEnabled);
                    this.initialIntensity = Controller.GetIntensity();
                    this.initialColor = Controller.GetCurrentColor();
                }
            }

            if (this.activated && !this.transitioned)
            {
                this.currentTransitionTime += delta;
                if (this.currentTransitionTime >= this.m_transitionTime)
                {
                    this.currentTransitionTime += this.currentTransitionTime - this.m_transitionTime;
                    this.currentTransitionTime = this.m_transitionTime;
                    this.transitioned = true;
                }

                float progress = this.m_transitionTime == 0 ? 1 : (this.currentTransitionTime / this.m_transitionTime);

                this.Controller.SetCurrentColor(Color.Lerp(this.initialColor, this.m_color, progress));
                this.Controller.SetCurrentIntensity(Mathf.Lerp(this.initialIntensity, this.m_intensity, progress));
            }

            if (this.activated && this.transitioned && !this.moveToNextState)
            {
                this.currentNextStateDelay += delta;
                if (this.currentNextStateDelay >= this.m_nextStateTime)
                {
                    float extraTime = this.currentNextStateDelay - this.m_nextStateTime;
                    this.currentNextStateDelay = this.m_nextStateTime;
                    this.moveToNextState = true;

                    this.Controller.ChangeState(this.m_nextState, extraTime);
                }
            }
        }
    }
}