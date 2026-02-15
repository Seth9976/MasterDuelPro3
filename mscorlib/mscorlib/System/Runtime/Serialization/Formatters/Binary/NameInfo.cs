using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200050C RID: 1292
	internal sealed class NameInfo
	{
		// Token: 0x060028CD RID: 10445 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal NameInfo()
		{
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000A7600 File Offset: 0x000A5800
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x000A766A File Offset: 0x000A586A
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x000A7692 File Offset: 0x000A5892
		// (set) Token: 0x060028D1 RID: 10449 RVA: 0x000A76B3 File Offset: 0x000A58B3
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x040014A9 RID: 5289
		internal string NIFullName;

		// Token: 0x040014AA RID: 5290
		internal long NIobjectId;

		// Token: 0x040014AB RID: 5291
		internal long NIassemId;

		// Token: 0x040014AC RID: 5292
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x040014AD RID: 5293
		internal Type NItype;

		// Token: 0x040014AE RID: 5294
		internal bool NIisSealed;

		// Token: 0x040014AF RID: 5295
		internal bool NIisArray;

		// Token: 0x040014B0 RID: 5296
		internal bool NIisArrayItem;

		// Token: 0x040014B1 RID: 5297
		internal bool NItransmitTypeOnObject;

		// Token: 0x040014B2 RID: 5298
		internal bool NItransmitTypeOnMember;

		// Token: 0x040014B3 RID: 5299
		internal bool NIisParentTypeOnObject;

		// Token: 0x040014B4 RID: 5300
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x040014B5 RID: 5301
		private bool NIsealedStatusChecked;
	}
}
