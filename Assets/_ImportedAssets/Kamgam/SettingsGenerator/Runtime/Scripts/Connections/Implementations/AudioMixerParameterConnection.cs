using UnityEngine;
using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator
{
    public class AudioMixerParameterConnection : Connection<float>
    {
        /// <summary>
        /// The mixer that should be controlled by this connection.
        /// </summary>
        public AudioMixer Mixer;

        /// <summary>
        /// The name of the exposed parameter.
        /// </summary>
        public string ExposedParameterName;

        public AudioMixerParameterConnection(AudioMixer mixer, string exposedParameterName)
        {
            Mixer = mixer;
            ExposedParameterName = exposedParameterName;
        }

        public override float Get()
        {
            float value;
            if (Mixer.GetFloat(ExposedParameterName, out value))
            {
                return value;
            }
            else
            {
                return 0f;
            }
        }

        public override void Set(float value)
        {
            Mixer.SetFloat(ExposedParameterName, value);
        }
    }
}
