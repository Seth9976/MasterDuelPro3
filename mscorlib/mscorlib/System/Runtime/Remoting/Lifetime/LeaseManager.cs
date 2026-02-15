using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000439 RID: 1081
	internal class LeaseManager
	{
		// Token: 0x060023EE RID: 9198 RVA: 0x000940A8 File Offset: 0x000922A8
		public void SetPollTime(TimeSpan timeSpan)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				if (this._timer != null)
				{
					this._timer.Change(timeSpan, timeSpan);
				}
			}
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00094100 File Offset: 0x00092300
		public void TrackLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				identity.Lease.Activate();
				this._objects.Add(identity);
				if (this._timer == null)
				{
					this.StartManager();
				}
			}
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00094168 File Offset: 0x00092368
		public void StopTrackingLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				this._objects.Remove(identity);
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000941B4 File Offset: 0x000923B4
		public void StartManager()
		{
			this._timer = new Timer(new TimerCallback(this.ManageLeases), null, LifetimeServices.LeaseManagerPollTime, LifetimeServices.LeaseManagerPollTime);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000941D8 File Offset: 0x000923D8
		public void StopManager()
		{
			Timer timer = this._timer;
			this._timer = null;
			if (timer != null)
			{
				timer.Dispose();
			}
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000941FC File Offset: 0x000923FC
		public void ManageLeases(object state)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				int i = 0;
				while (i < this._objects.Count)
				{
					ServerIdentity serverIdentity = (ServerIdentity)this._objects[i];
					serverIdentity.Lease.UpdateState();
					if (serverIdentity.Lease.CurrentState == LeaseState.Expired)
					{
						this._objects.RemoveAt(i);
						serverIdentity.OnLifetimeExpired();
					}
					else
					{
						i++;
					}
				}
				if (this._objects.Count == 0)
				{
					this.StopManager();
				}
			}
		}

		// Token: 0x04001159 RID: 4441
		private ArrayList _objects = new ArrayList();

		// Token: 0x0400115A RID: 4442
		private Timer _timer;
	}
}
