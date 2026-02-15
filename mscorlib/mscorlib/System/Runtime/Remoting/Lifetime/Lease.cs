using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000437 RID: 1079
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x060023E0 RID: 9184 RVA: 0x00093E0C File Offset: 0x0009200C
		public Lease()
		{
			this._currentState = LeaseState.Initial;
			this._initialLeaseTime = LifetimeServices.LeaseTime;
			this._renewOnCallTime = LifetimeServices.RenewOnCallTime;
			this._sponsorshipTimeout = LifetimeServices.SponsorshipTimeout;
			this._leaseExpireTime = DateTime.UtcNow + this._initialLeaseTime;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x00093E5D File Offset: 0x0009205D
		public TimeSpan CurrentLeaseTime
		{
			get
			{
				return this._leaseExpireTime - DateTime.UtcNow;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00093E6F File Offset: 0x0009206F
		public LeaseState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00093E77 File Offset: 0x00092077
		public void Activate()
		{
			this._currentState = LeaseState.Active;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00093E80 File Offset: 0x00092080
		public TimeSpan RenewOnCallTime
		{
			get
			{
				return this._renewOnCallTime;
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00093E88 File Offset: 0x00092088
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			DateTime dateTime = DateTime.UtcNow + renewalTime;
			if (dateTime > this._leaseExpireTime)
			{
				this._leaseExpireTime = dateTime;
			}
			return this.CurrentLeaseTime;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00093EBC File Offset: 0x000920BC
		public void Unregister(ISponsor obj)
		{
			lock (this)
			{
				if (this._sponsors != null)
				{
					for (int i = 0; i < this._sponsors.Count; i++)
					{
						if (this._sponsors[i] == obj)
						{
							this._sponsors.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00093F30 File Offset: 0x00092130
		internal void UpdateState()
		{
			if (this._currentState != LeaseState.Active)
			{
				return;
			}
			if (this.CurrentLeaseTime > TimeSpan.Zero)
			{
				return;
			}
			if (this._sponsors != null)
			{
				this._currentState = LeaseState.Renewing;
				lock (this)
				{
					this._renewingSponsors = new Queue(this._sponsors);
				}
				this.CheckNextSponsor();
				return;
			}
			this._currentState = LeaseState.Expired;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00093FB0 File Offset: 0x000921B0
		private void CheckNextSponsor()
		{
			if (this._renewingSponsors.Count == 0)
			{
				this._currentState = LeaseState.Expired;
				this._renewingSponsors = null;
				return;
			}
			ISponsor sponsor = (ISponsor)this._renewingSponsors.Peek();
			this._renewalDelegate = new Lease.RenewalDelegate(sponsor.Renewal);
			IAsyncResult asyncResult = this._renewalDelegate.BeginInvoke(this, null, null);
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.ProcessSponsorResponse), asyncResult, this._sponsorshipTimeout, true);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0009402C File Offset: 0x0009222C
		private void ProcessSponsorResponse(object state, bool timedOut)
		{
			if (!timedOut)
			{
				try
				{
					IAsyncResult asyncResult = (IAsyncResult)state;
					TimeSpan timeSpan = this._renewalDelegate.EndInvoke(asyncResult);
					if (timeSpan != TimeSpan.Zero)
					{
						this.Renew(timeSpan);
						this._currentState = LeaseState.Active;
						this._renewingSponsors = null;
						return;
					}
				}
				catch
				{
				}
			}
			this.Unregister((ISponsor)this._renewingSponsors.Dequeue());
			this.CheckNextSponsor();
		}

		// Token: 0x04001151 RID: 4433
		private DateTime _leaseExpireTime;

		// Token: 0x04001152 RID: 4434
		private LeaseState _currentState;

		// Token: 0x04001153 RID: 4435
		private TimeSpan _initialLeaseTime;

		// Token: 0x04001154 RID: 4436
		private TimeSpan _renewOnCallTime;

		// Token: 0x04001155 RID: 4437
		private TimeSpan _sponsorshipTimeout;

		// Token: 0x04001156 RID: 4438
		private ArrayList _sponsors;

		// Token: 0x04001157 RID: 4439
		private Queue _renewingSponsors;

		// Token: 0x04001158 RID: 4440
		private Lease.RenewalDelegate _renewalDelegate;

		// Token: 0x02000438 RID: 1080
		// (Invoke) Token: 0x060023EB RID: 9195
		private delegate TimeSpan RenewalDelegate(ILease lease);
	}
}
