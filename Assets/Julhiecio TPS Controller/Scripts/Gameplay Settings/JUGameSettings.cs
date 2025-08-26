using UnityEngine;
using UnityEngine.Events;

namespace JUTPS.GameSettings
{
    /// <summary>
    /// The game settings system, apply game configurations.
    /// </summary>
    public class JUGameSettings : MonoBehaviour
    {
        private const string GRAPHICS_RENDER_SCALE_KEY = "SETTINGS_GRAPHICS_RENDER_SCALE";
        private const string GRAPHICS_QUALITY_KEY = "SETTINGS_GRAPHICS_QUALITY";
        private const string CONTROLS_CAMERA_INVERT_VERTICAL_KEY = "SETTINGS_CONTROLS_CAMERA_INVERT_VERTICAL";
        private const string CONTROLS_CAMERA_INVERT_HORIZONTAL_KEY = "SETTINGS_CONTROLS_CAMERA_INVERT_HORIZONTAL";
        private const string CONTROLS_CAMERA_SENSITIVE_KEY = "SETTINGS_CONTROLS_CAMERA_SENSITIVE";
        private const string AUDIO_VOLUME_KEY = "SETTINGS_AUDIO_VOLUME";

#if UNITY_ANDROID || UNITY_IOS
        private static bool IsMobile => true;
#else
        private static bool IsMobile => false;
#endif

        /// <summary>
        /// Called when the settings is applied.
        /// </summary>
        public static UnityAction OnApplySettings;

        /// <summary>
        /// The render scale resolution multiplier, a value between 0.1 and 1 based on the window size.
        /// </summary>
        public static float RenderScale
        {
            get
            {
                return PlayerPrefs.GetFloat(GRAPHICS_RENDER_SCALE_KEY, IsMobile ? 0.75f : 1f);
            }
            set
            {
                value = Mathf.Clamp(value, 0.1f, 1f);
                PlayerPrefs.SetFloat(GRAPHICS_RENDER_SCALE_KEY, value);

                Resolution biggestResolution = Screen.resolutions[Screen.resolutions.Length - 1];
                Resolution currentResolution = Screen.currentResolution;
                Resolution targetResolution = new Resolution()
                {
                    height = (int)(biggestResolution.height * value),
                    width = (int)(biggestResolution.width * value),

#if UNITY_2022_3_OR_NEWER
                    refreshRateRatio = currentResolution.refreshRateRatio
#else
                    refreshRate = currentResolution.refreshRate
#endif

                };

                //On mobile devices the width and height are inverted
                if (!IsMobile)
                {
#if UNITY_2022_3_OR_NEWER
                    Screen.SetResolution(targetResolution.width, targetResolution.height, Screen.fullScreenMode, targetResolution.refreshRateRatio);
#else
                    Screen.SetResolution(targetResolution.width, targetResolution.height, Screen.fullScreen, targetResolution.refreshRate);
#endif
                }
                else
                {
#if UNITY_2022_3_OR_NEWER
                    Screen.SetResolution(targetResolution.height, targetResolution.width, Screen.fullScreenMode, targetResolution.refreshRateRatio);
#else
                    Screen.SetResolution(targetResolution.height, targetResolution.width, Screen.fullScreen, targetResolution.refreshRate);
#endif
                }

                OnApplySettings?.Invoke();
            }
        }

        /// <summary>
        /// The current graphics settings.
        /// </summary>
        public static int GraphicsQuality
        {
            get
            {
                return PlayerPrefs.GetInt(GRAPHICS_QUALITY_KEY, QualitySettings.GetQualityLevel());
            }
            set
            {
                PlayerPrefs.SetInt(GRAPHICS_QUALITY_KEY, value);
                QualitySettings.SetQualityLevel(value);

                OnApplySettings?.Invoke();
            }
        }

        /// <summary>
        /// Invert vertical camera orientation.
        /// </summary>
        public static bool CameraInvertVertical
        {
            get
            {
                return PlayerPrefs.GetInt(CONTROLS_CAMERA_INVERT_VERTICAL_KEY, 0) == 1 ? true : false;
            }
            set
            {
                PlayerPrefs.SetInt(CONTROLS_CAMERA_INVERT_VERTICAL_KEY, value ? 1 : 0);
                OnApplySettings?.Invoke();
            }
        }

        /// <summary>
        /// Invert horizontal camera orientation.
        /// </summary>
        public static bool CameraInvertHorizontal
        {
            get
            {
                return PlayerPrefs.GetInt(CONTROLS_CAMERA_INVERT_HORIZONTAL_KEY, 0) == 1 ? true : false;
            }
            set
            {
                PlayerPrefs.SetInt(CONTROLS_CAMERA_INVERT_HORIZONTAL_KEY, value ? 1 : 0);
                OnApplySettings?.Invoke();
            }
        }

        /// <summary>
        /// The camera rotation sensibility with user inputs.
        /// </summary>
        public static float CameraSensibility
        {
            get
            {
                return PlayerPrefs.GetFloat(CONTROLS_CAMERA_SENSITIVE_KEY, 1f);
            }
            set
            {
                if (value == CameraSensibility)
                    return;

                value = Mathf.Min(value, 10);
                PlayerPrefs.SetFloat(CONTROLS_CAMERA_SENSITIVE_KEY, value);
                OnApplySettings?.Invoke();
            }
        }

        /// <summary>
        /// The game audio volume.
        /// </summary>
        public static float AudioVolume
        {
            get
            {
                return PlayerPrefs.GetFloat(AUDIO_VOLUME_KEY, 1f);
            }
            set
            {
                if (value == AudioVolume)
                    return;

                value = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat(AUDIO_VOLUME_KEY, value);

                OnApplySettings?.Invoke();
            }
        }

        private void Awake()
        {
            ApplySettings();
        }

        /// <summary>
        /// Apply the game settings.
        /// </summary>
        public static void ApplySettings()
        {
            RenderScale = RenderScale;
            AudioVolume = AudioVolume;
            GraphicsQuality = GraphicsQuality;
            CameraInvertHorizontal = CameraInvertHorizontal;
            CameraInvertVertical = CameraInvertVertical;
            OnApplySettings?.Invoke();
        }

        /// <summary>
        /// Reset Game Settings.
        /// </summary>
        [ContextMenu("Reset Game Settings", false, 100)]
        public void ResetSettings()
        {
            PlayerPrefs.DeleteKey(GRAPHICS_RENDER_SCALE_KEY);
            PlayerPrefs.DeleteKey(GRAPHICS_QUALITY_KEY);
            PlayerPrefs.DeleteKey(CONTROLS_CAMERA_INVERT_VERTICAL_KEY);
            PlayerPrefs.DeleteKey(CONTROLS_CAMERA_INVERT_HORIZONTAL_KEY);
            PlayerPrefs.DeleteKey(CONTROLS_CAMERA_SENSITIVE_KEY);
            PlayerPrefs.DeleteKey(AUDIO_VOLUME_KEY);

            if (Application.isPlaying)
                ApplySettings();
        }
    }
}