using System.Collections.Generic;
using System.Linq;
using Jopalesha.Common.Infrastructure;

namespace Jopalesha.Common.Hosting
{
    public class HostingOptions
    {
        private readonly string[] _arguments;

        public HostingOptions(string name, string[] args)
        {
            Name = Check.NotNullOrEmpty(name);
            Operation = GetOperation(args);
            Type = GetType(args);
            _arguments = args;
        }

        public string Name { get; }

        public HostingOperation Operation { get; }

        public HostingType Type { get; }

        public IReadOnlyList<string> Arguments => _arguments;

        private static HostingOperation GetOperation(string[] args)
        {
            if (args.Contains(HostingArguments.InstallFlag))
            {
                return HostingOperation.Install;
            }
            if (args.Contains(HostingArguments.UninstallFlag))
            {
                return HostingOperation.Uninstall;
            }
            if (args.Contains(HostingArguments.StartFlag))
            {
                return HostingOperation.Start;
            }
            if (args.Contains(HostingArguments.StopFlag))
            {
                return HostingOperation.Stop;
            }

            return HostingOperation.Run;
        }

        private static HostingType GetType(IEnumerable<string> args)
        {
            if (args.Contains(HostingArguments.RunAsServiceFlag))
            {
                return HostingType.WindowsService;
            }

            return HostingType.Process;
        }
    }
}
