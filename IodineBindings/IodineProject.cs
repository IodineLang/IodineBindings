using System;
using System.Xml;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace IodineBinding
{
	public class IodineProject : Project
	{
		public IodineProject ()
		{
			
		}

		public IodineProject (ProjectCreateInformation info, XmlElement projectOptions)
			: this ()
		{
			if (info != null) {
				this.Name = info.ProjectName;
			}
			IodineConfiguration releaseConfig = CreateConfiguration ("Release") as IodineConfiguration;
			Configurations.Add (releaseConfig);
		}

		public override System.Collections.Generic.IEnumerable<string> GetProjectTypes ()
		{
			yield return "Iodine";
		}


		public override SolutionItemConfiguration CreateConfiguration (string name)
		{
			return new IodineConfiguration (name);
		}

		public override bool IsCompileable (string file_name)
		{
			return file_name.ToLower ().EndsWith (".id");
		}

		protected override bool OnGetCanExecute (ExecutionContext context, ConfigurationSelector configuration)
		{
			IodineConfiguration config = this.DefaultConfiguration as IodineConfiguration;
			return !string.IsNullOrWhiteSpace (config.MainFile);
		}

		protected override BuildResult DoBuild (IProgressMonitor monitor, ConfigurationSelector configuration)
		{
			return new BuildResult ("Built", 0, 0);
		}

		protected override void DoExecute (IProgressMonitor monitor, ExecutionContext context, ConfigurationSelector configuration)
		{
			IodineConfiguration config = (IodineConfiguration)GetConfiguration (configuration);
			IConsole console = config.ExternalConsole ?
				context.ExternalConsoleFactory.CreateConsole (!config.PauseConsoleOutput) :
				context.ConsoleFactory.CreateConsole (!config.PauseConsoleOutput);

			AggregatedOperationMonitor aggregatedMonitor = new AggregatedOperationMonitor (monitor);
			try {
				string param = string.Format ("\"{0}\" {1}", config.MainFile, config.CommandLineParameters);
			
				IProcessAsyncOperation op = Runtime.ProcessService.StartConsoleProcess ("iodine",
						param, BaseDirectory,
						config.EnvironmentVariables, console, null);

				monitor.CancelRequested += delegate {
					op.Cancel ();
				};

				aggregatedMonitor.AddOperation (op);
				op.WaitForCompleted ();
				monitor.Log.WriteLine ("Iodine exited with code: " + op.ExitCode);
			
			} catch (Exception e) {
				monitor.ReportError (GettextCatalog.GetString ("Cannot execute \"{0}\"", config.MainFile), e);
			} finally {
				console.Dispose ();
				aggregatedMonitor.Dispose ();
			}
		}

	}
}

