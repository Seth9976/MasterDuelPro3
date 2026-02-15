using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001DA RID: 474
	public class InputStateHistory : IDisposable, IEnumerable<InputStateHistory.Record>, IEnumerable, IInputStateChangeMonitor
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0005346E File Offset: 0x0005166E
		public int Count
		{
			get
			{
				return this.m_RecordCount;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00053476 File Offset: 0x00051676
		public uint version
		{
			get
			{
				return this.m_CurrentVersion;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0005347E File Offset: 0x0005167E
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00053486 File Offset: 0x00051686
		public int historyDepth
		{
			get
			{
				return this.m_HistoryDepth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("History depth cannot be negative", "value");
				}
				if (this.m_RecordBuffer.IsCreated)
				{
					throw new NotImplementedException();
				}
				this.m_HistoryDepth = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x000534B6 File Offset: 0x000516B6
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x000534BE File Offset: 0x000516BE
		public int extraMemoryPerRecord
		{
			get
			{
				return this.m_ExtraMemoryPerRecord;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Memory size cannot be negative", "value");
				}
				if (this.m_RecordBuffer.IsCreated)
				{
					throw new NotImplementedException();
				}
				this.m_ExtraMemoryPerRecord = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x000534F0 File Offset: 0x000516F0
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x00053522 File Offset: 0x00051722
		public InputUpdateType updateMask
		{
			get
			{
				InputUpdateType? updateMask = this.m_UpdateMask;
				if (updateMask == null)
				{
					return InputSystem.s_Manager.updateMask & ~InputUpdateType.Editor;
				}
				return updateMask.GetValueOrDefault();
			}
			set
			{
				if (value == InputUpdateType.None)
				{
					throw new ArgumentException("'InputUpdateType.None' is not a valid update mask", "value");
				}
				this.m_UpdateMask = new InputUpdateType?(value);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00053543 File Offset: 0x00051743
		public ReadOnlyArray<InputControl> controls
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_Controls, 0, this.m_ControlCount);
			}
		}

		// Token: 0x1700050E RID: 1294
		public InputStateHistory.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_RecordCount)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.m_RecordCount), "index");
				}
				int recordIndex = this.UserIndexToRecordIndex(index);
				return new InputStateHistory.Record(this, recordIndex, this.GetRecord(recordIndex));
			}
			set
			{
				if (index < 0 || index >= this.m_RecordCount)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.m_RecordCount), "index");
				}
				int recordIndex = this.UserIndexToRecordIndex(index);
				new InputStateHistory.Record(this, recordIndex, this.GetRecord(recordIndex)).CopyFrom(value);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0005360F File Offset: 0x0005180F
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00053617 File Offset: 0x00051817
		public Action<InputStateHistory.Record> onRecordAdded { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00053620 File Offset: 0x00051820
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00053628 File Offset: 0x00051828
		public Func<InputControl, double, InputEventPtr, bool> onShouldRecordStateChange { get; set; }

		// Token: 0x060011A6 RID: 4518 RVA: 0x00053631 File Offset: 0x00051831
		public InputStateHistory(int maxStateSizeInBytes)
		{
			if (maxStateSizeInBytes <= 0)
			{
				throw new ArgumentException("State size must be >= 0", "maxStateSizeInBytes");
			}
			this.m_AddNewControls = true;
			this.m_StateSizeInBytes = maxStateSizeInBytes.AlignToMultipleOf(4);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0005366C File Offset: 0x0005186C
		public InputStateHistory(string path)
		{
			using (InputControlList<InputControl> controls = InputSystem.FindControls(path))
			{
				this.m_Controls = controls.ToArray(false);
				this.m_ControlCount = this.m_Controls.Length;
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000536D0 File Offset: 0x000518D0
		public InputStateHistory(InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.m_Controls = new InputControl[] { control };
			this.m_ControlCount = 1;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00053708 File Offset: 0x00051908
		public InputStateHistory(IEnumerable<InputControl> controls)
		{
			if (controls != null)
			{
				this.m_Controls = controls.ToArray<InputControl>();
				this.m_ControlCount = this.m_Controls.Length;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00053738 File Offset: 0x00051938
		~InputStateHistory()
		{
			this.Dispose();
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00053764 File Offset: 0x00051964
		public void Clear()
		{
			this.m_HeadIndex = 0;
			this.m_RecordCount = 0;
			this.m_CurrentVersion += 1U;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00053784 File Offset: 0x00051984
		public unsafe InputStateHistory.Record AddRecord(InputStateHistory.Record record)
		{
			int index;
			InputStateHistory.RecordHeader* recordPtr = this.AllocateRecord(out index);
			InputStateHistory.Record newRecord = new InputStateHistory.Record(this, index, recordPtr);
			newRecord.CopyFrom(record);
			return newRecord;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000537B0 File Offset: 0x000519B0
		public void StartRecording()
		{
			foreach (InputControl inputControl in this.controls)
			{
				InputState.AddChangeMonitor(inputControl, this, -1L, 0U);
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00053808 File Offset: 0x00051A08
		public void StopRecording()
		{
			foreach (InputControl inputControl in this.controls)
			{
				InputState.RemoveChangeMonitor(inputControl, this, -1L);
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00053860 File Offset: 0x00051A60
		public unsafe InputStateHistory.Record RecordStateChange(InputControl control, InputEventPtr eventPtr)
		{
			if (eventPtr.IsA<DeltaStateEvent>())
			{
				throw new NotImplementedException();
			}
			if (!eventPtr.IsA<StateEvent>())
			{
				throw new ArgumentException(string.Format("Event must be a state event but is '{0}' instead", eventPtr), "eventPtr");
			}
			byte* statePtr = (byte*)StateEvent.From(eventPtr)->state - control.device.stateBlock.byteOffset;
			return this.RecordStateChange(control, (void*)statePtr, eventPtr.time);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000538D0 File Offset: 0x00051AD0
		public unsafe InputStateHistory.Record RecordStateChange(InputControl control, void* statePtr, double time)
		{
			int controlIndex = this.m_Controls.IndexOfReference(control, this.m_ControlCount);
			if (controlIndex == -1)
			{
				if (!this.m_AddNewControls)
				{
					throw new ArgumentException(string.Format("Control '{0}' is not part of InputStateHistory", control), "control");
				}
				if ((ulong)control.stateBlock.alignedSizeInBytes > (ulong)((long)this.m_StateSizeInBytes))
				{
					throw new InvalidOperationException(string.Format("Cannot add control '{0}' with state larger than {1} bytes", control, this.m_StateSizeInBytes));
				}
				controlIndex = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.m_Controls, ref this.m_ControlCount, control, 10);
			}
			int index;
			InputStateHistory.RecordHeader* recordPtr = this.AllocateRecord(out index);
			recordPtr->time = time;
			ref InputStateHistory.RecordHeader ptr = ref *recordPtr;
			uint num = this.m_CurrentVersion + 1U;
			this.m_CurrentVersion = num;
			ptr.version = num;
			byte* stateBufferPtr = recordPtr->statePtrWithoutControlIndex;
			if (this.m_ControlCount > 1 || this.m_AddNewControls)
			{
				recordPtr->controlIndex = controlIndex;
				stateBufferPtr = recordPtr->statePtrWithControlIndex;
			}
			uint stateSize = control.stateBlock.alignedSizeInBytes;
			uint stateOffset = control.stateBlock.byteOffset;
			UnsafeUtility.MemCpy((void*)stateBufferPtr, (void*)((byte*)statePtr + stateOffset), (long)((ulong)stateSize));
			InputStateHistory.Record record = new InputStateHistory.Record(this, index, recordPtr);
			Action<InputStateHistory.Record> onRecordAdded = this.onRecordAdded;
			if (onRecordAdded != null)
			{
				onRecordAdded(record);
			}
			return record;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000539FD File Offset: 0x00051BFD
		public IEnumerator<InputStateHistory.Record> GetEnumerator()
		{
			return new InputStateHistory.Enumerator(this);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00053A0A File Offset: 0x00051C0A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00053A12 File Offset: 0x00051C12
		public void Dispose()
		{
			this.StopRecording();
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00053A26 File Offset: 0x00051C26
		protected void Destroy()
		{
			if (this.m_RecordBuffer.IsCreated)
			{
				this.m_RecordBuffer.Dispose();
				this.m_RecordBuffer = default(NativeArray<byte>);
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00053A4C File Offset: 0x00051C4C
		private void Allocate()
		{
			if (!this.m_AddNewControls)
			{
				this.m_StateSizeInBytes = 0;
				foreach (InputControl control in this.controls)
				{
					this.m_StateSizeInBytes = (int)Math.Max((uint)this.m_StateSizeInBytes, control.stateBlock.alignedSizeInBytes);
				}
			}
			int totalSizeOfBuffer = this.bytesPerRecord * this.m_HistoryDepth;
			this.m_RecordBuffer = new NativeArray<byte>(totalSizeOfBuffer, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00053AE8 File Offset: 0x00051CE8
		protected internal int RecordIndexToUserIndex(int index)
		{
			if (index < this.m_HeadIndex)
			{
				return this.m_HistoryDepth - this.m_HeadIndex + index;
			}
			return index - this.m_HeadIndex;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00053B0B File Offset: 0x00051D0B
		protected internal int UserIndexToRecordIndex(int index)
		{
			return (this.m_HeadIndex + index) % this.m_HistoryDepth;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00053B1C File Offset: 0x00051D1C
		protected internal unsafe InputStateHistory.RecordHeader* GetRecord(int index)
		{
			if (!this.m_RecordBuffer.IsCreated)
			{
				throw new InvalidOperationException("History buffer has been disposed");
			}
			if (index < 0 || index >= this.m_HistoryDepth)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.GetRecordUnchecked(index);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00053B55 File Offset: 0x00051D55
		internal unsafe InputStateHistory.RecordHeader* GetRecordUnchecked(int index)
		{
			return (InputStateHistory.RecordHeader*)((byte*)this.m_RecordBuffer.GetUnsafePtr<byte>() + index * this.bytesPerRecord);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00053B6C File Offset: 0x00051D6C
		protected internal unsafe InputStateHistory.RecordHeader* AllocateRecord(out int index)
		{
			if (!this.m_RecordBuffer.IsCreated)
			{
				this.Allocate();
			}
			index = (this.m_HeadIndex + this.m_RecordCount) % this.m_HistoryDepth;
			if (this.m_RecordCount == this.m_HistoryDepth)
			{
				this.m_HeadIndex = (this.m_HeadIndex + 1) % this.m_HistoryDepth;
			}
			else
			{
				this.m_RecordCount++;
			}
			return (InputStateHistory.RecordHeader*)((byte*)this.m_RecordBuffer.GetUnsafePtr<byte>() + this.bytesPerRecord * index);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00053BEC File Offset: 0x00051DEC
		protected unsafe TValue ReadValue<TValue>(InputStateHistory.RecordHeader* data) where TValue : struct
		{
			bool flag = this.m_ControlCount == 1 && !this.m_AddNewControls;
			InputControl control = (flag ? this.controls[0] : this.controls[data->controlIndex]);
			InputControl<TValue> controlOfType = control as InputControl<TValue>;
			if (controlOfType == null)
			{
				throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from control '{1}' with value type '{2}'", typeof(TValue).GetNiceTypeName(), control, control.valueType.GetNiceTypeName()));
			}
			byte* statePtr = (flag ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			statePtr -= control.stateBlock.byteOffset;
			return controlOfType.ReadValueFromState((void*)statePtr);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00053C98 File Offset: 0x00051E98
		protected unsafe object ReadValueAsObject(InputStateHistory.RecordHeader* data)
		{
			bool flag = this.m_ControlCount == 1 && !this.m_AddNewControls;
			InputControl control = (flag ? this.controls[0] : this.controls[data->controlIndex]);
			byte* statePtr = (flag ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			statePtr -= control.stateBlock.byteOffset;
			return control.ReadValueFromStateAsObject((void*)statePtr);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00053D10 File Offset: 0x00051F10
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
		{
			bool currentUpdateType = InputState.currentUpdateType != InputUpdateType.None;
			InputUpdateType updateTypeMask = this.updateMask;
			if (((currentUpdateType ? InputUpdateType.Dynamic : InputUpdateType.None) & updateTypeMask) == InputUpdateType.None)
			{
				return;
			}
			if (this.onShouldRecordStateChange != null && !this.onShouldRecordStateChange(control, time, eventPtr))
			{
				return;
			}
			this.RecordStateChange(control, control.currentStatePtr, time);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000049FE File Offset: 0x00002BFE
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00053D56 File Offset: 0x00051F56
		internal int bytesPerRecord
		{
			get
			{
				return (this.m_StateSizeInBytes + this.m_ExtraMemoryPerRecord + ((this.m_ControlCount == 1 && !this.m_AddNewControls) ? 12 : 16)).AlignToMultipleOf(4);
			}
		}

		// Token: 0x04000AA7 RID: 2727
		private const int kDefaultHistorySize = 128;

		// Token: 0x04000AAA RID: 2730
		internal InputControl[] m_Controls;

		// Token: 0x04000AAB RID: 2731
		internal int m_ControlCount;

		// Token: 0x04000AAC RID: 2732
		private NativeArray<byte> m_RecordBuffer;

		// Token: 0x04000AAD RID: 2733
		private int m_StateSizeInBytes;

		// Token: 0x04000AAE RID: 2734
		private int m_RecordCount;

		// Token: 0x04000AAF RID: 2735
		private int m_HistoryDepth = 128;

		// Token: 0x04000AB0 RID: 2736
		private int m_ExtraMemoryPerRecord;

		// Token: 0x04000AB1 RID: 2737
		internal int m_HeadIndex;

		// Token: 0x04000AB2 RID: 2738
		internal uint m_CurrentVersion;

		// Token: 0x04000AB3 RID: 2739
		private InputUpdateType? m_UpdateMask;

		// Token: 0x04000AB4 RID: 2740
		internal readonly bool m_AddNewControls;

		// Token: 0x020001DB RID: 475
		private struct Enumerator : IEnumerator<InputStateHistory.Record>, IEnumerator, IDisposable
		{
			// Token: 0x060011C0 RID: 4544 RVA: 0x00053D83 File Offset: 0x00051F83
			public Enumerator(InputStateHistory history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x060011C1 RID: 4545 RVA: 0x00053D93 File Offset: 0x00051F93
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x060011C2 RID: 4546 RVA: 0x00053DBB File Offset: 0x00051FBB
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00053DC4 File Offset: 0x00051FC4
			public InputStateHistory.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00053DD7 File Offset: 0x00051FD7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060011C5 RID: 4549 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x04000AB5 RID: 2741
			private readonly InputStateHistory m_History;

			// Token: 0x04000AB6 RID: 2742
			private int m_Index;
		}

		// Token: 0x020001DC RID: 476
		[StructLayout(LayoutKind.Explicit)]
		protected internal struct RecordHeader
		{
			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00053DE4 File Offset: 0x00051FE4
			public unsafe byte* statePtrWithControlIndex
			{
				get
				{
					fixed (byte* ptr = &this.m_StateWithControlIndex.FixedElementField)
					{
						return ptr;
					}
				}
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00053E00 File Offset: 0x00052000
			public unsafe byte* statePtrWithoutControlIndex
			{
				get
				{
					fixed (byte* ptr = &this.m_StateWithoutControlIndex.FixedElementField)
					{
						return ptr;
					}
				}
			}

			// Token: 0x04000AB7 RID: 2743
			[FieldOffset(0)]
			public double time;

			// Token: 0x04000AB8 RID: 2744
			[FieldOffset(8)]
			public uint version;

			// Token: 0x04000AB9 RID: 2745
			[FieldOffset(12)]
			public int controlIndex;

			// Token: 0x04000ABA RID: 2746
			[FixedBuffer(typeof(byte), 1)]
			[FieldOffset(12)]
			private InputStateHistory.RecordHeader.<m_StateWithoutControlIndex>e__FixedBuffer m_StateWithoutControlIndex;

			// Token: 0x04000ABB RID: 2747
			[FixedBuffer(typeof(byte), 1)]
			[FieldOffset(16)]
			private InputStateHistory.RecordHeader.<m_StateWithControlIndex>e__FixedBuffer m_StateWithControlIndex;

			// Token: 0x04000ABC RID: 2748
			public const int kSizeWithControlIndex = 16;

			// Token: 0x04000ABD RID: 2749
			public const int kSizeWithoutControlIndex = 12;

			// Token: 0x020001DD RID: 477
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1)]
			public struct <m_StateWithControlIndex>e__FixedBuffer
			{
				// Token: 0x04000ABE RID: 2750
				public byte FixedElementField;
			}

			// Token: 0x020001DE RID: 478
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1)]
			public struct <m_StateWithoutControlIndex>e__FixedBuffer
			{
				// Token: 0x04000ABF RID: 2751
				public byte FixedElementField;
			}
		}

		// Token: 0x020001DF RID: 479
		public struct Record : IEquatable<InputStateHistory.Record>
		{
			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00053E1B File Offset: 0x0005201B
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00053E2E File Offset: 0x0005202E
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060011CA RID: 4554 RVA: 0x00053E38 File Offset: 0x00052038
			internal uint version
			{
				get
				{
					return this.m_Version;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060011CB RID: 4555 RVA: 0x00053E40 File Offset: 0x00052040
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060011CC RID: 4556 RVA: 0x00053E67 File Offset: 0x00052067
			public InputStateHistory owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x060011CD RID: 4557 RVA: 0x00053E6F File Offset: 0x0005206F
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060011CE RID: 4558 RVA: 0x00053E88 File Offset: 0x00052088
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x060011CF RID: 4559 RVA: 0x00053E9C File Offset: 0x0005209C
			public unsafe InputControl control
			{
				get
				{
					this.CheckValid();
					ReadOnlyArray<InputControl> controls = this.m_Owner.controls;
					if (controls.Count == 1 && !this.m_Owner.m_AddNewControls)
					{
						return controls[0];
					}
					return controls[this.header->controlIndex];
				}
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00053EF0 File Offset: 0x000520F0
			public InputStateHistory.Record next
			{
				get
				{
					this.CheckValid();
					int userIndex = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (userIndex + 1 >= this.m_Owner.Count)
					{
						return default(InputStateHistory.Record);
					}
					int recordIndex = this.m_Owner.UserIndexToRecordIndex(userIndex + 1);
					return new InputStateHistory.Record(this.m_Owner, recordIndex, this.m_Owner.GetRecord(recordIndex));
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00053F58 File Offset: 0x00052158
			public InputStateHistory.Record previous
			{
				get
				{
					this.CheckValid();
					int userIndex = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (userIndex - 1 < 0)
					{
						return default(InputStateHistory.Record);
					}
					int recordIndex = this.m_Owner.UserIndexToRecordIndex(userIndex - 1);
					return new InputStateHistory.Record(this.m_Owner, recordIndex, this.m_Owner.GetRecord(recordIndex));
				}
			}

			// Token: 0x060011D2 RID: 4562 RVA: 0x00053FB4 File Offset: 0x000521B4
			internal unsafe Record(InputStateHistory owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x00053FD2 File Offset: 0x000521D2
			public TValue ReadValue<TValue>() where TValue : struct
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x060011D4 RID: 4564 RVA: 0x00053FEB File Offset: 0x000521EB
			public object ReadValueAsObject()
			{
				this.CheckValid();
				return this.m_Owner.ReadValueAsObject(this.header);
			}

			// Token: 0x060011D5 RID: 4565 RVA: 0x00054004 File Offset: 0x00052204
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x060011D6 RID: 4566 RVA: 0x00054014 File Offset: 0x00052214
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x060011D7 RID: 4567 RVA: 0x0005405B File Offset: 0x0005225B
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x060011D8 RID: 4568 RVA: 0x00054069 File Offset: 0x00052269
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x000540A4 File Offset: 0x000522A4
			public unsafe void CopyFrom(InputStateHistory.Record record)
			{
				if (!record.valid)
				{
					throw new ArgumentException("Given history record is not valid", "record");
				}
				this.CheckValid();
				InputControl control = record.control;
				int controlIndex = this.m_Owner.controls.IndexOfReference(control);
				if (controlIndex == -1)
				{
					if (!this.m_Owner.m_AddNewControls)
					{
						throw new InvalidOperationException(string.Format("Control '{0}' is not tracked by target history", record.control));
					}
					controlIndex = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.m_Owner.m_Controls, ref this.m_Owner.m_ControlCount, control, 10);
				}
				int numBytesForState = this.m_Owner.m_StateSizeInBytes;
				if (numBytesForState != record.m_Owner.m_StateSizeInBytes)
				{
					throw new InvalidOperationException(string.Format("Cannot copy record from owner with state size '{0}' to owner with state size '{1}'", record.m_Owner.m_StateSizeInBytes, numBytesForState));
				}
				InputStateHistory.RecordHeader* thisRecordPtr = this.header;
				InputStateHistory.RecordHeader* otherRecordPtr = record.header;
				UnsafeUtility.MemCpy((void*)thisRecordPtr, (void*)otherRecordPtr, 12L);
				ref InputStateHistory.RecordHeader ptr = ref *thisRecordPtr;
				InputStateHistory owner = this.m_Owner;
				uint num = owner.m_CurrentVersion + 1U;
				owner.m_CurrentVersion = num;
				ptr.version = num;
				this.m_Version = thisRecordPtr->version;
				byte* dstPtr = thisRecordPtr->statePtrWithoutControlIndex;
				if (this.m_Owner.controls.Count > 1 || this.m_Owner.m_AddNewControls)
				{
					thisRecordPtr->controlIndex = controlIndex;
					dstPtr = thisRecordPtr->statePtrWithControlIndex;
				}
				byte* srcPtr = ((record.m_Owner.m_ControlCount > 1 || record.m_Owner.m_AddNewControls) ? otherRecordPtr->statePtrWithControlIndex : otherRecordPtr->statePtrWithoutControlIndex);
				UnsafeUtility.MemCpy((void*)dstPtr, (void*)srcPtr, (long)numBytesForState);
				int numBytesExtraMemory = this.m_Owner.m_ExtraMemoryPerRecord;
				if (numBytesExtraMemory > 0 && numBytesExtraMemory == record.m_Owner.m_ExtraMemoryPerRecord)
				{
					UnsafeUtility.MemCpy(this.GetUnsafeExtraMemoryPtr(), record.GetUnsafeExtraMemoryPtr(), (long)numBytesExtraMemory);
				}
				Action<InputStateHistory.Record> onRecordAdded = this.m_Owner.onRecordAdded;
				if (onRecordAdded == null)
				{
					return;
				}
				onRecordAdded(this);
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0005427B File Offset: 0x0005247B
			internal unsafe void CheckValid()
			{
				if (this.m_Owner == null || this.m_IndexPlusOne == 0)
				{
					throw new InvalidOperationException("Value not initialized");
				}
				if (this.header->version != this.m_Version)
				{
					throw new InvalidOperationException("Record is no longer valid");
				}
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x000542B6 File Offset: 0x000524B6
			public bool Equals(InputStateHistory.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x000542E4 File Offset: 0x000524E4
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory.Record)
				{
					InputStateHistory.Record other = (InputStateHistory.Record)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x060011DD RID: 4573 RVA: 0x00054309 File Offset: 0x00052509
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x060011DE RID: 4574 RVA: 0x0005433B File Offset: 0x0005253B
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValueAsObject(), this.time);
			}

			// Token: 0x04000AC0 RID: 2752
			private readonly InputStateHistory m_Owner;

			// Token: 0x04000AC1 RID: 2753
			private readonly int m_IndexPlusOne;

			// Token: 0x04000AC2 RID: 2754
			private uint m_Version;
		}
	}
}
