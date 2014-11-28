using System;
using System.IO;
using System.Linq;
using XEP_EsaModelData.EmdDirectory.Interface;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdDirectory.Impl
{
    internal class XEP_EmdDirectory : IXEP_EmdDirectory
    {
        public XEP_EmdDirectory()
        {
            CrossSectionFile = XEP_EmdFactrory.CreateCrossSectionEmdFile();
            ReinforcementFile = XEP_EmdFactrory.CreateReinforcementEmdFile();
            m_materialEmdFile = XEP_EmdFactrory.CreateMaterialsEmdFile();
            PlaceFile = XEP_EmdFactrory.CreatePlacesEmdFile();
            MemberInfoFile = XEP_EmdFactrory.Create1DMemberInfoEmdFile();
            LoadFile = XEP_EmdFactrory.CreateLoadEmdFile();
            IternalForcesFile = XEP_EmdFactrory.CreateIternalForcesEmdFile();
            m_dirChecker = XEP_EmdFactrory.CreateCheckReinfEmdInDirectory();
        }

        #region MEMBERS
        readonly IXEP_MaterialsEmdFile m_materialEmdFile;
        readonly IXEP_CheckReinfEmdInDirectory m_dirChecker;
        #endregion

        #region PROPERTIES
        public IXEP_CrossSectionEmdFile CrossSectionFile { get; private set; }
        public IXEP_ReinforcementEmdFile ReinforcementFile { get; private set; }
        public IXEP_PlacesEmdFile PlaceFile { get; private set; }
        public IXEP_1DMemberInfoEmdFile MemberInfoFile { get; private set; }
        public IXEP_LoadEmdFile LoadFile { get; private set; }
        public IXEP_IternalForcesEmdFile IternalForcesFile { get; private set; }
        #endregion

        #region INTERFACE IMPL
        public void Load(string emdDirPath)
        {
            m_dirChecker.CheckDirectoryLoad(emdDirPath);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_CrossSectionEmdFileName, CrossSectionFile);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_ReinforcementEmdFileName, ReinforcementFile);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_MaterialsEmdFileName, m_materialEmdFile);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_PlacesEmdFileName, PlaceFile);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_1DMemberInfoEmdFileName, MemberInfoFile);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_LoadEmdFileName, LoadFile);
            OpenAndLoadFile(emdDirPath, XEP_EmdFileNames.s_InternalForcesEmdFileName, IternalForcesFile);
        }
        public void Save(string emdDirPath)
        {
            SaveFile(emdDirPath, XEP_EmdFileNames.s_CrossSectionEmdFileName, CrossSectionFile);
            SaveFile(emdDirPath, XEP_EmdFileNames.s_ReinforcementEmdFileName, ReinforcementFile);
            m_dirChecker.CheckDirectorySave(emdDirPath, ReinforcementFile.IsReinforcementInputed());
        }
        public void CheckReinforcementFile(string emdDirPath)
        {
            m_dirChecker.CheckDirectorySave(emdDirPath, ReinforcementFile.IsReinforcementInputed());
        }
        public void PrepareDirectory(int sectionID)
        {
            ReinforcementFile.PrepareDocument(PlaceFile.GetSectionPos(sectionID), m_materialEmdFile.GetBaseMaterial(XEP_EmdNames.s_Value_eRsteel), MemberInfoFile.GetMemberLength(), sectionID);
            IternalForcesFile.PrepareDocument(sectionID);
        }
        #endregion

        #region METHODS PRIVATE
        static private void OpenAndLoadFile(string emdDirPath, string fileName, IXEP_BaseEmdFile emdFile)
        {
            FileInfo file = new FileInfo(Path.Combine(emdDirPath, fileName));
            if (!file.Exists)
            {
                return;
            }
            using (Stream stream = file.Open(FileMode.Open))
            {
                emdFile.Load(stream);
            }
        }
        static private void SaveFile(string emdDirPath, string fileName, IXEP_BaseEmdFile emdFile)
        {
            FileInfo file = new FileInfo(Path.Combine(emdDirPath + fileName));
            using (Stream stream = (file.Exists) ? file.Open(FileMode.Create) : file.Create())
            {
                emdFile.Save(stream);
            }
        }
        #endregion
    }
}
