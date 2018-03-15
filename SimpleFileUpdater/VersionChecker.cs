using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileUpdater
{
    class VersionChecker
    {
        public bool NewVersionExists(string localVersion, string versionFromServer)
        {
            Version verLocal = new Version(localVersion);
            Version verWeb = new Version(versionFromServer);
            return verLocal < verWeb;
        }
    }
}
