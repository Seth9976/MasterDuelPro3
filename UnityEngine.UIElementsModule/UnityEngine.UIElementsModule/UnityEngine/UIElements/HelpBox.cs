using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D7 RID: 215
	public class HelpBox : VisualElement
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001F700 File Offset: 0x0001D900
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001F720 File Offset: 0x0001D920
		[CreateProperty]
		public string text
		{
			get
			{
				return this.m_Label.text;
			}
			set
			{
				string previous = this.text;
				this.m_Label.text = value;
				bool flag = string.CompareOrdinal(previous, this.text) != 0;
				if (flag)
				{
					base.NotifyPropertyChanged(in HelpBox.textProperty);
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001F764 File Offset: 0x0001D964
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001F77C File Offset: 0x0001D97C
		[CreateProperty]
		public HelpBoxMessageType messageType
		{
			get
			{
				return this.m_HelpBoxMessageType;
			}
			set
			{
				bool flag = value != this.m_HelpBoxMessageType;
				if (flag)
				{
					this.m_HelpBoxMessageType = value;
					this.UpdateIcon(value);
					base.NotifyPropertyChanged(in HelpBox.messageTypeProperty);
				}
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001F7B7 File Offset: 0x0001D9B7
		public HelpBox()
			: this(string.Empty, HelpBoxMessageType.None)
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		public HelpBox(string text, HelpBoxMessageType messageType)
		{
			base.AddToClassList(HelpBox.ussClassName);
			this.m_HelpBoxMessageType = messageType;
			this.m_Label = new Label(text);
			this.m_Label.AddToClassList(HelpBox.labelUssClassName);
			base.Add(this.m_Label);
			this.m_Icon = new VisualElement();
			this.m_Icon.AddToClassList(HelpBox.iconUssClassName);
			this.UpdateIcon(messageType);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001F840 File Offset: 0x0001DA40
		private string GetIconClass(HelpBoxMessageType messageType)
		{
			string text;
			switch (messageType)
			{
			case HelpBoxMessageType.Info:
				text = HelpBox.iconInfoUssClassName;
				break;
			case HelpBoxMessageType.Warning:
				text = HelpBox.iconwarningUssClassName;
				break;
			case HelpBoxMessageType.Error:
				text = HelpBox.iconErrorUssClassName;
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001F888 File Offset: 0x0001DA88
		private void UpdateIcon(HelpBoxMessageType messageType)
		{
			bool flag = !string.IsNullOrEmpty(this.m_IconClass);
			if (flag)
			{
				this.m_Icon.RemoveFromClassList(this.m_IconClass);
			}
			this.m_IconClass = this.GetIconClass(messageType);
			bool flag2 = this.m_IconClass == null;
			if (flag2)
			{
				this.m_Icon.RemoveFromHierarchy();
			}
			else
			{
				this.m_Icon.AddToClassList(this.m_IconClass);
				bool flag3 = this.m_Icon.parent == null;
				if (flag3)
				{
					base.Insert(0, this.m_Icon);
				}
			}
		}

		// Token: 0x0400040D RID: 1037
		internal static readonly BindingId textProperty = "text";

		// Token: 0x0400040E RID: 1038
		internal static readonly BindingId messageTypeProperty = "messageType";

		// Token: 0x0400040F RID: 1039
		public static readonly string ussClassName = "unity-help-box";

		// Token: 0x04000410 RID: 1040
		public static readonly string labelUssClassName = HelpBox.ussClassName + "__label";

		// Token: 0x04000411 RID: 1041
		public static readonly string iconUssClassName = HelpBox.ussClassName + "__icon";

		// Token: 0x04000412 RID: 1042
		public static readonly string iconInfoUssClassName = HelpBox.iconUssClassName + "--info";

		// Token: 0x04000413 RID: 1043
		public static readonly string iconwarningUssClassName = HelpBox.iconUssClassName + "--warning";

		// Token: 0x04000414 RID: 1044
		public static readonly string iconErrorUssClassName = HelpBox.iconUssClassName + "--error";

		// Token: 0x04000415 RID: 1045
		private HelpBoxMessageType m_HelpBoxMessageType;

		// Token: 0x04000416 RID: 1046
		private VisualElement m_Icon;

		// Token: 0x04000417 RID: 1047
		private string m_IconClass;

		// Token: 0x04000418 RID: 1048
		private Label m_Label;

		// Token: 0x020000D8 RID: 216
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<HelpBox, HelpBox.UxmlTraits>
		{
		}

		// Token: 0x020000D9 RID: 217
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x0600069B RID: 1691 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				HelpBox helpBox = ve as HelpBox;
				helpBox.text = this.m_Text.GetValueFromBag(bag, cc);
				helpBox.messageType = this.m_MessageType.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000419 RID: 1049
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x0400041A RID: 1050
			private UxmlEnumAttributeDescription<HelpBoxMessageType> m_MessageType = new UxmlEnumAttributeDescription<HelpBoxMessageType>
			{
				name = "message-type",
				defaultValue = HelpBoxMessageType.None
			};
		}
	}
}
