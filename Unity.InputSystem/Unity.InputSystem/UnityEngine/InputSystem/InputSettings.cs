using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000C9 RID: 201
	public class InputSettings : ScriptableObject
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00039C5B File Offset: 0x00037E5B
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00039C63 File Offset: 0x00037E63
		public InputSettings.UpdateMode updateMode
		{
			get
			{
				return this.m_UpdateMode;
			}
			set
			{
				if (this.m_UpdateMode == value)
				{
					return;
				}
				this.m_UpdateMode = value;
				this.OnChange();
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00039C7C File Offset: 0x00037E7C
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x00039C84 File Offset: 0x00037E84
		public InputSettings.ScrollDeltaBehavior scrollDeltaBehavior
		{
			get
			{
				return this.m_ScrollDeltaBehavior;
			}
			set
			{
				if (this.m_ScrollDeltaBehavior == value)
				{
					return;
				}
				this.m_ScrollDeltaBehavior = value;
				this.OnChange();
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00039C9D File Offset: 0x00037E9D
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x00039CA5 File Offset: 0x00037EA5
		public bool compensateForScreenOrientation
		{
			get
			{
				return this.m_CompensateForScreenOrientation;
			}
			set
			{
				if (this.m_CompensateForScreenOrientation == value)
				{
					return;
				}
				this.m_CompensateForScreenOrientation = value;
				this.OnChange();
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001751C File Offset: 0x0001571C
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x000049FE File Offset: 0x00002BFE
		[Obsolete("filterNoiseOnCurrent is deprecated, filtering of noise is always enabled now.", false)]
		public bool filterNoiseOnCurrent
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00039CBE File Offset: 0x00037EBE
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00039CC6 File Offset: 0x00037EC6
		public float defaultDeadzoneMin
		{
			get
			{
				return this.m_DefaultDeadzoneMin;
			}
			set
			{
				if (this.m_DefaultDeadzoneMin == value)
				{
					return;
				}
				this.m_DefaultDeadzoneMin = value;
				this.OnChange();
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00039CDF File Offset: 0x00037EDF
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x00039CE7 File Offset: 0x00037EE7
		public float defaultDeadzoneMax
		{
			get
			{
				return this.m_DefaultDeadzoneMax;
			}
			set
			{
				if (this.m_DefaultDeadzoneMax == value)
				{
					return;
				}
				this.m_DefaultDeadzoneMax = value;
				this.OnChange();
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00039D00 File Offset: 0x00037F00
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00039D08 File Offset: 0x00037F08
		public float defaultButtonPressPoint
		{
			get
			{
				return this.m_DefaultButtonPressPoint;
			}
			set
			{
				if (this.m_DefaultButtonPressPoint == value)
				{
					return;
				}
				this.m_DefaultButtonPressPoint = Mathf.Clamp(value, 0.0001f, float.MaxValue);
				this.OnChange();
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00039D30 File Offset: 0x00037F30
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00039D38 File Offset: 0x00037F38
		public float buttonReleaseThreshold
		{
			get
			{
				return this.m_ButtonReleaseThreshold;
			}
			set
			{
				if (this.m_ButtonReleaseThreshold == value)
				{
					return;
				}
				this.m_ButtonReleaseThreshold = value;
				this.OnChange();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00039D51 File Offset: 0x00037F51
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00039D59 File Offset: 0x00037F59
		public float defaultTapTime
		{
			get
			{
				return this.m_DefaultTapTime;
			}
			set
			{
				if (this.m_DefaultTapTime == value)
				{
					return;
				}
				this.m_DefaultTapTime = value;
				this.OnChange();
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00039D72 File Offset: 0x00037F72
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00039D7A File Offset: 0x00037F7A
		public float defaultSlowTapTime
		{
			get
			{
				return this.m_DefaultSlowTapTime;
			}
			set
			{
				if (this.m_DefaultSlowTapTime == value)
				{
					return;
				}
				this.m_DefaultSlowTapTime = value;
				this.OnChange();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00039D93 File Offset: 0x00037F93
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00039D9B File Offset: 0x00037F9B
		public float defaultHoldTime
		{
			get
			{
				return this.m_DefaultHoldTime;
			}
			set
			{
				if (this.m_DefaultHoldTime == value)
				{
					return;
				}
				this.m_DefaultHoldTime = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00039DB4 File Offset: 0x00037FB4
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00039DBC File Offset: 0x00037FBC
		public float tapRadius
		{
			get
			{
				return this.m_TapRadius;
			}
			set
			{
				if (this.m_TapRadius == value)
				{
					return;
				}
				this.m_TapRadius = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00039DD5 File Offset: 0x00037FD5
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x00039DDD File Offset: 0x00037FDD
		public float multiTapDelayTime
		{
			get
			{
				return this.m_MultiTapDelayTime;
			}
			set
			{
				if (this.m_MultiTapDelayTime == value)
				{
					return;
				}
				this.m_MultiTapDelayTime = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00039DF6 File Offset: 0x00037FF6
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x00039DFE File Offset: 0x00037FFE
		public InputSettings.BackgroundBehavior backgroundBehavior
		{
			get
			{
				return this.m_BackgroundBehavior;
			}
			set
			{
				if (this.m_BackgroundBehavior == value)
				{
					return;
				}
				this.m_BackgroundBehavior = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00039E17 File Offset: 0x00038017
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x00039E1F File Offset: 0x0003801F
		public InputSettings.EditorInputBehaviorInPlayMode editorInputBehaviorInPlayMode
		{
			get
			{
				return this.m_EditorInputBehaviorInPlayMode;
			}
			set
			{
				if (this.m_EditorInputBehaviorInPlayMode == value)
				{
					return;
				}
				this.m_EditorInputBehaviorInPlayMode = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00039E38 File Offset: 0x00038038
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00039E40 File Offset: 0x00038040
		public InputSettings.InputActionPropertyDrawerMode inputActionPropertyDrawerMode
		{
			get
			{
				return this.m_InputActionPropertyDrawerMode;
			}
			set
			{
				if (this.m_InputActionPropertyDrawerMode == value)
				{
					return;
				}
				this.m_InputActionPropertyDrawerMode = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00039E59 File Offset: 0x00038059
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00039E61 File Offset: 0x00038061
		public int maxEventBytesPerUpdate
		{
			get
			{
				return this.m_MaxEventBytesPerUpdate;
			}
			set
			{
				if (this.m_MaxEventBytesPerUpdate == value)
				{
					return;
				}
				this.m_MaxEventBytesPerUpdate = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00039E7A File Offset: 0x0003807A
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x00039E82 File Offset: 0x00038082
		public int maxQueuedEventsPerUpdate
		{
			get
			{
				return this.m_MaxQueuedEventsPerUpdate;
			}
			set
			{
				if (this.m_MaxQueuedEventsPerUpdate == value)
				{
					return;
				}
				this.m_MaxQueuedEventsPerUpdate = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00039E9B File Offset: 0x0003809B
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00039EA8 File Offset: 0x000380A8
		public ReadOnlyArray<string> supportedDevices
		{
			get
			{
				return new ReadOnlyArray<string>(this.m_SupportedDevices);
			}
			set
			{
				if (this.supportedDevices.Count == value.Count)
				{
					bool hasChanged = false;
					for (int i = 0; i < this.supportedDevices.Count; i++)
					{
						if (this.m_SupportedDevices[i] != value[i])
						{
							hasChanged = true;
							break;
						}
					}
					if (!hasChanged)
					{
						return;
					}
				}
				this.m_SupportedDevices = value.ToArray();
				this.OnChange();
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00039F19 File Offset: 0x00038119
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00039F21 File Offset: 0x00038121
		public bool disableRedundantEventsMerging
		{
			get
			{
				return this.m_DisableRedundantEventsMerging;
			}
			set
			{
				if (this.m_DisableRedundantEventsMerging == value)
				{
					return;
				}
				this.m_DisableRedundantEventsMerging = value;
				this.OnChange();
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00039F3A File Offset: 0x0003813A
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00039F42 File Offset: 0x00038142
		public bool shortcutKeysConsumeInput
		{
			get
			{
				return this.m_ShortcutKeysConsumeInputs;
			}
			set
			{
				if (this.m_ShortcutKeysConsumeInputs == value)
				{
					return;
				}
				this.m_ShortcutKeysConsumeInputs = value;
				this.OnChange();
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00039F5C File Offset: 0x0003815C
		public void SetInternalFeatureFlag(string featureName, bool enabled)
		{
			if (string.IsNullOrEmpty(featureName))
			{
				throw new ArgumentNullException("featureName");
			}
			if (this.m_FeatureFlags == null)
			{
				this.m_FeatureFlags = new HashSet<string>();
			}
			if (enabled)
			{
				this.m_FeatureFlags.Add(featureName.ToUpperInvariant());
			}
			else
			{
				this.m_FeatureFlags.Remove(featureName.ToUpperInvariant());
			}
			this.OnChange();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00039FBE File Offset: 0x000381BE
		internal bool IsFeatureEnabled(string featureName)
		{
			return this.m_FeatureFlags != null && this.m_FeatureFlags.Contains(featureName.ToUpperInvariant());
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00039FDB File Offset: 0x000381DB
		internal void OnChange()
		{
			if (InputSystem.settings == this)
			{
				InputSystem.s_Manager.ApplySettings();
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00039FF4 File Offset: 0x000381F4
		private static bool CompareFloats(float a, float b)
		{
			return a - b <= float.Epsilon;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0003A004 File Offset: 0x00038204
		private static bool CompareSets<T>(ReadOnlyArray<T> a, ReadOnlyArray<T> b)
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				bool existsInB = false;
				for (int j = 0; j < b.Count; j++)
				{
					T t = a[i];
					if (t.Equals(b[j]))
					{
						existsInB = true;
						break;
					}
				}
				if (!existsInB)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0003A082 File Offset: 0x00038282
		private static bool CompareFeatureFlag(InputSettings a, InputSettings b, string featureName)
		{
			return a.IsFeatureEnabled(featureName) == b.IsFeatureEnabled(featureName);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003A094 File Offset: 0x00038294
		internal static bool AreEqual(InputSettings a, InputSettings b)
		{
			if (a == null)
			{
				return b == null;
			}
			return b != null && (a == b || (a.updateMode == b.updateMode && a.compensateForScreenOrientation == b.compensateForScreenOrientation && InputSettings.CompareFloats(a.defaultDeadzoneMin, b.defaultDeadzoneMin) && InputSettings.CompareFloats(a.defaultDeadzoneMax, b.defaultDeadzoneMax) && InputSettings.CompareFloats(a.defaultButtonPressPoint, b.defaultButtonPressPoint) && InputSettings.CompareFloats(a.buttonReleaseThreshold, b.buttonReleaseThreshold) && InputSettings.CompareFloats(a.defaultTapTime, b.defaultTapTime) && InputSettings.CompareFloats(a.defaultSlowTapTime, b.defaultSlowTapTime) && InputSettings.CompareFloats(a.defaultHoldTime, b.defaultHoldTime) && InputSettings.CompareFloats(a.tapRadius, b.tapRadius) && InputSettings.CompareFloats(a.multiTapDelayTime, b.multiTapDelayTime) && a.backgroundBehavior == b.backgroundBehavior && a.editorInputBehaviorInPlayMode == b.editorInputBehaviorInPlayMode && a.inputActionPropertyDrawerMode == b.inputActionPropertyDrawerMode && a.maxEventBytesPerUpdate == b.maxEventBytesPerUpdate && a.maxQueuedEventsPerUpdate == b.maxQueuedEventsPerUpdate && InputSettings.CompareSets<string>(a.supportedDevices, b.supportedDevices) && a.disableRedundantEventsMerging == b.disableRedundantEventsMerging && a.shortcutKeysConsumeInput == b.shortcutKeysConsumeInput && InputSettings.CompareFeatureFlag(a, b, "USE_OPTIMIZED_CONTROLS") && InputSettings.CompareFeatureFlag(a, b, "USE_READ_VALUE_CACHING") && InputSettings.CompareFeatureFlag(a, b, "PARANOID_READ_VALUE_CACHING_CHECKS") && InputSettings.CompareFeatureFlag(a, b, "DISABLE_UNITY_REMOTE_SUPPORT") && InputSettings.CompareFeatureFlag(a, b, "RUN_PLAYER_UPDATES_IN_EDIT_MODE") && InputSettings.CompareFeatureFlag(a, b, "USE_IMGUI_EDITOR_FOR_ASSETS")));
		}

		// Token: 0x040004AD RID: 1197
		[Tooltip("Determine which type of devices are used by the application. By default, this is empty meaning that all devices recognized by Unity will be used. Restricting the set of supported devices will make only those devices appear in the input system.")]
		[SerializeField]
		private string[] m_SupportedDevices;

		// Token: 0x040004AE RID: 1198
		[Tooltip("Determine when Unity processes events. By default, accumulated input events are flushed out before each fixed update and before each dynamic update. This setting can be used to restrict event processing to only where the application needs it.")]
		[SerializeField]
		private InputSettings.UpdateMode m_UpdateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

		// Token: 0x040004AF RID: 1199
		[SerializeField]
		private InputSettings.ScrollDeltaBehavior m_ScrollDeltaBehavior;

		// Token: 0x040004B0 RID: 1200
		[SerializeField]
		private int m_MaxEventBytesPerUpdate = 5242880;

		// Token: 0x040004B1 RID: 1201
		[SerializeField]
		private int m_MaxQueuedEventsPerUpdate = 1000;

		// Token: 0x040004B2 RID: 1202
		[SerializeField]
		private bool m_CompensateForScreenOrientation = true;

		// Token: 0x040004B3 RID: 1203
		[SerializeField]
		private InputSettings.BackgroundBehavior m_BackgroundBehavior;

		// Token: 0x040004B4 RID: 1204
		[SerializeField]
		private InputSettings.EditorInputBehaviorInPlayMode m_EditorInputBehaviorInPlayMode;

		// Token: 0x040004B5 RID: 1205
		[SerializeField]
		private InputSettings.InputActionPropertyDrawerMode m_InputActionPropertyDrawerMode;

		// Token: 0x040004B6 RID: 1206
		[SerializeField]
		private float m_DefaultDeadzoneMin = 0.125f;

		// Token: 0x040004B7 RID: 1207
		[SerializeField]
		private float m_DefaultDeadzoneMax = 0.925f;

		// Token: 0x040004B8 RID: 1208
		[Min(0.0001f)]
		[SerializeField]
		private float m_DefaultButtonPressPoint = 0.5f;

		// Token: 0x040004B9 RID: 1209
		[SerializeField]
		private float m_ButtonReleaseThreshold = 0.75f;

		// Token: 0x040004BA RID: 1210
		[SerializeField]
		private float m_DefaultTapTime = 0.2f;

		// Token: 0x040004BB RID: 1211
		[SerializeField]
		private float m_DefaultSlowTapTime = 0.5f;

		// Token: 0x040004BC RID: 1212
		[SerializeField]
		private float m_DefaultHoldTime = 0.4f;

		// Token: 0x040004BD RID: 1213
		[SerializeField]
		private float m_TapRadius = 5f;

		// Token: 0x040004BE RID: 1214
		[SerializeField]
		private float m_MultiTapDelayTime = 0.75f;

		// Token: 0x040004BF RID: 1215
		[SerializeField]
		private bool m_DisableRedundantEventsMerging;

		// Token: 0x040004C0 RID: 1216
		[SerializeField]
		private bool m_ShortcutKeysConsumeInputs;

		// Token: 0x040004C1 RID: 1217
		[NonSerialized]
		internal HashSet<string> m_FeatureFlags;

		// Token: 0x040004C2 RID: 1218
		internal const int s_OldUnsupportedFixedAndDynamicUpdateSetting = 0;

		// Token: 0x020000CA RID: 202
		public enum UpdateMode
		{
			// Token: 0x040004C4 RID: 1220
			ProcessEventsInDynamicUpdate = 1,
			// Token: 0x040004C5 RID: 1221
			ProcessEventsInFixedUpdate,
			// Token: 0x040004C6 RID: 1222
			ProcessEventsManually
		}

		// Token: 0x020000CB RID: 203
		public enum ScrollDeltaBehavior
		{
			// Token: 0x040004C8 RID: 1224
			UniformAcrossAllPlatforms,
			// Token: 0x040004C9 RID: 1225
			KeepPlatformSpecificInputRange
		}

		// Token: 0x020000CC RID: 204
		public enum BackgroundBehavior
		{
			// Token: 0x040004CB RID: 1227
			ResetAndDisableNonBackgroundDevices,
			// Token: 0x040004CC RID: 1228
			ResetAndDisableAllDevices,
			// Token: 0x040004CD RID: 1229
			IgnoreFocus
		}

		// Token: 0x020000CD RID: 205
		public enum EditorInputBehaviorInPlayMode
		{
			// Token: 0x040004CF RID: 1231
			PointersAndKeyboardsRespectGameViewFocus,
			// Token: 0x040004D0 RID: 1232
			AllDevicesRespectGameViewFocus,
			// Token: 0x040004D1 RID: 1233
			AllDeviceInputAlwaysGoesToGameView
		}

		// Token: 0x020000CE RID: 206
		public enum InputActionPropertyDrawerMode
		{
			// Token: 0x040004D3 RID: 1235
			Compact,
			// Token: 0x040004D4 RID: 1236
			MultilineEffective,
			// Token: 0x040004D5 RID: 1237
			MultilineBoth
		}
	}
}
