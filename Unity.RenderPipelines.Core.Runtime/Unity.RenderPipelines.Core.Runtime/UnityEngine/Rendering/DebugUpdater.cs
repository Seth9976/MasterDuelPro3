using System;
using System.Collections;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C7 RID: 199
	internal class DebugUpdater : MonoBehaviour
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x00005704 File Offset: 0x00003904
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void RuntimeInit()
		{
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000FBEF File Offset: 0x0000DDEF
		internal static void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				DebugUpdater.EnableRuntime();
				return;
			}
			DebugUpdater.DisableRuntime();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000FC00 File Offset: 0x0000DE00
		private static void EnableRuntime()
		{
			if (DebugUpdater.s_Instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "[Debug Updater]";
			DebugUpdater.s_Instance = gameObject.AddComponent<DebugUpdater>();
			DebugUpdater.s_Instance.m_Orientation = Screen.orientation;
			Object.DontDestroyOnLoad(gameObject);
			DebugManager.instance.EnableInputActions();
			EnhancedTouchSupport.Enable();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000FC59 File Offset: 0x0000DE59
		private static void DisableRuntime()
		{
			DebugManager instance = DebugManager.instance;
			instance.displayRuntimeUI = false;
			instance.displayPersistentRuntimeUI = false;
			if (DebugUpdater.s_Instance != null)
			{
				CoreUtils.Destroy(DebugUpdater.s_Instance.gameObject);
				DebugUpdater.s_Instance = null;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000FC8F File Offset: 0x0000DE8F
		internal static void HandleInternalEventSystemComponents(bool uiEnabled)
		{
			if (DebugUpdater.s_Instance == null)
			{
				return;
			}
			if (uiEnabled)
			{
				DebugUpdater.s_Instance.EnsureExactlyOneEventSystem();
				return;
			}
			DebugUpdater.s_Instance.DestroyDebugEventSystem();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		private void EnsureExactlyOneEventSystem()
		{
			EventSystem[] eventSystems = Object.FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
			EventSystem debugEventSystem = base.GetComponent<EventSystem>();
			if (eventSystems.Length > 1 && debugEventSystem != null)
			{
				Debug.Log("More than one EventSystem detected in scene. Destroying EventSystem owned by DebugUpdater.");
				this.DestroyDebugEventSystem();
				return;
			}
			if (eventSystems.Length == 0)
			{
				Debug.Log("No EventSystem available. Creating a new EventSystem to enable Rendering Debugger runtime UI.");
				this.CreateDebugEventSystem();
				return;
			}
			base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.CheckInputModuleExists)));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000FD21 File Offset: 0x0000DF21
		private IEnumerator DoAfterInputModuleUpdated(Action action)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			action();
			yield break;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000FD30 File Offset: 0x0000DF30
		private void CheckInputModuleExists()
		{
			if (EventSystem.current != null && EventSystem.current.currentInputModule == null)
			{
				Debug.LogWarning("Found a game object with EventSystem component but no corresponding BaseInputModule component - Debug UI input might not work correctly.");
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000FD5C File Offset: 0x0000DF5C
		private void AssignDefaultActions()
		{
			if (EventSystem.current != null)
			{
				InputSystemUIInputModule inputSystemModule = EventSystem.current.currentInputModule as InputSystemUIInputModule;
				if (inputSystemModule != null)
				{
					MethodInfo assignDefaultActionsMethod = inputSystemModule.GetType().GetMethod("AssignDefaultActions");
					if (assignDefaultActionsMethod != null)
					{
						assignDefaultActionsMethod.Invoke(inputSystemModule, null);
					}
				}
			}
			this.CheckInputModuleExists();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		private void CreateDebugEventSystem()
		{
			base.gameObject.AddComponent<EventSystem>();
			base.gameObject.AddComponent<InputSystemUIInputModule>();
			base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.AssignDefaultActions)));
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		private void DestroyDebugEventSystem()
		{
			Object component = base.GetComponent<EventSystem>();
			InputSystemUIInputModule inputModule = base.GetComponent<InputSystemUIInputModule>();
			if (inputModule)
			{
				CoreUtils.Destroy(inputModule);
				base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.AssignDefaultActions)));
			}
			CoreUtils.Destroy(component);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000FE30 File Offset: 0x0000E030
		private void Update()
		{
			DebugManager debugManager = DebugManager.instance;
			if (this.m_RuntimeUiWasVisibleLastFrame != debugManager.displayRuntimeUI)
			{
				DebugUpdater.HandleInternalEventSystemComponents(debugManager.displayRuntimeUI);
			}
			debugManager.UpdateActions();
			if (debugManager.GetAction(DebugAction.EnableDebugMenu) != 0f || debugManager.GetActionToggleDebugMenuWithTouch())
			{
				debugManager.displayRuntimeUI = !debugManager.displayRuntimeUI;
			}
			if (debugManager.displayRuntimeUI)
			{
				if (debugManager.GetAction(DebugAction.ResetAll) != 0f)
				{
					debugManager.Reset();
				}
				if (debugManager.GetActionReleaseScrollTarget())
				{
					debugManager.SetScrollTarget(null);
				}
			}
			if (this.m_Orientation != Screen.orientation)
			{
				base.StartCoroutine(DebugUpdater.RefreshRuntimeUINextFrame());
				this.m_Orientation = Screen.orientation;
			}
			this.m_RuntimeUiWasVisibleLastFrame = debugManager.displayRuntimeUI;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000FEE2 File Offset: 0x0000E0E2
		private static IEnumerator RefreshRuntimeUINextFrame()
		{
			yield return null;
			DebugManager.instance.ReDrawOnScreenDebug();
			yield break;
		}

		// Token: 0x0400026D RID: 621
		private static DebugUpdater s_Instance;

		// Token: 0x0400026E RID: 622
		private ScreenOrientation m_Orientation;

		// Token: 0x0400026F RID: 623
		private bool m_RuntimeUiWasVisibleLastFrame;
	}
}
