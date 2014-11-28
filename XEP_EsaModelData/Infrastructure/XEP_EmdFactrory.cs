using System;
using XEP_EsaModelData.EmdData.Impl;
using XEP_EsaModelData.EmdData.Inputs;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdDirectory.Impl;
using XEP_EsaModelData.EmdDirectory.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Impl;
using XEP_EsaModelData.General.Interface;

namespace XEP_EsaModelData.Infrastructure
{
    static public class XEP_EmdFactrory
    {
        static internal IXEP_EmdFileReader CreateEmdFileReader()
        {
            return new XEP_EmdFileReader();
        }
        static internal IXEP_EmdLine CreateEmdLine()
        {
            return new XEP_EmdLine();
        }
        static internal IXEP_EmdDom CreateEmdLinesParser()
        {
            return new XEP_EmdDom();
        }
        static internal IXEP_Reinf4BarsProxy CreateReinf4BarsProxy(IXEP_EmdElement elem4Work)
        {
            return new XEP_Reinf4BarsProxy(elem4Work);
        }
        static internal IXEP_Reinf4StirrupsProxy CreateReinf4StirrupsProxy(IXEP_EmdElement elem4Work)
        {
            return new XEP_Reinf4StirrupsProxy(elem4Work);
        }
        static internal IXEP_StirrupZonePreparator CreateStirrupZonePreparator()
        {
            return new XEP_StirrupZonePreparator();
        }
        static internal IXEP_CheckReinfEmdInDirectory CreateCheckReinfEmdInDirectory()
        {
            return new XEP_CheckReinfEmdInDirectory();
        }
        //
        static public IXEP_EmdIntendationGetter CreateEmdIntendationGetter()
        {
            return new XEP_EmdIntendationGetter();
        }
        static public IXEP_EmdAttribute CreateEmdAttribute()
        {
            return new XEP_EmdAttribute();
        }
        static public IXEP_EmdAttribute CreateEmdAttribute(string name, string value)
        {
            IXEP_EmdAttribute att = CreateEmdAttribute();
            att.Name = name;
            att.Value = value;
            return att;
        }
        static public IXEP_EmdElement CreateEmdElement()
        {
            return new XEP_EmdElement();
        }
        static public IXEP_EmdDocument CreateEmdDocument()
        {
            return new XEP_EmdDocument();
        }
        // Emd data
        static public IXEP_EmdPointData CreateEmdPointData()
        {
            return new XEP_EmdPointData();
        }
        static public IXEP_EmdShapeData CreateEmdShapeData()
        {
            return new XEP_EmdShapeData();
        }
        static public IXEP_EmdLcsData CreateEmdLcsData()
        {
            return new XEP_EmdLcsData();
        }
        static public IXEP_EmdPrincipalData CreateIEmdPrincipalData()
        {
            return new XEP_EmdPrincipalData();
        }
        static public IXEP_EmdFibreData CreateEmdFibreData()
        {
            return new XEP_EmdFibreData();
        }
        static public IXEP_EmdNameValueData CreateEmdNameValueData()
        {
            return new XEP_EmdNameValueData();
        }
        static public IXEP_EmdGeometryData CreateEmdGeometryData()
        {
            return new XEP_EmdGeometryData();
        }
        static public IXEP_EmdFibresData CreateEmdFibresData()
        {
            return new XEP_EmdFibresData();
        }
        static public IXEP_EmdBarData CreateEmdBarData()
        {
            return new XEP_EmdBarData();
        }
        static public IXEP_EmdStirrupBranchData CreateEmdStirrupBranchData()
        {
            return new XEP_EmdStirrupBranchData();
        }
        static public IXEP_EmdStirrupZoneShapeData CreateEmdStirrupZoneShapeData()
        {
            return new XEP_EmdStirrupZoneShapeData();
        }
        static public IXEP_EmdStirrupData CreateEmdStirrupData()
        {
            return new XEP_EmdStirrupData();
        }
        static public IXEP_CrossSectionEmdFile CreateCrossSectionEmdFile()
        {
            return new XEP_CrossSectionEmdFile();
        }
        static public IXEP_ReinforcementEmdFile CreateReinforcementEmdFile()
        {
            return new XEP_ReinforcementEmdFile();
        }
        static public IXEP_MaterialsEmdFile CreateMaterialsEmdFile()
        {
            return new XEP_MaterialsEmdFile();
        }
        static public IXEP_StirrupZoneIO CreateStirrupZoneIO()
        {
            return new XEP_StirrupZoneIO();
        }
        static public IXEP_BarIO CreateBarIO()
        {
            return new XEP_BarIO();
        }
        static public IXEP_BarIO CreateBarIO(double x, double y, double diam, double area)
        {
            IXEP_BarIO retVal = CreateBarIO();
            retVal.X = x;
            retVal.Y = y;
            retVal.D = diam;
            retVal.Area = area;
            return retVal;
        }
        static public IXEP_EmdStirrupZoneData CreateEmdStirrupZoneData()
        {
            return new XEP_EmdStirrupZoneData();
        }
        static public IXEP_PlacesEmdFile CreatePlacesEmdFile()
        {
            return new XEP_PlacesEmdFile();
        }
        static public IXEP_1DMemberInfoEmdFile Create1DMemberInfoEmdFile()
        {
            return new XEP_1DMemberInfoEmdFile();
        }
        static public IXEP_EmdDirectory CreateEmdDirectory()
        {
            return new XEP_EmdDirectory();
        }
        static public IXEP_EmdInternalForcesData CreateEmdInternalForcesData()
        {
            return new XEP_EmdInternalForcesData();
        }
        static public IXEP_IternalForcesEmdFile CreateIternalForcesEmdFile()
        {
            return new XEP_InternalForcesEmdFile();
        }
        static public IXEP_EmdLoadCombiData CreateEmdLoadData()
        {
            return new XEP_EmdLoadCombiData();
        }
        static public IXEP_LoadEmdFile CreateLoadEmdFile()
        {
            return new XEP_LoadEmdFile();
        }
    }
}