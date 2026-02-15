using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002A3 RID: 675
	public class DebugUIHandlerCanvas : MonoBehaviour
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x0004480C File Offset: 0x00042A0C
		private void OnEnable()
		{
			if (this.prefabs == null)
			{
				this.prefabs = new List<DebugUIPrefabBundle>();
			}
			if (this.m_PrefabsMap == null)
			{
				this.m_PrefabsMap = new Dictionary<Type, Transform>();
			}
			if (this.m_UIPanels == null)
			{
				this.m_UIPanels = new List<DebugUIHandlerPanel>();
			}
			DebugManager.instance.RegisterRootCanvas(this);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00044860 File Offset: 0x00042A60
		private void Update()
		{
			int state = DebugManager.instance.GetState();
			if (this.m_DebugTreeState != state)
			{
				this.ResetAllHierarchy();
			}
			this.HandleInput();
			if (this.m_UIPanels != null && this.m_SelectedPanel < this.m_UIPanels.Count && this.m_UIPanels[this.m_SelectedPanel] != null)
			{
				this.m_UIPanels[this.m_SelectedPanel].UpdateScroll();
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000448D7 File Offset: 0x00042AD7
		internal void RequestHierarchyReset()
		{
			this.m_DebugTreeState = -1;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000448E0 File Offset: 0x00042AE0
		private void ResetAllHierarchy()
		{
			foreach (object obj in base.transform)
			{
				CoreUtils.Destroy(((Transform)obj).gameObject);
			}
			this.Rebuild();
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00044944 File Offset: 0x00042B44
		private void Rebuild()
		{
			this.m_PrefabsMap.Clear();
			foreach (DebugUIPrefabBundle bundle in this.prefabs)
			{
				Type type = Type.GetType(bundle.type);
				if (type != null && bundle.prefab != null)
				{
					this.m_PrefabsMap.Add(type, bundle.prefab);
				}
			}
			this.m_UIPanels.Clear();
			this.m_DebugTreeState = DebugManager.instance.GetState();
			ReadOnlyCollection<DebugUI.Panel> panels = DebugManager.instance.panels;
			DebugUIHandlerWidget selectedWidget = null;
			foreach (DebugUI.Panel panel in panels)
			{
				if (!panel.isEditorOnly)
				{
					if (panel.children.Count((DebugUI.Widget x) => !x.isEditorOnly && !x.isHidden) != 0)
					{
						GameObject gameObject = Object.Instantiate<Transform>(this.panelPrefab, base.transform, false).gameObject;
						gameObject.name = panel.displayName;
						DebugUIHandlerPanel uiPanel = gameObject.GetComponent<DebugUIHandlerPanel>();
						uiPanel.SetPanel(panel);
						uiPanel.Canvas = this;
						this.m_UIPanels.Add(uiPanel);
						DebugUIHandlerContainer container = gameObject.GetComponent<DebugUIHandlerContainer>();
						DebugUIHandlerWidget selected = null;
						this.Traverse(panel, container.contentHolder, null, ref selected);
						if (selected != null && selected.GetWidget().queryPath.Contains(panel.queryPath))
						{
							selectedWidget = selected;
						}
					}
				}
			}
			this.ActivatePanel(this.m_SelectedPanel, selectedWidget);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00044B08 File Offset: 0x00042D08
		private void Traverse(DebugUI.IContainer container, Transform parentTransform, DebugUIHandlerWidget parentUIHandler, ref DebugUIHandlerWidget selectedHandler)
		{
			DebugUIHandlerWidget previousUIHandler = null;
			for (int i = 0; i < container.children.Count; i++)
			{
				DebugUI.Widget child = container.children[i];
				if (!child.isEditorOnly && !child.isHidden)
				{
					Transform prefab;
					if (!this.m_PrefabsMap.TryGetValue(child.GetType(), out prefab))
					{
						string text = "DebugUI widget doesn't have a prefab: ";
						Type type = child.GetType();
						Debug.LogWarning(text + ((type != null) ? type.ToString() : null));
					}
					else
					{
						GameObject go = Object.Instantiate<Transform>(prefab, parentTransform, false).gameObject;
						go.name = child.displayName;
						DebugUIHandlerWidget uiHandler = go.GetComponent<DebugUIHandlerWidget>();
						if (uiHandler == null)
						{
							string text2 = "DebugUI prefab is missing a DebugUIHandler for: ";
							Type type2 = child.GetType();
							Debug.LogWarning(text2 + ((type2 != null) ? type2.ToString() : null));
						}
						else
						{
							if (!string.IsNullOrEmpty(this.m_CurrentQueryPath) && child.queryPath.Equals(this.m_CurrentQueryPath))
							{
								selectedHandler = uiHandler;
							}
							if (previousUIHandler != null)
							{
								previousUIHandler.nextUIHandler = uiHandler;
							}
							uiHandler.previousUIHandler = previousUIHandler;
							previousUIHandler = uiHandler;
							uiHandler.parentUIHandler = parentUIHandler;
							uiHandler.SetWidget(child);
							DebugUIHandlerContainer childContainer = go.GetComponent<DebugUIHandlerContainer>();
							if (childContainer != null)
							{
								DebugUI.IContainer childAsContainer = child as DebugUI.IContainer;
								if (childAsContainer != null)
								{
									this.Traverse(childAsContainer, childContainer.contentHolder, uiHandler, ref selectedHandler);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00044C6C File Offset: 0x00042E6C
		private DebugUIHandlerWidget GetWidgetFromPath(string queryPath)
		{
			if (string.IsNullOrEmpty(queryPath))
			{
				return null;
			}
			return this.m_UIPanels[this.m_SelectedPanel].GetComponentsInChildren<DebugUIHandlerWidget>().FirstOrDefault((DebugUIHandlerWidget w) => w.GetWidget().queryPath == queryPath);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00044CBC File Offset: 0x00042EBC
		private void ActivatePanel(int index, DebugUIHandlerWidget selectedWidget = null)
		{
			if (this.m_UIPanels.Count == 0)
			{
				return;
			}
			if (index >= this.m_UIPanels.Count)
			{
				index = this.m_UIPanels.Count - 1;
			}
			this.m_UIPanels.ForEach(delegate(DebugUIHandlerPanel p)
			{
				p.gameObject.SetActive(false);
			});
			this.m_UIPanels[index].gameObject.SetActive(true);
			this.m_SelectedPanel = index;
			if (selectedWidget == null)
			{
				selectedWidget = this.m_UIPanels[index].GetFirstItem();
			}
			this.ChangeSelection(selectedWidget, true);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00044D60 File Offset: 0x00042F60
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			if (widget == null)
			{
				return;
			}
			if (this.m_SelectedWidget != null)
			{
				this.m_SelectedWidget.OnDeselection();
			}
			DebugUIHandlerWidget prev = this.m_SelectedWidget;
			this.m_SelectedWidget = widget;
			this.SetScrollTarget(widget);
			if (!this.m_SelectedWidget.OnSelection(fromNext, prev))
			{
				if (fromNext)
				{
					this.SelectNextItem();
					return;
				}
				this.SelectPreviousItem();
				return;
			}
			else
			{
				if (this.m_SelectedWidget == null || this.m_SelectedWidget.GetWidget() == null)
				{
					this.m_CurrentQueryPath = string.Empty;
					return;
				}
				this.m_CurrentQueryPath = this.m_SelectedWidget.GetWidget().queryPath;
				return;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00044E04 File Offset: 0x00043004
		internal void SelectPreviousItem()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			DebugUIHandlerWidget newSelection = this.m_SelectedWidget.Previous();
			if (newSelection != null)
			{
				this.ChangeSelection(newSelection, false);
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00044E40 File Offset: 0x00043040
		internal void SelectNextPanel()
		{
			int index = this.m_SelectedPanel + 1;
			if (index >= this.m_UIPanels.Count)
			{
				index = 0;
			}
			index = Mathf.Clamp(index, 0, this.m_UIPanels.Count - 1);
			this.ActivatePanel(index, null);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00044E84 File Offset: 0x00043084
		internal void SelectPreviousPanel()
		{
			int index = this.m_SelectedPanel - 1;
			if (index < 0)
			{
				index = this.m_UIPanels.Count - 1;
			}
			index = Mathf.Clamp(index, 0, this.m_UIPanels.Count - 1);
			this.ActivatePanel(index, null);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00044ECC File Offset: 0x000430CC
		internal void SelectNextItem()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			DebugUIHandlerWidget newSelection = this.m_SelectedWidget.Next();
			if (newSelection != null)
			{
				this.ChangeSelection(newSelection, true);
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00044F08 File Offset: 0x00043108
		private void ChangeSelectionValue(float multiplier)
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			bool fast = DebugManager.instance.GetAction(DebugAction.Multiplier) != 0f;
			if (multiplier < 0f)
			{
				this.m_SelectedWidget.OnDecrement(fast);
				return;
			}
			this.m_SelectedWidget.OnIncrement(fast);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00044F5B File Offset: 0x0004315B
		private void ActivateSelection()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			this.m_SelectedWidget.OnAction();
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00044F78 File Offset: 0x00043178
		private void HandleInput()
		{
			if (DebugManager.instance.GetAction(DebugAction.PreviousDebugPanel) != 0f)
			{
				this.SelectPreviousPanel();
			}
			if (DebugManager.instance.GetAction(DebugAction.NextDebugPanel) != 0f)
			{
				this.SelectNextPanel();
			}
			if (DebugManager.instance.GetAction(DebugAction.Action) != 0f)
			{
				this.ActivateSelection();
			}
			if (DebugManager.instance.GetAction(DebugAction.MakePersistent) != 0f && this.m_SelectedWidget != null)
			{
				DebugManager.instance.TogglePersistent(this.m_SelectedWidget.GetWidget(), null);
			}
			float moveHorizontal = DebugManager.instance.GetAction(DebugAction.MoveHorizontal);
			if (moveHorizontal != 0f)
			{
				this.ChangeSelectionValue(moveHorizontal);
			}
			float moveVertical = DebugManager.instance.GetAction(DebugAction.MoveVertical);
			if (moveVertical != 0f)
			{
				if (moveVertical < 0f)
				{
					this.SelectNextItem();
					return;
				}
				this.SelectPreviousItem();
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00045050 File Offset: 0x00043250
		internal void SetScrollTarget(DebugUIHandlerWidget widget)
		{
			if (this.m_UIPanels != null && this.m_SelectedPanel < this.m_UIPanels.Count && this.m_UIPanels[this.m_SelectedPanel] != null)
			{
				this.m_UIPanels[this.m_SelectedPanel].SetScrollTarget(widget);
			}
		}

		// Token: 0x04000C0B RID: 3083
		private int m_DebugTreeState;

		// Token: 0x04000C0C RID: 3084
		private Dictionary<Type, Transform> m_PrefabsMap;

		// Token: 0x04000C0D RID: 3085
		public Transform panelPrefab;

		// Token: 0x04000C0E RID: 3086
		public List<DebugUIPrefabBundle> prefabs;

		// Token: 0x04000C0F RID: 3087
		private List<DebugUIHandlerPanel> m_UIPanels;

		// Token: 0x04000C10 RID: 3088
		private int m_SelectedPanel;

		// Token: 0x04000C11 RID: 3089
		private DebugUIHandlerWidget m_SelectedWidget;

		// Token: 0x04000C12 RID: 3090
		private string m_CurrentQueryPath;
	}
}
