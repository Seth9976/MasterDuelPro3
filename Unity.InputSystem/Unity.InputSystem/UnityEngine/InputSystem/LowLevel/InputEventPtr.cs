using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001B9 RID: 441
	public struct InputEventPtr : IEquatable<InputEventPtr>
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		public unsafe InputEventPtr(InputEvent* eventPtr)
		{
			this.m_EventPtr = eventPtr;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0004F1BD File Offset: 0x0004D3BD
		public bool valid
		{
			get
			{
				return this.m_EventPtr != null;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x0004F1E3 File Offset: 0x0004D3E3
		public unsafe bool handled
		{
			get
			{
				return this.valid && this.m_EventPtr->handled;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->handled = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0004F204 File Offset: 0x0004D404
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x0004F21B File Offset: 0x0004D41B
		public unsafe int id
		{
			get
			{
				if (!this.valid)
				{
					return 0;
				}
				return this.m_EventPtr->eventId;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->eventId = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0004F23C File Offset: 0x0004D43C
		public unsafe FourCC type
		{
			get
			{
				if (!this.valid)
				{
					return default(FourCC);
				}
				return this.m_EventPtr->type;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0004F266 File Offset: 0x0004D466
		public unsafe uint sizeInBytes
		{
			get
			{
				if (!this.valid)
				{
					return 0U;
				}
				return this.m_EventPtr->sizeInBytes;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004F27D File Offset: 0x0004D47D
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0004F294 File Offset: 0x0004D494
		public unsafe int deviceId
		{
			get
			{
				if (!this.valid)
				{
					return 0;
				}
				return this.m_EventPtr->deviceId;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->deviceId = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004F2B5 File Offset: 0x0004D4B5
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0004F2D4 File Offset: 0x0004D4D4
		public unsafe double time
		{
			get
			{
				if (!this.valid)
				{
					return 0.0;
				}
				return this.m_EventPtr->time;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->time = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0004F2F5 File Offset: 0x0004D4F5
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x0004F314 File Offset: 0x0004D514
		internal unsafe double internalTime
		{
			get
			{
				if (!this.valid)
				{
					return 0.0;
				}
				return this.m_EventPtr->internalTime;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->internalTime = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0004F335 File Offset: 0x0004D535
		public unsafe InputEvent* data
		{
			get
			{
				return this.m_EventPtr;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0004F340 File Offset: 0x0004D540
		internal unsafe FourCC stateFormat
		{
			get
			{
				FourCC eventType = this.type;
				if (eventType == 1398030676)
				{
					return StateEvent.FromUnchecked(this)->stateFormat;
				}
				if (eventType == 1145852993)
				{
					return DeltaStateEvent.FromUnchecked(this)->stateFormat;
				}
				string text = "Event must be a StateEvent or DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0004F3C0 File Offset: 0x0004D5C0
		internal unsafe uint stateSizeInBytes
		{
			get
			{
				if (this.IsA<StateEvent>())
				{
					return StateEvent.From(this)->stateSizeInBytes;
				}
				if (this.IsA<DeltaStateEvent>())
				{
					return DeltaStateEvent.From(this)->deltaStateSizeInBytes;
				}
				string text = "Event must be a StateEvent or DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0004F424 File Offset: 0x0004D624
		internal unsafe uint stateOffset
		{
			get
			{
				if (this.IsA<DeltaStateEvent>())
				{
					return DeltaStateEvent.From(this)->stateOffset;
				}
				string text = "Event must be a DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004F470 File Offset: 0x0004D670
		public unsafe bool IsA<TOtherEvent>() where TOtherEvent : struct, IInputEventTypeInfo
		{
			if (this.m_EventPtr == null)
			{
				return false;
			}
			TOtherEvent otherEvent = default(TOtherEvent);
			return this.m_EventPtr->type == otherEvent.typeStatic;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004F4B0 File Offset: 0x0004D6B0
		public InputEventPtr Next()
		{
			if (!this.valid)
			{
				return default(InputEventPtr);
			}
			return new InputEventPtr(InputEvent.GetNextInMemory(this.m_EventPtr));
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004F4E0 File Offset: 0x0004D6E0
		public unsafe override string ToString()
		{
			if (!this.valid)
			{
				return "null";
			}
			InputEvent eventPtr = *this.m_EventPtr;
			return eventPtr.ToString();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004F514 File Offset: 0x0004D714
		public unsafe InputEvent* ToPointer()
		{
			return this;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004F521 File Offset: 0x0004D721
		public bool Equals(InputEventPtr other)
		{
			return this.m_EventPtr == other.m_EventPtr || InputEvent.Equals(this.m_EventPtr, other.m_EventPtr);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004F544 File Offset: 0x0004D744
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputEventPtr)
			{
				InputEventPtr ptr = (InputEventPtr)obj;
				return this.Equals(ptr);
			}
			return false;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004F56E File Offset: 0x0004D76E
		public override int GetHashCode()
		{
			return this.m_EventPtr;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004F578 File Offset: 0x0004D778
		public static bool operator ==(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr == right.m_EventPtr;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004F588 File Offset: 0x0004D788
		public static bool operator !=(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr != right.m_EventPtr;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004F59B File Offset: 0x0004D79B
		public unsafe static implicit operator InputEventPtr(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0004F59B File Offset: 0x0004D79B
		public unsafe static InputEventPtr From(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0004E450 File Offset: 0x0004C650
		public unsafe static implicit operator InputEvent*(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004E450 File Offset: 0x0004C650
		public unsafe static InputEvent* FromInputEventPtr(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x04000A01 RID: 2561
		private unsafe readonly InputEvent* m_EventPtr;
	}
}
