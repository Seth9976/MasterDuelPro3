using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000084 RID: 132
	[NativeType("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000048B8 File Offset: 0x00002AB8
		public void RegisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent == null;
			if (flag)
			{
				this.m_SpriteChangeEvent = new UnityEvent<SpriteRenderer>();
			}
			this.m_SpriteChangeEvent.AddListener(callback);
			this.hasSpriteChangeEvents = true;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000048F4 File Offset: 0x00002AF4
		public void UnregisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent != null;
			if (flag)
			{
				this.m_SpriteChangeEvent.RemoveListener(callback);
				bool flag2 = this.m_SpriteChangeEvent.GetCallsCount() == 0;
				if (flag2)
				{
					this.hasSpriteChangeEvents = false;
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004938 File Offset: 0x00002B38
		[RequiredByNativeCode]
		private void InvokeSpriteChanged()
		{
			try
			{
				UnityEvent<SpriteRenderer> spriteChangeEvent = this.m_SpriteChangeEvent;
				if (spriteChangeEvent != null)
				{
					spriteChangeEvent.Invoke(this);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000497C File Offset: 0x00002B7C
		internal bool shouldSupportTiling
		{
			[NativeMethod("ShouldSupportTiling")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_shouldSupportTiling_Injected(intPtr);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000049A0 File Offset: 0x00002BA0
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000049C4 File Offset: 0x00002BC4
		internal bool hasSpriteChangeEvents
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_hasSpriteChangeEvents_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_hasSpriteChangeEvents_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000049E8 File Offset: 0x00002BE8
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00004A10 File Offset: 0x00002C10
		public Sprite sprite
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Sprite>(SpriteRenderer.get_sprite_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_sprite_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Sprite>(value));
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004A38 File Offset: 0x00002C38
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00004A5C File Offset: 0x00002C5C
		public SpriteDrawMode drawMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_drawMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_drawMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00004A80 File Offset: 0x00002C80
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public Vector2 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				SpriteRenderer.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00004ACC File Offset: 0x00002CCC
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public float adaptiveModeThreshold
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_adaptiveModeThreshold_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_adaptiveModeThreshold_Injected(intPtr, value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00004B14 File Offset: 0x00002D14
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00004B38 File Offset: 0x00002D38
		public SpriteTileMode tileMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_tileMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_tileMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004B5C File Offset: 0x00002D5C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00004B84 File Offset: 0x00002D84
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				SpriteRenderer.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004BA8 File Offset: 0x00002DA8
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00004BCC File Offset: 0x00002DCC
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00004BF0 File Offset: 0x00002DF0
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00004C14 File Offset: 0x00002E14
		public bool flipX
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_flipX_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_flipX_Injected(intPtr, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00004C38 File Offset: 0x00002E38
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00004C5C File Offset: 0x00002E5C
		public bool flipY
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_flipY_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_flipY_Injected(intPtr, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00004C80 File Offset: 0x00002E80
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public SpriteSortPoint spriteSortPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_spriteSortPoint_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_spriteSortPoint_Injected(intPtr, value);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004CC8 File Offset: 0x00002EC8
		private IntPtr GetCurrentMeshDataPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SpriteRenderer.GetCurrentMeshDataPtr_Injected(intPtr);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004CEC File Offset: 0x00002EEC
		internal unsafe Mesh.MeshDataArray GetCurrentMeshData()
		{
			IntPtr ptr = this.GetCurrentMeshDataPtr();
			bool flag = ptr == IntPtr.Zero;
			Mesh.MeshDataArray meshDataArray;
			if (flag)
			{
				meshDataArray = new Mesh.MeshDataArray(0);
			}
			else
			{
				Mesh.MeshDataArray result = new Mesh.MeshDataArray(1);
				*result.m_Ptrs = ptr;
				meshDataArray = result;
			}
			return meshDataArray;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004D30 File Offset: 0x00002F30
		[NativeMethod(Name = "GetSpriteBounds")]
		internal Bounds Internal_GetSpriteBounds(SpriteDrawMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Bounds bounds;
			SpriteRenderer.Internal_GetSpriteBounds_Injected(intPtr, mode, out bounds);
			return bounds;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004D58 File Offset: 0x00002F58
		internal void GetSecondaryTextureProperties([NotNull] MaterialPropertyBlock mbp)
		{
			if (mbp == null)
			{
				ThrowHelper.ThrowArgumentNullException(mbp, "mbp");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(mbp);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mbp, "mbp");
			}
			SpriteRenderer.GetSecondaryTextureProperties_Injected(intPtr, intPtr2);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004DA0 File Offset: 0x00002FA0
		internal Bounds GetSpriteBounds()
		{
			return this.Internal_GetSpriteBounds(this.drawMode);
		}

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_shouldSupportTiling_Injected(IntPtr _unity_self);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasSpriteChangeEvents_Injected(IntPtr _unity_self);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hasSpriteChangeEvents_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sprite_Injected(IntPtr _unity_self);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sprite_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteDrawMode get_drawMode_Injected(IntPtr _unity_self);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drawMode_Injected(IntPtr _unity_self, SpriteDrawMode value);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060001AF RID: 431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_adaptiveModeThreshold_Injected(IntPtr _unity_self);

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_adaptiveModeThreshold_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteTileMode get_tileMode_Injected(IntPtr _unity_self);

		// Token: 0x060001B2 RID: 434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tileMode_Injected(IntPtr _unity_self, SpriteTileMode value);

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_flipX_Injected(IntPtr _unity_self);

		// Token: 0x060001B8 RID: 440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flipX_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001B9 RID: 441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_flipY_Injected(IntPtr _unity_self);

		// Token: 0x060001BA RID: 442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flipY_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001BB RID: 443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteSortPoint get_spriteSortPoint_Injected(IntPtr _unity_self);

		// Token: 0x060001BC RID: 444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spriteSortPoint_Injected(IntPtr _unity_self, SpriteSortPoint value);

		// Token: 0x060001BD RID: 445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCurrentMeshDataPtr_Injected(IntPtr _unity_self);

		// Token: 0x060001BE RID: 446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetSpriteBounds_Injected(IntPtr _unity_self, SpriteDrawMode mode, out Bounds ret);

		// Token: 0x060001BF RID: 447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSecondaryTextureProperties_Injected(IntPtr _unity_self, IntPtr mbp);

		// Token: 0x04000129 RID: 297
		private UnityEvent<SpriteRenderer> m_SpriteChangeEvent;
	}
}
