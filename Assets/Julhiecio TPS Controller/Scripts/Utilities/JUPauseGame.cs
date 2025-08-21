using UnityEngine;
using UnityEngine.Events;
using JUTPS.InputEvents;
using JUTPSEditor.JUHeader;
namespace JUTPS
{
    [AddComponentMenu(Constants.ComponentMenuNames.PauseGame)]
    public class JUPauseGame : MonoBehaviour
    {
        public static JUPauseGame Instance;
        public static bool IsPause;
        
        public MultipleActionEvent PauseInputs;

        [JUHeader("On Pause Events")]
        public UnityEvent OnPause;
        public UnityEvent OnUnpause;
        private FX.JUSlowmotion SlowmotionInstance;

        private void Start()
        {
            Instance = this;
            SlowmotionInstance = FindFirstObjectByType<FX.JUSlowmotion>();
            PauseInputs.OnButtonsDown.AddListener(Pause);
        }

        private void OnEnable() =>
            PauseInputs.Enable();

        private void OnDisable() =>
            PauseInputs.Disable();
        
        public static void Pause()
        {
            IsPause = !IsPause;
            Time.timeScale = IsPause ? 0 : 1;

            if (IsPause) 
                Instance.OnPause.Invoke();
            else 
                Instance.OnUnpause.Invoke();

            Instance.SlowmotionInstance.EnableSlowmotion = !IsPause;
        }
    }
}