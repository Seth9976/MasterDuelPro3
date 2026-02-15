using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200008F RID: 143
	[NativeHeader("Runtime/Math/AnimationCurve.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationCurve : IEquatable<AnimationCurve>
	{
		// Token: 0x06000230 RID: 560
		[FreeFunction("AnimationCurveBindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000231 RID: 561 RVA: 0x00005AE0 File Offset: 0x00003CE0
		[FreeFunction("AnimationCurveBindings::Internal_Create", IsThreadSafe = true)]
		private unsafe static IntPtr Internal_Create(Keyframe[] keys)
		{
			Span<Keyframe> span = new Span<Keyframe>(keys);
			IntPtr intPtr;
			fixed (Keyframe* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = AnimationCurve.Internal_Create_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00005B18 File Offset: 0x00003D18
		[FreeFunction("AnimationCurveBindings::Internal_Equals", HasExplicitThis = true, IsThreadSafe = true)]
		private bool Internal_Equals(IntPtr other)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.Internal_Equals_Injected(intPtr, other);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00005B3C File Offset: 0x00003D3C
		[FreeFunction("AnimationCurveBindings::Internal_CopyFrom", HasExplicitThis = true, IsThreadSafe = true)]
		private void Internal_CopyFrom(IntPtr other)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.Internal_CopyFrom_Injected(intPtr, other);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00005B60 File Offset: 0x00003D60
		protected override void Finalize()
		{
			try
			{
				bool requiresNativeCleanup = this.m_RequiresNativeCleanup;
				if (requiresNativeCleanup)
				{
					AnimationCurve.Internal_Destroy(this.m_Ptr);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00005BA0 File Offset: 0x00003DA0
		[ThreadSafe]
		public float Evaluate(float time)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.Evaluate_Injected(intPtr, time);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005BC4 File Offset: 0x00003DC4
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00005BDC File Offset: 0x00003DDC
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00005BE8 File Offset: 0x00003DE8
		[FreeFunction("AnimationCurveBindings::AddKeySmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		public int AddKey(float time, float value)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.AddKey_Injected(intPtr, time, value);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00005C0C File Offset: 0x00003E0C
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00005C28 File Offset: 0x00003E28
		[NativeMethod("AddKey", IsThreadSafe = true)]
		private int AddKey_Internal(Keyframe key)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.AddKey_Internal_Injected(intPtr, ref key);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00005C4C File Offset: 0x00003E4C
		[FreeFunction("AnimationCurveBindings::MoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		public int MoveKey(int index, Keyframe key)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.MoveKey_Injected(intPtr, index, ref key);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00005C74 File Offset: 0x00003E74
		[FreeFunction("AnimationCurveBindings::ClearKeys", HasExplicitThis = true, IsThreadSafe = true)]
		public void ClearKeys()
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.ClearKeys_Injected(intPtr);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00005C98 File Offset: 0x00003E98
		[FreeFunction("AnimationCurveBindings::RemoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		public void RemoveKey(int index)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.RemoveKey_Injected(intPtr, index);
		}

		// Token: 0x17000052 RID: 82
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey(index);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public int length
		{
			[NativeMethod("GetKeyCount", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationCurve.get_length_Injected(intPtr);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00005CFC File Offset: 0x00003EFC
		[FreeFunction("AnimationCurveBindings::SetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe void SetKeys(Keyframe[] keys)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Keyframe> span = new Span<Keyframe>(keys);
			fixed (Keyframe* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				AnimationCurve.SetKeys_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005D44 File Offset: 0x00003F44
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::GetKey", HasExplicitThis = true, IsThreadSafe = true)]
		private Keyframe GetKey(int index)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Keyframe keyframe;
			AnimationCurve.GetKey_Injected(intPtr, index, out keyframe);
			return keyframe;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00005D6C File Offset: 0x00003F6C
		[FreeFunction("AnimationCurveBindings::GetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		private Keyframe[] GetKeys()
		{
			Keyframe[] array2;
			try
			{
				IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				AnimationCurve.GetKeys_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Keyframe[] array;
				blittableArrayWrapper.Unmarshal<Keyframe>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00005DB0 File Offset: 0x00003FB0
		[FreeFunction("AnimationCurveBindings::GetHashCode", HasExplicitThis = true, IsThreadSafe = true)]
		public override int GetHashCode()
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AnimationCurve.GetHashCode_Injected(intPtr);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00005DD4 File Offset: 0x00003FD4
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::SmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		public void SmoothTangents(int index, float weight)
		{
			IntPtr intPtr = AnimationCurve.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationCurve.SmoothTangents_Injected(intPtr, index, weight);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe key = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { key });
			}
			else
			{
				float tangent = (valueEnd - valueStart) / (timeEnd - timeStart);
				Keyframe[] keys = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, tangent),
					new Keyframe(timeEnd, valueEnd, tangent, 0f)
				};
				animationCurve = new AnimationCurve(keys);
			}
			return animationCurve;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00005E74 File Offset: 0x00004074
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe key = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { key });
			}
			else
			{
				Keyframe[] keys = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, 0f),
					new Keyframe(timeEnd, valueEnd, 0f, 0f)
				};
				animationCurve = new AnimationCurve(keys);
			}
			return animationCurve;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005EEB File Offset: 0x000040EB
		public AnimationCurve(params Keyframe[] keys)
		{
			this.m_Ptr = AnimationCurve.Internal_Create(keys);
			this.m_RequiresNativeCleanup = true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005F08 File Offset: 0x00004108
		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.m_Ptr = AnimationCurve.Internal_Create(null);
			this.m_RequiresNativeCleanup = true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005F25 File Offset: 0x00004125
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal AnimationCurve(IntPtr ptr)
		{
			this.m_Ptr = ptr;
			this.m_RequiresNativeCleanup = false;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00005F40 File Offset: 0x00004140
		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == o;
				flag2 = flag3 || (o.GetType() == base.GetType() && this.Equals((AnimationCurve)o));
			}
			return flag2;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005F90 File Offset: 0x00004190
		public bool Equals(AnimationCurve other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				flag2 = flag3 || this.m_Ptr.Equals(other.m_Ptr) || this.Internal_Equals(other.m_Ptr);
			}
			return flag2;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005FE3 File Offset: 0x000041E3
		public void CopyFrom(AnimationCurve other)
		{
			this.Internal_CopyFrom(other.m_Ptr);
		}

		// Token: 0x0600024D RID: 589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create_Injected(ref ManagedSpanWrapper keys);

		// Token: 0x0600024E RID: 590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Equals_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x0600024F RID: 591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CopyFrom_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x06000250 RID: 592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Evaluate_Injected(IntPtr _unity_self, float time);

		// Token: 0x06000251 RID: 593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddKey_Injected(IntPtr _unity_self, float time, float value);

		// Token: 0x06000252 RID: 594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddKey_Internal_Injected(IntPtr _unity_self, [In] ref Keyframe key);

		// Token: 0x06000253 RID: 595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MoveKey_Injected(IntPtr _unity_self, int index, [In] ref Keyframe key);

		// Token: 0x06000254 RID: 596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearKeys_Injected(IntPtr _unity_self);

		// Token: 0x06000255 RID: 597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveKey_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000256 RID: 598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_length_Injected(IntPtr _unity_self);

		// Token: 0x06000257 RID: 599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeys_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keys);

		// Token: 0x06000258 RID: 600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKey_Injected(IntPtr _unity_self, int index, out Keyframe ret);

		// Token: 0x06000259 RID: 601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKeys_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x0600025A RID: 602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetHashCode_Injected(IntPtr _unity_self);

		// Token: 0x0600025B RID: 603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SmoothTangents_Injected(IntPtr _unity_self, int index, float weight);

		// Token: 0x0400014F RID: 335
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x04000150 RID: 336
		private bool m_RequiresNativeCleanup;

		// Token: 0x02000090 RID: 144
		internal static class BindingsMarshaller
		{
			// Token: 0x0600025C RID: 604 RVA: 0x00005FF3 File Offset: 0x000041F3
			public static AnimationCurve ConvertToManaged(IntPtr ptr)
			{
				return new AnimationCurve(ptr);
			}

			// Token: 0x0600025D RID: 605 RVA: 0x00005FFB File Offset: 0x000041FB
			public static IntPtr ConvertToNative(AnimationCurve animationCurve)
			{
				return animationCurve.m_Ptr;
			}
		}
	}
}
