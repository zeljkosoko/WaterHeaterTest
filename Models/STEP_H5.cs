using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WaterHeaterTest.Models
{
    [Serializable]
    [XmlRoot(ElementName = "STEP_H5")]
    public class STEP_H5
    {
        public int Id { get; set; }

        [XmlElement(ElementName = "ARCMAX")]
        public string ARCMAX { get; set; }

        [XmlElement(ElementName = "ARCREAL")]
        public string ARCREAL { get; set; }

        [XmlElement(ElementName = "CONNECT")]
        public string CONNECT { get; set; }

        [XmlElement(ElementName = "DEVNR")]
        public string DEVNR { get; set; }

        [XmlElement(ElementName = "ERRCODE")]
        public string ERRCODE { get; set; }

        [XmlElement(ElementName = "IMAX")]
        public string IMAX { get; set; }

        [XmlElement(ElementName = "IMIN")]
        public string IMIN { get; set; }

        [XmlElement(ElementName = "IREAL")]
        public string IREAL { get; set; }

        [XmlElement(ElementName = "IRMAX")]
        public string IRMAX { get; set; }

        [XmlElement(ElementName = "IRMIN")]
        public string IRMIN { get; set; }

        [XmlElement(ElementName = "ITYPE")]
        public string ITYPE { get; set; }

        [XmlElement(ElementName = "METHOD")]
        public string METHOD { get; set; }

        [XmlElement(ElementName = "MTXMINUS")]
        public string MTXMINUS { get; set; }

        [XmlElement(ElementName = "MTXMINUSSTR")]
        public string MTXMINUSSTR { get; set; }

        [XmlElement(ElementName = "MTXPLUS")]
        public string MTXPLUS { get; set; }

        [XmlElement(ElementName = "MTXPLUSSTR")]
        public string MTXPLUSSTR { get; set; }

        [XmlElement(ElementName = "MYRESULT")]
        public string MYRESULT { get; set; }

        [XmlElement(ElementName = "NRFAILED")]
        public string NRFAILED { get; set; }

        [XmlElement(ElementName = "NRPASSED")]
        public string NRPASSED { get; set; }

        [XmlElement(ElementName = "RAMPDOWN")]
        public string RAMPDOWN { get; set; }

        [XmlElement(ElementName = "RAMPERR")]
        public string RAMPERR { get; set; }

        [XmlElement(ElementName = "RAMPTIME")]
        public string RAMPTIME { get; set; }

        [XmlElement(ElementName = "STEPNR")]
        public string STEPNR { get; set; }

        [XmlElement(ElementName = "STEPTITLE")]
        public string STEPTITLE { get; set; }

        [XmlElement(ElementName = "TESTTIME")]
        public string TESTTIME { get; set; }

        [XmlElement(ElementName = "UNOM")]
        public string UNOM { get; set; }

        [XmlElement(ElementName = "UREAL")]
        public string UREAL { get; set; }

        [XmlElement(ElementName = "USEDARC")]
        public string USEDARC { get; set; }

        [XmlElement(ElementName = "USTART")]
        public string USTART { get; set; }

        [XmlElement(ElementName = "UTYPE")]
        public string UTYPE { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}