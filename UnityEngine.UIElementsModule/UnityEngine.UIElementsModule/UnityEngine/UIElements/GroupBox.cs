using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020000CF RID: 207
	public class GroupBox : BindableElement, IGroupBox
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001F289 File Offset: 0x0001D489
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001F2A0 File Offset: 0x0001D4A0
		[CreateProperty]
		public string text
		{
			get
			{
				Label titleLabel = this.m_TitleLabel;
				return (titleLabel != null) ? titleLabel.text : null;
			}
			set
			{
				string previous = this.text;
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_TitleLabel == null;
					if (flag2)
					{
						this.m_TitleLabel = new Label(value);
						this.m_TitleLabel.AddToClassList(GroupBox.labelUssClassName);
						base.Insert(0, this.m_TitleLabel);
					}
					this.m_TitleLabel.text = value;
				}
				else
				{
					bool flag3 = this.m_TitleLabel != null;
					if (flag3)
					{
						this.m_TitleLabel.RemoveFromHierarchy();
						this.m_TitleLabel = null;
					}
				}
				bool flag4 = string.CompareOrdinal(previous, this.text) != 0;
				if (flag4)
				{
					base.NotifyPropertyChanged(in GroupBox.textProperty);
				}
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001F350 File Offset: 0x0001D550
		public GroupBox()
			: this(null)
		{
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001F35B File Offset: 0x0001D55B
		public GroupBox(string text)
		{
			base.AddToClassList(GroupBox.ussClassName);
			this.text = text;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000020EA File Offset: 0x000002EA
		void IGroupBox.OnOptionAdded(IGroupBoxOption option)
		{
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000020EA File Offset: 0x000002EA
		void IGroupBox.OnOptionRemoved(IGroupBoxOption option)
		{
		}

		// Token: 0x040003FF RID: 1023
		internal static readonly BindingId textProperty = "text";

		// Token: 0x04000400 RID: 1024
		public static readonly string ussClassName = "unity-group-box";

		// Token: 0x04000401 RID: 1025
		public static readonly string labelUssClassName = GroupBox.ussClassName + "__label";

		// Token: 0x04000402 RID: 1026
		private Label m_TitleLabel;

		// Token: 0x020000D0 RID: 208
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<GroupBox, GroupBox.UxmlTraits>
		{
		}

		// Token: 0x020000D1 RID: 209
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x0600067D RID: 1661 RVA: 0x0001F3B1 File Offset: 0x0001D5B1
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((GroupBox)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000403 RID: 1027
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
