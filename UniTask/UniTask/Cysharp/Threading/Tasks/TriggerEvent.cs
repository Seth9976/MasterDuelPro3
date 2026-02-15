using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000073 RID: 115
	public struct TriggerEvent<T>
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00005A17 File Offset: 0x00003C17
		private void LogError(Exception ex)
		{
			Debug.LogException(ex);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005A20 File Offset: 0x00003C20
		public void SetResult(T value)
		{
			if (this.iteratingNode != null)
			{
				throw new InvalidOperationException("Can not trigger itself in iterating.");
			}
			for (ITriggerHandler<T> h = this.head; h != null; h = ((h == this.iteratingNode) ? h.Next : this.iteratingNode))
			{
				this.iteratingNode = h;
				try
				{
					h.OnNext(value);
				}
				catch (Exception ex)
				{
					this.LogError(ex);
					this.Remove(h);
				}
			}
			this.iteratingNode = null;
			if (this.iteratingHead != null)
			{
				this.Add(this.iteratingHead);
				this.iteratingHead = null;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public void SetCanceled(CancellationToken cancellationToken)
		{
			if (this.iteratingNode != null)
			{
				throw new InvalidOperationException("Can not trigger itself in iterating.");
			}
			ITriggerHandler<T> triggerHandler;
			for (ITriggerHandler<T> h = this.head; h != null; h = triggerHandler)
			{
				this.iteratingNode = h;
				try
				{
					h.OnCanceled(cancellationToken);
				}
				catch (Exception ex)
				{
					this.LogError(ex);
				}
				triggerHandler = ((h == this.iteratingNode) ? h.Next : this.iteratingNode);
				this.iteratingNode = null;
				this.Remove(h);
			}
			this.iteratingNode = null;
			if (this.iteratingHead != null)
			{
				this.Add(this.iteratingHead);
				this.iteratingHead = null;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005B58 File Offset: 0x00003D58
		public void SetCompleted()
		{
			if (this.iteratingNode != null)
			{
				throw new InvalidOperationException("Can not trigger itself in iterating.");
			}
			ITriggerHandler<T> triggerHandler;
			for (ITriggerHandler<T> h = this.head; h != null; h = triggerHandler)
			{
				this.iteratingNode = h;
				try
				{
					h.OnCompleted();
				}
				catch (Exception ex)
				{
					this.LogError(ex);
				}
				triggerHandler = ((h == this.iteratingNode) ? h.Next : this.iteratingNode);
				this.iteratingNode = null;
				this.Remove(h);
			}
			this.iteratingNode = null;
			if (this.iteratingHead != null)
			{
				this.Add(this.iteratingHead);
				this.iteratingHead = null;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public void SetError(Exception exception)
		{
			if (this.iteratingNode != null)
			{
				throw new InvalidOperationException("Can not trigger itself in iterating.");
			}
			ITriggerHandler<T> triggerHandler;
			for (ITriggerHandler<T> h = this.head; h != null; h = triggerHandler)
			{
				this.iteratingNode = h;
				try
				{
					h.OnError(exception);
				}
				catch (Exception ex)
				{
					this.LogError(ex);
				}
				triggerHandler = ((h == this.iteratingNode) ? h.Next : this.iteratingNode);
				this.iteratingNode = null;
				this.Remove(h);
			}
			this.iteratingNode = null;
			if (this.iteratingHead != null)
			{
				this.Add(this.iteratingHead);
				this.iteratingHead = null;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005C98 File Offset: 0x00003E98
		public void Add(ITriggerHandler<T> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.head == null)
			{
				this.head = handler;
				return;
			}
			if (this.iteratingNode != null)
			{
				if (this.iteratingHead == null)
				{
					this.iteratingHead = handler;
					return;
				}
				ITriggerHandler<T> last = this.iteratingHead.Prev;
				if (last == null)
				{
					this.iteratingHead.Prev = handler;
					this.iteratingHead.Next = handler;
					handler.Prev = this.iteratingHead;
					return;
				}
				this.iteratingHead.Prev = handler;
				last.Next = handler;
				handler.Prev = last;
				return;
			}
			else
			{
				ITriggerHandler<T> last2 = this.head.Prev;
				if (last2 == null)
				{
					this.head.Prev = handler;
					this.head.Next = handler;
					handler.Prev = this.head;
					return;
				}
				this.head.Prev = handler;
				last2.Next = handler;
				handler.Prev = last2;
				return;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005D78 File Offset: 0x00003F78
		public void Remove(ITriggerHandler<T> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			ITriggerHandler<T> prev = handler.Prev;
			ITriggerHandler<T> next = handler.Next;
			if (next != null)
			{
				next.Prev = prev;
			}
			if (handler == this.head)
			{
				this.head = next;
			}
			else if (prev != null)
			{
				prev.Next = next;
			}
			if (handler == this.iteratingNode)
			{
				this.iteratingNode = next;
			}
			if (handler == this.iteratingHead)
			{
				this.iteratingHead = next;
			}
			if (this.head != null && this.head.Prev == handler)
			{
				if (prev != this.head)
				{
					this.head.Prev = prev;
				}
				else
				{
					this.head.Prev = null;
				}
			}
			if (this.iteratingHead != null && this.iteratingHead.Prev == handler)
			{
				if (prev != this.iteratingHead.Prev)
				{
					this.iteratingHead.Prev = prev;
				}
				else
				{
					this.iteratingHead.Prev = null;
				}
			}
			handler.Prev = null;
			handler.Next = null;
		}

		// Token: 0x040000F9 RID: 249
		private ITriggerHandler<T> head;

		// Token: 0x040000FA RID: 250
		private ITriggerHandler<T> iteratingHead;

		// Token: 0x040000FB RID: 251
		private ITriggerHandler<T> iteratingNode;
	}
}
