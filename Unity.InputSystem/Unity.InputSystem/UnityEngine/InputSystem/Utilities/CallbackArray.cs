using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000236 RID: 566
	internal struct CallbackArray<TDelegate> where TDelegate : Delegate
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0005E835 File Offset: 0x0005CA35
		public int length
		{
			get
			{
				return this.m_Callbacks.length;
			}
		}

		// Token: 0x170005DF RID: 1503
		public TDelegate this[int index]
		{
			get
			{
				return this.m_Callbacks[index];
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0005E850 File Offset: 0x0005CA50
		public void Clear()
		{
			this.m_Callbacks.Clear();
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005E874 File Offset: 0x0005CA74
		public void AddCallback(TDelegate dlg)
		{
			if (!this.m_CannotMutateCallbacksArray)
			{
				if (!this.m_Callbacks.Contains(dlg))
				{
					this.m_Callbacks.AppendWithCapacity(dlg, 4);
				}
				return;
			}
			if (this.m_CallbacksToAdd.Contains(dlg))
			{
				return;
			}
			int removeIndex = this.m_CallbacksToRemove.IndexOf(dlg);
			if (removeIndex != -1)
			{
				this.m_CallbacksToRemove.RemoveAtByMovingTailWithCapacity(removeIndex);
			}
			this.m_CallbacksToAdd.AppendWithCapacity(dlg, 10);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005E8E4 File Offset: 0x0005CAE4
		public void RemoveCallback(TDelegate dlg)
		{
			if (!this.m_CannotMutateCallbacksArray)
			{
				int index = this.m_Callbacks.IndexOf(dlg);
				if (index >= 0)
				{
					this.m_Callbacks.RemoveAtWithCapacity(index);
				}
				return;
			}
			if (this.m_CallbacksToRemove.Contains(dlg))
			{
				return;
			}
			int addIndex = this.m_CallbacksToAdd.IndexOf(dlg);
			if (addIndex != -1)
			{
				this.m_CallbacksToAdd.RemoveAtByMovingTailWithCapacity(addIndex);
			}
			this.m_CallbacksToRemove.AppendWithCapacity(dlg, 10);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0005E952 File Offset: 0x0005CB52
		public void LockForChanges()
		{
			this.m_CannotMutateCallbacksArray = true;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005E95C File Offset: 0x0005CB5C
		public void UnlockForChanges()
		{
			this.m_CannotMutateCallbacksArray = false;
			for (int i = 0; i < this.m_CallbacksToRemove.length; i++)
			{
				this.RemoveCallback(this.m_CallbacksToRemove[i]);
			}
			for (int j = 0; j < this.m_CallbacksToAdd.length; j++)
			{
				this.AddCallback(this.m_CallbacksToAdd[j]);
			}
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x04000C41 RID: 3137
		private bool m_CannotMutateCallbacksArray;

		// Token: 0x04000C42 RID: 3138
		private InlinedArray<TDelegate> m_Callbacks;

		// Token: 0x04000C43 RID: 3139
		private InlinedArray<TDelegate> m_CallbacksToAdd;

		// Token: 0x04000C44 RID: 3140
		private InlinedArray<TDelegate> m_CallbacksToRemove;
	}
}
