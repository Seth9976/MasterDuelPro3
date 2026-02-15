using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000122 RID: 290
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class PopupField<T> : BasePopupField<T, T>
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0002AED0 File Offset: 0x000290D0
		internal override string GetValueToDisplay()
		{
			bool flag = this.m_FormatSelectedValueCallback != null;
			string text;
			if (flag)
			{
				text = this.m_FormatSelectedValueCallback(this.value);
			}
			else
			{
				bool flag2 = this.value != null;
				if (flag2)
				{
					T value = this.value;
					text = UIElementsUtility.ParseMenuName(value.ToString());
				}
				else
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002AF38 File Offset: 0x00029138
		internal override string GetListItemToDisplay(T value)
		{
			bool flag = this.m_FormatListItemCallback != null;
			string text;
			if (flag)
			{
				text = this.m_FormatListItemCallback(value);
			}
			else
			{
				text = ((value != null && this.m_Choices.Contains(value)) ? value.ToString() : string.Empty);
			}
			return text;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0002AF90 File Offset: 0x00029190
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0002AFA8 File Offset: 0x000291A8
		public override T value
		{
			get
			{
				return base.value;
			}
			set
			{
				int previousIndex = this.m_Index;
				List<T> choices = this.m_Choices;
				this.m_Index = ((choices != null) ? choices.IndexOf(value) : (-1));
				base.value = value;
				bool flag = this.m_Index != previousIndex;
				if (flag)
				{
					base.NotifyPropertyChanged(in PopupField<T>.indexProperty);
				}
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002AFFA File Offset: 0x000291FA
		public override void SetValueWithoutNotify(T newValue)
		{
			List<T> choices = this.m_Choices;
			this.m_Index = ((choices != null) ? choices.IndexOf(newValue) : (-1));
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0002B020 File Offset: 0x00029220
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0002B038 File Offset: 0x00029238
		[CreateProperty]
		public int index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				bool flag = value != this.m_Index;
				if (flag)
				{
					this.m_Index = value;
					bool flag2 = this.m_Index >= 0 && this.m_Index < this.m_Choices.Count;
					if (flag2)
					{
						this.value = this.m_Choices[this.m_Index];
					}
					else
					{
						this.value = default(T);
					}
					base.NotifyPropertyChanged(in PopupField<T>.indexProperty);
				}
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002B0B8 File Offset: 0x000292B8
		public PopupField(string label = null)
			: base(label)
		{
			base.AddToClassList(PopupField<T>.ussClassName);
			base.labelElement.AddToClassList(PopupField<T>.labelUssClassName);
			base.visualInput.AddToClassList(PopupField<T>.inputUssClassName);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002B0F8 File Offset: 0x000292F8
		internal override void AddMenuItems(IGenericMenu menu)
		{
			bool flag = menu == null;
			if (flag)
			{
				throw new ArgumentNullException("menu");
			}
			bool flag2 = this.m_Choices == null;
			if (!flag2)
			{
				using (List<T>.Enumerator enumerator = this.m_Choices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T item = enumerator.Current;
						bool isSelected = EqualityComparer<T>.Default.Equals(item, this.value) && !base.showMixedValue;
						menu.AddItem(this.GetListItemToDisplay(item), isSelected, delegate
						{
							this.ChangeValueFromMenu(item);
						});
					}
				}
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002B1C8 File Offset: 0x000293C8
		private void ChangeValueFromMenu(T menuItem)
		{
			this.value = menuItem;
		}

		// Token: 0x0400059D RID: 1437
		internal static readonly BindingId indexProperty = "index";

		// Token: 0x0400059E RID: 1438
		private int m_Index = -1;

		// Token: 0x0400059F RID: 1439
		public new static readonly string ussClassName = "unity-popup-field";

		// Token: 0x040005A0 RID: 1440
		public new static readonly string labelUssClassName = PopupField<T>.ussClassName + "__label";

		// Token: 0x040005A1 RID: 1441
		public new static readonly string inputUssClassName = PopupField<T>.ussClassName + "__input";
	}
}
