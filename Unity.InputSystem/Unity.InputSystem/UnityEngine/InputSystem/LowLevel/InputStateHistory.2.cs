using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001E0 RID: 480
	public class InputStateHistory<TValue> : InputStateHistory, IReadOnlyList<InputStateHistory<TValue>.Record>, IEnumerable<InputStateHistory<TValue>.Record>, IEnumerable, IReadOnlyCollection<InputStateHistory<TValue>.Record> where TValue : struct
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x0005436C File Offset: 0x0005256C
		public InputStateHistory(int? maxStateSizeInBytes = null)
			: base(maxStateSizeInBytes ?? UnsafeUtility.SizeOf<TValue>())
		{
			int? num = maxStateSizeInBytes;
			int num2 = UnsafeUtility.SizeOf<TValue>();
			if ((num.GetValueOrDefault() < num2) & (num != null))
			{
				throw new ArgumentException("Max state size cannot be smaller than sizeof(TValue)", "maxStateSizeInBytes");
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000543C4 File Offset: 0x000525C4
		public InputStateHistory(InputControl<TValue> control)
			: base(control)
		{
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000543D0 File Offset: 0x000525D0
		public InputStateHistory(string path)
			: base(path)
		{
			foreach (InputControl control in base.controls)
			{
				if (!typeof(TValue).IsAssignableFrom(control.valueType))
				{
					throw new ArgumentException(string.Format("Control '{0}' matched by '{1}' has value type '{2}' which is incompatible with '{3}'", new object[]
					{
						control,
						path,
						control.valueType.GetNiceTypeName(),
						typeof(TValue).GetNiceTypeName()
					}));
				}
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0005447C File Offset: 0x0005267C
		~InputStateHistory()
		{
			base.Destroy();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000544A8 File Offset: 0x000526A8
		public unsafe InputStateHistory<TValue>.Record AddRecord(InputStateHistory<TValue>.Record record)
		{
			int index;
			InputStateHistory.RecordHeader* recordPtr = base.AllocateRecord(out index);
			InputStateHistory<TValue>.Record newRecord = new InputStateHistory<TValue>.Record(this, index, recordPtr);
			newRecord.CopyFrom(record);
			return newRecord;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000544D4 File Offset: 0x000526D4
		public unsafe InputStateHistory<TValue>.Record RecordStateChange(InputControl<TValue> control, TValue value, double time = -1.0)
		{
			InputEventPtr eventPtr;
			InputStateHistory<TValue>.Record record2;
			using (StateEvent.From(control.device, out eventPtr, Allocator.Temp))
			{
				byte* statePtr = (byte*)StateEvent.From(eventPtr)->state - control.device.stateBlock.byteOffset;
				control.WriteValueIntoState(value, (void*)statePtr);
				if (time >= 0.0)
				{
					eventPtr.time = time;
				}
				InputStateHistory.Record record = base.RecordStateChange(control, eventPtr);
				record2 = new InputStateHistory<TValue>.Record(this, record.recordIndex, record.header);
			}
			return record2;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00054570 File Offset: 0x00052770
		public new IEnumerator<InputStateHistory<TValue>.Record> GetEnumerator()
		{
			return new InputStateHistory<TValue>.Enumerator(this);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0005457D File Offset: 0x0005277D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000520 RID: 1312
		public InputStateHistory<TValue>.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, base.Count), "index");
				}
				int recordIndex = base.UserIndexToRecordIndex(index);
				return new InputStateHistory<TValue>.Record(this, recordIndex, base.GetRecord(recordIndex));
			}
			set
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, base.Count), "index");
				}
				int recordIndex = base.UserIndexToRecordIndex(index);
				new InputStateHistory<TValue>.Record(this, recordIndex, base.GetRecord(recordIndex)).CopyFrom(value);
			}
		}

		// Token: 0x020001E1 RID: 481
		private struct Enumerator : IEnumerator<InputStateHistory<TValue>.Record>, IEnumerator, IDisposable
		{
			// Token: 0x060011E9 RID: 4585 RVA: 0x0005463F File Offset: 0x0005283F
			public Enumerator(InputStateHistory<TValue> history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x0005464F File Offset: 0x0005284F
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x00054677 File Offset: 0x00052877
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060011EC RID: 4588 RVA: 0x00054680 File Offset: 0x00052880
			public InputStateHistory<TValue>.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x060011ED RID: 4589 RVA: 0x00054693 File Offset: 0x00052893
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x04000AC3 RID: 2755
			private readonly InputStateHistory<TValue> m_History;

			// Token: 0x04000AC4 RID: 2756
			private int m_Index;
		}

		// Token: 0x020001E2 RID: 482
		public new struct Record : IEquatable<InputStateHistory<TValue>.Record>
		{
			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x060011EF RID: 4591 RVA: 0x000546A0 File Offset: 0x000528A0
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000546B3 File Offset: 0x000528B3
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000546BD File Offset: 0x000528BD
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000546E4 File Offset: 0x000528E4
			public InputStateHistory<TValue> owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000546EC File Offset: 0x000528EC
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00054705 File Offset: 0x00052905
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00054718 File Offset: 0x00052918
			public unsafe InputControl<TValue> control
			{
				get
				{
					this.CheckValid();
					ReadOnlyArray<InputControl> controls = this.m_Owner.controls;
					if (controls.Count == 1 && !this.m_Owner.m_AddNewControls)
					{
						return (InputControl<TValue>)controls[0];
					}
					return (InputControl<TValue>)controls[this.header->controlIndex];
				}
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00054774 File Offset: 0x00052974
			public InputStateHistory<TValue>.Record next
			{
				get
				{
					this.CheckValid();
					int userIndex = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (userIndex + 1 >= this.m_Owner.Count)
					{
						return default(InputStateHistory<TValue>.Record);
					}
					int recordIndex = this.m_Owner.UserIndexToRecordIndex(userIndex + 1);
					return new InputStateHistory<TValue>.Record(this.m_Owner, recordIndex, this.m_Owner.GetRecord(recordIndex));
				}
			}

			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000547DC File Offset: 0x000529DC
			public InputStateHistory<TValue>.Record previous
			{
				get
				{
					this.CheckValid();
					int userIndex = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (userIndex - 1 < 0)
					{
						return default(InputStateHistory<TValue>.Record);
					}
					int recordIndex = this.m_Owner.UserIndexToRecordIndex(userIndex - 1);
					return new InputStateHistory<TValue>.Record(this.m_Owner, recordIndex, this.m_Owner.GetRecord(recordIndex));
				}
			}

			// Token: 0x060011F8 RID: 4600 RVA: 0x00054838 File Offset: 0x00052A38
			internal unsafe Record(InputStateHistory<TValue> owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x060011F9 RID: 4601 RVA: 0x00054856 File Offset: 0x00052A56
			internal Record(InputStateHistory<TValue> owner, int index)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = 0U;
			}

			// Token: 0x060011FA RID: 4602 RVA: 0x0005486F File Offset: 0x00052A6F
			public TValue ReadValue()
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x00054888 File Offset: 0x00052A88
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x00054898 File Offset: 0x00052A98
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x060011FD RID: 4605 RVA: 0x000548DF File Offset: 0x00052ADF
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x060011FE RID: 4606 RVA: 0x000548ED File Offset: 0x00052AED
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x060011FF RID: 4607 RVA: 0x00054928 File Offset: 0x00052B28
			public void CopyFrom(InputStateHistory<TValue>.Record record)
			{
				this.CheckValid();
				if (!record.valid)
				{
					throw new ArgumentException("Given history record is not valid", "record");
				}
				InputStateHistory.Record temp = new InputStateHistory.Record(this.m_Owner, this.recordIndex, this.header);
				temp.CopyFrom(new InputStateHistory.Record(record.m_Owner, record.recordIndex, record.header));
				this.m_Version = temp.version;
			}

			// Token: 0x06001200 RID: 4608 RVA: 0x0005499A File Offset: 0x00052B9A
			private unsafe void CheckValid()
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

			// Token: 0x06001201 RID: 4609 RVA: 0x000549D5 File Offset: 0x00052BD5
			public bool Equals(InputStateHistory<TValue>.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x00054A04 File Offset: 0x00052C04
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory<TValue>.Record)
				{
					InputStateHistory<TValue>.Record other = (InputStateHistory<TValue>.Record)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x00054A29 File Offset: 0x00052C29
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x06001204 RID: 4612 RVA: 0x00054A5B File Offset: 0x00052C5B
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValue(), this.time);
			}

			// Token: 0x04000AC5 RID: 2757
			private readonly InputStateHistory<TValue> m_Owner;

			// Token: 0x04000AC6 RID: 2758
			private readonly int m_IndexPlusOne;

			// Token: 0x04000AC7 RID: 2759
			private uint m_Version;
		}
	}
}
