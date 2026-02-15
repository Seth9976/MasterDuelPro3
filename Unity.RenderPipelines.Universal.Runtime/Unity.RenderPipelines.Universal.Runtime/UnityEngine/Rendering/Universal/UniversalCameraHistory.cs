using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B6 RID: 438
	public class UniversalCameraHistory : ICameraHistoryReadAccess, ICameraHistoryWriteAccess, IPerFrameHistoryAccessTracker, IDisposable
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0002E714 File Offset: 0x0002C914
		public void RequestAccess<Type>() where Type : ContextItem
		{
			uint index = UniversalCameraHistory.TypeId<Type>.value;
			if ((ulong)index >= (ulong)((long)this.m_Items.Length))
			{
				UniversalCameraHistory.Item[] items = new UniversalCameraHistory.Item[math.max((long)((ulong)math.ceilpow2(UniversalCameraHistory.s_TypeCount)), (long)(this.m_Items.Length * 2))];
				for (int i = 0; i < this.m_Items.Length; i++)
				{
					items[i] = this.m_Items[i];
				}
				this.m_Items = items;
			}
			this.m_Items[(int)index].requestVersion = this.m_Version;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002E79C File Offset: 0x0002C99C
		public Type GetHistoryForRead<Type>() where Type : ContextItem
		{
			uint index = UniversalCameraHistory.TypeId<Type>.value;
			if ((ulong)index >= (ulong)((long)this.m_Items.Length))
			{
				return default(Type);
			}
			if (!this.IsValid((int)index))
			{
				return default(Type);
			}
			return (Type)((object)this.m_Items[(int)index].storage);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
		public bool IsAccessRequested<Type>() where Type : ContextItem
		{
			uint index = UniversalCameraHistory.TypeId<Type>.value;
			return (ulong)index < (ulong)((long)this.m_Items.Length) && this.IsValidRequest((int)index);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002E81C File Offset: 0x0002CA1C
		public Type GetHistoryForWrite<Type>() where Type : ContextItem, new()
		{
			uint index = UniversalCameraHistory.TypeId<Type>.value;
			if ((ulong)index >= (ulong)((long)this.m_Items.Length))
			{
				return default(Type);
			}
			if (!this.IsValidRequest((int)index))
			{
				return default(Type);
			}
			if (this.m_Items[(int)index].storage == null)
			{
				UniversalCameraHistory.Item[] items = this.m_Items;
				uint num = index;
				items[(int)num].storage = new Type();
				CameraHistoryItem hi = items[(int)num].storage as CameraHistoryItem;
				if (hi != null)
				{
					hi.OnCreate(this.m_HistoryTextures, index);
				}
			}
			this.m_Items[(int)index].writeVersion = this.m_Version;
			return (Type)((object)this.m_Items[(int)index].storage);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002E8D0 File Offset: 0x0002CAD0
		public bool IsWritten<Type>() where Type : ContextItem
		{
			uint index = UniversalCameraHistory.TypeId<Type>.value;
			return (ulong)index < (ulong)((long)this.m_Items.Length) && this.m_Items[(int)index].writeVersion == this.m_Version;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600096A RID: 2410 RVA: 0x0002E90C File Offset: 0x0002CB0C
		// (remove) Token: 0x0600096B RID: 2411 RVA: 0x0002E944 File Offset: 0x0002CB44
		public event ICameraHistoryReadAccess.HistoryRequestDelegate OnGatherHistoryRequests;

		// Token: 0x0600096C RID: 2412 RVA: 0x0002E97C File Offset: 0x0002CB7C
		internal UniversalCameraHistory()
		{
			for (int i = 0; i < this.m_Items.Length; i++)
			{
				this.m_Items[i].Reset();
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002E9CC File Offset: 0x0002CBCC
		public void Dispose()
		{
			for (int i = 0; i < this.m_Items.Length; i++)
			{
				this.m_Items[i].Reset();
			}
			this.m_HistoryTextures.ReleaseAll();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002EA08 File Offset: 0x0002CC08
		internal void GatherHistoryRequests()
		{
			ICameraHistoryReadAccess.HistoryRequestDelegate onGatherHistoryRequests = this.OnGatherHistoryRequests;
			if (onGatherHistoryRequests == null)
			{
				return;
			}
			onGatherHistoryRequests(this);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002EA1B File Offset: 0x0002CC1B
		private bool IsValidRequest(int i)
		{
			return this.m_Version - this.m_Items[i].requestVersion < 2;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002EA38 File Offset: 0x0002CC38
		private bool IsValid(int i)
		{
			return this.m_Version - this.m_Items[i].writeVersion < 2;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002EA58 File Offset: 0x0002CC58
		internal void ReleaseUnusedHistory()
		{
			for (int i = 0; i < this.m_Items.Length; i++)
			{
				if (!this.IsValidRequest(i) && !this.IsValid(i))
				{
					this.m_Items[i].Reset();
				}
			}
			this.m_Version++;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002EAA9 File Offset: 0x0002CCA9
		internal void SwapAndSetReferenceSize(int cameraWidth, int cameraHeight)
		{
			this.m_HistoryTextures.SwapAndSetReferenceSize(cameraWidth, cameraHeight);
		}

		// Token: 0x040009B4 RID: 2484
		private const int k_ValidVersionCount = 2;

		// Token: 0x040009B5 RID: 2485
		private static uint s_TypeCount;

		// Token: 0x040009B6 RID: 2486
		private UniversalCameraHistory.Item[] m_Items = new UniversalCameraHistory.Item[32];

		// Token: 0x040009B7 RID: 2487
		private int m_Version;

		// Token: 0x040009B8 RID: 2488
		private BufferedRTHandleSystem m_HistoryTextures = new BufferedRTHandleSystem();

		// Token: 0x020001B7 RID: 439
		private static class TypeId<T>
		{
			// Token: 0x040009BA RID: 2490
			public static uint value = UniversalCameraHistory.s_TypeCount++;
		}

		// Token: 0x020001B8 RID: 440
		private struct Item
		{
			// Token: 0x06000974 RID: 2420 RVA: 0x0002EACC File Offset: 0x0002CCCC
			public void Reset()
			{
				ContextItem contextItem = this.storage;
				if (contextItem != null)
				{
					contextItem.Reset();
				}
				this.requestVersion = -2;
				this.writeVersion = -2;
			}

			// Token: 0x040009BB RID: 2491
			public ContextItem storage;

			// Token: 0x040009BC RID: 2492
			public int requestVersion;

			// Token: 0x040009BD RID: 2493
			public int writeVersion;
		}
	}
}
