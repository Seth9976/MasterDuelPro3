using System;
using System.Linq;
using System.Text;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public struct InputBinding : IEquatable<InputBinding>
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001058A File Offset: 0x0000E78A
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00010592 File Offset: 0x0000E792
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001059C File Offset: 0x0000E79C
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000105CB File Offset: 0x0000E7CB
		public Guid id
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_Id))
				{
					return default(Guid);
				}
				return new Guid(this.m_Id);
			}
			set
			{
				this.m_Id = value.ToString();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000105E0 File Offset: 0x0000E7E0
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x000105E8 File Offset: 0x0000E7E8
		public string path
		{
			get
			{
				return this.m_Path;
			}
			set
			{
				this.m_Path = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000105F1 File Offset: 0x0000E7F1
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x000105F9 File Offset: 0x0000E7F9
		public string overridePath
		{
			get
			{
				return this.m_OverridePath;
			}
			set
			{
				this.m_OverridePath = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00010602 File Offset: 0x0000E802
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0001060A File Offset: 0x0000E80A
		public string interactions
		{
			get
			{
				return this.m_Interactions;
			}
			set
			{
				this.m_Interactions = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00010613 File Offset: 0x0000E813
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0001061B File Offset: 0x0000E81B
		public string overrideInteractions
		{
			get
			{
				return this.m_OverrideInteractions;
			}
			set
			{
				this.m_OverrideInteractions = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00010624 File Offset: 0x0000E824
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0001062C File Offset: 0x0000E82C
		public string processors
		{
			get
			{
				return this.m_Processors;
			}
			set
			{
				this.m_Processors = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00010635 File Offset: 0x0000E835
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0001063D File Offset: 0x0000E83D
		public string overrideProcessors
		{
			get
			{
				return this.m_OverrideProcessors;
			}
			set
			{
				this.m_OverrideProcessors = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00010646 File Offset: 0x0000E846
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0001064E File Offset: 0x0000E84E
		public string groups
		{
			get
			{
				return this.m_Groups;
			}
			set
			{
				this.m_Groups = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00010657 File Offset: 0x0000E857
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001065F File Offset: 0x0000E85F
		public string action
		{
			get
			{
				return this.m_Action;
			}
			set
			{
				this.m_Action = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00010668 File Offset: 0x0000E868
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00010675 File Offset: 0x0000E875
		public bool isComposite
		{
			get
			{
				return (this.m_Flags & InputBinding.Flags.Composite) == InputBinding.Flags.Composite;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputBinding.Flags.Composite;
					return;
				}
				this.m_Flags &= ~InputBinding.Flags.Composite;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00010698 File Offset: 0x0000E898
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x000106A5 File Offset: 0x0000E8A5
		public bool isPartOfComposite
		{
			get
			{
				return (this.m_Flags & InputBinding.Flags.PartOfComposite) == InputBinding.Flags.PartOfComposite;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputBinding.Flags.PartOfComposite;
					return;
				}
				this.m_Flags &= ~InputBinding.Flags.PartOfComposite;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000106C8 File Offset: 0x0000E8C8
		public bool hasOverrides
		{
			get
			{
				return this.overridePath != null || this.overrideProcessors != null || this.overrideInteractions != null;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public InputBinding(string path, string action = null, string groups = null, string processors = null, string interactions = null, string name = null)
		{
			this.m_Path = path;
			this.m_Action = action;
			this.m_Groups = groups;
			this.m_Processors = processors;
			this.m_Interactions = interactions;
			this.m_Name = name;
			this.m_Id = null;
			this.m_Flags = InputBinding.Flags.None;
			this.m_OverridePath = null;
			this.m_OverrideInteractions = null;
			this.m_OverrideProcessors = null;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010748 File Offset: 0x0000E948
		public string GetNameOfComposite()
		{
			if (!this.isComposite)
			{
				return null;
			}
			return NameAndParameters.Parse(this.effectivePath).name;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010774 File Offset: 0x0000E974
		internal void GenerateId()
		{
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001079A File Offset: 0x0000E99A
		internal void RemoveOverrides()
		{
			this.m_OverridePath = null;
			this.m_OverrideInteractions = null;
			this.m_OverrideProcessors = null;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public static InputBinding MaskByGroup(string group)
		{
			return new InputBinding
			{
				groups = group
			};
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public static InputBinding MaskByGroups(params string[] groups)
		{
			InputBinding inputBinding = default(InputBinding);
			inputBinding.groups = string.Join(";", groups.Where((string x) => !string.IsNullOrEmpty(x)));
			return inputBinding;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00010820 File Offset: 0x0000EA20
		public string effectivePath
		{
			get
			{
				return this.overridePath ?? this.path;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00010832 File Offset: 0x0000EA32
		public string effectiveInteractions
		{
			get
			{
				return this.overrideInteractions ?? this.interactions;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00010844 File Offset: 0x0000EA44
		public string effectiveProcessors
		{
			get
			{
				return this.overrideProcessors ?? this.processors;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00010856 File Offset: 0x0000EA56
		internal bool isEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.effectivePath) && string.IsNullOrEmpty(this.action) && string.IsNullOrEmpty(this.groups);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010880 File Offset: 0x0000EA80
		public bool Equals(InputBinding other)
		{
			return string.Equals(this.effectivePath, other.effectivePath, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.effectiveInteractions, other.effectiveInteractions, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.effectiveProcessors, other.effectiveProcessors, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.groups, other.groups, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.action, other.action, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputBinding)
			{
				InputBinding binding = (InputBinding)obj;
				return this.Equals(binding);
			}
			return false;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010922 File Offset: 0x0000EB22
		public static bool operator ==(InputBinding left, InputBinding right)
		{
			return left.Equals(right);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static bool operator !=(InputBinding left, InputBinding right)
		{
			return !(left == right);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010938 File Offset: 0x0000EB38
		public override int GetHashCode()
		{
			return (((((((((this.effectivePath != null) ? this.effectivePath.GetHashCode() : 0) * 397) ^ ((this.effectiveInteractions != null) ? this.effectiveInteractions.GetHashCode() : 0)) * 397) ^ ((this.effectiveProcessors != null) ? this.effectiveProcessors.GetHashCode() : 0)) * 397) ^ ((this.groups != null) ? this.groups.GetHashCode() : 0)) * 397) ^ ((this.action != null) ? this.action.GetHashCode() : 0);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000109D0 File Offset: 0x0000EBD0
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.action))
			{
				builder.Append(this.action);
				builder.Append(':');
			}
			string path = this.effectivePath;
			if (!string.IsNullOrEmpty(path))
			{
				builder.Append(path);
			}
			if (!string.IsNullOrEmpty(this.groups))
			{
				builder.Append('[');
				builder.Append(this.groups);
				builder.Append(']');
			}
			return builder.ToString();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010A50 File Offset: 0x0000EC50
		public string ToDisplayString(InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, InputControl control = null)
		{
			string text;
			string text2;
			return this.ToDisplayString(out text, out text2, options, control);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010A6C File Offset: 0x0000EC6C
		public string ToDisplayString(out string deviceLayoutName, out string controlPath, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, InputControl control = null)
		{
			if (this.isComposite)
			{
				deviceLayoutName = null;
				controlPath = null;
				return string.Empty;
			}
			InputControlPath.HumanReadableStringOptions readableStringOptions = InputControlPath.HumanReadableStringOptions.None;
			if ((options & InputBinding.DisplayStringOptions.DontOmitDevice) == (InputBinding.DisplayStringOptions)0)
			{
				readableStringOptions |= InputControlPath.HumanReadableStringOptions.OmitDevice;
			}
			if ((options & InputBinding.DisplayStringOptions.DontUseShortDisplayNames) == (InputBinding.DisplayStringOptions)0)
			{
				readableStringOptions |= InputControlPath.HumanReadableStringOptions.UseShortNames;
			}
			string result = InputControlPath.ToHumanReadableString(((options & InputBinding.DisplayStringOptions.IgnoreBindingOverrides) != (InputBinding.DisplayStringOptions)0) ? this.path : this.effectivePath, out deviceLayoutName, out controlPath, readableStringOptions, control);
			if (!string.IsNullOrEmpty(this.effectiveInteractions) && (options & InputBinding.DisplayStringOptions.DontIncludeInteractions) == (InputBinding.DisplayStringOptions)0)
			{
				string interactionString = string.Empty;
				foreach (NameAndParameters element in NameAndParameters.ParseMultiple(this.effectiveInteractions))
				{
					string interactionDisplayName = InputInteraction.GetDisplayName(element.name);
					if (!string.IsNullOrEmpty(interactionDisplayName))
					{
						if (!string.IsNullOrEmpty(interactionString))
						{
							interactionString = interactionString + " or " + interactionDisplayName;
						}
						else
						{
							interactionString = interactionDisplayName;
						}
					}
				}
				if (!string.IsNullOrEmpty(interactionString))
				{
					result = interactionString + " " + result;
				}
			}
			return result;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010B64 File Offset: 0x0000ED64
		internal bool TriggersAction(InputAction action)
		{
			return string.Compare(action.name, this.action, StringComparison.InvariantCultureIgnoreCase) == 0 || this.action == action.m_Id;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010B8D File Offset: 0x0000ED8D
		public bool Matches(InputBinding binding)
		{
			return this.Matches(ref binding, (InputBinding.MatchOptions)0);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010B98 File Offset: 0x0000ED98
		internal bool Matches(ref InputBinding binding, InputBinding.MatchOptions options = (InputBinding.MatchOptions)0)
		{
			if (this.name != null && (binding.name == null || !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.name, binding.name, ';')))
			{
				return false;
			}
			if (this.path != null && (binding.path == null || !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.path, binding.path, ';')))
			{
				return false;
			}
			if (this.action != null && (binding.action == null || !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.action, binding.action, ';')))
			{
				return false;
			}
			if (this.groups != null)
			{
				bool haveGroupsOnBinding = !string.IsNullOrEmpty(binding.groups);
				if (!haveGroupsOnBinding && (options & InputBinding.MatchOptions.EmptyGroupMatchesAny) == (InputBinding.MatchOptions)0)
				{
					return false;
				}
				if (haveGroupsOnBinding && !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.groups, binding.groups, ';'))
				{
					return false;
				}
			}
			return string.IsNullOrEmpty(this.m_Id) || !(binding.id != this.id);
		}

		// Token: 0x040001E7 RID: 487
		public const char Separator = ';';

		// Token: 0x040001E8 RID: 488
		internal const string kSeparatorString = ";";

		// Token: 0x040001E9 RID: 489
		[SerializeField]
		private string m_Name;

		// Token: 0x040001EA RID: 490
		[SerializeField]
		internal string m_Id;

		// Token: 0x040001EB RID: 491
		[Tooltip("Path of the control to bind to. Matched at runtime to controls from InputDevices present at the time.\n\nCan either be graphically from the control picker dropdown UI or edited manually in text mode by clicking the 'T' button. Internally, both methods result in control path strings that look like, for example, \"<Gamepad>/buttonSouth\".")]
		[SerializeField]
		private string m_Path;

		// Token: 0x040001EC RID: 492
		[SerializeField]
		private string m_Interactions;

		// Token: 0x040001ED RID: 493
		[SerializeField]
		private string m_Processors;

		// Token: 0x040001EE RID: 494
		[SerializeField]
		internal string m_Groups;

		// Token: 0x040001EF RID: 495
		[SerializeField]
		private string m_Action;

		// Token: 0x040001F0 RID: 496
		[SerializeField]
		internal InputBinding.Flags m_Flags;

		// Token: 0x040001F1 RID: 497
		[NonSerialized]
		private string m_OverridePath;

		// Token: 0x040001F2 RID: 498
		[NonSerialized]
		private string m_OverrideInteractions;

		// Token: 0x040001F3 RID: 499
		[NonSerialized]
		private string m_OverrideProcessors;

		// Token: 0x02000051 RID: 81
		[Flags]
		public enum DisplayStringOptions
		{
			// Token: 0x040001F5 RID: 501
			DontUseShortDisplayNames = 1,
			// Token: 0x040001F6 RID: 502
			DontOmitDevice = 2,
			// Token: 0x040001F7 RID: 503
			DontIncludeInteractions = 4,
			// Token: 0x040001F8 RID: 504
			IgnoreBindingOverrides = 8
		}

		// Token: 0x02000052 RID: 82
		[Flags]
		internal enum MatchOptions
		{
			// Token: 0x040001FA RID: 506
			EmptyGroupMatchesAny = 1
		}

		// Token: 0x02000053 RID: 83
		[Flags]
		internal enum Flags
		{
			// Token: 0x040001FC RID: 508
			None = 0,
			// Token: 0x040001FD RID: 509
			Composite = 4,
			// Token: 0x040001FE RID: 510
			PartOfComposite = 8
		}
	}
}
