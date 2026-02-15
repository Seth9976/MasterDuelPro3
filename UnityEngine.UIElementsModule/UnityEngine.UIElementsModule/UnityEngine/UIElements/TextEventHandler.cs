using System;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000448 RID: 1096
	internal class TextEventHandler
	{
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x000740A5 File Offset: 0x000722A5
		private TextInfo textInfo
		{
			get
			{
				return this.m_TextElement.uitkTextHandle.textInfo;
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000740B7 File Offset: 0x000722B7
		public TextEventHandler(TextElement textElement)
		{
			this.m_TextElement = textElement;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000740D0 File Offset: 0x000722D0
		private bool HasAllocatedLinkCallbacks()
		{
			return this.m_LinkTagOnPointerDown != null;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000740EC File Offset: 0x000722EC
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

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00074150 File Offset: 0x00072350
		private bool HasAllocatedATagCallbacks()
		{
			return this.m_ATagOnPointerUp != null;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0007416C File Offset: 0x0007236C
		private void AllocateATagCallbacks()
		{
			bool flag = this.HasAllocatedATagCallbacks();
			if (!flag)
			{
				this.m_ATagOnPointerUp = new EventCallback<PointerUpEvent>(this.ATagOnPointerUp);
				this.m_ATagOnPointerMove = new EventCallback<PointerMoveEvent>(this.ATagOnPointerMove);
				this.m_ATagOnPointerOver = new EventCallback<PointerOverEvent>(this.ATagOnPointerOver);
				this.m_ATagOnPointerOut = new EventCallback<PointerOutEvent>(this.ATagOnPointerOut);
			}
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000741D0 File Offset: 0x000723D0
		private void ATagOnPointerUp(PointerUpEvent pue)
		{
			Vector3 pos = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int intersectingLink = this.m_TextElement.uitkTextHandle.FindIntersectingLink(pos, true);
			bool flag = intersectingLink < 0;
			if (!flag)
			{
				LinkInfo link = this.textInfo.linkInfo[intersectingLink];
				bool flag2 = link.hashCode != 2535353;
				if (!flag2)
				{
					bool flag3 = link.linkId == null || link.linkIdLength <= 0;
					if (!flag3)
					{
						string href = link.GetLinkId();
						bool flag4 = Uri.IsWellFormedUriString(href, UriKind.Absolute);
						if (flag4)
						{
							Application.OpenURL(href);
						}
					}
				}
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000742A5 File Offset: 0x000724A5
		private void ATagOnPointerOver(PointerOverEvent _)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000742B0 File Offset: 0x000724B0
		private void ATagOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 pos = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int intersectingLink = this.m_TextElement.uitkTextHandle.FindIntersectingLink(pos, true);
			BaseVisualElementPanel baseVisualElementPanel = this.m_TextElement.panel as BaseVisualElementPanel;
			ICursorManager cursorManager = ((baseVisualElementPanel != null) ? baseVisualElementPanel.cursorManager : null);
			bool flag = intersectingLink >= 0;
			if (flag)
			{
				LinkInfo link = this.textInfo.linkInfo[intersectingLink];
				bool flag2 = link.hashCode == 2535353;
				if (flag2)
				{
					bool flag3 = !this.isOverridingCursor;
					if (flag3)
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
					return;
				}
			}
			bool flag4 = this.isOverridingCursor;
			if (flag4)
			{
				if (cursorManager != null)
				{
					cursorManager.SetCursor(this.m_TextElement.computedStyle.cursor);
				}
				this.isOverridingCursor = false;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000742A5 File Offset: 0x000724A5
		private void ATagOnPointerOut(PointerOutEvent evt)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000743D0 File Offset: 0x000725D0
		private void LinkTagOnPointerDown(PointerDownEvent pde)
		{
			Vector3 pos = pde.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int intersectingLink = this.m_TextElement.uitkTextHandle.FindIntersectingLink(pos, true);
			bool flag = intersectingLink < 0;
			if (!flag)
			{
				LinkInfo link = this.textInfo.linkInfo[intersectingLink];
				bool flag2 = link.hashCode == 2535353;
				if (!flag2)
				{
					bool flag3 = link.linkId == null || link.linkIdLength <= 0;
					if (!flag3)
					{
						using (PointerDownLinkTagEvent e = PointerDownLinkTagEvent.GetPooled(pde, link.GetLinkId(), link.GetLinkText(this.textInfo)))
						{
							e.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(e);
						}
					}
				}
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000744E0 File Offset: 0x000726E0
		private void LinkTagOnPointerUp(PointerUpEvent pue)
		{
			Vector3 pos = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int intersectingLink = this.m_TextElement.uitkTextHandle.FindIntersectingLink(pos, true);
			bool flag = intersectingLink < 0;
			if (!flag)
			{
				LinkInfo link = this.textInfo.linkInfo[intersectingLink];
				bool flag2 = link.hashCode == 2535353;
				if (!flag2)
				{
					bool flag3 = link.linkId == null || link.linkIdLength <= 0;
					if (!flag3)
					{
						using (PointerUpLinkTagEvent e = PointerUpLinkTagEvent.GetPooled(pue, link.GetLinkId(), link.GetLinkText(this.textInfo)))
						{
							e.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(e);
						}
					}
				}
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000745F0 File Offset: 0x000727F0
		private void LinkTagOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 pos = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int intersectingLink = this.m_TextElement.uitkTextHandle.FindIntersectingLink(pos, true);
			bool flag = intersectingLink >= 0;
			if (flag)
			{
				LinkInfo link = this.textInfo.linkInfo[intersectingLink];
				bool flag2 = link.hashCode != 2535353;
				if (flag2)
				{
					bool flag3 = this.currentLinkIDHash == -1;
					if (flag3)
					{
						this.currentLinkIDHash = link.hashCode;
						using (PointerOverLinkTagEvent e = PointerOverLinkTagEvent.GetPooled(pme, link.GetLinkId(), link.GetLinkText(this.textInfo)))
						{
							e.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(e);
						}
						return;
					}
					bool flag4 = this.currentLinkIDHash == link.hashCode;
					if (flag4)
					{
						using (PointerMoveLinkTagEvent e2 = PointerMoveLinkTagEvent.GetPooled(pme, link.GetLinkId(), link.GetLinkText(this.textInfo)))
						{
							e2.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(e2);
						}
						return;
					}
				}
			}
			bool flag5 = this.currentLinkIDHash != -1;
			if (flag5)
			{
				this.currentLinkIDHash = -1;
				using (PointerOutLinkTagEvent e3 = PointerOutLinkTagEvent.GetPooled(pme, string.Empty))
				{
					e3.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(e3);
				}
			}
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000747DC File Offset: 0x000729DC
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

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00074848 File Offset: 0x00072A48
		internal void HandleLinkAndATagCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				bool flag2 = this.hasLinkTag;
				if (flag2)
				{
					this.AllocateLinkCallbacks();
					this.m_TextElement.RegisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
				}
				else
				{
					bool flag3 = this.HasAllocatedLinkCallbacks();
					if (flag3)
					{
						this.m_TextElement.UnregisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
					}
				}
				bool flag4 = this.hasATag;
				if (flag4)
				{
					this.AllocateATagCallbacks();
					this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_ATagOnPointerUp, TrickleDown.TrickleDown);
					bool flag5 = this.m_TextElement.panel.contextType == ContextType.Editor;
					if (flag5)
					{
						this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_ATagOnPointerMove, TrickleDown.TrickleDown);
						this.m_TextElement.RegisterCallback<PointerOverEvent>(this.m_ATagOnPointerOver, TrickleDown.TrickleDown);
						this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_ATagOnPointerOut, TrickleDown.TrickleDown);
					}
				}
				else
				{
					bool flag6 = this.HasAllocatedATagCallbacks();
					if (flag6)
					{
						this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_ATagOnPointerUp, TrickleDown.TrickleDown);
						bool flag7 = this.m_TextElement.panel.contextType == ContextType.Editor;
						if (flag7)
						{
							this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_ATagOnPointerMove, TrickleDown.TrickleDown);
							this.m_TextElement.UnregisterCallback<PointerOverEvent>(this.m_ATagOnPointerOver, TrickleDown.TrickleDown);
							this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_ATagOnPointerOut, TrickleDown.TrickleDown);
						}
					}
				}
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00074A20 File Offset: 0x00072C20
		internal void HandleLinkTag()
		{
			for (int i = 0; i < this.textInfo.linkCount; i++)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[i];
				bool flag = linkInfo.hashCode != 2535353;
				if (flag)
				{
					this.hasLinkTag = true;
					this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
					return;
				}
			}
			bool flag2 = this.hasLinkTag;
			if (flag2)
			{
				this.hasLinkTag = false;
				this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
				return;
			}
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00074AB4 File Offset: 0x00072CB4
		internal void HandleATag()
		{
			for (int i = 0; i < this.textInfo.linkCount; i++)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[i];
				bool flag = linkInfo.hashCode == 2535353;
				if (flag)
				{
					this.hasATag = true;
					this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
					return;
				}
			}
			bool flag2 = this.hasATag;
			if (flag2)
			{
				this.hasATag = false;
				this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
				return;
			}
		}

		// Token: 0x04000E0A RID: 3594
		private TextElement m_TextElement;

		// Token: 0x04000E0B RID: 3595
		private EventCallback<PointerDownEvent> m_LinkTagOnPointerDown;

		// Token: 0x04000E0C RID: 3596
		private EventCallback<PointerUpEvent> m_LinkTagOnPointerUp;

		// Token: 0x04000E0D RID: 3597
		private EventCallback<PointerMoveEvent> m_LinkTagOnPointerMove;

		// Token: 0x04000E0E RID: 3598
		private EventCallback<PointerOutEvent> m_LinkTagOnPointerOut;

		// Token: 0x04000E0F RID: 3599
		private EventCallback<PointerUpEvent> m_ATagOnPointerUp;

		// Token: 0x04000E10 RID: 3600
		private EventCallback<PointerMoveEvent> m_ATagOnPointerMove;

		// Token: 0x04000E11 RID: 3601
		private EventCallback<PointerOverEvent> m_ATagOnPointerOver;

		// Token: 0x04000E12 RID: 3602
		private EventCallback<PointerOutEvent> m_ATagOnPointerOut;

		// Token: 0x04000E13 RID: 3603
		internal bool isOverridingCursor;

		// Token: 0x04000E14 RID: 3604
		internal int currentLinkIDHash = -1;

		// Token: 0x04000E15 RID: 3605
		internal bool hasLinkTag;

		// Token: 0x04000E16 RID: 3606
		internal bool hasATag;
	}
}
