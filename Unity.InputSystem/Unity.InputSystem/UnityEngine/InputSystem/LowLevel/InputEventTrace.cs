using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public sealed class InputEventTrace : IDisposable, IEnumerable<InputEventPtr>, IEnumerable
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0004F921 File Offset: 0x0004DB21
		public static FourCC FrameMarkerEvent
		{
			get
			{
				return new FourCC('F', 'R', 'M', 'E');
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0004F930 File Offset: 0x0004DB30
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x0004F938 File Offset: 0x0004DB38
		public int deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
			set
			{
				this.m_DeviceId = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0004F941 File Offset: 0x0004DB41
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0004F949 File Offset: 0x0004DB49
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x0004F954 File Offset: 0x0004DB54
		public bool recordFrameMarkers
		{
			get
			{
				return this.m_RecordFrameMarkers;
			}
			set
			{
				if (this.m_RecordFrameMarkers == value)
				{
					return;
				}
				this.m_RecordFrameMarkers = value;
				if (this.m_Enabled)
				{
					if (value)
					{
						InputSystem.onBeforeUpdate += this.OnBeforeUpdate;
						return;
					}
					InputSystem.onBeforeUpdate -= this.OnBeforeUpdate;
				}
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0004F9A0 File Offset: 0x0004DBA0
		public long eventCount
		{
			get
			{
				return this.m_EventCount;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0004F9A8 File Offset: 0x0004DBA8
		public long totalEventSizeInBytes
		{
			get
			{
				return this.m_EventSizeInBytes;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0004F9B0 File Offset: 0x0004DBB0
		public long allocatedSizeInBytes
		{
			get
			{
				if (this.m_EventBuffer == null)
				{
					return 0L;
				}
				return this.m_EventBufferSize;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0004F9C5 File Offset: 0x0004DBC5
		public long maxSizeInBytes
		{
			get
			{
				return this.m_MaxEventBufferSize;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0004F9CD File Offset: 0x0004DBCD
		public ReadOnlyArray<InputEventTrace.DeviceInfo> deviceInfos
		{
			get
			{
				return this.m_DeviceInfos;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0004F9DA File Offset: 0x0004DBDA
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x0004F9E2 File Offset: 0x0004DBE2
		public Func<InputEventPtr, InputDevice, bool> onFilterEvent
		{
			get
			{
				return this.m_OnFilterEvent;
			}
			set
			{
				this.m_OnFilterEvent = value;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600108B RID: 4235 RVA: 0x0004F9EB File Offset: 0x0004DBEB
		// (remove) Token: 0x0600108C RID: 4236 RVA: 0x0004F9F9 File Offset: 0x0004DBF9
		public event Action<InputEventPtr> onEvent
		{
			add
			{
				this.m_EventListeners.AddCallback(value);
			}
			remove
			{
				this.m_EventListeners.RemoveCallback(value);
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004FA07 File Offset: 0x0004DC07
		public InputEventTrace(InputDevice device, long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
			: this(bufferSizeInBytes, growBuffer, maxBufferSizeInBytes, growIncrementSizeInBytes)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.m_DeviceId = device.deviceId;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004FA30 File Offset: 0x0004DC30
		public InputEventTrace(long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
		{
			this.m_EventBufferSize = (long)((ulong)((uint)bufferSizeInBytes));
			if (!growBuffer)
			{
				this.m_MaxEventBufferSize = this.m_EventBufferSize;
				return;
			}
			if (maxBufferSizeInBytes < 0L)
			{
				this.m_MaxEventBufferSize = 268435456L;
			}
			else
			{
				this.m_MaxEventBufferSize = maxBufferSizeInBytes;
			}
			if (growIncrementSizeInBytes < 0L)
			{
				this.m_GrowIncrementSize = 1048576L;
				return;
			}
			this.m_GrowIncrementSize = growIncrementSizeInBytes;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0004FA94 File Offset: 0x0004DC94
		public void WriteTo(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			using (FileStream stream = File.OpenWrite(filePath))
			{
				this.WriteTo(stream);
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004FAE0 File Offset: 0x0004DCE0
		public unsafe void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream does not support seeking", "stream");
			}
			BinaryWriter writer = new BinaryWriter(stream);
			InputEventTrace.FileFlags flags = (InputEventTrace.FileFlags)0;
			if (InputSystem.settings.updateMode == InputSettings.UpdateMode.ProcessEventsInFixedUpdate)
			{
				flags |= InputEventTrace.FileFlags.FixedUpdate;
			}
			writer.Write(InputEventTrace.kFileFormat);
			writer.Write(InputEventTrace.kFileVersion);
			writer.Write((int)flags);
			writer.Write((int)Application.platform);
			writer.Write((ulong)this.m_EventCount);
			writer.Write((ulong)this.m_EventSizeInBytes);
			foreach (InputEventPtr eventPtr in this)
			{
				uint sizeInBytes = eventPtr.sizeInBytes;
				byte[] buffer = new byte[sizeInBytes];
				try
				{
					byte[] array;
					byte* bufferPtr;
					if ((array = buffer) == null || array.Length == 0)
					{
						bufferPtr = null;
					}
					else
					{
						bufferPtr = &array[0];
					}
					UnsafeUtility.MemCpy((void*)bufferPtr, (void*)eventPtr.data, (long)((ulong)sizeInBytes));
					writer.Write(buffer);
				}
				finally
				{
					byte[] array = null;
				}
			}
			writer.Flush();
			long positionOfDeviceList = stream.Position;
			int deviceCount = this.m_DeviceInfos.LengthSafe<InputEventTrace.DeviceInfo>();
			writer.Write(deviceCount);
			for (int i = 0; i < deviceCount; i++)
			{
				ref InputEventTrace.DeviceInfo device = ref this.m_DeviceInfos[i];
				writer.Write(device.deviceId);
				writer.Write(device.layout);
				writer.Write(device.stateFormat);
				writer.Write(device.stateSizeInBytes);
				writer.Write(device.m_FullLayoutJson ?? string.Empty);
			}
			writer.Flush();
			long offsetOfDeviceList = stream.Position - positionOfDeviceList;
			writer.Write(offsetOfDeviceList);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004FCB0 File Offset: 0x0004DEB0
		public void ReadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			using (FileStream stream = File.OpenRead(filePath))
			{
				this.ReadFrom(stream);
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004FCFC File Offset: 0x0004DEFC
		public unsafe void ReadFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading", "stream");
			}
			BinaryReader reader = new BinaryReader(stream);
			if (reader.ReadInt32() != InputEventTrace.kFileFormat)
			{
				throw new IOException(string.Format("Stream does not appear to be an InputEventTrace (no '{0}' code)", InputEventTrace.kFileFormat));
			}
			if (reader.ReadInt32() > InputEventTrace.kFileVersion)
			{
				throw new IOException(string.Format("Stream is an InputEventTrace but a newer version (expected version {0} or below)", InputEventTrace.kFileVersion));
			}
			reader.ReadInt32();
			reader.ReadInt32();
			ulong eventCount = reader.ReadUInt64();
			ulong totalEventSizeInBytes = reader.ReadUInt64();
			byte* oldBuffer = this.m_EventBuffer;
			if (eventCount > 0UL && totalEventSizeInBytes > 0UL)
			{
				byte* buffer;
				if (this.m_EventBuffer != null && this.m_EventBufferSize >= (long)totalEventSizeInBytes)
				{
					buffer = this.m_EventBuffer;
				}
				else
				{
					buffer = (byte*)UnsafeUtility.Malloc((long)totalEventSizeInBytes, 4, Allocator.Persistent);
					this.m_EventBufferSize = (long)totalEventSizeInBytes;
				}
				try
				{
					byte* tailPtr = buffer;
					byte* endPtr = tailPtr + totalEventSizeInBytes;
					long totalEventSize = 0L;
					for (ulong i = 0UL; i < eventCount; i += 1UL)
					{
						int eventType = reader.ReadInt32();
						uint eventSizeInBytes = (uint)reader.ReadUInt16();
						uint eventDeviceId = (uint)reader.ReadUInt16();
						if ((ulong)eventSizeInBytes > (ulong)((long)(endPtr - tailPtr)))
						{
							break;
						}
						*(int*)tailPtr = eventType;
						tailPtr += 4;
						*(short*)tailPtr = (short)((ushort)eventSizeInBytes);
						tailPtr += 2;
						*(short*)tailPtr = (short)((ushort)eventDeviceId);
						tailPtr += 2;
						int remainingSize = (int)(eventSizeInBytes - 4U - 2U - 2U);
						byte[] tempBuffer = reader.ReadBytes(remainingSize);
						try
						{
							byte[] array;
							byte* tempBufferPtr;
							if ((array = tempBuffer) == null || array.Length == 0)
							{
								tempBufferPtr = null;
							}
							else
							{
								tempBufferPtr = &array[0];
							}
							UnsafeUtility.MemCpy((void*)tailPtr, (void*)tempBufferPtr, (long)remainingSize);
						}
						finally
						{
							byte[] array = null;
						}
						tailPtr += remainingSize.AlignToMultipleOf(4);
						totalEventSize += (long)((ulong)eventSizeInBytes.AlignToMultipleOf(4U));
						if (tailPtr >= endPtr)
						{
							break;
						}
					}
					int deviceCount = reader.ReadInt32();
					InputEventTrace.DeviceInfo[] deviceInfos = new InputEventTrace.DeviceInfo[deviceCount];
					for (int j = 0; j < deviceCount; j++)
					{
						deviceInfos[j] = new InputEventTrace.DeviceInfo
						{
							deviceId = reader.ReadInt32(),
							layout = reader.ReadString(),
							stateFormat = reader.ReadInt32(),
							stateSizeInBytes = reader.ReadInt32(),
							m_FullLayoutJson = reader.ReadString()
						};
					}
					this.m_EventBuffer = buffer;
					this.m_EventBufferHead = this.m_EventBuffer;
					this.m_EventBufferTail = endPtr;
					this.m_EventCount = (long)eventCount;
					this.m_EventSizeInBytes = totalEventSize;
					this.m_DeviceInfos = deviceInfos;
					goto IL_0296;
				}
				catch
				{
					if (buffer != oldBuffer)
					{
						UnsafeUtility.Free((void*)buffer, Allocator.Persistent);
					}
					throw;
				}
			}
			this.m_EventBuffer = null;
			this.m_EventBufferHead = null;
			this.m_EventBufferTail = null;
			IL_0296:
			if (this.m_EventBuffer != oldBuffer && oldBuffer != null)
			{
				UnsafeUtility.Free((void*)oldBuffer, Allocator.Persistent);
			}
			this.m_ChangeCounter++;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0004FFF8 File Offset: 0x0004E1F8
		public static InputEventTrace LoadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			InputEventTrace inputEventTrace;
			using (FileStream stream = File.OpenRead(filePath))
			{
				inputEventTrace = InputEventTrace.LoadFrom(stream);
			}
			return inputEventTrace;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00050044 File Offset: 0x0004E244
		public static InputEventTrace LoadFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream must be readable", "stream");
			}
			InputEventTrace inputEventTrace = new InputEventTrace(1048576L, false, -1L, -1L);
			inputEventTrace.ReadFrom(stream);
			return inputEventTrace;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00050083 File Offset: 0x0004E283
		public InputEventTrace.ReplayController Replay()
		{
			this.Disable();
			return new InputEventTrace.ReplayController(this);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00050094 File Offset: 0x0004E294
		public unsafe bool Resize(long newBufferSize, long newMaxBufferSize = -1L)
		{
			if (newBufferSize <= 0L)
			{
				throw new ArgumentException("Size must be positive", "newBufferSize");
			}
			if (this.m_EventBufferSize == newBufferSize)
			{
				return true;
			}
			if (newMaxBufferSize < newBufferSize)
			{
				newMaxBufferSize = newBufferSize;
			}
			byte* newEventBuffer = (byte*)UnsafeUtility.Malloc(newBufferSize, 4, Allocator.Persistent);
			if (newEventBuffer == null)
			{
				return false;
			}
			if (this.m_EventCount > 0L)
			{
				if (newBufferSize < this.m_EventBufferSize || this.m_HasWrapped)
				{
					InputEventPtr fromPtr = new InputEventPtr((InputEvent*)this.m_EventBufferHead);
					InputEvent* toPtr = (InputEvent*)newEventBuffer;
					int newEventCount = 0;
					int newEventSizeInBytes = 0;
					long remainingEventBytes = this.m_EventSizeInBytes;
					int i = 0;
					while ((long)i < this.m_EventCount)
					{
						uint eventSizeInBytes = fromPtr.sizeInBytes;
						uint alignedEventSizeInBytes = eventSizeInBytes.AlignToMultipleOf(4U);
						if (remainingEventBytes <= newBufferSize)
						{
							UnsafeUtility.MemCpy((void*)toPtr, (void*)fromPtr.ToPointer(), (long)((ulong)eventSizeInBytes));
							toPtr = InputEvent.GetNextInMemory(toPtr);
							newEventSizeInBytes += (int)alignedEventSizeInBytes;
							newEventCount++;
						}
						remainingEventBytes -= (long)((ulong)alignedEventSizeInBytes);
						if (!this.GetNextEvent(ref fromPtr))
						{
							break;
						}
						i++;
					}
					this.m_HasWrapped = false;
					this.m_EventCount = (long)newEventCount;
					this.m_EventSizeInBytes = (long)newEventSizeInBytes;
				}
				else
				{
					UnsafeUtility.MemCpy((void*)newEventBuffer, (void*)this.m_EventBufferHead, this.m_EventSizeInBytes);
				}
			}
			if (this.m_EventBuffer != null)
			{
				UnsafeUtility.Free((void*)this.m_EventBuffer, Allocator.Persistent);
			}
			this.m_EventBufferSize = newBufferSize;
			this.m_EventBuffer = newEventBuffer;
			this.m_EventBufferHead = newEventBuffer;
			this.m_EventBufferTail = this.m_EventBuffer + this.m_EventSizeInBytes;
			this.m_MaxEventBufferSize = newMaxBufferSize;
			this.m_ChangeCounter++;
			return true;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000501F8 File Offset: 0x0004E3F8
		public unsafe void Clear()
		{
			byte* ptr = default(byte*);
			this.m_EventBufferTail = ptr;
			this.m_EventBufferHead = ptr;
			this.m_EventCount = 0L;
			this.m_EventSizeInBytes = 0L;
			this.m_ChangeCounter++;
			this.m_DeviceInfos = null;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00050240 File Offset: 0x0004E440
		public void Enable()
		{
			if (this.m_Enabled)
			{
				return;
			}
			if (this.m_EventBuffer == null)
			{
				this.Allocate();
			}
			InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputEvent);
			if (this.m_RecordFrameMarkers)
			{
				InputSystem.onBeforeUpdate += this.OnBeforeUpdate;
			}
			this.m_Enabled = true;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000502A1 File Offset: 0x0004E4A1
		public void Disable()
		{
			if (!this.m_Enabled)
			{
				return;
			}
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputEvent);
			InputSystem.onBeforeUpdate -= this.OnBeforeUpdate;
			this.m_Enabled = false;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000502E0 File Offset: 0x0004E4E0
		public unsafe bool GetNextEvent(ref InputEventPtr current)
		{
			if (this.m_EventBuffer == null)
			{
				return false;
			}
			if (this.m_EventBufferHead == null)
			{
				return false;
			}
			if (!current.valid)
			{
				current = new InputEventPtr((InputEvent*)this.m_EventBufferHead);
				return true;
			}
			byte* nextEvent = (byte*)current.Next().data;
			byte* endOfBuffer = this.m_EventBuffer + this.m_EventBufferSize;
			if (nextEvent == this.m_EventBufferTail)
			{
				return false;
			}
			if ((long)(endOfBuffer - nextEvent) < 20L || ((InputEvent*)nextEvent)->sizeInBytes == 0U)
			{
				nextEvent = this.m_EventBuffer;
				if (nextEvent == (byte*)current.ToPointer())
				{
					return false;
				}
			}
			current = new InputEventPtr((InputEvent*)nextEvent);
			return true;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0005037B File Offset: 0x0004E57B
		public IEnumerator<InputEventPtr> GetEnumerator()
		{
			return new InputEventTrace.Enumerator(this);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00050383 File Offset: 0x0004E583
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0005038B File Offset: 0x0004E58B
		public void Dispose()
		{
			this.Disable();
			this.Release();
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00050399 File Offset: 0x0004E599
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x000503A2 File Offset: 0x0004E5A2
		private unsafe byte* m_EventBuffer
		{
			get
			{
				return this.m_EventBufferStorage;
			}
			set
			{
				this.m_EventBufferStorage = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x000503AC File Offset: 0x0004E5AC
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x000503B5 File Offset: 0x0004E5B5
		private unsafe byte* m_EventBufferHead
		{
			get
			{
				return this.m_EventBufferHeadStorage;
			}
			set
			{
				this.m_EventBufferHeadStorage = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000503BF File Offset: 0x0004E5BF
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x000503C8 File Offset: 0x0004E5C8
		private unsafe byte* m_EventBufferTail
		{
			get
			{
				return this.m_EventBufferTailStorage;
			}
			set
			{
				this.m_EventBufferTailStorage = value;
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000503D2 File Offset: 0x0004E5D2
		private unsafe void Allocate()
		{
			this.m_EventBuffer = (byte*)UnsafeUtility.Malloc(this.m_EventBufferSize, 4, Allocator.Persistent);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000503E7 File Offset: 0x0004E5E7
		private unsafe void Release()
		{
			this.Clear();
			if (this.m_EventBuffer != null)
			{
				UnsafeUtility.Free((void*)this.m_EventBuffer, Allocator.Persistent);
				this.m_EventBuffer = null;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00050410 File Offset: 0x0004E610
		private unsafe void OnBeforeUpdate()
		{
			if (this.m_RecordFrameMarkers)
			{
				InputEvent frameMarkerEvent = new InputEvent
				{
					type = InputEventTrace.FrameMarkerEvent,
					internalTime = InputRuntime.s_Instance.currentTime,
					sizeInBytes = (uint)UnsafeUtility.SizeOf<InputEvent>()
				};
				this.OnInputEvent(new InputEventPtr((InputEvent*)UnsafeUtility.AddressOf<InputEvent>(ref frameMarkerEvent)), null);
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0005046C File Offset: 0x0004E66C
		private unsafe void OnInputEvent(InputEventPtr inputEvent, InputDevice device)
		{
			if (inputEvent.handled)
			{
				return;
			}
			if (this.m_DeviceId != 0 && inputEvent.deviceId != this.m_DeviceId && inputEvent.type != InputEventTrace.FrameMarkerEvent)
			{
				return;
			}
			if (this.m_OnFilterEvent != null && !this.m_OnFilterEvent(inputEvent, device))
			{
				return;
			}
			if (this.m_EventBuffer == null)
			{
				return;
			}
			uint bytesNeeded = inputEvent.sizeInBytes.AlignToMultipleOf(4U);
			if ((ulong)bytesNeeded > (ulong)this.m_MaxEventBufferSize)
			{
				return;
			}
			if (this.m_EventBufferTail == null)
			{
				this.m_EventBufferHead = this.m_EventBuffer;
				this.m_EventBufferTail = this.m_EventBuffer;
			}
			byte* newTail = this.m_EventBufferTail + bytesNeeded;
			bool newTailOvertakesHead = newTail != this.m_EventBufferHead && this.m_EventBufferHead != this.m_EventBuffer;
			if (newTail != this.m_EventBuffer + this.m_EventBufferSize)
			{
				if (this.m_EventBufferSize < this.m_MaxEventBufferSize && !this.m_HasWrapped)
				{
					long increment = Math.Max(this.m_GrowIncrementSize, (long)((ulong)bytesNeeded.AlignToMultipleOf(4U)));
					long newBufferSize = this.m_EventBufferSize + increment;
					if (newBufferSize > this.m_MaxEventBufferSize)
					{
						newBufferSize = this.m_MaxEventBufferSize;
					}
					if (newBufferSize < (long)((ulong)bytesNeeded))
					{
						return;
					}
					this.Resize(newBufferSize, -1L);
					newTail = this.m_EventBufferTail + bytesNeeded;
				}
				long spaceLeft = this.m_EventBufferSize - (long)(this.m_EventBufferTail - this.m_EventBuffer);
				if (spaceLeft < (long)((ulong)bytesNeeded))
				{
					this.m_HasWrapped = true;
					if (spaceLeft >= 20L)
					{
						UnsafeUtility.MemClear((void*)this.m_EventBufferTail, 20L);
					}
					this.m_EventBufferTail = this.m_EventBuffer;
					newTail = this.m_EventBuffer + bytesNeeded;
					if (newTailOvertakesHead)
					{
						this.m_EventBufferHead = this.m_EventBuffer;
					}
					newTailOvertakesHead = newTail != this.m_EventBufferHead;
				}
			}
			if (newTailOvertakesHead)
			{
				byte* newHead = this.m_EventBufferHead;
				byte* endOfBufferMinusOneEvent = this.m_EventBuffer + this.m_EventBufferSize - 20;
				while (newHead < newTail)
				{
					uint numBytes = ((InputEvent*)newHead)->sizeInBytes;
					newHead += numBytes;
					this.m_EventCount -= 1L;
					this.m_EventSizeInBytes -= (long)((ulong)numBytes);
					if (newHead != endOfBufferMinusOneEvent || ((InputEvent*)newHead)->sizeInBytes == 0U)
					{
						newHead = this.m_EventBuffer;
						break;
					}
				}
				this.m_EventBufferHead = newHead;
			}
			byte* buffer = this.m_EventBufferTail;
			this.m_EventBufferTail = newTail;
			UnsafeUtility.MemCpy((void*)buffer, (void*)inputEvent.data, (long)((ulong)inputEvent.sizeInBytes));
			this.m_ChangeCounter++;
			this.m_EventCount += 1L;
			this.m_EventSizeInBytes += (long)((ulong)bytesNeeded);
			if (device != null)
			{
				bool haveRecord = false;
				if (this.m_DeviceInfos != null)
				{
					for (int i = 0; i < this.m_DeviceInfos.Length; i++)
					{
						if (this.m_DeviceInfos[i].deviceId == device.deviceId)
						{
							haveRecord = true;
							break;
						}
					}
				}
				if (!haveRecord)
				{
					ArrayHelpers.Append<InputEventTrace.DeviceInfo>(ref this.m_DeviceInfos, new InputEventTrace.DeviceInfo
					{
						m_DeviceId = device.deviceId,
						m_Layout = device.layout,
						m_StateFormat = device.stateBlock.format,
						m_StateSizeInBytes = (int)device.stateBlock.alignedSizeInBytes,
						m_FullLayoutJson = (InputControlLayout.s_Layouts.IsGeneratedLayout(device.m_Layout) ? InputSystem.LoadLayout(device.layout).ToJson() : null)
					});
				}
			}
			if (this.m_EventListeners.length > 0)
			{
				DelegateHelpers.InvokeCallbacksSafe<InputEventPtr>(ref this.m_EventListeners, new InputEventPtr((InputEvent*)buffer), "InputEventTrace.onEvent", null);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x000507D8 File Offset: 0x0004E9D8
		private static FourCC kFileFormat
		{
			get
			{
				return new FourCC('I', 'E', 'V', 'T');
			}
		}

		// Token: 0x04000A0D RID: 2573
		private const int kDefaultBufferSize = 1048576;

		// Token: 0x04000A0E RID: 2574
		private static readonly ProfilerMarker k_InputEvenTraceMarker = new ProfilerMarker("InputEventTrace");

		// Token: 0x04000A0F RID: 2575
		[NonSerialized]
		private int m_ChangeCounter;

		// Token: 0x04000A10 RID: 2576
		[NonSerialized]
		private bool m_Enabled;

		// Token: 0x04000A11 RID: 2577
		[NonSerialized]
		private Func<InputEventPtr, InputDevice, bool> m_OnFilterEvent;

		// Token: 0x04000A12 RID: 2578
		[SerializeField]
		private int m_DeviceId;

		// Token: 0x04000A13 RID: 2579
		[NonSerialized]
		private CallbackArray<Action<InputEventPtr>> m_EventListeners;

		// Token: 0x04000A14 RID: 2580
		[SerializeField]
		private long m_EventBufferSize;

		// Token: 0x04000A15 RID: 2581
		[SerializeField]
		private long m_MaxEventBufferSize;

		// Token: 0x04000A16 RID: 2582
		[SerializeField]
		private long m_GrowIncrementSize;

		// Token: 0x04000A17 RID: 2583
		[SerializeField]
		private long m_EventCount;

		// Token: 0x04000A18 RID: 2584
		[SerializeField]
		private long m_EventSizeInBytes;

		// Token: 0x04000A19 RID: 2585
		[SerializeField]
		private ulong m_EventBufferStorage;

		// Token: 0x04000A1A RID: 2586
		[SerializeField]
		private ulong m_EventBufferHeadStorage;

		// Token: 0x04000A1B RID: 2587
		[SerializeField]
		private ulong m_EventBufferTailStorage;

		// Token: 0x04000A1C RID: 2588
		[SerializeField]
		private bool m_HasWrapped;

		// Token: 0x04000A1D RID: 2589
		[SerializeField]
		private bool m_RecordFrameMarkers;

		// Token: 0x04000A1E RID: 2590
		[SerializeField]
		private InputEventTrace.DeviceInfo[] m_DeviceInfos;

		// Token: 0x04000A1F RID: 2591
		private static int kFileVersion = 1;

		// Token: 0x020001BC RID: 444
		private class Enumerator : IEnumerator<InputEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x060010AA RID: 4266 RVA: 0x000507FE File Offset: 0x0004E9FE
			public Enumerator(InputEventTrace trace)
			{
				this.m_Trace = trace;
				this.m_ChangeCounter = trace.m_ChangeCounter;
			}

			// Token: 0x060010AB RID: 4267 RVA: 0x00050819 File Offset: 0x0004EA19
			public void Dispose()
			{
				this.m_Trace = null;
				this.m_Current = default(InputEventPtr);
			}

			// Token: 0x060010AC RID: 4268 RVA: 0x00050830 File Offset: 0x0004EA30
			public bool MoveNext()
			{
				if (this.m_Trace == null)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				if (this.m_Trace.m_ChangeCounter != this.m_ChangeCounter)
				{
					throw new InvalidOperationException("Trace has been modified while enumerating!");
				}
				return this.m_Trace.GetNextEvent(ref this.m_Current);
			}

			// Token: 0x060010AD RID: 4269 RVA: 0x00050880 File Offset: 0x0004EA80
			public void Reset()
			{
				this.m_Current = default(InputEventPtr);
				this.m_ChangeCounter = this.m_Trace.m_ChangeCounter;
			}

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x060010AE RID: 4270 RVA: 0x0005089F File Offset: 0x0004EA9F
			public InputEventPtr Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x060010AF RID: 4271 RVA: 0x000508A7 File Offset: 0x0004EAA7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000A20 RID: 2592
			private InputEventTrace m_Trace;

			// Token: 0x04000A21 RID: 2593
			private int m_ChangeCounter;

			// Token: 0x04000A22 RID: 2594
			internal InputEventPtr m_Current;
		}

		// Token: 0x020001BD RID: 445
		[Flags]
		private enum FileFlags
		{
			// Token: 0x04000A24 RID: 2596
			FixedUpdate = 1
		}

		// Token: 0x020001BE RID: 446
		public class ReplayController : IDisposable
		{
			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x060010B0 RID: 4272 RVA: 0x000508B4 File Offset: 0x0004EAB4
			public InputEventTrace trace
			{
				get
				{
					return this.m_EventTrace;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x060010B1 RID: 4273 RVA: 0x000508BC File Offset: 0x0004EABC
			// (set) Token: 0x060010B2 RID: 4274 RVA: 0x000508C4 File Offset: 0x0004EAC4
			public bool finished { get; private set; }

			// Token: 0x170004BE RID: 1214
			// (get) Token: 0x060010B3 RID: 4275 RVA: 0x000508CD File Offset: 0x0004EACD
			// (set) Token: 0x060010B4 RID: 4276 RVA: 0x000508D5 File Offset: 0x0004EAD5
			public bool paused { get; set; }

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x060010B5 RID: 4277 RVA: 0x000508DE File Offset: 0x0004EADE
			// (set) Token: 0x060010B6 RID: 4278 RVA: 0x000508E6 File Offset: 0x0004EAE6
			public int position { get; private set; }

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x060010B7 RID: 4279 RVA: 0x000508EF File Offset: 0x0004EAEF
			public IEnumerable<InputDevice> createdDevices
			{
				get
				{
					return this.m_CreatedDevices;
				}
			}

			// Token: 0x060010B8 RID: 4280 RVA: 0x000508FC File Offset: 0x0004EAFC
			internal ReplayController(InputEventTrace trace)
			{
				if (trace == null)
				{
					throw new ArgumentNullException("trace");
				}
				this.m_EventTrace = trace;
			}

			// Token: 0x060010B9 RID: 4281 RVA: 0x0005091C File Offset: 0x0004EB1C
			public void Dispose()
			{
				InputSystem.onBeforeUpdate -= this.OnBeginFrame;
				this.finished = true;
				foreach (InputDevice inputDevice in this.m_CreatedDevices)
				{
					InputSystem.RemoveDevice(inputDevice);
				}
				this.m_CreatedDevices = default(InlinedArray<InputDevice>);
			}

			// Token: 0x060010BA RID: 4282 RVA: 0x0005098C File Offset: 0x0004EB8C
			public InputEventTrace.ReplayController WithDeviceMappedFromTo(InputDevice recordedDevice, InputDevice playbackDevice)
			{
				if (recordedDevice == null)
				{
					throw new ArgumentNullException("recordedDevice");
				}
				if (playbackDevice == null)
				{
					throw new ArgumentNullException("playbackDevice");
				}
				this.WithDeviceMappedFromTo(recordedDevice.deviceId, playbackDevice.deviceId);
				return this;
			}

			// Token: 0x060010BB RID: 4283 RVA: 0x000509C0 File Offset: 0x0004EBC0
			public InputEventTrace.ReplayController WithDeviceMappedFromTo(int recordedDeviceId, int playbackDeviceId)
			{
				for (int i = 0; i < this.m_DeviceIDMappings.length; i++)
				{
					if (this.m_DeviceIDMappings[i].Key == recordedDeviceId)
					{
						if (recordedDeviceId == playbackDeviceId)
						{
							this.m_DeviceIDMappings.RemoveAtWithCapacity(i);
						}
						else
						{
							this.m_DeviceIDMappings[i] = new KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId);
						}
						return this;
					}
				}
				if (recordedDeviceId == playbackDeviceId)
				{
					return this;
				}
				this.m_DeviceIDMappings.AppendWithCapacity(new KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId), 10);
				return this;
			}

			// Token: 0x060010BC RID: 4284 RVA: 0x00050A3D File Offset: 0x0004EC3D
			public InputEventTrace.ReplayController WithAllDevicesMappedToNewInstances()
			{
				this.m_CreateNewDevices = true;
				return this;
			}

			// Token: 0x060010BD RID: 4285 RVA: 0x00050A47 File Offset: 0x0004EC47
			public InputEventTrace.ReplayController OnFinished(Action action)
			{
				this.m_OnFinished = action;
				return this;
			}

			// Token: 0x060010BE RID: 4286 RVA: 0x00050A51 File Offset: 0x0004EC51
			public InputEventTrace.ReplayController OnEvent(Action<InputEventPtr> action)
			{
				this.m_OnEvent = action;
				return this;
			}

			// Token: 0x060010BF RID: 4287 RVA: 0x00050A5C File Offset: 0x0004EC5C
			public InputEventTrace.ReplayController PlayOneEvent()
			{
				InputEventPtr eventPtr;
				if (!this.MoveNext(true, out eventPtr))
				{
					throw new InvalidOperationException("No more events");
				}
				this.QueueEvent(eventPtr);
				return this;
			}

			// Token: 0x060010C0 RID: 4288 RVA: 0x00050A87 File Offset: 0x0004EC87
			public InputEventTrace.ReplayController Rewind()
			{
				this.m_Enumerator = null;
				this.m_AllEventsByTime = null;
				this.m_AllEventsByTimeIndex = -1;
				this.position = 0;
				return this;
			}

			// Token: 0x060010C1 RID: 4289 RVA: 0x00050AA6 File Offset: 0x0004ECA6
			public InputEventTrace.ReplayController PlayAllFramesOneByOne()
			{
				this.finished = false;
				InputSystem.onBeforeUpdate += this.OnBeginFrame;
				return this;
			}

			// Token: 0x060010C2 RID: 4290 RVA: 0x00050AC4 File Offset: 0x0004ECC4
			public InputEventTrace.ReplayController PlayAllEvents()
			{
				this.finished = false;
				try
				{
					InputEventPtr eventPtr;
					while (this.MoveNext(true, out eventPtr))
					{
						this.QueueEvent(eventPtr);
					}
				}
				finally
				{
					this.Finished();
				}
				return this;
			}

			// Token: 0x060010C3 RID: 4291 RVA: 0x00050B08 File Offset: 0x0004ED08
			public InputEventTrace.ReplayController PlayAllEventsAccordingToTimestamps()
			{
				List<InputEventPtr> eventsByTime = new List<InputEventPtr>();
				InputEventPtr eventPtr;
				while (this.MoveNext(true, out eventPtr))
				{
					eventsByTime.Add(eventPtr);
				}
				eventsByTime.Sort((InputEventPtr a, InputEventPtr b) => a.time.CompareTo(b.time));
				this.m_Enumerator.Dispose();
				this.m_Enumerator = null;
				this.m_AllEventsByTime = eventsByTime;
				this.position = 0;
				this.finished = false;
				this.m_StartTimeAsPerFirstEvent = -1.0;
				this.m_AllEventsByTimeIndex = -1;
				InputSystem.onBeforeUpdate += this.OnBeginFrame;
				return this;
			}

			// Token: 0x060010C4 RID: 4292 RVA: 0x00050BA4 File Offset: 0x0004EDA4
			private void OnBeginFrame()
			{
				if (this.paused)
				{
					return;
				}
				InputEventPtr currentEventPtr;
				if (!this.MoveNext(false, out currentEventPtr))
				{
					if (this.m_AllEventsByTime == null || this.m_AllEventsByTimeIndex >= this.m_AllEventsByTime.Count)
					{
						this.Finished();
					}
					return;
				}
				int num;
				if (currentEventPtr.type == InputEventTrace.FrameMarkerEvent)
				{
					InputEventPtr nextEvent;
					if (!this.MoveNext(false, out nextEvent))
					{
						this.Finished();
						return;
					}
					if (nextEvent.type == InputEventTrace.FrameMarkerEvent)
					{
						num = this.position - 1;
						this.position = num;
						this.m_Enumerator.m_Current = currentEventPtr;
						return;
					}
					currentEventPtr = nextEvent;
				}
				for (;;)
				{
					this.QueueEvent(currentEventPtr);
					InputEventPtr nextEvent2;
					if (!this.MoveNext(false, out nextEvent2))
					{
						break;
					}
					if (nextEvent2.type == InputEventTrace.FrameMarkerEvent)
					{
						goto Block_9;
					}
					currentEventPtr = nextEvent2;
				}
				if (this.m_AllEventsByTime == null || this.m_AllEventsByTimeIndex >= this.m_AllEventsByTime.Count)
				{
					this.Finished();
					return;
				}
				return;
				Block_9:
				this.m_Enumerator.m_Current = currentEventPtr;
				num = this.position - 1;
				this.position = num;
			}

			// Token: 0x060010C5 RID: 4293 RVA: 0x00050CA6 File Offset: 0x0004EEA6
			private void Finished()
			{
				this.finished = true;
				InputSystem.onBeforeUpdate -= this.OnBeginFrame;
				Action onFinished = this.m_OnFinished;
				if (onFinished == null)
				{
					return;
				}
				onFinished();
			}

			// Token: 0x060010C6 RID: 4294 RVA: 0x00050CD0 File Offset: 0x0004EED0
			private void QueueEvent(InputEventPtr eventPtr)
			{
				double originalTimestamp = eventPtr.internalTime;
				if (this.m_AllEventsByTime != null)
				{
					eventPtr.internalTime = this.m_StartTimeAsPerRuntime + (eventPtr.internalTime - this.m_StartTimeAsPerFirstEvent);
				}
				else
				{
					eventPtr.internalTime = InputRuntime.s_Instance.currentTime;
				}
				int originalEventId = eventPtr.id;
				int originalDeviceId = eventPtr.deviceId;
				eventPtr.deviceId = this.ApplyDeviceMapping(originalDeviceId);
				Action<InputEventPtr> onEvent = this.m_OnEvent;
				if (onEvent != null)
				{
					onEvent(eventPtr);
				}
				try
				{
					InputSystem.QueueEvent(eventPtr);
				}
				finally
				{
					eventPtr.internalTime = originalTimestamp;
					eventPtr.id = originalEventId;
					eventPtr.deviceId = originalDeviceId;
				}
			}

			// Token: 0x060010C7 RID: 4295 RVA: 0x00050D80 File Offset: 0x0004EF80
			private bool MoveNext(bool skipFrameEvents, out InputEventPtr eventPtr)
			{
				eventPtr = default(InputEventPtr);
				int num;
				if (this.m_AllEventsByTime == null)
				{
					if (this.m_Enumerator == null)
					{
						this.m_Enumerator = new InputEventTrace.Enumerator(this.m_EventTrace);
					}
					while (this.m_Enumerator.MoveNext())
					{
						num = this.position + 1;
						this.position = num;
						eventPtr = this.m_Enumerator.Current;
						if (!skipFrameEvents || !(eventPtr.type == InputEventTrace.FrameMarkerEvent))
						{
							return true;
						}
					}
					return false;
				}
				if (this.m_AllEventsByTimeIndex + 1 >= this.m_AllEventsByTime.Count)
				{
					this.position = this.m_AllEventsByTime.Count;
					this.m_AllEventsByTimeIndex = this.m_AllEventsByTime.Count;
					return false;
				}
				if (this.m_AllEventsByTimeIndex < 0)
				{
					this.m_StartTimeAsPerFirstEvent = this.m_AllEventsByTime[0].internalTime;
					this.m_StartTimeAsPerRuntime = InputRuntime.s_Instance.currentTime;
				}
				else if (this.m_AllEventsByTimeIndex < this.m_AllEventsByTime.Count - 1 && this.m_AllEventsByTime[this.m_AllEventsByTimeIndex + 1].internalTime > this.m_StartTimeAsPerFirstEvent + (InputRuntime.s_Instance.currentTime - this.m_StartTimeAsPerRuntime))
				{
					return false;
				}
				this.m_AllEventsByTimeIndex++;
				num = this.position + 1;
				this.position = num;
				eventPtr = this.m_AllEventsByTime[this.m_AllEventsByTimeIndex];
				return true;
			}

			// Token: 0x060010C8 RID: 4296 RVA: 0x00050EF0 File Offset: 0x0004F0F0
			private int ApplyDeviceMapping(int originalDeviceId)
			{
				for (int i = 0; i < this.m_DeviceIDMappings.length; i++)
				{
					KeyValuePair<int, int> entry = this.m_DeviceIDMappings[i];
					if (entry.Key == originalDeviceId)
					{
						return entry.Value;
					}
				}
				if (this.m_CreateNewDevices)
				{
					try
					{
						int deviceIndex = this.m_EventTrace.deviceInfos.IndexOf((InputEventTrace.DeviceInfo x) => x.deviceId == originalDeviceId);
						if (deviceIndex != -1)
						{
							InputEventTrace.DeviceInfo deviceInfo = this.m_EventTrace.deviceInfos[deviceIndex];
							InternedString layoutName = new InternedString(deviceInfo.layout);
							if (!InputControlLayout.s_Layouts.HasLayout(layoutName))
							{
								if (string.IsNullOrEmpty(deviceInfo.m_FullLayoutJson))
								{
									return originalDeviceId;
								}
								InputSystem.RegisterLayout(deviceInfo.m_FullLayoutJson, null, null);
							}
							InputDevice device = InputSystem.AddDevice(layoutName, null, null);
							this.WithDeviceMappedFromTo(originalDeviceId, device.deviceId);
							this.m_CreatedDevices.AppendWithCapacity(device, 10);
							return device.deviceId;
						}
					}
					catch
					{
					}
				}
				return originalDeviceId;
			}

			// Token: 0x04000A28 RID: 2600
			private InputEventTrace m_EventTrace;

			// Token: 0x04000A29 RID: 2601
			private InputEventTrace.Enumerator m_Enumerator;

			// Token: 0x04000A2A RID: 2602
			private InlinedArray<KeyValuePair<int, int>> m_DeviceIDMappings;

			// Token: 0x04000A2B RID: 2603
			private bool m_CreateNewDevices;

			// Token: 0x04000A2C RID: 2604
			private InlinedArray<InputDevice> m_CreatedDevices;

			// Token: 0x04000A2D RID: 2605
			private Action m_OnFinished;

			// Token: 0x04000A2E RID: 2606
			private Action<InputEventPtr> m_OnEvent;

			// Token: 0x04000A2F RID: 2607
			private double m_StartTimeAsPerFirstEvent;

			// Token: 0x04000A30 RID: 2608
			private double m_StartTimeAsPerRuntime;

			// Token: 0x04000A31 RID: 2609
			private int m_AllEventsByTimeIndex;

			// Token: 0x04000A32 RID: 2610
			private List<InputEventPtr> m_AllEventsByTime;
		}

		// Token: 0x020001C1 RID: 449
		[Serializable]
		public struct DeviceInfo
		{
			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x060010CE RID: 4302 RVA: 0x00051078 File Offset: 0x0004F278
			// (set) Token: 0x060010CF RID: 4303 RVA: 0x00051080 File Offset: 0x0004F280
			public int deviceId
			{
				get
				{
					return this.m_DeviceId;
				}
				set
				{
					this.m_DeviceId = value;
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00051089 File Offset: 0x0004F289
			// (set) Token: 0x060010D1 RID: 4305 RVA: 0x00051091 File Offset: 0x0004F291
			public string layout
			{
				get
				{
					return this.m_Layout;
				}
				set
				{
					this.m_Layout = value;
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0005109A File Offset: 0x0004F29A
			// (set) Token: 0x060010D3 RID: 4307 RVA: 0x000510A2 File Offset: 0x0004F2A2
			public FourCC stateFormat
			{
				get
				{
					return this.m_StateFormat;
				}
				set
				{
					this.m_StateFormat = value;
				}
			}

			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000510AB File Offset: 0x0004F2AB
			// (set) Token: 0x060010D5 RID: 4309 RVA: 0x000510B3 File Offset: 0x0004F2B3
			public int stateSizeInBytes
			{
				get
				{
					return this.m_StateSizeInBytes;
				}
				set
				{
					this.m_StateSizeInBytes = value;
				}
			}

			// Token: 0x04000A36 RID: 2614
			[SerializeField]
			internal int m_DeviceId;

			// Token: 0x04000A37 RID: 2615
			[SerializeField]
			internal string m_Layout;

			// Token: 0x04000A38 RID: 2616
			[SerializeField]
			internal FourCC m_StateFormat;

			// Token: 0x04000A39 RID: 2617
			[SerializeField]
			internal int m_StateSizeInBytes;

			// Token: 0x04000A3A RID: 2618
			[SerializeField]
			internal string m_FullLayoutJson;
		}
	}
}
