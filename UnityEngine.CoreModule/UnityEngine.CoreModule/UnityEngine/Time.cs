using System;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001DC RID: 476
	[NativeHeader("Runtime/Input/TimeManager.h")]
	[StaticAccessor("GetTimeManager()", StaticAccessorType.Dot)]
	public class Time
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001252 RID: 4690
		[NativeProperty("CurTime")]
		public static extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001253 RID: 4691
		[NativeProperty("CurTime")]
		public static extern double timeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00026D98 File Offset: 0x00024F98
		[NativeProperty("CurTimeRational")]
		public static RationalTime timeAsRational
		{
			get
			{
				RationalTime rationalTime;
				Time.get_timeAsRational_Injected(out rationalTime);
				return rationalTime;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001255 RID: 4693
		public static extern float deltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001256 RID: 4694
		public static extern float unscaledTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001257 RID: 4695
		public static extern float fixedUnscaledTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001258 RID: 4696
		public static extern float unscaledDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001259 RID: 4697
		public static extern float fixedDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600125A RID: 4698
		public static extern float maximumDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600125B RID: 4699
		public static extern float smoothDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600125C RID: 4700
		// (set) Token: 0x0600125D RID: 4701
		public static extern float timeScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600125E RID: 4702
		public static extern int frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600125F RID: 4703
		[NativeProperty("RenderFrameCount")]
		public static extern int renderedFrameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001260 RID: 4704
		[NativeProperty("Realtime")]
		public static extern float realtimeSinceStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001261 RID: 4705
		[NativeProperty("Realtime")]
		public static extern double realtimeSinceStartupAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001262 RID: 4706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_timeAsRational_Injected(out RationalTime ret);
	}
}
