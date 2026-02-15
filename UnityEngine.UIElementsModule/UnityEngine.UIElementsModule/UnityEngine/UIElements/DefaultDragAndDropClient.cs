using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018B RID: 395
	internal class DefaultDragAndDropClient : DragAndDropData, IDragAndDrop
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000382CC File Offset: 0x000364CC
		public override object source
		{
			get
			{
				return this.GetGenericData("__unity-drag-and-drop__source-view");
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000382DC File Offset: 0x000364DC
		public override object GetGenericData(string key)
		{
			return this.m_GenericData.ContainsKey(key) ? this.m_GenericData[key] : null;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0003830C File Offset: 0x0003650C
		public void StartDrag(StartDragArgs args, Vector3 pointerPosition)
		{
			bool flag = args.unityObjectReferences != null;
			if (flag)
			{
				this.m_UnityObjectReferences = args.unityObjectReferences.ToArray<Object>();
			}
			this.paths = args.assetPaths;
			this.m_VisualMode = args.visualMode;
			foreach (object obj in args.genericData)
			{
				DictionaryEntry entry = (DictionaryEntry)obj;
				this.m_GenericData[(string)entry.Key] = entry.Value;
			}
			bool flag2 = string.IsNullOrWhiteSpace(args.title);
			if (!flag2)
			{
				VisualElement sourceElement = this.source as VisualElement;
				VisualElement root = ((sourceElement != null) ? sourceElement.panel.visualTree : null);
				bool flag3 = root == null;
				if (!flag3)
				{
					if (this.m_DraggedInfoLabel == null)
					{
						this.m_DraggedInfoLabel = new Label
						{
							pickingMode = PickingMode.Ignore,
							style = 
							{
								position = Position.Absolute
							}
						};
					}
					this.m_DraggedInfoLabel.text = args.title;
					this.m_DraggedInfoLabel.style.top = pointerPosition.y;
					this.m_DraggedInfoLabel.style.left = pointerPosition.x;
					root.Add(this.m_DraggedInfoLabel);
				}
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0003848C File Offset: 0x0003668C
		public void UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = this.m_DraggedInfoLabel == null;
			if (!flag)
			{
				this.m_DraggedInfoLabel.style.top = pointerPosition.y;
				this.m_DraggedInfoLabel.style.left = pointerPosition.x;
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000020EA File Offset: 0x000002EA
		public void AcceptDrag()
		{
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000384E1 File Offset: 0x000366E1
		public void SetVisualMode(DragVisualMode mode)
		{
			this.m_VisualMode = mode;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000384EB File Offset: 0x000366EB
		public void DragCleanup()
		{
			this.paths = null;
			this.m_UnityObjectReferences = null;
			Hashtable genericData = this.m_GenericData;
			if (genericData != null)
			{
				genericData.Clear();
			}
			this.SetVisualMode(DragVisualMode.None);
			Label draggedInfoLabel = this.m_DraggedInfoLabel;
			if (draggedInfoLabel != null)
			{
				draggedInfoLabel.RemoveFromHierarchy();
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00038529 File Offset: 0x00036729
		public DragAndDropData data
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400076E RID: 1902
		private readonly Hashtable m_GenericData = new Hashtable();

		// Token: 0x0400076F RID: 1903
		private Label m_DraggedInfoLabel;

		// Token: 0x04000770 RID: 1904
		private DragVisualMode m_VisualMode;

		// Token: 0x04000771 RID: 1905
		private IEnumerable<Object> m_UnityObjectReferences;
	}
}
