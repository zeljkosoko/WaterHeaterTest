using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WaterHeaterTest.Models
{
    [Serializable]
    [XmlRoot(ElementName = "STEP_PW")]
    public class STEP_PW
    {
        public int Id { get; set; }

        [XmlElement(ElementName = "DEVNR")]
        public string DEVNR { get; set; }

        [XmlElement(ElementName = "ERRCODE")]
        public string ERRCODE { get; set; }

        [XmlElement(ElementName = "FASTPW")]
        public string FASTPW { get; set; }

        [XmlElement(ElementName = "IMIN")]
        public string IMIN { get; set; }

        [XmlElement(ElementName = "IREAL")]
        public string IREAL { get; set; }

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

        [XmlElement(ElementName = "PWMSET")]
        public string PWMSET { get; set; }

        [XmlElement(ElementName = "RMAX")]
        public string RMAX { get; set; }

        [XmlElement(ElementName = "RMIN")]
        public string RMIN { get; set; }

        [XmlElement(ElementName = "RREAL")]
        public string RREAL { get; set; }

        [XmlElement(ElementName = "STARTMODE")]
        public string STARTMODE { get; set; }

        [XmlElement(ElementName = "STEPNR")]
        public string STEPNR { get; set; }

        [XmlElement(ElementName = "STEPTITLE")]
        public string STEPTITLE { get; set; }

        [XmlElement(ElementName = "TESTTIME")]
        public string TESTTIME { get; set; }

        [XmlElement(ElementName = "UDROPMAX")]
        public string UDROPMAX { get; set; }

        [XmlElement(ElementName = "UDROPREAL")]
        public string UDROPREAL { get; set; }

        [XmlElement(ElementName = "UMAX")]
        public string UMAX { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}