using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C8 RID: 712
	public class DebugUIHandlerWidget : MonoBehaviour
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x000481F8 File Offset: 0x000463F8
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00048200 File Offset: 0x00046400
		public DebugUIHandlerWidget parentUIHandler { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00048209 File Offset: 0x00046409
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x00048211 File Offset: 0x00046411
		public DebugUIHandlerWidget previousUIHandler { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0004821A File Offset: 0x0004641A
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00048222 File Offset: 0x00046422
		public DebugUIHandlerWidget nextUIHandler { get; set; }

		// Token: 0x06001312 RID: 4882 RVA: 0x00005704 File Offset: 0x00003904
		protected virtual void OnEnable()
		{
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004822B File Offset: 0x0004642B
		internal virtual void SetWidget(DebugUI.Widget widget)
		{
			this.m_Widget = widget;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00048234 File Offset: 0x00046434
		internal DebugUI.Widget GetWidget()
		{
			return this.m_Widget;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004823C File Offset: 0x0004643C
		protected T CastWidget<T>() where T : DebugUI.Widget
		{
			T t = this.m_Widget as T;
			string typeName = ((this.m_Widget == null) ? "null" : this.m_Widget.GetType().ToString());
			if (t == null)
			{
				string text = "Can't cast ";
				string text2 = typeName;
				string text3 = " to ";
				Type typeFromHandle = typeof(T);
				throw new InvalidOperationException(text + text2 + text3 + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
			}
			return t;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000104EC File Offset: 0x0000E6EC
		public virtual bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return true;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void OnDeselection()
		{
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void OnAction()
		{
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void OnIncrement(bool fast)
		{
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void OnDecrement(bool fast)
		{
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000482AD File Offset: 0x000464AD
		public virtual DebugUIHandlerWidget Previous()
		{
			if (this.previousUIHandler != null)
			{
				return this.previousUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				return this.parentUIHandler;
			}
			return null;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000482DC File Offset: 0x000464DC
		public virtual DebugUIHandlerWidget Next()
		{
			if (this.nextUIHandler != null)
			{
				return this.nextUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				DebugUIHandlerWidget p = this.parentUIHandler;
				while (p != null)
				{
					DebugUIHandlerWidget i = p.nextUIHandler;
					if (i != null)
					{
						return i;
					}
					p = p.parentUIHandler;
				}
			}
			return null;
		}

		// Token: 0x04000C97 RID: 3223
		[HideInInspector]
		public Color colorDefault = new Color(0.8f, 0.8f, 0.8f, 1f);

		// Token: 0x04000C98 RID: 3224
		[HideInInspector]
		public Color colorSelected = new Color(0.25f, 0.65f, 0.8f, 1f);

		// Token: 0x04000C9C RID: 3228
		protected DebugUI.Widget m_Widget;
	}
}
