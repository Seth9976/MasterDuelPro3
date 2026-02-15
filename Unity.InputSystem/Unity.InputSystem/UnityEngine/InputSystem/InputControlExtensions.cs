using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000072 RID: 114
	public static class InputControlExtensions
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x000157C4 File Offset: 0x000139C4
		public static TControl FindInParentChain<TControl>(this InputControl control) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			for (InputControl parent = control; parent != null; parent = parent.parent)
			{
				TControl parentOfType = parent as TControl;
				if (parentOfType != null)
				{
					return parentOfType;
				}
			}
			return default(TControl);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001580C File Offset: 0x00013A0C
		public static bool IsPressed(this InputControl control, float buttonPressPoint = 0f)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (Mathf.Approximately(0f, buttonPressPoint))
			{
				ButtonControl button = control as ButtonControl;
				if (button != null)
				{
					buttonPressPoint = button.pressPointOrDefault;
				}
				else
				{
					buttonPressPoint = ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
			}
			return control.IsActuated(buttonPressPoint);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00015858 File Offset: 0x00013A58
		public static bool IsActuated(this InputControl control, float threshold = 0f)
		{
			if (control.CheckStateIsAtDefault())
			{
				return false;
			}
			float magnitude = control.magnitude;
			if (magnitude < 0f)
			{
				return Mathf.Approximately(threshold, 0f);
			}
			if (Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000158AC File Offset: 0x00013AAC
		public static object ReadValueAsObject(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.currentStatePtr);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000158C8 File Offset: 0x00013AC8
		public unsafe static void ReadValueIntoBuffer(this InputControl control, void* buffer, int bufferSize)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			control.ReadValueFromStateIntoBuffer(control.currentStatePtr, buffer, bufferSize);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000158F6 File Offset: 0x00013AF6
		public static object ReadDefaultValueAsObject(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.defaultStatePtr);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00015914 File Offset: 0x00013B14
		public static TValue ReadValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			TValue value;
			if (!control.ReadValueFromEvent(inputEvent, out value))
			{
				return default(TValue);
			}
			return value;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00015948 File Offset: 0x00013B48
		public unsafe static bool ReadValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtr = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtr == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadValueFromState(statePtr);
			return true;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001598C File Offset: 0x00013B8C
		public unsafe static object ReadValueFromEventAsObject(this InputControl control, InputEventPtr inputEvent)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtr = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtr == null)
			{
				return control.ReadDefaultValueAsObject();
			}
			return control.ReadValueFromStateAsObject(statePtr);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000159C4 File Offset: 0x00013BC4
		public static TValue ReadUnprocessedValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			TValue result = default(TValue);
			control.ReadUnprocessedValueFromEvent(eventPtr, out result);
			return result;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000159F4 File Offset: 0x00013BF4
		public unsafe static bool ReadUnprocessedValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtr = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtr == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadUnprocessedValueFromState(statePtr);
			return true;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00015A38 File Offset: 0x00013C38
		public unsafe static void WriteValueFromObjectIntoEvent(this InputControl control, InputEventPtr eventPtr, object value)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtr = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtr == null)
			{
				return;
			}
			control.WriteValueFromObjectIntoState(value, statePtr);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00015A6C File Offset: 0x00013C6C
		public unsafe static void WriteValueIntoState(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			int valueSize = control.valueSizeInBytes;
			void* valuePtr = UnsafeUtility.Malloc((long)valueSize, 8, Allocator.Temp);
			try
			{
				control.ReadValueFromStateIntoBuffer(control.currentStatePtr, valuePtr, valueSize);
				control.WriteValueFromBufferIntoState(valuePtr, valueSize, statePtr);
			}
			finally
			{
				UnsafeUtility.Free(valuePtr, Allocator.Temp);
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00015ADC File Offset: 0x00013CDC
		public unsafe static void WriteValueIntoState<TValue>(this InputControl control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControl<TValue> controlOfType = control as InputControl<TValue>;
			if (controlOfType == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expecting control of type '",
					typeof(TValue).Name,
					"' but got '",
					control.GetType().Name,
					"'"
				}));
			}
			controlOfType.WriteValueIntoState(value, statePtr);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00015B54 File Offset: 0x00013D54
		public unsafe static void WriteValueIntoState<TValue>(this InputControl<TValue> control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			void* valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
			int valueSize = UnsafeUtility.SizeOf<TValue>();
			control.WriteValueFromBufferIntoState(valuePtr, valueSize, statePtr);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00015B96 File Offset: 0x00013D96
		public unsafe static void WriteValueIntoState<TValue>(this InputControl<TValue> control, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			control.WriteValueIntoState(control.ReadValue(), statePtr);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public unsafe static void WriteValueIntoState<TValue, TState>(this InputControl<TValue> control, TValue value, ref TState state) where TValue : struct where TState : struct, IInputStateTypeInfo
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			int sizeOfState = UnsafeUtility.SizeOf<TState>();
			if ((ulong)(control.stateOffsetRelativeToDeviceRoot + control.m_StateBlock.alignedSizeInBytes) >= (ulong)((long)sizeOfState))
			{
				throw new ArgumentException(string.Format("Control {0} with offset {1} and size of {2} bits is out of bounds for state of type {3} with size {4}", new object[]
				{
					control.path,
					control.stateOffsetRelativeToDeviceRoot,
					control.m_StateBlock.sizeInBits,
					typeof(TState).Name,
					sizeOfState
				}), "state");
			}
			byte* statePtr = (byte*)UnsafeUtility.AddressOf<TState>(ref state);
			control.WriteValueIntoState(value, (void*)statePtr);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00015C5C File Offset: 0x00013E5C
		public static void WriteValueIntoEvent<TValue>(this InputControl control, TValue value, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			InputControl<TValue> controlOfType = control as InputControl<TValue>;
			if (controlOfType == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expecting control of type '",
					typeof(TValue).Name,
					"' but got '",
					control.GetType().Name,
					"'"
				}));
			}
			controlOfType.WriteValueIntoEvent(value, eventPtr);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public unsafe static void WriteValueIntoEvent<TValue>(this InputControl<TValue> control, TValue value, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			void* statePtr = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtr == null)
			{
				return;
			}
			control.WriteValueIntoState(value, statePtr);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00015D30 File Offset: 0x00013F30
		public unsafe static void CopyState(this InputDevice device, void* buffer, int bufferSizeInBytes)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (bufferSizeInBytes <= 0)
			{
				throw new ArgumentException("bufferSizeInBytes must be positive", "bufferSizeInBytes");
			}
			InputStateBlock stateBlock = device.m_StateBlock;
			long sizeToCopy = Math.Min((long)bufferSizeInBytes, (long)((ulong)stateBlock.alignedSizeInBytes));
			UnsafeUtility.MemCpy(buffer, (void*)((byte*)device.currentStatePtr + stateBlock.byteOffset), sizeToCopy);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00015D8C File Offset: 0x00013F8C
		public unsafe static void CopyState<TState>(this InputDevice device, out TState state) where TState : struct, IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			state = default(TState);
			if (device.stateBlock.format != state.format)
			{
				throw new ArgumentException(string.Format("Struct '{0}' has state format '{1}' which doesn't match device '{2}' with state format '{3}'", new object[]
				{
					typeof(TState).Name,
					state.format,
					device,
					device.stateBlock.format
				}), "TState");
			}
			int stateSize = UnsafeUtility.SizeOf<TState>();
			void* statePtr = UnsafeUtility.AddressOf<TState>(ref state);
			device.CopyState(statePtr, stateSize);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00015E3F File Offset: 0x0001403F
		public static bool CheckStateIsAtDefault(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefault(control.currentStatePtr, null);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00015E5D File Offset: 0x0001405D
		public unsafe static bool CheckStateIsAtDefault(this InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(statePtr, control.defaultStatePtr, maskPtr);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00015E8B File Offset: 0x0001408B
		public static bool CheckStateIsAtDefaultIgnoringNoise(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefaultIgnoringNoise(control.currentStatePtr);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00015EA7 File Offset: 0x000140A7
		public unsafe static bool CheckStateIsAtDefaultIgnoringNoise(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CheckStateIsAtDefault(statePtr, InputStateBuffers.s_NoiseMaskBuffer);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00015ED3 File Offset: 0x000140D3
		public unsafe static bool CompareStateIgnoringNoise(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, control.noiseMaskPtr);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00015F08 File Offset: 0x00014108
		public unsafe static bool CompareState(this InputControl control, void* firstStatePtr, void* secondStatePtr, void* maskPtr = null)
		{
			byte* firstPtr = (byte*)firstStatePtr + control.m_StateBlock.byteOffset;
			byte* secondPtr = (byte*)secondStatePtr + control.m_StateBlock.byteOffset;
			byte* mask = ((maskPtr != null) ? ((byte*)maskPtr + control.m_StateBlock.byteOffset) : null);
			if (control.m_StateBlock.sizeInBits == 1U)
			{
				return (mask != null && MemoryHelpers.ReadSingleBit((void*)mask, control.m_StateBlock.bitOffset)) || MemoryHelpers.ReadSingleBit((void*)secondPtr, control.m_StateBlock.bitOffset) == MemoryHelpers.ReadSingleBit((void*)firstPtr, control.m_StateBlock.bitOffset);
			}
			return MemoryHelpers.MemCmpBitRegion((void*)firstPtr, (void*)secondPtr, control.m_StateBlock.bitOffset, control.m_StateBlock.sizeInBits, (void*)mask);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00015FB3 File Offset: 0x000141B3
		public unsafe static bool CompareState(this InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, maskPtr);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00015FE1 File Offset: 0x000141E1
		public unsafe static bool HasValueChangeInState(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareValue(control.currentStatePtr, statePtr);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00016010 File Offset: 0x00014210
		public unsafe static bool HasValueChangeInEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			void* statePtr = control.GetStatePtrFromStateEvent(eventPtr);
			return statePtr != null && control.CompareValue(control.currentStatePtr, statePtr);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001605B File Offset: 0x0001425B
		public unsafe static void* GetStatePtrFromStateEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			return control.GetStatePtrFromStateEventUnchecked(eventPtr, eventPtr.type);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00016090 File Offset: 0x00014290
		internal unsafe static void* GetStatePtrFromStateEventUnchecked(this InputControl control, InputEventPtr eventPtr, FourCC eventType)
		{
			uint stateOffset;
			FourCC stateFormat;
			uint stateSizeInBytes;
			void* statePtr;
			if (eventType == 1398030676)
			{
				StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
				stateOffset = 0U;
				stateFormat = ptr->stateFormat;
				stateSizeInBytes = ptr->stateSizeInBytes;
				statePtr = ptr->state;
			}
			else
			{
				if (!(eventType == 1145852993))
				{
					throw new ArgumentException(string.Format("Event must be a StateEvent or DeltaStateEvent but is a {0} instead", eventType), "eventPtr");
				}
				DeltaStateEvent* ptr2 = DeltaStateEvent.FromUnchecked(eventPtr);
				stateOffset = ptr2->stateOffset;
				stateFormat = ptr2->stateFormat;
				stateSizeInBytes = ptr2->deltaStateSizeInBytes;
				statePtr = ptr2->deltaState;
			}
			InputDevice device = control.device;
			if (stateFormat != device.m_StateBlock.format && (!device.hasStateCallbacks || !((IInputStateCallbackReceiver)device).GetStateOffsetForEvent(control, eventPtr, ref stateOffset)))
			{
				return null;
			}
			stateOffset += device.m_StateBlock.byteOffset;
			ref InputStateBlock controlStateBlock = ref control.m_StateBlock;
			long controlOffset = (long)controlStateBlock.effectiveByteOffset - (long)((ulong)stateOffset);
			if (controlOffset < 0L || controlOffset + (long)((ulong)controlStateBlock.alignedSizeInBytes) > (long)((ulong)stateSizeInBytes))
			{
				return null;
			}
			return (void*)((byte*)statePtr - stateOffset);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00016198 File Offset: 0x00014398
		public unsafe static bool ResetToDefaultStateInEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			FourCC eventType = eventPtr.type;
			if (eventType != 1398030676 && eventType != 1145852993)
			{
				throw new ArgumentException("Given event is not a StateEvent or a DeltaStateEvent", "eventPtr");
			}
			byte* statePtr = (byte*)control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtr == null)
			{
				return false;
			}
			byte* defaultStatePtr = (byte*)control.defaultStatePtr;
			ref InputStateBlock stateBlock = ref control.m_StateBlock;
			uint offset = stateBlock.byteOffset;
			MemoryHelpers.MemCpyBitRegion((void*)(statePtr + offset), (void*)(defaultStatePtr + offset), stateBlock.bitOffset, stateBlock.sizeInBits);
			return true;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016244 File Offset: 0x00014444
		public static void QueueValueChange<TValue>(this InputControl<TValue> control, TValue value, double time = -1.0) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputEventPtr eventPtr;
			using (StateEvent.From(control.device, out eventPtr, Allocator.Temp))
			{
				if (time >= 0.0)
				{
					eventPtr.time = time;
				}
				control.WriteValueIntoEvent(value, eventPtr);
				InputSystem.QueueEvent(eventPtr);
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000162B0 File Offset: 0x000144B0
		public unsafe static void AccumulateValueInEvent(this InputControl<float> control, void* currentStatePtr, InputEventPtr newState)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			float newValue;
			if (!control.ReadUnprocessedValueFromEvent(newState, out newValue))
			{
				return;
			}
			float oldValue = control.ReadUnprocessedValueFromState(currentStatePtr);
			control.WriteValueIntoEvent(oldValue + newValue, newState);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000162EC File Offset: 0x000144EC
		internal unsafe static void AccumulateValueInEvent(this InputControl<Vector2> control, void* currentStatePtr, InputEventPtr newState)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Vector2 newValue;
			if (!control.ReadUnprocessedValueFromEvent(newState, out newValue))
			{
				return;
			}
			Vector2 oldDelta = control.ReadUnprocessedValueFromState(currentStatePtr);
			control.WriteValueIntoEvent(oldDelta + newValue, newState);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001632C File Offset: 0x0001452C
		public static void FindControlsRecursive<TControl>(this InputControl parent, IList<TControl> controls, Func<TControl, bool> predicate) where TControl : InputControl
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (controls == null)
			{
				throw new ArgumentNullException("controls");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			TControl parentAsTControl = parent as TControl;
			if (parentAsTControl != null && predicate(parentAsTControl))
			{
				controls.Add(parentAsTControl);
			}
			int childCount = parent.children.Count;
			for (int i = 0; i < childCount; i++)
			{
				parent.children[i].FindControlsRecursive(controls, predicate);
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000163BC File Offset: 0x000145BC
		internal static string BuildPath(this InputControl control, string deviceLayout, StringBuilder builder = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(deviceLayout))
			{
				throw new ArgumentNullException("deviceLayout");
			}
			if (builder == null)
			{
				builder = new StringBuilder();
			}
			InputDevice device = control.device;
			builder.Append('<');
			builder.Append(deviceLayout.Escape("\\>", "\\>"));
			builder.Append('>');
			ReadOnlyArray<InternedString> deviceUsages = device.usages;
			for (int i = 0; i < deviceUsages.Count; i++)
			{
				builder.Append('{');
				builder.Append(deviceUsages[i].ToString().Escape("\\}", "\\}"));
				builder.Append('}');
			}
			builder.Append('/');
			string devicePath = device.path.Replace("\\", "\\\\");
			string controlPath = control.path.Replace("\\", "\\\\");
			builder.Append(controlPath, devicePath.Length + 1, controlPath.Length - devicePath.Length - 1);
			return builder.ToString();
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000164DC File Offset: 0x000146DC
		public static InputControlExtensions.InputEventControlCollection EnumerateControls(this InputEventPtr eventPtr, InputControlExtensions.Enumerate flags, InputDevice device = null, float magnitudeThreshold = 0f)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr", "Given event pointer must not be null");
			}
			FourCC eventType = eventPtr.type;
			if (eventType != 1398030676 && eventType != 1145852993)
			{
				throw new ArgumentException(string.Format("Event must be a StateEvent or DeltaStateEvent but is a {0} instead", eventType), "eventPtr");
			}
			if (device == null)
			{
				int deviceId = eventPtr.deviceId;
				device = InputSystem.GetDeviceById(deviceId);
				if (device == null)
				{
					throw new ArgumentException(string.Format("Cannot find device with ID {0} referenced by event", deviceId), "eventPtr");
				}
			}
			return new InputControlExtensions.InputEventControlCollection
			{
				m_Device = device,
				m_EventPtr = eventPtr,
				m_Flags = flags,
				m_MagnitudeThreshold = magnitudeThreshold
			};
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000165A3 File Offset: 0x000147A3
		public static InputControlExtensions.InputEventControlCollection EnumerateChangedControls(this InputEventPtr eventPtr, InputDevice device = null, float magnitudeThreshold = 0f)
		{
			return eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInCurrentState, device, magnitudeThreshold);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000165AE File Offset: 0x000147AE
		public static bool HasButtonPress(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			return eventPtr.GetFirstButtonPressOrNull(magnitude, buttonControlsOnly) != null;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000165BC File Offset: 0x000147BC
		public static InputControl GetFirstButtonPressOrNull(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				return null;
			}
			if (magnitude < 0f)
			{
				magnitude = InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (InputControl control in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (!buttonControlsOnly || control.isButton)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00016668 File Offset: 0x00014868
		public static IEnumerable<InputControl> GetAllButtonPresses(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				yield break;
			}
			if (magnitude < 0f)
			{
				magnitude = InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (InputControl control in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (!buttonControlsOnly || control.isButton)
				{
					yield return control;
				}
			}
			InputControlExtensions.InputEventControlEnumerator inputEventControlEnumerator = default(InputControlExtensions.InputEventControlEnumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00016688 File Offset: 0x00014888
		public static InputControlExtensions.ControlBuilder Setup(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.isSetupFinished)
			{
				throw new InvalidOperationException(string.Format("The setup of {0} cannot be modified; control is already in use", control));
			}
			return new InputControlExtensions.ControlBuilder
			{
				control = control
			};
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000166D0 File Offset: 0x000148D0
		public static InputControlExtensions.DeviceBuilder Setup(this InputDevice device, int controlCount, int usageCount, int aliasCount)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.isSetupFinished)
			{
				throw new InvalidOperationException(string.Format("The setup of {0} cannot be modified; control is already in use", device));
			}
			if (controlCount < 1)
			{
				throw new ArgumentOutOfRangeException("controlCount");
			}
			if (usageCount < 0)
			{
				throw new ArgumentOutOfRangeException("usageCount");
			}
			if (aliasCount < 0)
			{
				throw new ArgumentOutOfRangeException("aliasCount");
			}
			device.m_Device = device;
			device.m_ChildrenForEachControl = new InputControl[controlCount];
			if (usageCount > 0)
			{
				device.m_UsagesForEachControl = new InternedString[usageCount];
				device.m_UsageToControl = new InputControl[usageCount];
			}
			if (aliasCount > 0)
			{
				device.m_AliasesForEachControl = new InternedString[aliasCount];
			}
			return new InputControlExtensions.DeviceBuilder
			{
				device = device
			};
		}

		// Token: 0x02000073 RID: 115
		[Flags]
		public enum Enumerate
		{
			// Token: 0x04000296 RID: 662
			IgnoreControlsInDefaultState = 1,
			// Token: 0x04000297 RID: 663
			IgnoreControlsInCurrentState = 2,
			// Token: 0x04000298 RID: 664
			IncludeSyntheticControls = 4,
			// Token: 0x04000299 RID: 665
			IncludeNoisyControls = 8,
			// Token: 0x0400029A RID: 666
			IncludeNonLeafControls = 16
		}

		// Token: 0x02000074 RID: 116
		public struct InputEventControlCollection : IEnumerable<InputControl>, IEnumerable
		{
			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x00016781 File Offset: 0x00014981
			public InputEventPtr eventPtr
			{
				get
				{
					return this.m_EventPtr;
				}
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x00016789 File Offset: 0x00014989
			public InputControlExtensions.InputEventControlEnumerator GetEnumerator()
			{
				return new InputControlExtensions.InputEventControlEnumerator(this.m_EventPtr, this.m_Device, this.m_Flags, this.m_MagnitudeThreshold);
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x000167A8 File Offset: 0x000149A8
			IEnumerator<InputControl> IEnumerable<InputControl>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x000167A8 File Offset: 0x000149A8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400029B RID: 667
			internal InputDevice m_Device;

			// Token: 0x0400029C RID: 668
			internal InputEventPtr m_EventPtr;

			// Token: 0x0400029D RID: 669
			internal InputControlExtensions.Enumerate m_Flags;

			// Token: 0x0400029E RID: 670
			internal float m_MagnitudeThreshold;
		}

		// Token: 0x02000075 RID: 117
		public struct InputEventControlEnumerator : IEnumerator<InputControl>, IEnumerator, IDisposable
		{
			// Token: 0x06000594 RID: 1428 RVA: 0x000167B8 File Offset: 0x000149B8
			internal unsafe InputEventControlEnumerator(InputEventPtr eventPtr, InputDevice device, InputControlExtensions.Enumerate flags, float magnitudeThreshold = 0f)
			{
				this.m_Device = device;
				this.m_StateOffsetToControlIndex = device.m_StateOffsetToControlMap;
				this.m_StateOffsetToControlIndexLength = this.m_StateOffsetToControlIndex.LengthSafe<uint>();
				this.m_AllControls = device.m_ChildrenForEachControl;
				this.m_EventPtr = eventPtr;
				this.m_Flags = flags;
				this.m_CurrentControl = null;
				this.m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				this.m_CurrentControlStateBitOffset = 0U;
				this.m_EventState = default(byte*);
				this.m_CurrentBitOffset = 0U;
				this.m_EndBitOffset = 0U;
				this.m_MagnitudeThreshold = magnitudeThreshold;
				if ((flags & InputControlExtensions.Enumerate.IncludeNoisyControls) == (InputControlExtensions.Enumerate)0)
				{
					this.m_NoiseMask = (byte*)device.noiseMaskPtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_NoiseMask = default(byte*);
				}
				if ((flags & InputControlExtensions.Enumerate.IgnoreControlsInDefaultState) != (InputControlExtensions.Enumerate)0)
				{
					this.m_DefaultState = (byte*)device.defaultStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_DefaultState = default(byte*);
				}
				if ((flags & InputControlExtensions.Enumerate.IgnoreControlsInCurrentState) != (InputControlExtensions.Enumerate)0)
				{
					this.m_CurrentState = (byte*)device.currentStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_CurrentState = default(byte*);
				}
				this.Reset();
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x000168C4 File Offset: 0x00014AC4
			private unsafe bool CheckDefault(uint numBits)
			{
				return MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_DefaultState, this.m_CurrentBitOffset, numBits, (void*)this.m_NoiseMask);
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x000168E4 File Offset: 0x00014AE4
			private unsafe bool CheckCurrent(uint numBits)
			{
				return MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_CurrentState, this.m_CurrentBitOffset, numBits, (void*)this.m_NoiseMask);
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00016904 File Offset: 0x00014B04
			public unsafe bool MoveNext()
			{
				if (!this.m_EventPtr.valid)
				{
					throw new ObjectDisposedException("Enumerator has already been disposed");
				}
				if (this.m_CurrentControl != null && (this.m_Flags & InputControlExtensions.Enumerate.IncludeNonLeafControls) != (InputControlExtensions.Enumerate)0)
				{
					InputControl parent = this.m_CurrentControl.parent;
					if (parent != this.m_Device)
					{
						this.m_CurrentControl = parent;
						return true;
					}
				}
				bool ignoreDefault = this.m_DefaultState != null;
				bool ignoreCurrent = this.m_CurrentState != null;
				for (;;)
				{
					this.m_CurrentControl = null;
					if (ignoreCurrent || ignoreDefault)
					{
						if ((this.m_CurrentBitOffset & 7U) != 0U)
						{
							uint bitsLeftInByte = (this.m_CurrentBitOffset + 8U) & 7U;
							if ((ignoreCurrent && this.CheckCurrent(bitsLeftInByte)) || (ignoreDefault && this.CheckDefault(bitsLeftInByte)))
							{
								this.m_CurrentBitOffset += bitsLeftInByte;
							}
						}
						while (this.m_CurrentBitOffset < this.m_EndBitOffset)
						{
							uint byteOffset = this.m_CurrentBitOffset >> 3;
							byte eventByte = this.m_EventState[byteOffset];
							int maskByte = (int)((this.m_NoiseMask != null) ? this.m_NoiseMask[byteOffset] : byte.MaxValue);
							if (ignoreCurrent && ((int)this.m_CurrentState[byteOffset] & maskByte) == ((int)eventByte & maskByte))
							{
								this.m_CurrentBitOffset += 8U;
							}
							else
							{
								if (!ignoreDefault || ((int)this.m_DefaultState[byteOffset] & maskByte) != ((int)eventByte & maskByte))
								{
									break;
								}
								this.m_CurrentBitOffset += 8U;
							}
						}
					}
					if (this.m_CurrentBitOffset >= this.m_EndBitOffset || this.m_CurrentIndexInStateOffsetToControlIndexMap >= this.m_StateOffsetToControlIndexLength)
					{
						break;
					}
					while (this.m_CurrentIndexInStateOffsetToControlIndexMap < this.m_StateOffsetToControlIndexLength)
					{
						uint controlIndex;
						uint controlBitOffset;
						uint controlBitSize;
						InputDevice.DecodeStateOffsetToControlMapEntry(this.m_StateOffsetToControlIndex[this.m_CurrentIndexInStateOffsetToControlIndexMap], out controlIndex, out controlBitOffset, out controlBitSize);
						if (controlBitOffset >= this.m_CurrentControlStateBitOffset && this.m_CurrentBitOffset < controlBitOffset + controlBitSize - this.m_CurrentControlStateBitOffset)
						{
							if (controlBitOffset - this.m_CurrentControlStateBitOffset >= this.m_CurrentBitOffset + 8U)
							{
								this.m_CurrentBitOffset = controlBitOffset - this.m_CurrentControlStateBitOffset;
								break;
							}
							if (controlBitOffset + controlBitSize - this.m_CurrentControlStateBitOffset <= this.m_EndBitOffset)
							{
								if ((controlBitOffset & 7U) == 0U && (controlBitSize & 7U) == 0U)
								{
									this.m_CurrentControl = this.m_AllControls[(int)controlIndex];
								}
								else
								{
									if ((ignoreCurrent && MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_CurrentState, controlBitOffset - this.m_CurrentControlStateBitOffset, controlBitSize, (void*)this.m_NoiseMask)) || (ignoreDefault && MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_DefaultState, controlBitOffset - this.m_CurrentControlStateBitOffset, controlBitSize, (void*)this.m_NoiseMask)))
									{
										goto IL_02BF;
									}
									this.m_CurrentControl = this.m_AllControls[(int)controlIndex];
								}
								if ((this.m_Flags & InputControlExtensions.Enumerate.IncludeNoisyControls) == (InputControlExtensions.Enumerate)0 && this.m_CurrentControl.noisy)
								{
									this.m_CurrentControl = null;
								}
								else
								{
									if ((this.m_Flags & InputControlExtensions.Enumerate.IncludeSyntheticControls) != (InputControlExtensions.Enumerate)0 || (this.m_CurrentControl.m_ControlFlags & (InputControl.ControlFlags.IsSynthetic | InputControl.ControlFlags.UsesStateFromOtherControl)) <= (InputControl.ControlFlags)0)
									{
										this.m_CurrentIndexInStateOffsetToControlIndexMap++;
										break;
									}
									this.m_CurrentControl = null;
								}
							}
						}
						IL_02BF:
						this.m_CurrentIndexInStateOffsetToControlIndexMap++;
					}
					if (this.m_CurrentControl != null)
					{
						if (this.m_MagnitudeThreshold == 0f)
						{
							return true;
						}
						byte* statePtr = this.m_EventState - (this.m_CurrentControlStateBitOffset >> 3) - this.m_Device.m_StateBlock.byteOffset;
						float magnitude = this.m_CurrentControl.EvaluateMagnitude((void*)statePtr);
						if (magnitude < 0f || magnitude >= this.m_MagnitudeThreshold)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x00016C54 File Offset: 0x00014E54
			public unsafe void Reset()
			{
				if (!this.m_EventPtr.valid)
				{
					throw new ObjectDisposedException("Enumerator has already been disposed");
				}
				FourCC eventType = this.m_EventPtr.type;
				FourCC stateFormat;
				if (eventType == 1398030676)
				{
					StateEvent* stateEvent = StateEvent.FromUnchecked(this.m_EventPtr);
					this.m_EventState = (byte*)stateEvent->state;
					this.m_EndBitOffset = stateEvent->stateSizeInBytes * 8U;
					this.m_CurrentBitOffset = 0U;
					stateFormat = stateEvent->stateFormat;
				}
				else
				{
					if (!(eventType == 1145852993))
					{
						throw new NotSupportedException(string.Format("Cannot iterate over controls in event of type '{0}'", eventType));
					}
					DeltaStateEvent* deltaEvent = DeltaStateEvent.FromUnchecked(this.m_EventPtr);
					this.m_EventState = (byte*)deltaEvent->deltaState - deltaEvent->stateOffset;
					this.m_CurrentBitOffset = deltaEvent->stateOffset * 8U;
					this.m_EndBitOffset = this.m_CurrentBitOffset + deltaEvent->deltaStateSizeInBytes * 8U;
					stateFormat = deltaEvent->stateFormat;
				}
				this.m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				this.m_CurrentControlStateBitOffset = 0U;
				this.m_CurrentControl = null;
				if (stateFormat != this.m_Device.m_StateBlock.format)
				{
					uint stateOffset = 0U;
					if (this.m_Device.hasStateCallbacks && ((IInputStateCallbackReceiver)this.m_Device).GetStateOffsetForEvent(null, this.m_EventPtr, ref stateOffset))
					{
						this.m_CurrentControlStateBitOffset = stateOffset * 8U;
						if (this.m_CurrentState != null)
						{
							this.m_CurrentState += stateOffset;
						}
						if (this.m_DefaultState != null)
						{
							this.m_DefaultState += stateOffset;
						}
						if (this.m_NoiseMask != null)
						{
							this.m_NoiseMask += stateOffset;
							return;
						}
					}
					else if (!(this.m_Device is Touchscreen) || !this.m_EventPtr.IsA<StateEvent>() || !(StateEvent.FromUnchecked(this.m_EventPtr)->stateFormat == TouchState.Format))
					{
						throw new InvalidOperationException(string.Format("{0} event with state format {1} cannot be used with device '{2}'", eventType, stateFormat, this.m_Device));
					}
				}
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x00016E4D File Offset: 0x0001504D
			public void Dispose()
			{
				this.m_EventPtr = default(InputEventPtr);
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600059A RID: 1434 RVA: 0x00016E5B File Offset: 0x0001505B
			public InputControl Current
			{
				get
				{
					return this.m_CurrentControl;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x00016E63 File Offset: 0x00015063
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400029F RID: 671
			private InputControlExtensions.Enumerate m_Flags;

			// Token: 0x040002A0 RID: 672
			private readonly InputDevice m_Device;

			// Token: 0x040002A1 RID: 673
			private readonly uint[] m_StateOffsetToControlIndex;

			// Token: 0x040002A2 RID: 674
			private readonly int m_StateOffsetToControlIndexLength;

			// Token: 0x040002A3 RID: 675
			private readonly InputControl[] m_AllControls;

			// Token: 0x040002A4 RID: 676
			private unsafe byte* m_DefaultState;

			// Token: 0x040002A5 RID: 677
			private unsafe byte* m_CurrentState;

			// Token: 0x040002A6 RID: 678
			private unsafe byte* m_NoiseMask;

			// Token: 0x040002A7 RID: 679
			private InputEventPtr m_EventPtr;

			// Token: 0x040002A8 RID: 680
			private InputControl m_CurrentControl;

			// Token: 0x040002A9 RID: 681
			private int m_CurrentIndexInStateOffsetToControlIndexMap;

			// Token: 0x040002AA RID: 682
			private uint m_CurrentControlStateBitOffset;

			// Token: 0x040002AB RID: 683
			private unsafe byte* m_EventState;

			// Token: 0x040002AC RID: 684
			private uint m_CurrentBitOffset;

			// Token: 0x040002AD RID: 685
			private uint m_EndBitOffset;

			// Token: 0x040002AE RID: 686
			private float m_MagnitudeThreshold;
		}

		// Token: 0x02000076 RID: 118
		public struct ControlBuilder
		{
			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x00016E6B File Offset: 0x0001506B
			// (set) Token: 0x0600059D RID: 1437 RVA: 0x00016E73 File Offset: 0x00015073
			public InputControl control { readonly get; internal set; }

			// Token: 0x0600059E RID: 1438 RVA: 0x00016E7C File Offset: 0x0001507C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder At(InputDevice device, int index)
			{
				device.m_ChildrenForEachControl[index] = this.control;
				this.control.m_Device = device;
				return this;
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00016E9E File Offset: 0x0001509E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithParent(InputControl parent)
			{
				this.control.m_Parent = parent;
				return this;
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00016EB2 File Offset: 0x000150B2
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithName(string name)
			{
				this.control.m_Name = new InternedString(name);
				return this;
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x00016ECB File Offset: 0x000150CB
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithDisplayName(string displayName)
			{
				this.control.m_DisplayNameFromLayout = new InternedString(displayName);
				return this;
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00016EE9 File Offset: 0x000150E9
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithShortDisplayName(string shortDisplayName)
			{
				this.control.m_ShortDisplayNameFromLayout = new InternedString(shortDisplayName);
				return this;
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x00016F07 File Offset: 0x00015107
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithLayout(InternedString layout)
			{
				this.control.m_Layout = layout;
				return this;
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x00016F1B File Offset: 0x0001511B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithUsages(int startIndex, int count)
			{
				this.control.m_UsageStartIndex = startIndex;
				this.control.m_UsageCount = count;
				return this;
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00016F3B File Offset: 0x0001513B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithAliases(int startIndex, int count)
			{
				this.control.m_AliasStartIndex = startIndex;
				this.control.m_AliasCount = count;
				return this;
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00016F5B File Offset: 0x0001515B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithChildren(int startIndex, int count)
			{
				this.control.m_ChildStartIndex = startIndex;
				this.control.m_ChildCount = count;
				return this;
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x00016F7B File Offset: 0x0001517B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithStateBlock(InputStateBlock stateBlock)
			{
				this.control.m_StateBlock = stateBlock;
				return this;
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00016F8F File Offset: 0x0001518F
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithDefaultState(PrimitiveValue value)
			{
				this.control.m_DefaultState = value;
				this.control.m_Device.hasControlsWithDefaultState = true;
				return this;
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00016FB4 File Offset: 0x000151B4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithMinAndMax(PrimitiveValue min, PrimitiveValue max)
			{
				this.control.m_MinValue = min;
				this.control.m_MaxValue = max;
				return this;
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00016FD4 File Offset: 0x000151D4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithProcessor<TProcessor, TValue>(TProcessor processor) where TProcessor : InputProcessor<TValue> where TValue : struct
			{
				((InputControl<TValue>)this.control).m_ProcessorStack.Append(processor);
				return this;
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x00016FF8 File Offset: 0x000151F8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsNoisy(bool value)
			{
				this.control.noisy = value;
				return this;
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x0001700C File Offset: 0x0001520C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsSynthetic(bool value)
			{
				this.control.synthetic = value;
				return this;
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00017020 File Offset: 0x00015220
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder DontReset(bool value)
			{
				this.control.dontReset = value;
				if (value)
				{
					this.control.m_Device.hasDontResetControls = true;
				}
				return this;
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x00017048 File Offset: 0x00015248
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsButton(bool value)
			{
				this.control.isButton = value;
				return this;
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0001705C File Offset: 0x0001525C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				this.control.isSetupFinished = true;
			}
		}

		// Token: 0x02000077 RID: 119
		public struct DeviceBuilder
		{
			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001706A File Offset: 0x0001526A
			// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00017072 File Offset: 0x00015272
			public InputDevice device { readonly get; internal set; }

			// Token: 0x060005B2 RID: 1458 RVA: 0x0001707B File Offset: 0x0001527B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithName(string name)
			{
				this.device.m_Name = new InternedString(name);
				return this;
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x00017094 File Offset: 0x00015294
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithDisplayName(string displayName)
			{
				this.device.m_DisplayNameFromLayout = new InternedString(displayName);
				return this;
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x000170B2 File Offset: 0x000152B2
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithShortDisplayName(string shortDisplayName)
			{
				this.device.m_ShortDisplayNameFromLayout = new InternedString(shortDisplayName);
				return this;
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x000170D0 File Offset: 0x000152D0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithLayout(InternedString layout)
			{
				this.device.m_Layout = layout;
				return this;
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x000170E4 File Offset: 0x000152E4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithChildren(int startIndex, int count)
			{
				this.device.m_ChildStartIndex = startIndex;
				this.device.m_ChildCount = count;
				return this;
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x00017104 File Offset: 0x00015304
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithStateBlock(InputStateBlock stateBlock)
			{
				this.device.m_StateBlock = stateBlock;
				return this;
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x00017118 File Offset: 0x00015318
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder IsNoisy(bool value)
			{
				this.device.noisy = value;
				return this;
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0001712C File Offset: 0x0001532C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithControlUsage(int controlIndex, InternedString usage, InputControl control)
			{
				this.device.m_UsagesForEachControl[controlIndex] = usage;
				this.device.m_UsageToControl[controlIndex] = control;
				return this;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x00017154 File Offset: 0x00015354
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithControlAlias(int controlIndex, InternedString alias)
			{
				this.device.m_AliasesForEachControl[controlIndex] = alias;
				return this;
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0001716E File Offset: 0x0001536E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithStateOffsetToControlIndexMap(uint[] map)
			{
				this.device.m_StateOffsetToControlMap = map;
				return this;
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x00017184 File Offset: 0x00015384
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe InputControlExtensions.DeviceBuilder WithControlTree(byte[] controlTreeNodes, ushort[] controlTreeIndicies)
			{
				int sizeOfNode = UnsafeUtility.SizeOf<InputDevice.ControlBitRangeNode>();
				int numNodes = controlTreeNodes.Length / sizeOfNode;
				this.device.m_ControlTreeNodes = new InputDevice.ControlBitRangeNode[numNodes];
				fixed (byte[] array = controlTreeNodes)
				{
					byte* nodePtr;
					if (controlTreeNodes == null || array.Length == 0)
					{
						nodePtr = null;
					}
					else
					{
						nodePtr = &array[0];
					}
					for (int i = 0; i < numNodes; i++)
					{
						this.device.m_ControlTreeNodes[i] = *(InputDevice.ControlBitRangeNode*)(nodePtr + i * sizeOfNode);
					}
				}
				this.device.m_ControlTreeIndices = controlTreeIndicies;
				return this;
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x00017208 File Offset: 0x00015408
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				int i = 0;
				using (ReadOnlyArray<InputControl>.Enumerator enumerator = this.device.allControls.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is ButtonControl)
						{
							i++;
						}
					}
				}
				this.device.m_ButtonControlsCheckingPressState = new List<ButtonControl>(i);
				this.device.m_UpdatedButtons = new HashSet<int>(i);
				this.device.isSetupFinished = true;
			}
		}
	}
}
