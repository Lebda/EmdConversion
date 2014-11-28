﻿using System;
using System.Collections.Generic;
using XEP_EsaModelData.EmdFiles.Interface;
using XEP_EsaModelData.General.Interface;
using XEP_EsaModelData.Infrastructure;

namespace XEP_EsaModelData.EmdFiles.Impl
{
    internal class XEP_MaterialsEmdFile : XEP_BaseEmdFile, IXEP_MaterialsEmdFile
    {
        public XEP_MaterialsEmdFile()
            : base(XEP_EmdNames.s_KeyMaterial)
        {
        }

        #region INTERFACE IMPL
        public string GetBaseMaterial(string matNameEnum)
        {
            List<IXEP_EmdElement> domElems = GetElements(DocumentRoot, XEP_EmdNames.s_KeyMaterial);
            foreach (var item in domElems)
            {
                if (item.GetAttribute(XEP_EmdNames.s_KeyType).Value == matNameEnum)
                {
                    return item.GetAttribute(XEP_EmdNames.s_KeyID).Value;
                }
            }
            return String.Empty;
        }
        #endregion
    }
}
