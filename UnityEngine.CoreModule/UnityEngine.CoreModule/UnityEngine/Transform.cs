using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EC RID: 492
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Transform/Transform.h")]
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformScriptBindings.h")]
	[NativeHeader("Configuration/UnityConfigure.h")]
	public class Transform : Component, IEnumerable
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x00027CCE File Offset: 0x00025ECE
		protected Transform()
		{
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00027CD8 File Offset: 0x00025ED8
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00027D00 File Offset: 0x00025F00
		public Vector3 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00027D24 File Offset: 0x00025F24
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x00027D4C File Offset: 0x00025F4C
		public Vector3 localPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_localPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_localPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00027D70 File Offset: 0x00025F70
		internal Vector3 GetLocalEulerAngles(RotationOrder order)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.GetLocalEulerAngles_Injected(intPtr, order, out vector);
			return vector;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00027D98 File Offset: 0x00025F98
		internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetLocalEulerAngles_Injected(intPtr, ref euler, order);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00027DC0 File Offset: 0x00025FC0
		[NativeConditional("UNITY_EDITOR")]
		internal void SetLocalEulerHint(Vector3 euler)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetLocalEulerHint_Injected(intPtr, ref euler);
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00027DE4 File Offset: 0x00025FE4
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00027E04 File Offset: 0x00026004
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00027E14 File Offset: 0x00026014
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00027E34 File Offset: 0x00026034
		public Vector3 localEulerAngles
		{
			get
			{
				return this.localRotation.eulerAngles;
			}
			set
			{
				this.localRotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00027E44 File Offset: 0x00026044
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00027E66 File Offset: 0x00026066
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00027E7C File Offset: 0x0002607C
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00027E9E File Offset: 0x0002609E
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00027EB4 File Offset: 0x000260B4
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x00027ED6 File Offset: 0x000260D6
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00027EE8 File Offset: 0x000260E8
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x00027F10 File Offset: 0x00026110
		public Quaternion rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Transform.get_rotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_rotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00027F34 File Offset: 0x00026134
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00027F5C File Offset: 0x0002615C
		public Quaternion localRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Transform.get_localRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_localRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00027F80 File Offset: 0x00026180
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00027F98 File Offset: 0x00026198
		[NativeConditional("UNITY_EDITOR")]
		internal RotationOrder rotationOrder
		{
			get
			{
				return (RotationOrder)this.GetRotationOrderInternal();
			}
			set
			{
				this.SetRotationOrderInternal(value);
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00027FA4 File Offset: 0x000261A4
		[NativeMethod("GetRotationOrder")]
		[NativeConditional("UNITY_EDITOR")]
		internal int GetRotationOrderInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.GetRotationOrderInternal_Injected(intPtr);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00027FC8 File Offset: 0x000261C8
		[NativeMethod("SetRotationOrder")]
		[NativeConditional("UNITY_EDITOR")]
		internal void SetRotationOrderInternal(RotationOrder rotationOrder)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetRotationOrderInternal_Injected(intPtr, rotationOrder);
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00027FEC File Offset: 0x000261EC
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x00028014 File Offset: 0x00026214
		public Vector3 localScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_localScale_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_localScale_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00028038 File Offset: 0x00026238
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00028050 File Offset: 0x00026250
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				bool flag = this is RectTransform;
				if (flag)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00028080 File Offset: 0x00026280
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00028098 File Offset: 0x00026298
		internal Transform parentInternal
		{
			get
			{
				return this.GetParent();
			}
			set
			{
				this.SetParent(value);
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000280A4 File Offset: 0x000262A4
		private Transform GetParent()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Transform.GetParent_Injected(intPtr));
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000280CB File Offset: 0x000262CB
		public void SetParent(Transform p)
		{
			this.SetParent(p, true);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000280D8 File Offset: 0x000262D8
		[FreeFunction("SetParent", HasExplicitThis = true)]
		public void SetParent(Transform parent, bool worldPositionStays)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetParent_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(parent), worldPositionStays);
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00028104 File Offset: 0x00026304
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Transform.get_worldToLocalMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0002812C File Offset: 0x0002632C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Transform.get_localToWorldMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00028154 File Offset: 0x00026354
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetPositionAndRotation_Injected(intPtr, ref position, ref rotation);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0002817C File Offset: 0x0002637C
		public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetLocalPositionAndRotation_Injected(intPtr, ref localPosition, ref localRotation);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000281A4 File Offset: 0x000263A4
		public void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.GetPositionAndRotation_Injected(intPtr, out position, out rotation);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000281C8 File Offset: 0x000263C8
		public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.GetLocalPositionAndRotation_Injected(intPtr, out localPosition, out localRotation);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000281EC File Offset: 0x000263EC
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.World;
			if (flag)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00028230 File Offset: 0x00026430
		public void Translate(Vector3 translation)
		{
			this.Translate(translation, Space.Self);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0002823C File Offset: 0x0002643C
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00028250 File Offset: 0x00026450
		public void Translate(float x, float y, float z)
		{
			this.Translate(new Vector3(x, y, z), Space.Self);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00028264 File Offset: 0x00026464
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			bool flag = relativeTo;
			if (flag)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000282AA File Offset: 0x000264AA
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000282C0 File Offset: 0x000264C0
		public void Rotate(Vector3 eulers, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion eulerRot = Quaternion.Euler(eulers.x, eulers.y, eulers.z);
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.localRotation *= eulerRot;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * eulerRot * this.rotation;
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00028333 File Offset: 0x00026533
		public void Rotate(Vector3 eulers)
		{
			this.Rotate(eulers, Space.Self);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0002833F File Offset: 0x0002653F
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00028353 File Offset: 0x00026553
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), Space.Self);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00028368 File Offset: 0x00026568
		[NativeMethod("RotateAround")]
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.RotateAroundInternal_Injected(intPtr, ref axis, angle);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00028390 File Offset: 0x00026590
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000283D1 File Offset: 0x000265D1
		public void Rotate(Vector3 axis, float angle)
		{
			this.Rotate(axis, angle, Space.Self);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000283E0 File Offset: 0x000265E0
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 worldPos = this.position;
			Quaternion q = Quaternion.AngleAxis(angle, axis);
			Vector3 dif = worldPos - point;
			dif = q * dif;
			worldPos = point + dif;
			this.position = worldPos;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0002842C File Offset: 0x0002662C
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00028454 File Offset: 0x00026654
		public void LookAt(Transform target)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, Vector3.up);
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0002847E File Offset: 0x0002667E
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			this.Internal_LookAt(worldPosition, worldUp);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0002848A File Offset: 0x0002668A
		public void LookAt(Vector3 worldPosition)
		{
			this.Internal_LookAt(worldPosition, Vector3.up);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0002849C File Offset: 0x0002669C
		[FreeFunction("Internal_LookAt", HasExplicitThis = true)]
		private void Internal_LookAt(Vector3 worldPosition, Vector3 worldUp)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.Internal_LookAt_Injected(intPtr, ref worldPosition, ref worldUp);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000284C4 File Offset: 0x000266C4
		public Vector3 TransformDirection(Vector3 direction)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.TransformDirection_Injected(intPtr, ref direction, out vector);
			return vector;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000284EC File Offset: 0x000266EC
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0002850C File Offset: 0x0002670C
		[NativeMethod(Name = "TransformDirections")]
		internal unsafe void TransformDirectionsInternal(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = directions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedDirections;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.TransformDirectionsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00028574 File Offset: 0x00026774
		public void TransformDirections(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			bool flag = directions.Length != transformedDirections.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.TransformDirections() must be the same length");
			}
			this.TransformDirectionsInternal(directions, transformedDirections);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000285AD File Offset: 0x000267AD
		public void TransformDirections(Span<Vector3> directions)
		{
			this.TransformDirectionsInternal(directions, directions);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000285C0 File Offset: 0x000267C0
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.InverseTransformDirection_Injected(intPtr, ref direction, out vector);
			return vector;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000285E8 File Offset: 0x000267E8
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00028608 File Offset: 0x00026808
		[NativeMethod(Name = "InverseTransformDirections")]
		internal unsafe void InverseTransformDirectionsInternal(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = directions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedDirections;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.InverseTransformDirectionsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00028670 File Offset: 0x00026870
		public void InverseTransformDirections(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			bool flag = directions.Length != transformedDirections.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.InverseTransformDirections() must be the same length");
			}
			this.InverseTransformDirectionsInternal(directions, transformedDirections);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000286A9 File Offset: 0x000268A9
		public void InverseTransformDirections(Span<Vector3> directions)
		{
			this.InverseTransformDirectionsInternal(directions, directions);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000286BC File Offset: 0x000268BC
		public Vector3 TransformVector(Vector3 vector)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector2;
			Transform.TransformVector_Injected(intPtr, ref vector, out vector2);
			return vector2;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000286E4 File Offset: 0x000268E4
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00028704 File Offset: 0x00026904
		[NativeMethod(Name = "TransformVectors")]
		internal unsafe void TransformVectorsInternal(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = vectors;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedVectors;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.TransformVectorsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0002876C File Offset: 0x0002696C
		public void TransformVectors(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			bool flag = vectors.Length != transformedVectors.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.TransformVectors() must be the same length");
			}
			this.TransformVectorsInternal(vectors, transformedVectors);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000287A5 File Offset: 0x000269A5
		public void TransformVectors(Span<Vector3> vectors)
		{
			this.TransformVectorsInternal(vectors, vectors);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000287B8 File Offset: 0x000269B8
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector2;
			Transform.InverseTransformVector_Injected(intPtr, ref vector, out vector2);
			return vector2;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000287E0 File Offset: 0x000269E0
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00028800 File Offset: 0x00026A00
		[NativeMethod(Name = "InverseTransformVectors")]
		internal unsafe void InverseTransformVectorsInternal(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = vectors;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedVectors;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.InverseTransformVectorsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00028868 File Offset: 0x00026A68
		public void InverseTransformVectors(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			bool flag = vectors.Length != transformedVectors.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.InverseTransformVectors() must be the same length");
			}
			this.InverseTransformVectorsInternal(vectors, transformedVectors);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000288A1 File Offset: 0x00026AA1
		public void InverseTransformVectors(Span<Vector3> vectors)
		{
			this.InverseTransformVectorsInternal(vectors, vectors);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000288B4 File Offset: 0x00026AB4
		public Vector3 TransformPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.TransformPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000288DC File Offset: 0x00026ADC
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000288FC File Offset: 0x00026AFC
		[NativeMethod(Name = "TransformPoints")]
		internal unsafe void TransformPointsInternal(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = positions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedPositions;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.TransformPointsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00028964 File Offset: 0x00026B64
		public void TransformPoints(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			bool flag = positions.Length != transformedPositions.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.TransformPoints() must be the same length");
			}
			this.TransformPointsInternal(positions, transformedPositions);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0002899D File Offset: 0x00026B9D
		public void TransformPoints(Span<Vector3> positions)
		{
			this.TransformPointsInternal(positions, positions);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000289B0 File Offset: 0x00026BB0
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.InverseTransformPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000289D8 File Offset: 0x00026BD8
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000289F8 File Offset: 0x00026BF8
		[NativeMethod(Name = "InverseTransformPoints")]
		internal unsafe void InverseTransformPointsInternal(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = positions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedPositions;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.InverseTransformPointsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00028A60 File Offset: 0x00026C60
		public void InverseTransformPoints(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			bool flag = positions.Length != transformedPositions.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.InverseTransformPoints() must be the same length");
			}
			this.InverseTransformPointsInternal(positions, transformedPositions);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00028A99 File Offset: 0x00026C99
		public void InverseTransformPoints(Span<Vector3> positions)
		{
			this.InverseTransformPoints(positions, positions);
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00028AAC File Offset: 0x00026CAC
		public Transform root
		{
			get
			{
				return this.GetRoot();
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00028AC4 File Offset: 0x00026CC4
		private Transform GetRoot()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Transform.GetRoot_Injected(intPtr));
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00028AEC File Offset: 0x00026CEC
		public int childCount
		{
			[NativeMethod("GetChildrenCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Transform.get_childCount_Injected(intPtr);
			}
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00028B10 File Offset: 0x00026D10
		[FreeFunction("DetachChildren", HasExplicitThis = true)]
		public void DetachChildren()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.DetachChildren_Injected(intPtr);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00028B34 File Offset: 0x00026D34
		public void SetAsFirstSibling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetAsFirstSibling_Injected(intPtr);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00028B58 File Offset: 0x00026D58
		public void SetAsLastSibling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetAsLastSibling_Injected(intPtr);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00028B7C File Offset: 0x00026D7C
		public void SetSiblingIndex(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetSiblingIndex_Injected(intPtr, index);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00028BA0 File Offset: 0x00026DA0
		[NativeMethod("MoveAfterSiblingInternal")]
		internal void MoveAfterSibling(Transform transform, bool notifyEditorAndMarkDirty)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.MoveAfterSibling_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform), notifyEditorAndMarkDirty);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00028BCC File Offset: 0x00026DCC
		public int GetSiblingIndex()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.GetSiblingIndex_Injected(intPtr);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00028BF0 File Offset: 0x00026DF0
		[FreeFunction(HasExplicitThis = true)]
		private unsafe Transform FindRelativeTransformWithPath(string path, [DefaultValue("false")] bool isActiveOnly)
		{
			Transform transform;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = Transform.FindRelativeTransformWithPath_Injected(intPtr, ref managedSpanWrapper, isActiveOnly);
			}
			finally
			{
				IntPtr intPtr2;
				transform = Unmarshal.UnmarshalUnityObject<Transform>(intPtr2);
				char* ptr = null;
			}
			return transform;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00028C60 File Offset: 0x00026E60
		public Transform Find(string n)
		{
			bool flag = n == null;
			if (flag)
			{
				throw new ArgumentNullException("Name cannot be null");
			}
			return this.FindRelativeTransformWithPath(n, false);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00028C90 File Offset: 0x00026E90
		[NativeConditional("UNITY_EDITOR")]
		internal void SendTransformChangedScale()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SendTransformChangedScale_Injected(intPtr);
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00028CB4 File Offset: 0x00026EB4
		public Vector3 lossyScale
		{
			[NativeMethod("GetWorldScaleLossy")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_lossyScale_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00028CDC File Offset: 0x00026EDC
		[FreeFunction("Internal_IsChildOrSameTransform", HasExplicitThis = true)]
		public bool IsChildOf([NotNull] Transform parent)
		{
			if (parent == null)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(parent);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			return Transform.IsChildOf_Injected(intPtr, intPtr2);
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00028D24 File Offset: 0x00026F24
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x00028D48 File Offset: 0x00026F48
		[NativeProperty("HasChangedDeprecated")]
		public bool hasChanged
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Transform.get_hasChanged_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_hasChanged_Injected(intPtr, value);
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00028D6C File Offset: 0x00026F6C
		[Obsolete("FindChild has been deprecated. Use Find instead (UnityUpgradable) -> Find([mscorlib] System.String)", false)]
		public Transform FindChild(string n)
		{
			return this.Find(n);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00028D88 File Offset: 0x00026F88
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00028DA0 File Offset: 0x00026FA0
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.RotateAround_Injected(intPtr, ref axis, angle);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00028DC8 File Offset: 0x00026FC8
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.RotateAroundLocal_Injected(intPtr, ref axis, angle);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00028DF0 File Offset: 0x00026FF0
		[FreeFunction("GetChild", HasExplicitThis = true)]
		[NativeThrows]
		public Transform GetChild(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Transform.GetChild_Injected(intPtr, index));
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00028E18 File Offset: 0x00027018
		[Obsolete("warning use Transform.childCount instead (UnityUpgradable) -> Transform.childCount", false)]
		[NativeMethod("GetChildrenCount")]
		public int GetChildCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.GetChildCount_Injected(intPtr);
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00028E3C File Offset: 0x0002703C
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00028E54 File Offset: 0x00027054
		public int hierarchyCapacity
		{
			get
			{
				return this.internal_getHierarchyCapacity();
			}
			set
			{
				this.internal_setHierarchyCapacity(value);
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00028E60 File Offset: 0x00027060
		[FreeFunction("GetHierarchyCapacity", HasExplicitThis = true)]
		private int internal_getHierarchyCapacity()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.internal_getHierarchyCapacity_Injected(intPtr);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00028E84 File Offset: 0x00027084
		[FreeFunction("SetHierarchyCapacity", HasExplicitThis = true)]
		private void internal_setHierarchyCapacity(int value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.internal_setHierarchyCapacity_Injected(intPtr, value);
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00028EA8 File Offset: 0x000270A8
		public int hierarchyCount
		{
			get
			{
				return this.internal_getHierarchyCount();
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00028EC0 File Offset: 0x000270C0
		[FreeFunction("GetHierarchyCount", HasExplicitThis = true)]
		private int internal_getHierarchyCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.internal_getHierarchyCount_Injected(intPtr);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00028EE4 File Offset: 0x000270E4
		[FreeFunction("IsNonUniformScaleTransform", HasExplicitThis = true)]
		[NativeConditional("UNITY_EDITOR")]
		internal bool IsNonUniformScaleTransform()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.IsNonUniformScaleTransform_Injected(intPtr);
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00028F06 File Offset: 0x00027106
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x00028F0E File Offset: 0x0002710E
		[NativeConditional("UNITY_EDITOR")]
		internal bool constrainProportionsScale
		{
			get
			{
				return this.IsConstrainProportionsScale();
			}
			set
			{
				this.SetConstrainProportionsScale(value);
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00028F18 File Offset: 0x00027118
		[NativeConditional("UNITY_EDITOR")]
		private void SetConstrainProportionsScale(bool isLinked)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetConstrainProportionsScale_Injected(intPtr, isLinked);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00028F3C File Offset: 0x0002713C
		[NativeConditional("UNITY_EDITOR")]
		private bool IsConstrainProportionsScale()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.IsConstrainProportionsScale_Injected(intPtr);
		}

		// Token: 0x06001341 RID: 4929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06001342 RID: 4930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06001343 RID: 4931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06001344 RID: 4932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06001345 RID: 4933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalEulerAngles_Injected(IntPtr _unity_self, RotationOrder order, out Vector3 ret);

		// Token: 0x06001346 RID: 4934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalEulerAngles_Injected(IntPtr _unity_self, [In] ref Vector3 euler, RotationOrder order);

		// Token: 0x06001347 RID: 4935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalEulerHint_Injected(IntPtr _unity_self, [In] ref Vector3 euler);

		// Token: 0x06001348 RID: 4936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06001349 RID: 4937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x0600134A RID: 4938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x0600134B RID: 4939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x0600134C RID: 4940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRotationOrderInternal_Injected(IntPtr _unity_self);

		// Token: 0x0600134D RID: 4941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationOrderInternal_Injected(IntPtr _unity_self, RotationOrder rotationOrder);

		// Token: 0x0600134E RID: 4942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localScale_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600134F RID: 4943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localScale_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06001350 RID: 4944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetParent_Injected(IntPtr _unity_self);

		// Token: 0x06001351 RID: 4945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParent_Injected(IntPtr _unity_self, IntPtr parent, bool worldPositionStays);

		// Token: 0x06001352 RID: 4946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldToLocalMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06001353 RID: 4947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06001354 RID: 4948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionAndRotation_Injected(IntPtr _unity_self, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x06001355 RID: 4949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionAndRotation_Injected(IntPtr _unity_self, [In] ref Vector3 localPosition, [In] ref Quaternion localRotation);

		// Token: 0x06001356 RID: 4950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionAndRotation_Injected(IntPtr _unity_self, out Vector3 position, out Quaternion rotation);

		// Token: 0x06001357 RID: 4951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionAndRotation_Injected(IntPtr _unity_self, out Vector3 localPosition, out Quaternion localRotation);

		// Token: 0x06001358 RID: 4952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateAroundInternal_Injected(IntPtr _unity_self, [In] ref Vector3 axis, float angle);

		// Token: 0x06001359 RID: 4953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_LookAt_Injected(IntPtr _unity_self, [In] ref Vector3 worldPosition, [In] ref Vector3 worldUp);

		// Token: 0x0600135A RID: 4954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformDirection_Injected(IntPtr _unity_self, [In] ref Vector3 direction, out Vector3 ret);

		// Token: 0x0600135B RID: 4955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformDirectionsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper directions, ref ManagedSpanWrapper transformedDirections);

		// Token: 0x0600135C RID: 4956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformDirection_Injected(IntPtr _unity_self, [In] ref Vector3 direction, out Vector3 ret);

		// Token: 0x0600135D RID: 4957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformDirectionsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper directions, ref ManagedSpanWrapper transformedDirections);

		// Token: 0x0600135E RID: 4958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformVector_Injected(IntPtr _unity_self, [In] ref Vector3 vector, out Vector3 ret);

		// Token: 0x0600135F RID: 4959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformVectorsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vectors, ref ManagedSpanWrapper transformedVectors);

		// Token: 0x06001360 RID: 4960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformVector_Injected(IntPtr _unity_self, [In] ref Vector3 vector, out Vector3 ret);

		// Token: 0x06001361 RID: 4961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformVectorsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vectors, ref ManagedSpanWrapper transformedVectors);

		// Token: 0x06001362 RID: 4962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x06001363 RID: 4963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformPointsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions, ref ManagedSpanWrapper transformedPositions);

		// Token: 0x06001364 RID: 4964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x06001365 RID: 4965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformPointsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions, ref ManagedSpanWrapper transformedPositions);

		// Token: 0x06001366 RID: 4966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRoot_Injected(IntPtr _unity_self);

		// Token: 0x06001367 RID: 4967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_childCount_Injected(IntPtr _unity_self);

		// Token: 0x06001368 RID: 4968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DetachChildren_Injected(IntPtr _unity_self);

		// Token: 0x06001369 RID: 4969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAsFirstSibling_Injected(IntPtr _unity_self);

		// Token: 0x0600136A RID: 4970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAsLastSibling_Injected(IntPtr _unity_self);

		// Token: 0x0600136B RID: 4971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSiblingIndex_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600136C RID: 4972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveAfterSibling_Injected(IntPtr _unity_self, IntPtr transform, bool notifyEditorAndMarkDirty);

		// Token: 0x0600136D RID: 4973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSiblingIndex_Injected(IntPtr _unity_self);

		// Token: 0x0600136E RID: 4974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindRelativeTransformWithPath_Injected(IntPtr _unity_self, ref ManagedSpanWrapper path, [DefaultValue("false")] bool isActiveOnly);

		// Token: 0x0600136F RID: 4975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendTransformChangedScale_Injected(IntPtr _unity_self);

		// Token: 0x06001370 RID: 4976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lossyScale_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06001371 RID: 4977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsChildOf_Injected(IntPtr _unity_self, IntPtr parent);

		// Token: 0x06001372 RID: 4978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasChanged_Injected(IntPtr _unity_self);

		// Token: 0x06001373 RID: 4979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hasChanged_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001374 RID: 4980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateAround_Injected(IntPtr _unity_self, [In] ref Vector3 axis, float angle);

		// Token: 0x06001375 RID: 4981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateAroundLocal_Injected(IntPtr _unity_self, [In] ref Vector3 axis, float angle);

		// Token: 0x06001376 RID: 4982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetChild_Injected(IntPtr _unity_self, int index);

		// Token: 0x06001377 RID: 4983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildCount_Injected(IntPtr _unity_self);

		// Token: 0x06001378 RID: 4984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int internal_getHierarchyCapacity_Injected(IntPtr _unity_self);

		// Token: 0x06001379 RID: 4985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void internal_setHierarchyCapacity_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600137A RID: 4986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int internal_getHierarchyCount_Injected(IntPtr _unity_self);

		// Token: 0x0600137B RID: 4987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNonUniformScaleTransform_Injected(IntPtr _unity_self);

		// Token: 0x0600137C RID: 4988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstrainProportionsScale_Injected(IntPtr _unity_self, bool isLinked);

		// Token: 0x0600137D RID: 4989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsConstrainProportionsScale_Injected(IntPtr _unity_self);

		// Token: 0x020001ED RID: 493
		private class Enumerator : IEnumerator
		{
			// Token: 0x0600137E RID: 4990 RVA: 0x00028F5E File Offset: 0x0002715E
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x0600137F RID: 4991 RVA: 0x00028F78 File Offset: 0x00027178
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x06001380 RID: 4992 RVA: 0x00028F9C File Offset: 0x0002719C
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < childCount;
			}

			// Token: 0x06001381 RID: 4993 RVA: 0x00028FCE File Offset: 0x000271CE
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000718 RID: 1816
			private Transform outer;

			// Token: 0x04000719 RID: 1817
			private int currentIndex = -1;
		}
	}
}
