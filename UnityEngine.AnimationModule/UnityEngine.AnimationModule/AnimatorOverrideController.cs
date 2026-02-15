using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[NativeHeader("Modules/Animation/AnimatorOverrideController.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[UsedByNativeCode]
	public class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x06000207 RID: 519 RVA: 0x000053BE File Offset: 0x000035BE
		public AnimatorOverrideController()
		{
			AnimatorOverrideController.Internal_Create(this, null);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000053D7 File Offset: 0x000035D7
		public AnimatorOverrideController(RuntimeAnimatorController controller)
		{
			AnimatorOverrideController.Internal_Create(this, controller);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000053F0 File Offset: 0x000035F0
		[FreeFunction("AnimationBindings::CreateAnimatorOverrideController")]
		private static void Internal_Create([Writable] AnimatorOverrideController self, RuntimeAnimatorController controller)
		{
			AnimatorOverrideController.Internal_Create_Injected(self, Object.MarshalledUnityObject.Marshal<RuntimeAnimatorController>(controller));
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000540C File Offset: 0x0000360C
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00005434 File Offset: 0x00003634
		public RuntimeAnimatorController runtimeAnimatorController
		{
			[NativeMethod("GetAnimatorController")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RuntimeAnimatorController>(AnimatorOverrideController.get_runtimeAnimatorController_Injected(intPtr));
			}
			[NativeMethod("SetAnimatorController")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimatorOverrideController.set_runtimeAnimatorController_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RuntimeAnimatorController>(value));
			}
		}

		// Token: 0x1700004E RID: 78
		public AnimationClip this[string name]
		{
			get
			{
				return this.Internal_GetClipByName(name, true);
			}
			set
			{
				this.Internal_SetClipByName(name, value);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005484 File Offset: 0x00003684
		[NativeMethod("GetClip")]
		private unsafe AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip)
		{
			AnimationClip animationClip;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AnimatorOverrideController.Internal_GetClipByName_Injected(intPtr, ref managedSpanWrapper, returnEffectiveClip);
			}
			finally
			{
				IntPtr intPtr2;
				animationClip = Unmarshal.UnmarshalUnityObject<AnimationClip>(intPtr2);
				char* ptr = null;
			}
			return animationClip;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000054F4 File Offset: 0x000036F4
		[NativeMethod("SetClip")]
		private unsafe void Internal_SetClipByName(string name, AnimationClip clip)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AnimatorOverrideController.Internal_SetClipByName_Injected(intPtr, ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<AnimationClip>(clip));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x1700004F RID: 79
		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return this.GetClip(clip, true);
			}
			set
			{
				this.SetClip(clip, value, true);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005588 File Offset: 0x00003788
		private AnimationClip GetClip(AnimationClip originalClip, bool returnEffectiveClip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorOverrideController.GetClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(originalClip), returnEffectiveClip));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000055B8 File Offset: 0x000037B8
		private void SetClip(AnimationClip originalClip, AnimationClip overrideClip, bool notify)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimatorOverrideController.SetClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(originalClip), Object.MarshalledUnityObject.Marshal<AnimationClip>(overrideClip), notify);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000055E8 File Offset: 0x000037E8
		private void SendNotification()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimatorOverrideController.SendNotification_Injected(intPtr);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000560C File Offset: 0x0000380C
		private AnimationClip GetOriginalClip(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorOverrideController.GetOriginalClip_Injected(intPtr, index));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005634 File Offset: 0x00003834
		private AnimationClip GetOverrideClip(AnimationClip originalClip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorOverrideController.GetOverrideClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(originalClip)));
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00005664 File Offset: 0x00003864
		public int overridesCount
		{
			[NativeMethod("GetOriginalClipsCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimatorOverrideController.get_overridesCount_Injected(intPtr);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005688 File Offset: 0x00003888
		public void GetOverrides(List<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			int count = this.overridesCount;
			bool flag2 = overrides.Capacity < count;
			if (flag2)
			{
				overrides.Capacity = count;
			}
			overrides.Clear();
			for (int i = 0; i < count; i++)
			{
				AnimationClip originalClip = this.GetOriginalClip(i);
				overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(originalClip, this.GetOverrideClip(originalClip)));
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005700 File Offset: 0x00003900
		public void ApplyOverrides(IList<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this.SetClip(overrides[i].Key, overrides[i].Value, false);
			}
			this.SendNotification();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005764 File Offset: 0x00003964
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000057C8 File Offset: 0x000039C8
		[Obsolete("AnimatorOverrideController.clips property is deprecated. Use AnimatorOverrideController.GetOverrides and AnimatorOverrideController.ApplyOverrides instead.")]
		public AnimationClipPair[] clips
		{
			get
			{
				int count = this.overridesCount;
				AnimationClipPair[] clipPair = new AnimationClipPair[count];
				for (int i = 0; i < count; i++)
				{
					clipPair[i] = new AnimationClipPair();
					clipPair[i].originalClip = this.GetOriginalClip(i);
					clipPair[i].overrideClip = this.GetOverrideClip(clipPair[i].originalClip);
				}
				return clipPair;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.SetClip(value[i].originalClip, value[i].overrideClip, false);
				}
				this.SendNotification();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005808 File Offset: 0x00003A08
		[NativeConditional("UNITY_EDITOR")]
		internal void PerformOverrideClipListCleanup()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimatorOverrideController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimatorOverrideController.PerformOverrideClipListCleanup_Injected(intPtr);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000582C File Offset: 0x00003A2C
		[NativeConditional("UNITY_EDITOR")]
		[RequiredByNativeCode]
		internal static void OnInvalidateOverrideController(AnimatorOverrideController controller)
		{
			bool flag = controller.OnOverrideControllerDirty != null;
			if (flag)
			{
				controller.OnOverrideControllerDirty();
			}
		}

		// Token: 0x0600021E RID: 542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create_Injected([Writable] AnimatorOverrideController self, IntPtr controller);

		// Token: 0x0600021F RID: 543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_runtimeAnimatorController_Injected(IntPtr _unity_self);

		// Token: 0x06000220 RID: 544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_runtimeAnimatorController_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000221 RID: 545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetClipByName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, bool returnEffectiveClip);

		// Token: 0x06000222 RID: 546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetClipByName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, IntPtr clip);

		// Token: 0x06000223 RID: 547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetClip_Injected(IntPtr _unity_self, IntPtr originalClip, bool returnEffectiveClip);

		// Token: 0x06000224 RID: 548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetClip_Injected(IntPtr _unity_self, IntPtr originalClip, IntPtr overrideClip, bool notify);

		// Token: 0x06000225 RID: 549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendNotification_Injected(IntPtr _unity_self);

		// Token: 0x06000226 RID: 550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOriginalClip_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000227 RID: 551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOverrideClip_Injected(IntPtr _unity_self, IntPtr originalClip);

		// Token: 0x06000228 RID: 552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_overridesCount_Injected(IntPtr _unity_self);

		// Token: 0x06000229 RID: 553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PerformOverrideClipListCleanup_Injected(IntPtr _unity_self);

		// Token: 0x04000066 RID: 102
		internal AnimatorOverrideController.OnOverrideControllerDirtyCallback OnOverrideControllerDirty;

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x0600022B RID: 555
		internal delegate void OnOverrideControllerDirtyCallback();
	}
}
