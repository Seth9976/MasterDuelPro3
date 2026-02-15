using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004A RID: 74
	public class SignalReceiver : MonoBehaviour, INotificationReceiver
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000909C File Offset: 0x0000729C
		public void OnNotify(Playable origin, INotification notification, object context)
		{
			SignalEmitter signal = notification as SignalEmitter;
			UnityEvent evt;
			if (signal != null && signal.asset != null && this.m_Events.TryGetValue(signal.asset, out evt) && evt != null)
			{
				evt.Invoke();
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000090E8 File Offset: 0x000072E8
		public void AddReaction(SignalAsset asset, UnityEvent reaction)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("SignalAsset already used.");
			}
			this.m_Events.Append(asset, reaction);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009134 File Offset: 0x00007334
		public int AddEmptyReaction(UnityEvent reaction)
		{
			this.m_Events.Append(null, reaction);
			return this.m_Events.events.Count - 1;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009155 File Offset: 0x00007355
		public void Remove(SignalAsset asset)
		{
			if (!this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("The SignalAsset is not registered with this receiver.");
			}
			this.m_Events.Remove(asset);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009181 File Offset: 0x00007381
		public IEnumerable<SignalAsset> GetRegisteredSignals()
		{
			return this.m_Events.signals;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009190 File Offset: 0x00007390
		public UnityEvent GetReaction(SignalAsset key)
		{
			UnityEvent ret;
			if (this.m_Events.TryGetValue(key, out ret))
			{
				return ret;
			}
			return null;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000091B0 File Offset: 0x000073B0
		public int Count()
		{
			return this.m_Events.signals.Count;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000091C4 File Offset: 0x000073C4
		public void ChangeSignalAtIndex(int idx, SignalAsset newKey)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			if (this.m_Events.signals[idx] == newKey)
			{
				return;
			}
			bool alreadyUsed = this.m_Events.signals.Contains(newKey);
			if (newKey == null || this.m_Events.signals[idx] == null || !alreadyUsed)
			{
				this.m_Events.signals[idx] = newKey;
			}
			if (newKey != null && alreadyUsed)
			{
				throw new ArgumentException("SignalAsset already used.");
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009269 File Offset: 0x00007469
		public void RemoveAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.Remove(idx);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009296 File Offset: 0x00007496
		public void ChangeReactionAtIndex(int idx, UnityEvent reaction)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.events[idx] = reaction;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000092C9 File Offset: 0x000074C9
		public UnityEvent GetReactionAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.events[idx];
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000092FB File Offset: 0x000074FB
		public SignalAsset GetSignalAssetAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.signals[idx];
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00002811 File Offset: 0x00000A11
		private void OnEnable()
		{
		}

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private SignalReceiver.EventKeyValue m_Events = new SignalReceiver.EventKeyValue();

		// Token: 0x0200004B RID: 75
		[Serializable]
		private class EventKeyValue
		{
			// Token: 0x060002AE RID: 686 RVA: 0x00009340 File Offset: 0x00007540
			public bool TryGetValue(SignalAsset key, out UnityEvent value)
			{
				int index = this.m_Signals.IndexOf(key);
				if (index != -1)
				{
					value = this.m_Events[index];
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x060002AF RID: 687 RVA: 0x00009372 File Offset: 0x00007572
			public void Append(SignalAsset key, UnityEvent value)
			{
				this.m_Signals.Add(key);
				this.m_Events.Add(value);
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x0000938C File Offset: 0x0000758C
			public void Remove(int idx)
			{
				if (idx != -1)
				{
					this.m_Signals.RemoveAt(idx);
					this.m_Events.RemoveAt(idx);
				}
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x000093AC File Offset: 0x000075AC
			public void Remove(SignalAsset key)
			{
				int idx = this.m_Signals.IndexOf(key);
				if (idx != -1)
				{
					this.m_Signals.RemoveAt(idx);
					this.m_Events.RemoveAt(idx);
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x000093E2 File Offset: 0x000075E2
			public List<SignalAsset> signals
			{
				get
				{
					return this.m_Signals;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x000093EA File Offset: 0x000075EA
			public List<UnityEvent> events
			{
				get
				{
					return this.m_Events;
				}
			}

			// Token: 0x04000132 RID: 306
			[SerializeField]
			private List<SignalAsset> m_Signals = new List<SignalAsset>();

			// Token: 0x04000133 RID: 307
			[SerializeField]
			[CustomSignalEventDrawer]
			private List<UnityEvent> m_Events = new List<UnityEvent>();
		}
	}
}
