using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000148 RID: 328
	[NativeHeader("Runtime/Export/Math/Gradient.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Gradient : IEquatable<Gradient>
	{
		// Token: 0x06000D88 RID: 3464
		[FreeFunction(Name = "Gradient_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		// Token: 0x06000D89 RID: 3465 RVA: 0x0001A6A8 File Offset: 0x000188A8
		[FreeFunction(Name = "Gradient_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		private void Cleanup()
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Gradient.Cleanup_Injected(intPtr);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0001A6CC File Offset: 0x000188CC
		[FreeFunction("Gradient_Bindings::Internal_Equals", IsThreadSafe = true, HasExplicitThis = true)]
		private bool Internal_Equals(IntPtr other)
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Gradient.Internal_Equals_Injected(intPtr, other);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0001A6EF File Offset: 0x000188EF
		[RequiredByNativeCode]
		public Gradient()
		{
			this.m_Ptr = Gradient.Init();
			this.m_RequiresNativeCleanup = true;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0001A70B File Offset: 0x0001890B
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal Gradient(IntPtr ptr)
		{
			this.m_Ptr = ptr;
			this.m_RequiresNativeCleanup = false;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0001A724 File Offset: 0x00018924
		protected override void Finalize()
		{
			try
			{
				bool requiresNativeCleanup = this.m_RequiresNativeCleanup;
				if (requiresNativeCleanup)
				{
					this.Cleanup();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001A760 File Offset: 0x00018960
		[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
		public Color Evaluate(float time)
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Gradient.Evaluate_Injected(intPtr, time, out color);
			return color;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0001A788 File Offset: 0x00018988
		public GradientColorKey[] colorKeys
		{
			[FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			get
			{
				GradientColorKey[] array2;
				try
				{
					IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Gradient.get_colorKeys_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					GradientColorKey[] array;
					blittableArrayWrapper.Unmarshal<GradientColorKey>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0001A7CC File Offset: 0x000189CC
		public GradientAlphaKey[] alphaKeys
		{
			[FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			get
			{
				GradientAlphaKey[] array2;
				try
				{
					IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Gradient.get_alphaKeys_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					GradientAlphaKey[] array;
					blittableArrayWrapper.Unmarshal<GradientAlphaKey>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0001A810 File Offset: 0x00018A10
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0001A834 File Offset: 0x00018A34
		[NativeProperty(IsThreadSafe = true)]
		public GradientMode mode
		{
			get
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Gradient.get_mode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Gradient.set_mode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0001A858 File Offset: 0x00018A58
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0001A87C File Offset: 0x00018A7C
		[NativeProperty(IsThreadSafe = true)]
		public ColorSpace colorSpace
		{
			get
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Gradient.get_colorSpace_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Gradient.set_colorSpace_Injected(intPtr, value);
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
		public unsafe void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys)
		{
			IntPtr intPtr = Gradient.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<GradientColorKey> span = new Span<GradientColorKey>(colorKeys);
			fixed (GradientColorKey* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<GradientAlphaKey> span2 = new Span<GradientAlphaKey>(alphaKeys);
				fixed (GradientAlphaKey* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					Gradient.SetKeys_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001A914 File Offset: 0x00018B14
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
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = o.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((Gradient)o);
				}
			}
			return flag2;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001A968 File Offset: 0x00018B68
		public bool Equals(Gradient other)
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
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = this.m_Ptr.Equals(other.m_Ptr);
					flag2 = flag4 || this.Internal_Equals(other.m_Ptr);
				}
			}
			return flag2;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06000D99 RID: 3481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cleanup_Injected(IntPtr _unity_self);

		// Token: 0x06000D9A RID: 3482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Equals_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x06000D9B RID: 3483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Evaluate_Injected(IntPtr _unity_self, float time, out Color ret);

		// Token: 0x06000D9C RID: 3484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_colorKeys_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000D9D RID: 3485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_alphaKeys_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000D9E RID: 3486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GradientMode get_mode_Injected(IntPtr _unity_self);

		// Token: 0x06000D9F RID: 3487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mode_Injected(IntPtr _unity_self, GradientMode value);

		// Token: 0x06000DA0 RID: 3488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorSpace get_colorSpace_Injected(IntPtr _unity_self);

		// Token: 0x06000DA1 RID: 3489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colorSpace_Injected(IntPtr _unity_self, ColorSpace value);

		// Token: 0x06000DA2 RID: 3490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeys_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colorKeys, ref ManagedSpanWrapper alphaKeys);

		// Token: 0x0400057E RID: 1406
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x0400057F RID: 1407
		private bool m_RequiresNativeCleanup;

		// Token: 0x02000149 RID: 329
		internal static class BindingsMarshaller
		{
			// Token: 0x06000DA3 RID: 3491 RVA: 0x0001A9DD File Offset: 0x00018BDD
			public static IntPtr ConvertToNative(Gradient graident)
			{
				return graident.m_Ptr;
			}
		}
	}
}
