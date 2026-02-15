using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public struct InputControlScheme : IEquatable<InputControlScheme>
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000122B5 File Offset: 0x000104B5
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000122BD File Offset: 0x000104BD
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x000122C5 File Offset: 0x000104C5
		public string bindingGroup
		{
			get
			{
				return this.m_BindingGroup;
			}
			set
			{
				this.m_BindingGroup = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000122CE File Offset: 0x000104CE
		public ReadOnlyArray<InputControlScheme.DeviceRequirement> deviceRequirements
		{
			get
			{
				return new ReadOnlyArray<InputControlScheme.DeviceRequirement>(this.m_DeviceRequirements);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000122DC File Offset: 0x000104DC
		public InputControlScheme(string name, IEnumerable<InputControlScheme.DeviceRequirement> devices = null, string bindingGroup = null)
		{
			this = default(InputControlScheme);
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.SetNameAndBindingGroup(name, bindingGroup);
			this.m_DeviceRequirements = null;
			if (devices != null)
			{
				this.m_DeviceRequirements = devices.ToArray<InputControlScheme.DeviceRequirement>();
				if (this.m_DeviceRequirements.Length == 0)
				{
					this.m_DeviceRequirements = null;
				}
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00012331 File Offset: 0x00010531
		internal void SetNameAndBindingGroup(string name, string bindingGroup = null)
		{
			this.m_Name = name;
			if (!string.IsNullOrEmpty(bindingGroup))
			{
				this.m_BindingGroup = bindingGroup;
				return;
			}
			this.m_BindingGroup = (name.Contains(';') ? name.Replace(";", "") : name);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00012370 File Offset: 0x00010570
		public static InputControlScheme? FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, InputDevice mustIncludeDevice = null, bool allowUnsuccesfulMatch = false) where TDevices : IReadOnlyList<InputDevice> where TSchemes : IEnumerable<InputControlScheme>
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			InputControlScheme controlScheme;
			InputControlScheme.MatchResult matchResult;
			if (!InputControlScheme.FindControlSchemeForDevices<TDevices, TSchemes>(devices, schemes, out controlScheme, out matchResult, mustIncludeDevice, allowUnsuccesfulMatch))
			{
				return null;
			}
			matchResult.Dispose();
			return new InputControlScheme?(controlScheme);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000123CC File Offset: 0x000105CC
		public static bool FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, out InputControlScheme controlScheme, out InputControlScheme.MatchResult matchResult, InputDevice mustIncludeDevice = null, bool allowUnsuccessfulMatch = false) where TDevices : IReadOnlyList<InputDevice> where TSchemes : IEnumerable<InputControlScheme>
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			InputControlScheme.MatchResult? bestResult = null;
			InputControlScheme? bestScheme = null;
			foreach (InputControlScheme scheme in schemes)
			{
				InputControlScheme.MatchResult result = scheme.PickDevicesFrom<TDevices>(devices, mustIncludeDevice);
				if (!result.isSuccessfulMatch && (!allowUnsuccessfulMatch || result.score <= 0f))
				{
					result.Dispose();
				}
				else if (mustIncludeDevice != null && !result.devices.Contains(mustIncludeDevice))
				{
					result.Dispose();
				}
				else if (bestResult != null && bestResult.Value.score >= result.score)
				{
					result.Dispose();
				}
				else
				{
					if (bestResult != null)
					{
						bestResult.GetValueOrDefault().Dispose();
					}
					bestResult = new InputControlScheme.MatchResult?(result);
					bestScheme = new InputControlScheme?(scheme);
				}
			}
			matchResult = bestResult.GetValueOrDefault();
			controlScheme = bestScheme.GetValueOrDefault();
			return bestResult != null;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00012518 File Offset: 0x00010718
		public static InputControlScheme? FindControlSchemeForDevice<TSchemes>(InputDevice device, TSchemes schemes) where TSchemes : IEnumerable<InputControlScheme>
		{
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			return InputControlScheme.FindControlSchemeForDevices<OneOrMore<InputDevice, ReadOnlyArray<InputDevice>>, TSchemes>(new OneOrMore<InputDevice, ReadOnlyArray<InputDevice>>(device), schemes, null, false);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001254C File Offset: 0x0001074C
		public bool SupportsDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			for (int i = 0; i < this.m_DeviceRequirements.Length; i++)
			{
				if (InputControlPath.TryFindControl(device, this.m_DeviceRequirements[i].controlPath, 0) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00012598 File Offset: 0x00010798
		public InputControlScheme.MatchResult PickDevicesFrom<TDevices>(TDevices devices, InputDevice favorDevice = null) where TDevices : IReadOnlyList<InputDevice>
		{
			InputControlScheme.MatchResult matchResult;
			if (this.m_DeviceRequirements == null || this.m_DeviceRequirements.Length == 0)
			{
				matchResult = new InputControlScheme.MatchResult
				{
					m_Result = InputControlScheme.MatchResult.Result.AllSatisfied,
					m_Score = 0.5f
				};
				return matchResult;
			}
			bool haveAllRequired = true;
			bool haveAllOptional = true;
			int requirementCount = this.m_DeviceRequirements.Length;
			float score = 0f;
			InputControlList<InputControl> controls = new InputControlList<InputControl>(Allocator.Persistent, requirementCount);
			try
			{
				bool orChainIsSatisfied = false;
				bool orChainHasRequiredDevices = false;
				for (int i = 0; i < requirementCount; i++)
				{
					bool isOR = this.m_DeviceRequirements[i].isOR;
					bool isOptional = this.m_DeviceRequirements[i].isOptional;
					if (isOR && orChainIsSatisfied)
					{
						controls.Add(null);
					}
					else
					{
						string path = this.m_DeviceRequirements[i].controlPath;
						if (string.IsNullOrEmpty(path))
						{
							score += 1f;
							controls.Add(null);
						}
						else
						{
							InputControl match = null;
							int j = 0;
							while (j < devices.Count)
							{
								InputDevice device = devices[j];
								if (favorDevice != null)
								{
									if (j == 0)
									{
										device = favorDevice;
									}
									else if (device == favorDevice)
									{
										device = devices[0];
									}
								}
								InputControl matchedControl = InputControlPath.TryFindControl(device, path, 0);
								if (matchedControl != null && !controls.Contains(matchedControl))
								{
									match = matchedControl;
									InternedString deviceLayoutOfControlPath = new InternedString(InputControlPath.TryGetDeviceLayout(path));
									if (deviceLayoutOfControlPath.IsEmpty())
									{
										score += 1f;
										break;
									}
									InternedString deviceLayoutOfControl = matchedControl.device.m_Layout;
									int distance;
									if (InputControlLayout.s_Layouts.ComputeDistanceInInheritanceHierarchy(deviceLayoutOfControlPath, deviceLayoutOfControl, out distance))
									{
										score += 1f + 1f / (float)(Math.Abs(distance) + 1);
										break;
									}
									score += 1f;
									break;
								}
								else
								{
									j++;
								}
							}
							if (i + 1 < requirementCount && this.m_DeviceRequirements[i + 1].isOR)
							{
								if (match != null)
								{
									orChainIsSatisfied = true;
								}
								else if (!isOptional)
								{
									orChainHasRequiredDevices = true;
								}
							}
							else if (isOR && i == requirementCount - 1)
							{
								if (match == null)
								{
									if (orChainHasRequiredDevices)
									{
										haveAllRequired = false;
									}
									else
									{
										haveAllOptional = false;
									}
								}
							}
							else
							{
								if (match == null)
								{
									if (isOptional)
									{
										haveAllOptional = false;
									}
									else
									{
										haveAllRequired = false;
									}
								}
								if (i > 0 && this.m_DeviceRequirements[i - 1].isOR)
								{
									if (!orChainIsSatisfied)
									{
										if (orChainHasRequiredDevices)
										{
											haveAllRequired = false;
										}
										else
										{
											haveAllOptional = false;
										}
									}
									orChainIsSatisfied = false;
								}
							}
							controls.Add(match);
						}
					}
				}
			}
			catch (Exception)
			{
				controls.Dispose();
				throw;
			}
			matchResult = new InputControlScheme.MatchResult
			{
				m_Result = ((!haveAllRequired) ? InputControlScheme.MatchResult.Result.MissingRequired : ((!haveAllOptional) ? InputControlScheme.MatchResult.Result.MissingOptional : InputControlScheme.MatchResult.Result.AllSatisfied)),
				m_Controls = controls,
				m_Requirements = this.m_DeviceRequirements,
				m_Score = score
			};
			return matchResult;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001284C File Offset: 0x00010A4C
		public bool Equals(InputControlScheme other)
		{
			if (!string.Equals(this.m_Name, other.m_Name, StringComparison.InvariantCultureIgnoreCase) || !string.Equals(this.m_BindingGroup, other.m_BindingGroup, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (this.m_DeviceRequirements == null || this.m_DeviceRequirements.Length == 0)
			{
				return other.m_DeviceRequirements == null || other.m_DeviceRequirements.Length == 0;
			}
			if (other.m_DeviceRequirements == null || this.m_DeviceRequirements.Length != other.m_DeviceRequirements.Length)
			{
				return false;
			}
			int deviceCount = this.m_DeviceRequirements.Length;
			for (int i = 0; i < deviceCount; i++)
			{
				InputControlScheme.DeviceRequirement device = this.m_DeviceRequirements[i];
				bool haveMatch = false;
				for (int j = 0; j < deviceCount; j++)
				{
					if (other.m_DeviceRequirements[j] == device)
					{
						haveMatch = true;
						break;
					}
				}
				if (!haveMatch)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012918 File Offset: 0x00010B18
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputControlScheme && this.Equals((InputControlScheme)obj);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00012938 File Offset: 0x00010B38
		public override int GetHashCode()
		{
			return (((((this.m_Name != null) ? this.m_Name.GetHashCode() : 0) * 397) ^ ((this.m_BindingGroup != null) ? this.m_BindingGroup.GetHashCode() : 0)) * 397) ^ ((this.m_DeviceRequirements != null) ? this.m_DeviceRequirements.GetHashCode() : 0);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00012998 File Offset: 0x00010B98
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.m_Name))
			{
				return base.ToString();
			}
			if (this.m_DeviceRequirements == null)
			{
				return this.m_Name;
			}
			StringBuilder builder = new StringBuilder();
			builder.Append(this.m_Name);
			builder.Append('(');
			bool isFirst = true;
			foreach (InputControlScheme.DeviceRequirement device in this.m_DeviceRequirements)
			{
				if (!isFirst)
				{
					builder.Append(',');
				}
				builder.Append(device.controlPath);
				isFirst = false;
			}
			builder.Append(')');
			return builder.ToString();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00012A39 File Offset: 0x00010C39
		public static bool operator ==(InputControlScheme left, InputControlScheme right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00012A43 File Offset: 0x00010C43
		public static bool operator !=(InputControlScheme left, InputControlScheme right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000223 RID: 547
		[SerializeField]
		internal string m_Name;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		internal string m_BindingGroup;

		// Token: 0x04000225 RID: 549
		[SerializeField]
		internal InputControlScheme.DeviceRequirement[] m_DeviceRequirements;

		// Token: 0x0200005E RID: 94
		public struct MatchResult : IEnumerable<InputControlScheme.MatchResult.Match>, IEnumerable, IDisposable
		{
			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x00012A50 File Offset: 0x00010C50
			public float score
			{
				get
				{
					return this.m_Score;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x00012A58 File Offset: 0x00010C58
			public bool isSuccessfulMatch
			{
				get
				{
					return this.m_Result != InputControlScheme.MatchResult.Result.MissingRequired;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600044E RID: 1102 RVA: 0x00012A66 File Offset: 0x00010C66
			public bool hasMissingRequiredDevices
			{
				get
				{
					return this.m_Result == InputControlScheme.MatchResult.Result.MissingRequired;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x00012A71 File Offset: 0x00010C71
			public bool hasMissingOptionalDevices
			{
				get
				{
					return this.m_Result == InputControlScheme.MatchResult.Result.MissingOptional;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x00012A7C File Offset: 0x00010C7C
			public InputControlList<InputDevice> devices
			{
				get
				{
					if (this.m_Devices.Count == 0 && !this.hasMissingRequiredDevices)
					{
						int controlCount = this.m_Controls.Count;
						if (controlCount != 0)
						{
							this.m_Devices.Capacity = controlCount;
							for (int i = 0; i < controlCount; i++)
							{
								InputControl control = this.m_Controls[i];
								if (control != null)
								{
									InputDevice device = control.device;
									if (!this.m_Devices.Contains(device))
									{
										this.m_Devices.Add(device);
									}
								}
							}
						}
					}
					return this.m_Devices;
				}
			}

			// Token: 0x1700014D RID: 333
			public InputControlScheme.MatchResult.Match this[int index]
			{
				get
				{
					if (index < 0 || this.m_Requirements == null || index >= this.m_Requirements.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return new InputControlScheme.MatchResult.Match
					{
						m_RequirementIndex = index,
						m_Requirements = this.m_Requirements,
						m_Controls = this.m_Controls
					};
				}
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x00012B58 File Offset: 0x00010D58
			public IEnumerator<InputControlScheme.MatchResult.Match> GetEnumerator()
			{
				return new InputControlScheme.MatchResult.Enumerator
				{
					m_Index = -1,
					m_Requirements = this.m_Requirements,
					m_Controls = this.m_Controls
				};
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x00012B95 File Offset: 0x00010D95
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x00012B9D File Offset: 0x00010D9D
			public void Dispose()
			{
				this.m_Controls.Dispose();
				this.m_Devices.Dispose();
			}

			// Token: 0x04000226 RID: 550
			internal InputControlScheme.MatchResult.Result m_Result;

			// Token: 0x04000227 RID: 551
			internal float m_Score;

			// Token: 0x04000228 RID: 552
			internal InputControlList<InputDevice> m_Devices;

			// Token: 0x04000229 RID: 553
			internal InputControlList<InputControl> m_Controls;

			// Token: 0x0400022A RID: 554
			internal InputControlScheme.DeviceRequirement[] m_Requirements;

			// Token: 0x0200005F RID: 95
			internal enum Result
			{
				// Token: 0x0400022C RID: 556
				AllSatisfied,
				// Token: 0x0400022D RID: 557
				MissingRequired,
				// Token: 0x0400022E RID: 558
				MissingOptional
			}

			// Token: 0x02000060 RID: 96
			public struct Match
			{
				// Token: 0x1700014E RID: 334
				// (get) Token: 0x06000455 RID: 1109 RVA: 0x00012BB5 File Offset: 0x00010DB5
				public InputControl control
				{
					get
					{
						return this.m_Controls[this.m_RequirementIndex];
					}
				}

				// Token: 0x1700014F RID: 335
				// (get) Token: 0x06000456 RID: 1110 RVA: 0x00012BC8 File Offset: 0x00010DC8
				public InputDevice device
				{
					get
					{
						InputControl control = this.control;
						if (control == null)
						{
							return null;
						}
						return control.device;
					}
				}

				// Token: 0x17000150 RID: 336
				// (get) Token: 0x06000457 RID: 1111 RVA: 0x00012BDB File Offset: 0x00010DDB
				public int requirementIndex
				{
					get
					{
						return this.m_RequirementIndex;
					}
				}

				// Token: 0x17000151 RID: 337
				// (get) Token: 0x06000458 RID: 1112 RVA: 0x00012BE3 File Offset: 0x00010DE3
				public InputControlScheme.DeviceRequirement requirement
				{
					get
					{
						return this.m_Requirements[this.m_RequirementIndex];
					}
				}

				// Token: 0x17000152 RID: 338
				// (get) Token: 0x06000459 RID: 1113 RVA: 0x00012BF8 File Offset: 0x00010DF8
				public bool isOptional
				{
					get
					{
						return this.requirement.isOptional;
					}
				}

				// Token: 0x0400022F RID: 559
				internal int m_RequirementIndex;

				// Token: 0x04000230 RID: 560
				internal InputControlScheme.DeviceRequirement[] m_Requirements;

				// Token: 0x04000231 RID: 561
				internal InputControlList<InputControl> m_Controls;
			}

			// Token: 0x02000061 RID: 97
			private struct Enumerator : IEnumerator<InputControlScheme.MatchResult.Match>, IEnumerator, IDisposable
			{
				// Token: 0x0600045A RID: 1114 RVA: 0x00012C13 File Offset: 0x00010E13
				public bool MoveNext()
				{
					this.m_Index++;
					return this.m_Requirements != null && this.m_Index < this.m_Requirements.Length;
				}

				// Token: 0x0600045B RID: 1115 RVA: 0x00012C3D File Offset: 0x00010E3D
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x17000153 RID: 339
				// (get) Token: 0x0600045C RID: 1116 RVA: 0x00012C48 File Offset: 0x00010E48
				public InputControlScheme.MatchResult.Match Current
				{
					get
					{
						if (this.m_Requirements == null || this.m_Index < 0 || this.m_Index >= this.m_Requirements.Length)
						{
							throw new InvalidOperationException("Enumerator is not valid");
						}
						return new InputControlScheme.MatchResult.Match
						{
							m_RequirementIndex = this.m_Index,
							m_Requirements = this.m_Requirements,
							m_Controls = this.m_Controls
						};
					}
				}

				// Token: 0x17000154 RID: 340
				// (get) Token: 0x0600045D RID: 1117 RVA: 0x00012CB1 File Offset: 0x00010EB1
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600045E RID: 1118 RVA: 0x000049FE File Offset: 0x00002BFE
				public void Dispose()
				{
				}

				// Token: 0x04000232 RID: 562
				internal int m_Index;

				// Token: 0x04000233 RID: 563
				internal InputControlScheme.DeviceRequirement[] m_Requirements;

				// Token: 0x04000234 RID: 564
				internal InputControlList<InputControl> m_Controls;
			}
		}

		// Token: 0x02000062 RID: 98
		[Serializable]
		public struct DeviceRequirement : IEquatable<InputControlScheme.DeviceRequirement>
		{
			// Token: 0x17000155 RID: 341
			// (get) Token: 0x0600045F RID: 1119 RVA: 0x00012CBE File Offset: 0x00010EBE
			// (set) Token: 0x06000460 RID: 1120 RVA: 0x00012CC6 File Offset: 0x00010EC6
			public string controlPath
			{
				get
				{
					return this.m_ControlPath;
				}
				set
				{
					this.m_ControlPath = value;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000461 RID: 1121 RVA: 0x00012CCF File Offset: 0x00010ECF
			// (set) Token: 0x06000462 RID: 1122 RVA: 0x00012CDC File Offset: 0x00010EDC
			public bool isOptional
			{
				get
				{
					return (this.m_Flags & InputControlScheme.DeviceRequirement.Flags.Optional) > InputControlScheme.DeviceRequirement.Flags.None;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= InputControlScheme.DeviceRequirement.Flags.Optional;
						return;
					}
					this.m_Flags &= ~InputControlScheme.DeviceRequirement.Flags.Optional;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x00012CFF File Offset: 0x00010EFF
			// (set) Token: 0x06000464 RID: 1124 RVA: 0x00012D0A File Offset: 0x00010F0A
			public bool isAND
			{
				get
				{
					return !this.isOR;
				}
				set
				{
					this.isOR = !value;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000465 RID: 1125 RVA: 0x00012D16 File Offset: 0x00010F16
			// (set) Token: 0x06000466 RID: 1126 RVA: 0x00012D23 File Offset: 0x00010F23
			public bool isOR
			{
				get
				{
					return (this.m_Flags & InputControlScheme.DeviceRequirement.Flags.Or) > InputControlScheme.DeviceRequirement.Flags.None;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= InputControlScheme.DeviceRequirement.Flags.Or;
						return;
					}
					this.m_Flags &= ~InputControlScheme.DeviceRequirement.Flags.Or;
				}
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00012D48 File Offset: 0x00010F48
			public override string ToString()
			{
				if (string.IsNullOrEmpty(this.controlPath))
				{
					return base.ToString();
				}
				if (this.isOptional)
				{
					return this.controlPath + " (Optional)";
				}
				return this.controlPath + " (Required)";
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x00012D9C File Offset: 0x00010F9C
			public bool Equals(InputControlScheme.DeviceRequirement other)
			{
				return string.Equals(this.m_ControlPath, other.m_ControlPath) && this.m_Flags == other.m_Flags && string.Equals(this.controlPath, other.controlPath) && this.isOptional == other.isOptional;
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x00012DEF File Offset: 0x00010FEF
			public override bool Equals(object obj)
			{
				return obj != null && obj is InputControlScheme.DeviceRequirement && this.Equals((InputControlScheme.DeviceRequirement)obj);
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00012E0C File Offset: 0x0001100C
			public override int GetHashCode()
			{
				return (((((((this.m_ControlPath != null) ? this.m_ControlPath.GetHashCode() : 0) * 397) ^ this.m_Flags.GetHashCode()) * 397) ^ ((this.controlPath != null) ? this.controlPath.GetHashCode() : 0)) * 397) ^ this.isOptional.GetHashCode();
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00012E79 File Offset: 0x00011079
			public static bool operator ==(InputControlScheme.DeviceRequirement left, InputControlScheme.DeviceRequirement right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x00012E83 File Offset: 0x00011083
			public static bool operator !=(InputControlScheme.DeviceRequirement left, InputControlScheme.DeviceRequirement right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04000235 RID: 565
			[SerializeField]
			internal string m_ControlPath;

			// Token: 0x04000236 RID: 566
			[SerializeField]
			internal InputControlScheme.DeviceRequirement.Flags m_Flags;

			// Token: 0x02000063 RID: 99
			[Flags]
			internal enum Flags
			{
				// Token: 0x04000238 RID: 568
				None = 0,
				// Token: 0x04000239 RID: 569
				Optional = 1,
				// Token: 0x0400023A RID: 570
				Or = 2
			}
		}

		// Token: 0x02000064 RID: 100
		[Serializable]
		internal struct SchemeJson
		{
			// Token: 0x0600046D RID: 1133 RVA: 0x00012E90 File Offset: 0x00011090
			public InputControlScheme ToScheme()
			{
				InputControlScheme.DeviceRequirement[] deviceRequirements = null;
				if (this.devices != null && this.devices.Length != 0)
				{
					int count = this.devices.Length;
					deviceRequirements = new InputControlScheme.DeviceRequirement[count];
					for (int i = 0; i < count; i++)
					{
						deviceRequirements[i] = this.devices[i].ToDeviceEntry();
					}
				}
				return new InputControlScheme
				{
					m_Name = (string.IsNullOrEmpty(this.name) ? null : this.name),
					m_BindingGroup = (string.IsNullOrEmpty(this.bindingGroup) ? null : this.bindingGroup),
					m_DeviceRequirements = deviceRequirements
				};
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x00012F30 File Offset: 0x00011130
			public static InputControlScheme.SchemeJson ToJson(InputControlScheme scheme)
			{
				InputControlScheme.SchemeJson.DeviceJson[] devices = null;
				if (scheme.m_DeviceRequirements != null && scheme.m_DeviceRequirements.Length != 0)
				{
					int count = scheme.m_DeviceRequirements.Length;
					devices = new InputControlScheme.SchemeJson.DeviceJson[count];
					for (int i = 0; i < count; i++)
					{
						devices[i] = InputControlScheme.SchemeJson.DeviceJson.From(scheme.m_DeviceRequirements[i]);
					}
				}
				return new InputControlScheme.SchemeJson
				{
					name = scheme.m_Name,
					bindingGroup = scheme.m_BindingGroup,
					devices = devices
				};
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x00012FB0 File Offset: 0x000111B0
			public static InputControlScheme.SchemeJson[] ToJson(InputControlScheme[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int count = schemes.Length;
				InputControlScheme.SchemeJson[] result = new InputControlScheme.SchemeJson[count];
				for (int i = 0; i < count; i++)
				{
					result[i] = InputControlScheme.SchemeJson.ToJson(schemes[i]);
				}
				return result;
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x00012FF4 File Offset: 0x000111F4
			public static InputControlScheme[] ToSchemes(InputControlScheme.SchemeJson[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int count = schemes.Length;
				InputControlScheme[] result = new InputControlScheme[count];
				for (int i = 0; i < count; i++)
				{
					result[i] = schemes[i].ToScheme();
				}
				return result;
			}

			// Token: 0x0400023B RID: 571
			public string name;

			// Token: 0x0400023C RID: 572
			public string bindingGroup;

			// Token: 0x0400023D RID: 573
			public InputControlScheme.SchemeJson.DeviceJson[] devices;

			// Token: 0x02000065 RID: 101
			[Serializable]
			public struct DeviceJson
			{
				// Token: 0x06000471 RID: 1137 RVA: 0x00013038 File Offset: 0x00011238
				public InputControlScheme.DeviceRequirement ToDeviceEntry()
				{
					return new InputControlScheme.DeviceRequirement
					{
						controlPath = this.devicePath,
						isOptional = this.isOptional,
						isOR = this.isOR
					};
				}

				// Token: 0x06000472 RID: 1138 RVA: 0x00013078 File Offset: 0x00011278
				public static InputControlScheme.SchemeJson.DeviceJson From(InputControlScheme.DeviceRequirement requirement)
				{
					return new InputControlScheme.SchemeJson.DeviceJson
					{
						devicePath = requirement.controlPath,
						isOptional = requirement.isOptional,
						isOR = requirement.isOR
					};
				}

				// Token: 0x0400023E RID: 574
				public string devicePath;

				// Token: 0x0400023F RID: 575
				public bool isOptional;

				// Token: 0x04000240 RID: 576
				public bool isOR;
			}
		}
	}
}
