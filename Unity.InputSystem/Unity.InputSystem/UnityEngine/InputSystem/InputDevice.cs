using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000088 RID: 136
	public class InputDevice : InputControl
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00019BD2 File Offset: 0x00017DD2
		public InputDeviceDescription description
		{
			get
			{
				return this.m_Description;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00019BDA File Offset: 0x00017DDA
		public bool enabled
		{
			get
			{
				return (this.m_DeviceFlags & (InputDevice.DeviceFlags.DisabledInFrontend | InputDevice.DeviceFlags.DisabledWhileInBackground)) == (InputDevice.DeviceFlags)0 && this.QueryEnabledStateFromRuntime();
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00019BF2 File Offset: 0x00017DF2
		public bool canRunInBackground
		{
			get
			{
				return this.canDeviceRunInBackground;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00019BFC File Offset: 0x00017DFC
		internal bool canDeviceRunInBackground
		{
			get
			{
				if ((this.m_DeviceFlags & InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried) != (InputDevice.DeviceFlags)0)
				{
					return (this.m_DeviceFlags & InputDevice.DeviceFlags.CanRunInBackground) > (InputDevice.DeviceFlags)0;
				}
				QueryCanRunInBackground command = QueryCanRunInBackground.Create();
				this.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried;
				if (this.ExecuteCommand<QueryCanRunInBackground>(ref command) >= 0L && command.canRunInBackground)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackground;
					return true;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.CanRunInBackground;
				return false;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00019C7A File Offset: 0x00017E7A
		public bool added
		{
			get
			{
				return this.m_DeviceIndex != -1;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00019C88 File Offset: 0x00017E88
		public bool remote
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.Remote) == InputDevice.DeviceFlags.Remote;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00019C95 File Offset: 0x00017E95
		public bool native
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.Native) == InputDevice.DeviceFlags.Native;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00019CA4 File Offset: 0x00017EA4
		public bool updateBeforeRender
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.UpdateBeforeRender) == InputDevice.DeviceFlags.UpdateBeforeRender;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00019CB1 File Offset: 0x00017EB1
		public int deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00019CB9 File Offset: 0x00017EB9
		public double lastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTimeInternal - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00019CC7 File Offset: 0x00017EC7
		public bool wasUpdatedThisFrame
		{
			get
			{
				return this.m_CurrentUpdateStepCount == InputUpdate.s_UpdateStepCount;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00019CD6 File Offset: 0x00017ED6
		public ReadOnlyArray<InputControl> allControls
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_ChildrenForEachControl);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00019CE3 File Offset: 0x00017EE3
		public override Type valueType
		{
			get
			{
				return typeof(byte[]);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00019CEF File Offset: 0x00017EEF
		public override int valueSizeInBytes
		{
			get
			{
				return (int)this.m_StateBlock.alignedSizeInBytes;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00019CFC File Offset: 0x00017EFC
		[Obsolete("Use 'InputSystem.devices' instead. (UnityUpgradable) -> InputSystem.devices", false)]
		public static ReadOnlyArray<InputDevice> all
		{
			get
			{
				return InputSystem.devices;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00019D03 File Offset: 0x00017F03
		public InputDevice()
		{
			this.m_DeviceId = 0;
			this.m_ParticipantId = 0;
			this.m_DeviceIndex = -1;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000179CE File Offset: 0x00015BCE
		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00019D20 File Offset: 0x00017F20
		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			if (this.m_DeviceIndex == -1)
			{
				return null;
			}
			uint numBytes = base.stateBlock.alignedSizeInBytes;
			byte[] array2;
			byte[] array = (array2 = new byte[numBytes]);
			byte* arrayPtr;
			if (array == null || array2.Length == 0)
			{
				arrayPtr = null;
			}
			else
			{
				arrayPtr = &array2[0];
			}
			byte* adjustedStatePtr = (byte*)statePtr + this.m_StateBlock.byteOffset;
			UnsafeUtility.MemCpy((void*)arrayPtr, (void*)adjustedStatePtr, (long)((ulong)numBytes));
			array2 = null;
			return array;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00019D84 File Offset: 0x00017F84
		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			if (bufferSize < this.valueSizeInBytes)
			{
				throw new ArgumentException(string.Format("Buffer too small (expected: {0}, actual: {1}", this.valueSizeInBytes, bufferSize));
			}
			byte* adjustedStatePtr = (byte*)statePtr + this.m_StateBlock.byteOffset;
			UnsafeUtility.MemCpy(bufferPtr, (void*)adjustedStatePtr, (long)((ulong)this.m_StateBlock.alignedSizeInBytes));
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00019E00 File Offset: 0x00018000
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			if (firstStatePtr == null)
			{
				throw new ArgumentNullException("firstStatePtr");
			}
			if (secondStatePtr == null)
			{
				throw new ArgumentNullException("secondStatePtr");
			}
			void* ptr = (void*)((byte*)firstStatePtr + this.m_StateBlock.byteOffset);
			byte* adjustedSecondStatePtr = (byte*)firstStatePtr + this.m_StateBlock.byteOffset;
			return UnsafeUtility.MemCmp(ptr, (void*)adjustedSecondStatePtr, (long)((ulong)this.m_StateBlock.alignedSizeInBytes)) == 0;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00019E60 File Offset: 0x00018060
		internal void NotifyConfigurationChanged()
		{
			base.isConfigUpToDate = false;
			for (int i = 0; i < this.m_ChildrenForEachControl.Length; i++)
			{
				this.m_ChildrenForEachControl[i].isConfigUpToDate = false;
			}
			this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			this.OnConfigurationChanged();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void MakeCurrent()
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void OnAdded()
		{
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void OnRemoved()
		{
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void OnConfigurationChanged()
		{
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00019EAC File Offset: 0x000180AC
		public unsafe long ExecuteCommand<TCommand>(ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			InputDeviceCommand* commandPtr = (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command);
			InputManager manager = InputSystem.s_Manager;
			manager.m_DeviceCommandCallbacks.LockForChanges();
			for (int i = 0; i < manager.m_DeviceCommandCallbacks.length; i++)
			{
				try
				{
					long? result = manager.m_DeviceCommandCallbacks[i](this, commandPtr);
					if (result != null)
					{
						return result.Value;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onDeviceCommand' callbacks");
					Debug.LogException(ex);
				}
			}
			manager.m_DeviceCommandCallbacks.UnlockForChanges();
			return this.ExecuteCommand((InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command));
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00019F60 File Offset: 0x00018160
		protected unsafe virtual long ExecuteCommand(InputDeviceCommand* commandPtr)
		{
			return InputRuntime.s_Instance.DeviceCommand(this.deviceId, commandPtr);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00019F74 File Offset: 0x00018174
		internal bool QueryEnabledStateFromRuntime()
		{
			if ((this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime) == (InputDevice.DeviceFlags)0)
			{
				QueryEnabledStateCommand command = QueryEnabledStateCommand.Create();
				if (this.ExecuteCommand<QueryEnabledStateCommand>(ref command) >= 0L)
				{
					if (command.isEnabled)
					{
						this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
					}
					else
					{
						this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInRuntime;
					}
				}
				else
				{
					this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
				}
				this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			}
			return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInRuntime) == (InputDevice.DeviceFlags)0;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00019FFE File Offset: 0x000181FE
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001A00C File Offset: 0x0001820C
		internal bool disabledInFrontend
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInFrontend) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInFrontend;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInFrontend;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001A030 File Offset: 0x00018230
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001A041 File Offset: 0x00018241
		internal bool disabledInRuntime
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInRuntime) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInRuntime;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001A06B File Offset: 0x0001826B
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001A07C File Offset: 0x0001827C
		internal bool disabledWhileInBackground
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledWhileInBackground) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledWhileInBackground;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledWhileInBackground;
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001A0A6 File Offset: 0x000182A6
		internal static uint EncodeStateOffsetToControlMapEntry(uint controlIndex, uint stateOffsetInBits, uint stateSizeInBits)
		{
			return (stateOffsetInBits << 19) | (stateSizeInBits << 10) | controlIndex;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001A0B3 File Offset: 0x000182B3
		internal static void DecodeStateOffsetToControlMapEntry(uint entry, out uint controlIndex, out uint stateOffset, out uint stateSize)
		{
			controlIndex = entry & 1023U;
			stateOffset = entry >> 19;
			stateSize = (entry >> 10) & 511U;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001A0D0 File Offset: 0x000182D0
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0001A0DD File Offset: 0x000182DD
		internal bool hasControlsWithDefaultState
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasControlsWithDefaultState) == InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasControlsWithDefaultState;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001A100 File Offset: 0x00018300
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001A115 File Offset: 0x00018315
		internal bool hasDontResetControls
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasDontResetControls) == InputDevice.DeviceFlags.HasDontResetControls;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasDontResetControls;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasDontResetControls;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001A13F File Offset: 0x0001833F
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001A14C File Offset: 0x0001834C
		internal bool hasStateCallbacks
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasStateCallbacks) == InputDevice.DeviceFlags.HasStateCallbacks;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasStateCallbacks;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasStateCallbacks;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001A16F File Offset: 0x0001836F
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001A184 File Offset: 0x00018384
		internal bool hasEventMerger
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasEventMerger) == InputDevice.DeviceFlags.HasEventMerger;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasEventMerger;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasEventMerger;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001A1AE File Offset: 0x000183AE
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001A1C3 File Offset: 0x000183C3
		internal bool hasEventPreProcessor
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasEventPreProcessor) == InputDevice.DeviceFlags.HasEventPreProcessor;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasEventPreProcessor;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasEventPreProcessor;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001A1F0 File Offset: 0x000183F0
		internal void AddDeviceUsage(InternedString usage)
		{
			int totalUsageCount = this.m_UsageToControl.LengthSafe<InputControl>() + this.m_UsageCount;
			if (this.m_UsageCount == 0)
			{
				this.m_UsageStartIndex = totalUsageCount;
			}
			ArrayHelpers.AppendWithCapacity<InternedString>(ref this.m_UsagesForEachControl, ref totalUsageCount, usage, 10);
			this.m_UsageCount++;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001A240 File Offset: 0x00018440
		internal void RemoveDeviceUsage(InternedString usage)
		{
			int totalUsageCount = this.m_UsageToControl.LengthSafe<InputControl>() + this.m_UsageCount;
			int index = this.m_UsagesForEachControl.IndexOfValue(usage, this.m_UsageStartIndex, totalUsageCount);
			if (index == -1)
			{
				return;
			}
			this.m_UsagesForEachControl.EraseAtWithCapacity(ref totalUsageCount, index);
			this.m_UsageCount--;
			if (this.m_UsageCount == 0)
			{
				this.m_UsageStartIndex = 0;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001A2A4 File Offset: 0x000184A4
		internal void ClearDeviceUsages()
		{
			for (int i = this.m_UsageStartIndex; i < this.m_UsageCount; i++)
			{
				this.m_UsagesForEachControl[i] = default(InternedString);
			}
			this.m_UsageCount = 0;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001A2E0 File Offset: 0x000184E0
		internal bool RequestSync()
		{
			base.SetOptimizedControlDataTypeRecursively();
			RequestSyncCommand syncCommand = RequestSyncCommand.Create();
			return base.device.ExecuteCommand<RequestSyncCommand>(ref syncCommand) >= 0L;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001A310 File Offset: 0x00018510
		internal bool RequestReset()
		{
			base.SetOptimizedControlDataTypeRecursively();
			RequestResetCommand resetCommand = RequestResetCommand.Create();
			return base.device.ExecuteCommand<RequestResetCommand>(ref resetCommand) >= 0L;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001A340 File Offset: 0x00018540
		internal bool ExecuteEnableCommand()
		{
			base.SetOptimizedControlDataTypeRecursively();
			EnableDeviceCommand command = EnableDeviceCommand.Create();
			return base.device.ExecuteCommand<EnableDeviceCommand>(ref command) >= 0L;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001A370 File Offset: 0x00018570
		internal bool ExecuteDisableCommand()
		{
			DisableDeviceCommand command = DisableDeviceCommand.Create();
			return base.device.ExecuteCommand<DisableDeviceCommand>(ref command) >= 0L;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001A397 File Offset: 0x00018597
		internal void NotifyAdded()
		{
			this.OnAdded();
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001A39F File Offset: 0x0001859F
		internal void NotifyRemoved()
		{
			this.OnRemoved();
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001A3A8 File Offset: 0x000185A8
		internal static TDevice Build<TDevice>(string layoutName = null, string layoutVariants = null, InputDeviceDescription deviceDescription = default(InputDeviceDescription), bool noPrecompiledLayouts = false) where TDevice : InputDevice
		{
			InternedString internedLayoutName = new InternedString(layoutName);
			if (internedLayoutName.IsEmpty())
			{
				internedLayoutName = InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice));
				if (internedLayoutName.IsEmpty())
				{
					internedLayoutName = new InternedString(typeof(TDevice).Name);
				}
			}
			InputControlLayout.Collection.PrecompiledLayout precompiledLayout;
			if (!noPrecompiledLayouts && string.IsNullOrEmpty(layoutVariants) && InputControlLayout.s_Layouts.precompiledLayouts.TryGetValue(internedLayoutName, out precompiledLayout))
			{
				return (TDevice)((object)precompiledLayout.factoryMethod());
			}
			TDevice tdevice;
			using (InputDeviceBuilder.Ref())
			{
				InputDeviceBuilder.instance.Setup(internedLayoutName, new InternedString(layoutVariants), deviceDescription);
				InputDevice device = InputDeviceBuilder.instance.Finish();
				TDevice deviceOfType = device as TDevice;
				if (deviceOfType == null)
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Expected device of type '",
						typeof(TDevice).Name,
						"' but got device of type '",
						device.GetType().Name,
						"' instead"
					}), "TDevice");
				}
				tdevice = deviceOfType;
			}
			return tdevice;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001A4D8 File Offset: 0x000186D8
		internal unsafe void WriteChangedControlStates(byte* deviceStateBuffer, void* statePtr, uint stateSizeInBytes, uint stateOffsetInDevice)
		{
			if (this.m_ControlTreeNodes.Length == 0)
			{
				return;
			}
			this.m_UpdatedButtons.Clear();
			if (this.m_StateBlock.sizeInBits != stateSizeInBytes * 8U)
			{
				if (this.m_ControlTreeNodes[0].leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(stateSizeInBytes * 8U, stateOffsetInDevice * 8U, this.m_ControlTreeNodes[0], 0U);
					return;
				}
			}
			else if (this.m_ControlTreeNodes[0].leftChildIndex != -1)
			{
				this.WriteChangedControlStatesInternal(statePtr, deviceStateBuffer, this.m_ControlTreeNodes[0], 0U);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001A564 File Offset: 0x00018764
		private void WritePartialChangedControlStatesInternal(uint stateSizeInBits, uint stateOffsetInDeviceInBits, InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			InputDevice.ControlBitRangeNode leftNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			if (Math.Max(stateOffsetInDeviceInBits, startOffset) <= Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, (uint)leftNode.endBitOffset))
			{
				int controlEndIndex = (int)(leftNode.controlStartIndex + (ushort)leftNode.controlCount);
				for (int i = (int)leftNode.controlStartIndex; i < controlEndIndex; i++)
				{
					ushort controlIndex = this.m_ControlTreeIndices[i];
					InputControl control = this.m_ChildrenForEachControl[(int)controlIndex];
					control.MarkAsStale();
					if (control.isButton && ((ButtonControl)control).needsToCheckFramePress)
					{
						this.m_UpdatedButtons.Add((int)controlIndex);
					}
				}
				if (leftNode.leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(stateSizeInBits, stateOffsetInDeviceInBits, leftNode, startOffset);
				}
			}
			InputDevice.ControlBitRangeNode rightNode = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			if (Math.Max(stateOffsetInDeviceInBits, (uint)leftNode.endBitOffset) <= Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, (uint)rightNode.endBitOffset))
			{
				int controlEndIndex2 = (int)(rightNode.controlStartIndex + (ushort)rightNode.controlCount);
				for (int j = (int)rightNode.controlStartIndex; j < controlEndIndex2; j++)
				{
					ushort controlIndex2 = this.m_ControlTreeIndices[j];
					InputControl control2 = this.m_ChildrenForEachControl[(int)controlIndex2];
					control2.MarkAsStale();
					if (control2.isButton && ((ButtonControl)control2).needsToCheckFramePress)
					{
						this.m_UpdatedButtons.Add((int)controlIndex2);
					}
				}
				if (rightNode.leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(stateSizeInBits, stateOffsetInDeviceInBits, rightNode, (uint)leftNode.endBitOffset);
				}
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001A6C0 File Offset: 0x000188C0
		private void DumpControlBitRangeNode(int nodeIndex, InputDevice.ControlBitRangeNode node, uint startOffset, uint sizeInBits, List<string> output)
		{
			List<string> names = new List<string>();
			for (int i = 0; i < (int)node.controlCount; i++)
			{
				ushort controlIndex = this.m_ControlTreeIndices[(int)node.controlStartIndex + i];
				InputControl control = this.m_ChildrenForEachControl[(int)controlIndex];
				names.Add(control.path);
			}
			string namesStr = string.Join(", ", names);
			string children = ((node.leftChildIndex != -1) ? string.Format(" <{0}, {1}>", node.leftChildIndex, (int)(node.leftChildIndex + 1)) : "");
			output.Add(string.Format("{0} [{1}, {2}]{3}->{4}", new object[]
			{
				nodeIndex,
				startOffset,
				startOffset + sizeInBits,
				children,
				namesStr
			}));
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001A78C File Offset: 0x0001898C
		private void DumpControlTree(InputDevice.ControlBitRangeNode parentNode, uint startOffset, List<string> output)
		{
			InputDevice.ControlBitRangeNode leftNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			InputDevice.ControlBitRangeNode rightNode = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			this.DumpControlBitRangeNode((int)parentNode.leftChildIndex, leftNode, startOffset, (uint)leftNode.endBitOffset - startOffset, output);
			this.DumpControlBitRangeNode((int)(parentNode.leftChildIndex + 1), rightNode, (uint)leftNode.endBitOffset, (uint)(rightNode.endBitOffset - leftNode.endBitOffset), output);
			if (leftNode.leftChildIndex != -1)
			{
				this.DumpControlTree(leftNode, startOffset, output);
			}
			if (rightNode.leftChildIndex != -1)
			{
				this.DumpControlTree(rightNode, (uint)leftNode.endBitOffset, output);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001A824 File Offset: 0x00018A24
		internal string DumpControlTree()
		{
			List<string> output = new List<string>();
			this.DumpControlTree(this.m_ControlTreeNodes[0], 0U, output);
			return string.Join("\n", output);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001A858 File Offset: 0x00018A58
		private unsafe void WriteChangedControlStatesInternal(void* statePtr, byte* deviceStatePtr, InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			InputDevice.ControlBitRangeNode leftNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			if (InputDevice.HasDataChangedInRange(deviceStatePtr, statePtr, startOffset, (uint)leftNode.endBitOffset - startOffset + 1U))
			{
				int controlEndIndex = (int)(leftNode.controlStartIndex + (ushort)leftNode.controlCount);
				for (int i = (int)leftNode.controlStartIndex; i < controlEndIndex; i++)
				{
					ushort controlIndex = this.m_ControlTreeIndices[i];
					InputControl control = this.m_ChildrenForEachControl[(int)controlIndex];
					if (!control.CompareState((void*)(deviceStatePtr - this.m_StateBlock.byteOffset), (void*)((byte*)statePtr - this.m_StateBlock.byteOffset), null))
					{
						control.MarkAsStale();
						if (control.isButton && ((ButtonControl)control).needsToCheckFramePress)
						{
							this.m_UpdatedButtons.Add((int)controlIndex);
						}
					}
				}
				if (leftNode.leftChildIndex != -1)
				{
					this.WriteChangedControlStatesInternal(statePtr, deviceStatePtr, leftNode, startOffset);
				}
			}
			InputDevice.ControlBitRangeNode rightNode = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			if (!InputDevice.HasDataChangedInRange(deviceStatePtr, statePtr, (uint)leftNode.endBitOffset, (uint)(rightNode.endBitOffset - leftNode.endBitOffset + 1)))
			{
				return;
			}
			int rightNodeControlEndIndex = (int)(rightNode.controlStartIndex + (ushort)rightNode.controlCount);
			for (int j = (int)rightNode.controlStartIndex; j < rightNodeControlEndIndex; j++)
			{
				ushort controlIndex2 = this.m_ControlTreeIndices[j];
				InputControl control2 = this.m_ChildrenForEachControl[(int)controlIndex2];
				if (!control2.CompareState((void*)(deviceStatePtr - this.m_StateBlock.byteOffset), (void*)((byte*)statePtr - this.m_StateBlock.byteOffset), null))
				{
					control2.MarkAsStale();
					if (control2.isButton && ((ButtonControl)control2).needsToCheckFramePress)
					{
						this.m_UpdatedButtons.Add((int)controlIndex2);
					}
				}
			}
			if (rightNode.leftChildIndex != -1)
			{
				this.WriteChangedControlStatesInternal(statePtr, deviceStatePtr, rightNode, (uint)leftNode.endBitOffset);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001AA07 File Offset: 0x00018C07
		private unsafe static bool HasDataChangedInRange(byte* deviceStatePtr, void* statePtr, uint startOffset, uint sizeInBits)
		{
			if (sizeInBits == 1U)
			{
				return MemoryHelpers.ReadSingleBit((void*)deviceStatePtr, startOffset) != MemoryHelpers.ReadSingleBit(statePtr, startOffset);
			}
			return !MemoryHelpers.MemCmpBitRegion((void*)deviceStatePtr, statePtr, startOffset, sizeInBits, null);
		}

		// Token: 0x040002FD RID: 765
		public const int InvalidDeviceId = 0;

		// Token: 0x040002FE RID: 766
		internal const int kLocalParticipantId = 0;

		// Token: 0x040002FF RID: 767
		internal const int kInvalidDeviceIndex = -1;

		// Token: 0x04000300 RID: 768
		internal InputDevice.DeviceFlags m_DeviceFlags;

		// Token: 0x04000301 RID: 769
		internal int m_DeviceId;

		// Token: 0x04000302 RID: 770
		internal int m_ParticipantId;

		// Token: 0x04000303 RID: 771
		internal int m_DeviceIndex;

		// Token: 0x04000304 RID: 772
		internal uint m_CurrentProcessedEventBytesOnUpdate;

		// Token: 0x04000305 RID: 773
		internal InputDeviceDescription m_Description;

		// Token: 0x04000306 RID: 774
		internal double m_LastUpdateTimeInternal;

		// Token: 0x04000307 RID: 775
		internal uint m_CurrentUpdateStepCount;

		// Token: 0x04000308 RID: 776
		internal InternedString[] m_AliasesForEachControl;

		// Token: 0x04000309 RID: 777
		internal InternedString[] m_UsagesForEachControl;

		// Token: 0x0400030A RID: 778
		internal InputControl[] m_UsageToControl;

		// Token: 0x0400030B RID: 779
		internal InputControl[] m_ChildrenForEachControl;

		// Token: 0x0400030C RID: 780
		internal HashSet<int> m_UpdatedButtons;

		// Token: 0x0400030D RID: 781
		internal List<ButtonControl> m_ButtonControlsCheckingPressState;

		// Token: 0x0400030E RID: 782
		internal bool m_UseCachePathForButtonPresses;

		// Token: 0x0400030F RID: 783
		internal uint[] m_StateOffsetToControlMap;

		// Token: 0x04000310 RID: 784
		internal InputDevice.ControlBitRangeNode[] m_ControlTreeNodes;

		// Token: 0x04000311 RID: 785
		internal ushort[] m_ControlTreeIndices;

		// Token: 0x04000312 RID: 786
		internal const int kControlIndexBits = 10;

		// Token: 0x04000313 RID: 787
		internal const int kStateOffsetBits = 13;

		// Token: 0x04000314 RID: 788
		internal const int kStateSizeBits = 9;

		// Token: 0x02000089 RID: 137
		[Flags]
		[Serializable]
		internal enum DeviceFlags
		{
			// Token: 0x04000316 RID: 790
			UpdateBeforeRender = 1,
			// Token: 0x04000317 RID: 791
			HasStateCallbacks = 2,
			// Token: 0x04000318 RID: 792
			HasControlsWithDefaultState = 4,
			// Token: 0x04000319 RID: 793
			HasDontResetControls = 1024,
			// Token: 0x0400031A RID: 794
			HasEventMerger = 8192,
			// Token: 0x0400031B RID: 795
			HasEventPreProcessor = 16384,
			// Token: 0x0400031C RID: 796
			Remote = 8,
			// Token: 0x0400031D RID: 797
			Native = 16,
			// Token: 0x0400031E RID: 798
			DisabledInFrontend = 32,
			// Token: 0x0400031F RID: 799
			DisabledInRuntime = 128,
			// Token: 0x04000320 RID: 800
			DisabledWhileInBackground = 256,
			// Token: 0x04000321 RID: 801
			DisabledStateHasBeenQueriedFromRuntime = 64,
			// Token: 0x04000322 RID: 802
			CanRunInBackground = 2048,
			// Token: 0x04000323 RID: 803
			CanRunInBackgroundHasBeenQueried = 4096
		}

		// Token: 0x0200008A RID: 138
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct ControlBitRangeNode
		{
			// Token: 0x060006A0 RID: 1696 RVA: 0x0001AA2F File Offset: 0x00018C2F
			public ControlBitRangeNode(ushort endOffset)
			{
				this.controlStartIndex = 0;
				this.controlCount = 0;
				this.endBitOffset = endOffset;
				this.leftChildIndex = -1;
			}

			// Token: 0x04000324 RID: 804
			public ushort endBitOffset;

			// Token: 0x04000325 RID: 805
			public short leftChildIndex;

			// Token: 0x04000326 RID: 806
			public ushort controlStartIndex;

			// Token: 0x04000327 RID: 807
			public byte controlCount;
		}
	}
}
