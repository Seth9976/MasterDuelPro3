using System;

namespace System.Collections.Specialized
{
	/// <summary>Provides data for the <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event.</summary>
	// Token: 0x0200030A RID: 778
	public class NotifyCollectionChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" /> change.</summary>
		/// <param name="action">The action that caused the event. This must be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />.</param>
		// Token: 0x06001300 RID: 4864 RVA: 0x000547A1 File Offset: 0x000529A1
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
		{
			if (action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.Format("Constructor supports only the '{0}' action.", NotifyCollectionChangedAction.Reset), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItem">The item that is affected by the change.</param>
		/// <param name="index">The index where the change occurred.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, or if <paramref name="action" /> is Reset and either <paramref name="changedItems" /> is not null or <paramref name="index" /> is not -1.</exception>
		// Token: 0x06001301 RID: 4865 RVA: 0x000547E0 File Offset: 0x000529E0
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException("Constructor only supports either a Reset, Add, or Remove action.", "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[] { changedItem }, index);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException("Reset action must be initialized with no changed items.", "action");
			}
			if (index != -1)
			{
				throw new ArgumentException("Reset action must be initialized with index -1.", "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItem">The new item that is replacing the original item.</param>
		/// <param name="oldItem">The original item that is replaced.</param>
		/// <param name="index">The index of the item being replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		// Token: 0x06001302 RID: 4866 RVA: 0x00054864 File Offset: 0x00052A64
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.Format("Constructor supports only the '{0}' action.", NotifyCollectionChangedAction.Replace), "action");
			}
			this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, index, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItems">The new items that are replacing the original items.</param>
		/// <param name="oldItems">The original items that are replaced.</param>
		/// <param name="startingIndex">The index of the first item of the items that are being replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="oldItems" /> or <paramref name="newItems" /> is null.</exception>
		// Token: 0x06001303 RID: 4867 RVA: 0x000548C4 File Offset: 0x00052AC4
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.Format("Constructor supports only the '{0}' action.", NotifyCollectionChangedAction.Replace), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0005492D File Offset: 0x00052B2D
		private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action == NotifyCollectionChangedAction.Add)
			{
				this.InitializeAdd(action, changedItems, startingIndex);
				return;
			}
			if (action == NotifyCollectionChangedAction.Remove)
			{
				this.InitializeRemove(action, changedItems, startingIndex);
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00054949 File Offset: 0x00052B49
		private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : new ReadOnlyList(newItems));
			this._newStartingIndex = newStartingIndex;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0005496B File Offset: 0x00052B6B
		private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
		{
			this._action = action;
			this._oldItems = ((oldItems == null) ? null : new ReadOnlyList(oldItems));
			this._oldStartingIndex = oldStartingIndex;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0005498D File Offset: 0x00052B8D
		private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
		{
			this.InitializeAdd(action, newItems, startingIndex);
			this.InitializeRemove(action, oldItems, oldStartingIndex);
		}

		// Token: 0x04000B7D RID: 2941
		private NotifyCollectionChangedAction _action;

		// Token: 0x04000B7E RID: 2942
		private IList _newItems;

		// Token: 0x04000B7F RID: 2943
		private IList _oldItems;

		// Token: 0x04000B80 RID: 2944
		private int _newStartingIndex = -1;

		// Token: 0x04000B81 RID: 2945
		private int _oldStartingIndex = -1;
	}
}
