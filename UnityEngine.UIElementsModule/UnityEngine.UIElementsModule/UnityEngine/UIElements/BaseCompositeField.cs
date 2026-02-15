using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006F RID: 111
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class BaseCompositeField<TValueType, TField, TFieldValue> : BaseField<TValueType> where TField : TextValueField<TFieldValue>, new()
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00013D38 File Offset: 0x00011F38
		private VisualElement GetSpacer()
		{
			VisualElement spacer = new VisualElement();
			spacer.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.spacerUssClassName);
			spacer.visible = false;
			spacer.focusable = false;
			return spacer;
		}

		// Token: 0x06000405 RID: 1029
		internal abstract BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription[] DescribeFields();

		// Token: 0x06000406 RID: 1030 RVA: 0x00013D70 File Offset: 0x00011F70
		protected BaseCompositeField(string label, int fieldsByLine)
			: base(label, null)
		{
			base.delegatesFocus = false;
			base.visualInput.focusable = false;
			base.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName);
			base.labelElement.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.labelUssClassName);
			base.visualInput.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.inputUssClassName);
			this.m_ShouldUpdateDisplay = true;
			this.m_Fields = new List<TField>();
			BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription[] fieldDescriptions = this.DescribeFields();
			int numberOfLines = 1;
			bool flag = fieldsByLine > 1;
			if (flag)
			{
				numberOfLines = fieldDescriptions.Length / fieldsByLine;
			}
			bool isMultiLine = false;
			bool flag2 = numberOfLines > 1;
			if (flag2)
			{
				isMultiLine = true;
				base.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.multilineVariantUssClassName);
			}
			for (int i = 0; i < numberOfLines; i++)
			{
				VisualElement newLineGroup = null;
				bool flag3 = isMultiLine;
				if (flag3)
				{
					newLineGroup = new VisualElement();
					newLineGroup.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.fieldGroupUssClassName);
				}
				bool firstField = true;
				for (int j = i * fieldsByLine; j < i * fieldsByLine + fieldsByLine; j++)
				{
					BaseCompositeField<TValueType, TField, TFieldValue>.<>c__DisplayClass18_0 CS$<>8__locals1 = new BaseCompositeField<TValueType, TField, TFieldValue>.<>c__DisplayClass18_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.desc = fieldDescriptions[j];
					BaseCompositeField<TValueType, TField, TFieldValue>.<>c__DisplayClass18_0 CS$<>8__locals2 = CS$<>8__locals1;
					TField tfield = new TField();
					tfield.name = CS$<>8__locals1.desc.ussName;
					CS$<>8__locals2.field = tfield;
					CS$<>8__locals1.field.delegatesFocus = true;
					CS$<>8__locals1.field.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.fieldUssClassName);
					bool flag4 = firstField;
					if (flag4)
					{
						CS$<>8__locals1.field.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.firstFieldVariantUssClassName);
						firstField = false;
					}
					CS$<>8__locals1.field.label = CS$<>8__locals1.desc.name;
					CS$<>8__locals1.field.onValidateValue += delegate(TFieldValue newValue)
					{
						TValueType cur = CS$<>8__locals1.<>4__this.value;
						CS$<>8__locals1.desc.write(ref cur, newValue);
						TValueType validatedValue = CS$<>8__locals1.<>4__this.ValidatedValue(cur);
						return CS$<>8__locals1.desc.read(validatedValue);
					};
					CS$<>8__locals1.field.RegisterValueChangedCallback(delegate(ChangeEvent<TFieldValue> e)
					{
						TValueType cur2 = CS$<>8__locals1.<>4__this.value;
						CS$<>8__locals1.desc.write(ref cur2, e.newValue);
						TFieldValue newValue = e.newValue;
						string valueString = newValue.ToString();
						string textString = ((TField)((object)e.currentTarget)).text;
						bool flag9 = valueString != textString || CS$<>8__locals1.field.CanTryParse(textString);
						if (flag9)
						{
							CS$<>8__locals1.<>4__this.m_ShouldUpdateDisplay = false;
						}
						CS$<>8__locals1.<>4__this.value = cur2;
						CS$<>8__locals1.<>4__this.m_ShouldUpdateDisplay = true;
					});
					this.m_Fields.Add(CS$<>8__locals1.field);
					bool flag5 = isMultiLine;
					if (flag5)
					{
						newLineGroup.Add(CS$<>8__locals1.field);
					}
					else
					{
						base.visualInput.hierarchy.Add(CS$<>8__locals1.field);
					}
				}
				bool flag6 = fieldsByLine < 3;
				if (flag6)
				{
					int fieldsToAdd = 3 - fieldsByLine;
					for (int countToAdd = 0; countToAdd < fieldsToAdd; countToAdd++)
					{
						bool flag7 = isMultiLine;
						if (flag7)
						{
							newLineGroup.Add(this.GetSpacer());
						}
						else
						{
							base.visualInput.hierarchy.Add(this.GetSpacer());
						}
					}
				}
				bool flag8 = isMultiLine;
				if (flag8)
				{
					base.visualInput.hierarchy.Add(newLineGroup);
				}
			}
			this.UpdateDisplay();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00014040 File Offset: 0x00012240
		private void UpdateDisplay()
		{
			bool flag = this.m_Fields.Count != 0;
			if (flag)
			{
				int i = 0;
				BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription[] fieldDescriptions = this.DescribeFields();
				foreach (BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription fd in fieldDescriptions)
				{
					this.m_Fields[i].SetValueWithoutNotify(fd.read(base.rawValue));
					i++;
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000140BC File Offset: 0x000122BC
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			bool displayNeedsUpdate = this.m_ForceUpdateDisplay || (this.m_ShouldUpdateDisplay && !EqualityComparer<TValueType>.Default.Equals(base.rawValue, newValue));
			base.SetValueWithoutNotify(newValue);
			bool flag = displayNeedsUpdate;
			if (flag)
			{
				this.UpdateDisplay();
			}
			this.m_ForceUpdateDisplay = false;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00014112 File Offset: 0x00012312
		internal override void OnViewDataReady()
		{
			this.m_ForceUpdateDisplay = true;
			base.OnViewDataReady();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014124 File Offset: 0x00012324
		protected override void UpdateMixedValueContent()
		{
			foreach (TField field in this.m_Fields)
			{
				field.showMixedValue = base.showMixedValue;
			}
		}

		// Token: 0x04000211 RID: 529
		private List<TField> m_Fields;

		// Token: 0x04000212 RID: 530
		private bool m_ShouldUpdateDisplay;

		// Token: 0x04000213 RID: 531
		private bool m_ForceUpdateDisplay;

		// Token: 0x04000214 RID: 532
		public new static readonly string ussClassName = "unity-composite-field";

		// Token: 0x04000215 RID: 533
		public new static readonly string labelUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__label";

		// Token: 0x04000216 RID: 534
		public new static readonly string inputUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__input";

		// Token: 0x04000217 RID: 535
		public static readonly string spacerUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__field-spacer";

		// Token: 0x04000218 RID: 536
		public static readonly string multilineVariantUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "--multi-line";

		// Token: 0x04000219 RID: 537
		public static readonly string fieldGroupUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__field-group";

		// Token: 0x0400021A RID: 538
		public static readonly string fieldUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__field";

		// Token: 0x0400021B RID: 539
		public static readonly string firstFieldVariantUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.fieldUssClassName + "--first";

		// Token: 0x0400021C RID: 540
		public static readonly string twoLinesVariantUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "--two-lines";

		// Token: 0x02000070 RID: 112
		internal struct FieldDescription
		{
			// Token: 0x0600040C RID: 1036 RVA: 0x0001423F File Offset: 0x0001243F
			public FieldDescription(string name, string ussName, Func<TValueType, TFieldValue> read, BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription.WriteDelegate write)
			{
				this.name = name;
				this.ussName = ussName;
				this.read = read;
				this.write = write;
			}

			// Token: 0x0400021D RID: 541
			internal readonly string name;

			// Token: 0x0400021E RID: 542
			internal readonly string ussName;

			// Token: 0x0400021F RID: 543
			internal readonly Func<TValueType, TFieldValue> read;

			// Token: 0x04000220 RID: 544
			internal readonly BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription.WriteDelegate write;

			// Token: 0x02000071 RID: 113
			// (Invoke) Token: 0x0600040E RID: 1038
			public delegate void WriteDelegate(ref TValueType val, TFieldValue fieldValue);
		}
	}
}
