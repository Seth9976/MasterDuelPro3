using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000091 RID: 145
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class BoundsIntField : BaseField<BoundsInt>
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0001AF3E File Offset: 0x0001913E
		public BoundsIntField()
			: this(null)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001AF4C File Offset: 0x0001914C
		public BoundsIntField(string label)
			: base(label, null)
		{
			base.delegatesFocus = false;
			base.visualInput.focusable = false;
			base.AddToClassList(BoundsIntField.ussClassName);
			base.visualInput.AddToClassList(BoundsIntField.inputUssClassName);
			base.labelElement.AddToClassList(BoundsIntField.labelUssClassName);
			this.m_PositionField = new Vector3IntField("Position");
			this.m_PositionField.name = "unity-m_Position-input";
			this.m_PositionField.delegatesFocus = true;
			this.m_PositionField.AddToClassList(BoundsIntField.positionUssClassName);
			this.m_PositionField.RegisterValueChangedCallback(delegate(ChangeEvent<Vector3Int> e)
			{
				BoundsInt current = this.value;
				current.position = e.newValue;
				this.value = current;
			});
			base.visualInput.hierarchy.Add(this.m_PositionField);
			this.m_SizeField = new Vector3IntField("Size");
			this.m_SizeField.name = "unity-m_Size-input";
			this.m_SizeField.delegatesFocus = true;
			this.m_SizeField.AddToClassList(BoundsIntField.sizeUssClassName);
			this.m_SizeField.RegisterValueChangedCallback(delegate(ChangeEvent<Vector3Int> e)
			{
				BoundsInt current2 = this.value;
				current2.size = e.newValue;
				this.value = current2;
			});
			base.visualInput.hierarchy.Add(this.m_SizeField);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001B088 File Offset: 0x00019288
		public override void SetValueWithoutNotify(BoundsInt newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.m_PositionField.SetValueWithoutNotify(base.rawValue.position);
			this.m_SizeField.SetValueWithoutNotify(base.rawValue.size);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001B0D2 File Offset: 0x000192D2
		protected override void UpdateMixedValueContent()
		{
			this.m_PositionField.showMixedValue = base.showMixedValue;
			this.m_SizeField.showMixedValue = base.showMixedValue;
		}

		// Token: 0x04000339 RID: 825
		private Vector3IntField m_PositionField;

		// Token: 0x0400033A RID: 826
		private Vector3IntField m_SizeField;

		// Token: 0x0400033B RID: 827
		public new static readonly string ussClassName = "unity-bounds-int-field";

		// Token: 0x0400033C RID: 828
		public new static readonly string labelUssClassName = BoundsIntField.ussClassName + "__label";

		// Token: 0x0400033D RID: 829
		public new static readonly string inputUssClassName = BoundsIntField.ussClassName + "__input";

		// Token: 0x0400033E RID: 830
		public static readonly string positionUssClassName = BoundsIntField.ussClassName + "__position-field";

		// Token: 0x0400033F RID: 831
		public static readonly string sizeUssClassName = BoundsIntField.ussClassName + "__size-field";

		// Token: 0x02000092 RID: 146
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<BoundsIntField, BoundsIntField.UxmlTraits>
		{
		}

		// Token: 0x02000093 RID: 147
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<BoundsInt>.UxmlTraits
		{
			// Token: 0x06000575 RID: 1397 RVA: 0x0001B1C4 File Offset: 0x000193C4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BoundsIntField f = (BoundsIntField)ve;
				f.SetValueWithoutNotify(new BoundsInt(new Vector3Int(this.m_PositionXValue.GetValueFromBag(bag, cc), this.m_PositionYValue.GetValueFromBag(bag, cc), this.m_PositionZValue.GetValueFromBag(bag, cc)), new Vector3Int(this.m_SizeXValue.GetValueFromBag(bag, cc), this.m_SizeYValue.GetValueFromBag(bag, cc), this.m_SizeZValue.GetValueFromBag(bag, cc))));
			}

			// Token: 0x04000340 RID: 832
			private UxmlIntAttributeDescription m_PositionXValue = new UxmlIntAttributeDescription
			{
				name = "px"
			};

			// Token: 0x04000341 RID: 833
			private UxmlIntAttributeDescription m_PositionYValue = new UxmlIntAttributeDescription
			{
				name = "py"
			};

			// Token: 0x04000342 RID: 834
			private UxmlIntAttributeDescription m_PositionZValue = new UxmlIntAttributeDescription
			{
				name = "pz"
			};

			// Token: 0x04000343 RID: 835
			private UxmlIntAttributeDescription m_SizeXValue = new UxmlIntAttributeDescription
			{
				name = "sx"
			};

			// Token: 0x04000344 RID: 836
			private UxmlIntAttributeDescription m_SizeYValue = new UxmlIntAttributeDescription
			{
				name = "sy"
			};

			// Token: 0x04000345 RID: 837
			private UxmlIntAttributeDescription m_SizeZValue = new UxmlIntAttributeDescription
			{
				name = "sz"
			};
		}
	}
}
