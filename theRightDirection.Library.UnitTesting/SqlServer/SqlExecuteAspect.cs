using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Library.UnitTesting.Extensions;
using System.Management.Automation.Runspaces;
namespace theRightDirection.Library.UnitTesting.SqlServer
{
    public class SqlExecuteAspect : IMessageSink, IContextProperty, IContributeObjectSink
    {
        private readonly string _exportDatabasePowershellScriptPath;
        private readonly string _importDatabasePowershellScriptPath;
        private readonly bool _sqlExecuteAspectIsEnabled;
        public SqlExecuteAspect()
        {
            _powerShell = new PowerShellScriptExecuter();
            _sqlExecuteAspectIsEnabled = true;
            _exportDatabasePowershellScriptPath = Assembly.GetExecutingAssembly().FindFileNextToAssembly(@"sqlserver\export database.ps1", true);
            _importDatabasePowershellScriptPath = Assembly.GetExecutingAssembly().FindFileNextToAssembly(@"sqlserver\import database.ps1", true);
            if(string.IsNullOrEmpty(_exportDatabasePowershellScriptPath) || string.IsNullOrEmpty(_importDatabasePowershellScriptPath))
            {
                _sqlExecuteAspectIsEnabled = false;
            }
        }

        private PowerShellScriptExecuter _powerShell;
        private readonly string _name = typeof(SqlExecuteAspect).AssemblyQualifiedName;
        private IMessageSink _nextSink;

        #region IMessageSink Interface

        public IMessageSink NextSink
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
            get { return _nextSink; }
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new NotImplementedException();
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            if (msg == null)
                throw new ArgumentNullException("msg");

            ProcessAttributes(msg);
            IMessage returnMessage;

            try
            {
                // The following line is the call to the actual method we have intercepted. We can do stuff 
                // before we call this or after. This lets us change behaviour.
                returnMessage = _nextSink.SyncProcessMessage(msg);
            }
            finally
            {
                ProcessAnyPostSqlAttributes(msg);
            }

            return returnMessage;

        }

        #endregion IMessageSink Interface

        #region IContextProperty Interface

        public string Name
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
            get { return _name; }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public void Freeze(Context newContext)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }

        #endregion IContextProperty Interface

        #region IContributeObjectSink Interface

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            this._nextSink = nextSink;
            return this;
        }

        #endregion IContributeObjectSink Interface

        #region Helper Methods

        // The pre and post sql attribute processing private methods can probably be Genericized, to 
        // stop code reuse. But this would require we add a common interface or base classes to both
        // attributes. The following is probably be a simpler and neater solution.
        private void ProcessAttributes(IMessage msg)
        {
            if (!_sqlExecuteAspectIsEnabled)
            {
                return;
            }
            PreSqlExecuteAttribute requiredAttribute = GetAttribute<PreSqlExecuteAttribute>(msg);
            if (requiredAttribute != null)
            {
                var servername = requiredAttribute.ServerName;
                var databasename = requiredAttribute.DatabaseName;
                var script = requiredAttribute.SqlScriptToExecute;
                RunPreSqlScript(script, databasename, servername, _exportDatabasePowershellScriptPath);
            }
            return;
        }

        // The pre and post sql attribute processing private methods can probably be Genericized, to 
        // stop code reuse. But this would require we add a common interface or base classes to both
        // attributes. The following is probably be a simpler and neater solution.
        private void ProcessAnyPostSqlAttributes(IMessage msg)
        {
            if (!_sqlExecuteAspectIsEnabled)
            {
                return;
            }
            PostSqlExecuteAttribute requiredAttribute = GetAttribute<PostSqlExecuteAttribute>(msg);
            if (requiredAttribute != null)
            {
                var servername = requiredAttribute.ServerName;
                var databasename = requiredAttribute.DatabaseName;
                RunPostSqlScript(databasename, servername, _importDatabasePowershellScriptPath);
            }
            return;
        }

        private T GetAttribute<T>(IMessage message)
        {
            IMethodMessage messageAsMethodMessage = message as IMethodMessage;

            Type callingType = Type.GetType(messageAsMethodMessage.TypeName);
            MethodInfo methodInfo = callingType.GetMethod(messageAsMethodMessage.MethodName);

            object[] attributes = methodInfo.GetCustomAttributes(typeof(T), true);
            T attribute = default(T);
            if (attributes.Length > 0)
            {
                attribute = (T)attributes[0];
            }
            return attribute;
        }

        private void RunPreSqlScript(string pathToSqlScript, string databasename, string servername, string powershellScript)
        {
            var parameter = new CommandParameter("databaseName", databasename);
            var parameter2 = new CommandParameter("serverName", servername);
            var parameter3 = new CommandParameter("sqlScript", pathToSqlScript);
            var parameters = new List<CommandParameter>();
            parameters.Add(parameter);
            parameters.Add(parameter2);
            parameters.Add(parameter3);
            _powerShell.RunPowerShellScriptFile(powershellScript, parameters);
        }

        private void RunPostSqlScript(string databasename, string servername, string powershellScript)
        {
            var parameter = new CommandParameter("databaseName", databasename);
            var parameter2 = new CommandParameter("serverName", servername);
            var parameters = new List<CommandParameter>();
            parameters.Add(parameter);
            parameters.Add(parameter2);
            _powerShell.RunPowerShellScriptFile(powershellScript, parameters);
        }
        #endregion Helper Methods
    }
}