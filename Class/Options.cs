using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seek4Treasure.Class
{
    class Options
    {
        [Option("lang", Required = false, Default = "all", HelpText = "--lang csharp")]
        public string lang { get; set; }

        [Option("folder", Required = true, HelpText = @"--folder C:\User\Download")]
        public string folder { get; set; }

        [Option("output", Required = false, Default = "false", HelpText = @"--output C:\User\Desktop")]
        public string output { get; set; }

        [Option("excludeFileName", Required = false, Default = "false", HelpText = @"--excludeFileName bootstrap | --excludeFileName bootstrap,query")]
        public string excludeFileName { get; set; }

        [Option("excludeFileWithExtension", Required = false, Default = "false", HelpText = @"--excludeFileWithExtension bootstrap.js 
| --excludeFileWithExtension bootstrap.js,query.js")]
        public string excludeFileWithExtension { get; set; }

        [Option("excludeExtension", Required = false, Default = "false", HelpText = @"--excludeExtension js | --excludeExtension .js,.txt,.cs")]
        public string excludeExtension { get; set; }

    }
}
