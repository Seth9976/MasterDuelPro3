using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E7 RID: 487
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	[NativeClass("UI::RectTransform")]
	public sealed class RectTransform : Transform
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06001294 RID: 4756 RVA: 0x00027500 File Offset: 0x00025700
		// (remove) Token: 0x06001295 RID: 4757 RVA: 0x00027534 File Offset: 0x00025734
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00027568 File Offset: 0x00025768
		public Rect rect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				RectTransform.get_rect_Injected(intPtr, out rect);
				return rect;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00027590 File Offset: 0x00025790
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x000275B8 File Offset: 0x000257B8
		public Vector2 anchorMin
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_anchorMin_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_anchorMin_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x000275DC File Offset: 0x000257DC
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x00027604 File Offset: 0x00025804
		public Vector2 anchorMax
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_anchorMax_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_anchorMax_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00027628 File Offset: 0x00025828
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x00027650 File Offset: 0x00025850
		public Vector2 anchoredPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_anchoredPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_anchoredPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00027674 File Offset: 0x00025874
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x0002769C File Offset: 0x0002589C
		public Vector2 sizeDelta
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_sizeDelta_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_sizeDelta_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000276C0 File Offset: 0x000258C0
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x000276E8 File Offset: 0x000258E8
		public Vector2 pivot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_pivot_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_pivot_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0002770C File Offset: 0x0002590C
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x00027744 File Offset: 0x00025944
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 pos2 = this.anchoredPosition;
				return new Vector3(pos2.x, pos2.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 pos3 = base.localPosition;
				pos3.z = value.z;
				base.localPosition = pos3;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00027788 File Offset: 0x00025988
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x000277B8 File Offset: 0x000259B8
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 offset = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= offset;
				this.anchoredPosition += Vector2.Scale(offset, Vector2.one - this.pivot);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00027824 File Offset: 0x00025A24
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x0002785C File Offset: 0x00025A5C
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 offset = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += offset;
				this.anchoredPosition += Vector2.Scale(offset, this.pivot);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x000278C8 File Offset: 0x00025AC8
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x000278F0 File Offset: 0x00025AF0
		public Object drivenByObject
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Object>(RectTransform.get_drivenByObject_Injected(intPtr));
			}
			internal set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_drivenByObject_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(value));
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00027918 File Offset: 0x00025B18
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x0002793C File Offset: 0x00025B3C
		internal DrivenTransformProperties drivenProperties
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectTransform.get_drivenProperties_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_drivenProperties_Injected(intPtr, value);
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00027960 File Offset: 0x00025B60
		[NativeMethod("UpdateIfTransformDispatchIsDirty")]
		public void ForceUpdateRectTransforms()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RectTransform.ForceUpdateRectTransforms_Injected(intPtr);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00027984 File Offset: 0x00025B84
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				Rect tmpRect = this.rect;
				float x0 = tmpRect.x;
				float y0 = tmpRect.y;
				float x = tmpRect.xMax;
				float y = tmpRect.yMax;
				fourCornersArray[0] = new Vector3(x0, y0, 0f);
				fourCornersArray[1] = new Vector3(x0, y, 0f);
				fourCornersArray[2] = new Vector3(x, y, 0f);
				fourCornersArray[3] = new Vector3(x, y0, 0f);
			}
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00027A28 File Offset: 0x00025C28
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				this.GetLocalCorners(fourCornersArray);
				Matrix4x4 mat = base.transform.localToWorldMatrix;
				for (int i = 0; i < 4; i++)
				{
					fourCornersArray[i] = mat.MultiplyPoint(fourCornersArray[i]);
				}
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00027A90 File Offset: 0x00025C90
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int axis = ((edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Bottom) ? 1 : 0);
			bool end = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float anchorValue = (float)(end ? 1 : 0);
			Vector2 anchor = this.anchorMin;
			anchor[axis] = anchorValue;
			this.anchorMin = anchor;
			anchor = this.anchorMax;
			anchor[axis] = anchorValue;
			this.anchorMax = anchor;
			Vector2 sizeD = this.sizeDelta;
			sizeD[axis] = size;
			this.sizeDelta = sizeD;
			Vector2 positionCopy = this.anchoredPosition;
			positionCopy[axis] = (end ? (-inset - size * (1f - this.pivot[axis])) : (inset + size * this.pivot[axis]));
			this.anchoredPosition = positionCopy;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00027B5C File Offset: 0x00025D5C
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeD = this.sizeDelta;
			sizeD[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeD;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00027BB5 File Offset: 0x00025DB5
		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
			if (reapplyDrivenProperties != null)
			{
				reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00027BCC File Offset: 0x00025DCC
		internal Rect GetRectInParentSpace()
		{
			Rect rectResult = this.rect;
			Vector2 offset = this.offsetMin + Vector2.Scale(this.pivot, rectResult.size);
			bool flag = base.transform.parent;
			if (flag)
			{
				RectTransform parentRect = base.transform.parent.GetComponent<RectTransform>();
				bool flag2 = parentRect;
				if (flag2)
				{
					offset += Vector2.Scale(this.anchorMin, parentRect.rect.size);
				}
			}
			rectResult.x += offset.x;
			rectResult.y += offset.y;
			return rectResult;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00027C84 File Offset: 0x00025E84
		private Vector2 GetParentSize()
		{
			RectTransform parentRect = base.parent as RectTransform;
			bool flag = !parentRect;
			Vector2 vector;
			if (flag)
			{
				vector = Vector2.zero;
			}
			else
			{
				vector = parentRect.rect.size;
			}
			return vector;
		}

		// Token: 0x060012B4 RID: 4788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060012B5 RID: 4789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchorMin_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060012B6 RID: 4790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchorMin_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060012B7 RID: 4791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchorMax_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060012B8 RID: 4792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchorMax_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060012B9 RID: 4793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchoredPosition_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060012BA RID: 4794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchoredPosition_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060012BB RID: 4795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sizeDelta_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060012BC RID: 4796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sizeDelta_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060012BD RID: 4797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivot_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060012BE RID: 4798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pivot_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060012BF RID: 4799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_drivenByObject_Injected(IntPtr _unity_self);

		// Token: 0x060012C0 RID: 4800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drivenByObject_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060012C1 RID: 4801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DrivenTransformProperties get_drivenProperties_Injected(IntPtr _unity_self);

		// Token: 0x060012C2 RID: 4802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drivenProperties_Injected(IntPtr _unity_self, DrivenTransformProperties value);

		// Token: 0x060012C3 RID: 4803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ForceUpdateRectTransforms_Injected(IntPtr _unity_self);

		// Token: 0x020001E8 RID: 488
		public enum Edge
		{
			// Token: 0x0400070A RID: 1802
			Left,
			// Token: 0x0400070B RID: 1803
			Right,
			// Token: 0x0400070C RID: 1804
			Top,
			// Token: 0x0400070D RID: 1805
			Bottom
		}

		// Token: 0x020001E9 RID: 489
		public enum Axis
		{
			// Token: 0x0400070F RID: 1807
			Horizontal,
			// Token: 0x04000710 RID: 1808
			Vertical
		}

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x060012C5 RID: 4805
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
