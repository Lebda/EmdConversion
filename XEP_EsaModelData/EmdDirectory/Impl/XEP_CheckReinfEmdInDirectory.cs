using System;
using System.IO;
using System.Linq;
using System.Text;
using XEP_EsaModelData.EmdDirectory.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdDirectory.Impl
{
    internal class XEP_CheckReinfEmdInDirectory : IXEP_CheckReinfEmdInDirectory
    {
        public XEP_CheckReinfEmdInDirectory()
        {
        }
        
        #region MEMBERS
        static readonly string s_writtenTag = XEP_EmdFileConstants.s_ElementStart + Path.GetFileNameWithoutExtension(XEP_EmdFileNames.s_ReinforcementEmdFileName) + XEP_EmdFileConstants.s_ElementEnd;
        #endregion
        
        #region INTERFACE IMPL
        public void CheckDirectoryLoad(string directoryPath)
        {
            #if SILVERLIGHT
            return;
            #else
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            FileInfo[] files = dirInfo.GetFiles(XEP_EmdFileNames.s_ReinforcementEmdFileName, SearchOption.TopDirectoryOnly);
            if (files.Count() > 1)
            {
                throw new InvalidOperationException("There is more than one " + XEP_EmdFileNames.s_ReinforcementEmdFileName + " file");
            }
            else if (files.Count() == 0)
            {
                // Create the file. 
                using (FileStream fs = File.Create(Path.Combine(directoryPath, XEP_EmdFileNames.s_ReinforcementEmdFileName)))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(s_writtenTag);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            #endif
        }
        public void CheckDirectorySave(string directoryPath, bool isReinforcementInputed)
        {
            #if SILVERLIGHT
            return;
            #else
            if (isReinforcementInputed)
            {
                return;
            }
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            FileInfo[] files = dirInfo.GetFiles(XEP_EmdFileNames.s_ReinforcementEmdFileName, SearchOption.TopDirectoryOnly);
            if (files.Count() > 1)
            {
                throw new InvalidOperationException("There is more than one " + XEP_EmdFileNames.s_ReinforcementEmdFileName + " file");
            }
            else if (files.Count() == 0)
            {
                throw new InvalidOperationException("There is no " + XEP_EmdFileNames.s_ReinforcementEmdFileName + " file");
            }
            files[0].Delete();
            #endif
        }
        #endregion      
    }
}