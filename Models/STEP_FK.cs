using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WaterHeaterTest.Models
{
    [Serializable]
    [XmlRoot(ElementName = "STEP_FK")]
    public class STEP_FK
    {
        public int Id { get; set; }

        [XmlElement(ElementName = "CHECKU1")]
        public string CHECKU1 { get; set; }

        [XmlElement(ElementName = "CHECKU2")]
        public string CHECKU2 { get; set; }

        [XmlElement(ElementName = "CHECKU3")]
        public string CHECKU3 { get; set; }

        [XmlElement(ElementName = "COSPHIMAX1")]
        public string COSPHIMAX1 { get; set; }

        [XmlElement(ElementName = "COSPHIMAX2")]
        public string COSPHIMAX2 { get; set; }

        [XmlElement(ElementName = "COSPHIMAX3")]
        public string COSPHIMAX3 { get; set; }

        [XmlElement(ElementName = "COSPHIMIN1")]
        public string COSPHIMIN1 { get; set; }

        [XmlElement(ElementName = "COSPHIMIN2")]
        public string COSPHIMIN2 { get; set; }

        [XmlElement(ElementName = "COSPHIMIN3")]
        public string COSPHIMIN3 { get; set; }

        [XmlElement(ElementName = "COSPHIREAL1")]
        public string COSPHIREAL1 { get; set; }

        [XmlElement(ElementName = "COSPHIREAL2")]
        public string COSPHIREAL2 { get; set; }

        [XmlElement(ElementName = "COSPHIREAL3")]
        public string COSPHIREAL3 { get; set; }

        [XmlElement(ElementName = "DEVNR")]
        public string DEVNR { get; set; }

        [XmlElement(ElementName = "ERRCODE")]
        public string ERRCODE { get; set; }

        [XmlElement(ElementName = "GOODTIME")]
        public string GOODTIME { get; set; }

        [XmlElement(ElementName = "GOODTIMEOK")]
        public string GOODTIMEOK { get; set; }

        [XmlElement(ElementName = "IMAX1")]
        public string IMAX1 { get; set; }

        [XmlElement(ElementName = "IMAX2")]
        public string IMAX2 { get; set; }

        [XmlElement(ElementName = "IMAX3")]
        public string IMAX3 { get; set; }

        [XmlElement(ElementName = "IMIN1")]
        public string IMIN1 { get; set; }

        [XmlElement(ElementName = "IMIN2")]
        public string IMIN2 { get; set; }

        [XmlElement(ElementName = "IMIN3")]
        public string IMIN3 { get; set; }

        [XmlElement(ElementName = "IREAL1")]
        public string IREAL1 { get; set; }

        [XmlElement(ElementName = "IREAL2")]
        public string IREAL2 { get; set; }

        [XmlElement(ElementName = "IREAL3")]
        public string IREAL3 { get; set; }

        [XmlElement(ElementName = "KEEPPOWER")]
        public string KEEPPOWER { get; set; }

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

        [XmlElement(ElementName = "PHASE")]
        public string PHASE { get; set; }

        [XmlElement(ElementName = "PMAX")]
        public string PMAX { get; set; }

        [XmlElement(ElementName = "PMIN")]
        public string PMIN { get; set; }

        [XmlElement(ElementName = "POWERREAL")]
        public string POWERREAL { get; set; }

        [XmlElement(ElementName = "STEPNR")]
        public string STEPNR { get; set; }

        [XmlElement(ElementName = "STEPTITLE")]
        public string STEPTITLE { get; set; }

        [XmlElement(ElementName = "TESTTIME")]
        public string TESTTIME { get; set; }

        [XmlElement(ElementName = "UMAX1")]
        public string UMAX1 { get; set; }

        [XmlElement(ElementName = "UMAX2")]
        public string UMAX2 { get; set; }

        [XmlElement(ElementName = "UMAX3")]
        public string UMAX3 { get; set; }

        [XmlElement(ElementName = "UMIN1")]
        public string UMIN1 { get; set; }

        [XmlElement(ElementName = "UMIN2")]
        public string UMIN2 { get; set; }

        [XmlElement(ElementName = "UMIN3")]
        public string UMIN3 { get; set; }

        [XmlElement(ElementName = "UREAL1")]
        public string UREAL1 { get; set; }

        [XmlElement(ElementName = "UREAL2")]
        public string UREAL2 { get; set; }

        [XmlElement(ElementName = "UREAL3")]
        public string UREAL3 { get; set; }

        [XmlElement(ElementName = "USECOSPHI1")]
        public string USECOSPHI1 { get; set; }

        [XmlElement(ElementName = "USECOSPHI2")]
        public string USECOSPHI2 { get; set; }

        [XmlElement(ElementName = "USECOSPHI3")]
        public string USECOSPHI3 { get; set; }

        [XmlElement(ElementName = "USEPHASE1")]
        public string USEPHASE1 { get; set; }

        [XmlElement(ElementName = "USEPHASE2")]
        public string USEPHASE2 { get; set; }

        [XmlElement(ElementName = "USEPHASE3")]
        public string USEPHASE3 { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}