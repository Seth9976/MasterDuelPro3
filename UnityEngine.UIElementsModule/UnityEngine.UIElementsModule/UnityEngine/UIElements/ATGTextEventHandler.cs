using System;
using UnityEngine.TextCore;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000444 RID: 1092
	internal class ATGTextEventHandler
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x0007251C File Offset: 0x0007071C
		public ATGTextEventHandler(TextElement textElement)
		{
			Debug.Assert(textElement.uitkTextHandle.useAdvancedText);
			this.m_TextElement = textElement;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00072548 File Offset: 0x00070748
		private bool HasAllocatedLinkCallbacks()
		{
			return this.m_LinkTagOnPointerDown != null;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00072564 File Offset: 0x00070764
		private void AllocateLinkCallbacks()
		{
			bool flag = this.HasAllocatedLinkCallbacks();
			if (!flag)
			{
				this.m_LinkTagOnPointerDown = new EventCallback<PointerDownEvent>(this.LinkTagOnPointerDown);
				this.m_LinkTagOnPointerUp = new EventCallback<PointerUpEvent>(this.LinkTagOnPointerUp);
				this.m_LinkTagOnPointerMove = new EventCallback<PointerMoveEvent>(this.LinkTagOnPointerMove);
				this.m_LinkTagOnPointerOut = new EventCallback<PointerOutEvent>(this.LinkTagOnPointerOut);
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000725C8 File Offset: 0x000707C8
		private bool HasAllocatedHyperlinkCallbacks()
		{
			return this.m_HyperlinkOnPointerUp != null;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000725E4 File Offset: 0x000707E4
		private void AllocateHyperlinkCallbacks()
		{
			bool flag = this.HasAllocatedHyperlinkCallbacks();
			if (!flag)
			{
				this.m_HyperlinkOnPointerUp = new EventCallback<PointerUpEvent>(this.HyperlinkOnPointerUp);
				this.m_HyperlinkOnPointerMove = new EventCallback<PointerMoveEvent>(this.HyperlinkOnPointerMove);
				this.m_HyperlinkOnPointerOver = new EventCallback<PointerOverEvent>(this.HyperlinkOnPointerOver);
				this.m_HyperlinkOnPointerOut = new EventCallback<PointerOutEvent>(this.HyperlinkOnPointerOut);
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00072648 File Offset: 0x00070848
		private void HyperlinkOnPointerUp(PointerUpEvent pue)
		{
			Vector3 pos = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(pos);
			RichTextTagParser.TagType type = valueTuple.Item1;
			string link = valueTuple.Item2;
			bool flag = link == null || type > RichTextTagParser.TagType.Hyperlink;
			if (!flag)
			{
				bool flag2 = Uri.IsWellFormedUriString(link, UriKind.Absolute);
				if (flag2)
				{
					Application.OpenURL(link);
				}
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000726E0 File Offset: 0x000708E0
		private void HyperlinkOnPointerOver(PointerOverEvent _)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000726EC File Offset: 0x000708EC
		private void HyperlinkOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 pos = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(pos);
			RichTextTagParser.TagType type = valueTuple.Item1;
			string link = valueTuple.Item2;
			BaseVisualElementPanel baseVisualElementPanel = this.m_TextElement.panel as BaseVisualElementPanel;
			ICursorManager cursorManager = ((baseVisualElementPanel != null) ? baseVisualElementPanel.cursorManager : null);
			bool flag = link != null && type == RichTextTagParser.TagType.Hyperlink;
			if (flag)
			{
				bool flag2 = !this.isOverridingCursor;
				if (flag2)
				{
					this.isOverridingCursor = true;
					if (cursorManager != null)
					{
						cursorManager.SetCursor(new Cursor
						{
							defaultCursorId = 4
						});
					}
				}
			}
			else
			{
				bool flag3 = this.isOverridingCursor;
				if (flag3)
				{
					if (cursorManager != null)
					{
						cursorManager.SetCursor(this.m_TextElement.computedStyle.cursor);
					}
					this.isOverridingCursor = false;
				}
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000726E0 File Offset: 0x000708E0
		private void HyperlinkOnPointerOut(PointerOutEvent evt)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000727F8 File Offset: 0x000709F8
		private void LinkTagOnPointerDown(PointerDownEvent pde)
		{
			Vector3 pos = pde.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(pos);
			RichTextTagParser.TagType type = valueTuple.Item1;
			string link = valueTuple.Item2;
			bool flag = link == null || type != RichTextTagParser.TagType.Link;
			if (!flag)
			{
				using (PointerDownLinkTagEvent e = PointerDownLinkTagEvent.GetPooled(pde, link, "test"))
				{
					e.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(e);
				}
			}
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000728CC File Offset: 0x00070ACC
		private void LinkTagOnPointerUp(PointerUpEvent pue)
		{
			Vector3 pos = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(pos);
			RichTextTagParser.TagType type = valueTuple.Item1;
			string link = valueTuple.Item2;
			bool flag = link == null || type != RichTextTagParser.TagType.Link;
			if (!flag)
			{
				using (PointerUpLinkTagEvent e = PointerUpLinkTagEvent.GetPooled(pue, link, "test"))
				{
					e.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(e);
				}
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000729A0 File Offset: 0x00070BA0
		private void LinkTagOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 pos = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(pos);
			RichTextTagParser.TagType type = valueTuple.Item1;
			string link = valueTuple.Item2;
			bool flag = link != null && type == RichTextTagParser.TagType.Link;
			if (flag)
			{
				bool flag2 = this.currentLinkIDHash == -1;
				if (flag2)
				{
					this.currentLinkIDHash = 0;
					using (PointerOverLinkTagEvent e = PointerOverLinkTagEvent.GetPooled(pme, link, "test"))
					{
						e.elementTarget = this.m_TextElement;
						this.m_TextElement.SendEvent(e);
					}
					return;
				}
				bool flag3 = this.currentLinkIDHash == 0;
				if (flag3)
				{
					using (PointerMoveLinkTagEvent e2 = PointerMoveLinkTagEvent.GetPooled(pme, link, "test"))
					{
						e2.elementTarget = this.m_TextElement;
						this.m_TextElement.SendEvent(e2);
					}
					return;
				}
			}
			bool flag4 = this.currentLinkIDHash != -1;
			if (flag4)
			{
				this.currentLinkIDHash = -1;
				using (PointerOutLinkTagEvent e3 = PointerOutLinkTagEvent.GetPooled(pme, string.Empty))
				{
					e3.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(e3);
				}
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00072B4C File Offset: 0x00070D4C
		private void LinkTagOnPointerOut(PointerOutEvent poe)
		{
			bool flag = this.currentLinkIDHash != -1;
			if (flag)
			{
				using (PointerOutLinkTagEvent e = PointerOutLinkTagEvent.GetPooled(poe, string.Empty))
				{
					e.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(e);
				}
				this.currentLinkIDHash = -1;
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00072BB8 File Offset: 0x00070DB8
		internal void RegisterLinkTagCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				this.AllocateLinkCallbacks();
				this.m_TextElement.RegisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
				this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
				this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
				this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00072C34 File Offset: 0x00070E34
		internal void UnRegisterLinkTagCallbacks()
		{
			bool flag = this.HasAllocatedLinkCallbacks();
			if (flag)
			{
				this.m_TextElement.UnregisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x00072C9C File Offset: 0x00070E9C
		internal void RegisterHyperlinkCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				this.AllocateHyperlinkCallbacks();
				this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_HyperlinkOnPointerUp, TrickleDown.TrickleDown);
				bool flag2 = this.m_TextElement.panel.contextType == ContextType.Editor;
				if (flag2)
				{
					this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_HyperlinkOnPointerMove, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerOverEvent>(this.m_HyperlinkOnPointerOver, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_HyperlinkOnPointerOut, TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00072D34 File Offset: 0x00070F34
		internal void UnRegisterHyperlinkCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				bool flag2 = this.HasAllocatedHyperlinkCallbacks();
				if (flag2)
				{
					this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_HyperlinkOnPointerUp, TrickleDown.TrickleDown);
					bool flag3 = this.m_TextElement.panel.contextType == ContextType.Editor;
					if (flag3)
					{
						this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_HyperlinkOnPointerMove, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerOverEvent>(this.m_HyperlinkOnPointerOver, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_HyperlinkOnPointerOut, TrickleDown.TrickleDown);
					}
				}
			}
		}

		// Token: 0x04000DEF RID: 3567
		private TextElement m_TextElement;

		// Token: 0x04000DF0 RID: 3568
		private EventCallback<PointerDownEvent> m_LinkTagOnPointerDown;

		// Token: 0x04000DF1 RID: 3569
		private EventCallback<PointerUpEvent> m_LinkTagOnPointerUp;

		// Token: 0x04000DF2 RID: 3570
		private EventCallback<PointerMoveEvent> m_LinkTagOnPointerMove;

		// Token: 0x04000DF3 RID: 3571
		private EventCallback<PointerOutEvent> m_LinkTagOnPointerOut;

		// Token: 0x04000DF4 RID: 3572
		private EventCallback<PointerUpEvent> m_HyperlinkOnPointerUp;

		// Token: 0x04000DF5 RID: 3573
		private EventCallback<PointerMoveEvent> m_HyperlinkOnPointerMove;

		// Token: 0x04000DF6 RID: 3574
		private EventCallback<PointerOverEvent> m_HyperlinkOnPointerOver;

		// Token: 0x04000DF7 RID: 3575
		private EventCallback<PointerOutEvent> m_HyperlinkOnPointerOut;

		// Token: 0x04000DF8 RID: 3576
		internal bool isOverridingCursor;

		// Token: 0x04000DF9 RID: 3577
		internal int currentLinkIDHash = -1;
	}
}
