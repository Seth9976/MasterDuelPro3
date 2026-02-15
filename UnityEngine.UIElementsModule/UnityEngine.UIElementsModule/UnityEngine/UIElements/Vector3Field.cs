using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A8 RID: 168
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector3Field : BaseCompositeField<Vector3, FloatField, float>
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x0001C160 File Offset: 0x0001A360
		internal override BaseCompositeField<Vector3, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector3, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Vector3, FloatField, float>.FieldDescription[3];
			array[0] = new BaseCompositeField<Vector3, FloatField, float>.FieldDescription("X", "unity-x-input", (Vector3 r) => r.x, delegate(ref Vector3 r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector3, FloatField, float>.FieldDescription("Y", "unity-y-input", (Vector3 r) => r.y, delegate(ref Vector3 r, float v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Vector3, FloatField, float>.FieldDescription("Z", "unity-z-input", (Vector3 r) => r.z, delegate(ref Vector3 r, float v)
			{
				r.z = v;
			});
			return array;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001C274 File Offset: 0x0001A474
		public Vector3Field()
			: this(null)
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001C27F File Offset: 0x0001A47F
		public Vector3Field(string label)
			: base(label, 3)
		{
			base.AddToClassList(Vector3Field.ussClassName);
			base.labelElement.AddToClassList(Vector3Field.labelUssClassName);
			base.visualInput.AddToClassList(Vector3Field.inputUssClassName);
		}

		// Token: 0x0400037E RID: 894
		public new static readonly string ussClassName = "unity-vector3-field";

		// Token: 0x0400037F RID: 895
		public new static readonly string labelUssClassName = Vector3Field.ussClassName + "__label";

		// Token: 0x04000380 RID: 896
		public new static readonly string inputUssClassName = Vector3Field.ussClassName + "__input";

		// Token: 0x020000A9 RID: 169
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector3Field, Vector3Field.UxmlTraits>
		{
		}

		// Token: 0x020000AA RID: 170
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector3>.UxmlTraits
		{
			// Token: 0x060005C4 RID: 1476 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector3Field f = (Vector3Field)ve;
				f.SetValueWithoutNotify(new Vector3(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000381 RID: 897
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000382 RID: 898
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};

			// Token: 0x04000383 RID: 899
			private UxmlFloatAttributeDescription m_ZValue = new UxmlFloatAttributeDescription
			{
				name = "z"
			};
		}
	}
}
