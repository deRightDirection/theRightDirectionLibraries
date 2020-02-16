using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
namespace theRightDirection.Library.UnitTesting
{
    internal class PowerShellScriptExecuter
    {
        private WindowsIdentity _identity;

        public PowerShellScriptExecuter()
        {
            _identity = WindowsIdentity.GetCurrent();
        }

        public string Execute(string scriptFile, CommandParameter command)
        {
            var collection = new List<CommandParameter>();
            collection.Add(command);
            return RunPowerShellScriptFile(scriptFile, collection);
        }

        public string RunPowerShellScriptFile(string scriptFile, List<CommandParameter> commands)
        {
            Collection<PSObject> resultCollection;
            string result = null;
            using (Runspace runspace = RunspaceFactory.CreateRunspace())
            {
                using (Pipeline pipeline = runspace.CreatePipeline())
                {
                    pipeline.Commands.Add(scriptFile);
                    var powerShellCommand = pipeline.Commands[0];
                    commands.ForEach(c => powerShellCommand.Parameters.Add(c));
                    using (_identity.Impersonate())
                    {
                        runspace.Open();
                        try
                        {
                            resultCollection = pipeline.Invoke();
                            if (resultCollection.Count > 0)
                            {
                                result = resultCollection[0].ToString();
                            }
                            else
                            {
                                result = "Ok";
                            }
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        runspace.Close();
                    }
                }
            }
            return result;
        }
    }
}