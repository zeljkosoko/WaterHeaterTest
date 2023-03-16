using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WaterHeaterTest.Models
{
    [Serializable]
    [XmlRoot(ElementName ="SPS-RESULTS")]
    public class SPSResults
    {
        [XmlElement(ElementName ="TEST-RESULTS")]
        public List<TEST_RESULTS> TEST_RESULTS_List { get; set; }
    }
}
