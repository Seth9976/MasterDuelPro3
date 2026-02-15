using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001CE RID: 462
	internal class NativeInputRuntime : IInputRuntime
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x0005161C File Offset: 0x0004F81C
		public int AllocateDeviceId()
		{
			return NativeInputSystem.AllocateDeviceId();
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00051623 File Offset: 0x0004F823
		public void Update(InputUpdateType updateType)
		{
			NativeInputSystem.Update((NativeInputUpdateType)updateType);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0005162B File Offset: 0x0004F82B
		public unsafe void QueueEvent(InputEvent* ptr)
		{
			NativeInputSystem.QueueInputEvent((IntPtr)((void*)ptr));
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00051638 File Offset: 0x0004F838
		public unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr)
		{
			if (commandPtr == null)
			{
				throw new ArgumentNullException("commandPtr");
			}
			return NativeInputSystem.IOCTL(deviceId, commandPtr->type, new IntPtr(commandPtr->payloadPtr), commandPtr->payloadSizeInBytes);
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0005166C File Offset: 0x0004F86C
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x00051674 File Offset: 0x0004F874
		public unsafe InputUpdateDelegate onUpdate
		{
			get
			{
				return this.m_OnUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onUpdate = delegate(NativeInputUpdateType updateType, NativeInputEventBuffer* eventBufferPtr)
					{
						InputEventBuffer buffer = new InputEventBuffer((InputEvent*)eventBufferPtr->eventBuffer, eventBufferPtr->eventCount, eventBufferPtr->sizeInBytes, eventBufferPtr->capacityInBytes);
						try
						{
							value((InputUpdateType)updateType, ref buffer);
						}
						catch (Exception e)
						{
							Debug.LogException(e);
							Debug.LogError(string.Format("{0} during event processing of {1} update; resetting event buffer", e.GetType().Name, updateType));
							buffer.Reset();
						}
						if (buffer.eventCount > 0)
						{
							eventBufferPtr->eventCount = buffer.eventCount;
							eventBufferPtr->sizeInBytes = (int)buffer.sizeInBytes;
							eventBufferPtr->capacityInBytes = (int)buffer.capacityInBytes;
							eventBufferPtr->eventBuffer = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(buffer.data);
							return;
						}
						eventBufferPtr->eventCount = 0;
						eventBufferPtr->sizeInBytes = 0;
					};
				}
				else
				{
					NativeInputSystem.onUpdate = null;
				}
				this.m_OnUpdate = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x000516BB File Offset: 0x0004F8BB
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x000516C4 File Offset: 0x0004F8C4
		public Action<InputUpdateType> onBeforeUpdate
		{
			get
			{
				return this.m_OnBeforeUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onBeforeUpdate = delegate(NativeInputUpdateType updateType)
					{
						value((InputUpdateType)updateType);
					};
				}
				else
				{
					NativeInputSystem.onBeforeUpdate = null;
				}
				this.m_OnBeforeUpdate = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0005170B File Offset: 0x0004F90B
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00051714 File Offset: 0x0004F914
		public Func<InputUpdateType, bool> onShouldRunUpdate
		{
			get
			{
				return this.m_OnShouldRunUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onShouldRunUpdate = (NativeInputUpdateType updateType) => value((InputUpdateType)updateType);
				}
				else
				{
					NativeInputSystem.onShouldRunUpdate = null;
				}
				this.m_OnShouldRunUpdate = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0005175B File Offset: 0x0004F95B
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x00051762 File Offset: 0x0004F962
		public Action<int, string> onDeviceDiscovered
		{
			get
			{
				return NativeInputSystem.onDeviceDiscovered;
			}
			set
			{
				NativeInputSystem.onDeviceDiscovered = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0005176A File Offset: 0x0004F96A
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x00051772 File Offset: 0x0004F972
		public Action onShutdown
		{
			get
			{
				return this.m_ShutdownMethod;
			}
			set
			{
				if (value == null)
				{
					Application.quitting -= this.OnShutdown;
				}
				else if (this.m_ShutdownMethod == null)
				{
					Application.quitting += this.OnShutdown;
				}
				this.m_ShutdownMethod = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x000517AA File Offset: 0x0004F9AA
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x000517B2 File Offset: 0x0004F9B2
		public Action<bool> onPlayerFocusChanged
		{
			get
			{
				return this.m_FocusChangedMethod;
			}
			set
			{
				if (value == null)
				{
					Application.focusChanged -= this.OnFocusChanged;
				}
				else if (this.m_FocusChangedMethod == null)
				{
					Application.focusChanged += this.OnFocusChanged;
				}
				this.m_FocusChangedMethod = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000517EA File Offset: 0x0004F9EA
		public bool isPlayerFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000517F1 File Offset: 0x0004F9F1
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x000517F9 File Offset: 0x0004F9F9
		public float pollingFrequency
		{
			get
			{
				return this.m_PollingFrequency;
			}
			set
			{
				this.m_PollingFrequency = value;
				NativeInputSystem.SetPollingFrequency(value);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00051808 File Offset: 0x0004FA08
		public double currentTime
		{
			get
			{
				return NativeInputSystem.currentTime;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0005180F File Offset: 0x0004FA0F
		public double currentTimeForFixedUpdate
		{
			get
			{
				return (double)Time.fixedUnscaledTime + this.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0005181E File Offset: 0x0004FA1E
		public double currentTimeOffsetToRealtimeSinceStartup
		{
			get
			{
				return NativeInputSystem.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00051825 File Offset: 0x0004FA25
		public float unscaledGameTime
		{
			get
			{
				return Time.unscaledTime;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0005182C File Offset: 0x0004FA2C
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x0005183D File Offset: 0x0004FA3D
		public bool runInBackground
		{
			get
			{
				return Application.runInBackground || this.m_RunInBackground;
			}
			set
			{
				this.m_RunInBackground = value;
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00051846 File Offset: 0x0004FA46
		private void OnShutdown()
		{
			this.m_ShutdownMethod();
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00051853 File Offset: 0x0004FA53
		private bool OnWantsToShutdown()
		{
			if (!this.m_DidCallOnShutdown)
			{
				this.OnShutdown();
				this.m_DidCallOnShutdown = true;
			}
			return true;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0005186B File Offset: 0x0004FA6B
		private void OnFocusChanged(bool focus)
		{
			this.m_FocusChangedMethod(focus);
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00051879 File Offset: 0x0004FA79
		public Vector2 screenSize
		{
			get
			{
				return new Vector2((float)Screen.width, (float)Screen.height);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005188C File Offset: 0x0004FA8C
		public ScreenOrientation screenOrientation
		{
			get
			{
				return Screen.orientation;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00051893 File Offset: 0x0004FA93
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x0005189A File Offset: 0x0004FA9A
		public bool normalizeScrollWheelDelta
		{
			get
			{
				return NativeInputSystem.normalizeScrollWheelDelta;
			}
			set
			{
				NativeInputSystem.normalizeScrollWheelDelta = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000518A2 File Offset: 0x0004FAA2
		public float scrollWheelDeltaPerTick
		{
			get
			{
				return NativeInputSystem.GetScrollWheelDeltaPerTick();
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000518A9 File Offset: 0x0004FAA9
		public bool isInBatchMode
		{
			get
			{
				return Application.isBatchMode;
			}
		}

		// Token: 0x04000A60 RID: 2656
		public static readonly NativeInputRuntime instance = new NativeInputRuntime();

		// Token: 0x04000A61 RID: 2657
		private bool m_RunInBackground;

		// Token: 0x04000A62 RID: 2658
		private Action m_ShutdownMethod;

		// Token: 0x04000A63 RID: 2659
		private InputUpdateDelegate m_OnUpdate;

		// Token: 0x04000A64 RID: 2660
		private Action<InputUpdateType> m_OnBeforeUpdate;

		// Token: 0x04000A65 RID: 2661
		private Func<InputUpdateType, bool> m_OnShouldRunUpdate;

		// Token: 0x04000A66 RID: 2662
		private float m_PollingFrequency = 60f;

		// Token: 0x04000A67 RID: 2663
		private bool m_DidCallOnShutdown;

		// Token: 0x04000A68 RID: 2664
		private Action<bool> m_FocusChangedMethod;
	}
}
