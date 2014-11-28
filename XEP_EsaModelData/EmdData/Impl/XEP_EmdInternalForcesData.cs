using System;
using System.Linq;
using XEP_EsaModelData.EmdData.Interface;
using XEP_EsaModelData.EmdFiles.Impl;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdData.Impl
{
    internal class XEP_EmdInternalForcesData : IXEP_EmdInternalForcesData
    {
        public XEP_EmdInternalForcesData()
        {
            ID = 0;
            N = 0.0;
            My = 0.0;
            Mz = 0.0;
            Mx = 0.0;
            Vy = 0.0;
            Vz = 0.0;
            IsCritical = 1;
        }

        public override string ToString()
        {
            return
                  "ID: " + ID.ToString() + "|" +
                  "N: " + N.ToString() + "|" +
                  "My: " + My.ToString() + "|" +
                  "Mz: " + Mz.ToString() + "|" +
                  "Mx: " + Mx.ToString() + "|" +
                  "Vy: " + Vy.ToString() + "|" +
                  "Vz: " + Vz.ToString() + "|" +
                  "IsCritical: " + IsCritical.ToString();
        }

        #region MEMBERS
        readonly static string s_namePrefix = "Co";
        #endregion

        #region PROPERTIES
        public int ID { get; set; }
        public double N { get; set; }
        public double My { get; set; }
        public double Mz { get; set; }
        public double Mx { get; set; }
        public double Vy { get; set; }
        public double Vz { get; set; }
        public int IsCritical { get; set; }
        #endregion

        #region INTERFACE IMPL
        public string Name()
        {
            return s_namePrefix + ID.ToString();
        }
        public IXEP_EmdElement CreateEmdElement()
        {
            IXEP_EmdElement elemEmd = XEP_EmdFactrory.CreateEmdElement();
            elemEmd.Name = XEP_EmdNames.s_KeyInternalForces;
            //
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesID, ID.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesN, N.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesMy, My.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesMz, Mz.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesMx, Mx.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesVy, Vy.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesVz, Vz.ToString()));
            elemEmd.AddAttribute(XEP_EmdFactrory.CreateEmdAttribute(XEP_EmdNames.s_KeyInternalForcesIsCritical, IsCritical.ToString()));
            return elemEmd;
        }
        public void CreateFromEmdElement(IXEP_EmdElement elem)
        {
            XEP_BaseEmdFile.CheckName(elem.Name, XEP_EmdNames.s_KeyInternalForces);
            ID = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesID).GetIntValue();
            N = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesN).GetDoubleValue();
            My = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesMy).GetDoubleValue();
            Mz = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesMz).GetDoubleValue();
            Mx = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesMx).GetDoubleValue();
            Vy = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesVy).GetDoubleValue();
            Vz = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesVz).GetDoubleValue();
            IsCritical = elem.GetAttribute(XEP_EmdNames.s_KeyInternalForcesIsCritical).GetIntValue();
        }
        #endregion
    }
}
