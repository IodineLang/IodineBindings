using System;
using Mono.Addins;
using MonoDevelop.Projects;
using MonoDevelop.Core.Serialization;


namespace IodineBinding
{
	public class IodineConfiguration : ProjectConfiguration
	{
		[ItemProperty ("MainFile", DefaultValue = "program.id")]
		string _MainFile = "program.id";
		[ItemProperty ("InterpreterArgs")]
		string _InterpreterArgs = String.Empty;

		public string MainFile {
			set {
				_MainFile = value;
			}
			get {
				return _MainFile;
			}
		}

		public IodineConfiguration ()
		{
		}

		public IodineConfiguration (string name)
		{
			this.Name = name;
		}

		public override void CopyFrom (ItemConfiguration config)
		{
			var iodineConfig = config as IodineConfiguration;
			if (iodineConfig == null)
				throw new ArgumentException ("Not a Iodine configuration", "config");

			base.CopyFrom (config);

			_MainFile = iodineConfig._MainFile;
			_InterpreterArgs = iodineConfig._InterpreterArgs;
		}
	}
}

