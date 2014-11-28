using System.IO;
using System.Xml.Linq;

namespace XEP_EsaModelData.General.Interface
{
    public interface IXEP_EmdDocument
    {
        void Save(Stream stream);
        IXEP_EmdElement Root { get; }
        void Load(Stream stream);
        void Load(XDocument xDocument);
    }
}