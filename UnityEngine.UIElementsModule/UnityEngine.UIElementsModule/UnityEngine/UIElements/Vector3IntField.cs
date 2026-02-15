using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B4 RID: 180
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector3IntField : BaseCompositeField<Vector3Int, IntegerField, int>
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
		internal override BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription[] array = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription[3];
			array[0] = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription("X", "unity-x-input", (Vector3Int r) => r.x, delegate(ref Vector3Int r, int v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription("Y", "unity-y-input", (Vector3Int r) => r.y, delegate(ref Vector3Int r, int v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription("Z", "unity-z-input", (Vector3Int r) => r.z, delegate(ref Vector3Int r, int v)
			{
				r.z = v;
			});
			return array;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		public Vector3IntField()
			: this(null)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001CA03 File Offset: 0x0001AC03
		public Vector3IntField(string label)
			: base(label, 3)
		{
			base.AddToClassList(Vector3IntField.ussClassName);
			base.labelElement.AddToClassList(Vector3IntField.labelUssClassName);
			base.visualInput.AddToClassList(Vector3IntField.inputUssClassName);
		}

		// Token: 0x040003A5 RID: 933
		public new static readonly string ussClassName = "unity-vector3-int-field";

		// Token: 0x040003A6 RID: 934
		public new static readonly string labelUssClassName = Vector3IntField.ussClassName + "__label";

		// Token: 0x040003A7 RID: 935
		public new static readonly string inputUssClassName = Vector3IntField.ussClassName + "__input";

		// Token: 0x020000B5 RID: 181
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector3IntField, Vector3IntField.UxmlTraits>
		{
		}

		// Token: 0x020000B6 RID: 182
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector3Int>.UxmlTraits
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x0001CA7C File Offset: 0x0001AC7C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector3IntField f = (Vector3IntField)ve;
				f.SetValueWithoutNotify(new Vector3Int(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x040003A8 RID: 936
			private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
			{
				name = "x"
			};

			// Token: 0x040003A9 RID: 937
			private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
			{
				name = "y"
			};

			// Token: 0x040003AA RID: 938
			private UxmlIntAttributeDescription m_ZValue = new UxmlIntAttributeDescription
			{
				name = "z"
			};
		}
	}
}
