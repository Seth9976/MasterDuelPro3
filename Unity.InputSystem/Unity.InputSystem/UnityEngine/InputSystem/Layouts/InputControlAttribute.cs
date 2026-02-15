using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x020001F3 RID: 499
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public sealed class InputControlAttribute : PropertyAttribute
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0005513B File Offset: 0x0005333B
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x00055143 File Offset: 0x00053343
		public string layout { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0005514C File Offset: 0x0005334C
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00055154 File Offset: 0x00053354
		public string variants { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0005515D File Offset: 0x0005335D
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00055165 File Offset: 0x00053365
		public string name { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0005516E File Offset: 0x0005336E
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00055176 File Offset: 0x00053376
		public string format { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0005517F File Offset: 0x0005337F
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00055187 File Offset: 0x00053387
		public string usage { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00055190 File Offset: 0x00053390
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00055198 File Offset: 0x00053398
		public string[] usages { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x000551A1 File Offset: 0x000533A1
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x000551A9 File Offset: 0x000533A9
		public string parameters { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x000551B2 File Offset: 0x000533B2
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x000551BA File Offset: 0x000533BA
		public string processors { get; set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x000551C3 File Offset: 0x000533C3
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x000551CB File Offset: 0x000533CB
		public string alias { get; set; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x000551D4 File Offset: 0x000533D4
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x000551DC File Offset: 0x000533DC
		public string[] aliases { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000551E5 File Offset: 0x000533E5
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x000551ED File Offset: 0x000533ED
		public string useStateFrom { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x000551F6 File Offset: 0x000533F6
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x000551FE File Offset: 0x000533FE
		public uint bit { get; set; } = uint.MaxValue;

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00055207 File Offset: 0x00053407
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x0005520F File Offset: 0x0005340F
		public uint offset { get; set; } = uint.MaxValue;

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00055218 File Offset: 0x00053418
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x00055220 File Offset: 0x00053420
		public uint sizeInBits { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00055229 File Offset: 0x00053429
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x00055231 File Offset: 0x00053431
		public int arraySize { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0005523A File Offset: 0x0005343A
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x00055242 File Offset: 0x00053442
		public string displayName { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0005524B File Offset: 0x0005344B
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x00055253 File Offset: 0x00053453
		public string shortDisplayName { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0005525C File Offset: 0x0005345C
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x00055264 File Offset: 0x00053464
		public bool noisy { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0005526D File Offset: 0x0005346D
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x00055275 File Offset: 0x00053475
		public bool synthetic { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0005527E File Offset: 0x0005347E
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x00055286 File Offset: 0x00053486
		public bool dontReset { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0005528F File Offset: 0x0005348F
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00055297 File Offset: 0x00053497
		public object defaultState { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x000552A0 File Offset: 0x000534A0
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x000552A8 File Offset: 0x000534A8
		public object minValue { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x000552B1 File Offset: 0x000534B1
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x000552B9 File Offset: 0x000534B9
		public object maxValue { get; set; }
	}
}
