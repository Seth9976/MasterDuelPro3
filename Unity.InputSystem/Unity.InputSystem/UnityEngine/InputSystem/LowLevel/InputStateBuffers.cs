using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001D8 RID: 472
	internal struct InputStateBuffers
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00052F04 File Offset: 0x00051104
		public InputStateBuffers.DoubleBuffers GetDoubleBuffersFor(InputUpdateType updateType)
		{
			if (updateType - InputUpdateType.Dynamic <= 1 || updateType == InputUpdateType.BeforeRender || updateType == InputUpdateType.Manual)
			{
				return this.m_PlayerStateBuffers;
			}
			throw new ArgumentException("Unrecognized InputUpdateType: " + updateType.ToString(), "updateType");
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00052F3D File Offset: 0x0005113D
		public unsafe static void* GetFrontBufferForDevice(int deviceIndex)
		{
			return InputStateBuffers.s_CurrentBuffers.GetFrontBuffer(deviceIndex);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00052F4A File Offset: 0x0005114A
		public unsafe static void* GetBackBufferForDevice(int deviceIndex)
		{
			return InputStateBuffers.s_CurrentBuffers.GetBackBuffer(deviceIndex);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00052F57 File Offset: 0x00051157
		public static void SwitchTo(InputStateBuffers buffers, InputUpdateType update)
		{
			InputStateBuffers.s_CurrentBuffers = buffers.GetDoubleBuffersFor(update);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00052F68 File Offset: 0x00051168
		public unsafe void AllocateAll(InputDevice[] devices, int deviceCount)
		{
			this.sizePerBuffer = InputStateBuffers.ComputeSizeOfSingleStateBuffer(devices, deviceCount);
			if (this.sizePerBuffer == 0U)
			{
				return;
			}
			this.sizePerBuffer = this.sizePerBuffer.AlignToMultipleOf(4U);
			uint mappingTableSizePerBuffer = (uint)(deviceCount * sizeof(void*) * 2);
			this.totalSize = 0U;
			this.totalSize += this.sizePerBuffer * 2U;
			this.totalSize += mappingTableSizePerBuffer;
			this.totalSize += this.sizePerBuffer * 3U;
			this.m_AllBuffers = UnsafeUtility.Malloc((long)((ulong)this.totalSize), 4, Allocator.Persistent);
			UnsafeUtility.MemClear(this.m_AllBuffers, (long)((ulong)this.totalSize));
			byte* ptr = (byte*)this.m_AllBuffers;
			this.m_PlayerStateBuffers = InputStateBuffers.SetUpDeviceToBufferMappings(deviceCount, ref ptr, this.sizePerBuffer, mappingTableSizePerBuffer);
			this.defaultStateBuffer = (void*)ptr;
			this.noiseMaskBuffer = (void*)(ptr + this.sizePerBuffer);
			this.resetMaskBuffer = (void*)(ptr + this.sizePerBuffer * 2U);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00053050 File Offset: 0x00051250
		private unsafe static InputStateBuffers.DoubleBuffers SetUpDeviceToBufferMappings(int deviceCount, ref byte* bufferPtr, uint sizePerBuffer, uint mappingTableSizePerBuffer)
		{
			byte* front = bufferPtr;
			byte* back = bufferPtr + sizePerBuffer;
			void** mappings = bufferPtr / (IntPtr)sizeof(void*) + sizePerBuffer * 2U;
			bufferPtr += (IntPtr)((UIntPtr)(sizePerBuffer * 2U + mappingTableSizePerBuffer));
			InputStateBuffers.DoubleBuffers buffers = new InputStateBuffers.DoubleBuffers
			{
				deviceToBufferMapping = mappings,
				deviceCount = deviceCount
			};
			for (int i = 0; i < deviceCount; i++)
			{
				int deviceIndex = i;
				buffers.SetFrontBuffer(deviceIndex, (void*)front);
				buffers.SetBackBuffer(deviceIndex, (void*)back);
			}
			return buffers;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000530C0 File Offset: 0x000512C0
		public void FreeAll()
		{
			if (this.m_AllBuffers != null)
			{
				UnsafeUtility.Free(this.m_AllBuffers, Allocator.Persistent);
				this.m_AllBuffers = null;
			}
			this.m_PlayerStateBuffers = default(InputStateBuffers.DoubleBuffers);
			InputStateBuffers.s_CurrentBuffers = default(InputStateBuffers.DoubleBuffers);
			if (InputStateBuffers.s_DefaultStateBuffer == this.defaultStateBuffer)
			{
				InputStateBuffers.s_DefaultStateBuffer = null;
			}
			this.defaultStateBuffer = null;
			if (InputStateBuffers.s_NoiseMaskBuffer == this.noiseMaskBuffer)
			{
				InputStateBuffers.s_NoiseMaskBuffer = null;
			}
			if (InputStateBuffers.s_ResetMaskBuffer == this.resetMaskBuffer)
			{
				InputStateBuffers.s_ResetMaskBuffer = null;
			}
			this.noiseMaskBuffer = null;
			this.resetMaskBuffer = null;
			this.totalSize = 0U;
			this.sizePerBuffer = 0U;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00053164 File Offset: 0x00051364
		public void MigrateAll(InputDevice[] devices, int deviceCount, InputStateBuffers oldBuffers)
		{
			if (oldBuffers.totalSize > 0U)
			{
				InputStateBuffers.MigrateDoubleBuffer(this.m_PlayerStateBuffers, devices, deviceCount, oldBuffers.m_PlayerStateBuffers);
				InputStateBuffers.MigrateSingleBuffer(this.defaultStateBuffer, devices, deviceCount, oldBuffers.defaultStateBuffer);
				InputStateBuffers.MigrateSingleBuffer(this.noiseMaskBuffer, devices, deviceCount, oldBuffers.noiseMaskBuffer);
				InputStateBuffers.MigrateSingleBuffer(this.resetMaskBuffer, devices, deviceCount, oldBuffers.resetMaskBuffer);
			}
			uint newOffset = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice device = devices[i];
				uint oldOffset = device.m_StateBlock.byteOffset;
				if (oldOffset == 4294967295U)
				{
					device.m_StateBlock.byteOffset = 0U;
					if (newOffset != 0U)
					{
						device.BakeOffsetIntoStateBlockRecursive(newOffset);
					}
				}
				else
				{
					uint delta = newOffset - oldOffset;
					if (delta != 0U)
					{
						device.BakeOffsetIntoStateBlockRecursive(delta);
					}
				}
				newOffset = InputStateBuffers.NextDeviceOffset(newOffset, device);
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0005321C File Offset: 0x0005141C
		private unsafe static void MigrateDoubleBuffer(InputStateBuffers.DoubleBuffers newBuffer, InputDevice[] devices, int deviceCount, InputStateBuffers.DoubleBuffers oldBuffer)
		{
			if (!newBuffer.valid)
			{
				return;
			}
			if (!oldBuffer.valid)
			{
				return;
			}
			uint newStateBlockOffset = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice device = devices[i];
				if (device.m_StateBlock.byteOffset == 4294967295U)
				{
					break;
				}
				int oldDeviceIndex = device.m_DeviceIndex;
				int newDeviceIndex = i;
				uint numBytes = device.m_StateBlock.alignedSizeInBytes;
				byte* oldFrontPtr = (byte*)oldBuffer.GetFrontBuffer(oldDeviceIndex) + device.m_StateBlock.byteOffset;
				byte* oldBackPtr = (byte*)oldBuffer.GetBackBuffer(oldDeviceIndex) + device.m_StateBlock.byteOffset;
				byte* newFrontPtr = (byte*)newBuffer.GetFrontBuffer(newDeviceIndex) + newStateBlockOffset;
				void* ptr = (void*)((byte*)newBuffer.GetBackBuffer(newDeviceIndex) + newStateBlockOffset);
				UnsafeUtility.MemCpy((void*)newFrontPtr, (void*)oldFrontPtr, (long)((ulong)numBytes));
				UnsafeUtility.MemCpy(ptr, (void*)oldBackPtr, (long)((ulong)numBytes));
				newStateBlockOffset = InputStateBuffers.NextDeviceOffset(newStateBlockOffset, device);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000532E0 File Offset: 0x000514E0
		private unsafe static void MigrateSingleBuffer(void* newBuffer, InputDevice[] devices, int deviceCount, void* oldBuffer)
		{
			uint newStateBlockOffset = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice device = devices[i];
				if (device.m_StateBlock.byteOffset == 4294967295U)
				{
					break;
				}
				uint numBytes = device.m_StateBlock.alignedSizeInBytes;
				byte* oldStatePtr = (byte*)oldBuffer + device.m_StateBlock.byteOffset;
				UnsafeUtility.MemCpy((void*)((byte*)newBuffer + newStateBlockOffset), (void*)oldStatePtr, (long)((ulong)numBytes));
				newStateBlockOffset = InputStateBuffers.NextDeviceOffset(newStateBlockOffset, device);
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00053340 File Offset: 0x00051540
		private static uint ComputeSizeOfSingleStateBuffer(InputDevice[] devices, int deviceCount)
		{
			uint sizeInBytes = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				sizeInBytes = InputStateBuffers.NextDeviceOffset(sizeInBytes, devices[i]);
			}
			return sizeInBytes;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00053368 File Offset: 0x00051568
		private static uint NextDeviceOffset(uint currentOffset, InputDevice device)
		{
			uint sizeOfDevice = device.m_StateBlock.alignedSizeInBytes;
			if (sizeOfDevice == 0U)
			{
				throw new ArgumentException(string.Format("Device '{0}' has a zero-size state buffer", device), "device");
			}
			return currentOffset + sizeOfDevice.AlignToMultipleOf(4U);
		}

		// Token: 0x04000A9A RID: 2714
		public uint sizePerBuffer;

		// Token: 0x04000A9B RID: 2715
		public uint totalSize;

		// Token: 0x04000A9C RID: 2716
		public unsafe void* defaultStateBuffer;

		// Token: 0x04000A9D RID: 2717
		public unsafe void* noiseMaskBuffer;

		// Token: 0x04000A9E RID: 2718
		public unsafe void* resetMaskBuffer;

		// Token: 0x04000A9F RID: 2719
		private unsafe void* m_AllBuffers;

		// Token: 0x04000AA0 RID: 2720
		internal InputStateBuffers.DoubleBuffers m_PlayerStateBuffers;

		// Token: 0x04000AA1 RID: 2721
		internal unsafe static void* s_DefaultStateBuffer;

		// Token: 0x04000AA2 RID: 2722
		internal unsafe static void* s_NoiseMaskBuffer;

		// Token: 0x04000AA3 RID: 2723
		internal unsafe static void* s_ResetMaskBuffer;

		// Token: 0x04000AA4 RID: 2724
		internal static InputStateBuffers.DoubleBuffers s_CurrentBuffers;

		// Token: 0x020001D9 RID: 473
		[Serializable]
		internal struct DoubleBuffers
		{
			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x06001191 RID: 4497 RVA: 0x000533A3 File Offset: 0x000515A3
			public bool valid
			{
				get
				{
					return this.deviceToBufferMapping != null;
				}
			}

			// Token: 0x06001192 RID: 4498 RVA: 0x000533B2 File Offset: 0x000515B2
			public unsafe void SetFrontBuffer(int deviceIndex, void* ptr)
			{
				if (deviceIndex < this.deviceCount)
				{
					*(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = ptr;
				}
			}

			// Token: 0x06001193 RID: 4499 RVA: 0x000533D1 File Offset: 0x000515D1
			public unsafe void SetBackBuffer(int deviceIndex, void* ptr)
			{
				if (deviceIndex < this.deviceCount)
				{
					*(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2 + 1) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = ptr;
				}
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x000533F2 File Offset: 0x000515F2
			public unsafe void* GetFrontBuffer(int deviceIndex)
			{
				if (deviceIndex < this.deviceCount)
				{
					return *(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*));
				}
				return null;
			}

			// Token: 0x06001195 RID: 4501 RVA: 0x00053413 File Offset: 0x00051613
			public unsafe void* GetBackBuffer(int deviceIndex)
			{
				if (deviceIndex < this.deviceCount)
				{
					return *(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2 + 1) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*));
				}
				return null;
			}

			// Token: 0x06001196 RID: 4502 RVA: 0x00053438 File Offset: 0x00051638
			public unsafe void SwapBuffers(int deviceIndex)
			{
				if (!this.valid)
				{
					return;
				}
				void* front = this.GetFrontBuffer(deviceIndex);
				void* back = this.GetBackBuffer(deviceIndex);
				this.SetFrontBuffer(deviceIndex, back);
				this.SetBackBuffer(deviceIndex, front);
			}

			// Token: 0x04000AA5 RID: 2725
			public unsafe void** deviceToBufferMapping;

			// Token: 0x04000AA6 RID: 2726
			public int deviceCount;
		}
	}
}
