using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B0 RID: 176
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector2IntField : BaseCompositeField<Vector2Int, IntegerField, int>
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		internal override BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription[] array = new BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription[2];
			array[0] = new BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription("X", "unity-x-input", (Vector2Int r) => r.x, delegate(ref Vector2Int r, int v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription("Y", "unity-y-input", (Vector2Int r) => r.y, delegate(ref Vector2Int r, int v)
			{
				r.y = v;
			});
			return array;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001C7B0 File Offset: 0x0001A9B0
		public Vector2IntField()
			: this(null)
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C7BB File Offset: 0x0001A9BB
		public Vector2IntField(string label)
			: base(label, 2)
		{
			base.AddToClassList(Vector2IntField.ussClassName);
			base.labelElement.AddToClassList(Vector2IntField.labelUssClassName);
			base.visualInput.AddToClassList(Vector2IntField.inputUssClassName);
		}

		// Token: 0x0400039B RID: 923
		public new static readonly string ussClassName = "unity-vector2-int-field";

		// Token: 0x0400039C RID: 924
		public new static readonly string labelUssClassName = Vector2IntField.ussClassName + "__label";

		// Token: 0x0400039D RID: 925
		public new static readonly string inputUssClassName = Vector2IntField.ussClassName + "__input";

		// Token: 0x020000B1 RID: 177
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector2IntField, Vector2IntField.UxmlTraits>
		{
		}

		// Token: 0x020000B2 RID: 178
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector2Int>.UxmlTraits
		{
			// Token: 0x060005E4 RID: 1508 RVA: 0x0001C834 File Offset: 0x0001AA34
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector2IntField f = (Vector2IntField)ve;
				f.SetValueWithoutNotify(new Vector2Int(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x0400039E RID: 926
			private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
			{
				name = "x"
			};

			// Token: 0x0400039F RID: 927
			private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
			{
				name = "y"
			};
		}
	}
}
