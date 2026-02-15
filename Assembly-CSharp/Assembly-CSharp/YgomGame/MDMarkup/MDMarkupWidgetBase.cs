using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD2 RID: 3026
	public abstract class MDMarkupWidgetBase : ElementWidgetBase
	{
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x0000216A File Offset: 0x0000036A
		private MDMarkupIndentWidget YgomGame_002EMDMarkup_002EIMDMarkupWidget_002EindentWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600564A RID: 22090 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupWidgetBase(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null)
		{
		}

		// Token: 0x0600564C RID: 22092
		public abstract void BindContentData(IMDMarkupContent mdMarkupContent);

		// Token: 0x0600564D RID: 22093 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnReady()
		{
		}

		// Token: 0x04009330 RID: 37680
		public readonly MDMarkupIndentWidget indentWidget;
	}
}
