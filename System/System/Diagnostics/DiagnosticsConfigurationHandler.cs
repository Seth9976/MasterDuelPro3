using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace System.Diagnostics
{
	/// <summary>Handles the diagnostics section of configuration files.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000180 RID: 384
	[Obsolete("This class is obsoleted")]
	public class DiagnosticsConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DiagnosticsConfigurationHandler" /> class.</summary>
		// Token: 0x06000911 RID: 2321 RVA: 0x00030170 File Offset: 0x0002E370
		public DiagnosticsConfigurationHandler()
		{
			this.elementHandlers["assert"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddAssertNode);
			this.elementHandlers["performanceCounters"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddPerformanceCountersNode);
			this.elementHandlers["switches"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSwitchesNode);
			this.elementHandlers["trace"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddTraceNode);
			this.elementHandlers["sources"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSourcesNode);
		}

		/// <summary>Parses the configuration settings for the &lt;system.diagnostics&gt; Element section of configuration files.</summary>
		/// <returns>A new configuration object, in the form of a <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <param name="parent">The object inherited from the parent path</param>
		/// <param name="configContext">Reserved. Used in ASP.NET to convey the virtual path of the configuration being evaluated.</param>
		/// <param name="section">The root XML node at the section to handle.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Switches could not be found.-or-Assert could not be found.-or-Trace could not be found.-or-Performance counters could not be found.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000912 RID: 2322 RVA: 0x0003021C File Offset: 0x0002E41C
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			IDictionary dictionary;
			if (parent == null)
			{
				dictionary = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				dictionary = (IDictionary)((ICloneable)parent).Clone();
			}
			if (dictionary.Contains(".__TraceInfoSettingsKey__."))
			{
				this.configValues = (TraceImplSettings)dictionary[".__TraceInfoSettingsKey__."];
			}
			else
			{
				dictionary.Add(".__TraceInfoSettingsKey__.", this.configValues = new TraceImplSettings());
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && !(xmlNode.LocalName != "sharedListeners"))
				{
					this.AddTraceListeners(dictionary, xmlNode, this.GetSharedListeners(dictionary));
				}
			}
			foreach (object obj2 in section.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode2.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Comment && nodeType != XmlNodeType.Whitespace)
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
				else if (!(xmlNode2.LocalName == "sharedListeners"))
				{
					DiagnosticsConfigurationHandler.ElementHandler elementHandler = (DiagnosticsConfigurationHandler.ElementHandler)this.elementHandlers[xmlNode2.Name];
					if (elementHandler != null)
					{
						elementHandler(dictionary, xmlNode2);
					}
					else
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000303B0 File Offset: 0x0002E5B0
		private void AddAssertNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "assertuienabled", false, node);
			string attribute2 = this.GetAttribute(attributes, "logfilename", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["assertuienabled"] = bool.Parse(attribute);
				}
				catch (Exception ex)
				{
					throw new ConfigurationException("The `assertuienabled' attribute must be `true' or `false'", ex, node);
				}
			}
			if (attribute2 != null)
			{
				d["logfilename"] = attribute2;
			}
			DefaultTraceListener defaultTraceListener = (DefaultTraceListener)this.configValues.Listeners["Default"];
			if (defaultTraceListener != null)
			{
				if (attribute != null)
				{
					defaultTraceListener.AssertUiEnabled = (bool)d["assertuienabled"];
				}
				if (attribute2 != null)
				{
					defaultTraceListener.LogFileName = attribute2;
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00030498 File Offset: 0x0002E698
		private void AddPerformanceCountersNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "filemappingsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["filemappingsize"] = int.Parse(attribute);
				}
				catch (Exception ex)
				{
					throw new ConfigurationException("The `filemappingsize' attribute must be an integral value.", ex, node);
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003051C File Offset: 0x0002E71C
		private void AddSwitchesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			IDictionary dictionary = new Hashtable();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									this.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									dictionary.Clear();
								}
							}
							else
							{
								string text = this.GetAttribute(attributes, "name", true, xmlNode);
								dictionary.Remove(text);
							}
						}
						else
						{
							string text = this.GetAttribute(attributes, "name", true, xmlNode);
							string attribute = this.GetAttribute(attributes, "value", true, xmlNode);
							dictionary[text] = DiagnosticsConfigurationHandler.GetSwitchValue(text, attribute);
						}
						this.ValidateInvalidAttributes(attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
			d[node.Name] = dictionary;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00030668 File Offset: 0x0002E868
		private static object GetSwitchValue(string name, string value)
		{
			return value;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003066C File Offset: 0x0002E86C
		private void AddTraceNode(IDictionary d, XmlNode node)
		{
			this.AddTraceAttributes(d, node);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, this.configValues.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00030720 File Offset: 0x0002E920
		private void AddTraceAttributes(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "autoflush", false, node);
			string attribute2 = this.GetAttribute(attributes, "indentsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				bool flag = false;
				try
				{
					flag = bool.Parse(attribute);
					d["autoflush"] = flag;
				}
				catch (Exception ex)
				{
					throw new ConfigurationException("The `autoflush' attribute must be `true' or `false'", ex, node);
				}
				this.configValues.AutoFlush = flag;
			}
			if (attribute2 != null)
			{
				int num = 0;
				try
				{
					num = int.Parse(attribute2);
					d["indentsize"] = num;
				}
				catch (Exception ex2)
				{
					throw new ConfigurationException("The `indentsize' attribute must be an integral value.", ex2, node);
				}
				this.configValues.IndentSize = num;
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000307F0 File Offset: 0x0002E9F0
		private TraceListenerCollection GetSharedListeners(IDictionary d)
		{
			TraceListenerCollection traceListenerCollection = d["sharedListeners"] as TraceListenerCollection;
			if (traceListenerCollection == null)
			{
				traceListenerCollection = new TraceListenerCollection();
				d["sharedListeners"] = traceListenerCollection;
			}
			return traceListenerCollection;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00030824 File Offset: 0x0002EA24
		private void AddSourcesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			Hashtable hashtable = d["sources"] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				d["sources"] = hashtable;
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "source")
						{
							this.AddTraceSource(d, hashtable, xmlNode);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000308F0 File Offset: 0x0002EAF0
		private void AddTraceSource(IDictionary d, Hashtable sources, XmlNode node)
		{
			string text = null;
			SourceLevels sourceLevels = SourceLevels.Error;
			StringDictionary stringDictionary = new StringDictionary();
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (!(name == "name"))
				{
					if (!(name == "switchValue"))
					{
						stringDictionary[xmlAttribute.Name] = xmlAttribute.Value;
					}
					else
					{
						sourceLevels = (SourceLevels)Enum.Parse(typeof(SourceLevels), xmlAttribute.Value);
					}
				}
				else
				{
					text = xmlAttribute.Value;
				}
			}
			if (text == null)
			{
				throw new ConfigurationException("Mandatory attribute 'name' is missing in 'source' element.");
			}
			if (sources.ContainsKey(text))
			{
				return;
			}
			TraceSourceInfo traceSourceInfo = new TraceSourceInfo(text, sourceLevels, this.configValues);
			sources.Add(traceSourceInfo.Name, traceSourceInfo);
			foreach (object obj2 in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, traceSourceInfo.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00030A94 File Offset: 0x0002EC94
		private void AddTraceListeners(IDictionary d, XmlNode listenersNode, TraceListenerCollection listeners)
		{
			this.ValidateInvalidAttributes(listenersNode.Attributes, listenersNode);
			foreach (object obj in listenersNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									this.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									this.configValues.Listeners.Clear();
								}
							}
							else
							{
								string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
								this.RemoveTraceListener(attribute);
							}
						}
						else
						{
							this.AddTraceListener(d, xmlNode, attributes, listeners);
						}
						this.ValidateInvalidAttributes(attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00030BA8 File Offset: 0x0002EDA8
		private void AddTraceListener(IDictionary d, XmlNode child, XmlAttributeCollection attributes, TraceListenerCollection listeners)
		{
			string attribute = this.GetAttribute(attributes, "name", true, child);
			string attribute2 = this.GetAttribute(attributes, "type", false, child);
			if (attribute2 == null)
			{
				TraceListener traceListener = this.GetSharedListeners(d)[attribute];
				if (traceListener == null)
				{
					throw new ConfigurationException(string.Format("Shared trace listener {0} does not exist.", attribute));
				}
				if (attributes.Count != 0)
				{
					throw new ConfigurationErrorsException(string.Format("Listener '{0}' references a shared listener and can only have a 'Name' attribute.", attribute));
				}
				traceListener.IndentSize = this.configValues.IndentSize;
				listeners.Add(traceListener);
				return;
			}
			else
			{
				Type type = Type.GetType(attribute2);
				if (type == null)
				{
					throw new ConfigurationException(string.Format("Invalid Type Specified: {0}", attribute2));
				}
				string attribute3 = this.GetAttribute(attributes, "initializeData", false, child);
				object[] array;
				Type[] array2;
				if (attribute3 != null)
				{
					array = new object[] { attribute3 };
					array2 = new Type[] { typeof(string) };
				}
				else
				{
					array = null;
					array2 = Type.EmptyTypes;
				}
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
				if (type.Assembly == base.GetType().Assembly)
				{
					bindingFlags |= BindingFlags.NonPublic;
				}
				ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, array2, null);
				if (constructor == null)
				{
					throw new ConfigurationException("Couldn't find constructor for class " + attribute2);
				}
				TraceListener traceListener2 = (TraceListener)constructor.Invoke(array);
				traceListener2.Name = attribute;
				string attribute4 = this.GetAttribute(attributes, "traceOutputOptions", false, child);
				if (attribute4 != null)
				{
					if (attribute4 != attribute4.Trim())
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					TraceOptions traceOptions;
					try
					{
						traceOptions = (TraceOptions)Enum.Parse(typeof(TraceOptions), attribute4);
					}
					catch (ArgumentException)
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					traceListener2.TraceOutputOptions = traceOptions;
				}
				string[] supportedAttributes = traceListener2.GetSupportedAttributes();
				if (supportedAttributes != null)
				{
					foreach (string text in supportedAttributes)
					{
						string attribute5 = this.GetAttribute(attributes, text, false, child);
						if (attribute5 != null)
						{
							traceListener2.Attributes.Add(text, attribute5);
						}
					}
				}
				traceListener2.IndentSize = this.configValues.IndentSize;
				listeners.Add(traceListener2);
				return;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		private void RemoveTraceListener(string name)
		{
			try
			{
				this.configValues.Listeners.Remove(name);
			}
			catch (ArgumentException)
			{
			}
			catch (Exception ex)
			{
				throw new ConfigurationException(string.Format("Unknown error removing listener: {0}", name), ex);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00030E28 File Offset: 0x0002F028
		private string GetAttribute(XmlAttributeCollection attrs, string attr, bool required, XmlNode node)
		{
			XmlAttribute xmlAttribute = attrs[attr];
			string text = null;
			if (xmlAttribute != null)
			{
				text = xmlAttribute.Value;
				if (required)
				{
					this.ValidateAttribute(attr, text, node);
				}
				attrs.Remove(xmlAttribute);
			}
			else if (required)
			{
				this.ThrowMissingAttribute(attr, node);
			}
			return text;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00030E6D File Offset: 0x0002F06D
		private void ValidateAttribute(string attribute, string value, XmlNode node)
		{
			if (value == null || value.Length == 0)
			{
				throw new ConfigurationException(string.Format("Required attribute '{0}' cannot be empty.", attribute), node);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00030E8C File Offset: 0x0002F08C
		private void ValidateInvalidAttributes(XmlAttributeCollection c, XmlNode node)
		{
			if (c.Count != 0)
			{
				this.ThrowUnrecognizedAttribute(c[0].Name, node);
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00030EA9 File Offset: 0x0002F0A9
		private void ThrowMissingAttribute(string attribute, XmlNode node)
		{
			throw new ConfigurationException(string.Format("Required attribute '{0}' not found.", attribute), node);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00030EBC File Offset: 0x0002F0BC
		private void ThrowUnrecognizedNode(XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized node `{0}'; nodeType={1}", node.Name, node.NodeType), node);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00030EDF File Offset: 0x0002F0DF
		private void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized element '{0}'.", node.Name), node);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00030EF7 File Offset: 0x0002F0F7
		private void ThrowUnrecognizedAttribute(string attribute, XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized attribute '{0}' on element <{1}/>.", attribute, node.Name), node);
		}

		// Token: 0x040006F6 RID: 1782
		private TraceImplSettings configValues;

		// Token: 0x040006F7 RID: 1783
		private IDictionary elementHandlers = new Hashtable();

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x06000927 RID: 2343
		private delegate void ElementHandler(IDictionary d, XmlNode node);
	}
}
