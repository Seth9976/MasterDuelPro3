using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000B0 RID: 176
	[AddComponentMenu("Event/Event System")]
	[DisallowMultipleComponent]
	public class EventSystem : UIBehaviour
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00019237 File Offset: 0x00017437
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00019254 File Offset: 0x00017454
		public static EventSystem current
		{
			get
			{
				if (EventSystem.m_EventSystems.Count <= 0)
				{
					return null;
				}
				return EventSystem.m_EventSystems[0];
			}
			set
			{
				int index = EventSystem.m_EventSystems.IndexOf(value);
				if (index > 0)
				{
					EventSystem.m_EventSystems.RemoveAt(index);
					EventSystem.m_EventSystems.Insert(0, value);
					return;
				}
				if (index < 0)
				{
					Debug.LogError("Failed setting EventSystem.current to unknown EventSystem " + ((value != null) ? value.ToString() : null));
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000192A9 File Offset: 0x000174A9
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x000192B1 File Offset: 0x000174B1
		public bool sendNavigationEvents
		{
			get
			{
				return this.m_sendNavigationEvents;
			}
			set
			{
				this.m_sendNavigationEvents = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000192BA File Offset: 0x000174BA
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x000192C2 File Offset: 0x000174C2
		public int pixelDragThreshold
		{
			get
			{
				return this.m_DragThreshold;
			}
			set
			{
				this.m_DragThreshold = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000192CB File Offset: 0x000174CB
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_CurrentInputModule;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000192D3 File Offset: 0x000174D3
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000192DB File Offset: 0x000174DB
		public GameObject firstSelectedGameObject
		{
			get
			{
				return this.m_FirstSelected;
			}
			set
			{
				this.m_FirstSelected = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000192E4 File Offset: 0x000174E4
		public GameObject currentSelectedGameObject
		{
			get
			{
				return this.m_CurrentSelected;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001513D File Offset: 0x0001333D
		[Obsolete("lastSelectedGameObject is no longer supported")]
		public GameObject lastSelectedGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000192EC File Offset: 0x000174EC
		public bool isFocused
		{
			get
			{
				return this.m_HasFocus;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000192F4 File Offset: 0x000174F4
		protected EventSystem()
		{
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00019320 File Offset: 0x00017520
		public void UpdateModules()
		{
			base.GetComponents<BaseInputModule>(this.m_SystemInputModules);
			for (int i = this.m_SystemInputModules.Count - 1; i >= 0; i--)
			{
				if (!this.m_SystemInputModules[i] || !this.m_SystemInputModules[i].IsActive())
				{
					this.m_SystemInputModules.RemoveAt(i);
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00019383 File Offset: 0x00017583
		public bool alreadySelecting
		{
			get
			{
				return this.m_SelectionGuard;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001938C File Offset: 0x0001758C
		public void SetSelectedGameObject(GameObject selected, BaseEventData pointer)
		{
			if (this.m_SelectionGuard)
			{
				Debug.LogError("Attempting to select " + ((selected != null) ? selected.ToString() : null) + "while already selecting an object.");
				return;
			}
			this.m_SelectionGuard = true;
			if (selected == this.m_CurrentSelected)
			{
				this.m_SelectionGuard = false;
				return;
			}
			ExecuteEvents.Execute<IDeselectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.deselectHandler);
			this.m_CurrentSelected = selected;
			ExecuteEvents.Execute<ISelectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.selectHandler);
			this.m_SelectionGuard = false;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00019412 File Offset: 0x00017612
		private BaseEventData baseEventDataCache
		{
			get
			{
				if (this.m_DummyData == null)
				{
					this.m_DummyData = new BaseEventData(this);
				}
				return this.m_DummyData;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001942E File Offset: 0x0001762E
		public void SetSelectedGameObject(GameObject selected)
		{
			this.SetSelectedGameObject(selected, this.baseEventDataCache);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00019440 File Offset: 0x00017640
		private static int RaycastComparer(RaycastResult lhs, RaycastResult rhs)
		{
			if (lhs.module != rhs.module)
			{
				Camera lhsEventCamera = lhs.module.eventCamera;
				Camera rhsEventCamera = rhs.module.eventCamera;
				if (lhsEventCamera != null && rhsEventCamera != null && lhsEventCamera.depth != rhsEventCamera.depth)
				{
					if (lhsEventCamera.depth < rhsEventCamera.depth)
					{
						return 1;
					}
					if (lhsEventCamera.depth == rhsEventCamera.depth)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (lhs.module.sortOrderPriority != rhs.module.sortOrderPriority)
					{
						return rhs.module.sortOrderPriority.CompareTo(lhs.module.sortOrderPriority);
					}
					if (lhs.module.renderOrderPriority != rhs.module.renderOrderPriority)
					{
						return rhs.module.renderOrderPriority.CompareTo(lhs.module.renderOrderPriority);
					}
				}
			}
			if (lhs.sortingLayer != rhs.sortingLayer)
			{
				int rid = SortingLayer.GetLayerValueFromID(rhs.sortingLayer);
				int lid = SortingLayer.GetLayerValueFromID(lhs.sortingLayer);
				return rid.CompareTo(lid);
			}
			if (lhs.sortingOrder != rhs.sortingOrder)
			{
				return rhs.sortingOrder.CompareTo(lhs.sortingOrder);
			}
			if (lhs.depth != rhs.depth && lhs.module.rootRaycaster == rhs.module.rootRaycaster)
			{
				return rhs.depth.CompareTo(lhs.depth);
			}
			if (lhs.distance != rhs.distance)
			{
				return lhs.distance.CompareTo(rhs.distance);
			}
			if (lhs.sortingGroupID != SortingGroup.invalidSortingGroupID && rhs.sortingGroupID != SortingGroup.invalidSortingGroupID)
			{
				if (lhs.sortingGroupID != rhs.sortingGroupID)
				{
					return lhs.sortingGroupID.CompareTo(rhs.sortingGroupID);
				}
				if (lhs.sortingGroupOrder != rhs.sortingGroupOrder)
				{
					return rhs.sortingGroupOrder.CompareTo(lhs.sortingGroupOrder);
				}
			}
			return lhs.index.CompareTo(rhs.index);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001964C File Offset: 0x0001784C
		public void RaycastAll(PointerEventData eventData, List<RaycastResult> raycastResults)
		{
			raycastResults.Clear();
			List<BaseRaycaster> modules = RaycasterManager.GetRaycasters();
			int modulesCount = modules.Count;
			for (int i = 0; i < modulesCount; i++)
			{
				BaseRaycaster module = modules[i];
				if (!(module == null) && module.IsActive())
				{
					module.Raycast(eventData, raycastResults);
				}
			}
			raycastResults.Sort(EventSystem.s_RaycastComparer);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000196A4 File Offset: 0x000178A4
		public bool IsPointerOverGameObject()
		{
			return this.IsPointerOverGameObject(-1);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000196AD File Offset: 0x000178AD
		public bool IsPointerOverGameObject(int pointerId)
		{
			return this.m_CurrentInputModule != null && this.m_CurrentInputModule.IsPointerOverGameObject(pointerId);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000196CB File Offset: 0x000178CB
		private bool isUIToolkitActiveEventSystem
		{
			get
			{
				return EventSystem.s_UIToolkitOverride.activeEventSystem == this || EventSystem.s_UIToolkitOverride.activeEventSystem == null;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000196F1 File Offset: 0x000178F1
		private bool sendUIToolkitEvents
		{
			get
			{
				return EventSystem.s_UIToolkitOverride.sendEvents && this.isUIToolkitActiveEventSystem;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00019707 File Offset: 0x00017907
		private bool createUIToolkitPanelGameObjectsOnStart
		{
			get
			{
				return EventSystem.s_UIToolkitOverride.createPanelGameObjectsOnStart && this.isUIToolkitActiveEventSystem;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00019720 File Offset: 0x00017920
		public static void SetUITookitEventSystemOverride(EventSystem activeEventSystem, bool sendEvents = true, bool createPanelGameObjectsOnStart = true)
		{
			UIElementsRuntimeUtility.UnregisterEventSystem(UIElementsRuntimeUtility.activeEventSystem);
			EventSystem.s_UIToolkitOverride = new EventSystem.UIToolkitOverrideConfig
			{
				activeEventSystem = activeEventSystem,
				sendEvents = sendEvents,
				createPanelGameObjectsOnStart = createPanelGameObjectsOnStart
			};
			if (sendEvents && ((activeEventSystem != null) ? activeEventSystem : EventSystem.current).isActiveAndEnabled)
			{
				UIElementsRuntimeUtility.RegisterEventSystem(activeEventSystem);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00019780 File Offset: 0x00017980
		private void StartTrackingUIToolkitPanels()
		{
			if (this.createUIToolkitPanelGameObjectsOnStart)
			{
				foreach (Panel panel2 in UIElementsRuntimeUtility.GetSortedPlayerPanels())
				{
					BaseRuntimePanel panel = (BaseRuntimePanel)panel2;
					this.CreateUIToolkitPanelGameObject(panel);
				}
				UIElementsRuntimeUtility.onCreatePanel += this.CreateUIToolkitPanelGameObject;
				this.m_IsTrackingUIToolkitPanels = true;
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000197F8 File Offset: 0x000179F8
		private void StopTrackingUIToolkitPanels()
		{
			if (this.m_IsTrackingUIToolkitPanels)
			{
				UIElementsRuntimeUtility.onCreatePanel -= this.CreateUIToolkitPanelGameObject;
				this.m_IsTrackingUIToolkitPanels = false;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001981C File Offset: 0x00017A1C
		private void CreateUIToolkitPanelGameObject(BaseRuntimePanel panel)
		{
			if (panel.selectableGameObject == null)
			{
				GameObject go = new GameObject(panel.name, new Type[]
				{
					typeof(PanelEventHandler),
					typeof(PanelRaycaster)
				});
				go.transform.SetParent(base.transform);
				panel.selectableGameObject = go;
				panel.destroyed += delegate
				{
					Object.DestroyImmediate(go);
				};
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000198A2 File Offset: 0x00017AA2
		protected override void Start()
		{
			base.Start();
			this.m_Started = true;
			this.StartTrackingUIToolkitPanels();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000198B7 File Offset: 0x00017AB7
		protected override void OnEnable()
		{
			base.OnEnable();
			EventSystem.m_EventSystems.Add(this);
			if (this.m_Started && !this.m_IsTrackingUIToolkitPanels)
			{
				this.StartTrackingUIToolkitPanels();
			}
			if (this.sendUIToolkitEvents)
			{
				UIElementsRuntimeUtility.RegisterEventSystem(this);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000198EE File Offset: 0x00017AEE
		protected override void OnDisable()
		{
			this.StopTrackingUIToolkitPanels();
			UIElementsRuntimeUtility.UnregisterEventSystem(this);
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
				this.m_CurrentInputModule = null;
			}
			EventSystem.m_EventSystems.Remove(this);
			base.OnDisable();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00019930 File Offset: 0x00017B30
		private void TickModules()
		{
			int systemInputModulesCount = this.m_SystemInputModules.Count;
			for (int i = 0; i < systemInputModulesCount; i++)
			{
				if (this.m_SystemInputModules[i] != null)
				{
					this.m_SystemInputModules[i].UpdateModule();
				}
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001997A File Offset: 0x00017B7A
		protected virtual void OnApplicationFocus(bool hasFocus)
		{
			this.m_HasFocus = hasFocus;
			if (!this.m_HasFocus)
			{
				this.TickModules();
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00019994 File Offset: 0x00017B94
		protected virtual void Update()
		{
			if (EventSystem.current != this)
			{
				return;
			}
			this.TickModules();
			bool changedModule = false;
			int systemInputModulesCount = this.m_SystemInputModules.Count;
			int i = 0;
			while (i < systemInputModulesCount)
			{
				BaseInputModule module = this.m_SystemInputModules[i];
				if (module.IsModuleSupported() && module.ShouldActivateModule())
				{
					if (this.m_CurrentInputModule != module)
					{
						this.ChangeEventModule(module);
						changedModule = true;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.m_CurrentInputModule == null)
			{
				for (int j = 0; j < systemInputModulesCount; j++)
				{
					BaseInputModule module2 = this.m_SystemInputModules[j];
					if (module2.IsModuleSupported())
					{
						this.ChangeEventModule(module2);
						changedModule = true;
						break;
					}
				}
			}
			if (!changedModule && this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.Process();
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00019A63 File Offset: 0x00017C63
		private void ChangeEventModule(BaseInputModule module)
		{
			if (this.m_CurrentInputModule == module)
			{
				return;
			}
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
			}
			if (module != null)
			{
				module.ActivateModule();
			}
			this.m_CurrentInputModule = module;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "<b>Selected:</b>";
			GameObject currentSelectedGameObject = this.currentSelectedGameObject;
			stringBuilder.AppendLine(text + ((currentSelectedGameObject != null) ? currentSelectedGameObject.ToString() : null));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine((this.m_CurrentInputModule != null) ? this.m_CurrentInputModule.ToString() : "No module");
			return stringBuilder.ToString();
		}

		// Token: 0x040002DC RID: 732
		private List<BaseInputModule> m_SystemInputModules = new List<BaseInputModule>();

		// Token: 0x040002DD RID: 733
		private BaseInputModule m_CurrentInputModule;

		// Token: 0x040002DE RID: 734
		private static List<EventSystem> m_EventSystems = new List<EventSystem>();

		// Token: 0x040002DF RID: 735
		[SerializeField]
		[FormerlySerializedAs("m_Selected")]
		private GameObject m_FirstSelected;

		// Token: 0x040002E0 RID: 736
		[SerializeField]
		private bool m_sendNavigationEvents = true;

		// Token: 0x040002E1 RID: 737
		[SerializeField]
		private int m_DragThreshold = 10;

		// Token: 0x040002E2 RID: 738
		private GameObject m_CurrentSelected;

		// Token: 0x040002E3 RID: 739
		private bool m_HasFocus = true;

		// Token: 0x040002E4 RID: 740
		private bool m_SelectionGuard;

		// Token: 0x040002E5 RID: 741
		private BaseEventData m_DummyData;

		// Token: 0x040002E6 RID: 742
		private static readonly Comparison<RaycastResult> s_RaycastComparer = new Comparison<RaycastResult>(EventSystem.RaycastComparer);

		// Token: 0x040002E7 RID: 743
		private static EventSystem.UIToolkitOverrideConfig s_UIToolkitOverride = new EventSystem.UIToolkitOverrideConfig
		{
			activeEventSystem = null,
			sendEvents = true,
			createPanelGameObjectsOnStart = true
		};

		// Token: 0x040002E8 RID: 744
		private bool m_Started;

		// Token: 0x040002E9 RID: 745
		private bool m_IsTrackingUIToolkitPanels;

		// Token: 0x020000B1 RID: 177
		private struct UIToolkitOverrideConfig
		{
			// Token: 0x040002EA RID: 746
			public EventSystem activeEventSystem;

			// Token: 0x040002EB RID: 747
			public bool sendEvents;

			// Token: 0x040002EC RID: 748
			public bool createPanelGameObjectsOnStart;
		}
	}
}
