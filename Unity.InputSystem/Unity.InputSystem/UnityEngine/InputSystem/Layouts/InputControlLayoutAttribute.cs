using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000209 RID: 521
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class InputControlLayoutAttribute : Attribute
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00058399 File Offset: 0x00056599
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x000583A1 File Offset: 0x000565A1
		public Type stateType { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000583AA File Offset: 0x000565AA
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x000583B2 File Offset: 0x000565B2
		public string stateFormat { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000583BB File Offset: 0x000565BB
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x000583C3 File Offset: 0x000565C3
		public string[] commonUsages { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x000583CC File Offset: 0x000565CC
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x000583D4 File Offset: 0x000565D4
		public string variants { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x000583DD File Offset: 0x000565DD
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x000583E5 File Offset: 0x000565E5
		public bool isNoisy { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x000583EE File Offset: 0x000565EE
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x000583FB File Offset: 0x000565FB
		public bool canRunInBackground
		{
			get
			{
				return this.canRunInBackgroundInternal.Value;
			}
			set
			{
				this.canRunInBackgroundInternal = new bool?(value);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00058409 File Offset: 0x00056609
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x00058416 File Offset: 0x00056616
		public bool updateBeforeRender
		{
			get
			{
				return this.updateBeforeRenderInternal.Value;
			}
			set
			{
				this.updateBeforeRenderInternal = new bool?(value);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00058424 File Offset: 0x00056624
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0005842C File Offset: 0x0005662C
		public bool isGenericTypeOfDevice { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00058435 File Offset: 0x00056635
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x0005843D File Offset: 0x0005663D
		public string displayName { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00058446 File Offset: 0x00056646
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0005844E File Offset: 0x0005664E
		public string description { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00058457 File Offset: 0x00056657
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x0005845F File Offset: 0x0005665F
		public bool hideInUI { get; set; }

		// Token: 0x04000B8C RID: 2956
		internal bool? canRunInBackgroundInternal;

		// Token: 0x04000B8D RID: 2957
		internal bool? updateBeforeRenderInternal;
	}
}
