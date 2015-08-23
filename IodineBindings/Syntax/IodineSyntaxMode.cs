using System;
using System.Collections.Generic;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Ide.CodeCompletion;
using MonoDevelop.Core;
using Mono.TextEditor;
using Mono.TextEditor.Highlighting;

namespace IodineBinding
{
	class IodineSyntaxMode : SyntaxMode
	{
		string GetSyntaxMode ()
		{
			return "IodineSyntaxMode.xml";
		}

		public IodineSyntaxMode ()
		{
			this.DocumentSet += delegate {
				if (this.Document == null)
					return;

				this.Document.FileNameChanged += delegate {
					var provider = new ResourceStreamProvider (typeof(IodineSyntaxMode).Assembly, GetSyntaxMode ());

					var reader = provider.Open ();
					var basemode = SyntaxMode.Read (reader);

					this.rules = new List<Rule> (basemode.Rules);
					this.keywords = new List<Keywords> (basemode.Keywords);
					this.spans = basemode.Spans;
					this.matches = basemode.Matches;
					this.prevMarker = basemode.PrevMarker;
					this.SemanticRules = basemode.SemanticRules;
					this.keywordTable = basemode.keywordTable;
					this.properties = basemode.Properties;
				};
			};
		}


	}
}

